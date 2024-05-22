
var idnguoidung = "";
function DanhSachNguoiDung() {
    $.ajax({
        type: 'GET',
        url: '/Admin/QLNguoiDung/DanhSachNguoiDungJson', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạnff
        success: function (result) {
            $("#table-user").empty();
            var content = '';
            if (result.length > 0) {
                for (var i = 0; i < result.length; i++) {
                    var stt = i + 1;
                    content += '<tr>';
                    content += '<td>' + stt;
                    content += '</td>';
                    content += '<td>' + result[i].tenNguoiDung;
                    content += '</td>';
                    content += '<td>';
                    content += '<ul class="list-inline">';
                    content += '<li class="list-inline-item">';
                    content += '<img alt="Avatar" class="table-avatar" src="https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev=2540745">';
                    content += '</li>';
                    content += '</ul>';
                    content += '</td>';
                    content += '<td>' + result[i].email;
                    content += '</td>';
                    content += '<td>' + result[i].sdt;
                    content += '</td >';
                    content += '<td class="project-state">';
                    if (result[i].trangThai == 1) {
                        content += '<span class="badge badge-success">Hoạt động</span>';
                    } else {
                        content += '<span class="badge badge-error">không hoạt động</span>';
                    }
                    content += '</td>';
                    content += '<td class="project-actions text-center">';
                    if (result[i].trangThai == 1) {
                        content += '<button onclick="TatHoatDongTaiKhoan(\'' + result[i].id + '\');" class="btn btn-danger btn-sm"><i class="fas fa-lock"></i> Khóa</button>';
                    } else {
                        content += '<button onclick="KichHoatTaiKhoan(\'' + result[i].id + '\')" class="btn btn-primary btn-sm" ><i class="fas fa-lock-open"></i> Kích hoạt</button>';
                    }

                    content += '</td>';
                    content += '</tr>'
                }
                $("#table-user").append(content);
            } else {
                content += '<tr>Không có dữ liệu</tr>';
                $("#table-user").append(content);
            }
            console.log(result);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
}
function TimkiemNguoiDungTheoTuKhoa() {
    var tukhoa = document.getElementById("tukhoa").value;
    if (tukhoa == null || tukhoa == "" || tukhoa == undefined) {
        DanhSachNguoiDung();
    } else {
        $.ajax({
            type: 'POST',
            url: '/Admin/QLNguoiDung/DanhSachNguoiDungTheoTuKhoa',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(tukhoa),
            success: function (result) {
                $("#table-user").empty();
                var content = '';
                if (result.length > 0) {
                    for (var i = 0; i < result.length; i++) {
                        var stt = i + 1;
                        content += '<tr>';
                        content += '<td>' + stt;
                        content += '</td>';
                        content += '<td>' + result[i].tenNguoiDung;
                        content += '</td>';
                        content += '<td>';
                        content += '<ul class="list-inline">';
                        content += '<li class="list-inline-item">';
                        content += '<img alt="Avatar" class="table-avatar" src="https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev=2540745">';
                        content += '</li>';
                        content += '</ul>';
                        content += '</td>';
                        content += '<td>' + result[i].email;
                        content += '</td>';
                        content += '<td>' + result[i].sdt;
                        content += '</td >';
                        content += '<td class="project-state">';
                        if (result[i].trangThai == 1) {
                            content += '<span class="badge badge-success">Hoạt động</span>';
                        } else {
                            content += '<span class="badge badge-error">không hoạt động</span>';
                        }
                        content += '</td>';
                        content += '<td class="project-actions text-center">';
                        if (result[i].trangThai == 1) {
                            content += '<button class="btn btn-danger btn-sm" onclick="TatHoatDongTaiKhoan(\'' + result[i].id + '\');" ><i class="fas fa-lock"></i> Khóa</button>';
                        } else {
                            content += '<button class="btn btn-primary btn-sm" onclick="KichHoatTaiKhoan(\'' + result[i].id + '\')" ><i class="fas fa-lock-open"></i> Kích hoạt</button>';
                        }
                        content += '</td>';
                        content += '</tr>'
                    }
                    $("#table-user").append(content);
                } else {
                    content += '<tr>Không có dữ liệu</tr>';
                    $("#table-user").append(content);
                }

                console.log(result)
            },
            error: function (error) { }

        })
    }
    return;
};
document.getElementById("trangthaiFilter").addEventListener("change", function () {
    var selectedValue = this.value;
    if (selectedValue == "-1") {
        DanhSachNguoiDung();
    } else {
        $.ajax({
            type: 'POST',
            url: '/Admin/QLNguoiDung/DanhSachNguoiDungTrangThai',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(selectedValue),
            success: function (result) {
                $("#table-user").empty();
                var content = '';
                if (result.length > 0) {

                    for (var i = 0; i < result.length; i++) {
                        var stt = i + 1;

                        content += '<tr>';
                        content += '<td>' + stt;
                        content += '</td>';
                        content += '<td>' + result[i].tenNguoiDung;
                        content += '</td>';
                        content += '<td>';
                        content += '<ul class="list-inline">';
                        content += '<li class="list-inline-item">';
                        content += '<img alt="Avatar" class="table-avatar" src="https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev=2540745">';
                        content += '</li>';
                        content += '</ul>';
                        content += '</td>';
                        content += '<td>' + result[i].email;
                        content += '</td>';
                        content += '<td>' + result[i].sdt;
                        content += '</td >';
                        content += '<td class="project-state">';
                        if (result[i].trangThai == 1) {
                            content += '<span class="badge badge-success">Hoạt động</span>';
                        } else {
                            content += '<span class="badge badge-error">không hoạt động</span>';
                        }
                        content += '</td>';
                        content += '<td class="project-actions text-center">';
                        if (result[i].trangThai == 1) {
                            content += '<button class="btn btn-danger btn-sm" onclick="TatHoatDongTaiKhoan(\'' + result[i].id + '\');"><i class="fas fa-lock"></i> Khóa</button>';
                        } else {
                            content += '<button class="btn btn-primary btn-sm" onclick="KichHoatTaiKhoan(\'' + result[i].id + '\')"><i class="fas fa-lock-open"></i> Kích hoạt</button>';
                        }
                        content += '</td>';
                        content += '</tr>'
                        //'<option value="' + result[i].wardCode + '" data-url="' + '/GioHang/TinhTienShip?WardCode=' + data[i].wardCode + '">' + data[i].wardName + '</option>'
                    }
                    $("#table-user").append(content);
                } else {
                    content += '<tr>Không có dữ liệu</tr>';
                    $("#table-user").append(content);
                }
                console.log(result)
            },
            error: function (error) {

            }
        });
    }
    return;
});
function TatHoatDongTaiKhoan(id) {
    if (id == "" || id == null || id == undefined) {
        alert("Thất bại.");
    } else {
        var data = {
            Id: id
        }
        $.ajax({
            type: 'POST',
            url: '/Admin/QLNguoiDung/KhoaTaiKhoan',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                alert(result.message)
                console.log(result);
                DanhSachNguoiDung();
            },
            error: function (error) {

            }
        });
    }
    return;
};
function KichHoatTaiKhoan(id) {
    if (id == "" || id == null || id == undefined) {
        alert("Thất bại.");
    } else {
        var data = {
            Id: id
        }
        $.ajax({
            type: 'POST',
            url: '/Admin/QLNguoiDung/KichHoaTaiKhoan',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                alert(result.message);
                console.log(result);
                DanhSachNguoiDung();
            },
            error: function (error) {

            }
        });
    }
    return;
};

function KiemtraEmail(event) {
    event.preventDefault();
    var email = document.getElementById("txt_email").value;
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (email == "") {
        document.getElementById("CheckEmail").innerText = "Bạn chưa nhập email";
        return;
    } else if (!emailRegex.test(email)) {
        document.getElementById("CheckEmail").innerText = "Mời bạn nhập đúng dạng email.";
        return;
    } else {
        $.ajax({
            url: '/DangNhap/KiemTraEmail',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(email),
            success: function (result) {
                if (result.length == 0) {
                    document.getElementById("CheckEmail").innerText = "Email bạn nhập chưa được đăng ký.";
                    return;
                } else {
                    document.getElementById("CheckEmail").innerText = "";
                    document.getElementById("matkhau").style.display = "block";
                    document.getElementById("LuuMK").style.display = "block";
                    document.getElementById("kiemtraemail").style.display = "none";
                    console.log(result);
                    for (var i = 0; i < result.length; i++) {
                        idnguoidung = String(result[i].id);
                    }
                }
            },
            error: function (xhr, status, error) {
                alert('Lỗi: ', error);
            }
        });
    };
};

function DoiMatKhau(event) {
    event.preventDefault();
    var idnguoidung = document.getElementById("idnguoidung").innerText;
    var password = document.getElementById("password").value;
    var Repassword = document.getElementById("Repassword").value;
    if (password == "") {
        document.getElementById("Checkpassword").innerText = "Bạn chưa nhập mật khẩu.";
        document.getElementById("Repassword").value = "";
        return;
    } else if (Repassword == "" && password != "") {
        document.getElementById("CheckRepassword").innerText = "Bạn chưa xác nhận lại mật khẩu.";
        document.getElementById("Checkpassword").innerText = "";
        return;
    } else if (password != Repassword) {
        document.getElementById("CheckRepassword").innerText = "Mật khẩu không khớp mời bạn nhập lại.";
        document.getElementById("Repassword").value = "";
        return;
    } else {
        document.getElementById("Repassword").value = "";
        var data = {
            Id: idnguoidung,
            MatKhau: Repassword,
        }
        $.ajax({
            url: '/DangNhap/DoiMatKhau',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (result) {
                alert(result.message);
                window.location.href = "/KhachHang/ThongTinCaNhan/ThongTinCaNhan";
            },
            error: function (xhr, status, error) {
                alert('Lỗi: ', error);
            }
        });
    }
};
function ThongTinCaNhan() {
    var data = {
        Id: idnguoidung,
        MatKhau: Repassword,
    }
    $.ajax({
        url: '/KhachHang/ThongTinCaNhan/ThongTinCaNhanJson',
        type: 'GET',
        contentType: 'application/json',
        data: JSON.stringify(data),
        success: function (result) {
            console.log(result);
           
            var inputNgaySinh = document.getElementById("NgaySinh");
            // Lấy ra các thành phần ngày, tháng và năm
   
            var ngaySinhDate = new Date(result.ngaySinh);

            var year = ngaySinhDate.getFullYear();
            var month = ngaySinhDate.getMonth() + 1; // Tháng trong JavaScript bắt đầu từ 0, cần cộng thêm 1
            var day = ngaySinhDate.getDate();

            var formattedDate = year + '-' + (month < 10 ? '0' : '') + month + '-' + (day < 10 ? '0' : '') + day;
            // Gán chuỗi có định dạng hợp lệ vào trường nhập dạng date
            inputNgaySinh.value = formattedDate;
            document.getElementById("HoVaTen").value = result.tenNguoiDung;
            //document.getElementById("ten").innerText = result.tenNguoiDung;
            document.getElementById("idnguoidung").innerText = result.id;
            //document.getElementById("NgaySinh").value = result.ngaySinh;
            document.getElementById("SDT").value = result.sdt;
            document.getElementById("Email").value = result.email;
            document.getElementById("Tinh").value = result.tinhThanh;
            document.getElementById("Quan").value = result.quanHuyen;
            document.getElementById("Phuong").value = result.phuongXa;
            document.getElementById("DiaChi").value = result.diaChi;
            $("#linkanh").empty();
            var content = '';
            if (result.anh == "") {
                content += '<img class="rounded-circle mt-5" width="150px" src="https://st3.depositphotos.com/15648834/17930/v/600/depositphotos_179308454-stock-illustration-unknown-person-silhouette-glasses-profile.jpg">';
            } else {
                content += '<img class="rounded-circle mt-5" width="150px" src="' + result.anh + '">';
            }
            $("#linkanh").append(content);
            console.log(result);
        },
        error: function (xhr, status, error) {
            alert('Lỗi: ', error);
        }
    });
};

function HienThiDoiMatKhau(event) {
    event.preventDefault();

    var doimk = document.getElementById("doimk");
    doimk.style.display = "block";

    var thongtin = document.getElementById("thongtin");
    thongtin.style.display = "none";

    var buttonDoiThongTin = document.getElementById("DoiThongTin");
    buttonDoiThongTin.style.display = "none";

    var buttonDoiMK = document.getElementById("DoiMK");
    buttonDoiMK.style.display = "none";

    var buttonHuy = document.getElementById("Huy");
    buttonHuy.style.display = "block";
}
function HienThiThongTin(event) {
    event.preventDefault();
    var buttonDoiThongTin = document.getElementById("DoiThongTin");
    buttonDoiThongTin.style.display = "none";

    var buttonDoiMK = document.getElementById("DoiMK");
    buttonDoiMK.style.display = "none";

    var buttonLuuThongTin = document.getElementById("LuuThongTin");
    buttonLuuThongTin.style.display = "block";

    var buttonHuy = document.getElementById("Huy");
    buttonHuy.style.display = "block";

    var inputs = document.querySelectorAll('.thongtin');
    inputs.forEach(function (input) {
        input.readOnly = false;
        input.classList.remove('backgroundInput');
    });
};

function LuuThongTin() {
    var validateSDT = validateSDT1();
    var validateEmail = validateEmail1();
    var id = document.getElementById("idnguoidung").innerText;
    var ten = document.getElementById("HoVaTen").value;
    var sdt = document.getElementById("SDT").value;
    var email = document.getElementById("Email").value;
    var tinh = document.getElementById("Tinh").value;
    var quan = document.getElementById("Quan").value;
    var phuong = document.getElementById("Phuong").value;
    var diachi = document.getElementById("DiaChi").value;
    var ngaySinh = document.getElementById("NgaySinh").value;
    if (validateSDT == false) {
        return;
    } else if (validateEmail == false) {
        return;
    } else {
        var data = {
            Id: id,
            TenNguoiDung: ten,
            NgaySinh: ngaySinh,
            SDT: sdt,
            Email: email,
            QuanHuyen: quan,
            TinhThanh: tinh,
            PhuongXa: phuong,
            DiaChi: diachi,
        }
        $.ajax({
            url: '/KhachHang/ThongTinCaNhan/ThayThoiThongTinCaNhan',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            success: function (result) {
                if (result.message == "Chỉnh sửa thông tin thất bại.") {
                    alert("Lưu thất bại.");
                } else if (result.message == "Chỉnh sửa thông tin thành công.") {
                    alert("Lưu thành công.");

                } else {
                    alert("Lỗi.");
                }
                window.location.href = "/KhachHang/ThongTinCaNhan/ThongTinCaNhan";
                console.log(result);
            },
            error: function (xhr, status, error) {
                alert('Lỗi: ', error);
            }
        });
    }
};
function DanhSachNhanVien() {
    $.ajax({
        type: 'GET',
        url: '/Admin/QLNguoiDung/DanhSachNhanVienJson', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            $("#table-user").empty();
            var content = '';
            if (result.length > 0) {
                var stt = 0;
                for (var i = 0; i < result.length; i++) {
                    stt++;

                    content += '<tr>';
                    content += '<td>' + stt;
                    content += '</td>';
                    content += '<td>' + result[i].tenNguoiDung;
                    content += '</td>';
                    content += '<td>';
                    content += '<ul class="list-inline">';
                    content += '<li class="list-inline-item">';
                    content += '<img alt="Avatar" class="table-avatar" src="https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev=2540745">';
                    content += '</li>';
                    content += '</ul>';
                    content += '</td>';
                    content += '<td>' + result[i].email;
                    content += '</td>';
                    content += '<td>' + result[i].sdt;
                    content += '</td >';
                    content += '<td class="project-state">';
                    if (result[i].trangThai == 1) {
                        content += '<span class="badge badge-success">Hoạt động</span>';
                    } else {
                        content += '<span class="badge badge-error">không hoạt động</span>';
                    }
                    content += '</td>';
                    content += '<td class="project-actions text-center">';
                    if (result[i].trangThai == 1) {
                        content += '<button onclick="TatHoatDongTaiKhoan(\'' + result[i].id + '\');" class="btn btn-danger btn-sm"><i class="fas fa-lock"></i> Khóa</button>';
                    } else {
                        content += '<button onclick="KichHoatTaiKhoan(\'' + result[i].id + '\')" class="btn btn-primary btn-sm" ><i class="fas fa-lock-open"></i> Kích hoạt</button>';
                    }

                    content += '</td>';
                    content += '</tr>'
                    //'<option value="' + result[i].wardCode + '" data-url="' + '/GioHang/TinhTienShip?WardCode=' + data[i].wardCode + '">' + data[i].wardName + '</option>'
                }
                $("#table-user").append(content);
            } else {
                content += '<tr>Không có dữ liệu</tr>';
                $("#table-user").append(content);
            }
            console.log(result);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
}
function TimkiemNhanVienTheoTuKhoa() {
    var tukhoa = document.getElementById("tukhoa").value;
    if (tukhoa == null || tukhoa == "" || tukhoa == undefined) {
        DanhSachNhanVien();
    } else {
        $.ajax({
            type: 'POST',
            url: '/Admin/QLNguoiDung/DanhSachNhanVienTheoTuKhoa',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(tukhoa),
            success: function (result) {
                $("#table-user").empty();
                var content = '';
                if (result.length > 0) {
                    var stt = 0;
                    for (var i = 0; i < result.length; i++) {
                        stt++;

                        content += '<tr>';
                        content += '<td>' + stt;
                        content += '</td>';
                        content += '<td>' + result[i].tenNguoiDung;
                        content += '</td>';
                        content += '<td>';
                        content += '<ul class="list-inline">';
                        content += '<li class="list-inline-item">';
                        content += '<img alt="Avatar" class="table-avatar" src="https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev=2540745">';
                        content += '</li>';
                        content += '</ul>';
                        content += '</td>';
                        content += '<td>' + result[i].email;
                        content += '</td>';
                        content += '<td>' + result[i].sdt;
                        content += '</td >';
                        content += '<td class="project-state">';
                        if (result[i].trangThai == 1) {
                            content += '<span class="badge badge-success">Hoạt động</span>';
                        } else {
                            content += '<span class="badge badge-error">không hoạt động</span>';
                        }
                        content += '</td>';
                        content += '<td class="project-actions text-center">';
                        if (result[i].trangThai == 1) {
                            content += '<button class="btn btn-danger btn-sm" onclick="TatHoatDongTaiKhoan(\'' + result[i].id + '\');" ><i class="fas fa-lock"></i> Khóa</button>';
                        } else {
                            content += '<button class="btn btn-primary btn-sm" onclick="KichHoatTaiKhoan(\'' + result[i].id + '\')" ><i class="fas fa-lock-open"></i> Kích hoạt</button>';
                        }

                        content += '</td>';
                        content += '</tr>'
                    }
                    $("#table-user").append(content);
                }
                else {
                    content += '<tr>Không có dữ liệu</td>';
                    $("#table-user").append(content);
                }
                console.log(result)
            },
            error: function (error) { }

        })
    }
    return;
};
document.getElementById("trangthaiNhanVienFilter").addEventListener("change", function () {
    var selectedValue = this.value;
    if (selectedValue == "-1") {
        DanhSachNhanVien();
    } else {
        $.ajax({
            type: 'POST',
            url: '/Admin/QLNguoiDung/DanhSachNhanVienTrangThai',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(selectedValue),
            success: function (result) {
                $("#table-user").empty();
                var content = '';
                if (result.length > 0) {
                    var stt = 0;
                    for (var i = 0; i < result.length; i++) {
                        stt++;

                        content += '<tr>';
                        content += '<td>' + stt;
                        content += '</td>';
                        content += '<td>' + result[i].tenNguoiDung;
                        content += '</td>';
                        content += '<td>';
                        content += '<ul class="list-inline">';
                        content += '<li class="list-inline-item">';
                        content += '<img alt="Avatar" class="table-avatar" src="https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev=2540745">';
                        content += '</li>';
                        content += '</ul>';
                        content += '</td>';
                        content += '<td>' + result[i].email;
                        content += '</td>';
                        content += '<td>' + result[i].sdt;
                        content += '</td >';
                        content += '<td class="project-state">';
                        if (result[i].trangThai == 1) {
                            content += '<span class="badge badge-success">Hoạt động</span>';
                        } else {
                            content += '<span class="badge badge-error">không hoạt động</span>';
                        }
                        content += '</td>';
                        content += '<td class="project-actions text-center">';
                        if (result[i].trangThai == 1) {
                            content += '<button class="btn btn-danger btn-sm" onclick="TatHoatDongTaiKhoan(\'' + result[i].id + '\');"><i class="fas fa-lock"></i> Khóa</button>';
                        } else {
                            content += '<button class="btn btn-primary btn-sm" onclick="KichHoatTaiKhoan(\'' + result[i].id + '\')"><i class="fas fa-lock-open"></i> Kích hoạt</button>';
                        }
                        content += '</td>';
                        content += '</tr>'
                    }
                    $("#table-user").append(content);
                } else {
                    content += '<tr>Không có dữ liệu</tr>';
                }
                console.log(result)
            },
            error: function (error) {

            }
        });
    }
    return;
});
document.getElementById("ChucVuFilter").addEventListener("change", function () {
    var selectedValue = this.value;
    if (selectedValue == "-1") {
        DanhSachNhanVien();
    } else {
        $.ajax({
            type: 'POST',
            url: '/Admin/QLNguoiDung/DanhSachNhanVienTheoChucVu',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(selectedValue),
            success: function (result) {
                $("#table-user").empty();
                var content = '';
                if (result.length > 0) {
                    var stt = 0;
                    for (var i = 0; i < result.length; i++) {
                        stt++;

                        content += '<tr>';
                        content += '<td>' + stt;
                        content += '</td>';
                        content += '<td>' + result[i].tenNguoiDung;
                        content += '</td>';
                        content += '<td>';
                        content += '<ul class="list-inline">';
                        content += '<li class="list-inline-item">';
                        content += '<img alt="Avatar" class="table-avatar" src="https://ps.w.org/user-avatar-reloaded/assets/icon-256x256.png?rev=2540745">';
                        content += '</li>';
                        content += '</ul>';
                        content += '</td>';
                        content += '<td>' + result[i].email;
                        content += '</td>';
                        content += '<td>' + result[i].sdt;
                        content += '</td >';
                        content += '<td class="project-state">';
                        if (result[i].trangThai == 1) {
                            content += '<span class="badge badge-success">Hoạt động</span>';
                        } else {
                            content += '<span class="badge badge-error">không hoạt động</span>';
                        }
                        content += '</td>';
                        content += '<td class="project-actions text-center">';
                        if (result[i].trangThai == 1) {
                            content += '<button class="btn btn-danger btn-sm" onclick="TatHoatDongTaiKhoan(\'' + result[i].id + '\');"><i class="fas fa-lock"></i> Khóa</button>';
                        } else {
                            content += '<button class="btn btn-primary btn-sm" onclick="KichHoatTaiKhoan(\'' + result[i].id + '\')"><i class="fas fa-lock-open"></i> Kích hoạt</button>';
                        }

                        content += '</td>';
                        content += '</tr>'
                    }
                    $("#table-user").append(content);
                } else {
                    content += '<tr>Không có dữ liệu</tr>';
                }
                console.log(result)
            },
            error: function (error) {

            }
        });
    }
    return;
});

function validateSDT1() {
    // Kiểm tra xem chuỗi có bắt đầu bằng số 0 không
    var sdt = document.getElementById("SDT").value;
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

function validateEmail1() {
    // Sử dụng biểu thức chính quy để kiểm tra địa chỉ email
    var regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var email = document.getElementById("Email").value;
    var erroremail = document.getElementById("erroremail");
    if (regex.test(email)) {
        erroremail.innerText = "";
        return true;
    } else {
        erroremail.innerText = "Yêu cầu nhập đúng dạng email @gmail.com";
        return false;
    }

}

