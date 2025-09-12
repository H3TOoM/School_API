using Microsoft.EntityFrameworkCore;
using school_api.Data.Models;

namespace school_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options ) : base( options )
        {
        }

        // DB Sets
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<FileUpload> FileUploads { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );

            // Unique constraints
            modelBuilder.Entity<User>()
                .HasIndex( u => u.Email )
                .IsUnique();

            // User one-to-ones
            modelBuilder.Entity<Staff>()
                .HasOne( st => st.User )
                .WithOne()
                .HasForeignKey<Staff>( st => st.UserId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Student>()
                .HasOne( s => s.User )
                .WithOne()
                .HasForeignKey<Student>( s => s.UserId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Teacher>()
                .HasOne( t => t.User )
                .WithOne()
                .HasForeignKey<Teacher>( t => t.UserId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Manager>()
                .HasOne( m => m.User )
                .WithOne()
                .HasForeignKey<Manager>( m => m.UserId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Parent>()
                .HasOne( p => p.User )
                .WithOne()
                .HasForeignKey<Parent>( p => p.UserId )
                .OnDelete( DeleteBehavior.Restrict );

            // Student - Parent (optional) using shadow FK ParentId
            modelBuilder.Entity<Student>()
                .HasOne( s => s.Parent )
                .WithMany( p => p.Students )
                .HasForeignKey( "ParentId" )
                .OnDelete( DeleteBehavior.SetNull );

            // Student - StudentClass (required)
            modelBuilder.Entity<Student>()
                .HasOne( s => s.StudentClass )
                .WithMany( sc => sc.Students )
                .HasForeignKey( s => s.StudentClassId )
                .OnDelete( DeleteBehavior.Restrict );

            // Staff - Department (required)
            modelBuilder.Entity<Staff>()
                .HasOne( st => st.Department )
                .WithMany( d => d.StaffMembers )
                .HasForeignKey( st => st.DepartmentId )
                .OnDelete( DeleteBehavior.Restrict );

            // Department - Manager (required)
            modelBuilder.Entity<Department>()
                .HasOne( d => d.Manager )
                .WithMany()
                .HasForeignKey( d => d.ManagerId )
                .OnDelete( DeleteBehavior.Restrict );

            // Teacher - Department (required)
            modelBuilder.Entity<Teacher>()
                .HasOne( t => t.Department )
                .WithMany( d => d.Teachers )
                .HasForeignKey( t => t.DepartmentId )
                .OnDelete( DeleteBehavior.Restrict );

            // Teacher - Subject (required)
            modelBuilder.Entity<Teacher>()
                .HasOne( t => t.Subject )
                .WithMany( s => s.Teachers )
                .HasForeignKey( t => t.SubjectId )
                .OnDelete( DeleteBehavior.Restrict );

            // Course - Subject (required)
            modelBuilder.Entity<Course>()
                .HasOne( c => c.Subject )
                .WithMany( s => s.Courses )
                .HasForeignKey( c => c.SubjectId )
                .OnDelete( DeleteBehavior.Restrict );

            // Course - Teacher (optional)
            modelBuilder.Entity<Course>()
                .HasOne( c => c.Teacher )
                .WithMany( t => t.Courses )
                .HasForeignKey( c => c.TeacherId )
                .OnDelete( DeleteBehavior.SetNull );

            // StudentClass - Department (required)
            modelBuilder.Entity<StudentClass>()
                .HasOne( sc => sc.Department )
                .WithMany( d => d.Classes )
                .HasForeignKey( sc => sc.DepartmentId )
                .OnDelete( DeleteBehavior.Restrict );

            // StudentClass - Teacher (optional homeroom)
            modelBuilder.Entity<StudentClass>()
                .HasOne( sc => sc.Teacher )
                .WithMany()
                .HasForeignKey( sc => sc.TeacherId )
                .OnDelete( DeleteBehavior.SetNull );

            // Schedule relations (required)
            modelBuilder.Entity<Schedule>()
                .HasOne( sch => sch.Course )
                .WithMany()
                .HasForeignKey( sch => sch.CourseId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Schedule>()
                .HasOne( sch => sch.Teacher )
                .WithMany()
                .HasForeignKey( sch => sch.TeacherId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Schedule>()
                .HasOne( sch => sch.StudentClass )
                .WithMany( sc => sc.Schedules )
                .HasForeignKey( sch => sch.StudentClassId )
                .OnDelete( DeleteBehavior.Restrict );

            // Attendance relations (required)
            modelBuilder.Entity<Attendance>()
                .HasOne( a => a.Student )
                .WithMany( s => s.Attendances )
                .HasForeignKey( a => a.StudentId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Attendance>()
                .HasOne( a => a.Schedule )
                .WithMany( sch => sch.Attendances )
                .HasForeignKey( a => a.ScheduleId )
                .OnDelete( DeleteBehavior.Restrict );

            // Grade relations (required)
            modelBuilder.Entity<Grade>()
                .HasOne( g => g.Student )
                .WithMany( s => s.Grades )
                .HasForeignKey( g => g.StudentId )
                .OnDelete( DeleteBehavior.Restrict );

            modelBuilder.Entity<Grade>()
                .HasOne( g => g.Course )
                .WithMany()
                .HasForeignKey( g => g.CourserId )
                .OnDelete( DeleteBehavior.Restrict );

            // FileUpload relations
            modelBuilder.Entity<FileUpload>()
                .HasOne( f => f.Uploader )
                .WithMany()
                .HasForeignKey( f => f.UploadedBy )
                .OnDelete( DeleteBehavior.Restrict );

            // RefreshToken relations
            modelBuilder.Entity<RefreshToken>()
                .HasOne( rt => rt.User )
                .WithMany()
                .HasForeignKey( rt => rt.UserId )
                .OnDelete( DeleteBehavior.Cascade );

            // AuditLog relations
            modelBuilder.Entity<AuditLog>()
                .HasOne( al => al.User )
                .WithMany()
                .HasForeignKey( al => al.UserId )
                .OnDelete( DeleteBehavior.Restrict );
        }
    }
}
