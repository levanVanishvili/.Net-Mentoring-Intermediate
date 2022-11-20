﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using craft_beer.Data;

#nullable disable

namespace craftbeer.Migrations
{
    [DbContext(typeof(BeerDbContext))]
    [Migration("20221120181947_ondeletecascade")]
    partial class ondeletecascade
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("craft_beer.Models.BeerStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Style")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BeerStyles");
                });

            modelBuilder.Entity("craft_beer.Models.beer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BeerStyleId")
                        .HasColumnType("int");

                    b.Property<string>("BeerTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BeerStyleId");

                    b.ToTable("beer");
                });

            modelBuilder.Entity("craft_beer.Models.beer", b =>
                {
                    b.HasOne("craft_beer.Models.BeerStyle", "Style")
                        .WithMany("Beers")
                        .HasForeignKey("BeerStyleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Style");
                });

            modelBuilder.Entity("craft_beer.Models.BeerStyle", b =>
                {
                    b.Navigation("Beers");
                });
#pragma warning restore 612, 618
        }
    }
}
