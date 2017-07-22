using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodoSchool.Data.Migrations
{
    public partial class RemoveIdAndIsCorrectFromStudentProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "StudentProgress");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentProgress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "StudentProgress",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StudentProgress",
                nullable: false,
                defaultValue: 0);
        }
    }
}
