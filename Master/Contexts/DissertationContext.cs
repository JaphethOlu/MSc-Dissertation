using System;
using Microsoft.EntityFrameworkCore;

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
        public virtual DbSet<DirectorAccount> DirectorAccounts { get; set; }
        public virtual DbSet<Organisation> Organisations { get; set; }
        public virtual DbSet<RecruiterAccount> RecruiterAccounts { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractorProfile> ContractorProfiles { get; set; }
        public virtual DbSet<Industry> Industries { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        
        // Unable to generate entity type for table 'applied_contract'. Please see the warning messages.
        // Unable to generate entity type for table 'saved_contract'. Please see the warning messages.

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
                entity.HasKey(c => c.EmailAddress);

                entity.ToTable("contractor_account");
                
                entity.HasIndex(c => c.EmailAddress)
                    .HasName("EmailAddress")
                    .IsUnique();
                
                entity.Property(c => c.EmailAddress)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(c => c.Password)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(c => c.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(c => c.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");
			});

             modelBuilder.Entity<ContractorProfile>(entity =>
            {
                entity.HasKey(p => p.EmailAddress);

                entity.ToTable("contractor_profile");

                entity.Property(p => p.EmailAddress)
                    .HasColumnType("varchar(50)");

                entity.Property(p => p.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(p => p.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(p => p.Headline)
                    .HasColumnType("varchar(120)");

                entity.Property(p => p.Location)
                    .HasColumnType("varchar(30)");

                entity.Property(p => p.PersonalStatement)
                    .HasColumnType("varchar(800)");

                entity.HasOne(p => p.ProfileAccount)
                    .WithOne()
                    .HasForeignKey<ContractorProfile>(p => p.EmailAddress)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Contractor_EmailAddress");
            });

            modelBuilder.Entity<DirectorAccount>(entity => 
            {
                entity.HasKey(d => d.EmailAddress);

                entity.ToTable("director_account");

                entity.HasIndex(d => d.EmailAddress)
                    .HasName("EmailAddress")
                    .IsUnique();

                entity.Property(d => d.Password)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(d => d.FirstName)
                    .IsRequired()
                    .HasColumnType("varvhar(30)");

                entity.Property(d => d.LastName)
                    .IsRequired()
                    .HasColumnType("varvhar(30)");
            });
            
            modelBuilder.HasSequence<int>("OrganisationID")
                .StartsAt(101100)
                .IncrementsBy(1);

            modelBuilder.Entity<Organisation>(entity =>
            {
                entity.HasKey(o => o.OrganisationId);

                entity.ToTable("organisation");
                
                entity.HasIndex(o => o.Director)
                  .HasName("FK_Organisation_Director");
                
                entity.HasIndex(o => new { o.OrganisationId, o.OrganisationName, o.Director })
                    .HasName("U_Organisation")
                    .IsUnique();

                entity.Property(o => o.OrganisationId)
                    .HasColumnName("OrganisationID")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("NEXT VALUE FOR OrganisationID");

                entity.Property(o => o.OrganisationName)
                    .IsRequired()
                    .HasColumnType("varchar(75)");

                entity.Property(o => o.OrganisationType)
                    .IsRequired()
                    .HasConversion(
                        t => t.ToString(),
                        t => (OrganisationType)Enum.Parse(typeof(OrganisationType), t));

                entity.Property(o => o.OrganisationStatement)
                    .HasColumnType("varchar(1500)");
                
                entity.Property(o => o.Director)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(o => o.Location)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(o => o.NumberOfAvailableAdverts)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("'5'");

                entity.HasOne(o => o.DirectorAccount)
                    .WithOne()
                    .HasForeignKey<Organisation>(o => o.Director)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organisation_Director");
            });

            modelBuilder.Entity<RecruiterAccount>(entity =>
            {
                entity.HasKey(r => r.EmailAddress);

                entity.ToTable("recruiter_account");

                entity.HasIndex(r => r.EmailAddress)
                    .HasName("EmailAddress")
                    .IsUnique();

                entity.HasIndex(r => r.OrganisationId)
                    .HasName("FK_Recruiter_Organisation");

                entity.Property(r => r.EmailAddress)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(r => r.Password)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                entity.Property(r => r.FirstName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(r => r.LastName)
                    .IsRequired()
                    .HasColumnType("varchar(30)");

                entity.Property(r => r.OrganisationId)
                    .HasColumnName("OrganisationID")
                    .HasColumnType("int(11)");
                
                entity.HasOne(r => r.Organisation)
                    .WithMany(o => o.Recruiters)
                    .HasForeignKey(r => r.OrganisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recruiter_Organisation");
                
            });

            modelBuilder.HasSequence<int>("ContractID")
                            .StartsAt(10000)
                            .IncrementsBy(1);

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(c => c.ContractId);

                entity.ToTable("contract");

                entity.HasIndex(c => c.ContractId)
                    .HasName("ContractID")
                    .IsUnique();

                entity.HasIndex(c => c.OrganisationId)
                    .HasName("FK_Contract_Organisation");

                entity.Property(c => c.ContractId)
                    .HasColumnName("ContractID")
                    .HasColumnType("int(11)");

                entity.Property(c => c.DateCreated)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("current_timestamp");

                entity.Property(c => c.Description)
                    .HasColumnType("varchar(2000)");

                entity.Property(c => c.Duration)
                    .HasColumnType("tinyint(4)");

                entity.Property(c => c.EndDate)
                    .HasColumnType("date");

                entity.Property(c => c.Location)
                    .HasColumnType("varchar(30)");

                entity.Property(c => c.OrganisationId)
                    .HasColumnName("OrganisationID")
                    .HasColumnType("int(11)");

                entity.Property(c => c.Position)
                    .HasColumnType("varchar(75)");

                entity.Property(c => c.StartDate)
                    .HasColumnType("date");

                entity.HasOne(c => c.Organisation)
                    .WithMany(o => o.Contracts)
                    .HasForeignKey(c => c.OrganisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_Organisation");
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
