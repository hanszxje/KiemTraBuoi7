using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraBuoi7.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanLogin",
                table: "SinhViens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "Masv",
                keyValue: "0123456789",
                column: "CanLogin",
                value: false);

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "Masv",
                keyValue: "9876543210",
                column: "CanLogin",
                value: false);

            migrationBuilder.InsertData(
                table: "SinhViens",
                columns: new[] { "Masv", "CanLogin", "GioiTinh", "Hinh", "HoTen", "MaNganh", "NgaySinh" },
                values: new object[] { "admin", false, "Nam", "/Content/images/admin.jpg", "Quản trị viên", "CNTT", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SinhViens",
                keyColumn: "Masv",
                keyValue: "admin");

            migrationBuilder.DropColumn(
                name: "CanLogin",
                table: "SinhViens");
        }
    }
}
