using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddVendorContactProjectQuotePRApprovalToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(nullable: false),
                    WorkPackageIn = table.Column<string>(nullable: false),
                    ContractNo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorContacts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Branch = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorContacts_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRApprovalId = table.Column<int>(nullable: false),
                    PRApprovalTitle = table.Column<string>(nullable: true),
                    PRApprovalDescription = table.Column<string>(nullable: true),
                    WorkOrder = table.Column<string>(nullable: true),
                    CMOA = table.Column<bool>(nullable: false),
                    MatCorVer = table.Column<bool>(nullable: false),
                    Warranty = table.Column<bool>(nullable: false),
                    WorkDurationSiteDays = table.Column<int>(nullable: false),
                    RateSheet = table.Column<bool>(nullable: false),
                    GateAccess = table.Column<bool>(nullable: false),
                    RentalPeriodDays = table.Column<int>(nullable: false),
                    EquipmentTireEngine = table.Column<bool>(nullable: false),
                    JustificationVendor = table.Column<string>(nullable: true),
                    VendorId = table.Column<int>(nullable: false),
                    PurchaseTypeId = table.Column<int>(nullable: false),
                    SourcedBy = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRApprovals_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRApprovals_PurchaseTypes_PurchaseTypeId",
                        column: x => x.PurchaseTypeId,
                        principalTable: "PurchaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRApprovals_Employees_SourcedBy",
                        column: x => x.SourcedBy,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRApprovals_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteDescription = table.Column<string>(nullable: false),
                    QuoteAmount = table.Column<double>(nullable: false),
                    SiteCompliant = table.Column<bool>(nullable: false),
                    Less3K = table.Column<bool>(nullable: false),
                    SoleProvider = table.Column<bool>(nullable: false),
                    OEM = table.Column<bool>(nullable: false),
                    ScheduleDrivenPur = table.Column<bool>(nullable: false),
                    Commonality = table.Column<bool>(nullable: false),
                    FirstNation = table.Column<bool>(nullable: false),
                    Metis = table.Column<bool>(nullable: false),
                    LocalVendor = table.Column<bool>(nullable: false),
                    ETADays = table.Column<int>(nullable: false),
                    QuoteDate = table.Column<DateTime>(nullable: false),
                    PdfUrl = table.Column<string>(nullable: true),
                    VendorId = table.Column<int>(nullable: false),
                    VendorContactId = table.Column<int>(nullable: false),
                    ShippingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quotes_Shippings_ShippingId",
                        column: x => x.ShippingId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Quotes_VendorContacts_VendorContactId",
                        column: x => x.VendorContactId,
                        principalTable: "VendorContacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quotes_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRAQuotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRAQuoteDate = table.Column<DateTime>(nullable: false),
                    PRARevision = table.Column<int>(nullable: false),
                    JustificationRev = table.Column<string>(nullable: true),
                    EstimatedPrice = table.Column<double>(nullable: false),
                    AddWarCost = table.Column<double>(nullable: false),
                    FreightCost = table.Column<double>(nullable: false),
                    EnvFees = table.Column<double>(nullable: false),
                    CarbonTax = table.Column<double>(nullable: false),
                    PSTCost = table.Column<double>(nullable: false),
                    Mobilization = table.Column<double>(nullable: false),
                    SiteOrientation = table.Column<double>(nullable: false),
                    RentalInsurance = table.Column<double>(nullable: false),
                    EquipmentDisinfection = table.Column<double>(nullable: false),
                    ContingencyPercentage = table.Column<double>(nullable: false),
                    ContingencyAmount = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    PRAId = table.Column<int>(nullable: false),
                    SubmittedBy = table.Column<int>(nullable: false),
                    QuoteId = table.Column<int>(nullable: false),
                    QuoteAl1Id = table.Column<int>(nullable: false),
                    QuoteAl2Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRAQuotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRAQuotes_PRApprovals_PRAId",
                        column: x => x.PRAId,
                        principalTable: "PRApprovals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRAQuotes_Quotes_QuoteAl1Id",
                        column: x => x.QuoteAl1Id,
                        principalTable: "Quotes",
                        principalColumn: "Id" );
                    table.ForeignKey(
                        name: "FK_PRAQuotes_Quotes_QuoteAl2Id",
                        column: x => x.QuoteAl2Id,
                        principalTable: "Quotes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PRAQuotes_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PRAQuotes_Employees_SubmittedBy",
                        column: x => x.SubmittedBy,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRApprovals_ProjectId",
                table: "PRApprovals",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PRApprovals_PurchaseTypeId",
                table: "PRApprovals",
                column: "PurchaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PRApprovals_SourcedBy",
                table: "PRApprovals",
                column: "SourcedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PRApprovals_VendorId",
                table: "PRApprovals",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PRAQuotes_PRAId",
                table: "PRAQuotes",
                column: "PRAId");

            migrationBuilder.CreateIndex(
                name: "IX_PRAQuotes_QuoteAl1Id",
                table: "PRAQuotes",
                column: "QuoteAl1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PRAQuotes_QuoteAl2Id",
                table: "PRAQuotes",
                column: "QuoteAl2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PRAQuotes_QuoteId",
                table: "PRAQuotes",
                column: "QuoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PRAQuotes_SubmittedBy",
                table: "PRAQuotes",
                column: "SubmittedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_ShippingId",
                table: "Quotes",
                column: "ShippingId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_VendorContactId",
                table: "Quotes",
                column: "VendorContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_VendorId",
                table: "Quotes",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorContacts_VendorId",
                table: "VendorContacts",
                column: "VendorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRAQuotes");

            migrationBuilder.DropTable(
                name: "PRApprovals");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "VendorContacts");
        }
    }
}
