using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoleProfilestringRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleProfiles_AspNetRoles_RoleId1",
                table: "RoleProfiles");

            migrationBuilder.DropIndex(
                name: "IX_RoleProfiles_RoleId1",
                table: "RoleProfiles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "RoleProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "RoleProfiles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RoleProfiles_RoleId",
                table: "RoleProfiles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleProfiles_AspNetRoles_RoleId",
                table: "RoleProfiles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleProfiles_AspNetRoles_RoleId",
                table: "RoleProfiles");

            migrationBuilder.DropIndex(
                name: "IX_RoleProfiles_RoleId",
                table: "RoleProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "RoleProfiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "RoleProfiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleProfiles_RoleId1",
                table: "RoleProfiles",
                column: "RoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleProfiles_AspNetRoles_RoleId1",
                table: "RoleProfiles",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
