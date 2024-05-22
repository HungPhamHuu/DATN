using AppData.DB_Context;
using AppData.Entities.Models;
using AppData.Entities.ViewModels;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Microsoft.Azure.Cosmos.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories.BillDetails
{
    public class XuatHoaDon
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public string InHoaDon(Guid idhd)
        {
            try
            {
                HoaDon hd = HoadonTheoID(idhd);
                List<HoaDonChiTietViewModel> lsthdct = HoadonchiTietTheoID(idhd);

                Document doc = new Document();
                doc.PageInfo.Margin.Top = 50;
                doc.PageInfo.Margin.Bottom = 50;
                doc.PageInfo.Margin.Left = 50;
                doc.PageInfo.Margin.Right = 50;
                // Add a page to the document
                Page page = doc.Pages.Add();

                // Create a TextFragment object
                TextFragment textFragment = new TextFragment("6S SNEAKER");
                textFragment.HorizontalAlignment = HorizontalAlignment.Center;
                textFragment.TextState.FontSize = 36;

                TextFragment textFragment1 = new TextFragment("");

                // Add the text fragment to the paragraphs collection of the page
                page.Paragraphs.Add(textFragment);
                page.Paragraphs.Add(textFragment1);
                page.Paragraphs.Add(textFragment1);
                page.Paragraphs.Add(textFragment1);


                Table tblthongtin = new Table();
                tblthongtin.ColumnWidths = "300 300";
                page.Paragraphs.Add(tblthongtin);
                // bảng 3 dòng 2 cột
                for (int i = 0; i < 3; i++)
                {
                    Row row = tblthongtin.Rows.Add();

                    // Thêm dữ liệu vào hàng (row) cụ thể
                    for (int j = 0; j < 2; j++)
                    {
                        Cell cell = row.Cells.Add("");
                        if (i == 0 && j == 0)
                        {
                            cell.Paragraphs.Add(new TextFragment("Họ và tên: " + hd.TenKhachHang));
                        }
                        else if (i == 0 && j == 1)
                        {
                            cell.Paragraphs.Add(new TextFragment("Mã hóa đơn: " + hd.MaHoaDon));
                        }
                        else if (i == 1 && j == 0)
                        {
                            cell.Paragraphs.Add(new TextFragment("SĐT: " + hd.SDTKhachHang));
                        }
                        else if (i == 1 && j == 1)
                        {
                            cell.Paragraphs.Add(new TextFragment("Ngày tạo: " + hd.NgayTao));
                        }
                        else if (i == 2 && j == 0)
                        {
                            cell.Paragraphs.Add(new TextFragment("Địa chỉ: " + hd.DiaChi));
                        }
                        cell.Border = new BorderInfo(BorderSide.None, 1f);
                    }
                }
                page.Paragraphs.Add(textFragment1);
                page.Paragraphs.Add(textFragment1);
                page.Paragraphs.Add(textFragment1);



                Table tbl1 = new Table();
                tbl1.ColumnWidths = "30 170 100 100 100"; // Đặt chiều rộng của từng cột
                page.Paragraphs.Add(tbl1);

                for (int i = 0; i < 1; i++)
                {
                    Row row = tbl1.Rows.Add();
                    for (int j = 0; j < 5; j++)
                    {
                        Cell cell = row.Cells.Add("");
                        if (i == 0 && j == 0)
                        {
                            cell.Paragraphs.Add(new TextFragment("STT"));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        else if (i == 0 && j == 1)
                        {
                            cell.Paragraphs.Add(new TextFragment("Tên sản phẩm"));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                      
                        else if (i == 0 && j == 2)
                        {
                            cell.Paragraphs.Add(new TextFragment("Số lượng"));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        else if (i == 0 && j == 3)
                        {
                            cell.Paragraphs.Add(new TextFragment("Đơn giá"));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        else if (i == 0 && j == 4)
                        {
                            cell.Paragraphs.Add(new TextFragment("Thành tiền"));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        cell.Border = new BorderInfo(BorderSide.All, 1f);
                    }
                }

                var SoLuongDong = lsthdct.Count();
                // Bảng Thông tin hóa đơn
                Table table = new Table();
                table.ColumnWidths = "30 170 100 100 100"; // Đặt chiều rộng của từng cột
                page.Paragraphs.Add(table);


                // Thêm 5 dòng vào bảng
                for (int i = 0; i < SoLuongDong; i++)
                {
                    var gia = lsthdct[i].Gia.ToString("#,##0");
                    var thanhtien1 = (lsthdct[i].Gia * lsthdct[i].SoLuong).ToString("#,##0");

                    Row row = table.Rows.Add();
                    var index = i;
                    // Thêm 5 ô vào từng dòng
                    for (int j = 0; j < 5; j++)
                    {
                        Cell cell = row.Cells.Add("");
                        if (i == index && j == 0)
                        {
                            cell.Paragraphs.Add(new TextFragment(Convert.ToString(i + 1)));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        if (i == index && j == 1)
                        {
                            if (lsthdct[i].IDSPCT != null)
                            {
                                cell.Paragraphs.Add(new TextFragment(Convert.ToString(lsthdct[i].TenSP + "( " + lsthdct[i].Size + ", " + lsthdct[i].MauSac + " )")));
                                cell.Alignment = HorizontalAlignment.Center;
                            }
                            else
                            {
                                cell.Paragraphs.Add(new TextFragment(Convert.ToString(lsthdct[i].TenSP + "( " + lsthdct[i].Size + ", " + lsthdct[i].MauSac + " )")));
                                cell.Alignment = HorizontalAlignment.Center;
                            }
                        }
                        if (i == index && j == 2)
                        {
                            cell.Paragraphs.Add(new TextFragment(Convert.ToString(lsthdct[i].SoLuong)));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        if (i == index && j == 3)
                        {
                            cell.Paragraphs.Add(new TextFragment(gia + " đ"));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        if (i == index && j == 4)
                        {
                            cell.Paragraphs.Add(new TextFragment(thanhtien1 + " đ"));
                            cell.Alignment = HorizontalAlignment.Center;
                        }
                        cell.Border = new BorderInfo(BorderSide.All, 1f);
                    }
                }

                page.Paragraphs.Add(textFragment1);
                page.Paragraphs.Add(textFragment1);
                page.Paragraphs.Add(textFragment1);

                var tongtien = hd.TongTien.ToString("#,##0");
                var thanhtien = hd.ThanhTien.ToString("#,##0");
                var tienship = hd.TienShip.ToString("#,##0");
                var tiengiamgia = hd.TienGiamGia.ToString("#,##0");

                Table tbl2 = new Table();
                tbl2.ColumnWidths = "350 200"; // Đặt chiều rộng của từng cột
                page.Paragraphs.Add(tbl2);

                for (int i = 0; i < 4; i++)
                {
                    Row row = tbl2.Rows.Add();

                    // Thêm dữ liệu vào hàng (row) cụ thể
                    for (int j = 0; j < 2; j++)
                    {
                        Cell cell = row.Cells.Add("");
                        if (i == 0 && j == 1)
                        {
                            cell.Paragraphs.Add(new TextFragment("Tổng tiền: " + thanhtien + " đ"));
                        }
                        else if (i == 1 && j == 1)
                        {
                            cell.Paragraphs.Add(new TextFragment("Tiền ship: " + tienship + " đ"));
                        }
                        else if (i == 2 && j == 1)
                        {
                            cell.Paragraphs.Add(new TextFragment("Tiền giảm giá: " + tiengiamgia + " đ"));
                        }
                        else if (i == 3 && j == 1)
                        {
                            cell.Paragraphs.Add(new TextFragment("Thanh toán: " + tongtien + " đ"));
                        }
                        cell.Border = new BorderInfo(BorderSide.None, 1f);
                    }
                }

                // Save the PDF to a file
                string filePath = @"N:\PhieuHoaDon\"+ hd.MaHoaDon +".pdf";
                doc.Save(filePath);

                return "In hóa đơn thành công.";

            }
            catch (Exception ex)
            {
                return "In hóa đơn thất bại." + ex.Message;
            }
        }

        public HoaDon HoadonTheoID(Guid idhd)
        {
            var url = $"https://localhost:7265/api/HoaDon/id?id={idhd}";
            var response = _httpClient.GetAsync(url).Result;
            string apiData = response.Content.ReadAsStringAsync().Result;
            HoaDon hd = JsonConvert.DeserializeObject<HoaDon>(apiData);
            return hd;
        }

        public List<HoaDonChiTietViewModel> HoadonchiTietTheoID(Guid idhd)
        {
            var url = $"https://localhost:7265/api/HoaDonCT/DanhSachHDChiTietTheoIDhd?idhd={idhd}";
            var response = _httpClient.GetAsync(url).Result;
            string apiData = response.Content.ReadAsStringAsync().Result;
            List<HoaDonChiTietViewModel> hdct = JsonConvert.DeserializeObject<List<HoaDonChiTietViewModel>>(apiData);
            return hdct;
        }
    }
}
