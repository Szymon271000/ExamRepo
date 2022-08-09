using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Datas.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationalMaterialsCounter = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    MaterialTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefinitionMaterialType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.MaterialTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "EducationalMaterials",
                columns: table => new
                {
                    EducationalMaterialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    authorId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    materialTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalMaterials", x => x.EducationalMaterialId);
                    table.ForeignKey(
                        name: "FK_EducationalMaterials_Authors_authorId",
                        column: x => x.authorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EducationalMaterials_MaterialTypes_materialTypeId",
                        column: x => x.materialTypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "MaterialTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialReviews",
                columns: table => new
                {
                    MaterialReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    educationalMaterialId = table.Column<int>(type: "int", nullable: true),
                    MaterialReviewDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialReviewDigit = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialReviews", x => x.MaterialReviewId);
                    table.ForeignKey(
                        name: "FK_MaterialReviews_EducationalMaterials_educationalMaterialId",
                        column: x => x.educationalMaterialId,
                        principalTable: "EducationalMaterials",
                        principalColumn: "EducationalMaterialId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "AuthorName", "Description", "EducationalMaterialsCounter" },
                values: new object[,]
                {
                    { 1, "Willy", "New author", null },
                    { 2, "Bobby", "New author2", null },
                    { 3, "Jack", "New author3", null },
                    { 4, "Harry", "New author4", null }
                });

            migrationBuilder.InsertData(
                table: "MaterialTypes",
                columns: new[] { "MaterialTypeId", "DefinitionMaterialType", "MaterialTypeName" },
                values: new object[,]
                {
                    { 1, "Video tutorial focused on EF", "Video Tutorial" },
                    { 2, "Documentation focused on EF", "Documentation" },
                    { 3, "Excersises focused on EF", "Exercises" },
                    { 4, "Video explanation focused on EF", "Video explanation" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "EducationalMaterials",
                columns: new[] { "EducationalMaterialId", "Description", "Location", "Title", "authorId", "materialTypeId" },
                values: new object[,]
                {
                    { 1, "First material", "codecoolʼs library at Slusarska 9", "FirstOne", 1, 1 },
                    { 2, "Second material", "codecoolʼs library at Slusarska 9", "SecondOne", 2, 2 },
                    { 3, "Third material", "codecoolʼs library at Slusarska 9", "ThirdOne", 3, 3 },
                    { 4, "Fourth material", "codecoolʼs library at Slusarska 9", "FourhOne", 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "MaterialReviews",
                columns: new[] { "MaterialReviewId", "MaterialReviewDescription", "MaterialReviewDigit", "educationalMaterialId" },
                values: new object[,]
                {
                    { 1, "I like the content but author has terrible accent", 2f, 1 },
                    { 2, "Good content", 8f, 2 },
                    { 3, "Bad content", 1f, 3 },
                    { 4, "Normal content", 6f, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationalMaterials_authorId",
                table: "EducationalMaterials",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalMaterials_materialTypeId",
                table: "EducationalMaterials",
                column: "materialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialReviews_educationalMaterialId",
                table: "MaterialReviews",
                column: "educationalMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersUserId",
                table: "RoleUser",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialReviews");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "EducationalMaterials");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "MaterialTypes");
        }
    }
}
