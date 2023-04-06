using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.Log.Dal.Migrations
{
    public partial class Update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogInterface_Service_Name",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "LogInterface");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "LogInterface",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true,
                comment: "Remark",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Remark")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDt",
                table: "LogInterface",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestHeader",
                table: "LogInterface",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RequestPara",
                table: "LogInterface",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RequestPath",
                table: "LogInterface",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "LogInterface",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDt",
                table: "LogInterface",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponseHeader",
                table: "LogInterface",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LogInterface_Service_RequestPath",
                table: "LogInterface",
                columns: new[] { "Service", "RequestPath" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LogInterface_Service_RequestPath",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "RequestDt",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "RequestHeader",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "RequestPara",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "RequestPath",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "ResponseDt",
                table: "LogInterface");

            migrationBuilder.DropColumn(
                name: "ResponseHeader",
                table: "LogInterface");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                table: "LogInterface",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Remark",
                oldClrType: typeof(string),
                oldType: "varchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true,
                oldComment: "Remark")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
                name: "Value",
                table: "LogInterface",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Para")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LogInterface_Service_Name",
                table: "LogInterface",
                columns: new[] { "Service", "Name" });
        }
    }
}
