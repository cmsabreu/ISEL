/*
*   ISEL-ADEETC-SI2
*   ND 2014-2019
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04___Quering
{
    class Program
    {
        #region HELPER
        static void print<T>(IEnumerable<T> q)
        {
            Console.WriteLine();
            foreach (var s in q)
            {
                Console.WriteLine(s.ToString());
            }
        }
        

        #endregion
        static void Main(string[] args)
        {


            using (var ctx = new School())
            {
                //Using LINQ Query syntax
                var stLINQ = (from sts in ctx.Students
                              select sts);
                print<Student>(stLINQ);


                //Using SqlQuery queries
                var q1 = ctx.Database.SqlQuery<Student>("select * from student");
                print<Student>(q1);

                //Using SqlQuery queries
                var q2 = ctx.Students.SqlQuery("select * from student");
                print<Student>(q2);

                //Valid
                var q3 = ctx.Database.SqlQuery<Student>("select distinct s.* from Student s inner join StudentCourse c on (c.studentid = s.studentid)");
                print<Student>(q3);
                
                //valid
                var q4 = ctx.Students.SqlQuery("select distinct s.* from Student s inner join StudentCourse c on (c.studentid = s.studentid)");
                print<Student>(q4);
                
                //invalid
                var q5 = ctx.Students.SqlQuery("select s.name, c.name from Student s inner join Country c on (c.countryid = s.country)");
                foreach (var s in q5)
                {
                    Console.WriteLine("???");
                }     
                
            } 




        }
    }
}
