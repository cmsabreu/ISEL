namespace _03_LazyLoading
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public Student()
        {
            Courses = new HashSet<Course>();
        }

        public int studentId { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateBirth { get; set; }

        [Required]
        [StringLength(1)]
        public string sex { get; set; }

        public int country { get; set; }

        public virtual Country OriginCountry { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
