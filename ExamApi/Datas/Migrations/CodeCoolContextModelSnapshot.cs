﻿// <auto-generated />
using System;
using Datas.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Datas.Migrations
{
    [DbContext(typeof(CodeCoolContext))]
    partial class CodeCoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Datas.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"), 1L, 1);

                    b.Property<string>("AuthorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EducationalMaterialsCounter")
                        .HasColumnType("int");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            AuthorName = "Willy",
                            Description = "New author"
                        });
                });

            modelBuilder.Entity("Datas.Models.EducationalMaterial", b =>
                {
                    b.Property<int>("EducationalMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EducationalMaterialId"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("authorId")
                        .HasColumnType("int");

                    b.Property<int?>("materialTypeId")
                        .HasColumnType("int");

                    b.HasKey("EducationalMaterialId");

                    b.HasIndex("authorId");

                    b.HasIndex("materialTypeId");

                    b.ToTable("EducationalMaterials");

                    b.HasData(
                        new
                        {
                            EducationalMaterialId = 1,
                            Description = "First material",
                            Location = "codecoolʼs library at Slusarska 9",
                            authorId = 1,
                            materialTypeId = 1
                        });
                });

            modelBuilder.Entity("Datas.Models.MaterialReview", b =>
                {
                    b.Property<int>("MaterialReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialReviewId"), 1L, 1);

                    b.Property<string>("MaterialReviewDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float?>("MaterialReviewDigit")
                        .HasColumnType("real");

                    b.Property<int?>("educationalMaterialId")
                        .HasColumnType("int");

                    b.HasKey("MaterialReviewId");

                    b.HasIndex("educationalMaterialId");

                    b.ToTable("MaterialReviews");

                    b.HasData(
                        new
                        {
                            MaterialReviewId = 1,
                            MaterialReviewDescription = "I like the content but author has terrible accent",
                            MaterialReviewDigit = 2f,
                            educationalMaterialId = 1
                        });
                });

            modelBuilder.Entity("Datas.Models.MaterialType", b =>
                {
                    b.Property<int>("MaterialTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialTypeId"), 1L, 1);

                    b.Property<string>("DefinitionMaterialType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaterialTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaterialTypeId");

                    b.ToTable("MaterialTypes");

                    b.HasData(
                        new
                        {
                            MaterialTypeId = 1,
                            DefinitionMaterialType = "Video tutorial focused on EF",
                            MaterialTypeName = "Video Tutorial"
                        },
                        new
                        {
                            MaterialTypeId = 2,
                            DefinitionMaterialType = "Documentation focused on EF",
                            MaterialTypeName = "Documentation"
                        },
                        new
                        {
                            MaterialTypeId = 3,
                            DefinitionMaterialType = "Excersises focused on EF",
                            MaterialTypeName = "Exercises"
                        },
                        new
                        {
                            MaterialTypeId = 4,
                            DefinitionMaterialType = "Video explanation focused on EF",
                            MaterialTypeName = "Video explanation"
                        });
                });

            modelBuilder.Entity("Datas.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<int?>("Access")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Datas.Models.EducationalMaterial", b =>
                {
                    b.HasOne("Datas.Models.Author", "author")
                        .WithMany("EducationalMaterials")
                        .HasForeignKey("authorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Datas.Models.MaterialType", "materialType")
                        .WithMany("educationalMaterials")
                        .HasForeignKey("materialTypeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("author");

                    b.Navigation("materialType");
                });

            modelBuilder.Entity("Datas.Models.MaterialReview", b =>
                {
                    b.HasOne("Datas.Models.EducationalMaterial", "educationalMaterial")
                        .WithMany()
                        .HasForeignKey("educationalMaterialId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("educationalMaterial");
                });

            modelBuilder.Entity("Datas.Models.Author", b =>
                {
                    b.Navigation("EducationalMaterials");
                });

            modelBuilder.Entity("Datas.Models.MaterialType", b =>
                {
                    b.Navigation("educationalMaterials");
                });
#pragma warning restore 612, 618
        }
    }
}
