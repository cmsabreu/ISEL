/*
*   ISEL-ADEETC-SI2
*   ND 2014-2019
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/
//#define CASE1
#define CASE2
#define CASE3
#define CASE4
#define CASE5

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace _03_LazyLoading
{

    class Program
    {
        static void Main(string[] args)
        {
#if CASE1
            using (var ctx = new SchoolLazy())
            {
                  Console.WriteLine("LazyLoadingEnabled = false");
                ctx.Configuration.LazyLoadingEnabled = false;
                var student = (from s in ctx.Students
                                    where s.studentId == 1
                                    select s).FirstOrDefault<Student>();

                Console.WriteLine("{0} from {1} - {2} courses", student.name, "???", student.Courses.Count); // cannot acess eagerStudent.OriginCountry.name withou loading it first;
               //no courses are loaded 
                foreach (var c in student.Courses)
                {
                    Console.WriteLine("{0}", c.name);
                }
            }
#endif
#if CASE2
            using (var ctx = new SchoolLazy())
            {
                Console.WriteLine("\n\nLazyLoadingEnabled = false\n");
                ctx.Configuration.LazyLoadingEnabled = false;
                //notice the include statement. It will load all related courses
                var eagerStudent = ctx.Students.Include("Courses").Where(s => s.studentId == 1).FirstOrDefault();
                

                Console.WriteLine("{0} from {1}", eagerStudent.name, "???"); // cannot acess eagerStudent.OriginCountry.name without loading it first;

                foreach (var c in eagerStudent.Courses)
                {
                    Console.WriteLine("{0}", c.name);
                }
            }
#endif
#if CASE3
            //With LazyLoading Enabled
            using (var ctx = new SchoolLazy())
            {
                ctx.Configuration.LazyLoadingEnabled = true;
                Console.WriteLine("\n\nLazyLoadingEnabled = true\n");

                var lazystudent = (from s in ctx.Students
                                   where s.studentId == 1
                                   select s).FirstOrDefault<Student>();
                Console.WriteLine("{0} from {1}", lazystudent.name, lazystudent.OriginCountry.name); 
                foreach (var c in lazystudent.Courses)
                {
                    Console.WriteLine("{0}", c.name);
                }

            }
#endif
#if CASE4
            //with explicit loading
            using (var ctx = new SchoolLazy())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                Console.WriteLine("\n\nLazyLoadingEnabled = false, with explicit loading\n");
                var student = (from s in ctx.Students
                                   where s.studentId == 1
                                   select s).FirstOrDefault<Student>();
                //loading Country before using it, unless an exception is raised
                ctx.Entry(student).Reference(c => c.OriginCountry).Load();
                Console.WriteLine("{0} from {1}", student.name, student.OriginCountry.name);
                //loadding related Courses
                ctx.Entry(student).Collection("Courses").Load();
                foreach (var c in student.Courses)
                {
                    Console.WriteLine("{0}", c.name);
                }

            }
#endif

#if CASE5
            using (var ctx = new SchoolLazy())
            {
                Student noproxy = new Student(); // Not a proxy
                Console.WriteLine(noproxy.GetType().ToString());

                var st = (from a in ctx.Students    // proxy
                          where a.studentId == 1
                          select a)
                        .SingleOrDefault();
                Console.WriteLine(st.GetType().ToString());

                Student proxy = ctx.Students.Create();  // proxy

                Console.WriteLine(proxy.GetType().ToString());
            }
#endif
        }
    }
}
