using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class DocumentTypeIdstring2int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalEntries_SystemCodeDetails_DocumentTypeId1",
                table: "ApprovalEntries");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalEntries_DocumentTypeId1",
                table: "ApprovalEntries");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId1",
                table: "ApprovalEntries");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentTypeId",
                table: "ApprovalEntries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalEntries_DocumentTypeId",
                table: "ApprovalEntries",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalEntries_SystemCodeDetails_DocumentTypeId",
                table: "ApprovalEntries",
                column: "DocumentTypeId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalEntries_SystemCodeDetails_DocumentTypeId",
                table: "ApprovalEntries");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalEntries_DocumentTypeId",
                table: "ApprovalEntries");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentTypeId",
                table: "ApprovalEntries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId1",
                table: "ApprovalEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalEntries_DocumentTypeId1",
                table: "ApprovalEntries",
                column: "DocumentTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalEntries_SystemCodeDetails_DocumentTypeId1",
                table: "ApprovalEntries",
                column: "DocumentTypeId1",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
