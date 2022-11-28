﻿// <auto-generated />
using System;
using GameLog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameLog.Infrastructure.Migrations
{
    [DbContext(typeof(GameLogDbContext))]
    [Migration("20221128153455_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GameLog.Infrastructure.Database.Entities.GameProfile", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Developer")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Genre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Publisher")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("GameProfiles", (string)null);
                });

            modelBuilder.Entity("GameLog.Infrastructure.Database.Entities.Gamer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nickname")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.Property<int>("NumberOfPlayedGames")
                        .HasColumnType("int");

                    b.Property<string>("Rank")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Gamers", (string)null);
                });

            modelBuilder.Entity("GameLog.Infrastructure.Database.Entities.Librarian", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nickname")
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)");

                    b.HasKey("Id");

                    b.ToTable("Librarians", (string)null);
                });

            modelBuilder.Entity("GameLog.Infrastructure.Database.Entities.PlayedGame", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("GameProfileId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GamerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("HoursPlayed")
                        .HasColumnType("int");

                    b.Property<int>("PercentageScore")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameProfileId");

                    b.HasIndex("GamerId");

                    b.ToTable("PlayedGames", (string)null);
                });

            modelBuilder.Entity("GameLog.Infrastructure.Database.Entities.PlayedGame", b =>
                {
                    b.HasOne("GameLog.Infrastructure.Database.Entities.GameProfile", "GameProfile")
                        .WithMany()
                        .HasForeignKey("GameProfileId");

                    b.HasOne("GameLog.Infrastructure.Database.Entities.Gamer", "Gamer")
                        .WithMany("PlayedGames")
                        .HasForeignKey("GamerId");

                    b.Navigation("GameProfile");

                    b.Navigation("Gamer");
                });

            modelBuilder.Entity("GameLog.Infrastructure.Database.Entities.Gamer", b =>
                {
                    b.Navigation("PlayedGames");
                });
#pragma warning restore 612, 618
        }
    }
}
