﻿// <auto-generated />
using System;
using HealthcareAppointmentApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthcareAppointmentApp.Migrations
{
    [DbContext(typeof(HealthCareDbContext))]
    [Migration("20240609231506_AddConstraintsPhoneNumber")]
    partial class AddConstraintsPhoneNumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Appointment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("AppointmentStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("STATUS");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATETIME");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint")
                        .HasColumnName("DOCTOR_ID");

                    b.Property<long?>("PatientId")
                        .HasColumnType("bigint")
                        .HasColumnName("PATIENT_ID");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("APPOINTMENTS", (string)null);
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.AvailableTimeSlot", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("DATE");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint")
                        .HasColumnName("DOCTOR_ID");

                    b.Property<TimeOnly>("EndTimeSlot")
                        .HasColumnType("time")
                        .HasColumnName("END_TIME");

                    b.Property<TimeOnly>("StartTimeSlot")
                        .HasColumnType("time")
                        .HasColumnName("START_TIME");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("AVAILABLE_TIMESLOTS", (string)null);
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Doctor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("ADDRESS");

                    b.Property<long>("AppointmentDuration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValue(30L)
                        .HasColumnName("APPOINTMENT_DURATION");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("CITY");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FIRSTNAME");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LASTNAME");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<long?>("SpecialityId")
                        .HasColumnType("bigint")
                        .HasColumnName("SPECIALITY_ID");

                    b.Property<long>("StreetNumber")
                        .HasColumnType("bigint")
                        .HasColumnName("STREET_NUMBER");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex(new[] { "Lastname" }, "IX_D_LASTNAME");

                    b.HasIndex(new[] { "PhoneNumber" }, "UQ_D_PHONE_NUMBER")
                        .IsUnique();

                    b.ToTable("DOCTORS", (string)null);
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Patient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Age")
                        .HasColumnType("bigint")
                        .HasColumnName("AGE");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FIRSTNAME");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("GENDER");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LASTNAME");

                    b.Property<string>("MedicalHistory")
                        .HasMaxLength(-1)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MEDICAL_HISTORY");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.HasIndex(new[] { "Lastname" }, "IX_P_LASTNAME");

                    b.HasIndex(new[] { "PhoneNumber" }, "UQ_P_PHONE_NUMBER")
                        .IsUnique();

                    b.ToTable("PATIENTS", (string)null);
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Speciality", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("SpecialityName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("SPECIALITY_NAME");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.ToTable("SPECIALITIES", (string)null);
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("CREATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("EMAIL");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("USER_STATUS");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasColumnName("UPDATED_AT")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("ROLE");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "UQ_EMAIL")
                        .IsUnique();

                    b.HasIndex(new[] { "Username" }, "UQ_USERNAME")
                        .IsUnique();

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Appointment", b =>
                {
                    b.HasOne("HealthcareAppointmentApp.Data.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HealthcareAppointmentApp.Data.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.AvailableTimeSlot", b =>
                {
                    b.HasOne("HealthcareAppointmentApp.Data.Doctor", "Doctor")
                        .WithMany("AvailableTimeSlots")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Doctor", b =>
                {
                    b.HasOne("HealthcareAppointmentApp.Data.Speciality", "Speciality")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialityId");

                    b.HasOne("HealthcareAppointmentApp.Data.User", "User")
                        .WithOne("Doctor")
                        .HasForeignKey("HealthcareAppointmentApp.Data.Doctor", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Speciality");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Patient", b =>
                {
                    b.HasOne("HealthcareAppointmentApp.Data.User", "User")
                        .WithOne("Patient")
                        .HasForeignKey("HealthcareAppointmentApp.Data.Patient", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Doctor", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("AvailableTimeSlots");
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Patient", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.Speciality", b =>
                {
                    b.Navigation("Doctors");
                });

            modelBuilder.Entity("HealthcareAppointmentApp.Data.User", b =>
                {
                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
