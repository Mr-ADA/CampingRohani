using Microsoft.EntityFrameworkCore;
using CampingRohani.Model;

namespace CampingRohani.Data
{
    public class CampingRohaniContext : DbContext
    {
        public DbSet<Camp> Camps { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<RegistrationContactPerson> RegistrationContactPersons { get; set; }

        public CampingRohaniContext(DbContextOptions<CampingRohaniContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camp>().ToTable("Camps");
            modelBuilder.Entity<ContactPerson>().ToTable("ContactPersons");
            modelBuilder.Entity<Participant>().ToTable("Participants");
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<Region>().ToTable("Regions");
            modelBuilder.Entity<Registration>().ToTable("Registrations");
            modelBuilder.Entity<RegistrationContactPerson>().ToTable("RegistrationContactPersons");

            // ===== CAMP =====
            modelBuilder.Entity<Camp>()
                .Property(c => c.CreatedTime);
            modelBuilder.Entity<Camp>()
                .Property(c => c.UpdatedTime);

            modelBuilder.Entity<Region>()
                .Property(r => r.RegionId)
                .ValueGeneratedNever();

            // One-to-many: Camp -> Regions
            modelBuilder.Entity<Camp>()
                .HasMany(c => c.Regions)
                .WithOne(r => r.Camp)
                .HasForeignKey(r => r.CampId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-many: Camp -> Registrations
            modelBuilder.Entity<Camp>()
                .HasMany(c => c.Registrations)
                .WithOne(r => r.Camp)
                .HasForeignKey(r => r.CampId)
                .OnDelete(DeleteBehavior.NoAction);

            // ===== REGION =====
            modelBuilder.Entity<Region>()
                .Property(r => r.CreatedTime);
            modelBuilder.Entity<Region>()
                .Property(r => r.UpdatedTime);

            // One-to-many: Region -> ContactPersons
            modelBuilder.Entity<Region>()
                .HasMany(r => r.ContactPersons)
                .WithOne(cp => cp.Region)
                .HasForeignKey(cp => cp.RegionId)
                .OnDelete(DeleteBehavior.NoAction);

            // ===== CONTACT PERSON =====
            modelBuilder.Entity<ContactPerson>()
                .Property(cp => cp.CreatedTime);
            modelBuilder.Entity<ContactPerson>()
                .Property(cp => cp.UpdatedTime);

            // One-to-many: ContactPerson -> Registrations
            modelBuilder.Entity<ContactPerson>()
                .HasMany(cp => cp.AssignedRegistrations)
                .WithOne(r => r.ContactPerson)
                .HasForeignKey(r => r.ContactPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            // One-to-many: ContactPerson -> Payments
            modelBuilder.Entity<ContactPerson>()
                .HasMany(cp => cp.Payments)
                .WithOne(p => p.ContactPerson)
                .HasForeignKey(p => p.ContactPersonId)
                .OnDelete(DeleteBehavior.NoAction);

            // ===== PARTICIPANT =====
            modelBuilder.Entity<Participant>()
                .Property(p => p.CreatedTime);
            modelBuilder.Entity<Participant>()
                .Property(p => p.UpdatedTime);

            // One-to-one: Participant -> Registration
            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Registration)
                .WithOne(r => r.Participant)
                .HasForeignKey<Registration>(r => r.ParticipantId)
                .OnDelete(DeleteBehavior.NoAction);

            // ===== REGISTRATION =====
            modelBuilder.Entity<Registration>()
                .Property(r => r.CreatedTime);
            modelBuilder.Entity<Registration>()
                .Property(r => r.UpdatedTime);

            // One-to-one: Registration -> Payment
            modelBuilder.Entity<Registration>()
                .HasOne(r => r.Payment)
                .WithOne(p => p.Registration)
                .HasForeignKey<Payment>(p => p.RegistrationId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Registration>()
            .HasMany(cpr => cpr.ContactPersonAssignments)
            .WithOne(cp => cp.Registration)
            .HasForeignKey(p => p.RegistrationId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

            // ===== PAYMENT =====
            modelBuilder.Entity<Payment>()
                .Property(p => p.CreatedTime);
            modelBuilder.Entity<Payment>()
                .Property(p => p.UpdatedTime);
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // ===== REGISTRATION_CONTACT_PERSON (Junction Table) =====
            modelBuilder.Entity<RegistrationContactPerson>()
                .ToTable("RegistrationContactPersons");

            modelBuilder.Entity<RegistrationContactPerson>()
                .HasOne(rcp => rcp.Registration)
                .WithMany(r => r.ContactPersonAssignments)
                .HasForeignKey(rcp => rcp.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RegistrationContactPerson>()
                .HasOne(rcp => rcp.ContactPerson)
                .WithMany(cp => cp.AssignedRegistrations)
                .HasForeignKey(rcp => rcp.ContactPersonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure composite key uniqueness (one registration + one contact person)
            modelBuilder.Entity<RegistrationContactPerson>()
                .HasIndex(rcp => new { rcp.RegistrationId, rcp.ContactPersonId })
                .IsUnique();

            // Only one primary contact per registration
            modelBuilder.Entity<RegistrationContactPerson>()
                .HasIndex(rcp => new { rcp.RegistrationId, rcp.IsPrimary })
                .IsUnique()
                .HasFilter("[IsPrimary] = 1");

            base.OnModelCreating(modelBuilder);
        }
    }
}

