using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addMoreRoleMaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoleMaster",
                columns: new[] { "RoleId", "RoleCode", "RoleName" },
                values: new object[] { 3, "SUPERUSER", "SuperUser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleMaster",
                keyColumn: "RoleId",
                keyValue: 3);
        }
    }
}
