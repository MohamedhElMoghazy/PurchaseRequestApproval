using Microsoft.EntityFrameworkCore.Migrations;

namespace PurchaseRequestApproval.DataAccess.Migrations
{
    public partial class AddStoredProcForProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"CREATE PROC usp_GetProjects 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Projects 
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_GetProject 
                                    @Id int 
                                    AS 
                                    BEGIN 
                                     SELECT * FROM   dbo.Projects  WHERE  (Id = @Id) 
                                    END ");

            migrationBuilder.Sql(@"CREATE PROC usp_UpdateProject
	                                @Id int,
	                                @ProjectName varchar(100),
                                    @WorkPackageIn varchar(100),
                                    @ContractNo varchar(100)
                                    AS 
                                    BEGIN 
                                     UPDATE dbo.Projects
                                     SET  ProjectName = @ProjectName, WorkPackageIn = @WorkPackageIn, ContractNo = @ContractNo
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_DeleteProject
	                                @Id int
                                    AS 
                                    BEGIN 
                                     DELETE FROM dbo.Projects
                                     WHERE  Id = @Id
                                    END");

            migrationBuilder.Sql(@"CREATE PROC usp_CreateProject
                                   @ProjectName varchar(100),
                                   @WorkPackageIn varchar(100),
                                   @ContractNo varchar(100)
                                   AS 
                                   BEGIN 
                                    INSERT INTO dbo.Projects(
                                    [ProjectName]
                                    ,[WorkPackageIn]
                                    , [ContractNo]
                                    )
                                    VALUES (@ProjectName,@WorkPackageIn,@ContractNo)
                                  
                                   END");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetProjects");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_GetProject");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_UpdateProject");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_DeleteProject");
            migrationBuilder.Sql(@"DROP PROCEDURE usp_CreateProject");
        }
    }
}
