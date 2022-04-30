using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRegistration.Infrastructure.Migrations
{
    public partial class UPDATEDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "modifiedDate",
                table: "FileDetails",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modifiedBy",
                table: "FileDetails",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "FileDetails",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deletedDate",
                table: "FileDetails",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "deletedBy",
                table: "FileDetails",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "FileDetails",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "createDate",
                table: "FileDetails",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "modifiedDate",
                table: "Customer",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "modifiedBy",
                table: "Customer",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Customer",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "deletedDate",
                table: "Customer",
                newName: "DeletedDate");

            migrationBuilder.RenameColumn(
                name: "deletedBy",
                table: "Customer",
                newName: "DeletedBy");

            migrationBuilder.RenameColumn(
                name: "createdBy",
                table: "Customer",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "createDate",
                table: "Customer",
                newName: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "FileDetails",
                newName: "modifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "FileDetails",
                newName: "modifiedBy");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "FileDetails",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "FileDetails",
                newName: "deletedDate");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "FileDetails",
                newName: "deletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "FileDetails",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "FileDetails",
                newName: "createDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Customer",
                newName: "modifiedDate");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Customer",
                newName: "modifiedBy");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Customer",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Customer",
                newName: "deletedDate");

            migrationBuilder.RenameColumn(
                name: "DeletedBy",
                table: "Customer",
                newName: "deletedBy");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Customer",
                newName: "createdBy");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Customer",
                newName: "createDate");
        }
    }
}
