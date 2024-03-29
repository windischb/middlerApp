﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using middlerApp.Core.DataAccess;

namespace middlerApp.Core.DataAccess.SqlServer.Migrations
{
    [DbContext(typeof(APPDbContext))]
    [Migration("20201026154457_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointActionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<Guid>("EndpointRuleEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Order")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Parameters")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Terminating")
                        .HasColumnType("bit");

                    b.Property<bool>("WriteStreamDirect")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EndpointRuleEntityId");

                    b.ToTable("EndpointActions");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Hostname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HttpMethods")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Order")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Scheme")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EndpointRules");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.TreeNode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointActionEntity", b =>
                {
                    b.HasOne("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", null)
                        .WithMany("Actions")
                        .HasForeignKey("EndpointRuleEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", b =>
                {
                    b.Navigation("Actions");
                });
#pragma warning restore 612, 618
        }
    }
}
