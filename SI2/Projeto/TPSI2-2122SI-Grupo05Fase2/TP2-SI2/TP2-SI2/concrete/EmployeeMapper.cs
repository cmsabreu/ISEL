using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TP2SI2.dal;
using TP2SI2.mapper;
using TP2SI2.mapper.interfaces;
using TP2SI2.model;

namespace TP2SI2.concrete
{
    class EmployeeMapper : AbstractMapper<Employee, int?, List<Employee>>, IEmployeeMapper
    {
        public EmployeeMapper(IContext ctx) : base(ctx)
        {
        }
        protected override string SelectAllCommandText
        {
            get
            {
                return "select ssn, f_name, l_name, birth_date, address, postal_code, city, job, phone_number, mail from employee";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0} where ssn=@ssn", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update employee set f_name=@f_name, l_name=@l_name, birth_date=@birth_date, " +
                    "address=@address, postal_code=@postal_code, city=@city, job=@job, phone_number=@phone_number," +
                    " mail=@mail where ssn=@ssn";
            }
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from employee where ssn=@ssn";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO employee (ssn, f_name, l_name, birth_date, address, postal_code, city, job, phone_number, mail) " +
                    "VALUES(@ssn, @f_name, @l_name, @birth_date, @address, @postal_code, @city, @job, @phone_number, @mail); ";
            }
        }

        protected override void DeleteParameters(IDbCommand command, Employee e)
        {
            SqlParameter p1 = new SqlParameter("@ssn", e.Ssn);
            command.Parameters.Add(p1);
        }
        protected override void InsertParameters(IDbCommand command, Employee e)
        {
            SqlParameter p = new SqlParameter("@ssn", SqlDbType.Int);

            SqlParameter p1 = new SqlParameter("@fName", e.FName);
            SqlParameter p2 = new SqlParameter("@lName", e.LName);
            SqlParameter p3 = new SqlParameter("@birthDate", e.BirthDate);
            SqlParameter p4 = new SqlParameter("@address", e.Address);
            SqlParameter p5 = new SqlParameter("@postalCode", e.PostalCode);
            SqlParameter p6 = new SqlParameter("@city", e.City);
            SqlParameter p7 = new SqlParameter("@job", e.Job);
            SqlParameter p8 = new SqlParameter("@phoneNumber", e.PhoneNumber);
            SqlParameter p9 = new SqlParameter("@mail", e.Mail);

           
            p.Direction = ParameterDirection.InputOutput;

            if (e.Ssn != null)
                p.Value = e.Ssn;
            else
                p.Value = DBNull.Value;
            command.Parameters.Add(p);
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.Parameters.Add(p4);
            command.Parameters.Add(p5);
            command.Parameters.Add(p6);
            command.Parameters.Add(p7);
            command.Parameters.Add(p8);
            command.Parameters.Add(p9);

        }

        protected override Employee Map(IDataRecord record)
        {
            Employee e = new Employee();
            e.Ssn = record.GetInt32(0);
            e.FName = record.GetString(1);
            e.LName = record.GetString(2);
            e.BirthDate = record.GetDateTime(3);
            e.Address = record.GetString(4);
            e.PostalCode = record.GetString(5);
            e.City = record.GetString(6);
            e.Job = record.GetString(7);
            e.PhoneNumber = record.GetInt32(8);
            e.Mail = record.GetString(9);

            return e;
        }

        protected override void SelectParameters(IDbCommand command, int? k)
        {
            SqlParameter p1 = new SqlParameter("@ssn", k);
            command.Parameters.Clear();
            command.Parameters.Add(p1);
        }

        protected override Employee UpdateEntityID(IDbCommand cmd, Employee e)
        {
            return e;
        }

        protected override void UpdateParameters(IDbCommand command, Employee e)
        {
            InsertParameters(command, e);
        }
    }
}
