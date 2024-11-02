using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedEmployeeTableEmplymentTerms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_EmplymentTermsId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmplymentTermsId",
                table: "Employees",
                newName: "EmploymentTermsId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmplymentTermsId",
                table: "Employees",
                newName: "IX_Employees_EmploymentTermsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_EmploymentTermsId",
                table: "Employees",
                column: "EmploymentTermsId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SystemCodeDetails_EmploymentTermsId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EmploymentTermsId",
                table: "Employees",
                newName: "EmplymentTermsId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_EmploymentTermsId",
                table: "Employees",
                newName: "IX_Employees_EmplymentTermsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SystemCodeDetails_EmplymentTermsId",
                table: "Employees",
                column: "EmplymentTermsId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
