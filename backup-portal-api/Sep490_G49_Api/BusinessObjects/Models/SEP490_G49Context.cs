using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace BusinessObjects.Models
{
    public class SEP490_G49Context : DbContext
    {
        public SEP490_G49Context()
        {

        }
        public SEP490_G49Context(DbContextOptions<SEP490_G49Context> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UserHistory> UserHistories { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<OKR> OKRs { get; set; }
        public DbSet<OkrHistory> okrHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroup_Role> UserGroup_Roles { get; set; }
        public DbSet<UserGroup_User> UserGroup_Users { get; set; }
        public DbSet<OkrUser> OkrUsers { get; set; }
        public DbSet<OkrDepartment> OkrDepartments { get; set; }

        public DbSet<CommentFile> CommentFiles { get; set; }

        public DbSet<PolicyStep> PolicySteps { get; set; }
        public DbSet<PolicyDocument> PolicyDocument { get; set; }
        public DbSet<PolicyStepUser> PolicyStepUsers { get; set; }
        public DbSet<PolicyStepDepartment> PolicyStepDepartments { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<SubmissionParticipant> SubmissionParticipants { get; set; }
        public DbSet<SubmissionDepartment> SubmissionDepartments { get; set; }
        public DbSet<SubmissionEvent> SubmissionEvents { get; set; }
        public DbSet<SubmissionFile> SubmissionFiles { get; set; }
        public DbSet<SubmissionComment> SubmissionComments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleAttachment> ScheduleAttachments { get; set; }
        public DbSet<ScheduleParticipant> ScheduleParticipants { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot conf = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(conf.GetConnectionString("SEP490_G49"));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Schedule>(e =>
            {
                e.ToTable("Schedules");
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).HasMaxLength(255).IsRequired();
                e.Property(x => x.Description).HasMaxLength(1000);
                e.HasOne(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(x => x.ApprovedBy).WithMany().HasForeignKey(x => x.ApprovedById).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<ScheduleAttachment>(e =>
            {
                e.ToTable("ScheduleAttachments");
                e.HasKey(x => x.Id);
                e.Property(x => x.FileName).HasMaxLength(255).IsRequired();
                e.Property(x => x.StoredPath).HasMaxLength(500).IsRequired();
                e.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
                e.HasOne(x => x.Schedule).WithMany(s => s.Attachments).HasForeignKey(x => x.ScheduleId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ScheduleParticipant>(e =>
            {
                e.ToTable("ScheduleParticipants");
                e.HasKey(x => new { x.ScheduleId, x.UserId });
                e.HasOne(x => x.Schedule).WithMany(s => s.Participants).HasForeignKey(x => x.ScheduleId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CommentFile>(e =>
       {
           e.ToTable("CommentFiles");
           e.HasKey(x => x.Id);
           e.Property(x => x.FileName).HasMaxLength(255).IsRequired();
           e.Property(x => x.StoredPath).HasMaxLength(500).IsRequired();
           e.Property(x => x.ContentType).HasMaxLength(100).IsRequired();
           e.HasOne(x => x.OkrHistory)
                           .WithMany(h => h.Attachments)
                           .HasForeignKey(x => x.OkrHistoryId)
                           .OnDelete(DeleteBehavior.Cascade);
       });

            modelBuilder.Entity<OkrUser>().ToTable("OkrUsers");
            modelBuilder.Entity<OkrUser>()
.HasKey(ou => new { ou.OkrId, ou.UserId, ou.Role });
            modelBuilder.Entity<OkrUser>()
                .HasOne(ou => ou.Okr)
                .WithMany(o => o.OkrUsers)
                .HasForeignKey(ou => ou.OkrId);
            modelBuilder.Entity<OkrUser>()
                .HasOne(ou => ou.User)
                .WithMany(u => u.OkrUsers)
                .HasForeignKey(ou => ou.UserId);

            modelBuilder.Entity<OkrDepartment>().ToTable("OkrDepartments");
            modelBuilder.Entity<OkrDepartment>().HasKey(od => new { od.OkrId, od.DepartmentId });
            modelBuilder.Entity<OkrDepartment>()
                .HasOne(od => od.Okr)
                .WithMany(o => o.OkrDepartments)
                .HasForeignKey(od => od.OkrId);
            modelBuilder.Entity<OkrDepartment>()
                .HasOne(od => od.Department)
                .WithMany(d => d.OkrDepartments)
                .HasForeignKey(od => od.DepartmentId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasOne(e => e.UserInformation)
                .WithOne(e => e.User)
                .HasForeignKey<UserInformation>(e => e.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<UserGroup_User>()
                .HasKey(ugu => new { ugu.UserGroupId, ugu.UserId });

            modelBuilder.Entity<UserGroup_User>()
                .HasOne(ugu => ugu.UserGroup)
                .WithMany(ug => ug.UserGroup_Users)
                .HasForeignKey(ugu => ugu.UserGroupId);

            modelBuilder.Entity<UserGroup_User>()
                .HasOne(ugu => ugu.User)
                .WithMany(u => u.UserGroup_Users)
                .HasForeignKey(ugu => ugu.UserId);

            modelBuilder.Entity<UserGroup_Role>()
                .HasKey(ugr => new { ugr.UserGroupId, ugr.RoleId });

            modelBuilder.Entity<UserGroup_Role>()
                .HasOne(ugr => ugr.UserGroup)
                .WithMany(ug => ug.UserGroup_Roles)
                .HasForeignKey(ugr => ugr.UserGroupId);

            modelBuilder.Entity<UserGroup_Role>()
                .HasOne(ugr => ugr.Role)
                .WithMany(r => r.UserGroup_Roles)
                .HasForeignKey(ugr => ugr.RoleId);

            // Thiết lập quan hệ giữa Role và Department
            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId);


            // Thiết lập quan hệ giữa User và UserHistory
            modelBuilder.Entity<UserHistory>()
                .HasOne(uh => uh.User)
                .WithMany(u => u.UserHistories)
                .HasForeignKey(uh => uh.UserId);

            // Thiết lập quan hệ giữa UserInformation và Family
            modelBuilder.Entity<Family>()
                .HasOne(f => f.UserInformation)
                .WithMany(ui => ui.FamilyInformation)
                .HasForeignKey(f => f.UserInformationId);

            modelBuilder.Entity<UserInformation>()
            .HasMany(u => u.UserFiles)
            .WithOne(f => f.UserInformation)
            .HasForeignKey(f => f.UserId);


            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserPermissions)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.Permission)
                .WithMany(p => p.UserPermissions)
                .HasForeignKey(up => up.PermissionId);
            var permissions = new List<Permission>
            {
                new Permission { Id = Guid.Parse("054ad0e5-acf7-4f83-815c-e5500bee5e6b"), Name = "Candidate:List" },
                new Permission { Id = Guid.Parse("8a587770-c2c8-4eca-93a2-9e165b909d7b"), Name = "Candidate:Update" },
                new Permission { Id = Guid.Parse("0e593329-b61a-4ec2-a43d-878fa01a5867"), Name = "Candidate:Create" },
                new Permission { Id = Guid.Parse("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"), Name = "JobPost:List" },
                new Permission { Id = Guid.Parse("e477af98-e937-40b1-aee9-c53c8a91d954"), Name = "JobPost:Detail" },
                new Permission { Id = Guid.Parse("6b6d688a-40b8-48f1-9e20-5762ad427715"), Name = "JobPost:Create" },
                new Permission { Id = Guid.Parse("d192acc4-1ee7-4cf7-838e-0680076a78c3"), Name = "JobPost:Update" },
                new Permission { Id = Guid.Parse("01ce805d-e56f-4837-9f27-f4e31e67a22e"), Name = "Employee:List" },
                new Permission { Id = Guid.Parse("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"), Name = "Employee:Create" },
                new Permission { Id = Guid.Parse("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"), Name = "Employee:Edit" },
                new Permission { Id = Guid.Parse("c6df77f3-81a5-4c57-b116-53a7f21c4986"), Name = "Employee:UpdateStatus" },
                new Permission { Id = Guid.Parse("84f5598f-98b9-4c82-b212-b63efca554f2"), Name = "EmployeeFamily:List" },
                new Permission { Id = Guid.Parse("cbbcc181-d1fb-4970-9949-777a1a3ce387"), Name = "EmployeeFamily:Create" },
                new Permission { Id = Guid.Parse("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"), Name = "EmployeeFamily:Edit" },
                new Permission { Id = Guid.Parse("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"), Name = "EmployeeFamily:Delete" },
                new Permission { Id = Guid.Parse("0829e08b-583e-4ac3-a349-e9d56ef71a9d"), Name = "EmployeeHistory:List" },
                new Permission { Id = Guid.Parse("d7811916-2ffc-426a-8561-3013225cf9e2"), Name = "Okr:List" },
                new Permission { Id = Guid.Parse("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"), Name = "Okr:Detail" },
                new Permission { Id = Guid.Parse("ded1da72-2523-41f4-a651-330ca8804e3b"), Name = "Okr:Comment" },
                new Permission { Id = Guid.Parse("3f4456ad-6589-4896-89b6-a18e4d8b7086"), Name = "Okr:History" },
                new Permission { Id = Guid.Parse("2df7174c-c270-4021-9e86-69faa72086a1"), Name = "Okr:EditProgress" },
                new Permission { Id = Guid.Parse("4550e1f6-57c0-4b9f-8362-4562bfa88826"), Name = "Okr:EditOwner" },
                new Permission { Id = Guid.Parse("4d77c298-d02f-420f-ad98-936e37f26ec1"), Name = "OkrRequest:List" },
                new Permission { Id = Guid.Parse("18b6356d-2bb2-4525-baab-214afaf13faf"), Name = "OkrRequest:Create" },
                new Permission { Id = Guid.Parse("0a6608a6-b1f9-44cf-9e7d-8d033c548947"), Name = "OkrRequest:Edit" },
                new Permission { Id = Guid.Parse("5b96eab3-85fe-4d9f-b625-db72fe1d8455"), Name = "OkrRequest:Update" },
                new Permission { Id = Guid.Parse("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"), Name = "OkrRequest:Detail" },
                new Permission { Id = Guid.Parse("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"), Name = "Admin" },
                new Permission { Id = Guid.Parse("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"), Name = "Common" },
                new Permission { Id = Guid.Parse("4f9068dd-6f69-483f-97fa-77a79b59edf9"), Name = "EmployeeInformation:Detail" },
                new Permission { Id = Guid.Parse("9e89898e-ad66-4b6b-b607-1353d22dd71b"), Name = "EmployeeInformation:Edit" },
                new Permission { Id = Guid.Parse("380c2e30-9242-4401-879f-3756ad0156ef"), Name = "Employee:UpdatePosition" }

            };



            modelBuilder.Entity<Permission>().HasData(permissions);
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                    Name = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "Administrator role with all permissions.",
                    Type = "Basic"
                },
                new Role
                {
                    Id = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"),
                    Name = "HR",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "Human Resources.",
                    Type = "Basic"
                },
                new Role
                {
                    Id = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"),
                    Name = "Employee",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "Employees.",
                    Type = "Basic"
                },
                new Role
                {
                    Id = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"),
                    Name = "BOD",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "Board of Directors",
                    Type = "Basic"
                },
                new Role
                {
                    Id = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"),
                    Name = "Manager",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Description = "Manager Of Department",
                    Type = "Basic"
                }
            );
            CreatePasswordHash("VanPhuong1507@", out byte[] passwordHash, out byte[] passwordSalt);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("12240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                    Username = "AdminSHR",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    CreatedAt = DateTime.Now,
                    IsDeleted = false,
                    Status = true,
                    RoleId = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"),
                    DepartmentId = Guid.Parse("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                }
            );
            modelBuilder.Entity<UserInformation>().HasData(
                new UserInformation
                {
                    Id = Guid.Parse("a852b906-1eac-44ba-bcbe-4f43ad62af11"),
                    UserId = Guid.Parse("62240fc0-2a2a-4b45-9ba5-6a57667413c2"),
                    Status = "Active",
                    FullName = "AdminSHR",
                    Code = "ADMIN"
                }
            );
            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = Guid.Parse("7bf10d6a-c521-44be-804e-8f70e6ae26d1"),
                    Name = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    Description = "Admin",
                }
            );
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("2df7174c-c270-4021-9e86-69faa72086a1"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("3f4456ad-6589-4896-89b6-a18e4d8b7086"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("84f5598f-98b9-4c82-b212-b63efca554f2"),
                    IsEnabled = true // EmployeeHistory:List
                },


                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("0829e08b-583e-4ac3-a349-e9d56ef71a9d"),
                    IsEnabled = true // EmployeeHistory:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("4f9068dd-6f69-483f-97fa-77a79b59edf9"),
                    IsEnabled = true // EmployeeHistory:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"),
                    IsEnabled = true // EmployeeHistory:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("18b6356d-2bb2-4525-baab-214afaf13faf"),
                    IsEnabled = true // OkrRequest:Create
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("0a6608a6-b1f9-44cf-9e7d-8d033c548947"),
                    IsEnabled = true // OkrRequest:Edit
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("4d77c298-d02f-420f-ad98-936e37f26ec1"),
                    IsEnabled = true // OkrRequest:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("d7811916-2ffc-426a-8561-3013225cf9e2"),
                    IsEnabled = true // Okr:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("ded1da72-2523-41f4-a651-330ca8804e3b"),
                    IsEnabled = true // Okr:Comment
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"),
                    IsEnabled = true // Okr:Detail
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"),
                    IsEnabled = true // EmployeeFamily:Delete
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"),
                    IsEnabled = true // EmployeeFamily:Edit
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("5659d538-3a19-4c5a-aec8-2c024cba0f05"), // Employee Role Id
                    PermissionId = Guid.Parse("cbbcc181-d1fb-4970-9949-777a1a3ce387"),
                    IsEnabled = true // EmployeeFamily:Create
                }
            );
            var hrRoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5");

            // Thêm các RolePermission liên kết các permission với role HR
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("4d77c298-d02f-420f-ad98-936e37f26ec1"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("4550e1f6-57c0-4b9f-8362-4562bfa88826"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("2df7174c-c270-4021-9e86-69faa72086a1"),
                    IsEnabled = true // Employee:Edit
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("84f5598f-98b9-4c82-b212-b63efca554f2"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("c6df77f3-81a5-4c57-b116-53a7f21c4986"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("01ce805d-e56f-4837-9f27-f4e31e67a22e"),
                    IsEnabled = true // EmployeeHistory:List
                }, new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"),
                    IsEnabled = true // EmployeeHistory:List
                },



                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("0829e08b-583e-4ac3-a349-e9d56ef71a9d"),
                    IsEnabled = true // EmployeeHistory:List
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("4f9068dd-6f69-483f-97fa-77a79b59edf9"),
                    IsEnabled = true // EmployeeHistory:List
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"),
                    IsEnabled = true // EmployeeHistory:List
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("18b6356d-2bb2-4525-baab-214afaf13faf"),
                    IsEnabled = true // OkrRequest:Create
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("0a6608a6-b1f9-44cf-9e7d-8d033c548947"),
                    IsEnabled = true // OkrRequest:Edit
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("d7811916-2ffc-426a-8561-3013225cf9e2"),
                    IsEnabled = true // Okr:List
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("ded1da72-2523-41f4-a651-330ca8804e3b"),
                    IsEnabled = true // Okr:Comment
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"),
                    IsEnabled = true // Okr:Detail
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"),
                    IsEnabled = true // EmployeeFamily:Delete
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"),
                    IsEnabled = true // EmployeeFamily:Edit
                },
                new RolePermission
                {
                    RoleId = hrRoleId,
                    PermissionId = Guid.Parse("cbbcc181-d1fb-4970-9949-777a1a3ce387"),
                    IsEnabled = true // EmployeeFamily:Create
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), // HR Role Id
                    PermissionId = Guid.Parse("054ad0e5-acf7-4f83-815c-e5500bee5e6b"),
                    IsEnabled = true // Candidate:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), // HR Role Id
                    PermissionId = Guid.Parse("8a587770-c2c8-4eca-93a2-9e165b909d7b"),
                    IsEnabled = true // Candidate:Update
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), // HR Role Id
                    PermissionId = Guid.Parse("0e593329-b61a-4ec2-a43d-878fa01a5867"),
                    IsEnabled = true // Candidate:Create
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), // HR Role Id
                    PermissionId = Guid.Parse("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"),
                    IsEnabled = true // JobPost:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), // HR Role Id
                    PermissionId = Guid.Parse("e477af98-e937-40b1-aee9-c53c8a91d954"),
                    IsEnabled = true // JobPost:Detail
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), // HR Role Id
                    PermissionId = Guid.Parse("6b6d688a-40b8-48f1-9e20-5762ad427715"),
                    IsEnabled = true // JobPost:Create
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("4f2dbb46-273e-4aff-b0ae-4a5fa528aaa5"), // HR Role Id
                    PermissionId = Guid.Parse("d192acc4-1ee7-4cf7-838e-0680076a78c3"),
                    IsEnabled = true // JobPost:Update
                }
            );
            //Manager
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission
                {
                    RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                    PermissionId = Guid.Parse("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"),
                    IsEnabled = true // Employee:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                    PermissionId = Guid.Parse("4550e1f6-57c0-4b9f-8362-4562bfa88826"),
                    IsEnabled = true // Employee:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                    PermissionId = Guid.Parse("2df7174c-c270-4021-9e86-69faa72086a1"),
                    IsEnabled = true // Employee:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                    PermissionId = Guid.Parse("3f4456ad-6589-4896-89b6-a18e4d8b7086"),
                    IsEnabled = true // Employee:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                    PermissionId = Guid.Parse("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"),
                    IsEnabled = true // Employee:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                    PermissionId = Guid.Parse("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"),
                    IsEnabled = true // Employee:List
                }, new RolePermission
                {
                    RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                    PermissionId = Guid.Parse("cbbcc181-d1fb-4970-9949-777a1a3ce387"),
                    IsEnabled = true // Employee:List
                },

            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("01ce805d-e56f-4837-9f27-f4e31e67a22e"),
                IsEnabled = true // Employee:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("4f9068dd-6f69-483f-97fa-77a79b59edf9"),
                IsEnabled = true // Employee:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"),
                IsEnabled = true // Employee:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"),
                IsEnabled = true // Employee:Create
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"),
                IsEnabled = true // Employee:Edit
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("c6df77f3-81a5-4c57-b116-53a7f21c4986"),
                IsEnabled = true // Employee:Update
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("84f5598f-98b9-4c82-b212-b63efca554f2"),
                IsEnabled = true // EmployeeFamily:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("0829e08b-583e-4ac3-a349-e9d56ef71a9d"),
                IsEnabled = true // EmployeeHistory:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("d7811916-2ffc-426a-8561-3013225cf9e2"),
                IsEnabled = true // Okr:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"),
                IsEnabled = true // Okr:Detail
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("ded1da72-2523-41f4-a651-330ca8804e3b"),
                IsEnabled = true // Okr:Comment
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("4d77c298-d02f-420f-ad98-936e37f26ec1"),
                IsEnabled = true // OkrRequest:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("18b6356d-2bb2-4525-baab-214afaf13faf"),
                IsEnabled = true // OkrRequest:Create
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("0a6608a6-b1f9-44cf-9e7d-8d033c548947"),
                IsEnabled = true // OkrRequest:Edit
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("5b96eab3-85fe-4d9f-b625-db72fe1d8455"),
                IsEnabled = true // OkrRequest:Update
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("054ad0e5-acf7-4f83-815c-e5500bee5e6b"),
                IsEnabled = true // Candidate:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("8a587770-c2c8-4eca-93a2-9e165b909d7b"),
                IsEnabled = true // Candidate:Update
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"),
                IsEnabled = true // JobPost:List
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("e477af98-e937-40b1-aee9-c53c8a91d954"),
                IsEnabled = true // JobPost:Detail
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("6b6d688a-40b8-48f1-9e20-5762ad427715"),
                IsEnabled = true // JobPost:Create
            },
            new RolePermission
            {
                RoleId = Guid.Parse("bc2e856e-0abd-4ca1-9c5f-7f4ea3faa14d"), // Manager Role Id
                PermissionId = Guid.Parse("d192acc4-1ee7-4cf7-838e-0680076a78c3"),
                IsEnabled = true // JobPost:Update
            }
        );
            //BOD
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("ba2a70b3-10ee-4e5f-94aa-35a1db617c10"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("daefe1d3-28b3-4901-9e20-41ccd74f3b5e"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("4550e1f6-57c0-4b9f-8362-4562bfa88826"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("2df7174c-c270-4021-9e86-69faa72086a1"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("3f4456ad-6589-4896-89b6-a18e4d8b7086"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("cbbcc181-d1fb-4970-9949-777a1a3ce387"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("8fb6d3a0-daa5-4ea9-b59c-6541ddf65ebb"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("01ce805d-e56f-4837-9f27-f4e31e67a22e"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("4f9068dd-6f69-483f-97fa-77a79b59edf9"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"),
                    IsEnabled = true // Employee:Create
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("2e607831-8cd6-41fa-b934-9a2ac19dfc1d"),
                    IsEnabled = true // Employee:Edit
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("c6df77f3-81a5-4c57-b116-53a7f21c4986"),
                    IsEnabled = true // Employee:Update
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("84f5598f-98b9-4c82-b212-b63efca554f2"),
                    IsEnabled = true // EmployeeFamily:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("0829e08b-583e-4ac3-a349-e9d56ef71a9d"),
                    IsEnabled = true // EmployeeHistory:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("d7811916-2ffc-426a-8561-3013225cf9e2"),
                    IsEnabled = true // Okr:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("a0508a87-2c38-48ea-bbc3-8dd25cf2c584"),
                    IsEnabled = true // Okr:Detail
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("ded1da72-2523-41f4-a651-330ca8804e3b"),
                    IsEnabled = true // Okr:Comment
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("4d77c298-d02f-420f-ad98-936e37f26ec1"),
                    IsEnabled = true // OkrRequest:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("0a6608a6-b1f9-44cf-9e7d-8d033c548947"),
                    IsEnabled = true // OkrRequest:Edit
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("5b96eab3-85fe-4d9f-b625-db72fe1d8455"),
                    IsEnabled = true // OkrRequest:Update
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("054ad0e5-acf7-4f83-815c-e5500bee5e6b"),
                    IsEnabled = true // Candidate:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("8a587770-c2c8-4eca-93a2-9e165b909d7b"),
                    IsEnabled = true // Candidate:Update
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("f4c9a5d6-2c32-48a3-b982-931769f7b7fe"),
                    IsEnabled = true // JobPost:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("e477af98-e937-40b1-aee9-c53c8a91d954"),
                    IsEnabled = true // JobPost:Detail
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("6b6d688a-40b8-48f1-9e20-5762ad427715"),
                    IsEnabled = true // JobPost:Create
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("7d4e0498-36dd-478c-ab11-b28c25e9da0f"), // Manager Role Id
                    PermissionId = Guid.Parse("d192acc4-1ee7-4cf7-838e-0680076a78c3"),
                    IsEnabled = true // JobPost:Update
                }
            );
            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission
                {
                    RoleId = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), // Admin Role Id
                    PermissionId = Guid.Parse("a3b2674d-c3bd-44b2-b6a9-cd11cc10339b"),
                    IsEnabled = true // Common
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), // Admin Role Id
                    PermissionId = Guid.Parse("01ce805d-e56f-4837-9f27-f4e31e67a22e"),
                    IsEnabled = true // Employee:List
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), // Admin Role Id
                    PermissionId = Guid.Parse("eec37f38-d5b5-4a16-ab3f-4ebc5b747d34"),
                    IsEnabled = true // Employee:Create
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), // Admin Role Id
                    PermissionId = Guid.Parse("c6df77f3-81a5-4c57-b116-53a7f21c4986"),
                    IsEnabled = true // Employee:Update
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), // Admin Role Id
                    PermissionId = Guid.Parse("f2fbfa3c-a3f9-4909-830c-7fe16999dc5c"),
                    IsEnabled = true // Admin
                },
                new RolePermission
                {
                    RoleId = Guid.Parse("9b80c7d9-7417-4a2c-9f93-919f18a89dd7"), // Admin Role Id
                    PermissionId = Guid.Parse("380c2e30-9242-4401-879f-3756ad0156ef"),
                    IsEnabled = true // Update Position
                }
            );
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
