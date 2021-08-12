using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeymenCodeCase.Context.Migrations
{
    public partial class CraeteDateUpdateDateAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ScheduleTask",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "ScheduleTask",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Configuration",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Configuration",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ScheduleTask");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "ScheduleTask");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Configuration");
        }
    }
}
