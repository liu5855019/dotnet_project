﻿// <auto-generated />
using System;
using DM.Log.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DM.Log.Dal.Migrations
{
    [DbContext(typeof(LogDBContext))]
    [Migration("20230110095718_Update5")]
    partial class Update5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DM.Log.Entity.LogDotaRun", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("CreateDt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DeviceId")
                        .HasColumnType("longtext");

                    b.Property<string>("GroupId")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsShop")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("RequestId")
                        .HasColumnType("bigint");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdateDt")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("LogDotaRun");
                });

            modelBuilder.Entity("DM.Log.Entity.LogInterface", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CreateBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("CreateDt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("Interface Name");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasComment("Remark");

                    b.Property<long>("RequestId")
                        .HasColumnType("bigint");

                    b.Property<string>("Service")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("Service Name");

                    b.Property<string>("UpdateBy")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdateDt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Value")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasComment("Para");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("LogInterface");
                });
#pragma warning restore 612, 618
        }
    }
}
