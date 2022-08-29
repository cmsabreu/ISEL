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
*/
using System.Linq;
using System.Collections.Generic;
using DAL.model;
using DAL.mapper.concrete;
using System;

namespace DAL
{
    class StudentRepository : IStudentRepository
    {
        private IContext context;
        public StudentRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Student> Find(Func<Student, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Student> FindAll()
        {
            return new StudentMapper(context).ReadAll();
        }
    }
}
