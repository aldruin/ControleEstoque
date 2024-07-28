using ControleEstoque.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleEstoque.Infrastructure.Mapping;

public sealed class ItemMapping : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Item");
        builder.HasKey(i => i.Id)
            .HasName("PK_Item");
        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("ItemId")
            .HasColumnOrder(1)
            .HasComment("Primary Key Item");
        builder.Property(i => i.Name)
            .HasColumnName("Name")
            .HasColumnOrder(2)
            .HasComment("Name of Item");
        builder.Property(i => i.Quantity)
            .HasColumnName("Quantity of Items")
            .HasColumnOrder(3)
            .HasComment("Quantity of Items");
        builder.Property(i => i.Class)
            .HasColumnName("Class of Item")
            .HasColumnOrder(4)
            .HasComment("Type Class of Item");
        builder.Property(i => i.Description)
            .HasColumnName("Description of Item")
            .HasColumnOrder(5)
            .HasComment("Description of item");
        builder.Property(i => i.StaffId)
            .HasColumnName("StaffId")
            .HasColumnOrder(6)
            .HasComment("Foreign Key to Staff");

        builder.Property(i => i.Start)
            .HasColumnName("Start")
            .HasColumnOrder(7)
            .HasComment("Start date of the item");

        builder.Property(i => i.End)
            .HasColumnName("End")
            .HasColumnOrder(8)
            .HasComment("End date of the item");

        builder.HasOne(i => i.Staff)
            .WithMany(s => s.Items)
            .HasForeignKey(i => i.StaffId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}