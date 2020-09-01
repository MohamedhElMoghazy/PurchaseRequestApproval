using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddStoredProcForEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROC usp_GetEmployees 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Employees 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetEmployee 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Employees  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateEmployee
	                                @Id int,
	                                @Name varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Employees
                                     SET  Name = @Name
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteEmployee
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Employees
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateEmployee
                                   @Name varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Employees(Name)
                                    VALUES (@Name)
                                   END");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetEmployees");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetEmployee");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateEmployee");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteEmployee");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateEmployee");
        }
    }
}
