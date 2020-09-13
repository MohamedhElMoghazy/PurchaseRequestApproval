using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddUserPropertiesLinkEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl1Id",
                table: "PRAQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl2Id",
                table: "PRAQuotes");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteAl2Id",
                table: "PRAQuotes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteAl1Id",
                table: "PRAQuotes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "AccessLevel",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeUser",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeUser",
                table: "AspNetUsers",
                column: "EmployeeUser");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeUser",
                table: "AspNetUsers",
                column: "EmployeeUser",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl1Id",
                table: "PRAQuotes",
                column: "QuoteAl1Id",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl2Id",
                table: "PRAQuotes",
                column: "QuoteAl2Id",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Employees_EmployeeUser",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl1Id",
                table: "PRAQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl2Id",
                table: "PRAQuotes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmployeeUser",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AccessLevel",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmployeeUser",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "QuoteAl2Id",
                table: "PRAQuotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuoteAl1Id",
                table: "PRAQuotes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl1Id",
                table: "PRAQuotes",
                column: "QuoteAl1Id",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PRAQuotes_Quotes_QuoteAl2Id",
                table: "PRAQuotes",
                column: "QuoteAl2Id",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
