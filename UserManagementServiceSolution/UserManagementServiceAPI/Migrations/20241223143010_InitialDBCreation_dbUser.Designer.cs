﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserManagementServiceAPI.DbContexts;

#nullable disable

namespace UserManagementServiceAPI.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20241223143010_InitialDBCreation_dbUser")]
    partial class InitialDBCreation_dbUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserManagementServiceAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserCity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Age = 0,
                            Email = "johndoe@gmail.com",
                            Password = "JohnDoe123",
                            PhoneNumber = "9422221230",
                            UserCity = "Chennai",
                            UserName = "John"
                        },
                        new
                        {
                            UserId = 2,
                            Age = 0,
                            Email = "dralex@apollo.com",
                            Password = "AlexAdmin",
                            PhoneNumber = "9022296230",
                            UserCity = "Chennai",
                            UserName = "Alex"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}