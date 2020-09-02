using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddStoredProcForVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetVendors 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Vendors 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetVendor 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Vendors  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateVendor
	                                @Id int,
                                    @VendorName varchar(100),
                                    @VendoreCode varchar(100),
                                    @VendorAddress varchar(100),
                                    @VendorPhone varchar(100),
                                    @SalesContactName varchar(100),
                                    @SalesContactEmail varchar(100),
                                    @AccountContactName varchar(100),
                                    @AccContactEmail varchar(100),
                                    @RegVendor bit	        

                                    AS 
                                    BEGIN 
                                    UPDATE dbo.Vendors
                                    SET  VendorName = @VendorName,
                                    VendoreCode = @VendoreCode,
                                    VendorAddress = @VendorAddress,
                                    VendorPhone = @VendorPhone, 
                                    SalesContactName = @SalesContactName,
                                    SalesContactEmail = @SalesContactEmail, 
                                    AccountContactName = @AccountContactName, 
                                    AccContactEmail = @AccContactEmail,
                                    RegVendor = @RegVendor
                                    WHERE  Id = @Id
                                    END");



            migrationBuilder.Sql(@"CREATE PROC usp_DeleteVendor
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Vendors
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateVendor
                                   @VendorName varchar(100),
                                   @VendoreCode varchar(100),
                                   @VendorAddress varchar(100),
				                   @VendorPhone  varchar(100),
                                   @SalesContactName varchar(100),
                                   @SalesContactEmail varchar(100),
				   				   @AccountContactName  varchar(100),
                                   @AccContactEmail varchar(100),
                                   @RegVendor bit
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Vendors(
                                    [VendorName],
                                    [VendoreCode], 
                                    [VendorAddress],
                                    [VendorPhone],
                                    [SalesContactName], 
                                    [SalesContactEmail],
                                    [AccountContactName],
                                    [AccContactEmail],
                                    [RegVendor]
                                    )
                                    VALUES
                                    (
                                    @VendorName,
                                    @VendoreCode,
                                    @VendorAddress,
                                    @VendorPhone,
                                    @SalesContactName,
                                    @SalesContactEmail,
                                    @AccountContactName,
                                    @AccContactEmail,
                                    @RegVendor)
                                    END");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetVendors");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetVendor");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateVendor");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteVendor");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateVendor");

        }
    }
}
