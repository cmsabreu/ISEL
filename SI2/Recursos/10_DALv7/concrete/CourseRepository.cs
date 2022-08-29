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
using System;
using System.Collections.Generic;
using System.Linq;

using DAL.model;
using DAL.mapper.concrete;

namespace DAL.concrete
{
    class CourseRepository : ICourseRepository
    {
        private IContext context;
        public CourseRepository(IContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Course> Find(Func<Course, bool> criteria)
        {
            //Implementação muito pouco eficiente.  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Course> FindAll()
        {
            return new CourseMapper(context).ReadAll();
        }
    }
}
