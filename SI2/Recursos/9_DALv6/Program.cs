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
*   - Não há registo de quais as entidades que foram carregadas da BD e quais as entidades que foram criadas programaticamente.
*/
using System;
using System.Configuration;
using DAL.concrete;
using DAL.model;
using DAL.mapper.concrete;
using System.Collections.Generic;

namespace ex1
{

    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ex1cs"].ConnectionString;

            /** TESTAR COUNTRY*/
            Console.WriteLine(" TESTAR COUNTRY");

            using (Context ctx = new Context(connectionString))
            {

                Country c = new Country();
                c.Name = "brasil";

                CountryMapper countryMap = new CountryMapper(ctx);
                c = countryMap.Create(c);
                Country c1 = countryMap.Read(c.Id);
                Console.WriteLine("Country: {0}-{1}", c1.Id, c1.Name);

                c1.Name = "Brasil";
                countryMap.Update(c1);
                c1 = countryMap.Read(c1.Id);
                Console.WriteLine("Country: {0}-{1}", c1.Id, c1.Name);

                Country c2 = new Country();
                c2.Name = "Portugal";
                countryMap.Create(c2);


                foreach (var country in countryMap.ReadAll())
                {
                    Console.WriteLine("Country: {0}-{1}", country.Id, country.Name);
                    countryMap.Delete(country);
                }


                foreach (var country in countryMap.ReadAll())
                {
                    Console.WriteLine("Country: {0}-{1}", country.Id, country.Name);
                }

            }


            /** TESTAR COURSE*/
           using (Context ctx = new Context(connectionString))
            {
                Console.WriteLine("\nTESTAR COURSE");
                Course cs = new Course();
                cs.Name = "Information System II";
                CourseMapper courseMap = new CourseMapper(ctx);
                cs = courseMap.Create(cs);
                Course cs1 = courseMap.Read(cs.Id);
                Console.WriteLine("Course: {0}-{1}", cs1.Id, cs1.Name);

                cs1.Name = "Information System 2";
                courseMap.Update(cs1);
                cs1 = courseMap.Read(cs1.Id);
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
                StudentMapper studentMap = new StudentMapper(ctx);

                Student s = new Student();
                s.Number = 123;
                s.Name = "Student One";
                s.Sex = 'M';
                s.DateOfBirth = "11/01/1990";

                Country c = new Country();
                c.Name = "Portugal";
                countryMap.Create(c);
                s.HomeCountry = c;

                studentMap.Create(s);
                Student s1 = studentMap.Read(123);
                Console.WriteLine("Student: {0}-{1}-{2}-{3}-{4}", s1.Number, s1.Name, s1.Sex, s1.DateOfBirth, s1.HomeCountry.Name);

                s.Name = "Student No. One";
                s1 = studentMap.Update(s);
                Console.WriteLine("Student: {0}-{1}-{2}-{3}-{4}", s1.Number, s1.Name, s1.Sex, s1.DateOfBirth, s1.HomeCountry.Name);

                s1.Number = 456;
                s1.Name = "Student No. Two";
                studentMap.Create(s1);
                foreach (var student in studentMap.ReadAll())
                {
                    Console.WriteLine("Student: {0}-{1}-{2}", student.Number, student.Name, student.HomeCountry.Name);
                    studentMap.Delete(student);
                }

                //clean
                countryMap.Delete(c);
            }

            /** TESTAR Student with Courses*/
            using (Context ctx = new Context(connectionString))
            {
                Console.WriteLine("\nTESTAR COURSE with STUDENT");
                CourseMapper courseMap = new CourseMapper(ctx);
                StudentMapper studentMap = new StudentMapper(ctx);
                CountryMapper countryMap = new CountryMapper(ctx);

                Course cs2 = new Course();
                cs2.Name = "Information Systems 2";
                cs2 = courseMap.Create(cs2);

                Course cs1 = new Course();
                cs1.Name = "Internet Programming";
                cs1 = courseMap.Create(cs1);

                Country c = new Country();
                c.Name = "Portugal";
                countryMap.Create(c);

                Student si1s = new Student();
                si1s.DateOfBirth = "06/01/1991";
                si1s.Name = "Student 2";
                si1s.Sex = 'F';
                si1s.Number = 456;
                si1s.HomeCountry = c;

                si1s.EnrolledCourses.Add(cs1);
                si1s.EnrolledCourses.Add(cs2);

                si1s = studentMap.Create(si1s);

                Student si2s = new Student();
                si2s.DateOfBirth = "06/01/1991";
                si2s.Sex = 'M';
                si2s.Number = 789;
                si2s.Name = "Student 3";
                si2s.HomeCountry = c;


                si1s.EnrolledCourses.Add(cs2);
                studentMap.Create(si2s);


                Student res1 = studentMap.Read(si1s.Number);
                Console.WriteLine("Nº cursos: {0}", res1.EnrolledCourses.Count);
                res1 = studentMap.Read(si2s.Number);
                Console.WriteLine("Nº cursos: {0}", res1.EnrolledCourses.Count);

                Console.WriteLine("Curso {0} com {1} estudantes", cs2.Name, cs2.EnrolledStudents.Count);
                

                //apagar
                foreach (var student in studentMap.ReadAll())
                {
                    studentMap.Delete(student);
                }

                foreach (var course in courseMap.ReadAll())
                {
                    courseMap.Delete(course);
                }
                foreach (var country in countryMap.ReadAll())
                {
                    countryMap.Delete(country);
                }

            }
        }
    }
}
