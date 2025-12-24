using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeCapsule.Migrations
{
    /// <inheritdoc />
    public partial class AddOverriderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OverriderId",
                table: "TimeCapsules",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverriderId",
                table: "TimeCapsules");
        }
    }
}
