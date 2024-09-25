﻿// <auto-generated />
using System;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObjects.Migrations
{
    [DbContext(typeof(SEP490_G49Context))]
    [Migration("20240722143629_UpdateDepartmentRole")]
    partial class UpdateDepartmentRole
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObjects.Models.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CvDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateApply")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid?>("JobPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("JobPostId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("BusinessObjects.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("BusinessObjects.Models.Family", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Relationship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserInformationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserInformationId");

                    b.ToTable("Families");
                });

            modelBuilder.Entity("BusinessObjects.Models.JobPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Benefits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ExperienceYear")
                        .HasColumnType("float");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfRecruits")
                        .HasColumnType("int");

                    b.Property<string>("Requirements")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("JobPosts");
                });

            modelBuilder.Entity("BusinessObjects.Models.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RedirectUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("BusinessObjects.Models.OKR", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Achieved")
                        .HasColumnType("int");

                    b.Property<string>("ActionPlan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ActionPlanDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproveStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfidenceLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cycle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Owner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scope")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TargerNumber")
                        .HasColumnType("int");

                    b.Property<int>("TargetProgress")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitOfTarget")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("OKRs");
                });

            modelBuilder.Entity("BusinessObjects.Models.OkrHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NewProgress")
                        .HasColumnType("int");

                    b.Property<string>("NewStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OkrId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("OldProgress")
                        .HasColumnType("int");

                    b.Property<string>("OldStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("Owner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OkrId");

                    b.ToTable("okrHistories");
                });

            modelBuilder.Entity("BusinessObjects.Models.Role", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BusinessObjects.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AccessTokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RefreshTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("TemporaryPasswordExpires")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("TemporaryPasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("TemporaryPasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserFile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFiles");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserHistories");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcademicLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressOfBirth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressOfProvide")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankingNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfProvide")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DriverLicenseIssueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverLicenseIssuePlace")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverLicenseNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ethnic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HealthyStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HiCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeTown")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdCardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPartyMember")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUnionMember")
                        .HasColumnType("bit");

                    b.Property<string>("MatitalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportIssuedAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PassportIssuedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassportNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PitCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Religious")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SiCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserInformations");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserRole", b =>
                {
                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("BusinessObjects.Models.Candidate", b =>
                {
                    b.HasOne("BusinessObjects.Models.JobPost", "JobPost")
                        .WithMany("Candidates")
                        .HasForeignKey("JobPostId");

                    b.Navigation("JobPost");
                });

            modelBuilder.Entity("BusinessObjects.Models.Family", b =>
                {
                    b.HasOne("BusinessObjects.Models.UserInformation", "UserInformation")
                        .WithMany("FamilyInformation")
                        .HasForeignKey("UserInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInformation");
                });

            modelBuilder.Entity("BusinessObjects.Models.JobPost", b =>
                {
                    b.HasOne("BusinessObjects.Models.Department", "Department")
                        .WithMany("Posts")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("BusinessObjects.Models.Notification", b =>
                {
                    b.HasOne("BusinessObjects.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.Models.OKR", b =>
                {
                    b.HasOne("BusinessObjects.Models.Department", "Department")
                        .WithMany("OKRs")
                        .HasForeignKey("DepartmentId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("BusinessObjects.Models.OkrHistory", b =>
                {
                    b.HasOne("BusinessObjects.Models.OKR", "OKR")
                        .WithMany("OkrHistories")
                        .HasForeignKey("OkrId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OKR");
                });

            modelBuilder.Entity("BusinessObjects.Models.User", b =>
                {
                    b.HasOne("BusinessObjects.Models.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserFile", b =>
                {
                    b.HasOne("BusinessObjects.Models.UserInformation", "UserInformation")
                        .WithMany("UserFiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInformation");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserHistory", b =>
                {
                    b.HasOne("BusinessObjects.Models.User", "User")
                        .WithMany("UserHistories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserInformation", b =>
                {
                    b.HasOne("BusinessObjects.Models.User", "User")
                        .WithOne("UserInformation")
                        .HasForeignKey("BusinessObjects.Models.UserInformation", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserRole", b =>
                {
                    b.HasOne("BusinessObjects.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObjects.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObjects.Models.Department", b =>
                {
                    b.Navigation("OKRs");

                    b.Navigation("Posts");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObjects.Models.JobPost", b =>
                {
                    b.Navigation("Candidates");
                });

            modelBuilder.Entity("BusinessObjects.Models.OKR", b =>
                {
                    b.Navigation("OkrHistories");
                });

            modelBuilder.Entity("BusinessObjects.Models.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BusinessObjects.Models.User", b =>
                {
                    b.Navigation("Notifications");

                    b.Navigation("UserHistories");

                    b.Navigation("UserInformation");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("BusinessObjects.Models.UserInformation", b =>
                {
                    b.Navigation("FamilyInformation");

                    b.Navigation("UserFiles");
                });
#pragma warning restore 612, 618
        }
    }
}
