﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using middlerApp.Core.DataAccess;

namespace middlerApp.Core.DataAccess.Postgres.Migrations
{
    [DbContext(typeof(APPDbContext))]
    [Migration("20201201185420_VariableByteContent")]
    partial class VariableByteContent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointActionEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActionType")
                        .HasColumnType("text");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<Guid>("EndpointRuleEntityId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Order")
                        .HasColumnType("numeric");

                    b.Property<string>("Parameters")
                        .HasColumnType("text");

                    b.Property<bool>("Terminating")
                        .HasColumnType("boolean");

                    b.Property<bool>("WriteStreamDirect")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("EndpointRuleEntityId");

                    b.ToTable("EndpointActions");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRuleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Hostname")
                        .HasColumnType("text");

                    b.Property<string>("HttpMethods")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Order")
                        .HasColumnType("numeric");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<string>("Scheme")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EndpointRules");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.EndpointRulePermission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessMode")
                        .HasColumnType("text");

                    b.Property<string>("Client")
                        .HasColumnType("text");

                    b.Property<Guid?>("EndpointRuleEntityId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Order")
                        .HasColumnType("numeric");

                    b.Property<string>("PrincipalName")
                        .HasColumnType("text");

                    b.Property<string>("SourceAddress")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EndpointRuleEntityId");

                    b.ToTable("EndpointRulePermission");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.TreeNode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Bytes")
                        .HasColumnType("bytea");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Extension")
                        .HasColumnType("text");

                    b.Property<bool>("IsFolder")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Parent")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Variables");
                });

            modelBuilder.Entity("middlerApp.Core.DataAccess.Entities.Models.TypeDefinition", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Module")
                        .HasColumnType("text");

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
