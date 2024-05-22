
function TimKiem(event) {
    // Lấy giá trị của trường nhập liệu tìm kiếm
    event.preventDefault();
    var searchValue = document.querySelector('.search').value;
    // Kiểm tra xem giá trị tìm kiếm có tồn tại không
    if (searchValue.trim() !== '') {
        window.location.href = 'https://localhost:7211/Home/TimKiem?tensp=' + searchValue;
    }
};

function LoaddanhsachTimkiem(tukhoa) {
    $.ajax({
        type: 'POST',
        url: '/Admin/SanPhamChiTiet/TimKiemTheoTenSanpham', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(tukhoa),
        success: function (result) {
            $("#sanpham").empty();
            if (result != null) {
                var content = '';
                for (i = 0; i < result.length; i++) {
                    var giaban = result[i].giaBan.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    content += '<div class="col-4 text-center">';
                    content += ' <div class="product-entry border">';
                    content += '<a href="#" class="prod-img">';
                    if (result[i].lstAnhSanPham.length == 0) {
                        content += '<img src="https://congtygiaphat104.com/template/Default/img/no.png" class="img-fluid" alt="Free html5 bootstrap 4 template">';
                    } else {
                        content += '<img src="/Admin/Anh/DisplayImage?DuongDan=' + result[i].lstAnhSanPham[0].duongDan + '" class="img-fluid" alt="Free html5 bootstrap 4 template" style="width:225px;height: 225px;">';
                    }
                    content += '</a>';
                    content += '<div class="desc">';
                    content += '<h2><a href="/KhachHang/TrangChu/SanPhamChiTiet?idspct=' + result[i].id + '">' + result[i].tenSP + '</a ></h2>';
                    content += '<span class="price">' + giaban + '</span>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                }
                $("#sanpham").append(content);
            };

            PhanTrang();
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
};
function DanhSachThuongHieu() {
    $.ajax({
        type: 'GET',
        url: '/KhachHang/TrangChu/DanhSachThuongHieu', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
           /* $("#lstThuongHieu").empty();*/
            if (result.length > 0) {
                var content = '';
                for (var i = 0; i < result.length; i++) {
                    content += '<li><a href="#" style="color:white;">' + result[i].tenThuongHieu + '</a></li>';
                };
                $("#lstThuongHieu").append(content);
            }
            console.log(result);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
};
function DanhSachTheLoai() {
    $.ajax({
        type: 'GET',
        url: '/KhachHang/TrangChu/DanhSachTheLoai', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            /*$("#lstTheLoai").empty();*/
            if (result.length > 0) {
                var content = '';
                for (var i = 0; i < result.length; i++) {
                    content += '<li style="color:white;">' + result[i].tenThuongHieu + '</li>';
                };
                $("#lstTheLoai").append(content);
            }
            console.log(result);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
};

function validateEmailDangNhap() {
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var email = document.getElementById("Email");
    var erroremail = document.getElementById("erroremail");
    document.getElementById('btnDangKy').disabled = false;
    if (regex.test(email.value)) {
        erroremail.innerText = "";
        return true;
    } else {
        erroremail.innerText = "Yêu cầu nhập đúng dạng email @gmail.com";
        document.getElementById('btnDangKy').disabled = true;
        return false;
    }
};
function validateSDT() {
    // Kiểm tra xem chuỗi có bắt đầu bằng số 0 không
    var sdt = document.getElementById("SDT").value;
    var errorsdt = document.getElementById("errorsdt");
    document.getElementById('btnDangKy').disabled = false;
    if (sdt.charAt(0) !== '0') {
        errorsdt.innerText = "Số điện thoại bắt đầu bằng số 0.";
         document.getElementById('btnDangKy').disabled = true;
        return false;
    }

    // Kiểm tra xem tất cả các ký tự còn lại có phải là số không
    for (var i = 1; i < sdt.length; i++) {
        if (isNaN(sdt.charAt(i))) {
            errorsdt.innerText = "Yêu cầu nhập số";
            document.getElementById('btnDangKy').disabled = true;
            return false;
        }
    }
    // Kiểm tra xem độ dài của chuỗi có phải là 10 không
    if (sdt.length != 10) {
        errorsdt.innerText = "Số điện thoại độ dài là 10.";
        document.getElementById('btnDangKy').disabled = true;
        return false;
    }


    // Nếu tất cả các điều kiện đều được thoả mãn, trả về true
    errorsdt.innerText = "";
    return true;
};
function validateTen() {
    var TenNguoiDung = document.getElementById("TenNguoiDung");
    var errorTen = document.getElementById("errorTen");
    document.getElementById('btnDangKy').disabled = false;
    if (TenNguoiDung.value == "") {
        errorTen.innerText = "Bạn nhập tên";
        document.getElementById('btnDangKy').disabled = true;

    } else {
        errorTen.innerText = "";
    }
    return true;
}
function validatePass() {
    var MatKhau = document.getElementById("MatKhau");
    var errorpass = document.getElementById("errorpass");
    document.getElementById('btnDangKy').disabled = false;
    if (MatKhau.value == "") {
        errorpass.innerText = "Bạn nhập mật khẩu";
        document.getElementById('btnDangKy').disabled = true;
    } else {
        errorpass.innerText = "";
    }
    return true;
}
function validateNgaySinh() {
    var NgaySinh = document.getElementById("NgaySinh");
    var errorNgaySinh = document.getElementById("errorNgaySinh");
    document.getElementById('btnDangKy').disabled = false;
    if (NgaySinh.value == "") {
        errorNgaySinh.innerText = "Bạn nhập ngày sinh.";
        document.getElementById('btnDangKy').disabled = true;
    } else {
        errorNgaySinh.innerText = "";
    }
    return true;
}

