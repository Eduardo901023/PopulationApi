using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Insurify.Population.DataAccess.Implementation.Migrations
{
    public partial class TurningValueFieldLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AlterColumn<long>(
                name: "Value",
                table: "CountryPopulations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "CountryPopulations",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
