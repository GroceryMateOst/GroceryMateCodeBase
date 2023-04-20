﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using grocery_mate_backend.Data.Context;

#nullable disable

namespace grocery_mate_backend.Migrations
{
    [DbContext(typeof(GroceryContext))]
    partial class GroceryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.Shopping.Grocery", b =>
                {
                    b.Property<Guid>("GroceryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Unit")
                        .HasColumnType("integer");

                    b.HasKey("GroceryId");

                    b.ToTable("Grocery");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.Shopping.GroceryRequest", b =>
                {
                    b.Property<Guid>("GroceryRequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ContractorUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RatingId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ShoppingListId")
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("GroceryRequestId");

                    b.HasIndex("ClientUserId");

                    b.HasIndex("ContractorUserId");

                    b.HasIndex("RatingId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("GroceryRequests");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.Shopping.ShoppingList", b =>
                {
                    b.Property<Guid>("ShoppingListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PreferredStore")
                        .HasColumnType("integer");

                    b.HasKey("ShoppingListId");

                    b.ToTable("ShoppingList");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.Shopping.ShoppingListItem", b =>
                {
                    b.Property<Guid>("ShoppingListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid>("GroceryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreferredBrand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ShoppingListId")
                        .HasColumnType("uuid");

                    b.HasKey("ShoppingListItemId");

                    b.HasIndex("GroceryId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("ShoppingListItem");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.UserManagement.Address.Address", b =>
                {
                    b.Property<Guid>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HouseNr")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ZipCode")
                        .HasColumnType("integer");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.UserManagement.Rating", b =>
                {
                    b.Property<Guid>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EvaluatorUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("UserRating")
                        .HasColumnType("integer");

                    b.HasKey("RatingId");

                    b.HasIndex("EvaluatorUserId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.UserManagement.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IdentityId")
                        .HasColumnType("text");

                    b.Property<string>("ResidencyDetails")
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("AddressId");

                    b.HasIndex("IdentityId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.Shopping.GroceryRequest", b =>
                {
                    b.HasOne("grocery_mate_backend.Data.DataModels.UserManagement.User", "Client")
                        .WithMany()
                        .HasForeignKey("ClientUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("grocery_mate_backend.Data.DataModels.UserManagement.User", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("grocery_mate_backend.Data.DataModels.UserManagement.Rating", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId");

                    b.HasOne("grocery_mate_backend.Data.DataModels.Shopping.ShoppingList", "ShoppingList")
                        .WithMany()
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Contractor");

                    b.Navigation("Rating");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.Shopping.ShoppingListItem", b =>
                {
                    b.HasOne("grocery_mate_backend.Data.DataModels.Shopping.Grocery", "Grocery")
                        .WithMany()
                        .HasForeignKey("GroceryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("grocery_mate_backend.Data.DataModels.Shopping.ShoppingList", null)
                        .WithMany("Items")
                        .HasForeignKey("ShoppingListId");

                    b.Navigation("Grocery");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.UserManagement.Rating", b =>
                {
                    b.HasOne("grocery_mate_backend.Data.DataModels.UserManagement.User", "Evaluator")
                        .WithMany()
                        .HasForeignKey("EvaluatorUserId");

                    b.Navigation("Evaluator");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.UserManagement.User", b =>
                {
                    b.HasOne("grocery_mate_backend.Data.DataModels.UserManagement.Address.Address", null)
                        .WithMany("Users")
                        .HasForeignKey("AddressId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Identity")
                        .WithMany()
                        .HasForeignKey("IdentityId");

                    b.Navigation("Identity");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.Shopping.ShoppingList", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("grocery_mate_backend.Data.DataModels.UserManagement.Address.Address", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
