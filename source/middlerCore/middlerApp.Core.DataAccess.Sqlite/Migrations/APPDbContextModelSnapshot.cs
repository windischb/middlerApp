﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using middlerApp.Core.DataAccess;

namespace middlerApp.Core.DataAccess.Sqlite.Migrations
{
    [DbContext(typeof(APPDbContext))]
    partial class APPDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointActionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionType")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("EndpointRuleEntityId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Order")
                        .HasColumnType("TEXT");

                    b.Property<string>("Parameters")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Terminating")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("WriteStreamDirect")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EndpointRuleEntityId");

                    b.ToTable("EndpointActions");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Hostname")
                        .HasColumnType("TEXT");

                    b.Property<string>("HttpMethods")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Order")
                        .HasColumnType("TEXT");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.Property<string>("Scheme")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EndpointRules");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRulePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("AccessMode")
                        .HasColumnType("TEXT");

                    b.Property<string>("Client")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("EndpointRuleEntityId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Order")
                        .HasColumnType("TEXT");

                    b.Property<string>("PrincipalName")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EndpointRuleEntityId");

                    b.ToTable("EndpointRulePermission");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.TreeNode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("Bytes")
                        .HasColumnType("BLOB");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Extension")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Parent")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.TypeDefinition", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("Module")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TypeDefinitions");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointActionEntity", b =>
                {
                    b.HasOne("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", null)
                        .WithMany("Actions")
                        .HasForeignKey("EndpointRuleEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRulePermission", b =>
                {
                    b.HasOne("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", null)
                        .WithMany("Permissions")
                        .HasForeignKey("EndpointRuleEntityId");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
