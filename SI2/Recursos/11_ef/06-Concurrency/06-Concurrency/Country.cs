//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _06___Concurrency
{
    using System;
    using System.Collections.Generic;
    
    public partial class Country
    {
        public Country()
        {
            this.Students = new HashSet<Student>();
        }
    
        public int countryId { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Student> Students { get; set; }
    }
}
