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
	                                @Name varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Shippings
                                     SET  Name = @Name
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
                                   @Name varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Shippings(Name)
                                    VALUES (@Name)
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
