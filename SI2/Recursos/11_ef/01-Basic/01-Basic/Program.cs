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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EF DEMO 01");
            using(var ctx = new EF_DEMOEntities())
            {
                foreach(var student in ctx.Students)
                {
                    Console.WriteLine(string.Format("{0} - {1}",student.studentId, student.name));
                }

                ctx.Students.Add(new Student()
                                {
                                    sex = "M",
                                    dateBirth=DateTime.Now,
                                    name="Tancredo"
                                });

                ctx.SaveChanges();
            }
        }
    }
}
