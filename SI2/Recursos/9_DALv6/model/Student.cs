/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material did?tico para apoio 
*   ? unidade curricular de 
*   Sistemas de Informa??o II
*
*	Os exemplos podem n?o ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedag?gicos
*	Eventuais incorrec??es s?o alvo de discuss?o
*	nas aulas.
*/
using System.Collections.Generic;

namespace DAL.model
{
    public class Student
    {
        public Student()
        {
            EnrolledCourses = new List<Course>();
        }
        public int? Number { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public char Sex { get; set; }
        public virtual Country HomeCountry { get; set; }
        public virtual  List<Course> EnrolledCourses { get; set; }
    }
}

