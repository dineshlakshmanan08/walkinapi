using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Walkin.Models;

public partial class QuantumContext : DbContext
{
    public QuantumContext()
    {
    }

    public QuantumContext(DbContextOptions<QuantumContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Applicationsrole> Applicationsroles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Technology> Technologies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userexpertisetech> Userexpertiseteches { get; set; }

    public virtual DbSet<Userfamiliartech> Userfamiliarteches { get; set; }

    public virtual DbSet<Userpreferredrole> Userpreferredroles { get; set; }

    public virtual DbSet<Walkin> Walkins { get; set; }

    public virtual DbSet<Walkinrole> Walkinroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=zeus@123;database=quantum", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationsId).HasName("PRIMARY");

            entity.ToTable("applications");

            entity.HasIndex(e => e.ApplicationsId, "Applications_id").IsUnique();

            entity.HasIndex(e => e.UserId, "applications_ibfk_1");

            entity.HasIndex(e => e.WalkinId, "applications_ibfk_2");

            entity.Property(e => e.ApplicationsId).HasColumnName("Applications_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.EndTime)
                .HasColumnType("time")
                .HasColumnName("End_time");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.StartTime)
                .HasColumnType("time")
                .HasColumnName("Start_time");
            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.WalkinId).HasColumnName("Walkin_id");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applications_ibfk_1");

