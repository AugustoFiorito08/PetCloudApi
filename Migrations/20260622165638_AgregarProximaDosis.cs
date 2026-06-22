using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetCloudApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarProximaDosis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaProximaDosis",
                table: "Vacunas",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaProximaDosis",
                table: "Vacunas");
        }
    }
}
