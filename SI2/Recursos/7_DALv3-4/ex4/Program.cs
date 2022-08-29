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
*
*Problemas Encontrados:
*   - Há muito código repetido no acesso a dados
*   - Persistem alguns problemas no carregamento de entidades com referências circulares
*/
using System;
using System.Configuration;
using DAL.concrete;
using DAL.model;
using DAL.mapper.concrete;
using System.Transactions;

namespace ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ex1cs"].ConnectionString;

            /** TESTAR COUNTRY*/
            Console.WriteLine(" TESTAR COUNTRY");
            Country c = new Country();
            using (Context ctx = new Context(connectionString))
            {
                
                c.Name = "brasil";

                CountryMapper countryMap = new CountryMapper(ctx);
                c = countryMap.Create(c);
                Country c1 = countryMap.Read(c.Id);
                Console.WriteLine("Country: {0}-{1}", c1.Id, c1.Name);
                
            }

            using (Context ctx = new Context(connectionString))
            {
                CountryMapper countryMap = new CountryMapper(ctx);
                Country c1 = new Country();
                c1.Name = "Brasil";
                c1.Id = c.Id;
                
                countryMap.Update(c1);
                c1 = countryMap.Read(c1.Id);
                Console.WriteLine("Country: {0}-{1}", c1.Id, c1.Name);

                countryMap.Delete(c1);
                c1 = countryMap.Read(1);
                if (c1 == null)
                    Console.WriteLine("Apagado");
                else
                    Console.WriteLine("Não Apagado!");

                
            }

            /** TESTAR COURSE*/
            using (Context ctx = new Context(connectionString))
            {
                Console.WriteLine("\nTESTAR COURSE");
                Course cs = new Course();
                cs.Name = "Information System II";
                CourseMapper courseMap = new CourseMapper(ctx);
                cs=courseMap.Create(cs);
                Course cs1 = courseMap.Read(cs.Id);
                Console.WriteLine("Course: {0}-{1}", cs1.Id, cs1.Name);

                cs1.Name = "Information System 2";
                courseMap.Update(cs1);
                cs1 = courseMap.Read(1);
                Console.WriteLine("Course: {0}-{1}", cs1.Id, cs1.Name);

                courseMap.Delete(cs1);
                cs1 = courseMap.Read(1);
                if (cs1 == null)
                    Console.WriteLine("Apagado");
                else
                    Console.WriteLine("Não Apagado!");
            }

            using (Context ctx = new Context(connectionString))
            {
                /** TESTAR STUDENT*/
                Console.WriteLine("\nTESTAR STUDENT");
                CountryMapper countryMap = new CountryMapper(ctx);

                Student s = new Student();
                s.Number = 123;
                s.Name = "Stundent One";
                s.Sex = 'M';
                s.DateOfBirth = "11/01/1990";

                c = new Country();
                c.Id = 2;
                c.Name = "Portugal";
                countryMap.Create(c);
                s.HomeCountry = c;
                StudentMapper studentMap = new StudentMapper(ctx);
                studentMap.Create(s);
                Student s1 = studentMap.Read(123);
                Console.WriteLine("Student: {0}-{1}-{2}-{3}-{4}", s.Number, s.Name, s.Sex, s.DateOfBirth, s.HomeCountry.Name);
            }

            /** TESTAR COURSE with STUDENT*/
            using (Context ctx = new Context(connectionString))
            {
                Console.WriteLine("\nTESTAR COURSE with STUDENT");
                CourseMapper courseMap = new CourseMapper(ctx);
                StudentMapper studentMap = new StudentMapper(ctx);
                                
                Course cs2 = new Course();
                cs2.Name = "Information Systems 1";
                Student si1s = new Student();
                si1s.DateOfBirth = "06/01/1991";
                si1s.Name = "Student 2";
                si1s.Sex = 'F';
                //estudante vai ficar com numero 0.
                si1s.Number = 456;
                si1s.HomeCountry = c;
                studentMap.Create(si1s);
                
                cs2.EnroledStudents.Add(si1s);
                cs2 = courseMap.Create(cs2);

                Course cs2r = courseMap.Read(cs2.Id);
                Console.WriteLine("Nº estudantes: {0}", cs2r.EnroledStudents.Count);
            }
         }
    }
}
