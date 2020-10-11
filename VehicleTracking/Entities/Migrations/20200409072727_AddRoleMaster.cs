using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class AddRoleMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleMaster",
                columns: new[] { "RoleId", "RoleCode", "RoleName" },
                values: new object[] { 1, "ADMIN", "Administrator" });

            migrationBuilder.InsertData(
                table: "RoleMaster",
                columns: new[] { "RoleId", "RoleCode", "RoleName" },
                values: new object[] { 2, "USER", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleMaster",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoleMaster",
                keyColumn: "RoleId",
                keyValue: 2);
        }
    }
}
