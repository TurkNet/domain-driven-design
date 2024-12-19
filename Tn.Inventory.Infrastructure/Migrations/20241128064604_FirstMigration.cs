using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Tn.Inventory.Domain.ValueObjects;

#nullable disable

namespace Tn.Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BrandId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    PurchaseStatus = table.Column<int>(type: "integer", nullable: false),
                    OrderNumber = table.Column<string>(type: "text", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TargetWarehouseId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SupplierId = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<Address>(type: "jsonb", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LineNumber = table.Column<int>(type: "integer", nullable: false),
                    PurchaseId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseDetails_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SourceWarehouseId = table.Column<int>(type: "integer", nullable: false),
                    TargetWarehouseId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ShipmentStatus = table.Column<int>(type: "integer", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ShippingAddress = table.Column<Address>(type: "jsonb", nullable: false),
                    ReceiverName = table.Column<string>(type: "text", nullable: false),
                    ReceiverPhoneNumber = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shipments_Warehouses_SourceWarehouseId",
                        column: x => x.SourceWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Warehouses_TargetWarehouseId",
                        column: x => x.TargetWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stocks_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LineNumber = table.Column<int>(type: "integer", nullable: false),
                    ShipmentId = table.Column<int>(type: "integer", nullable: false),
                    DeviceId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentDetails_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentDetails_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BrandId",
                table: "Devices",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_DeviceId",
                table: "PurchaseDetails",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_PurchaseId",
                table: "PurchaseDetails",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_SupplierId",
                table: "Purchases",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_DeviceId",
                table: "ShipmentDetails",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDetails_ShipmentId",
                table: "ShipmentDetails",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SourceWarehouseId",
                table: "Shipments",
                column: "SourceWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_TargetWarehouseId",
                table: "Shipments",
                column: "TargetWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_DeviceId",
                table: "Stocks",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WarehouseId",
                table: "Stocks",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_SupplierId",
                table: "Warehouses",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "ShipmentDetails");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
