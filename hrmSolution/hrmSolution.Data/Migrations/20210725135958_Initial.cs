using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hrmSolution.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_DanhMucChucNang",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    c_Code = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    c_TenChucNang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    c_NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    c_TrangThai = table.Column<int>(type: "int", nullable: false),
                    c_IsDeleted = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_DanhMucChucNang", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DanhMucManHinh",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    c_Code = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    c_TenManHinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    c_NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    c_TrangThai = table.Column<int>(type: "int", nullable: false),
                    c_IsDeleted = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_DanhMucManHinh", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DanhMucTenQuyen",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    c_Code = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    c_TenQuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    c_NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    c_TrangThai = table.Column<int>(type: "int", nullable: false),
                    c_IsDeleted = table.Column<int>(type: "int", nullable: false, defaultValue: 2)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_DanhMucTenQuyen", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ChiTietQuyenTheoManHinh",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    c_NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    c_TrangThai = table.Column<int>(type: "int", nullable: false),
                    c_IsDeleted = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    Fk_DanhMucTenQuyen = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fk_DanhMucChucNang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fk_DanhMucManHinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ChiTietQuyenTheoManHinh", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tbl_ChiTietQuyenTheoManHinh_Tbl_DanhMucChucNang_Fk_DanhMucChucNang",
                        column: x => x.Fk_DanhMucChucNang,
                        principalTable: "Tbl_DanhMucChucNang",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_ChiTietQuyenTheoManHinh_Tbl_DanhMucManHinh_Fk_DanhMucManHinh",
                        column: x => x.Fk_DanhMucManHinh,
                        principalTable: "Tbl_DanhMucManHinh",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tbl_ChiTietQuyenTheoManHinh_Tbl_DanhMucTenQuyen_Fk_DanhMucTenQuyen",
                        column: x => x.Fk_DanhMucTenQuyen,
                        principalTable: "Tbl_DanhMucTenQuyen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_NguoiDung",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    c_Code = table.Column<string>(type: "varchar(900)", unicode: false, nullable: false),
                    c_Email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    c_MatKhau = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    c_HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    c_SoDienThoai = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    c_NgayTao = table.Column<DateTime>(type: "datetime", nullable: false),
                    c_TrangThai = table.Column<int>(type: "int", nullable: false),
                    c_IsDeleted = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    Fk_DanhMucTenQuyen = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_NguoiDung", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tbl_NguoiDung_Tbl_DanhMucTenQuyen_Fk_DanhMucTenQuyen",
                        column: x => x.Fk_DanhMucTenQuyen,
                        principalTable: "Tbl_DanhMucTenQuyen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ChiTietQuyenTheoManHinh_Fk_DanhMucChucNang",
                table: "Tbl_ChiTietQuyenTheoManHinh",
                column: "Fk_DanhMucChucNang");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ChiTietQuyenTheoManHinh_Fk_DanhMucManHinh",
                table: "Tbl_ChiTietQuyenTheoManHinh",
                column: "Fk_DanhMucManHinh");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ChiTietQuyenTheoManHinh_Fk_DanhMucTenQuyen",
                table: "Tbl_ChiTietQuyenTheoManHinh",
                column: "Fk_DanhMucTenQuyen");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_DanhMucChucNang_c_Code",
                table: "Tbl_DanhMucChucNang",
                column: "c_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_DanhMucManHinh_c_Code",
                table: "Tbl_DanhMucManHinh",
                column: "c_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_DanhMucTenQuyen_c_Code",
                table: "Tbl_DanhMucTenQuyen",
                column: "c_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NguoiDung_c_Code",
                table: "Tbl_NguoiDung",
                column: "c_Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_NguoiDung_Fk_DanhMucTenQuyen",
                table: "Tbl_NguoiDung",
                column: "Fk_DanhMucTenQuyen");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_ChiTietQuyenTheoManHinh");

            migrationBuilder.DropTable(
                name: "Tbl_NguoiDung");

            migrationBuilder.DropTable(
                name: "Tbl_DanhMucChucNang");

            migrationBuilder.DropTable(
                name: "Tbl_DanhMucManHinh");

            migrationBuilder.DropTable(
                name: "Tbl_DanhMucTenQuyen");
        }
    }
}
