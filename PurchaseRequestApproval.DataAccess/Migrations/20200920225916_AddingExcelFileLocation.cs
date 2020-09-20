using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddingExcelFileLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExcelFileUrl",
                table: "PRApprovals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExcelFileUrl",
                table: "PRApprovals");
        }
    }
}
