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
using System.Data.Entity; //For EntityState
using System.Data.Entity.Infrastructure; //For DbEntityEntry
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_trackChanges
{
    class Program
    {

        static void Main(string[] args)
        {
            
            using(var ctx= new School())
            {
                
                //selecting a student using LINQ to Entities
                var st = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();

                st.name = "Modified";
                st.name = "John";

                //track changes access
                DbEntityEntry<Student> entry=ctx.Entry<Student>(st);
                DbPropertyValues original = entry.OriginalValues;
                DbPropertyValues current = entry.CurrentValues;
                EntityState state = entry.State;
                Console.WriteLine("State:{0}\nOriginal:{1}\nCurrent:{2}",
                    state,original.GetValue<string>("name"),current.GetValue<string>("name"));


                var stnew = new Student() { name = "new", sex = "F", country = 1 };
                ctx.Students.Add(stnew);

                DbEntityEntry<Student> newentry = ctx.Entry<Student>(stnew);
                Console.WriteLine("\nState:{0}\nOriginal:{1}\nCurrent:{2}",
                    newentry.State, "None", newentry.CurrentValues.GetValue<string>("name")); //no original values for new entries

                var stdel = (from sts in ctx.Students
                          where sts.studentId == 5 //3
                          select sts).SingleOrDefault();
                ctx.Students.Remove(stdel);
                DbEntityEntry<Student> delentry = ctx.Entry<Student>(stdel);
                Console.WriteLine("\nState:{0}\nOriginal:{1}\nCurrent:{2}",
                    delentry.State, delentry.OriginalValues.GetValue<string>("name"), "None"); //no current values for deleted entries



                ctx.SaveChanges();
                Console.WriteLine("\n\nState:{0}\nOriginal:{1}\nCurrent:{2}",
                    entry.State, original.GetValue<string>("name"), current.GetValue<string>("name"));
                Console.WriteLine("\nState:{0}\nOriginal:{1}\nCurrent:{2}",
                    newentry.State, newentry.OriginalValues.GetValue<string>("name"), newentry.CurrentValues.GetValue<string>("name"));
                
            }
            
           /*
           
            using (var ctx = new School())
            {
                //tracking
                Student st = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
                DbEntityEntry<Student> entryst = ctx.Entry<Student>(st);
                st.name = "Another Name, with tracking";

                //No Tracking LINQ to Entities queries
                var ntst = ctx.Students.AsNoTracking().Where(s => s.studentId == 2).SingleOrDefault();
                DbEntityEntry<Student> entryntst = ctx.Entry<Student>(ntst);
                ntst.name = "New Name, without tracking";

                Console.WriteLine("\nWith tracking: {0}, No tracking:{1}", entryst.State, entryntst.State);

            }*/
        
            
        }
    }
}
