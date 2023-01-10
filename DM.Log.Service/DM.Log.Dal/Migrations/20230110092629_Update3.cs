using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.Log.Dal.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "LogInterface");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LogInterface",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "Interface Name")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "LogInterface",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Remark")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "LogInterface",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Service Name")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "LogInterface");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LogInterface",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                comment: "Scenario Description")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "LogInterface",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "Scenario Title")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
