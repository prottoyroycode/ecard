﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.DataContext;

namespace WebApi.Migrations
{
    [DbContext(typeof(EfDbContext))]
    [Migration("20211125110612_refcodecolinresponsetable")]
    partial class refcodecolinresponsetable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Entities.CallbackData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CardCount")
                        .HasColumnType("int");

                    b.Property<string>("CardResults")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<DateTime>("created_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("is_completed")
                        .HasColumnType("bit");

                    b.Property<string>("next")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<string>("previous")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("reference_code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("responseCode")
                        .HasColumnType("int");

                    b.Property<int>("sku")
                        .HasColumnType("int");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<string>("status_text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("success")
                        .HasColumnType("bit");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("unit_price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("CallbackDatas");
                });

            modelBuilder.Entity("Models.Entities.EmailSMTPConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Host")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSSL")
                        .HasColumnType("bit");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("SenderEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderEmailPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebDomain")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EmailSMTPConfigurations");
                });

            modelBuilder.Entity("Models.Entities.Favourite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrlPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFavourite")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Sku")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updatedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("Models.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Channel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Delivery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("OrderAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Payment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updatedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WhatsApp")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Models.Entities.OrderDetails", b =>
                {
                    b.Property<Guid>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageUrlPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Sku")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Models.Entities.OrderResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updatedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("dataStore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("reference_code")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("responseCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("success")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderResponses");
                });

            modelBuilder.Entity("Models.Entities.RockVilleOrderData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updatedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("pre_order")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("reference_code")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("sku")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("store_id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RockVilleOrderData");
                });

            modelBuilder.Entity("Models.Entities.UserPasswordResetCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("UserPasswordResetCodes");
                });

            modelBuilder.Entity("Models.Securities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacebookId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("GoogleId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ImageName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ImageSource")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LinkedinId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime");

                    b.Property<string>("RegisterWith")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("TwitterId")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Models.Securities.UserFcmDeviceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FcmDeviceId")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserFcmDeviceHistory");
                });

            modelBuilder.Entity("Models.Securities.UserLoginHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FcmDeviceId")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime>("LogInTIme")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LogOutTIme")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserLoginHistory");
                });

            modelBuilder.Entity("Models.Securities.UserPasswordHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Password")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPasswordHistory");
                });

            modelBuilder.Entity("Models.bKash.bKashCallback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updatedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("apiVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bKashCallbacks");
                });

            modelBuilder.Entity("Models.bKash.bKashCreateAgreementRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updatedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("callbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("payerReference")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bKashCreateAgreementRequests");
                });

            modelBuilder.Entity("Models.bKash.bKashCreateAgreementResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Updatedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agreementID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("amount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("bkashURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("callbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cancelledCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("failureCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("intent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("merchantInvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentCreateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("statusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("successCallbackURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("transactionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("bKashCreateAgreementResponses");
                });

            modelBuilder.Entity("Models.Entities.OrderDetails", b =>
                {
                    b.HasOne("Models.Entities.Order", null)
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Models.Securities.UserFcmDeviceHistory", b =>
                {
                    b.HasOne("Models.Securities.User", null)
                        .WithMany("UserFcmDeviceHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Securities.UserLoginHistory", b =>
                {
                    b.HasOne("Models.Securities.User", null)
                        .WithMany("UserLoginHistry")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Securities.UserPasswordHistory", b =>
                {
                    b.HasOne("Models.Securities.User", null)
                        .WithMany("UserPasswordHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Models.Securities.User", b =>
                {
                    b.Navigation("UserFcmDeviceHistory");

                    b.Navigation("UserLoginHistry");

                    b.Navigation("UserPasswordHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
