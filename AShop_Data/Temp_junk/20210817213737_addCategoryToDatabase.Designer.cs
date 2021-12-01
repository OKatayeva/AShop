﻿// <auto-generated />
using AShop_Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AShop_Data.Migrations
{
    [DbContext(typeof(AshopDB))]
    [Migration("20210817213737_addCategoryToDatabase")]
    partial class addCategoryToDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //modelBuilder.Entity("AShop_Models.ApplicationType", b =>
            //    {
            //        b.Property<int>("Id")
            //            .ValueGeneratedOnAdd()
            //            .HasColumnType("int")
            //            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //        b.Property<string>("Name")
            //            .HasColumnType("nvarchar(max)");

            //        b.HasKey("Id");

            //        b.ToTable("ApplicationType");
            //    });

            modelBuilder.Entity("AShop_Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
