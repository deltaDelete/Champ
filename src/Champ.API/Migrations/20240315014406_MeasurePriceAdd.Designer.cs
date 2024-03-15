﻿// <auto-generated />
using System;
using Champ.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Champ.API.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240315014406_MeasurePriceAdd")]
    partial class MeasurePriceAdd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Champ.API.Models.Department", b =>
                {
                    b.Property<long>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Champ.API.Models.Diagnosis", b =>
                {
                    b.Property<long>("DiagnosisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Diagnosis");

                    b.HasKey("DiagnosisId");

                    b.ToTable("Diagnoses");
                });

            modelBuilder.Entity("Champ.API.Models.Doctor", b =>
                {
                    b.Property<long>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Champ.API.Models.Hospitalization", b =>
                {
                    b.Property<long>("HospitalizationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("AdditionalInfo")
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<DateTimeOffset>("DateEnd")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<DateTimeOffset>("DateStart")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<long>("DiagosisId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRejected")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("MedCardId")
                        .HasColumnType("bigint");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<string>("RejectionReason")
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.HasKey("HospitalizationId");

                    b.HasIndex("DiagosisId");

                    b.HasIndex("MedCardId");

                    b.ToTable("Hospitalizations");
                });

            modelBuilder.Entity("Champ.API.Models.InsuranceCompany", b =>
                {
                    b.Property<long>("InsuranceCompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("InsuranceCompanyId");

                    b.ToTable("InsuranceCompanies");
                });

            modelBuilder.Entity("Champ.API.Models.Measure", b =>
                {
                    b.Property<long>("MeasureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("DoctorId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("MeasureDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("MeasureName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long>("MeasureTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("MedCardId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Recommendations")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.Property<string>("Results")
                        .IsRequired()
                        .HasMaxLength(4096)
                        .HasColumnType("varchar(4096)");

                    b.HasKey("MeasureId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("MeasureTypeId");

                    b.HasIndex("MedCardId");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("Champ.API.Models.MeasureType", b =>
                {
                    b.Property<long>("MeasureTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("MeasureTypeId");

                    b.ToTable("MeasureTypes");
                });

            modelBuilder.Entity("Champ.API.Models.MedCard", b =>
                {
                    b.Property<long>("MedCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateOfIssue")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.HasKey("MedCardId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedCards");
                });

            modelBuilder.Entity("Champ.API.Models.Patient", b =>
                {
                    b.Property<long>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long>("PassportNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("mediumblob");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Champ.API.Models.Policy", b =>
                {
                    b.Property<long>("PolicyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("TIMESTAMP");

                    b.Property<long>("InsuranceCompanyId")
                        .HasColumnType("bigint");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.HasKey("PolicyId");

                    b.HasIndex("InsuranceCompanyId");

                    b.HasIndex("PatientId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Champ.API.Models.Visit", b =>
                {
                    b.Property<int>("VisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("Date")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<long>("PatientId")
                        .HasColumnType("bigint");

                    b.HasKey("VisitId");

                    b.HasIndex("PatientId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("Champ.API.Models.Hospitalization", b =>
                {
                    b.HasOne("Champ.API.Models.Diagnosis", "Diagnosis")
                        .WithMany()
                        .HasForeignKey("DiagosisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Champ.API.Models.MedCard", "MedCard")
                        .WithMany()
                        .HasForeignKey("MedCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diagnosis");

                    b.Navigation("MedCard");
                });

            modelBuilder.Entity("Champ.API.Models.Measure", b =>
                {
                    b.HasOne("Champ.API.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Champ.API.Models.MeasureType", "MeasureType")
                        .WithMany()
                        .HasForeignKey("MeasureTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Champ.API.Models.MedCard", "MedCard")
                        .WithMany()
                        .HasForeignKey("MedCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("MeasureType");

                    b.Navigation("MedCard");
                });

            modelBuilder.Entity("Champ.API.Models.MedCard", b =>
                {
                    b.HasOne("Champ.API.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Champ.API.Models.Policy", b =>
                {
                    b.HasOne("Champ.API.Models.InsuranceCompany", "InsuranceCompany")
                        .WithMany()
                        .HasForeignKey("InsuranceCompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Champ.API.Models.Patient", "Patient")
                        .WithMany("Policies")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InsuranceCompany");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Champ.API.Models.Visit", b =>
                {
                    b.HasOne("Champ.API.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Champ.API.Models.Patient", b =>
                {
                    b.Navigation("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}
