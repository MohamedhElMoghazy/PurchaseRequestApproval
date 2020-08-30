using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class addVendorEmployeeToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(maxLength: 50, nullable: false),
                    EmployeeCode = table.Column<string>(nullable: true),
                    EmployeePosition = table.Column<string>(nullable: true),
                    EmployeePhone = table.Column<string>(nullable: false),
                    EmployeeEmail = table.Column<string>(nullable: false),
                    EmployeeSite = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(maxLength: 50, nullable: false),
                    VendoreCode = table.Column<string>(nullable: true),
                    VendorAddress = table.Column<string>(nullable: true),
                    VendorPhone = table.Column<string>(nullable: false),
                    SalesContactName = table.Column<string>(nullable: false),
                    SalesContactEmail = table.Column<string>(nullable: true),
                    AccountContactName = table.Column<string>(nullable: true),
                    AccContactEmail = table.Column<string>(nullable: true),
                    RegVendor = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
