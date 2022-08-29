namespace _02_trackChanges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
        }

        public int courseId { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
