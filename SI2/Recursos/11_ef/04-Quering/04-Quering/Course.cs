//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _04___Quering
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
        }
    
        public int courseId { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<Student> Students { get; set; }
    }
}
