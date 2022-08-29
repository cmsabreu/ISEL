/*
*   ISEL-ADEETC-SI2
*   ND 2014-2019
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*/
//#define _EX1
#define _EX2
//#define _EX3

using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;



namespace _07___Transactions
{
    class Program
    {

        
#if _EX1 || _EX2
        static Course getCourseByID(int id)
        {
            using (var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                return (from c in ctx.Courses where c.courseId == id select c).SingleOrDefault();
            }
        }
        static Student getUserByID(int id)
        {
            using(var ctx = new School())
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                return ctx.Students.Where(s => s.studentId == id).SingleOrDefault();    
            }            
        }
       
        static void enrolStudent(Student st,Course c)
        {
            
            using (var ctx = new School())
            {
                //Attach to the context
                ctx.Entry<Student>(st).State = System.Data.EntityState.Modified;
                ctx.Entry<Course>(c).State = System.Data.EntityState.Unchanged;//if this line is missing, a new course will be added!
                st.Courses.Add(c);
                ctx.SaveChanges();
            }
            
        }
#endif

#if _EX3
        static Course getCourseByID(int id, DbConnection cn)
        {
            using (var ctx = new School(cn))
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                return (from c in ctx.Courses where c.courseId == id select c).SingleOrDefault();
            }
        }
        static Student getUserByID(int id, DbConnection cn)
        {
            using (var ctx = new School(cn))
            {
                ctx.Configuration.ProxyCreationEnabled = false;
                return ctx.Students.Where(s => s.studentId == id).SingleOrDefault();
            }
        }

        static void enrolStudent(Student st, Course c, DbConnection cn)
        {

            using (var ctx = new School(cn))
            {
                //Attach to the context
                ctx.Entry<Student>(st).State = System.Data.EntityState.Modified;
                ctx.Entry<Course>(c).State = System.Data.EntityState.Unchanged;//if this line is missing, a new course will be added!
                st.Courses.Add(c);
                ctx.SaveChanges();
            }

        }
#endif
        static void Main(string[] args)
        {

            Student st = null;
            Course c = null;

#if _EX1
            //################## DB inconsistency
            //Re create the DB
            using (var ts = new TransactionScope())
            {
                try
                {
                    using (var ctx = new School())
                    {
                        st = ctx.Students.Where(s => s.studentId == 2).SingleOrDefault();    
                        st.Courses.Add( getCourseByID(1));
                        ctx.SaveChanges();//See what happens in the DB
                        ts.Complete();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
#endif

#if _EX2
            //################## Transaction can be promoted to distributed
            //Re create the DB
            // From documentation:
            //Promotion of a transaction to a DTC may occur when a connection is closed and reopened within a single transaction. 
            //Because Object Services opens and closes the connection automatically, 
            //you should consider manually opening and closing the connection to avoid transaction promotion.
            using (var ts = new TransactionScope())
            {
                try
                {
                    st = getUserByID(2);
                    c = getCourseByID(1);
                    enrolStudent(st, c);
                    ts.Complete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
#endif

#if _EX3
            //################## Only one connection, so it is always a local transaction 
            //Re create the DB            
            using (var ts = new TransactionScope())
            {
                using(EntityConnection cn = new EntityConnection("name = School"))
                {
                    try
                    {
                        st = getUserByID(2,cn);
                        c = getCourseByID(1,cn);
                        enrolStudent(st, c,cn);
                        ts.Complete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

            }
#endif


        }

      
    }
}
