using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hrmSolution.Data.Migrations
{
    public partial class AddColumnAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "c_Avatar",
                table: "Tbl_NguoiDung",
                type: "varchar(max)",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Tbl_DanhMucTenQuyen",
                columns: new[] { "id", "c_Code", "c_IsDeleted", "c_NgayTao", "c_TenQuyen", "c_TrangThai" },
                values: new object[] { new Guid("5ef507d6-48ff-419d-bffa-07291275379e"), "ROLE001", 2, new DateTime(2021, 8, 1, 23, 1, 6, 933, DateTimeKind.Local).AddTicks(5270), "Admin", 2 });

            migrationBuilder.InsertData(
                table: "Tbl_NguoiDung",
                columns: new[] { "id", "c_Avatar", "c_Code", "c_Email", "c_HoVaTen", "c_IsDeleted", "c_MatKhau", "c_NgayTao", "c_SoDienThoai", "c_TrangThai", "Fk_DanhMucTenQuyen" },
                values: new object[] { new Guid("5721d0e5-3d84-4add-b584-2fda076e8f12"), "avatarpath", "ACC0001", "admin@gmail.com", "Tao La Admin", 2, "admin", new DateTime(2021, 8, 1, 23, 1, 6, 936, DateTimeKind.Local).AddTicks(821), "0974084154", 2, new Guid("5ef507d6-48ff-419d-bffa-07291275379e") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tbl_NguoiDung",
                keyColumn: "id",
                keyValue: new Guid("5721d0e5-3d84-4add-b584-2fda076e8f12"));

            migrationBuilder.DeleteData(
                table: "Tbl_DanhMucTenQuyen",
                keyColumn: "id",
                keyValue: new Guid("5ef507d6-48ff-419d-bffa-07291275379e"));

            migrationBuilder.DropColumn(
                name: "c_Avatar",
                table: "Tbl_NguoiDung");
        }
    }
}
