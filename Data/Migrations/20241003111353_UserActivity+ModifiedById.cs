using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserActivityModifiedById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "RejectedOn",
                table: "LeaveApplications");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "SystemCodes",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "SystemCodeDetails",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "LeaveTypes",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "RejectedById",
                table: "LeaveApplications",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Employees",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Designations",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Departments",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Countries",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Banks",
                newName: "ModifiedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "SystemCodes",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "SystemCodeDetails",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "LeaveTypes",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "LeaveApplications",
                newName: "RejectedById");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Employees",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Designations",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Departments",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Countries",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                table: "Banks",
                newName: "ModifiedBy");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "LeaveApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedOn",
                table: "LeaveApplications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
