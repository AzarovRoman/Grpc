using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grpc.Dal.Migrations
{
    public partial class ChangeSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "NotPublic");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Students",
                newSchema: "NotPublic");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Students",
                schema: "NotPublic",
                newName: "Students");
        }
    }
}
