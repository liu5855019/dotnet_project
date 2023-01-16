using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.Log.Dal.Migrations
{
    public partial class Update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LogDotaRun",
                keyColumn: "GroupId",
                keyValue: null,
                column: "GroupId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "LogDotaRun",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "LogDotaRun",
                keyColumn: "DeviceId",
                keyValue: null,
                column: "DeviceId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "LogDotaRun",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LogInterface_Service_Name",
                table: "LogInterface",
                columns: new[] { "Service", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_LogDotaRun_DeviceId_GroupId_IsShop",
                table: "LogDotaRun",
                columns: new[] { "DeviceId", "GroupId", "IsShop" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogInterface_Service_Name",
                table: "LogInterface");

            migrationBuilder.DropIndex(
                name: "IX_LogDotaRun_DeviceId_GroupId_IsShop",
                table: "LogDotaRun");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "LogDotaRun",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "DeviceId",
                table: "LogDotaRun",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
