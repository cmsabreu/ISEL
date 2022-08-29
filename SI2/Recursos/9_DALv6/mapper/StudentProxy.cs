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
using DAL;
using DAL.model;
using System.Data;
using System.Collections.Generic;

namespace DAL.mapper.concrete
{
    class StudentProxy : Student
    {
        private IContext context;
        private int? countryId;
        
        public StudentProxy(Student s, IContext ctx, int? countryId):base()
        {
            base.Number = s.Number;
            base.Name = s.Name;
            base.Sex = s.Sex;
            base.DateOfBirth = s.DateOfBirth;
            base.EnrolledCourses = null;
            base.HomeCountry = null;
            context = ctx;
            this.countryId = countryId;
        }

        public override Country HomeCountry
        {
            get
            {
                if (base.HomeCountry == null) //lazy load
                {
                    StudentMapper sm = new StudentMapper(context);
                    base.HomeCountry = sm.LoadCountry(this);
                }
                return base.HomeCountry;
            }

            set
            {
                base.HomeCountry = value;
            }
        }

        public override List<Course> EnrolledCourses
        {
            get
            {
                if (base.EnrolledCourses == null)
                {
                    StudentMapper sm = new StudentMapper(context);
                    base.EnrolledCourses=sm.LoadCourses(this);
                }
                return base.EnrolledCourses;
            }

            set
            {
                base.EnrolledCourses = value;
            }
        }

    }
}
