using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DermDiag.Models;

public partial class DermDiagContext : DbContext
{
    public DermDiagContext()
    {
    }

    public DermDiagContext(DbContextOptions<DermDiagContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Consulte> Consultes { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<MedicineAdvice> MedicineAdvices { get; set; }

    public virtual DbSet<ModelInputImage> ModelInputImages { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientModelHistory> PatientModelHistories { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Tasks> Tasks { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<EmailMessage> EmailMessages { get; set; }
    public virtual DbSet<EmailAttachment> EmailAttachments { get; set; }
    public virtual DbSet<Wallet> Wallets { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<EmailAttachment>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("EmailAttachment");
        });


        modelBuilder.Entity<EmailMessage>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("EmailMessage");
        });

        modelBuilder.Entity<Tasks>(entity =>
        {
            entity.HasKey(entity => entity.Id);
            entity.ToTable("Tasks");
            entity.HasOne(entity => entity.Patient).WithMany(entity => entity.Tasks).HasForeignKey(entity => entity.PatientId);
        });

        modelBuilder.Entity<Review>(entity =>
        {

            entity.HasKey(entity =>new { entity.PatientId,entity.DoctorId });
            entity.ToTable("Reviews");
            entity.HasOne(entity => entity.Patient).WithMany(entity => entity.Reviews).HasForeignKey(entity => entity.PatientId);
            entity.HasOne(entity => entity.Doctor).WithMany(entity => entity.Reviews).HasForeignKey(entity => entity.DoctorId);


        });
        modelBuilder.Entity<Wallet>(entity =>
        {
            entity
            .HasKey(e => e.Id)
            .HasName("ID");

            entity.
            ToTable("Wallet");

            entity
            .Property(e => e.Balance)
            .HasColumnName("Balance")
            .HasColumnType("decimal(10, 2)");

        });

        modelBuilder.Entity<Book>(entity =>
        {
            
            entity
                .HasKey(b => new { b.PatientId, b.DoctorId, b.PaymentId });

            entity
                .ToTable("Book");

            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("Appointment_Date");

            entity.Property(e => e.DoctorId).HasColumnName("Doctor_ID");
            entity.Property(e => e.PatientId).HasColumnName("Patient_ID");
            entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");

            entity.HasOne(d => d.Doctor).WithMany()
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Book__Doctor_ID__4D94879B");

            entity.HasOne(d => d.Patient).WithMany()
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Book__Patient_ID__4CA06362");

            entity.HasOne(d => d.Payment).WithMany()
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Book__Payment_ID__4E88ABD4");
            
        });

        modelBuilder.Entity<Consulte>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.DoctorId , e.Date}).HasName("PK__Consulte__2FF13E69B0C57837");

            entity.ToTable("Consulte");

            entity.Property(e => e.PatientId).HasColumnName("Patient_ID");
            entity.Property(e => e.DoctorId).HasColumnName("Doctor_ID");
            entity.Property(e => e.Date).HasColumnName("Date");
            entity.Property(e => e.PatientLink).HasColumnName("PatientLink");
            entity.Property(e => e.DoctorLink).HasColumnName("DoctorLink");
            entity.Property(e => e.DoctorAttendance).HasColumnName("Doctor_Attendance");
            entity.Property(e => e.PatientAttendance).HasColumnName("Patient_Attendance");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Doctor).WithMany(p => p.Consultes)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consulte__Doctor__5629CD9C");

            entity.HasOne(d => d.Patient).WithMany(p => p.Consultes)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consulte__Patien__5535A963");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Doctor__3214EC277293A442");

            entity.ToTable("Doctor");

            entity.HasIndex(e => e.Email, "UQ__Doctor__A9D105344BCD1DAD").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("ID");
            entity.Property(e => e.AcceptanceStatus).HasColumnName("Acceptance_Status");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fees).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.NoReviews).HasColumnName("No_Reviews");
            entity.Property(e => e.NoSessions).HasColumnName("No_Sessions");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MedicineAdvice>(entity =>
        {
            entity
                .HasKey(m => new { m.DoctorId, m.PatientId , m.MedicineName });

            entity.ToTable("Medicine_Advice");

            entity.Property(e => e.DoctorId).HasColumnName("Doctor_ID");
            entity.Property(e => e.MedicineName)
                .HasMaxLength(255)
                .HasColumnName("Medicine_Name");
            entity.Property(e => e.PatientId).HasColumnName("Patient_ID");
            

            entity.HasOne(d => d.Doctor).WithMany()
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Medicine___Docto__59063A47");

            entity.HasOne(d => d.Patient).WithMany()
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Medicine___Patie__5812160E");
        });

        modelBuilder.Entity<ModelInputImage>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Model_Input_Images");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Image_URL");
            entity.Property(e => e.ModelHistoryId).HasColumnName("Model_History_ID");

            entity.HasOne(d => d.ModelHistory).WithMany()
                .HasForeignKey(d => d.ModelHistoryId)
                .HasConstraintName("FK__Model_Inp__Model__48CFD27E");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient__3214EC277AF761B0");

            entity.ToTable("Patient");

            entity.HasIndex(e => e.Email, "UQ__Patient__A9D105346DCA7C3A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("Name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.Doctors).WithMany(p => p.Patients)
                .UsingEntity<Dictionary<string, object>>(
                    "Favorite",
                    r => r.HasOne<Doctor>().WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__Doctor__52593CB8"),
                    l => l.HasOne<Patient>().WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Favorite__Patien__5165187F"),
                    j =>
                    {
                        j.HasKey("PatientId", "DoctorId").HasName("PK__Favorite__2FF13E69A503B9A2");
                        j.ToTable("Favorite");
                        j.IndexerProperty<int>("PatientId").HasColumnName("Patient_ID");
                        j.IndexerProperty<int>("DoctorId").HasColumnName("Doctor_ID");
                    });
        });

        modelBuilder.Entity<PatientModelHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Patient___3214EC275616F051");

            entity.ToTable("Patient_Model_History");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ModelResult)
                .HasColumnType("text")
                .HasColumnName("Model_Result");
            entity.Property(e => e.PatientId).HasColumnName("Patient_ID");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientModelHistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK__Patient_M__Patie__46E78A0C");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity
            .HasKey(e => e.Id)
            .HasName("PK__Payment__3214EC27357C9B09");

            entity.ToTable("Payment");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");

            entity.Property(e => e.Method).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReceiverID).HasMaxLength(255);
            entity.Property(e => e.SenderID).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
