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
using System;
using System.Data.SqlClient;
using DAL.mapper.interfaces;

namespace DAL
{
    interface IContext: IDisposable
    {
        void Open();
        SqlCommand createCommand();

    }
}
