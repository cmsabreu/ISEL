/*
*   ISEL-ADEETC-SI2
*   ND 2015-2016
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/

USE SI2;
IF OBJECT_ID('dbo.xferContext ') IS NOT NULL
	DROP TABLE dbo.xferContext
IF OBJECT_ID('dbo.Account') IS NOT NULL
	DROP TABLE dbo.Account
	
CREATE TABLE dbo.xferContext
(
	id INT PRIMARY KEY,
	STATE INT
)

CREATE TABLE dbo.Account 
(
	id INT PRIMARY KEY IDENTITY(1,1),
	balance NUMERIC(10,2)
)

INSERT INTO dbo.Account VALUES (100),(200);

GO
IF OBJECT_ID('credit') IS NOT NULL
	DROP PROC credit;
GO
CREATE PROC credit @creditAcc INT, @value NUMERIC(7,2)
AS
	SET NOCOUNT ON
	UPDATE dbo.account SET balance = balance+@value
	WHERE id = @creditAcc
	IF @@ROWCOUNT <> 1
		RETURN -1 -- erro ao actualizar o saldo --- conta inexistente
	RETURN 0
GO

GO
IF OBJECT_ID('debit') IS NOT NULL
	DROP PROC debit;
GO
CREATE PROC debit @debitAcc INT, @value NUMERIC(7,2)
AS
	SET NOCOUNT ON
	UPDATE dbo.account SET balance = balance-@value
	WHERE id = @debitAcc AND balance>=@value
	IF @@ROWCOUNT <> 1
		RETURN -1 -- erro ao actualizar o saldo --- conta inexistente ou saldo insuficiente
	RETURN 0
GO


GO
IF OBJECT_ID('xFer') IS NOT NULL
	DROP PROC xFer;
GO
CREATE PROC xFer @idxfer INT, @debitAcc INT, @creditAcc INT, @value NUMERIC(5,2)
AS
DECLARE @state INT, @nextstate INT
DECLARE @error INT
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
SET NOCOUNT ON
BEGIN TRAN
	IF NOT EXISTS (SELECT id FROM xferContext WHERE id = @idxfer)
	BEGIN
		INSERT INTO xferContext VALUES(@idxfer,0) -- state 0 - debitar
	END
COMMIT
SELECT @state = STATE FROM xferContext
WHERE id = @idxfer
WHILE @state <> 2 AND @state <> 4
BEGIN
--########## BEGIN STATE 0 DEBIT ##########
	IF @state = 0 
	BEGIN
		BEGIN TRAN
			EXEC @error = debit @debitAcc, @value -- correr T1 (debitar)
			IF @error = 0
				SET @nextstate = 1 -- creditar
			ELSE
				SET @nextstate = 4 -- Fim (Erro)
			UPDATE xferContext SET STATE = @nextstate WHERE id = @idxfer
			IF @@ROWCOUNT <> 1
			BEGIN
				ROLLBACK
				RAISERROR (N'Severe unknown error. Try again!',
							10, -- Severity,
							-1) -- State,
				RETURN -1 -- erro ao actualizar o contexto (não se pode fazer nada)
			END
		COMMIT
	END

--########## END STATE 0 DEBIT ##########
--########## BEGIN STATE 1 CREDIT ##########
	IF @state = 1 
	BEGIN
		BEGIN TRAN
			EXEC @error = credit @creditAcc, @value -- correr T2 (creditar)
			IF @error = 0 
				SET @nextstate = 2 -- FIM, com sucesso
			ELSE
				SET @nextstate = 3 -- Compensar debito

			UPDATE xferContext SET STATE = @nextstate WHERE id = @idxfer
			IF @@ROWCOUNT <> 1
			BEGIN
				ROLLBACK
				RAISERROR (N'Severe unknown error. Try again!',
							10, -- Severity,
							-1) -- State,
				RETURN -1 -- erro ao actualizar o contexto (não se pode fazer nada)
			END
		COMMIT
	END

--########## END STATE 1 CREDIT ##########

--########## BEGIN COMPENSATE STATE 1: do a CREDIT ##########
	IF @state = 3
	BEGIN
		BEGIN TRAN
			EXEC @error = credit @debitAcc, @value -- correr T2 (creditar)
			IF @error = 0
				SET @nextstate = 4 -- FIM, com sucesso
			ELSE
				SET @nextstate = 3 -- Erro a compensar o débito. Manter estado

			UPDATE xferContext SET STATE = @nextstate WHERE id = @idxfer
			IF @@ROWCOUNT <> 1
			BEGIN
				ROLLBACK
				RAISERROR (N'Severe unknown error. Try again!',
							10, -- Severity,
							-1) -- State,
				RETURN -1 -- erro ao actualizar o contexto (não se pode fazer nada)
			END
		COMMIT
	END

--########## END COMPENSATE STATE 1: do a CREDIT ##########
	IF @state NOT IN (0,1,3)
	BEGIN
	RAISERROR (N'Internal error',
				10, -- Severity,
				-4) -- State,
	RETURN -4 -- estado indeterminado
	END
	SET @state = @nextstate
END
IF @state = 2
BEGIN
	RAISERROR (N'Transfer done',5,@state)
	RETURN @state
END
IF @state = 4
BEGIN
	RAISERROR (N'Error during transfer',5,@state)
	RETURN @state
END



GO
