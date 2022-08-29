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

namespace _05___SP
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new school())
            {
                //It is necessary to edit the return type of GetCoursesByStudentId to Course
                var cs = ctx.GetCoursesByStudentId(1);
                foreach (var c in cs)
                {
                    Console.WriteLine(c.name);
                }
            }
        }
    }
}
