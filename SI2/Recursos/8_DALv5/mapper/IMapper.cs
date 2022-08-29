﻿/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*/

namespace DAL.mapper.interfaces
{
    public interface IMapper<T, Tid, TCol> 
    {
        T Create(T entity);
        T Read(Tid id);
        TCol ReadAll();
        T Update(T entity);
        T Delete(T entity);
    }
}