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
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> EnroledStudents { get; set; }

    }
}

