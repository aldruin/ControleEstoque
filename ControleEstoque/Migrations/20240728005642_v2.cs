using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleEstoque.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Item",
                type: "datetime2",
                nullable: true,
                comment: "End date of the item")
                .Annotation("Relational:ColumnOrder", 8);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Item",
                type: "datetime2",
                nullable: true,
                comment: "Start date of the item")
                .Annotation("Relational:ColumnOrder", 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Item");
        }
    }
}
