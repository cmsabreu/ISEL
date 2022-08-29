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


namespace DAL.mapper.interfaces
{
    public interface IMapper<T, Tid>
    {        void Create(T entity);
        T Read(Tid id);
        void Update(T entity);
        void Delete(T entity);
    }
}