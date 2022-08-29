using System;
using System.Transactions;
using System.Data.SqlClient;
using TP2SI2.concrete;

namespace TP2SI2.dal
{
    interface IContext : IDisposable
    {
        void Open();
        SqlCommand createCommand();
        void EnlistTransaction();
    }

}
