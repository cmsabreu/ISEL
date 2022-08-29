/*
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
using DAL.mapper.interfaces;
using DAL.model;
using System.Data;
using System.Data.SqlClient;

namespace DAL.mapper.concrete
{
    class CountryMapper : ICountryMapper
    {
        private IContext context;
        public CountryMapper(IContext ctx)
        { context = ctx; }
        public Country Create(Country entity)
        {
            using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "INSERT INTO Country (Name) output INSERTED.countryId  VALUES(@Name)";
                SqlParameter p = new SqlParameter("@Name", entity.Name);
                cmd.Parameters.Add(p);
                entity.Id = (int)cmd.ExecuteScalar();
                  
            }
            
            return entity;

        }
        public Country Read(int id)
        {
            Country c = null;
            using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "select name from Country where countryId=@id";
                SqlParameter p = new SqlParameter("@id", id);
                cmd.Parameters.Add(p);
                    
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        c = new Country();
                        c.Id = id;
                        c.Name = rd.GetString(0);
                    }
                }
            }
            return c;
        }
        public void Update(Country entity)
        {
            using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "update Country set name=@name where countryId=@id";
                SqlParameter p1 = new SqlParameter("@id", entity.Id);
                SqlParameter p2 = new SqlParameter("@name", entity.Name);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.ExecuteNonQuery();
            }
          
        }
        public void Delete(Country entity)
        {
            using (SqlCommand cmd = context.createCommand())
            {
                cmd.CommandText = "delete from Country where countryId=@id";
                SqlParameter p1 = new SqlParameter("@id", entity.Id);
                cmd.Parameters.Add(p1);
                cmd.ExecuteNonQuery();
            }
           
        }
    }
}
