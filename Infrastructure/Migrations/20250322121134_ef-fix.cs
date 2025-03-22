using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class effix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Products_ProductId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Employees_EmployeeId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "isPayed",
                table: "Bills",
                newName: "IsPayed");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Bills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillNumber",
                table: "Bills",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "BillItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BillId",
                table: "BillItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Products_ProductId",
                table: "BillItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Employees_EmployeeId",
                table: "Bills",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BillItems_Products_ProductId",
                table: "BillItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Employees_EmployeeId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillNumber",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "IsPayed",
                table: "Bills",
                newName: "isPayed");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Bills",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "BillItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "BillId",
                table: "BillItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Bills_BillId",
                table: "BillItems",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BillItems_Products_ProductId",
                table: "BillItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Employees_EmployeeId",
                table: "Bills",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
