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
	                                @Name varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Vendors
                                     SET  Name = @Name
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
                                   @Name varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Vendors(Name)
                                    VALUES (@Name)
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
