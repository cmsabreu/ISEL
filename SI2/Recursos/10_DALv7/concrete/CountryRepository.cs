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

namespace DAL.concrete
{
    
    class CountryRepository : ICountryRepository
    {
        private IContext context;
        public CountryRepository(IContext ctx)
        {
            context = ctx;
        }
         
        public IEnumerable<Country> Find(System.Func<Country, bool> criteria)
        {
            //Implementação muito pouco eficiente!!!!  
            return FindAll().Where(criteria);
        }

        public IEnumerable<Country> FindAll()
        {
            return new CountryMapper(context).ReadAll();
        }
    }
}
