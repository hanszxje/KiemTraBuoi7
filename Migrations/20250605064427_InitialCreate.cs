using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KiemTraBuoi7.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HocPhans",
                columns: table => new
                {
                    Mahp = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    TenHp = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SoTinChi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocPhans", x => x.Mahp);
                });

            migrationBuilder.CreateTable(
                name: "NganhHocs",
                columns: table => new
                {
                    MaNganh = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TenNganh = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NganhHocs", x => x.MaNganh);
                });

            migrationBuilder.CreateTable(
                name: "SinhViens",
                columns: table => new
                {
                    Masv = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaNganh = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    MaNganhNavigationMaNganh = table.Column<string>(type: "nvarchar(4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhViens", x => x.Masv);
                    table.ForeignKey(
                        name: "FK_SinhViens_NganhHocs_MaNganhNavigationMaNganh",
                        column: x => x.MaNganhNavigationMaNganh,
                        principalTable: "NganhHocs",
                        principalColumn: "MaNganh");
                });

            migrationBuilder.CreateTable(
                name: "DangKies",
                columns: table => new
                {
                    Madk = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayDk = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Masv = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKies", x => x.Madk);
                    table.ForeignKey(
                        name: "FK_DangKies_SinhViens_Masv",
                        column: x => x.Masv,
                        principalTable: "SinhViens",
                        principalColumn: "Masv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDangKies",
                columns: table => new
                {
                    Mahp = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Madk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDangKies", x => new { x.Madk, x.Mahp });
                    table.ForeignKey(
                        name: "FK_ChiTietDangKies_DangKies_Madk",
                        column: x => x.Madk,
                        principalTable: "DangKies",
                        principalColumn: "Madk",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietDangKies_HocPhans_Mahp",
                        column: x => x.Mahp,
                        principalTable: "HocPhans",
                        principalColumn: "Mahp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HocPhans",
                columns: new[] { "Mahp", "SoTinChi", "TenHp" },
                values: new object[,]
                {
                    { "CNTT01", 3, "Lập trình C" },
                    { "CNTT02", 2, "Cơ sở dữ liệu" },
                    { "QTKD01", 2, "Khởi sự và thương kệ" },
                    { "QTKD02", 3, "Xác suất thống kê" }
                });

            migrationBuilder.InsertData(
                table: "NganhHocs",
                columns: new[] { "MaNganh", "TenNganh" },
                values: new object[,]
                {
                    { "CNTT", "Công nghệ thông tin" },
                    { "QTKD", "Quản trị kinh doanh" }
                });

            migrationBuilder.InsertData(
                table: "SinhViens",
                columns: new[] { "Masv", "GioiTinh", "Hinh", "HoTen", "MaNganh", "MaNganhNavigationMaNganh", "NgaySinh" },
                values: new object[,]
                {
                    { "0123456789", "Nam", "/Content/images/sv1.jpg", "Nguyễn Văn A", "CNTT", null, new DateTime(2000, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "9876543210", "Nữ", "/Content/images/sv2.jpg", "Nguyễn Thị B", "QTKD", null, new DateTime(2000, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "DangKies",
                columns: new[] { "Madk", "Masv", "NgayDk" },
                values: new object[,]
                {
                    { 1, "0123456789", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "9876543210", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "ChiTietDangKies",
                columns: new[] { "Madk", "Mahp" },
                values: new object[,]
                {
                    { 1, "CNTT01" },
                    { 1, "CNTT02" },
                    { 2, "QTKD01" },
                    { 2, "QTKD02" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDangKies_Mahp",
                table: "ChiTietDangKies",
                column: "Mahp");

            migrationBuilder.CreateIndex(
                name: "IX_DangKies_Masv",
                table: "DangKies",
                column: "Masv");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaNganhNavigationMaNganh",
                table: "SinhViens",
                column: "MaNganhNavigationMaNganh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDangKies");

            migrationBuilder.DropTable(
                name: "DangKies");

            migrationBuilder.DropTable(
                name: "HocPhans");

            migrationBuilder.DropTable(
                name: "SinhViens");

            migrationBuilder.DropTable(
                name: "NganhHocs");
        }
    }
}
