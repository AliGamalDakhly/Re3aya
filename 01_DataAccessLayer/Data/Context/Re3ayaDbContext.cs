using System.Reflection;
using _01_DataAccessLayer.Enums;
using _01_DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
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
            var hasher = new PasswordHasher<AppUser>();

            // Seed Governments
            modelBuilder.Entity<Government>().HasData(
                new Government { GovernmentId = 1, Name = "Cairo" },
                new Government { GovernmentId = 2, Name = "Alexandria" }
            );

            // Seed Cities
            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, Name = "Cairo", GovernmentId = 1 },
                new City { CityId = 2, Name = "Alexandria", GovernmentId = 2 }
            );

            // Seed Specializations
            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { SpecializationId = 1, Name = "Cardiology", Description = "Heart care" },
                new Specialization { SpecializationId = 2, Name = "Pediatrics", Description = "Child care" }
            );

            // Seed AppUsers
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id = "1",
                    UserName = "ahmed@example.com",
                    NormalizedUserName = "ahmed@example.com".ToUpper(),
                    Email = "ahmed@example.com",
                    NormalizedEmail = "ahmed@example.com".ToUpper(),
                    FullName = "Ahmed Khaled",
                    DateOfBirth = new DateOnly(1990, 5, 15),
                    CreatedAt = new DateTime(2025, 7, 1, 10, 0, 0, DateTimeKind.Utc),
                    Gender = Gender.Male,
                    PhoneNumber = "01234567890",
                    PasswordHash = hasher.HashPassword(null, "Password123!")
                },
                new AppUser
                {
                    Id = "2",
                    UserName = "sara.ali@doc.com",
                    NormalizedUserName = "sara.ali@doc.com".ToUpper(),
                    Email = "sara.ali@doc.com",
                    NormalizedEmail = "sara.ali@doc.com".ToUpper(),
                    FullName = "Dr. Sara Ali",
                    DateOfBirth = new DateOnly(1985, 9, 22),
                    CreatedAt = new DateTime(2025, 7, 1, 11, 0, 0, DateTimeKind.Utc),
                    Gender = Gender.Female,
                    PhoneNumber = "01987654321",
                    PasswordHash = hasher.HashPassword(null, "Doctor123!")
                },
                new AppUser
                {
                    Id = "3",
                    UserName = "omar@admin.com",
                    NormalizedUserName = "omar@admin.com".ToUpper(),
                    Email = "omar@admin.com",
                    NormalizedEmail = "omar@admin.com".ToUpper(),
                    FullName = "Admin Omar",
                    DateOfBirth = new DateOnly(1980, 3, 10),
                    CreatedAt = new DateTime(2025, 7, 1, 12, 0, 0, DateTimeKind.Utc),
                    Gender = Gender.Male,
                    PhoneNumber = "+201102682493",
                    PasswordHash = hasher.HashPassword(null, "Admin123!")
                }
            );

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient { PatientId = 1, AppUserId = "1" }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    DoctorId = 1,
                    Balance = 1500.00,
                    ExpYears = 10,
                    AboutMe = "Experienced cardiologist with 10 years in heart care.",
                    RatingValue = 4.8f,
                    Fees = 500.00,
                    Status = DoctorAccountStatus.Approved,
                    Service = DoctorService.InClinic,
                    NationalId = "12345678901234",
                    SpecializationId = 1,
                    AppUserId = "2",
                    Addresses = new List<Address>(),
                    Ratings = new List<Rating>(),
                    Documents = new List<Document>(),
                    DoctorTimeSlots = new List<DoctorTimeSlot>()
                }
            );

            // Seed Admins
            modelBuilder.Entity<Admin>().HasData(
                new Admin { AdminId = 1, AppUserId = "3", SystemId = 1 }
            );

            // Seed SystemInfo
            modelBuilder.Entity<SystemInfo>().HasData(
                new SystemInfo { SystemInfoId = 1, Balance = 10000.00, Name = "Rea3ya", Email = "support@rea3ya.com", PhoneNumber = "+20212345678" }
            );

            // Seed Addresses
            modelBuilder.Entity<Address>().HasData(
                new Address { AddressId = 1, Location = "Cairo", DetailedAddress = "5th Settlement", CityId = 1, DoctorId = 1 }
            );

            // Seed TimeSlots
            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot { TimeSlotId = 1, StartTime = new DateTime(2025, 7, 21, 9, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2025, 7, 21, 12, 0, 0, DateTimeKind.Utc), DayOfWeek = WeekDays.Monday },
                new TimeSlot { TimeSlotId = 2, StartTime = new DateTime(2025, 7, 22, 14, 0, 0, DateTimeKind.Utc), EndTime = new DateTime(2025, 7, 22, 17, 0, 0, DateTimeKind.Utc), DayOfWeek = WeekDays.Tuesday }
            );

            // Seed DoctorTimeSlots
            modelBuilder.Entity<DoctorTimeSlot>().HasData(
                new DoctorTimeSlot { DoctorTimeSlotId = 1, DoctorId = 1, TimeSlotId = 1, IsAvailable = true },
                new DoctorTimeSlot { DoctorTimeSlotId = 2, DoctorId = 1, TimeSlotId = 2, IsAvailable = true }
            );

            // Seed Documents
            modelBuilder.Entity<Document>().HasData(
                new Document { DocumentId = 1, DocumentType = DocumentType.NationalId, FilePath = "cert_sara.pdf", UploadedAt = new DateTime(2025, 7, 1, 10, 0, 0, DateTimeKind.Utc), IsVerified = true, DoctorId = 1 },
                new Document { DocumentId = 2, DocumentType = DocumentType.ExperienceCertificate, FilePath = "id_sara.pdf", UploadedAt = new DateTime(2025, 7, 1, 10, 0, 0, DateTimeKind.Utc), IsVerified = true, DoctorId = 1 }
            );

            // Seed Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { AppointmentId = 1, Status = AppointmentStatus.Confirmed, CreatedAt = new DateTime(2025, 7, 19, 11, 0, 0, DateTimeKind.Utc), Notes = "Initial visit", PatientId = 1, DoctorTimeSlotId = 1, PaymentId = 1 }
            );

            // Seed Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment { PaymentId = 1, Amount = 500.00, Status = PaymentStatus.Completed, TransactionId = 123456, CreatedAt = new DateTime(2025, 7, 19, 11, 0, 0, DateTimeKind.Utc) }
            );

            // Seed Ratings
            modelBuilder.Entity<Rating>().HasData(
                new Rating { RatingId = 1, RatingValue = 4.8f, Comment = "Great service!", DoctorId = 1, PatientId = 1 }
            );

            base.OnModelCreating(modelBuilder);
        

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
