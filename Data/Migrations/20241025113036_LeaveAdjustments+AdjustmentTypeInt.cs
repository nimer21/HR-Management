using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class LeaveAdjustmentsAdjustmentTypeInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAdjustmentEntries_SystemCodeDetails_AdjustmentTypeId1",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAdjustmentEntries_AdjustmentTypeId1",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropColumn(
                name: "AdjustmentTypeId1",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.AlterColumn<int>(
                name: "AdjustmentTypeId",
                table: "LeaveAdjustmentEntries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_AdjustmentTypeId",
                table: "LeaveAdjustmentEntries",
                column: "AdjustmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAdjustmentEntries_SystemCodeDetails_AdjustmentTypeId",
                table: "LeaveAdjustmentEntries",
                column: "AdjustmentTypeId",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveAdjustmentEntries_SystemCodeDetails_AdjustmentTypeId",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.DropIndex(
                name: "IX_LeaveAdjustmentEntries_AdjustmentTypeId",
                table: "LeaveAdjustmentEntries");

            migrationBuilder.AlterColumn<string>(
                name: "AdjustmentTypeId",
                table: "LeaveAdjustmentEntries",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AdjustmentTypeId1",
                table: "LeaveAdjustmentEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_AdjustmentTypeId1",
                table: "LeaveAdjustmentEntries",
                column: "AdjustmentTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveAdjustmentEntries_SystemCodeDetails_AdjustmentTypeId1",
                table: "LeaveAdjustmentEntries",
                column: "AdjustmentTypeId1",
                principalTable: "SystemCodeDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
