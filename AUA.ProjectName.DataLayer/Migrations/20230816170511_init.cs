using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AUA.ProjectName.DataLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "acc");

            migrationBuilder.CreateTable(
                name: "ActiveAccessToken",
                schema: "acc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "GetDate()"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveAccessToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                schema: "acc",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "GetDate()"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "acc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "GetDate()"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccess",
                schema: "acc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    AccessCode = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsIndirect = table.Column<bool>(type: "bit", nullable: false),
                    PageTitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PageDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "GetDate()"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "acc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "GetDate()"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalSchema: "acc",
                        principalTable: "AppUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "acc",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRoleAccess",
                schema: "acc",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserAccessId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "GetDate()"),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleAccess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleAccess_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "acc",
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRoleAccess_UserAccess_UserAccessId",
                        column: x => x.UserAccessId,
                        principalSchema: "acc",
                        principalTable: "UserAccess",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                schema: "acc",
                table: "AppUser",
                columns: new[] { "Id", "CreatorUserId", "Email", "FirstName", "IsActive", "LastName", "Password", "Phone", "UserName" },
                values: new object[] { 1L, null, "Mr_lotfi@ymail.com", "Rahim", true, "Lotfi", "3C9909AFEC25354D551DAE21590BB26E38D53F2173B8D3DC3EEE4C047E7AB1C1EB8B85103E3BE7BA613B31BB5C9C36214DC9F14A42FD7A2FDB84856BCA5C44C2", "+989199906342", "admin" });

            migrationBuilder.InsertData(
                schema: "acc",
                table: "Role",
                columns: new[] { "Id", "CreatorUserId", "Description", "IsActive", "Title" },
                values: new object[] { 1, 1L, "AUA default role", true, "Admin" });

            migrationBuilder.InsertData(
                schema: "acc",
                table: "UserAccess",
                columns: new[] { "Id", "AccessCode", "CreatorUserId", "IsActive", "IsIndirect", "PageDescription", "PageTitle", "ParentId", "Title", "Url" },
                values: new object[,]
                {
                    { 1, 1, 1L, true, true, "User Management", "User Management", 0, "User Management", "../Accounting/AppUser" },
                    { 2, 3, 1L, true, true, "Access level management", "Access level management", 0, "Access level management", "../Accounting/UserAccess" },
                    { 3, 2, 1L, true, true, "Role Management", "Role Management", 0, "Role Management", "../Accounting/Role" },
                    { 4, 4, 1L, true, true, "Report access to users", "Report access to users", 0, "Report access to users", "../reports/UserAccessReport" },
                    { 5, 5, 1L, true, false, "Reset Password", "Reset User Password", 0, "Reset Password", "" },
                    { 6, 6, 1L, true, true, "Assign access to roles", "Assign access to roles", 0, "User Access Report", "" }
                });

            migrationBuilder.InsertData(
                schema: "acc",
                table: "UserRole",
                columns: new[] { "Id", "AppUserId", "CreatorUserId", "IsActive", "RoleId" },
                values: new object[] { 1, 1L, null, true, 1 });

            migrationBuilder.InsertData(
                schema: "acc",
                table: "UserRoleAccess",
                columns: new[] { "Id", "CreatorUserId", "IsActive", "RoleId", "UserAccessId" },
                values: new object[,]
                {
                    { 1, 1L, true, 1, 1 },
                    { 2, 1L, true, 1, 2 },
                    { 3, 1L, true, 1, 3 },
                    { 4, 1L, true, 1, 4 },
                    { 5, 1L, true, 1, 5 },
                    { 6, 1L, true, 1, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_AppUserId",
                schema: "acc",
                table: "UserRole",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                schema: "acc",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAccess_RoleId",
                schema: "acc",
                table: "UserRoleAccess",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleAccess_UserAccessId",
                schema: "acc",
                table: "UserRoleAccess",
                column: "UserAccessId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveAccessToken",
                schema: "acc");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "acc");

            migrationBuilder.DropTable(
                name: "UserRoleAccess",
                schema: "acc");

            migrationBuilder.DropTable(
                name: "AppUser",
                schema: "acc");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "acc");

            migrationBuilder.DropTable(
                name: "UserAccess",
                schema: "acc");
        }
    }
}
