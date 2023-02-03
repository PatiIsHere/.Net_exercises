using Cwiczenia6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cwiczenia6.Config
{
    public class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(pre => pre.IdPrescription).HasName("Prescpription_PK");
            builder.Property(pre => pre.IdPrescription).UseIdentityColumn();
            builder.Property(pre => pre.Date).HasColumnType("Date");
            builder.Property(pre => pre.DueDate).HasColumnType("Date");

            builder.HasOne(pre => pre.Patient)
                .WithMany(pat => pat.Prescriptions)
                .HasForeignKey(pre => pre.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdPatient_Prescription");

            builder.HasOne(pre => pre.Doctor)
                .WithMany(doc => doc.Prescriptions)
                .HasForeignKey(pre => pre.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("IdDoctor_Prescription");

            builder.HasData(
                new Prescription
                {
                    IdPrescription = 1,
                    Date = DateTime.Parse("2022-01-01")
                ,
                    DueDate = DateTime.Parse("2022-02-01"),
                    IdPatient = 1,
                    IdDoctor = 1
                },
                new Prescription
                {
                    IdPrescription = 2,
                    Date = DateTime.Parse("2022-02-01")
                ,
                    DueDate = DateTime.Parse("2022-03-01"),
                    IdPatient = 2,
                    IdDoctor = 2
                },
                new Prescription
                {
                    IdPrescription = 3,
                    Date = DateTime.Parse("2022-03-01")
                ,
                    DueDate = DateTime.Parse("2022-04-01"),
                    IdPatient = 3,
                    IdDoctor = 3
                }
                );
        }
    }
}
