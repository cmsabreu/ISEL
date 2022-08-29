/*
*  ISEL-ADEETC-SI2
*   ND 2014-2021
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
* Problemas Encontrados:
*   - As chaves geradas não são automaticamente preenchidas nos objectos
*   - Não são guardadas as associações relacionados com Course e Students
*   - Para obter uma entidade podem ser usadas mais de uma ligação (não reutilização)
*   - Possíveis problemas de referências circulares
*   - Há muito código repetido no acesso a dados
*/
using System;
using System.Configuration;
using DAL.model;
using DAL.mapper.concrete;
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
            c.Name = "brasil";

            CountryMapper countryMap = new CountryMapper(connectionString);
            countryMap.Create(c);
            Country c1 = countryMap.Read(1);
            Console.WriteLine("Country: {0}-{1}",c1.Id,c1.Name);

            c1.Name = "Brasil";
            countryMap.Update(c1);
            c1 = countryMap.Read(1);
            Console.WriteLine("Country: {0}-{1}", c1.Id, c1.Name);

            countryMap.Delete(c1);
            c1 = countryMap.Read(1);
            if(c1 == null)
                Console.WriteLine("Apagado");
            else
                Console.WriteLine("Não Apagado!");

            /** TESTAR COURSE*/
            Console.WriteLine("\nTESTAR COURSE");
            Course cs = new Course();
            cs.Name = "Information System II";
            CourseMapper courseMap = new CourseMapper(connectionString);
            courseMap.Create(cs);
            Course cs1 = courseMap.Read(1);
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

            /** TESTAR STUDENT*/
            Console.WriteLine("\nTESTAR STUDENT");
            Student s = new Student();
            s.Number = 123;
            s.Name = "Student One";
            s.Sex = 'M';
            s.DateOfBirth = "11/01/1990";

            c = new Country();
            c.Id = 2;
            c.Name = "Portugal";
            countryMap.Create(c);
            s.HomeCountry = c;
            StudentMapper studentMap = new StudentMapper(connectionString);
            studentMap.Create(s);
            Student s1 = studentMap.Read(123);
            Console.WriteLine("Student: {0}-{1}-{2}-{3}-{4}", s.Number,s.Name,s.Sex,s.DateOfBirth,s.HomeCountry.Name);

            //Console.WriteLine("Enrolled in {0} courses", s1.EnrolledCourses.Count);

        }
    }
}
