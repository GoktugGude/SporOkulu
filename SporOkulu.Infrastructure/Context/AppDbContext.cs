using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SporOkulu.Domain;
using SporOkulu.Domain.Entities;

namespace SporOkulu.Infrastructure.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

        public DbSet<Attendance> Attendances {get;set;}
        public DbSet<Branch> Branches {get;set;}
        public DbSet<Coach> Coaches {get;set;}
        public DbSet<Parent> Parents {get;set;}
        public DbSet<Payment> Payments {get;set;}
        public DbSet<Student> Students {get;set;}
        public DbSet<StudentTestResult> StudentTestResults {get;set;}
        public DbSet<TestType> TestTypes {get;set;}
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    // Dinamik Seed Data (PasswordHash vb.) kaynaklı uyarıyı susturuyoruz
    optionsBuilder.ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Student>()
            .HasOne(s => s.AppUser)
            .WithOne()
            .HasForeignKey<Student>(s => s.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Student>()
            .HasOne(s => s.Branch)
            .WithMany(b => b.students)
            .HasForeignKey(s => s.BranchId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Branch>().HasData(
                new Branch{Id = 1, Name = "Basketbol"},
                new Branch{Id = 2, Name = "Futbol"},
                new Branch{Id = 3, Name = "Voleybol"}
            );

            var adminRoleId = 1;
            builder.Entity<AppRole>().HasData(new AppRole 
            { 
                Id = adminRoleId, 
                Name = "Admin", 
                NormalizedName = "ADMIN" 
            });
            var adminUserId = 1;
           var adminUser = new AppUser
            {
                Id = adminUserId,
                FirstName = "Goktug",
                LastName = "GUDE",
                UserName = "goktuggude@gmail.com",
                NormalizedUserName = "GOKTUGGUDE@GMAIL.COM", // Identity için şart
                Email = "goktuggude@gmail.com",             // Ekledik
                NormalizedEmail = "GOKTUGGUDE@GMAIL.COM",    // Ekledik
                EmailConfirmed = true,
                SecurityStamp = "9ca07185-1815-4674-9f44-67f707f15e75"
            };
            var passwordHasher = new PasswordHasher<AppUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Mrgude123.");
            builder.Entity<AppUser>().HasData(adminUser);
            builder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });

            }
   
}

