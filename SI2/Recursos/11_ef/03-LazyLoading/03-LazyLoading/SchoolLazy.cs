namespace _03_LazyLoading
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SchoolLazy : DbContext
    {
        //to avoid caals to dbo.EdmMetadata and dbo.__MigrationHistory, as espected for a ModelFirst taht already has a database.
        static SchoolLazy()
         {
             Database.SetInitializer<SchoolLazy>(null);        
         }
        public SchoolLazy()
            : base("name=SchoolLazy")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Country>()
                .HasMany(e => e.Students)
                .WithRequired(e => e.OriginCountry)
                .HasForeignKey(e => e.country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Courses)
                .Map(m => m.ToTable("StudentCourse").MapLeftKey("courseId").MapRightKey("studentId"));

            modelBuilder.Entity<Student>()
                .Property(e => e.sex)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
