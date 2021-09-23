using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MAWS.Models
{
    /// <summary>
    /// 
    /// Class Represents Database Context 
    /// 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /*public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }*/

        public ApplicationDbContext(DbContextOptions options)
            :base (options)
        {
        }

        public DbSet<Unit> Unit { get; set; }
        public DbSet<AcademicStaff> AcademicStaff { get; set; }
        public DbSet<Research> Research { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Supervision> Supervision { get; set; }
        public DbSet<MiscTeachingActivity> MiscTeachingActivity { get; set; }
        public DbSet<UnitOffering> UnitOffering { get; set; }
        public DbSet<Discipline> Discipline { get; set; }
        public DbSet<TeachingPattern> TeachingPattern { get; set; }
        public DbSet<TeachingActivity> TeachingActivity { get; set; }
        public DbSet<TeachingActivity> TeachingActivityAssignment { get; set; }
        public DbSet<AspNetUserAcademicStaff> AspNetUserAcademicStaff { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        //--------- Audit Tables --------- //
        public DbSet<AuditTableStatus> AuditTableStatus { get; set; }
        public DbSet<Research_audit> Research_audit{get; set;}
        public DbSet<MiscTeachingActivity_audit> MiscTeachingActivity_audit{get;set;}
        public DbSet<Service_audit> Service_audit{get;set;}
        public DbSet<Supervision_audit> Supervision_audit{get;set;}
        public DbSet<TeachingActivityAssignment_audit> TeachingActivityAssignment_audit { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //--------- Audits  --------- //
            modelBuilder.Entity<AuditTableStatus>().HasData(new AuditTableStatus { IsActive = false });

            modelBuilder.Entity<Research_audit>()
            .HasKey(r => new {r.operation,r.stamp});

            modelBuilder.Entity<MiscTeachingActivity_audit>()
            .HasKey(r => new {r.operation,r.stamp});

            modelBuilder.Entity<Service_audit>()
            .HasKey(r => new {r.operation,r.stamp});

            modelBuilder.Entity<Supervision_audit>()
            .HasKey(r => new {r.operation,r.stamp});

            modelBuilder.Entity<TeachingActivityAssignment_audit>()
            .HasKey(r => new {r.operation,r.stamp});

           //---------  Academnic Staff  --------- //
            modelBuilder.Entity<AcademicStaff>()
            .Property(p => p.WorkHrs)
            .HasComputedColumnSql(@"""WorkFraction""*""FTBaseHrs""");

            modelBuilder.Entity<AcademicStaff>()
            .HasAlternateKey(p => p.AcademicStaffID)
            .HasName("AcademicStaffID");

            //---------  Disicpline  --------- //
            modelBuilder.Entity<Discipline>()
            .HasOne(d => d.AcademicStaff)
            .WithOne(s => s.Discipline)
            .HasForeignKey<Discipline>(d => d.HeadOfDiscipline)
            .HasPrincipalKey<AcademicStaff>(s => s.AcademicStaffID);


            //---------  MiscTeachingActivity  --------- //
            modelBuilder.Entity<MiscTeachingActivity>()
            .HasOne(m => m.AcademicStaff)
            .WithMany(s => s.MiscTeachingActivityList)
            .HasForeignKey(m => m.AcademicStaffID)
            .HasPrincipalKey(s => s.AcademicStaffID);

            //---------  Research  --------- //
            modelBuilder.Entity<Research>()
            .HasOne(r => r.AcademicStaff)
            .WithMany(s => s.ReasearchList)
            .HasForeignKey(r => r.AcademicStaffID)
            .HasPrincipalKey(s => s.AcademicStaffID);

            modelBuilder.Entity<Research>()
            .Property(p => p.Percentage)
            .HasComputedColumnSql(@"""Fifteen_Pc""+""ECR_Pc""+""Income_Pc""+""Completions_Pc""+""Pubs_Pc""+""RCI_Pc""+""Discretionary_Pc""");

            //---------  Service  --------- //
            modelBuilder.Entity<Service>()
            .HasOne(service => service.AcademicStaff)
            .WithMany(s => s.ServiceList)
            .HasForeignKey(service => service.AcademicStaffID)
            .HasPrincipalKey(s => s.AcademicStaffID);


             //---------  Supervision  --------- //
            modelBuilder.Entity<Supervision>()
            .HasOne(sup => sup.AcademicStaff)
            .WithMany(s => s.SupervisionList)
            .HasForeignKey(sup => sup.AcademicStaffID)
            .HasPrincipalKey(s => s.AcademicStaffID);

            //---------  TeachingPattern  --------- //
            modelBuilder.Entity<TeachingPattern>()
            .HasOne(tp=> tp.AcademicStaff)
            .WithMany(s => s.TeachingPatternList)
            .HasForeignKey(tp => tp.AcademicStaffID)
            .HasPrincipalKey(s => s.AcademicStaffID);

            //---------  UnitOffering  --------- //
            modelBuilder.Entity<UnitOffering>()
            .HasOne( uo => uo.AcademicStaff)
            .WithMany(s => s.UnitOfferingList)
            .HasForeignKey(uo => uo.AcademicStaffID)
            .HasPrincipalKey(s => s.AcademicStaffID);

            //--------------------  AcademicStaffUserID - ACADEMIC STAFF
            modelBuilder.Entity<AspNetUserAcademicStaff>()
                .HasOne(d => d.AcademicStaff)
                .WithOne(s => s.AspNetUserAcademicStaff)
                .HasForeignKey<AspNetUserAcademicStaff>(d => d.StaffID)
                .HasPrincipalKey<AcademicStaff>(s => s.AcademicStaffID);

            //--------------------  AspNetUserAcademicStaff - ACADEMIC STAFF
            modelBuilder.Entity<AspNetUserAcademicStaff>()
                .HasOne(d => d.ApplicationUser)
                .WithOne(s => s.AspNetUserAcademicStaff)
                .HasForeignKey<AspNetUserAcademicStaff>(d => d.Id);
        }
    }
}
