using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Master.Models;

namespace Master.Contexts
{
    public partial class DissertationContext : DbContext
    {
        public DissertationContext()
        {

        }

        public DissertationContext(DbContextOptions<DissertationContext> options) : base(options)
        {

        }

        public virtual DbSet<ContractorAccount> ContractorAccounts { get; set; }
        public virtual DbSet<RecruiterAccount> RecruiterAccounts { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractorProfile> ContractorProfiles { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        

        // Unable to generate entity type for table 'applied_contract'. Please see the warning messages.
        // Unable to generate entity type for table 'education'. Please see the warning messages.
        // Unable to generate entity type for table 'saved_contract'. Please see the warning messages.
        // Unable to generate entity type for table 'work_experience'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
				// Console.WriteLine("Configure the Database");
                optionsBuilder.UseMySql("Server=localhost;Database=dissertation;User=root;Password=M4st3rD1ss0;");
			}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractorAccount>(entity =>
            {
                entity.HasKey(ca => ca.EmailAddress);

                entity.ToTable("contractor_account");
                
                entity.HasIndex(ca => ca.EmailAddress)
                    .HasName("EmailAddress")
                    .IsUnique();
                
                entity.Property(ca => ca.EmailAddress)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(ca => ca.Password)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(ca => ca.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(ca => ca.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");
			});
            
            modelBuilder.Entity<RecruiterAccount>(entity =>
            {
                entity.HasKey(ra => ra.EmailAddress);

                entity.ToTable("recruiter_account");

                entity.HasIndex(ra => ra.EmailAddress)
                    .HasName("EmailAddress")
                    .IsUnique();

                entity.HasIndex(ra => ra.OrganisationId)
                    .HasName("FK_Recruiter_Organisation");

                entity.Property(ra => ra.EmailAddress)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(ca => ca.Password)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(ra => ra.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(ra => ra.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(ra => ra.OrganisationId)
                    .HasColumnName("OrganisationID")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Organisation)
                    .WithMany(p => p.Recruiters)
                    .HasForeignKey(d => d.OrganisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recruiter_Organisation");
            });

            modelBuilder.Entity<Organisation>(entity =>
            {
                entity.HasKey(e => e.OrganisationId);

                entity.ToTable("organisation");

                entity.HasIndex(e => e.Director)
                    .HasName("FK_Organisation_Director");

                entity.HasIndex(e => new { e.OrganisationId, e.OrganisationName, e.Director })
                    .HasName("U_Organisation")
                    .IsUnique();

                entity.Property(e => e.OrganisationId)
                    .HasColumnName("OrganisationID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.NumberOfAvailableAdverts)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'5'");

                entity.Property(e => e.OrganisationName)
                    .IsRequired()
                    .HasColumnType("varchar(75)");

                entity.Property(e => e.PersonalStatement).HasColumnType("varchar(1500)");

                entity.HasOne(d => d.DirectorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Director)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organisation_Director");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.ContractId);

                entity.ToTable("contract");

                entity.HasIndex(e => e.ContractId)
                    .HasName("ContractID")
                    .IsUnique();

                entity.HasIndex(e => e.OrganisationId)
                    .HasName("FK_Contract_Organisation");

                entity.Property(e => e.ContractId)
                    .HasColumnName("ContractID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateCreated).HasColumnType("date");

                entity.Property(e => e.Description).HasColumnType("varchar(2000)");

                entity.Property(e => e.Duration).HasColumnType("tinyint(4)");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Location).HasColumnType("varchar(30)");

                entity.Property(e => e.OrganisationId)
                    .HasColumnName("OrganisationID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Position).HasColumnType("varchar(30)");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Organisation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.OrganisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Organisation");
            });

            modelBuilder.Entity<ContractorProfile>(entity =>
            {
                entity.HasKey(cp => cp.EmailAddress);

                entity.ToTable("contractor_profile");

                entity.Property(cp => cp.EmailAddress).HasColumnType("varchar(50)");

                entity.Property(cp => cp.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(cp => cp.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(cp => cp.Headline)
                    .HasColumnType("varchar(120)");

                entity.Property(cp => cp.Location)
                    .HasColumnType("varchar(30)");

                entity.Property(cp => cp.PersonalStatement)
                    .HasColumnType("varchar(800)");

                entity.HasOne(d => d.EmailAddressNavigation)
                    .WithOne()
                    .HasForeignKey<ContractorProfile>(d => d.EmailAddress)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contractor_EmailAddress");
            });

            modelBuilder.Entity<Industry>(entity =>
            {
                entity.HasKey(e => e._Industry);

                entity.ToTable("industries");

                entity.HasIndex(e => e._Industry)
                    .HasName("Industry")
                    .IsUnique();

                entity.Property(e => e._Industry).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e._Language);

                entity.ToTable("languages");

                entity.HasIndex(e => e._Language)
                    .HasName("Language")
                    .IsUnique();

                entity.Property(e => e._Language).HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(e => e._Skill);

                entity.ToTable("skills");

                entity.HasIndex(e => e._Skill)
                    .HasName("Skill")
                    .IsUnique();

                entity.Property(e => e._Skill).HasColumnType("varchar(50)");
            });       
        }
    }
}
