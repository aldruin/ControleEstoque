using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleEstoque.Infrastructure.Mapping;

public sealed class StaffMapping : IEntityTypeConfiguration<Staff>
{
    public void Configure(EntityTypeBuilder<Staff> builder)
    {
        builder.ToTable("Staff");
        builder.HasKey(s => s.Id)
            .HasName("PK_Staff");
        builder.Property(s => s.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("StaffId")
            .HasColumnOrder(1)
            .HasComment("Primary Key Staff");
        builder.Property(s => s.Name)
            .HasColumnName("Name")
            .HasColumnOrder(2)
            .HasComment("Name of Staff Leader");
        builder.Property(s => s.Role)
            .HasColumnName("Role")
            .HasColumnOrder(3)
            .HasComment("Staff Role Type");
    }
}