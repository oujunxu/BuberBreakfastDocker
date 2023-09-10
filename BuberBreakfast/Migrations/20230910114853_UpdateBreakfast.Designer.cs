﻿// <auto-generated />
using System;
using BuberBreakfast.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuberBreakfast.Migrations
{
    [DbContext(typeof(BreakfastContext))]
    [Migration("20230910114853_UpdateBreakfast")]
    partial class UpdateBreakfast
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BuberBreakfast.Entities.BreakfastEntity", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Breakfasts");
                });
#pragma warning restore 612, 618
        }
    }
}
