using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EventNet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Attendee");

            migrationBuilder.EnsureSchema(
                name: "Event");

            migrationBuilder.CreateTable(
                name: "Event",
                schema: "Event",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attendee",
                schema: "Attendee",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentType = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: false),
                    AttendeeType = table.Column<int>(type: "integer", nullable: false),
                    LegalName = table.Column<string>(type: "text", nullable: true),
                    RegistrationCode = table.Column<string>(type: "text", nullable: true),
                    NumberOfAttendees = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    PersonalIdCode = table.Column<string>(type: "text", nullable: true),
                    IndividualDescription = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendee_Event_EventId",
                        column: x => x.EventId,
                        principalSchema: "Event",
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Event",
                table: "Event",
                columns: new[] { "Id", "Description", "EventDate", "Location", "Name" },
                values: new object[] { 1L, "A conference for software developers to network and learn.", new DateTime(2025, 2, 7, 12, 0, 0, 0, DateTimeKind.Utc), "Convention Center, City", "Annual Developer Conference" });

            migrationBuilder.InsertData(
                schema: "Attendee",
                table: "Attendee",
                columns: new[] { "Id", "AttendeeType", "Description", "EventId", "LegalName", "NumberOfAttendees", "PaymentType", "RegistrationCode" },
                values: new object[] { 1L, 2, "Tech Corp's delegation attending the conference.", 1L, "Tech Corp Ltd", 5, 1, "TC123456" });

            migrationBuilder.InsertData(
                schema: "Attendee",
                table: "Attendee",
                columns: new[] { "Id", "AttendeeType", "IndividualDescription", "EventId", "FirstName", "LastName", "PaymentType", "PersonalIdCode" },
                values: new object[] { 2L, 1, "John Doe attending as an individual.", 1L, "John", "Doe", 0, "39506036025" });

            migrationBuilder.CreateIndex(
                name: "IX_Attendee_EventId",
                schema: "Attendee",
                table: "Attendee",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendee",
                schema: "Attendee");

            migrationBuilder.DropTable(
                name: "Event",
                schema: "Event");
        }
    }
}
