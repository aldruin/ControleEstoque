using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleEstoque.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_AspNetUsers_Foreign key to User",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_Foreign key to User",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Foreign key to User",
                table: "Staff");

            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StaffId",
                table: "AspNetUsers",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Staff_StaffId",
                table: "AspNetUsers",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "StaffId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Staff_StaffId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StaffId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "Foreign key to User",
                table: "Staff",
                type: "uniqueidentifier",
                nullable: true,
                comment: "Foreign Key to User")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Foreign key to User",
                table: "Staff",
                column: "Foreign key to User",
                unique: true,
                filter: "[Foreign key to User] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_AspNetUsers_Foreign key to User",
                table: "Staff",
                column: "Foreign key to User",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
