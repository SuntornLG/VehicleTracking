using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addIndexToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PositionTransactions_DiviseId",
                table: "PositionTransactions");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_PositionTransactions_DiviseId_TransactionDate",
                table: "PositionTransactions",
                columns: new[] { "DiviseId", "TransactionDate" })
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PositionTransactions_DiviseId_TransactionDate",
                table: "PositionTransactions");

            migrationBuilder.CreateIndex(
                name: "IX_PositionTransactions_DiviseId",
                table: "PositionTransactions",
                column: "DiviseId");
        }
    }
}
