﻿// <auto-generated />
using System;
using CountryHolidays_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CountryHolidays_API.Migrations
{
    [DbContext(typeof(CountryHolidaysContext))]
    [Migration("20210512170829_Added_HolidayFullname")]
    partial class Added_HolidayFullname
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CountryHolidays_API.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CountryCode")
                        .HasMaxLength(25);

                    b.Property<int?>("FromDateId");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("HolidayName");

                    b.Property<int?>("ToDateId");

                    b.HasKey("Id");

                    b.HasIndex("FromDateId");

                    b.HasIndex("ToDateId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CountryHolidays_API.Entities.Date", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Day");

                    b.Property<int>("DayOfWeek");

                    b.Property<int>("Month");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("FromToDate");
                });

            modelBuilder.Entity("CountryHolidays_API.Entities.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId");

                    b.Property<int?>("DateId");

                    b.Property<string>("Name");

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("DateId");

                    b.HasIndex("TypeId");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("CountryHolidays_API.Entities.HolidayType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("HolidayType");
                });

            modelBuilder.Entity("CountryHolidays_API.Entities.Country", b =>
                {
                    b.HasOne("CountryHolidays_API.Entities.Date", "FromDate")
                        .WithMany()
                        .HasForeignKey("FromDateId");

                    b.HasOne("CountryHolidays_API.Entities.Date", "ToDate")
                        .WithMany()
                        .HasForeignKey("ToDateId");
                });

            modelBuilder.Entity("CountryHolidays_API.Entities.Holiday", b =>
                {
                    b.HasOne("CountryHolidays_API.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("CountryHolidays_API.Entities.Date", "Date")
                        .WithMany()
                        .HasForeignKey("DateId");

                    b.HasOne("CountryHolidays_API.Entities.HolidayType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
