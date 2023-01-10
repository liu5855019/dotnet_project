using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.Log.Dal.Migrations
{
    public partial class Update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "LogInterface",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Para")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "LogInterface");
        }
    }
}
