using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddStoredProcForPurchaseType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetPurchaseTypes 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.PurchaseTypes 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetPurchaseType 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.PurchaseTypes  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdatePurchaseType
	                                @Id int,
	                                @PurcahseTypeName(100),
                                    @PurcahseCode varchar(100)


                                    AS 
                                    BEGIN 
                                     UPDATE dbo.PurchaseTypes
                                     SET  PurcahseTypeName = @PurcahseTypeName, PurcahseCode = @PurcahseCode

                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeletePurchaseType
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.PurchaseTypes
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreatePurchaseType
                                   @PurcahseTypeName(100),
                                   @PurcahseCode varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.PurchaseTypes([PurcahseTypeName],[PurcahseCode])
                                    VALUES (@PurcahseTypeName,@PurcahseCode)

                                   END");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCoverTypes");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetCoverType");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateCoverType");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteCoverType");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateCoverType");

        }
    }
}
