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
	                                @EmployeeName varchar(100),
                                    	@EmployeeCode  varchar(100),
                                   	@EmployeePosition  varchar(100),
					@EmployeePhone  varchar(100),
                                    	@EmployeeEmail  varchar(100),
                                   	@EmployeeSite varchar(100)	



                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Employees
                                     SET  EmployeeName = @EmployeeName, EmployeeCode = @EmployeeCode, EmployeePosition= @EmployeePosition,EmployeePhone  = @EmployeePhone ,EmployeeEmail = @EmployeeEmail
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
                                   @EmployeeName varchar(100),
                                   @EmployeeCode varchar(100),
                                   @EmployeePosition varchar(100),
				   @EmployeePhone varchar(100),
                                   @EmployeeEmail varchar(100),
                                   @EmployeeSite varchar(100)	
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Employees(
                                    [EmployeeName]
                                    ,[EmployeeCode]
                                    , [EmployeePosition]
				    ,[EmployeePhone]
                                    ,[EmployeeEmail]
                                    , [EmployeeSite]
                                    )
                                    VALUES (@EmployeeName,@EmployeeCode,@EmployeePosition,@EmployeePhone,@EmployeeEmail,@EmployeeSite)
                                  
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
