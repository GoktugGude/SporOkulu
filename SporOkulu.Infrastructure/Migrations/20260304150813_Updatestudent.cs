using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SporOkulu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatestudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Students");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "25c91043-a6cf-4d1d-9b9e-44598130efac", "AQAAAAIAAYagAAAAEAbPIajNseWYKh2iKx0yvnhl6gVhsVCrbycNSeGLNOScC6w6PlMUEDt1OsfSqKaAaA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6d92c18b-ad50-4cbb-a935-81672274f3df");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "44deaa09-7de8-451a-96bb-d40febe35e2e", "AQAAAAIAAYagAAAAENPpC4oRsctgszQ7e4MgIv+CyemwE3J0cCh7QNTACB0w14UJLPJ6aJde5jF7BKSKIw==" });
        }
    }
}
