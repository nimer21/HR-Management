using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR_Management.Data.Migrations
{
    /// <inheritdoc />
    public partial class LeaveAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveAdjustmentEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeavePeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    NoOfDays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeaveAdjustmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaveEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdjustmentTypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdjustmentTypeId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAdjustmentEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveAdjustmentEntries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveAdjustmentEntries_SystemCodeDetails_AdjustmentTypeId1",
                        column: x => x.AdjustmentTypeId1,
                        principalTable: "SystemCodeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_AdjustmentTypeId1",
                table: "LeaveAdjustmentEntries",
                column: "AdjustmentTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAdjustmentEntries_EmployeeId",
                table: "LeaveAdjustmentEntries",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveAdjustmentEntries");
        }
    }
}
