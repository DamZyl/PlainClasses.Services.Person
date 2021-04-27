using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlainClasses.Services.Person.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MilitaryRanks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Acronym = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonalNumber = table.Column<string>(nullable: true),
                    MilitaryRankId = table.Column<Guid>(nullable: false),
                    PlatoonId = table.Column<Guid>(nullable: true),
                    MilitaryRankAcr = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    WorkPhoneNumber = table.Column<string>(nullable: true),
                    PersonalPhoneNumber = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_MilitaryRanks_MilitaryRankId",
                        column: x => x.MilitaryRankId,
                        principalTable: "MilitaryRanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAuths",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    AuthName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAuths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAuths_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonAuths_PersonId",
                table: "PersonAuths",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_MilitaryRankId",
                table: "Persons",
                column: "MilitaryRankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonAuths");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "MilitaryRanks");
        }
    }
}
