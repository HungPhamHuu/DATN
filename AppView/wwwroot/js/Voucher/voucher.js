// hiển thị trang chủ
var lstid = [];
function HienThiVoucherLenTrangChu(lst) {
    console.log(lst);
    $.ajax({
        type: 'GET',
        url: '/Admin/Voucher/DanhSachJson',// Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            console.log(result);
            $("#lstVoucher").empty();
            var content = '';
            for (i = 0; i < result.length && i < 4; i++) {
                const today = new Date();
                const dateToday = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
                const ngayketthuc = new Date(result[i].ngayKetThuc);
                const dateKetThuc = ngayketthuc.getFullYear() + '-' + (ngayketthuc.getMonth() + 1) + '-' + ngayketthuc.getDate();
                if (result[i].trangThai == 1 && dateToday < dateKetThuc) {
                    var ngaykt = new Date(result[i].ngayKetThuc);
                    var date = ngaykt.getDate();
                    var month = ngaykt.getMonth();
                    var year = ngaykt.getFullYear();
                    var ngayktstring = date + '-' + month + '-' + year;
                    content += '<div class="card col-md-6" style="max-width: 540px;">';
                    content += '<div class="row">';
                    content += '<div class="col-md-4">';
                    content += '<img class="img-voucher" src="https://cdn-icons-png.freepik.com/512/869/869649.png" style="background-color:#ee4d2d; width: 100%;" />';
                    content += '</div>';
                    content += '<div class="col-md-8">';
                    content += '<div class="card-body">';
                    content += '<h5 class="card-title"><a href="/KhachHang/VoucherKH/VoucherTheoIdVoucher?idvoucherdetail=' + result[i].id + '" style="text-decoration: none;font-family: "Font Awesome 5 Brands";color: black;">' + result[i].maVoucher + '</a></h5>';
                    content += '<p class="card-text">Giảm giá đến ' + result[i].giaTriVoucher + '% trên hóa đơn.</p>';
                    //content += '<p style="margin-top: 10px;">Hạn sử dụng</p>';
                    content += '<p class="card-text"><small class="text-muted">Hạn đến: ' + ngayktstring + '</small></p>';
                    if (lst == undefined || lst.length == 0 || lst == null) {
                        content += '<button class="btn btn-primary" onclick="LayVoucher(\'' + result[i].id + '\')">Nhận</button>';

                    } else {
                        for (var j = 0; j < lst.length; j++) {
                            if (result[i].id == lst[j].idVoucher) {
                                content += '<button class="btn btn-secondary" onclick="DaCo();">Đã có</button>';
                                break;
                            } else if (result[i].id != lst[j].idVoucher) {
                                content += '<button class="btn btn-primary" onclick="LayVoucher(\'' + result[i].id + '\')">Nhận</button>';
                                break;
                            }
                        }
                    }
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                }
            }
            $("#lstVoucher").append(content);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        },

    });
};
function LayVoucher(id) {
    var data = {
        IDVoucher: id
    }
    $.ajax({
        type: 'POST',
        url: '/KhachHang/VoucherKH/LayVoucherChoNguoiDung',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),// Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            if (result.message == "true") {
                alert("Voucher đã được lấy.");
            } else if (result.message == "Yêu cầu đăng nhập") {
                alert(result.message);
                window.location.href = "/DangNhap/DanhNhap";
            } else {
                alert("Đã xảy ra lỗi khi lấy voucher.");
            }
            LstVoucherTheoKH()
            console.log(result);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};

function LstVoucherTheoKH() {
    $.ajax({
        type: 'GET',
        url: '/KhachHang/GioHang/DanhSachVoucherTheoKhachHang',
        success: function (data) {
            if (data == null) {
                lstid.push(0)
            } else {
                lstid.splice(0, lstid.length);
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        lstid.push(data[i].idVoucher)
                    }
                }
                console.log(data);
                HienThiVoucherLenTrangChu(data);
            }
            return ;
        },
        error: function (error) {
        }
    });
    
};
function DaCo() {
    alert("Voucher đã có");
    return;
}


