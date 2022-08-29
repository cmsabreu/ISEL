/*
*   ISEL-ADEETC-SI2
*   ND 2014-2019
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/
#define _IGNOREDISCONNECTED
//#define _IGNORE
//#define _CLIENTWINS



using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;//for DbUpdateConcurrencyException
using System.Linq;
using System.Text;
using System.Threading;

namespace _06___Concurrency
{
    class Program
    {
        static void Main(string[] args)
        {
            Student st1 = null, st2 = null;
#if _IGNOREDISCONNECTED
            //################ Ignoring Concurrency Conflicts in disconnected mode
            //Test with concurrency mode=none and then mode=fixed
            using (var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                //selecting a student using LINQ to Entities
                st1 = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
                Console.Write("Original values-> {0}",st1.name);
            }

            using (var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                //selecting a student using LINQ to Entities
                st2 = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
                Console.WriteLine("  {0}",st2.dateBirth);
            }



            //Disconnected changes
            st1.name = "Not Just John";
            st2.dateBirth = null;

            //Updating the name
            using (var ctx = new School())
            {

                try
                {
                    ctx.Entry(st1).State = System.Data.EntityState.Modified;
                    ctx.SaveChanges();  //OK 
                    Console.WriteLine("Name changed to '{0}'", st1.name);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e.Message);

                    var entry = e.Entries.Single();
                    var dbValues = entry.GetDatabaseValues();
                    var curValues = entry.CurrentValues;

                    Console.WriteLine("DB:{0} Current:{1}", dbValues.GetValue<string>("name"), curValues.GetValue<string>("name"));

                }
                
            }

            //Updating the date of birth
            using (var ctx = new School())
            {
                
                try 
                {
                    ctx.Entry(st2).State = System.Data.EntityState.Modified;                    
                    ctx.SaveChanges();  //OK    
                    Console.WriteLine("Date of Birth changed to null");
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e.Message);
                    
                    var entry = e.Entries.Single();
                    var dbValues = entry.GetDatabaseValues();
                    var curValues = entry.CurrentValues;

                    Console.WriteLine("DB:{0} Current:{1}", dbValues.GetValue<DateTime>("dateBirth"), curValues.GetValue<DateTime>("dateBirth"));

                }
            }

            //see what is now on the database
            using (var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                //selecting a student using LINQ to Entities
                st1 = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
                Console.WriteLine("Actual values-> {0} {1}", st1.name, st1.dateBirth == null ? "NULL" : st1.dateBirth.ToString());
            }

#endif
            
#if _IGNORE
            //################ Ignoring Concurrency Conflicts in connected mode
            //Test with concorrency mode=none and then, test again with concurrency=fixed (right click Student.name at School.edmx )
            //Re-create the model
            using (var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = true;
                //selecting a student using LINQ to Entities
                st1 = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
                Console.WriteLine("Original values-> {0} {1}", st1.name, st1.dateBirth == null ? "NULL" : st1.dateBirth.ToString());
                st1.name = "Not Just John";
                //make un update in other transaction to student date of birth
                Console.WriteLine("Now");
                Thread.Sleep(8000);
                ctx.SaveChanges();
                Console.WriteLine("Name changed to '{0}'", st1.name);
            }
            using (var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                //selecting a student using LINQ to Entities
                st1 = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
                //see what was changed...
                Console.WriteLine("Actual values-> {0} {1}", st1.name, st1.dateBirth == null ? "NULL" : st1.dateBirth.ToString());                
            }
            
#endif

#if _CLIENTWINS
            //################ CLIENT WINS
            //Re-create the database
            //Change Student type setting concurrency=fixed (right click Student.name at School.edmx )
            using (var ctx = new School())
            {
                //selecting a student using LINQ to Entities
                st1 = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
            }
            //Disconnected changes
            st1.name = "ClientWins";
            Console.WriteLine("now");
            Thread.Sleep(8000); // change the name for student 1 in a concurrent transaction

            using (var ctx = new School())
            {
                
                try 
                {
                    ctx.Entry(st1).State = System.Data.EntityState.Modified;
                    ctx.SaveChanges();                    
                }
                catch (DbUpdateConcurrencyException e)
                {
                    Console.WriteLine(e.Message);
                    // esmagar as alterações na BD
                    var entry = e.Entries.Single();
                    var dbValues = entry.GetDatabaseValues();                
                    entry.OriginalValues.SetValues(dbValues);    
                    ctx.SaveChanges();                               

                }
            }
            using (var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                //selecting a student using LINQ to Entities
                st1 = ctx.Students.Where(s => s.studentId == 1).SingleOrDefault();
                //see what was changed...
                Console.WriteLine("Actual values-> {0} {1}", st1.name, st1.dateBirth == null ? "NULL" : st1.dateBirth.ToString());
            }
   
#endif
                        
        }
    }
}
