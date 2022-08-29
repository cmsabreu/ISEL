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
*/

using System.Collections.Generic;

namespace DAL.model
{
    public class Student    {        public int Number { get; set; }        public string Name { get; set; }        public string DateOfBirth { get; set; }        public char Sex { get; set; }        public Country HomeCountry { get; set; }        public List<Course> EnrolledCourses { get; set; }    }}
