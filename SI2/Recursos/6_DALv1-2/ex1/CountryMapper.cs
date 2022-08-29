using DAL.mapper.interfaces;
using DAL.model;
using System.Data;
using System.Data.SqlClient;

namespace DAL.mapper.concrete
{
    class CountryMapper : ICountryMapper
    {
        private string connectionString;
        public CountryMapper(string cs)
        { connectionString = cs; }
        public void Create(Country entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Country (Name) VALUES(@Name)";
                    SqlParameter p = new SqlParameter("@Name", entity.Name);
                    cmd.Parameters.Add(p);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }                
            }
        }
        public Country Read(int id)
        {
            Country c = null;
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "select name from Country where countryId=@id";
                    SqlParameter p = new SqlParameter("@id", id);
                    cmd.Parameters.Add(p);
                    con.Open();
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
            }
            return c;
        }
        public void Update(Country entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "update Country set name=@name where countryId=@id";
                    SqlParameter p1 = new SqlParameter("@id", entity.Id);
                    SqlParameter p2 = new SqlParameter("@name", entity.Name);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Country entity)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "delete from Country where countryId=@id";
                    SqlParameter p1 = new SqlParameter("@id", entity.Id);
                    cmd.Parameters.Add(p1);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
