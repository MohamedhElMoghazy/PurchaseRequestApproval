using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class ConnectToPurchaseTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurcahseTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    PurcahseCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseTypes");
        }
    }
}
