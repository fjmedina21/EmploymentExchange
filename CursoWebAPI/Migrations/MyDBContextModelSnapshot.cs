﻿// <auto-generated />
using System;
using EmploymentExchange.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmploymentExchange.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmploymentExchange.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EmploymentExchange.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RecruiterEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("EmploymentExchange.Models.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<Guid>("JobPositionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("JobPositionId");

                    b.HasIndex("JobTypeId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("EmploymentExchange.Models.JobPosition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("JobPositions");
                });

            modelBuilder.Entity("EmploymentExchange.Models.JobType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("JobTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b908c4e9-c250-4bec-976a-a13c930f572a"),
                            CreatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8012),
                            Name = "Full-time",
                            State = true,
                            UpdatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8032)
                        },
                        new
                        {
                            Id = new Guid("8402369e-0b1f-4cb7-9157-d93989f80856"),
                            CreatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8045),
                            Name = "Part-time",
                            State = true,
                            UpdatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8046)
                        },
                        new
                        {
                            Id = new Guid("50151c78-2079-4db5-bee8-e422a53dae5c"),
                            CreatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8050),
                            Name = "Contract",
                            State = true,
                            UpdatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8051)
                        },
                        new
                        {
                            Id = new Guid("3bb18c14-99c8-4469-aa09-8fb4648f336d"),
                            CreatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8054),
                            Name = "Internship",
                            State = true,
                            UpdatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8055)
                        });
                });

            modelBuilder.Entity("EmploymentExchange.Models.JobUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("JobUser");
                });

            modelBuilder.Entity("EmploymentExchange.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2ec4f022-3209-40d5-a4e3-3f8243048d08"),
                            CreatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8061),
                            Description = "Site owner",
                            Name = "Admin",
                            State = true,
                            UpdatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8062)
                        },
                        new
                        {
                            Id = new Guid("9a59725c-a3ae-453c-93b2-e9924a427058"),
                            CreatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8068),
                            Description = "Recruiter, looking for employ personal",
                            Name = "Poster",
                            State = true,
                            UpdatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8069)
                        },
                        new
                        {
                            Id = new Guid("259470d3-b08f-4a84-9ac2-1625fdbaa5ee"),
                            CreatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8073),
                            Description = "Employee, looking for a job",
                            Name = "User",
                            State = true,
                            UpdatedAt = new DateTime(2023, 7, 2, 17, 32, 18, 288, DateTimeKind.Local).AddTicks(8074)
                        });
                });

            modelBuilder.Entity("EmploymentExchange.Models.RoleUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("EmploymentExchange.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_Users_Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EmploymentExchange.Models.Job", b =>
                {
                    b.HasOne("EmploymentExchange.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmploymentExchange.Models.JobPosition", "JobPosition")
                        .WithMany()
                        .HasForeignKey("JobPositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmploymentExchange.Models.JobType", "JobType")
                        .WithMany()
                        .HasForeignKey("JobTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("JobPosition");

                    b.Navigation("JobType");
                });

            modelBuilder.Entity("EmploymentExchange.Models.JobPosition", b =>
                {
                    b.HasOne("EmploymentExchange.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EmploymentExchange.Models.JobUser", b =>
                {
                    b.HasOne("EmploymentExchange.Models.Job", "Job")
                        .WithMany("JobsUsers")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmploymentExchange.Models.User", "User")
                        .WithMany("JobsUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmploymentExchange.Models.RoleUser", b =>
                {
                    b.HasOne("EmploymentExchange.Models.Role", "Role")
                        .WithMany("RolesUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmploymentExchange.Models.User", "User")
                        .WithMany("RolesUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EmploymentExchange.Models.Job", b =>
                {
                    b.Navigation("JobsUsers");
                });

            modelBuilder.Entity("EmploymentExchange.Models.Role", b =>
                {
                    b.Navigation("RolesUsers");
                });

            modelBuilder.Entity("EmploymentExchange.Models.User", b =>
                {
                    b.Navigation("JobsUsers");

                    b.Navigation("RolesUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
