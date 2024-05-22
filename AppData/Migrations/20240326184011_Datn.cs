using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class Datn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anh",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DuongDan = table.Column<string>(type: "varchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anh", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ChucVu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MauSac",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMauSac = table.Column<string>(type: "nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauSac", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaSale = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "DateTime", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "DateTime", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSanPham = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false),
                    KhoiLuong = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeNumber = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "thanhtoans",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(225)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thanhtoans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(225)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaVoucher = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "DateTime", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "DateTime", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "DateTime", nullable: true),
                    GiaTriVoucher = table.Column<decimal>(type: "Decimal", nullable: false),
                    DieuKienMin = table.Column<decimal>(type: "Decimal", nullable: false),
                    DieuKienMax = table.Column<decimal>(type: "Decimal", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenNguoiDung = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "DateTime", nullable: true),
                    IdChucVu = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "varchar(225)", nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    QuanHuyen = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    TinhThanh = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    PhuongXa = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDung_ChucVu_IdChucVu",
                        column: x => x.IdChucVu,
                        principalTable: "ChucVu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SanPhamChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSP = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDSize = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDMauSac = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDThuongHieu = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaSPCT = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal", nullable: false),
                    LoaiSanPham = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QrImage = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPhamChiTiet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_MauSac_IDMauSac",
                        column: x => x.IDMauSac,
                        principalTable: "MauSac",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_SanPham_IDSP",
                        column: x => x.IDSP,
                        principalTable: "SanPham",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_Size_IDSize",
                        column: x => x.IDSize,
                        principalTable: "Size",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SanPhamChiTiet_ThuongHieu_IDThuongHieu",
                        column: x => x.IDThuongHieu,
                        principalTable: "ThuongHieu",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GioHang_NguoiDung_IDNguoiDung",
                        column: x => x.IDNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoucherChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherChiTiet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_VoucherChiTiet_NguoiDung_IDNguoiDung",
                        column: x => x.IDNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VoucherChiTiet_Voucher_IDVoucher",
                        column: x => x.IDVoucher,
                        principalTable: "Voucher",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AnhSanPham",
                columns: table => new
                {
                    IdSanPhamChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Idanh = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnhSanPham", x => new { x.Idanh, x.IdSanPhamChiTiet });
                    table.ForeignKey(
                        name: "FK_AnhSanPham_Anh_Idanh",
                        column: x => x.Idanh,
                        principalTable: "Anh",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnhSanPham_SanPhamChiTiet_IdSanPhamChiTiet",
                        column: x => x.IdSanPhamChiTiet,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnhSanPhamChiTiet",
                columns: table => new
                {
                    ListAnhID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SanPhamChiTietsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnhSanPhamChiTiet", x => new { x.ListAnhID, x.SanPhamChiTietsID });
                    table.ForeignKey(
                        name: "FK_AnhSanPhamChiTiet_Anh_ListAnhID",
                        column: x => x.ListAnhID,
                        principalTable: "Anh",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnhSanPhamChiTiet_SanPhamChiTiet_SanPhamChiTietsID",
                        column: x => x.SanPhamChiTietsID,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BinhLuan",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSpCt = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DanhGiaSanPham = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    HinhAnh = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    IDNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "DateTime", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinhLuan", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BinhLuan_NguoiDung_IDNguoiDung",
                        column: x => x.IDNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BinhLuan_SanPhamChiTiet_IDSpCt",
                        column: x => x.IDSpCt,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSale = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDSPCT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleChiTiet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SaleChiTiet_Sale_IDSale",
                        column: x => x.IDSale,
                        principalTable: "Sale",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SaleChiTiet_SanPhamChiTiet_IDSPCT",
                        column: x => x.IDSPCT,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDNguoiDung = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDVoucherChiTiet = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDHinhThucThanhToan = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MaHoaDon = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "DateTime", nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SDTKhachHang = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TongSoLuong = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<decimal>(type: "decimal", nullable: false),
                    TienShip = table.Column<decimal>(type: "decimal", nullable: false),
                    TienGiamGia = table.Column<decimal>(type: "decimal", nullable: false),
                    TongTien = table.Column<decimal>(type: "decimal", nullable: false),
                    NgayThanhToan = table.Column<DateTime>(type: "DateTime", nullable: true),
                    NgayNhanHang = table.Column<DateTime>(type: "DateTime", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HoaDon_NguoiDung_IDNguoiDung",
                        column: x => x.IDNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDon_thanhtoans_IDHinhThucThanhToan",
                        column: x => x.IDHinhThucThanhToan,
                        principalTable: "thanhtoans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDon_VoucherChiTiet_IDVoucherChiTiet",
                        column: x => x.IDVoucherChiTiet,
                        principalTable: "VoucherChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSPCT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDSaleCT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_GioHang_IDGioHang",
                        column: x => x.IDGioHang,
                        principalTable: "GioHang",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_SaleChiTiet_IDSaleCT",
                        column: x => x.IDSaleCT,
                        principalTable: "SaleChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_SanPhamChiTiet_IDSPCT",
                        column: x => x.IDSPCT,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiet",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IDSPCT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IDSaleCT = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_HoaDon_IDHoaDon",
                        column: x => x.IDHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_SaleChiTiet_IDSaleCT",
                        column: x => x.IDSaleCT,
                        principalTable: "SaleChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_SanPhamChiTiet_IDSPCT",
                        column: x => x.IDSPCT,
                        principalTable: "SanPhamChiTiet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnhSanPham_IdSanPhamChiTiet",
                table: "AnhSanPham",
                column: "IdSanPhamChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_AnhSanPhamChiTiet_SanPhamChiTietsID",
                table: "AnhSanPhamChiTiet",
                column: "SanPhamChiTietsID");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuan_IDNguoiDung",
                table: "BinhLuan",
                column: "IDNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_BinhLuan_IDSpCt",
                table: "BinhLuan",
                column: "IDSpCt");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_IDNguoiDung",
                table: "GioHang",
                column: "IDNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IDGioHang",
                table: "GioHangChiTiet",
                column: "IDGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IDSaleCT",
                table: "GioHangChiTiet",
                column: "IDSaleCT");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IDSPCT",
                table: "GioHangChiTiet",
                column: "IDSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IDHinhThucThanhToan",
                table: "HoaDon",
                column: "IDHinhThucThanhToan");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IDNguoiDung",
                table: "HoaDon",
                column: "IDNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IDVoucherChiTiet",
                table: "HoaDon",
                column: "IDVoucherChiTiet");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IDHoaDon",
                table: "HoaDonChiTiet",
                column: "IDHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IDSaleCT",
                table: "HoaDonChiTiet",
                column: "IDSaleCT");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IDSPCT",
                table: "HoaDonChiTiet",
                column: "IDSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDung_IdChucVu",
                table: "NguoiDung",
                column: "IdChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_SaleChiTiet_IDSale",
                table: "SaleChiTiet",
                column: "IDSale");

            migrationBuilder.CreateIndex(
                name: "IX_SaleChiTiet_IDSPCT",
                table: "SaleChiTiet",
                column: "IDSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_IDMauSac",
                table: "SanPhamChiTiet",
                column: "IDMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_IDSize",
                table: "SanPhamChiTiet",
                column: "IDSize");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_IDSP",
                table: "SanPhamChiTiet",
                column: "IDSP");

            migrationBuilder.CreateIndex(
                name: "IX_SanPhamChiTiet_IDThuongHieu",
                table: "SanPhamChiTiet",
                column: "IDThuongHieu");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherChiTiet_IDNguoiDung",
                table: "VoucherChiTiet",
                column: "IDNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherChiTiet_IDVoucher",
                table: "VoucherChiTiet",
                column: "IDVoucher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnhSanPham");

            migrationBuilder.DropTable(
                name: "AnhSanPhamChiTiet");

            migrationBuilder.DropTable(
                name: "BinhLuan");

            migrationBuilder.DropTable(
                name: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiet");

            migrationBuilder.DropTable(
                name: "Anh");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "SaleChiTiet");

            migrationBuilder.DropTable(
                name: "thanhtoans");

            migrationBuilder.DropTable(
                name: "VoucherChiTiet");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "SanPhamChiTiet");

            migrationBuilder.DropTable(
                name: "NguoiDung");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "MauSac");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "ChucVu");
        }
    }
}
