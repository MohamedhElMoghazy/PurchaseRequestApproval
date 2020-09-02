using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddStoredProcForShipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetShippings 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Shippings 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetShipping 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Shippings  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateShipping
	                                @Id int,
	                                @ShippingName varchar(100),
                                    @ShippingDescription varchar(100),
                                    @ShippingACC varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Shippings
                                     SET  ShippingName = @ShippingName, ShippingDescription = @ShippingDescription, ShippingACC = @ShippingACC
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteShipping
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Shippings
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateShipping
                                   @ShippingName varchar(100),
                                   @ShippingDescription varchar(100),
                                   @ShippingACC varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Shippings(
                                    [ShippingName]
                                    ,[ShippingDescription]
                                    , [ShippingACC]
                                    )
                                    VALUES (@ShippingName,@ShippingDescription,@ShippingACC)
                                  
                                   END");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetShippings");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetShipping");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateShipping");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteShipping");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateShipping");

        }
    }
}
