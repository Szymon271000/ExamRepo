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
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Access = table.Column<int>(type: "int", nullable: true),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                values: new object[] { 1, "Willy", "New author", null });

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
                table: "EducationalMaterials",
                columns: new[] { "EducationalMaterialId", "Description", "Location", "Title", "authorId", "materialTypeId" },
                values: new object[] { 1, "First material", "codecoolʼs library at Slusarska 9", null, 1, 1 });

            migrationBuilder.InsertData(
                table: "MaterialReviews",
                columns: new[] { "MaterialReviewId", "MaterialReviewDescription", "MaterialReviewDigit", "educationalMaterialId" },
                values: new object[] { 1, "I like the content but author has terrible accent", 2f, 1 });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialReviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "EducationalMaterials");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "MaterialTypes");
        }
    }
}
