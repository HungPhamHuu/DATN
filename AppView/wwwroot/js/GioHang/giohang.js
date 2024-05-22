
$(document).ready(function () {
    // Sự kiện hiển thị suggest khi click vào ô input
    $('#magiamgia').on('focus', function () {
        suggestVoucher();
    });

    // Sự kiện ẩn suggest khi click ra ngoài ô input
    $(document).on('click', function (event) {
        const target = $(event.target);
        // Kiểm tra nếu không phải là ô input hoặc các phần tử của suggestList
        if (!target.is('#magiamgia') && !target.closest('#suggestionListVoucher').length) {
            $("#suggestionListVoucher").empty();
        }
    });
});

function ChonThanhPho() {
    var selectElement = document.getElementById("provinceList");
    var selectedOption = selectElement.options[selectElement.selectedIndex];

    if (selectedOption.value !== "") {
        var dataUrl = selectedOption.getAttribute("data-url");

        if (dataUrl) {
            $.ajax({
                url: dataUrl,
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(selectedOption.value), // Chuyển đổi sang chuỗi trước khi gửi
                success: function (data) {
                    updateDistrictList(data);
                    layDuLieuTuQuanHuyen();
                },
                error: function () {
                    console.log("Có lỗi xảy ra");
                }
            });
        } else {
            console.log("Không lấy được ID Tỉnh/Thành phố");
        }
    } else {
        console.log("Vui lòng chọn một Tỉnh/Thành phố");
    }
};

function ChonHuyen() {
    var selectElement = document.getElementById("districtList");
    var selectedOption = selectElement.options[selectElement.selectedIndex];

    if (selectedOption.value !== "") {
        var dataUrl = selectedOption.getAttribute("data-url");
        if (dataUrl) {
            $.ajax({
                url: dataUrl,
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(selectedOption.value),
                success: function (data) {
                    //alert("Lấy ID thành công: " + selectedId);
                    CapNhatXaPhuong(data)
                    layDuLieuTuXaPhuong();
                },
                error: function () {
                    console.log("Có lỗi xảy ra");
                }
            });
        } else {
            console.log("Không lấy được ID Quận huyện");
        }
    } else {
        console.log("Vui lòng chọn một Quận huyện.");
    }
};

function ChonPhuong() {
    var selectElement = document.getElementById("wardList");
    var selectedOption = selectElement.options[selectElement.selectedIndex];
    if (selectedOption.value !== "") {
        var dataUrl = selectedOption.getAttribute("data-url");
        if (dataUrl) {
            $.ajax({
                url: dataUrl,
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(selectedOption.value),
                success: function (data) {
                    //alert("Lấy ID thành công: " + selectedId);
                    layDuLieuTuXaPhuong();
                    hienthitienship();
                },
                error: function () {
                    console.log("Có lỗi xảy ra");
                }
            });

        } else {
            console.log("Không lấy được ID Phường Xã");
        }
    } else {
        console.log("Vui lòng chọn một Phường Xã");
    }
}
function SuDungVoucher() {
    var magiamgia = document.getElementById("magiamgia");
    if (magiamgia.value == null || magiamgia.value == '' || magiamgia.value == "") {
        alert("Không có voucher")
    }
};

