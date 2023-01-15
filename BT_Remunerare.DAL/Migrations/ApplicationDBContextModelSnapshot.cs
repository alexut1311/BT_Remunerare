﻿// <auto-generated />
using BT_Remunerare.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTRemunerare.DAL.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Period", b =>
                {
                    b.Property<int>("PeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PeriodId"));

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("PeriodId");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"));

                    b.Property<int>("NumberOfProducts")
                        .HasColumnType("int");

                    b.Property<int>("PeriodId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("SaleId");

                    b.HasIndex("PeriodId");

                    b.HasIndex("ProductId");

                    b.HasIndex("VendorId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.SalesRemunerationRule", b =>
                {
                    b.Property<int>("RemunerationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RemunerationId"));

                    b.Property<int>("PeriodId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Remuneration")
                        .HasColumnType("int");

                    b.HasKey("RemunerationId");

                    b.HasIndex("PeriodId");

                    b.HasIndex("ProductId");

                    b.ToTable("SalesRemunerationRules");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Vendor", b =>
                {
                    b.Property<int>("VendorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendorId"));

                    b.Property<string>("VendorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VendorId");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Sale", b =>
                {
                    b.HasOne("BT_Remunerare.DAL.Entities.Period", "SalePeriod")
                        .WithMany("Sales")
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BT_Remunerare.DAL.Entities.Product", "SaleProduct")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BT_Remunerare.DAL.Entities.Vendor", "SaleVendor")
                        .WithMany("Sales")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SalePeriod");

                    b.Navigation("SaleProduct");

                    b.Navigation("SaleVendor");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.SalesRemunerationRule", b =>
                {
                    b.HasOne("BT_Remunerare.DAL.Entities.Period", "SalesRemunerationPeriod")
                        .WithMany("SalesRemunerationRules")
                        .HasForeignKey("PeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BT_Remunerare.DAL.Entities.Product", "RemunerationProduct")
                        .WithMany("SalesRemunerationRules")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RemunerationProduct");

                    b.Navigation("SalesRemunerationPeriod");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Period", b =>
                {
                    b.Navigation("Sales");

                    b.Navigation("SalesRemunerationRules");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Product", b =>
                {
                    b.Navigation("SalesRemunerationRules");
                });

            modelBuilder.Entity("BT_Remunerare.DAL.Entities.Vendor", b =>
                {
                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
