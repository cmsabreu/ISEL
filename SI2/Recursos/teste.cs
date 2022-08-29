class DBHelper{
    public static string DBString { get =>
        ConfigurationManager.ConnectionStrings["ex4Bd"].ConnectionString; }
}
class ManutencaoMapper{
    public void InsereManutencao(Manutencao m){
        string cs = DBHelper.DBString;
        var options = new TransactionOptions();
        options.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, options)){
            using (SqlConnection con = new SqlConnection(cs)){
                con.Open();
                using (SqlCommand cmd = con.CreateCommand()){
                    if (m.itens.Count == 0)
                        throw new Exception("Manutencao Tem de ter itens a inserir."); 
                    addParametersToCommand(cmd);
                    foreach (var item in m.itens){
                        setValuesInCmdParameters(m, item, cmd);
                        executeCommand(cmd);
                    }
                }
            }
            ts.Complete();
        }
    }
    private void addParametersToCommand(SqlCommand cmd){
        cmd.CommandText = "insereManutencaoItem";
        cmd.CommandType = CommandType.StoredProcedure;
        SqlParameter paramMatricula = new SqlParameter("@matricula", SqlDbType.VarChar);
        cmd.Parameters.Add(paramMatricula);
        SqlParameter paramKms = new SqlParameter("@kms", SqlDbType.Int);
        cmd.Parameters.Add(paramKms);
        SqlParameter paramData = new SqlParameter("@data", SqlDbType.Date);
        cmd.Parameters.Add(paramData);
        SqlParameter paramNLinha = new SqlParameter("@nLinha", SqlDbType.Int);
        cmd.Parameters.Add(paramNLinha);
        SqlParameter paramValor = new SqlParameter("@valor", SqlDbType.Decimal);
        cmd.Parameters.Add(paramValor);
    }
    private void setValuesInCmdParameters(Manutencao m, ManutencaoItem item, SqlCommand cmd){
       cmd.Parameters[0].Value = m.matricula;
       cmd.Parameters[1].Value = m.km;
       cmd.Parameters[2].Value = m.data.Date;
       cmd.Parameters[3].Value = item.nLinha;
       cmd.Parameters[4].Value = item.valor;
    }
    private static void executeCommand(SqlCommand cmd){
        cmd.Prepare();
        cmd.ExecuteNonQuery();
    }
}

