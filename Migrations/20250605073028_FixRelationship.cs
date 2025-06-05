using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KiemTraBuoi7.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhViens_NganhHocs_MaNganhNavigationMaNganh",
                table: "SinhViens");

            migrationBuilder.DropIndex(
                name: "IX_SinhViens_MaNganhNavigationMaNganh",
                table: "SinhViens");

            migrationBuilder.DropColumn(
                name: "MaNganhNavigationMaNganh",
                table: "SinhViens");

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaNganh",
                table: "SinhViens",
                column: "MaNganh");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhViens_NganhHocs_MaNganh",
                table: "SinhViens",
                column: "MaNganh",
                principalTable: "NganhHocs",
                principalColumn: "MaNganh",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhViens_NganhHocs_MaNganh",
                table: "SinhViens");

            migrationBuilder.DropIndex(
                name: "IX_SinhViens_MaNganh",
                table: "SinhViens");

            migrationBuilder.AddColumn<string>(
                name: "MaNganhNavigationMaNganh",
                table: "SinhViens",
                type: "nvarchar(4)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "Masv",
                keyValue: "0123456789",
                column: "MaNganhNavigationMaNganh",
                value: null);

            migrationBuilder.UpdateData(
                table: "SinhViens",
                keyColumn: "Masv",
                keyValue: "9876543210",
                column: "MaNganhNavigationMaNganh",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_SinhViens_MaNganhNavigationMaNganh",
                table: "SinhViens",
                column: "MaNganhNavigationMaNganh");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhViens_NganhHocs_MaNganhNavigationMaNganh",
                table: "SinhViens",
                column: "MaNganhNavigationMaNganh",
                principalTable: "NganhHocs",
                principalColumn: "MaNganh");
        }
    }
}
