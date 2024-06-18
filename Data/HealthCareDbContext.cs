using Microsoft.EntityFrameworkCore;

namespace HealthcareAppointmentApp.Data
{
    public class HealthCareDbContext : DbContext
    {
        public HealthCareDbContext(DbContextOptions<HealthCareDbContext> options) : base(options) { }
       
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Speciality> Specialities { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("APPOINTMENTS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DateTime).HasColumnName("DATETIME");
                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");
                entity.Property(e => e.PatientId).HasColumnName("PATIENT_ID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UPDATED_AT")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnAddOrUpdate();
                entity.Property(e => e.AppointmentStatus).HasColumnName("STATUS")
                    .HasConversion<string>()
                    .HasMaxLength(30).IsRequired();
                entity.HasOne(a => a.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(e => e.PatientId);
                entity.HasOne(a => a.Doctor)
                    .WithMany(d => d.Appointments)
                    .HasForeignKey(e => e.DoctorId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.HasIndex(e => e.Username, "UQ_USERNAME").IsUnique();
                entity.HasIndex(e => e.Email, "UQ_EMAIL").IsUnique();
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Username).HasMaxLength(40).HasColumnName("USERNAME");
                entity.Property(e => e.Password).HasMaxLength(80).HasColumnName("PASSWORD");
                entity.Property(e => e.Email).HasMaxLength(50).HasColumnName("EMAIL");
                entity.Property(e => e.UserRole).HasMaxLength(30).HasColumnName("ROLE").HasConversion<string>();
                entity.Property(e => e.Status).HasMaxLength(20).HasColumnName("USER_STATUS").HasConversion<string>();
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UPDATED_AT")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("PATIENTS");
                entity.HasIndex(p => p.Lastname, "IX_P_LASTNAME");
                entity.HasIndex(p => p.PhoneNumber, "UQ_P_PHONE_NUMBER").IsUnique();
                entity.Property(p => p.Id).HasColumnName("ID");
                entity.Property(p => p.Firstname).HasMaxLength(50).HasColumnName("FIRSTNAME");
                entity.Property(p => p.Lastname).HasMaxLength(50).HasColumnName("LASTNAME");
                entity.Property(p => p.Age).HasColumnName("AGE");
                entity.Property(p => p.Gender).HasMaxLength(10).HasColumnName("GENDER");
                entity.Property(p => p.MedicalHistory).HasMaxLength(-1).HasColumnName("MEDICAL_HISTORY");
                entity.Property(p => p.PhoneNumber).HasMaxLength(20).HasColumnName("PHONE_NUMBER");
                entity.Property(p => p.UserId).HasColumnName("USER_ID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UPDATED_AT")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnAddOrUpdate();
                entity.HasOne(p => p.User).WithOne(u => u.Patient)
                    .HasForeignKey<Patient>(p => p.UserId);
            });

            modelBuilder.Entity<Speciality>(entity =>
            {
                entity.ToTable("SPECIALITIES");
                entity.Property(s => s.Id).HasColumnName("ID");
                entity.Property(s => s.SpecialityName).HasMaxLength(80).HasColumnName("SPECIALITY_NAME");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UPDATED_AT")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnAddOrUpdate();
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("DOCTORS");
                entity.HasIndex(d => d.Lastname, "IX_D_LASTNAME");
                entity.HasIndex(d => d.PhoneNumber, "UQ_D_PHONE_NUMBER").IsUnique();
                entity.Property(d => d.Id).HasColumnName("ID");
                entity.Property(p => p.Firstname).HasMaxLength(50).HasColumnName("FIRSTNAME");
                entity.Property(d => d.Lastname).HasMaxLength(50).HasColumnName("LASTNAME");
                entity.Property(d => d.SpecialityId).HasColumnName("SPECIALITY_ID");
                entity.Property(d => d.UserId).HasColumnName("USER_ID");
                entity.Property(d => d.City).HasMaxLength(50).HasColumnName("CITY");
                entity.Property(d => d.Address).HasMaxLength(50).HasColumnName("ADDRESS");
                entity.Property(d => d.StreetNumber).HasColumnName("STREET_NUMBER");
                entity.Property(d => d.PhoneNumber).HasMaxLength(20).HasColumnName("PHONE_NUMBER");
                entity.Property(d => d.AppointmentDuration).HasColumnName("APPOINTMENT_DURATION").HasDefaultValue(30);
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UPDATED_AT")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnAddOrUpdate();
                entity.HasOne(d => d.User)
                    .WithOne(u => u.Doctor)
                    .HasForeignKey<Doctor>(d => d.UserId);
                entity.HasOne(d => d.Speciality)
                    .WithMany(s => s.Doctors)
                    .HasForeignKey(d => d.SpecialityId);
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.ToTable("TIMESLOTS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DoctorId).HasColumnName("DOCTOR_ID");
                entity.Property(e => e.Date).HasColumnName("DATE");
                entity.Property(e => e.StartTimeSlot).HasColumnName("START_TIME");
                entity.Property(e => e.EndTimeSlot).HasColumnName("END_TIME");
                entity.Property(e => e.Status).HasColumnName("STATUS").HasConversion<string>();
                entity.Property(e => e.CreatedAt).HasColumnName("CREATED_AT").HasDefaultValueSql("getdate()");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UPDATED_AT")
                    .HasDefaultValueSql("getdate()")
                    .ValueGeneratedOnAddOrUpdate();
                entity.HasOne(e => e.Doctor)
                    .WithMany(d => d.TimeSlots)
                    .HasForeignKey(e => e.DoctorId);
            });
        }
    }
}
