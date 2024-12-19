using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using Tn.Inventory.Domain.ValueObjects;

#nullable disable

namespace Tn.Inventory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createdDate = DateTime.Now;

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name", "IsActive", "IsDeleted", "CreatedAt", "CreatedBy" },
                values: new object[,]
                {
                    { 1, "AirTies", true, false, createdDate, "system" },
                    { 2, "Zyxel", true, false, createdDate, "system" }
                }
            );

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Code", "Name", "Description", "IsActive", "IsDeleted", "CreatedAt", "CreatedBy" },
                values: new object[,]
                {
                    { 1, "001", "Net Tedarik", "Net Tedarik AŞ", true, false, createdDate, "system" }
                }
            );

            var warehouseAddress = new Address("Net Tedarik Depo", "Büyükdere Cd. Torun Center No:89 A Blok No: 74 A", "Şişli", "İstanbul");
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Name", "SupplierId", "Address", "IsActive", "IsDeleted", "CreatedAt", "CreatedBy" },
                values: new object[,]
                {
                    { 1, "Net Tedarik Depo 1", 1, JsonConvert.SerializeObject(warehouseAddress), true, false, createdDate, "system" },
                    { 2, "Net Tedarik Depo 2", 1, JsonConvert.SerializeObject(warehouseAddress), true, false, createdDate, "system" },
                    { 3, "Net Tedarik Depo 3", 1, JsonConvert.SerializeObject(warehouseAddress), true, false, createdDate, "system" }
                }
            );
            
            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "BrandId", "Code", "Name", "Description", "Price", "IsActive", "IsDeleted", "CreatedAt", "CreatedBy" },
                values: new object[,]
                {
                    { 1, 1, "001", "AirTies 001", "AirTies 001", 2000M, true, false, createdDate, "system" },
                    { 2, 1, "002", "AirTies 002", "AirTies 002", 2500M, true, false, createdDate, "system" },
                    { 3, 2, "003", "Zyxel 001", "Zyxel 001", 2000M, true, false, createdDate, "system" },
                    { 4, 2, "004", "Zyxel 002", "Zyxel 002", 2500M, true, false, createdDate, "system" },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Devices",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4 });
            
            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValues: new object[] { 1 });
            
            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "Id",
                keyValues: new object[] { 1 });
            
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2 });
        }

    }
}
