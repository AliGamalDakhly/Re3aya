using System.Reflection;
using _01_DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _01_DataAccessLayer.Data.Context
{
    public class Re3ayaDbContext: IdentityDbContext<AppUser>
    {
        public Re3ayaDbContext(DbContextOptions<Re3ayaDbContext> options): base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Government> Governments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Document> Documents { get; set; }  
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<DoctorTimeSlot> DoctorTimeSlots { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SystemInfo> SystemInfos { get; set; }
    }
}
