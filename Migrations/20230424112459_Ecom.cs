using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUD.EF.Migrations
{
    /// <inheritdoc />
    public partial class Ecom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "ProductsTable");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ProductsTable");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "ProductsTable");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "ProductsTable",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "ProductsTable",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "Designation",
                table: "ProductsTable",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "ProductsTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "ProductsTable",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "ProductsTable");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductsTable");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "ProductsTable",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ProductsTable",
                newName: "Designation");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "ProductsTable",
                newName: "Gender");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "ProductsTable",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ProductsTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "ProductsTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_ProductsTable_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "ProductsTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_EmployeeId",
                table: "Skills",
                column: "EmployeeId");
        }
    }
}