function updateDistrictList(data) {
    // Xóa tất cả các option hiện tại trong dropdown "district"

    $("#districtList").empty();
    $("#districtList").append('<option value="">Chọn Quận/Huyện </option>');
    if (data != null) {
        for (var i = 0; i < data.length; i++) {
            $("#districtList").append('<option value="' + data[i].districtId + '" data-url="' + '/GioHang/NhanIDHuyen?IdQuanHuyen=' + data[i].districtId + '">' + data[i].districtName + '</option>');
        }
    }
}
function layDuLieuTuQuanHuyen() {
    $.ajax({
        type: 'GET',
        url: '/GioHang/LayQuanHuyen', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            // Dữ liệu được trả về từ controller
            var thongTinQuanHuyen = result.thongTinQuanHuyen;
            //updateDistrictDropdown(thongTinQuanHuyen)
            console.log(thongTinQuanHuyen);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
};
function layDuLieuTuXaPhuong() {
    $.ajax({
        type: 'GET',
        url: '/GioHang/GetAllXaPhuong',
        success: function (result) {
            var ThongTinXaPhuong = result.thongTinXaPhuong;
            console.log(ThongTinXaPhuong);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
};
function CapNhatXaPhuong(data) {
    // Xóa tất cả các option hiện tại trong dropdown "district"

    $("#wardList").empty();
    $("#wardList").append('<option value="">Chọn Xã/Phường </option>');
    if (data != null) {
        for (var i = 0; i < data.length; i++) {
            $("#wardList").append('<option value="' + data[i].wardCode + '" data-url="' + '/GioHang/TinhTienShip?WardCode=' + data[i].wardCode + '">' + data[i].wardName + '</option>');
        }
    }
};

function hienthitienship() {
    $.ajax({
        type: 'GET',
        url: '/GioHang/HienThiTienShip', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            // Dữ liệu được trả về từ controller
            var tienship = result.tienship;
            var hienthitiienship = document.getElementById("tienship");
            var number2 = 0;
            if (tienship == null) {
                number2 = 0;
            } else {
                var formattedNumber = tienship.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                hienthitiienship.innerText = formattedNumber;
                number2 = parseFloat(tienship)
            }
            if (hienthitiienship == '') {
                jQuery("#btnChon").attr("style", "pointer-events: none;");
            }
            else {
                const tongtien = document.getElementById("tongtien").innerText;
                const number = parseFloat(tongtien.replace(/[^\d]/g, ''));
                var tiemgiamgia = document.getElementById("tiemgiamgia").innerText;
                var tiengiam;
                if (tiemgiamgia == "" || tiemgiamgia == undefined || tiemgiamgia == null) {
                    tiemgiamgia = 0;
                    tiengiam = 0;
                } else {
                    tiengiam = parseFloat(tiemgiamgia.replace(/[^\d]/g, ''));
                }

                var number1 = parseFloat(number);
                var tiemgiamgia1 = parseFloat(tiengiam);
                var sum = number1 + number2 - tiemgiamgia1;
                var formattedNumber1 = sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                document.getElementById("thanhtien").innerText = formattedNumber1;
                console.log(formattedNumber);
                console.log(formattedNumber1);
                jQuery("#btnChon").attr("style", "pointer-events: auto;");
            }

        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};
function ApDungVoucher(event) {
    event.preventDefault();
    var inPutVoucher = document.getElementById("magiamgia").value;
    if (inPutVoucher == "") {
        const tongtien = document.getElementById("tongtien").innerText;
        document.getElementById("phantramgiamgia").innerText = "";
        const number = parseFloat(tongtien.replace(/[^\d]/g, ''));
        const tiengiamgia = 0;
        document.getElementById("tiemgiamgia").innerText = "";
        const thanhtien = number - tiengiamgia;
        document.getElementById("thanhtien").innerText = thanhtien.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
        document.getElementById("errorvoucher").innerText = "";
    } else {
        $.ajax({
            url: '/KhachHang/GioHang/TimKiemVoucher',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(inPutVoucher),
            success: function (result) {
                if (result == "" || result == null || result == undefined) {
                    document.getElementById("errorvoucher").innerText = "Không tìm thấy voucher.";
                    return;
                } else {
                    const ngaybatdau = new Date(result.ngayBatDau);
                    const dateBatDau = ngaybatdau.getFullYear() + '-' + (ngaybatdau.getMonth() + 1) + '-' + ngaybatdau.getDate();
                    console.log(dateBatDau);

                    const today = new Date();
                    const dateToday = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
                    console.log(dateToday);

                    const ngayketthuc = new Date(result.ngayKetThuc);
                    const dateKetThuc = ngayketthuc.getFullYear() + '-' + (ngayketthuc.getMonth() + 1) + '-' + ngayketthuc.getDate();
                    console.log(dateKetThuc);



                    if (today < ngaybatdau ) {
                        document.getElementById("errorvoucher").innerText = "Không sử dụng được.";
                        return;
                    } else if (dateToday > dateKetThuc) {
                        document.getElementById("errorvoucher").innerText = "Hết hạn sử dụng voucher.";
                        return;
                    } else if (result.trangThai != 1) {
                        document.getElementById("errorvoucher").innerText = "Không sử dụng được.";
                        return;
                    } else {
                        const tongtien = document.getElementById("tongtien").innerText;
                        var giatrivoucher = result.giaTriVoucher + "%";
                        $("#phantramgiamgia").text(giatrivoucher);
                        const number = parseFloat(tongtien.replace(/[^\d]/g, ''));
                        const tiengiamgia = number * result.giaTriVoucher / 100;
                        const tiengiamgiahoanthien = tiengiamgia.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                        document.getElementById("tiemgiamgia").innerText = tiengiamgiahoanthien;
                        const thanhtien = number - tiengiamgia;
                        document.getElementById("thanhtien").innerText = thanhtien.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                        document.getElementById("idvoucher").innerText = result.id;
                        document.getElementById("errorvoucher").innerText = "";
                    }
                }
                console.log(result);
            },
            error: function (xhr, status, error) {
                alert('Lỗi gọi api', error);
            }
        });
    }

};
function KiemTraTruocKhiThanhToan() {
    var ketqua = [];
    ketqua.splice(0, ketqua.length);

    var name = validateName();
    ketqua.push(name);
    var diachi = validatediachi1();
    ketqua.push(diachi);
    var email = validateEmail();
    ketqua.push(email);
    var sdt = validateSDT();
    ketqua.push(sdt);
    console.log(ketqua);
    for (var i = 0; i < ketqua.length; i++) {
        if (ketqua[i] == false) {
            return false;
        }
    }
    return true;
};

function validatediachi1() {
    var diachi = document.getElementById("diachi");
    var errorDiaChi = document.getElementById("errorDiaChi");
    // Kiểm tra điều kiện nếu tên rỗng
    if (diachi.value.trim() === '') {
        errorDiaChi.innerText = "Địa chỉ không được để trống";
    } else {
        errorDiaChi.textContent = ""; // Xóa thông báo lỗi nếu tên hợp lệ
    }
};

function DanhSachHinhThucJson() {
    $.ajax({
        type: 'GET',
        url: '/Admin/HinhThucThanhToan/DanhSachHinhThucJson', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            console.log(result);

            $("#HinhThucThanhToan").empty();
            var content = '';
            content += '<h3>Hình thức thanh toán</h3>';
            content += '<label>';
            if (result.length > 0) {
                for (var i = 0; i < result.length; i++) {
                    if (result[i].hinhThucThanhToan == "Thanh toán khi nhận hàng") {
                        idthanhtoan = result[i].id;
                        content += '<span class="paymant-method__item-cutom-checkbox custom-radio">';
                        content += '<input   type="radio" id="payment-COD" name="payment-method" value="' + result[i].id + '" checked/>';
                        content += '</span>';
                        content += '<span class="payment-method__item-name"> Thanh toán khi nhận hàng</span>';
                        content += '<br>';
                    }
                    // Thanh toán ATM Nội địa
                    if (result[i].hinhThucThanhToan == "Thanh toán online") {
                        content += '<span class="paymant-method__item-cutom-checkbox custom-radio">';
                        content += '<input  type="radio" style="margin-left: 20px;" id="bankcode_Vnbank" name="payment-method" value="' + result[i].id + '" />';
                        content += '</span>';
                        content += '<span class="payment-method__item-name"> Thanh toán online</span>';
                        content += '<br>';
                    }
                }
            }
            content += '</label>';
            $("#HinhThucThanhToan").append(content);
            const btnThanhToan = document.getElementById('btnThanhToan');
            const btn_online = document.getElementById('btn_online');
            const radioButtons = document.querySelectorAll('label input[type="radio"]');
            radioButtons.forEach(radioButton => {
                radioButton.addEventListener('click', function () {
                    idthanhtoan = this.value;
                    if (this.id === 'bankcode_Vnbank') {
                        ThanhToanOnline();
                        btnThanhToan.style.display = 'none';
                        btn_online.style.display = 'block';
                    } else {
                        btnThanhToan.style.display = 'block';
                        btn_online.style.display = 'none';
                    }
                });
            });
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
}

function TaoHoaDon() {
    var check = KiemTraTruocKhiThanhToan();
    const tienship1 = document.getElementById("tienship").innerText;
    if (check == false) {
        alert("Nhập thông tin đầy đủ để nhận sản phẩm.");
        return;
    } else if (tienship1 == null || tienship1 == "" || tienship1 == undefined) {
        alert("Mời bạn chọn địa điểm ship hàng.");
        return;
    }
    else {
        var tenkh = document.getElementById("name").value;
        var idvoucher = document.getElementById("idvoucher").innerText;
        var sdtkh = document.getElementById("sdt").value;
        var diachi = document.getElementById("diachi").value;
        var thanhtien = parseFloat(document.getElementById("tongtien").innerText.replace(/[^\d]/g, ''));
        var tienship = parseFloat(document.getElementById("tienship").innerText.replace(/[^\d]/g, ''));
        var tiengiamgia;
        if (document.getElementById("tiemgiamgia").innerText == "" || document.getElementById("tiemgiamgia").innerText == null || document.getElementById("tiemgiamgia").innerText == undefined) {
            tiengiamgia = 0;
        } else {
            tiengiamgia = parseFloat(document.getElementById("tiemgiamgia").innerText.replace(/[^\d]/g, ''));
        }
        var ghichu = document.getElementById("w3review").value;
        if (idvoucher == "") {
            idvoucher = null;
        } 
        var data = {
            IDHinhThucThanhToan: idthanhtoan,
            IDVoucherChiTiet: idvoucher,
            TenKhachHang: tenkh,
            SDTKhachHang: sdtkh,
            DiaChi: diachi,
            TongSoLuong: 0,
            ThanhTien: thanhtien,
            TienShip: tienship,
            TienGiamGia: tiengiamgia,
            GhiChu: ghichu
        };
        jQuery.ajax({
            type: 'POST',
            url: '/KhachHang/GioHang/TaoHoaDon',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (response) {
                // Handle success response, if needed
                if (response.message == "Đơn hàng đang chờ xử lý") {
                    alert('Đặt hàng thành công.');
                    window.location.href = '/KhachHang/HoaDonKH/DanhSachHoaDonTheoNguoiDungKH';
                } else {
                    alert('Đặt hàng thất bại.');
                    hienthitienship();
                    LoadGioHangChiTiet();
                    ViewSoLuong();
                }
            },
            error: function (xhr, status, error) {
                alert('Đặt hàng thất bại.');
                hienthitienship();
                LoadGioHangChiTiet();
            }
        });

    }
};
function ThanhToanOnline() {
    var check = KiemTraTruocKhiThanhToan();
    const tienship1 = document.getElementById("tienship").innerText;
    if (check == false) {
        alert("Nhập thông tin đầy đủ để nhận sản phẩm.");
        return;
    } else if (tienship1 == null || tienship1 == "" || tienship1 == undefined) {
        alert("Mời bạn chọn địa điểm ship hàng.");
        return;
    } else {
        var tenkh = document.getElementById("name").value;
        var idvoucher = document.getElementById("errorvoucher").innerText;
        var sdtkh = document.getElementById("sdt").value;
        var diachi = document.getElementById("diachi").value;
        var thanhtien = parseFloat(document.getElementById("tongtien").innerText.replace(/[^\d]/g, ''));
        var tienship = parseFloat(document.getElementById("tienship").innerText.replace(/[^\d]/g, ''));
        var tiengiamgia;
        if (document.getElementById("tiemgiamgia").innerText == "" || document.getElementById("tiemgiamgia").innerText == null || document.getElementById("tiemgiamgia").innerText == undefined) {
            tiengiamgia = 0;
        } else {
            tiengiamgia = parseFloat(document.getElementById("tiemgiamgia").innerText.replace(/[^\d]/g, ''));
        }
        var ghichu = document.getElementById("w3review").value;
        if (idvoucher == "") {
            idvoucher = null;
        } else {
            idvoucher == $("#errorvoucher").text();
        }
        var data = {
            IDHinhThucThanhToan: idthanhtoan,
            IDVoucherChiTiet: idvoucher,
            TenKhachHang: tenkh,
            SDTKhachHang: sdtkh,
            DiaChi: diachi,
            TongSoLuong: 0,
            ThanhTien: thanhtien,
            TienShip: tienship,
            TienGiamGia: tiengiamgia,
            GhiChu: ghichu
        };
        jQuery.ajax({
            type: 'POST',
            url: '/KhachHang/GioHang/NhanHoaDon',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (response) {
            },
            error: function (xhr, status, error) {
                alert('Đặt hàng thất bại.');
                hienthitienship();
                LoadGioHangChiTiet();
            }
        });
    }
};
function LoadGioHangChiTiet() {
    $.ajax({
        type: 'GET',
        url: '/KhachHang/GioHang/GioHangChiTietJson',
        success: function (result) {

            $("#danhSachsanpham").empty();
            var sum = 0;
            if (result.length > 0) {
                for (var i = 0; i < result.length; i++) {
                    var TienSP = result[i].gia.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    var giaGoc = result[i].giaGoc.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    var ThanhTien = result[i].thanhTien.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    var data = {
                        TenSP: result[i].tenSP,
                        MauSac: result[i].mauSac,
                        LoaiSanPham: result[i].loaiSanPham,
                        TenThuongHieu: result[i].thuongHieu,
                    }
                    var droplstId = result[i].id.toString();
                    var content = '';
                    content += '<div class="product-cart d-flex">';
                    content += '<div class="one-forth">';
                    if (result[i].duongDanAnh == null || result[i].duongDanAnh == undefined || result[i].duongDanAnh == "") {
                        content += '<a href="/KhachHang/TrangChu/SanPhamChiTiet?idspct=' + result[i].idspct + '" style="text-decoration:none;"><img class="product-img" src="https://congtygiaphat104.com/template/Default/img/no.png" /></a>';
                    } else {
                        content += '<a href="/KhachHang/TrangChu/SanPhamChiTiet?idspct=' + result[i].idspct + '" style="text-decoration:none;"><img class="product-img" src="/Admin/Anh/DisplayImage?DuongDan=' + result[i].duongDanAnh + '" /></a>';
                    }
                    content += '<div class="display-tc">';
                    content += '<h3 id="tensanpham"><a href="/KhachHang/TrangChu/SanPhamChiTiet?idspct=' + result[i].idspct + '" style="text-decoration:none;">' + result[i].tenSP + '</a></h3>';
                    content += '</div>';
                    content += '</div>';
                    content += '<div class="one-eight" style="margin-left: 1%;">';
                    content += '<div class="display-tc">';
                    content += '<div class="mausac">';
                    content += 'Màu sắc: <span id="MauSac">' + result[i].mauSac + '</span>';
                    content += '</div>';

                    content += '<div class="size">';
                    content += 'Size: <span id="' + result[i].size + '">' + result[i].size + '</span>';
                    content += '</div>';

                    content += '</div>';
                    content += '</div>';

                    content += '<div class="one-eight text-center">';
                    content += '<div class="display-tc">';
                    content += '<span class="price" style="color:red;">' + TienSP + '</span>';
                    if (result[i].idSaleCT != null) {
                        content += '</br>';
                        content += '<span class="price" style="color: #999;text-decoration: line-through;">' + giaGoc + '</span>';
                    }
                    content += '</div>';
                    content += '</div>';
                    content += '<div class="one-eight text-center">';
                    content += '<div class="display-tc">';
                    content += '<form action="#">';
                    content += '<div class="inputsoluong" style="display:flex;">';
                    content += '<button id="GiamSoLuong" onclick="ClickGiamSoLuong(event,\'' + result[i].id + '\');"><i class="far fa-minus-square"></i> </button>';
                    content += '<input type="text" id="' + result[i].id + '" name="soluong" onblur="UpdateSoLuong(\'' + result[i].id + '\');" class="form-control input-number text-center" value="' + result[i].soLuong + '" min="1" max="100" style"width: 50px;height: 33px"/>';
                    content += '<button id="TangSoLuong" onclick="ClickTangSoLuong(event,\'' + result[i].id + '\');"><i class="far fa-plus-square"></i> </button>';
                    content += '</div>';
                    content += '</form>';
                    content += '</div>'
                    content += '</div>'
                    content += '<div class="one-eight text-center">';
                    content += '<div class="display-tc">';
                    content += '<span class="price">' + ThanhTien + '</span>'
                    content += '</div>';
                    content += '</div>';

                    content += '<div class="one-eight text-center">';
                    content += '<div class="display-tc">';
                    content += '<button onclick="XoaSanPham(\'' + result[i].id + '\');" class="" style="color: red; font-size:16px; border:none; background-color :white; margin-left: -4px;margin-top: 10px; "><i class="fas fa-trash" style="color: #ff0000;"></i></button>';

                    content += '</div>';
                    content += '</div>';

                    content += '</div>';
                    DanhSachSanPhamCungTenCungMau(data, droplstId);
                    $("#danhSachsanpham").append(content);

                    sum += result[i].thanhTien;
                    var fomatVND = sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    document.getElementById("tongtien").innerText = fomatVND;
                    hienthitienship();
                }

                console.log(result);
            } else {
                var fomatVND = sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                document.getElementById("tongtien").innerText = fomatVND;
                hienthitienship();
            }
        },
        error: function (xhr, status, error) {
            // Handle error response, if needed
            // Display error message or retry, based on your application logic
        }
    });
    return;
};

function DanhSachSanPhamCungTenCungMau(data, droplstId) {
    $.ajax({
        url: '/KhachHang/TrangChu/TruyVanSize',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (response) {
            $("#" + droplstId).empty();
            if (response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    var content1 = '';
                    content1 += '<select style="width: 70px; height: 30px; font-size: 15px; ">';
                    content1 += '<option value="' + response[i].size + '">' + response[i].size + '</option>';
                    content1 += '</select>';
                }
                $("#" + droplstId).append(content1);
            };
            console.log(response);
        },
        error: function (xhr, status, error) {
            alert('Lỗi gọi api', error);
        }
    });
};
function XoaSanPham(id) {
    if (id == "" || id == null || id == undefined) {
        alert("Thất bại.");
    } else {
        $.ajax({
            type: 'POST',
            url: '/KhachHang/GioHangChiTiet/DeleteCartDetail',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(id),
            success: function (result) {
                if (result.message == "true") {
                    alert("Xóa thành công.");
                }
                else {
                    alert("Xóa thất bại.");
                }
                ViewSoLuong();
                console.log(result);
                LoadGioHangChiTiet();
            },
            error: function (error) {

            }
        });
    }
};

function ClickGiamSoLuong(event, id) {
    event.preventDefault();
    var stringid = String(id);
    var quantityInput = $('input[id="' + stringid + '"]').val();
    var soluong = parseInt(quantityInput);
    var currentValue = soluong;
    if (currentValue > 1) { // Đảm bảo số lượng không nhỏ hơn 1
        var soluongsautru = currentValue - 1;
        $('input[id="' + stringid + '"]').val(soluongsautru);
        UpdateSoLuong(id);
    }

};

function ClickTangSoLuong(event, id) {
    event.preventDefault();
    var stringid = String(id);
    var quantityInput = $('input[id="' + stringid + '"]').val();
    var soluong = parseInt(quantityInput);
    var currentValue = soluong;
    // Đảm bảo số lượng không nhỏ hơn 1
    var soluongsautru = currentValue + 1;
    $('input[id="' + stringid + '"]').val(soluongsautru);
    UpdateSoLuong(id);
};

function UpdateSoLuong(id) {
    var soluong = $('input[id="' + id + '"]').val();
    var data = {
        ID: id,
        SoLuong: soluong,
    }
    $.ajax({
        url: '/KhachHang/GioHang/UpdateSoluongtronggiohang',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (response) {
            if (response.message == "Cập nhật số lượng thành công.") {

            } else {
                alert(response.message);
            }
            LoadGioHangChiTiet();
            console.log(response);
        },
        error: function (xhr, status, error) {
            alert('Lỗi gọi api', error);
        }
    });
};
function ViewSoLuong() {
    $.ajax({
        type: 'GET',
        url: '/KhachHang/GioHang/Soluong',
        success: function (result) {
            $('p[id="soluongtrongiohang"]').text(result);
            console.log(result);
        },
        error: function (xhr, status, error) {
            console.log('Lỗi gọi api: ' + error);
        }
    });
};
function validateSDT() {
    // Kiểm tra xem chuỗi có bắt đầu bằng số 0 không
    var sdt = document.getElementById("sdt").value;
    var errorsdt = document.getElementById("errorsdt");
    if (sdt.charAt(0) !== '0') {
        errorsdt.innerText = "Số điện thoại bắt đầu bằng số 0.";
        return false;
    }

    // Kiểm tra xem tất cả các ký tự còn lại có phải là số không
    for (var i = 1; i < sdt.length; i++) {
        if (isNaN(sdt.charAt(i))) {
            errorsdt.innerText = "Yêu cầu nhập số";
            return false;
        }
    }
    // Kiểm tra xem độ dài của chuỗi có phải là 10 không
    if (sdt.length != 10) {
        errorsdt.innerText = "Số điện thoại độ dài là 10.";
        return false;
    }


    // Nếu tất cả các điều kiện đều được thoả mãn, trả về true
    errorsdt.innerText = "";
    return true;
};

function validateEmail() {
    // Sử dụng biểu thức chính quy để kiểm tra địa chỉ email
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var email = document.getElementById("email").value;
    var erroremail = document.getElementById("erroremail");
    if (regex.test(email)) {
        erroremail.innerText = "";
        return true;
    } else {
        erroremail.innerText = "Yêu cầu nhập đúng dạng email @gmail.com";
        return false;
    }

}
function validateName() {
    var name = document.getElementById("name").value;
    var errorten = document.getElementById("errorten");
    if (name == "" || name == null || name == undefined) {
        errorten.innerText = "Không được để trống";
        return false;
    } else {
        errorten.innerText = "";
        return true;
    }
}
function validatediachi1() {
    var diachi = document.getElementById("diachi").value;
    var errorDiaChi = document.getElementById("errorDiaChi");
    if (diachi == "" || diachi == null || diachi == undefined) {
        errorDiaChi.innerText = "Không được để trống";
        return false;
    } else {
        errorDiaChi.innerText = "";
        return true;
    }
}
function validatevoucher() {
    var vaVouch = document.getElementById("voucher").value;
    var errVouch = document.getElementById("errVouch");
    if (vaVouch == "" || vaVouch == null || vaVouch == undefined) {
        errVouch.innerText = "Không được để trống";
        return false;
    } else {
        errVouch.innerText = "";
        return true;
    }
}

function suggestVoucher(event) {
    var inputValue = "";
    if (event != undefined) {
        inputValue = event.target.value.toLowerCase();
    }

    $.ajax({
        type: 'GET',
        url: '/KhachHang/GioHang/DanhSachVoucherTheoKhachHang',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            const suggestionList = $("#suggestionListVoucher");
            suggestionList.empty();
            let suggestions;

            if (event != undefined) {
                suggestions = data.filter(voucher =>
                    voucher.maVoucher.toLowerCase().includes(inputValue)
                );
            } else {
                suggestions = data;
            }

            if (suggestions.length === 0) {
                suggestions = data;
            }

            suggestions.forEach(voucher => {
                suggestionList.append(`<li>${voucher.maVoucher} - ${voucher.giaTriVoucher} %</li>`);
            });

            suggestionList.find('li').on('click', function () {
                // Lấy thông tin của gợi ý được chọn
                const selectedVoucher = $(this).text().split(' - ');
                const maVoucher = selectedVoucher[0];

                // Điền thông tin vào các ô input
                $('#magiamgia').val(maVoucher);

                // Xóa danh sách gợi ý
                $("#suggestionListVoucher").empty();
            });
        },
        error: function (error) {
            // Xử lý lỗi nếu cần
        }
    });
}




