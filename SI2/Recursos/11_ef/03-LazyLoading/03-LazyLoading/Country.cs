namespace _03_LazyLoading
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]
    public partial class Country
    {
        public Country()
        {
            Students = new HashSet<Student>();
        }

        public int countryId { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