            entity.HasOne(d => d.Walkin).WithMany(p => p.Applications)
                .HasForeignKey(d => d.WalkinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applications_ibfk_2");
        });

        modelBuilder.Entity<Applicationsrole>(entity =>
        {
            entity.HasKey(e => e.ApplicationsRolesId).HasName("PRIMARY");

            entity.ToTable("applicationsroles");

            entity.HasIndex(e => e.ApplicationsRolesId, "ApplicationsRoles_id").IsUnique();

            entity.HasIndex(e => e.ApplicationsId, "applicationsroles_ibfk_1");

            entity.HasIndex(e => e.RoleId, "applicationsroles_ibfk_2");

            entity.Property(e => e.ApplicationsRolesId).HasColumnName("ApplicationsRoles_id");
            entity.Property(e => e.ApplicationsId).HasColumnName("Applications_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");

            entity.HasOne(d => d.Applications).WithMany(p => p.Applicationsroles)
                .HasForeignKey(d => d.ApplicationsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applicationsroles_ibfk_1");

            entity.HasOne(d => d.Role).WithMany(p => p.Applicationsroles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("applicationsroles_ibfk_2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleId, "Role_id_UNIQUE").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.Descriptions).HasColumnType("text");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.Requirements).HasColumnType("text");
            entity.Property(e => e.RoleName)
                .HasMaxLength(30)
                .HasColumnName("Role_name");
        });

        modelBuilder.Entity<Technology>(entity =>
        {
            entity.HasKey(e => e.TechnologiesId).HasName("PRIMARY");

            entity.ToTable("technologies");

            entity.HasIndex(e => e.TechnologiesId, "Technologies_id_UNIQUE").IsUnique();

            entity.Property(e => e.TechnologiesId).HasColumnName("Technologies_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.Technology1)
                .HasMaxLength(20)
                .HasColumnName("Technology");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.EmailId, "Email_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.PhoneNo, "Phone_no_UNIQUE").IsUnique();

            entity.HasIndex(e => e.UserId, "User_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.GuiId, "gui_id_UNIQUE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("User_id");
            entity.Property(e => e.AggregatePercentage).HasColumnName("Aggregate_percentage");
            entity.Property(e => e.ApplicantType)
                .HasMaxLength(20)
                .HasColumnName("Applicant_type");
            entity.Property(e => e.AppliedInZeus).HasColumnName("Applied_in_zeus");
            entity.Property(e => e.AppliedRoleInZeus)
                .HasMaxLength(40)
                .HasColumnName("Applied_role_in_zeus");
            entity.Property(e => e.College).HasMaxLength(20);
            entity.Property(e => e.CollegeLocation)
                .HasMaxLength(20)
                .HasColumnName("College_location");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.CurrentCtc).HasColumnName("Current_Ctc");
            entity.Property(e => e.EmailId)
                .HasMaxLength(20)
                .HasColumnName("Email_id");
            entity.Property(e => e.ExpectedCtc).HasColumnName("Expected_Ctc");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("First_name");
            entity.Property(e => e.GuiId).HasColumnName("gui_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("Last_name");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.NoticePeriod).HasColumnName("Notice_period");
            entity.Property(e => e.NoticePeriodDurationInMonth).HasColumnName("Notice_period_duration_in_month");
            entity.Property(e => e.NoticePeriodEndDate).HasColumnName("Notice_period_end_date");
            entity.Property(e => e.PassingYear).HasColumnName("Passing_year");
            entity.Property(e => e.PasswordUser).HasColumnName("Password_user");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(20)
                .HasColumnName("Phone_no");
            entity.Property(e => e.Portfolio).HasMaxLength(20);
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(45)
                .HasColumnName("profile_image");
            entity.Property(e => e.Qualification).HasMaxLength(20);
            entity.Property(e => e.ResumeUser)
                .HasMaxLength(30)
                .HasColumnName("Resume_user");
            entity.Property(e => e.StreamUser)
                .HasMaxLength(20)
                .HasColumnName("Stream_user");
        });

        modelBuilder.Entity<Userexpertisetech>(entity =>
        {
            entity.HasKey(e => e.UserExpertiseTechId).HasName("PRIMARY");

            entity.ToTable("userexpertisetech");

            entity.HasIndex(e => e.UserExpertiseTechId, "UserExpertiseTech_id").IsUnique();

            entity.HasIndex(e => e.UserId, "userexpertisetech_ibfk_1");

            entity.HasIndex(e => e.TechnologiesId, "userexpertisetech_ibfk_2");

            entity.Property(e => e.UserExpertiseTechId).HasColumnName("UserExpertiseTech_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.TechnologiesId).HasColumnName("Technologies_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Technologies).WithMany(p => p.Userexpertiseteches)
                .HasForeignKey(d => d.TechnologiesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userexpertisetech_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Userexpertiseteches)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userexpertisetech_ibfk_1");
        });

        modelBuilder.Entity<Userfamiliartech>(entity =>
        {
            entity.HasKey(e => e.UserFamiliarTechId).HasName("PRIMARY");

            entity.ToTable("userfamiliartech");

            entity.HasIndex(e => e.UserFamiliarTechId, "UserFamiliarTech_id").IsUnique();

            entity.HasIndex(e => e.UserId, "userfamiliartech_ibfk_1");

            entity.HasIndex(e => e.TechnologiesId, "userfamiliartech_ibfk_2");

            entity.Property(e => e.UserFamiliarTechId).HasColumnName("UserFamiliarTech_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.TechnologiesId).HasColumnName("Technologies_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Technologies).WithMany(p => p.Userfamiliarteches)
                .HasForeignKey(d => d.TechnologiesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userfamiliartech_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Userfamiliarteches)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userfamiliartech_ibfk_1");
        });

        modelBuilder.Entity<Userpreferredrole>(entity =>
        {
            entity.HasKey(e => e.UserPreferredRolesId).HasName("PRIMARY");

            entity.ToTable("userpreferredroles");

            entity.HasIndex(e => e.UserPreferredRolesId, "UserPreferredRoles_id").IsUnique();

            entity.HasIndex(e => e.UserId, "userpreferredroles_ibfk_1");

            entity.HasIndex(e => e.RoleId, "userpreferredroles_ibfk_2");

            entity.Property(e => e.UserPreferredRolesId).HasColumnName("UserPreferredRoles_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Userpreferredroles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userpreferredroles_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Userpreferredroles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("userpreferredroles_ibfk_1");
        });

        modelBuilder.Entity<Walkin>(entity =>
        {
            entity.HasKey(e => e.WalkinId).HasName("PRIMARY");

            entity.ToTable("walkin");

            entity.HasIndex(e => e.WalkinId, "Walkin_id_UNIQUE").IsUnique();

            entity.Property(e => e.WalkinId).HasColumnName("Walkin_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.EndDate).HasColumnName("End_date");
            entity.Property(e => e.GeneralInstructions).HasColumnName("General_instructions");
            entity.Property(e => e.InstructionsForExam).HasColumnName("Instructions_for_exam");
            entity.Property(e => e.Internship).HasMaxLength(30);
            entity.Property(e => e.Location).HasMaxLength(30);
            entity.Property(e => e.MinimumRequirements).HasColumnName("Minimum_requirements");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.StartDate).HasColumnName("Start_date");
            entity.Property(e => e.ThingsToRemember).HasColumnName("Things_to_remember");
            entity.Property(e => e.Title).HasMaxLength(255);
            entity.Property(e => e.WalkinVenue).HasColumnName("Walkin_venue");
        });

        modelBuilder.Entity<Walkinrole>(entity =>
        {
            entity.HasKey(e => e.WalkInRolesId).HasName("PRIMARY");

            entity.ToTable("walkinroles");

            entity.HasIndex(e => e.WalkInRolesId, "WalkInRoles_id").IsUnique();

            entity.HasIndex(e => e.WalkinId, "walkinroles_ibfk_1");

            entity.HasIndex(e => e.RoleId, "walkinroles_ibfk_2");

            entity.Property(e => e.WalkInRolesId).HasColumnName("WalkInRoles_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.ModifiedDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("modified_date");
            entity.Property(e => e.RoleId).HasColumnName("Role_id");
            entity.Property(e => e.WalkinId).HasColumnName("Walkin_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Walkinroles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walkinroles_ibfk_2");

            entity.HasOne(d => d.Walkin).WithMany(p => p.Walkinroles)
                .HasForeignKey(d => d.WalkinId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("walkinroles_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
