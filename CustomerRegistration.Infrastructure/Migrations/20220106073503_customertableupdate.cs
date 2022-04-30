using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRegistration.Infrastructure.Migrations
{
    public partial class customertableupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createDate",
                table: "Customer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "createdBy",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "deletedBy",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletedDate",
                table: "Customer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Customer",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "modifiedBy",
                table: "Customer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modifiedDate",
                table: "Customer",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "deletedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "deletedDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "modifiedBy",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "modifiedDate",
                table: "Customer");
        }
    }
}
