﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ResourcesWebApplication.Models.Context;

namespace ResourcesWebApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240310142300_MLFthMigration")]
    partial class MLFthMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ResourcesWebApplication.Models.Astrology.Zodiac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Zodiacs");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Chronometers.Chronometer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Hour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chronometers");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Cisco.IPAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InterfaceName")
                        .HasColumnType("int");

                    b.Property<string>("LoginID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subnet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IPAddresses");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Cisco.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Cisco.Port", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAdressID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ports");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Commands.CiscoCommand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CiscoCommands");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Commands.Cli", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Commands.Container", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Command")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Created")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ports")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Dictionaries.Dictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Concept")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Definition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Dictionaries");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Documents.Code", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Codes");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Documents.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Documents.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Page")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Search")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Documents.Reading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DocumentID")
                        .HasColumnType("int");

                    b.Property<string>("EndAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartAT")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Readings");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.DualPlayers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthPreference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthStrategy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthUtility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPreference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndStrategy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndUtility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DualPlayers");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.TwoPlayers.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FthPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Choices");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.TwoPlayers.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.TwoPlayers.PlayersDataset", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FthPlayer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthPreference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthStrategy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPreference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndStrategy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PlayersDatasets");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.TwoPlayers.Preference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FthPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthPlayerPreference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerPreference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Preferences");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.TwoPlayers.Rule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Choice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.TwoPlayers.Strategy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FthPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthPlayerStrategy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerStrategy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Strategies");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Games.TwoPlayers.Utility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FthPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FthPlayerUtility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SndPlayerUtility")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Utilities");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Knowledge.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CodeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DocumentID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkillID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Knowledge.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.MachineLearning.Columns", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Column")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatasetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.MachineLearning.Describe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Count")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiftyPercent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Max")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mean")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Min")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeventyFivePercent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Std")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TwentyFivePercent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Describes");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.MachineLearning.Info", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DatasetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Infos");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.MachineLearning.Visualizations.Plot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Encoding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Plots");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.MachineLearning.Visualizations.PlotByColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Column")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Encoding")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PlotByColumns");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Networking.IPv4.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Plaintext", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Plaintexts");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Politics.USPresident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Born")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Died")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndOfPresidency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NetWorth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PoliticalParty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostPresidency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("President")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PresidentNO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartOfPresidency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("USPresidents");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Prompts.Prompt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prompts");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Servers.Backups.Backup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Folder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RockyBackup");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Servers.Groups.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RockyGroups");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Servers.Users.Hash", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hashed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hashes");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Servers.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RockyUsers");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Tenses.Conjugation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Person")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tense")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Verb")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerbForm")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Conjugations");
                });

            modelBuilder.Entity("ResourcesWebApplication.Models.Tenses.Tense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CreatedAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tenses");
                });
#pragma warning restore 612, 618
        }
    }
}
