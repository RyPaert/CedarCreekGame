using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CedarCreek.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migrat1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CharacterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CharacterXP = table.Column<int>(type: "int", nullable: false),
                    CharacterXPNextLevel = table.Column<int>(type: "int", nullable: false),
                    CharacterLevel = table.Column<int>(type: "int", nullable: false),
                    CharacterMaxHealth = table.Column<int>(type: "int", nullable: false),
                    CharacterHealth = table.Column<int>(type: "int", nullable: false),
                    CharacterClass = table.Column<int>(type: "int", nullable: false),
                    PrimaryAttackPower = table.Column<int>(type: "int", nullable: false),
                    PrimaryAttackName = table.Column<int>(type: "int", nullable: false),
                    SpecialAttackPower = table.Column<int>(type: "int", nullable: false),
                    SpecialAttackName = table.Column<int>(type: "int", nullable: false),
                    CharacterCreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CharacterStatus = table.Column<int>(type: "int", nullable: false),
                    CharacterRank = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Characters");
        }
    }
}
