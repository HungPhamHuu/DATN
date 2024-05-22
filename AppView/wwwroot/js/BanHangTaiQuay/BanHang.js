var idhd = "";
var idthanhtoan = "";
$(document).ready(function () {
    // Gọi hàm để load danh sách sản phẩm khi trang được tải
    // LoadDanhSachSanPham();
    // LoadDanhSachSanPhamSale();
    // Sự kiện khi click vào tên sản phẩm
    $(document).on('click', '.card-title a', function () {
        var productId = $(this).data('id'); // Lấy id của sản phẩm từ thuộc tính data-id
        $('#productId').text(productId);// Hiển thị id của sản phẩm trong modal

        var productId1 = $(this).data('id'); // Lấy id của sản phẩm từ thuộc tính data-id
        $('#productIdsale').text(productId1);// Hiển thị id của sản phẩm trong modal
    });

    // Sự kiện khi modal được hiển thị
    $('#myModal').on('show.bs.modal', function (event) {
        var idhdhientai = $('#idhd').text();
        var button = $(event.relatedTarget); // Button mở modal
        var productId = button.data('id'); // Lấy productId từ thuộc tính data-id của button
        var iframe = $(this).find('iframe'); // Tìm iframe trong modal
        idhd = idhdhientai;
        $('#nhanidhd').text(idhdhientai);
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/NhanIDhD',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(idhdhientai),
            success: function (result) {
                console.log(result);
            },
            error: function (error) {
                alert(error);
            }
        });

        // Thay đổi src của iframe để chứa productId
        iframe.attr('src', '/Admin/BanHangTaiQuay/SanPhamChiTiet?idspct=' + productId);
    });

    $('#myModalSale').on('show.bs.modal', function (event) {
        var idhdhientai = $('#idhd').text();
        var button = $(event.relatedTarget); // Button mở modal
        var productId = button.data('id'); // Lấy productId từ thuộc tính data-id của button
        var iframe = $(this).find('iframe'); // Tìm iframe trong modal
        idhd = idhdhientai;
        $('#nhanidhd').text(idhdhientai);
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/NhanIDhD',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(idhdhientai),
            success: function (result) {
                console.log(result);
            },
            error: function (error) {
                alert(error);
            }
        });

        // Thay đổi src của iframe để chứa productId
        iframe.attr('src', '/Admin/BanHangTaiQuay/SanPhamSaleChiTiet?idspsale=' + productId);
    });

});
function LoadDanhSachSanPham() {
    $.ajax({
        type: 'GET',
        url: '/Admin/BanHangTaiQuay/DanhSachSanPhamHoanThien', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            $("#ThongTinSanPham").empty();

            var content = '';
            content += '<div class="row" id="DanhSachSanPham">';
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    let chuoi = result[i].tenSP;
                    let viTriKhoangCachThuHai = chuoi.indexOf(' ', chuoi.indexOf(' ') + 4); // Tìm vị trí của khoảng cách thứ hai
                    let chuoiCat = chuoi.slice(0, viTriKhoangCachThuHai).concat("...");
                    var thanhtien = result[i].giaBan.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    // Thêm thẻ <a> để hiển thị tên sản phẩm và id của sản phẩm
                    content += '<div class="col-4">';
                    content += '<div class="card product-entry " style="width:200px">';
                    if (result[i].lstAnhSanPham.length > 0) {
                        content += '<a href="#" data-toggle="modal" data-target="#myModal" data-id="' + result[i].id + '"><img class="card-img-top" src="/Admin/Anh/DisplayImage?DuongDan=' + result[i].lstAnhSanPham[0].duongDan + '" alt="Card image" style="height: 200px;" /></a>';
                    } else {
                        content += '<a href="#" data-toggle="modal" data-target="#myModal" data-id="' + result[i].id + '"><img class="card-img-top" src="https://congtygiaphat104.com/template/Default/img/no.png" alt="Card image" style="width: 200px;height: 200px; "></a>';
                    }
                    content += '<div class="card-body">';
                    content += '<div class="tooltip-custom">';
                    content += '<h5 class="card-title"><a href="#" data-toggle="modal" data-target="#myModal" data-id="' + result[i].id + '">' + chuoiCat + '</a></h5>'; /*tooltip */
                    content += '<br/>';

                    content += '<span>' + thanhtien + '</span>';
                    content += '<span class="tooltiptext"> ' + result[i].tenSP + '</span>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                }
            }
            content += '</div>';
            $("#ThongTinSanPham").append(content);
            PhanTrang1();
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};
function LoadDanhSachSanPhamSale() {
    $.ajax({
        type: 'GET',
        url: '/Admin/BanHangTaiQuay/DanhSachSaleJson', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            $("#salesanpham").empty();
            var content = '';
            content += '<div class="row" id="salesanpham">';
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    let chuoi = result[i].tenSP;
                    let viTriKhoangCachThuHai = chuoi.indexOf(' ', chuoi.indexOf(' ') + 4); // Tìm vị trí của khoảng cách thứ hai
                    let chuoiCat = chuoi.slice(0, viTriKhoangCachThuHai).concat("...");
                    var giaGiam = result[i].giaGiam.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    // Thêm thẻ <a> để hiển thị tên sản phẩm và id của sản phẩm
                    content += '<div class="col-4">';
                    content += '<div class="card product-entry1 " style="width:200px">';
                    if (result[i].lstAnhSanPham.length > 0) {
                        content += '<a href="#" data-toggle="modal" data-target="#myModalSale" data-id="' + result[i].idSalechitiet + '"><img class="card-img-top" src="/Admin/Anh/DisplayImage?DuongDan=' + result[i].lstAnhSanPham[0].duongDan + '" alt="Card image" style="height: 200px;" /></a>';
                    } else {
                        content += '<a href="#" data-toggle="modal" data-target="#myModalSale" data-id="' + result[i].idSalechitiet + '"><img class="card-img-top" src="https://congtygiaphat104.com/template/Default/img/no.png" alt="Card image" style="width: 200px;height: 200px; "></a>';
                    }
                    content += '<div class="card-body">';
                    content += '<div class="tooltip-custom">';
                    content += '<h5 class="card-title"><a href="#" data-toggle="modal" data-target="#myModalSale" data-id="' + result[i].idSalechitiet + '">' + chuoiCat + '</a></h5>'; /*tooltip */
                    content += '<br/>';

                    content += '<span>' + giaGiam + '</span>';
                    content += '<span class="tooltiptext"> ' + result[i].tenSP + '</span>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                }
            }
            content += '</div>';
            $("#salesanpham").append(content);
            PhanTrang2();
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};
function TimKiemSpTaiQuay() {
    var tukhoa = document.getElementById("tukhoa").value;
    if (tukhoa == "") {
        LoadDanhSachSanPham();
        LoadDanhSachSanPhamSale();
    } else {
        TimkiemSanPhamTheoTen(tukhoa);
        TimkiemsaleKH(tukhoa);
    }
    return;
}
function TimkiemSanPhamTheoTen(tukhoa) {
    $.ajax({
        type: 'POST',
        url: '/Admin/SanPhamChiTiet/TimKiemTheoTenSanpham', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(tukhoa),
        success: function (result) {
            $("#ThongTinSanPham").empty();
            var content = '';
            content += '<div class="row" id="DanhSachSanPham">';
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    let chuoi = result[i].tenSP;
                    let viTriKhoangCachThuHai = chuoi.indexOf(' ', chuoi.indexOf(' ') + 4); // Tìm vị trí của khoảng cách thứ hai
                    let chuoiCat = chuoi.slice(0, viTriKhoangCachThuHai).concat("...");
                    var thanhtien = result[i].giaBan.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });

                    // Thêm thẻ <a> để hiển thị tên sản phẩm và id của sản phẩm
                    content += '<div class="col-4">';
                    content += '<div class="card product-entry " style="width:200px">';
                    if (result[i].lstAnhSanPham.length > 0) {
                        content += '<a href="#" data-toggle="modal" data-target="#myModal" data-id="' + result[i].id + '"><img class="card-img-top" src="/Admin/Anh/DisplayImage?DuongDan=' + result[i].lstAnhSanPham[0].duongDan + '" alt="Card image" style="height: 200px;" /></a>';
                    } else {
                        content += '<a href="#" data-toggle="modal" data-target="#myModal" data-id="' + result[i].id + '"><img class="card-img-top" src="https://congtygiaphat104.com/template/Default/img/no.png" alt="Card image" style="width: 200px;height: 200px; "></a>';
                    }
                    content += '<div class="card-body">';
                    content += '<div class="tooltip-custom">';
                    content += '<h5 class="card-title"><a href="#" data-toggle="modal" data-target="#myModal" data-id="' + result[i].id + '">' + chuoiCat + '</a></h5>'; /*tooltip */
                    content += '<br/>';
                    content += '<span>' + thanhtien + '</span>';
                    content += '<span class="tooltiptext"> ' + result[i].tenSP + '</span>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                }
            }
            content += '</div>';
            $("#ThongTinSanPham").append(content);
            PhanTrang1();
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};
function TimkiemsaleKH(tukhoa) {
    $.ajax({
        type: 'POST',
        url: '/KhachHang/SaleKH/TimKiemSPSale',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(tukhoa),
        success: function (result) {
            $("#salesanpham").empty();
            var content = '';
            content += '<div class="row" id="salesanpham">';
            if (result != null) {
                for (var i = 0; i < result.length; i++) {
                    let chuoi = result[i].tenSP;
                    let viTriKhoangCachThuHai = chuoi.indexOf(' ', chuoi.indexOf(' ') + 4); // Tìm vị trí của khoảng cách thứ hai
                    let chuoiCat = chuoi.slice(0, viTriKhoangCachThuHai).concat("...");
                    var giaGiam = result[i].giaGiam.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                    // Thêm thẻ <a> để hiển thị tên sản phẩm và id của sản phẩm
                    content += '<div class="col-4">';
                    content += '<div class="card product-entry1 " style="width:200px">';
                    if (result[i].lstAnhSanPham.length > 0) {
                        content += '<a href="#" data-toggle="modal" data-target="#myModalSale" data-id="' + result[i].idSalechitiet + '"><img class="card-img-top" src="/Admin/Anh/DisplayImage?DuongDan=' + result[i].lstAnhSanPham[0].duongDan + '" alt="Card image" style="height: 200px;" /></a>';
                    } else {
                        content += '<a href="#" data-toggle="modal" data-target="#myModalSale" data-id="' + result[i].idSalechitiet + '"><img class="card-img-top" src="https://congtygiaphat104.com/template/Default/img/no.png" alt="Card image" style="width: 200px;height: 200px; "></a>';
                    }
                    content += '<div class="card-body">';
                    content += '<div class="tooltip-custom">';
                    content += '<h5 class="card-title"><a href="#" data-toggle="modal" data-target="#myModalSale" data-id="' + result[i].idSalechitiet + '">' + chuoiCat + '</a></h5>'; /*tooltip */
                    content += '<br/>';
                    content += '<span>' + giaGiam + '</span>';
                    content += '<span class="tooltiptext"> ' + result[i].tenSP + '</span>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                    content += '</div>';
                }
            }
            content += '</div>';
            $("#salesanpham").append(content);
            PhanTrang2();
        },
        error: function (error) {
            console.log(error);
        }
    });
    return;
}
function PhanTrang1() {
    console.log("PhanTrang() has been called."); // Thêm dòng này để kiểm tra

    // Lấy danh sách tất cả các sản phẩm
    const products = document.querySelectorAll('.product-entry');

    // Số sản phẩm hiển thị trên mỗi trang
    const itemsPerPage = 6;

    // Hàm tạo các nút phân trang
    function createPaginationButtons(numPages, currentPage) {
        const pagination = document.getElementById('phantrang');
        if (pagination == null) {

        } else {
            pagination.innerHTML = '';

        }

        // Tạo nút "Previous" nếu không phải ở trang đầu tiên
        if (currentPage > 1) {
            const previousPage = createPaginationButton('<<', currentPage - 1);
            if (pagination == null) {

            } else {
                pagination.appendChild(previousPage);
            }
        }

        // Tạo nút trang
        if (numPages <= 5) {
            for (let i = 1; i <= numPages; i++) {
                const page = createPaginationButton(i, i);
                if (pagination == null) {

                } else {
                    pagination.appendChild(page);
                }

            }
        } else {
            // Nếu tổng số trang lớn hơn 5, chỉ hiển thị trang trước, trang hiện tại và trang sau
            let startPage = currentPage - 2;
            let endPage = currentPage + 2;

            // Đảm bảo không hiển thị trang âm hoặc trang lớn hơn tổng số trang
            if (startPage < 1) {
                startPage = 1;
                endPage = 5;
            } else if (endPage > numPages) {
                endPage = numPages;
                startPage = numPages - 4;
            }

            for (let i = startPage; i <= endPage; i++) {
                const page = createPaginationButton(i, i);
                if (pagination == null) {

                } else {
                    pagination.appendChild(page);
                }

            }
        }

        // Tạo nút "Next" nếu không phải ở trang cuối cùng
        if (currentPage < numPages) {
            const nextPage = createPaginationButton('>>', currentPage + 1);
            if (pagination == null) {

            } else {
                pagination.appendChild(nextPage);
            }
        }
    }

    // Hàm tạo một nút phân trang
    function createPaginationButton(label, pageNum) {
        const li = document.createElement('li');
        const a = document.createElement('a');
        a.href = '#';
        a.textContent = label;
        a.addEventListener('click', function () {
            showPage(pageNum);
        });
        li.appendChild(a);
        return li;
    }

    // Hàm hiển thị các sản phẩm của một trang cụ thể
    function showPage(pageNum) {
        const startIndex = (pageNum - 1) * itemsPerPage;
        const endIndex = startIndex + itemsPerPage;

        // Ẩn các sản phẩm của trang trước khi chuyển trang
        const previousPageNum = (pageNum === 1) ? numPages : pageNum - 1;
        const previousStartIndex = (previousPageNum - 1) * itemsPerPage;
        const previousEndIndex = previousStartIndex + itemsPerPage;
        products.forEach(function (product, index) {
            if (index >= previousStartIndex && index < previousEndIndex) {
                product.style.display = 'none';
            }
        });

        // Hiển thị các sản phẩm của trang hiện tại
        products.forEach(function (product, index) {
            if (index >= startIndex && index < endIndex) {
                product.style.display = 'block';
            } else {
                product.style.display = 'none';
            }
        });

        // Hiển thị lại các nút phân trang sau khi chuyển trang
        createPaginationButtons(numPages, pageNum);
    }

    // Tính số trang và hiển thị trang đầu tiên khi tải trang
    const numPages = Math.ceil(products.length / itemsPerPage);
    showPage(1);
};
function PhanTrang2() {
    console.log("PhanTrang() has been called."); // Thêm dòng này để kiểm tra

    // Lấy danh sách tất cả các sản phẩm
    const products = document.querySelectorAll('.product-entry1');

    // Số sản phẩm hiển thị trên mỗi trang
    const itemsPerPage = 6;

    // Hàm tạo các nút phân trang
    function createPaginationButtons(numPages, currentPage) {
        const pagination = document.getElementById('phantrang1');
        if (pagination == null) {

        } else {
            pagination.innerHTML = '';

        }

        // Tạo nút "Previous" nếu không phải ở trang đầu tiên
        if (currentPage > 1) {
            const previousPage = createPaginationButton('<<', currentPage - 1);
            if (pagination == null) {

            } else {
                pagination.appendChild(previousPage);
            }
        }

        // Tạo nút trang
        if (numPages <= 5) {
            for (let i = 1; i <= numPages; i++) {
                const page = createPaginationButton(i, i);
                if (pagination == null) {

                } else {
                    pagination.appendChild(page);
                }

            }
        } else {
            // Nếu tổng số trang lớn hơn 5, chỉ hiển thị trang trước, trang hiện tại và trang sau
            let startPage = currentPage - 2;
            let endPage = currentPage + 2;

            // Đảm bảo không hiển thị trang âm hoặc trang lớn hơn tổng số trang
            if (startPage < 1) {
                startPage = 1;
                endPage = 5;
            } else if (endPage > numPages) {
                endPage = numPages;
                startPage = numPages - 4;
            }

            for (let i = startPage; i <= endPage; i++) {
                const page = createPaginationButton(i, i);
                if (pagination == null) {

                } else {
                    pagination.appendChild(page);
                }

            }
        }

        // Tạo nút "Next" nếu không phải ở trang cuối cùng
        if (currentPage < numPages) {
            const nextPage = createPaginationButton('>>', currentPage + 1);
            if (pagination == null) {

            } else {
                pagination.appendChild(nextPage);
            }
        }
    }

    // Hàm tạo một nút phân trang
    function createPaginationButton(label, pageNum) {
        const li = document.createElement('li');
        const a = document.createElement('a');
        a.href = '#';
        a.textContent = label;
        a.addEventListener('click', function () {
            showPage(pageNum);
        });
        li.appendChild(a);
        return li;
    }

    // Hàm hiển thị các sản phẩm của một trang cụ thể
    function showPage(pageNum) {
        const startIndex = (pageNum - 1) * itemsPerPage;
        const endIndex = startIndex + itemsPerPage;

        // Ẩn các sản phẩm của trang trước khi chuyển trang
        const previousPageNum = (pageNum === 1) ? numPages : pageNum - 1;
        const previousStartIndex = (previousPageNum - 1) * itemsPerPage;
        const previousEndIndex = previousStartIndex + itemsPerPage;
        products.forEach(function (product, index) {
            if (index >= previousStartIndex && index < previousEndIndex) {
                product.style.display = 'none';
            }
        });

        // Hiển thị các sản phẩm của trang hiện tại
        products.forEach(function (product, index) {
            if (index >= startIndex && index < endIndex) {
                product.style.display = 'block';
            } else {
                product.style.display = 'none';
            }
        });

        // Hiển thị lại các nút phân trang sau khi chuyển trang
        createPaginationButtons(numPages, pageNum);
    }

    // Tính số trang và hiển thị trang đầu tiên khi tải trang
    const numPages = Math.ceil(products.length / itemsPerPage);
    showPage(1);
};
function suggest(event) {
    const inputValue = event.target.value.toLowerCase();
    if (inputValue === "") {
        clearSuggestionList();
        return;
    }

    $.ajax({
        type: 'GET',
        url: '/Admin/QLNguoiDung/DanhSachNguoiDungJson',
        dataType: 'json',
        success: function (data) {
            const filteredSuggestions = data.filter(user =>
                user.tenNguoiDung.toLowerCase().includes(inputValue) ||
                user.sdt.includes(inputValue) ||
                user.email.toLowerCase().includes(inputValue)
            );
            const suggestionList = $("#suggestionList");
            suggestionList.empty();

            if (filteredSuggestions.length === 0) {
                suggestionList.append("<li class='noMatch'>Không có bản ghi tương ứng</li>");
            } else {
                filteredSuggestions.forEach(user => {
                    suggestionList.append(`<li>${user.tenNguoiDung} - ${user.sdt} - ${user.email}</li>`);
                });
                suggestionList.find('li').on('click', function () {
                    // Lấy thông tin của gợi ý được chọn
                    const selectedUser = $(this).text().split(' - ');
                    const tenNguoiDung = selectedUser[0];
                    const sdt = selectedUser[1];
                    const email = selectedUser[2];

                    // Điền thông tin vào các ô input
                    $('#name').val(tenNguoiDung);
                    $('#sdt').val(sdt);
                    $('#email').val(email);

                    // Xóa danh sách gợi ý
                    clearSuggestionList();
                    validateEmail();
                    validateSDT();
                    validateName();
                    TruyenEmail()
                });
            }
        },
        error: function (error) {

        }
    });
    return;
}
function clearSuggestionList() {
    $("#suggestionList").empty();
};
function TaoHoaDonBanHangTaiQuay() {
    var tenkh = document.getElementById("name").value;
    var sdt = document.getElementById("sdt").value;
    var email = document.getElementById("email").value;
    if (tenkh == "") {
        validateName();
        return;
    } else if (sdt == "") {
        validateSDT();
        return;
    } else if (email == "") {
        validateEmail();
        return;
    } else {
        var data = {
            TenKhachHang: tenkh,
            SDTKhachHang: sdt,
        }
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/TaoHoaDonCho',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                console.log(result);
                if (result.message == "true") {
                    alert("Tạo hóa đơn thành công.");
                } else {
                    alert("Tạo hóa đơn thất bại.");
                }
                LayHoaDonChoMoiNhat();
                var DanhSachPhanTrang = document.getElementById("DanhSachPhanTrang");
                DanhSachPhanTrang.classList.remove("hidden");
                var ThanhToan = document.getElementById("ThanhToan");
                ThanhToan.classList.remove("hidden");

                var HoaDonChiTiet = document.getElementById("HoaDonChiTiet");
                HoaDonChiTiet.classList.remove("hidden");
                var danhsachsanphamhdct = document.getElementById("danhsachsanphamhdct");
                danhsachsanphamhdct.classList.remove("hidden");

                var HoaDonCho = document.getElementById("HoaDonCho");
                HoaDonCho.classList.add("hidden");
                var ThongTin = document.getElementById("ThongTin");
                ThongTin.classList.add("hidden");


            },
            error: function (error) {

            }
        });
    }
    return;
};
function LayIDHd() {

    $.ajax({
        type: 'GET',
        url: '/Admin/BanHangTaiQuay/TraIDHoaDon',
        success: function (result) {
            console.log(result);
            LstHoaDonChiTiet(result);
        },
        error: function (error) {
            console.log(error);
        }
    });
    return;
}
function ThemSanPhamVaoHoaDonChiTiet() {

    var size = document.getElementById("GiaTriSize").innerText;
    var mausac = document.getElementById("GiaTriMau").innerText;

    if (size == "") {
        alert("Bạn chưa chọn size.");
        return;
    } else if (mausac == "") {
        alert("Bạn chưa chọn màu.");
        return;
    }
    else {
        var idsp;
        var idspct = document.getElementById("idsanpham");
        var idsanphamSale = document.getElementById("idsanphamSale");
        if (idspct == null) {
            idsp = idsanphamSale.innerText;
        } else {
            idsp = idspct.innerText;
        }
        var soluongspct = document.getElementById("quantity").value;

        var data = {
            IDSPCT: idsp,
            SoLuong: soluongspct,
        };

        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/ThemSanPhamVaoHDCT',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                alert(result.message);
                //LayIDHd();
                Refresh();
            },
            error: function (error) {
                alert(error);
            }
        });
    }
    return;
};
function TruyenEmail() {
    var email = document.getElementById("email").value;
    $.ajax({
        type: 'POST',
        url: '/Admin/BanHangTaiQuay/NhanEmail',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(email),
        success: function (result) {

        },
        error: function (error) {

        }
    });
};
function TimKiemHoaDonCho() {
    var tukhoa = document.getElementById("timkiemhd").value;
    if (tukhoa == "") {
        GetHoaDonCho();
    } else {
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/TimKiemHoaDonCho', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(tukhoa),
            success: function (result) {
                $("#tbody_hoadoncho").empty();
                if (result != null) {
                    var content = '';
                    var stt = 0;
                    for (var i = 0; i < result.length; i++) {
                        stt++;
                        content += '<tr>';
                        content += '<td>' + stt + '</td>';
                        content += '<td style="display:none;">' + result[i].id + '</td>';
                        content += '<td>' + result[i].maHoaDon + '</td>';
                        content += '<td>' + result[i].tenKhachHang + '</td>';
                        content += '<td>' + result[i].sdtKhachHang + '</td>';
                        content += '<td>';
                        content += '<button onclick="LoadHoaDonChiTiet(\'' + result[i].id + '\');" class="btn"><i class="fas fa-shopping-cart"></i></button>';
                        content += '</td>';
                        content += '</tr>';
                    }
                    $("#tbody_hoadoncho").append(content);
                }
                console.log(result);
            },
            error: function (error) {
                console.error('Lỗi khi lấy dữ liệu từ Session:', error);
            }
        });
    }
    return;
};
function GetHoaDonCho() {
    $.ajax({
        type: 'GET',
        url: '/Admin/BanHangTaiQuay/DanhSachHoaDonCho', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            $("#tbody_hoadoncho").empty();
            if (result != null) {
                var content = '';
                var stt = 0;
                for (var i = 0; i < result.length; i++) {
                    stt++;
                    content += '<tr>';
                    content += '<td>' + stt + '</td>';
                    content += '<td style="display:none;">' + result[i].id + '</td>';
                    content += '<td>' + result[i].maHoaDon + '</td>';
                    content += '<td>' + result[i].tenKhachHang + '</td>';
                    content += '<td>' + result[i].sdtKhachHang + '</td>';
                    content += '<td>';
                    content += '<button onclick="LoadHoaDonChiTiet(\'' + result[i].id + '\');" class="btn"><i class="fas fa-shopping-cart"></i></button>';
                    content += '</td>';
                    content += '</tr>';
                }
                $("#tbody_hoadoncho").append(content);
            }
            console.log(result);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};
function LoadHoaDonChiTiet(id) {
    if (id == "" || id == null || id == undefined) {
        console.log("Thất bại.");
    } else {
        var data = {
            ID: id
        }
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/HoaDonChiTiet', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                if (result != null) {

                    var DanhSachPhanTrang = document.getElementById("DanhSachPhanTrang");
                    DanhSachPhanTrang.classList.remove("hidden");
                    var ThanhToan = document.getElementById("ThanhToan");
                    ThanhToan.classList.remove("hidden");

                    var HoaDonChiTiet = document.getElementById("HoaDonChiTiet");
                    HoaDonChiTiet.classList.remove("hidden");
                    var danhsachsanphamhdct = document.getElementById("danhsachsanphamhdct");
                    danhsachsanphamhdct.classList.remove("hidden");

                    var HoaDonCho = document.getElementById("HoaDonCho");
                    HoaDonCho.classList.add("hidden");


                    var ThongTin = document.getElementById("ThongTin");
                    ThongTin.classList.add("hidden");

                    document.getElementById("hoten").innerText = result.tenKhachHang;
                    document.getElementById("sdtkh").innerText = result.sdtKhachHang;
                    document.getElementById("mahoadon").innerText = result.maHoaDon;
                    document.getElementById("ngaytao").innerText = result.ngayTao;
                    document.getElementById("idhd").innerText = result.id;
                };
                LstHoaDonChiTiet(id);
                console.log(result);
            },
            error: function (error) {
                console.error('Lỗi khi lấy dữ liệu từ Session:', error);
            }
        });
    }
    return;
};
function LoadTien(idhd) {
    var data = {
        ID: idhd
    }
    $.ajax({
        type: 'POST',
        url: '/Admin/BanHangTaiQuay/HoaDonChiTiet', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (result) {
            if (result != null) {
                var thanhtien = result.thanhTien.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                var khuyenmai = result.tienGiamGia.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                var thanhtoan = (result.thanhTien - result.tienGiamGia).toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                $('label[id="thanhtien"]').text(thanhtien);
                $('label[id="khuyenmai"]').text(khuyenmai);
                $('label[id="thanhtoanhd"]').text(thanhtoan);
                console.log(thanhtoan);
            };
           
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
}
function Refresh() {
    const idhd2 = document.getElementById("idhd").innerText;
    LstHoaDonChiTiet(idhd2);
};
function LstHoaDonChiTiet(id) {
    if (id == null || id == undefined || id == "") {

    } else {
        var data = {
            ID: id
        }
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/LstHoaDonChiTiet', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                $("#sanpham").empty();
                var content = '';
                if (result.length > 0) {
                    var stt = 0;
                    for (var i = 0; i < result.length; i++) {
                        var gia = result[i].gia.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                        var thanhtien = result[i].thanhTien.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                        stt++;
                        content += '<tr>'
                        content += '<td style="text-align: center; ">' + stt + '</td>';
                        content += '<td><a href="#" data-toggle="modal" data-target="#myModal" data-id="' + result[i].idspct + '">' + result[i].tenSP + '</a> (' + result[i].mauSac + ', ' + result[i].size + ')</td>';/* */
                        content += '<td>' + gia + '</td>';
                        content += '<td>';
                        content += '<div class="soluong" style="width: 70px;">';
                        content += '<button class="GiamSoLuong" onclick="GiamSoLuong1(\'' + result[i].id + '\');"><i class="fas fa-minus"></i></button>';
                        content += '<input type="text" onblur="UpdateSoLuongHDCT(\'' + result[i].id + '\');" id="' + result[i].id + '" name="SoLuong" value="' + result[i].soLuong + '" style="width: 50px;text-align: center; "/>';
                        content += '<button class="TangSoLuong" onclick="TangSoLuong1(\'' + result[i].id + '\');"><i class="fas fa-plus"></i></button>';
                        content += '</div>';
                        content += '</td>';
                        content += '<td>' + thanhtien + '</td>';
                        content += '<td style="text-align: center;"><button onclick="XoaSanPhamTrongHoaDonChiTiet(\'' + result[i].id + '\');" style="border: none;background-color: white; "><i class="fas fa-trash-alt" style="color: #ff0000;font-size: 20px;"></i></button></td>';
                        content += '</tr>';
                    }
                    $("#sanpham").append(content);
                } else {

                    content += '<tr>';
                    content += 'td colspan="6" style="text-align: center;">Không có bản ghi.</td>';
                    content += '</tr>';
                    $("#sanpham").append(content);
                }
                UpdateHoaDonTaiQuay(id);
                LoadTien(id);
                console.log(result);
            },
            error: function (error) {
                console.error('Lỗi khi lấy dữ liệu từ Session:', error);
            }
        });
    }

    return;
};
function XoaSanPham(id) {
    var idhd1 = document.getElementById("idhd").innerText;
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
                console.log(result);
                LstHoaDonChiTiet(idhd1);
                LayIDHd();
            },
            error: function (error) {

            }
        });
    }
    return;
};
function GiamSoLuong1(id) {
    var stringid = String(id);
    var quantityInput = $('input[id="' + stringid + '"]').val();
    var soluong = parseInt(quantityInput);
    var currentValue = soluong;
    if (currentValue > 1) { // Đảm bảo số lượng không nhỏ hơn 1
        var soluongsautru = currentValue - 1;
        $('input[id="' + stringid + '"]').val(soluongsautru);
        UpdateSoLuongHDCT(stringid);
    }
    event.preventDefault();
    return;
};
function TangSoLuong1(id) {
    var stringid = String(id);
    var quantityInput = $('input[id="' + stringid + '"]').val();
    var soluong = parseInt(quantityInput);
    var currentValue = soluong;
    var soluongsautru = currentValue + 1;
    $('input[id="' + stringid + '"]').val(soluongsautru);
    UpdateSoLuongHDCT(stringid);

    event.preventDefault();
    return;
};
function UpdateSoLuongHDCT(id) {
    const idhd2 = document.getElementById("idhd").innerText;
    var soluong = $('input[id="' + id + '"]').val();
    var data = {
        ID: id,
        SoLuong: soluong,
    }
    $.ajax({
        type: 'POST',
        url: '/Admin/BanHangTaiQuay/CapNhatSoLuongHDCT',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (result) {
            if (result.message == "Cập nhật số lượng thành công") {

            }
            else if (result.message = "Sửa thất bại.") {
                alert("Cập nhật số lượng không thành công.");
            }
            console.log(result);
            LstHoaDonChiTiet(idhd2);
        },
        error: function (error) {

        }
    });
    return;
};
function UpdateHoaDonTaiQuay(id) {
    var data = {
        ID: id,
    }
    $.ajax({
        type: 'POST',
        url: '/KhachHang/HoaDonKH/UpdateHoaDon',
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        success: function (result) {
            console.log(result);
            LoadTien(id);
        },
        error: function (error) {

        }
    });
    return;
};
function TienTraKhach() {
    const thanhtoanhd = $('label[id="thanhtoanhd"]').text();
    const thanhtoanhdnguyenthuy = parseInt(thanhtoanhd.replace(/[^\d]/g, ''));
    const Tienkhachdua = $('input[id="Tienkhachdua"]').val();

    const tientrakhach = Tienkhachdua.replace(/\./g, '') - thanhtoanhdnguyenthuy;
    const tienkhachhoanthanh = tientrakhach.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });;
    console.log(tienkhachhoanthanh);
    if (tientrakhach < 0) {
        const thongbao = "Không thể thanh toán thiếu: " + tienkhachhoanthanh;
        $('span[id="tientrakhach"]').text(thongbao);

    } else {
        const thongbao = "Trả lại khách: " + tienkhachhoanthanh;
        $('span[id="tientrakhach"]').text(thongbao);
    }
    if (tientrakhach < 0) {
        document.getElementById("tientrakhach").style.color = "red";
        document.getElementById("button_thanhtoan").style.pointerEvents = "none";

    } else {
        document.getElementById("tientrakhach").style.color = "blue";
        document.getElementById("button_thanhtoan").style.pointerEvents = "auto";
    }
    return;
};
function Thanhtoantaiquay() {
    const tienkhachdua = document.getElementById("Tienkhachdua").value;
    const thanhtien = document.getElementById("thanhtien").innerText;
    const thanhtiennguyenthuy = thanhtien.replace(/\./g, '');

    if (tienkhachdua == "") {
        document.getElementById("errorTienKhachDua").innerText = "Mời nhập tiền.";
        return;
    } else if (thanhtiennguyenthuy == 0) {
        alert("Bạn không thể thanh toán vì không có sản phẩm nào.");
        return;
    }
    else {
        const idhd = $('span[id="idhd"]').text();
        var data = {
            ID: idhd,
            TrangThai: 1,
        }
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/HoaDonChoXuLy', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                alert(result.message);
                console.log(result);
                window.location.href = '/Admin/BanHangTaiQuay/BanHang';
            },
            error: function (error) {
                console.error('Lỗi khi lấy dữ liệu từ Session:', error);
            }
        });
    };
    return;
}
function LayHoaDonChoMoiNhat() {
    $.ajax({
        type: 'GET',
        url: '/Admin/BanHangTaiQuay/LayHoaDonChoMoiNhat', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            if (result != null) {
                document.getElementById("hoten").innerText = result.tenKhachHang;
                document.getElementById("sdtkh").innerText = result.sdtKhachHang;
                document.getElementById("mahoadon").innerText = result.maHoaDon;
                document.getElementById("ngaytao").innerText = result.ngayTao;
                document.getElementById("idhd").innerText = result.id;
                idhd = result.id;
            }
            console.log(result);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
}
function XoaSanPhamTrongHoaDonChiTiet(id) {
    const idhd2 = document.getElementById("idhd").innerText;
    $.ajax({
        type: 'POST',
        url: '/Admin/BanHangTaiQuay/XoaSanPhamTrongHoaDonChiTiet', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(id),
        success: function (result) {
            if (result != null) {
                if (result.message == "Xóa Thành Công.") {
                    alert("Xóa Thành Công.");
                }
                else if (result.message = "Xóa Thất Bại.") {
                    alert("Xóa Thất Bại.");
                } else {
                    alert("Lỗi.")
                }
            }
            console.log(result);
            LstHoaDonChiTiet(idhd2);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};
function Duyethoadoncho(event) {
    event.preventDefault();
    //GetHoaDonCho();
    //var DanhSachPhanTrang = document.getElementById("DanhSachPhanTrang");
    //DanhSachPhanTrang.classList.add("hidden");
    //var ThanhToan = document.getElementById("ThanhToan");
    //ThanhToan.classList.add("hidden");

    //var HoaDonChiTiet = document.getElementById("HoaDonChiTiet");
    //HoaDonChiTiet.classList.add("hidden");
    //var danhsachsanphamhdct = document.getElementById("danhsachsanphamhdct");
    //danhsachsanphamhdct.classList.add("hidden");


    //var HoaDonCho = document.getElementById("HoaDonCho");
    //HoaDonCho.classList.remove("hidden");
    //var ThongTin = document.getElementById("ThongTin");
    //ThongTin.classList.remove("hidden");
    window.location.href = "/Admin/BanHangTaiQuay/BanHang";
    return;
};
function validateName() {
    var tenkhachhang = document.getElementById("name");
    var errorten = document.getElementById("errorten");

    // Kiểm tra điều kiện nếu tên rỗng
    if (tenkhachhang.value.trim() === '') {
        errorten.innerText = "Tên không được để trống";

    } else {
        errorten.textContent = ""; // Xóa thông báo lỗi nếu tên hợp lệ
    }
};
function validateSDT() {
    var sdtkh = document.getElementById("sdt");
    var errorsdt = document.getElementById("errorsdt");
    var phoneRegex = /^0\d{9}$/;
    // Kiểm tra điều kiện nếu tên rỗng
    if (sdtkh.value.trim() === '') {
        errorsdt.innerText = "Số điện thoại không được để trống";
    } else if (!phoneRegex.test(sdtkh.value)) {
        errorsdt.innerText = "Số điện thoại có 10 số và bắt đầu bằng số 0.";
    } else {
        errorsdt.innerText = ""; // Xóa thông báo lỗi nếu số điện thoại hợp lệ
    }
};
function validateEmail() {
    var email = document.getElementById("email");
    var erroremail = document.getElementById("erroremail");
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    // Kiểm tra điều kiện nếu tên rỗng
    if (email.value.trim() === '') {
        erroremail.innerText = "Email không được để trống"; // Check rỗng
    }
    else if (!emailRegex.test(email.value)) {
        erroremail.innerText = "Vui lòng nhập đúng dạng email."; // Check định dạng
    }
    else {
        erroremail.innerText = "";

    }
    TruyenEmail();
    console.log(email);
};
function ThemNguoiDungTaiQuay() {
    var name = document.getElementById("name").value;
    var sdt = document.getElementById("sdt").value;
    var email = document.getElementById("email").value;
    if (name == "" || sdt == "" || email == "") {
        alert("Bạn chưa nhập đầy đủ thông tin");

    } else {
        var data = {
            TenNguoiDung: name,
            SDT: sdt,
            Email: email,
        }
        $.ajax({
            type: 'POST',
            url: '/Admin/BanHangTaiQuay/ThemNguoiDungMoi', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(data),
            success: function (result) {
                alert(result.message);
                console.log(result);
                window.location.href = '/Admin/BanHangTaiQuay/BanHang';
            },
            error: function (error) {
                console.error('Lỗi khi lấy dữ liệu từ Session:', error);
            }
        });
    }
    return;
};
function DanhSachHinhThucJson1() {
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
                    if (result[i].hinhThucThanhToan == "Thanh toán tại quầy") {
                        content += '<span class="paymant-method__item-cutom-checkbox custom-radio">';
                        content += '<input type="radio" id="payment-COD" name="payment-method" value="' + result[i].id + '" />';
                        content += '</span>';
                        content += '<span class="payment-method__item-name"> Thanh toán tại quầy</span>';
                        content += '<br>';
                    }
                    // Thanh toán ATM Nội địa
                    if (result[i].hinhThucThanhToan == "Thanh toán online") {
                        content += '<span class="paymant-method__item-cutom-checkbox custom-radio">';
                        content += '<input  type="radio" style="margin-left: 0px;" id="bankcode_Vnbank" name="payment-method" value="' + result[i].id + '" />';
                        content += '</span>';
                        content += '<span class="payment-method__item-name"> Thanh toán online</span>';
                        content += '<br>';
                    }
                }
            }
            content += '</label>';
            $("#HinhThucThanhToan").append(content);
            const btnThanhToan = document.getElementById('button_thanhtoan');
            const btn_online = document.getElementById('button_thanhtoanon');

            const tienkhachdua = document.getElementById('tienkhachdua1');
            const tientrakhach = document.getElementById('tientrakhach1');
            const radioButtons = document.querySelectorAll('label input[type="radio"]');
            radioButtons.forEach(radioButton => {
                radioButton.addEventListener('click', function () {
                    idthanhtoan = this.value;
                    TruyenIdThanhToan(this.value, this.id);
                });
            });
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
    return;
};
function TruyenIdThanhToan(idthanhtoan, value) {
    $.ajax({
        type: 'POST',
        url: '/Admin/BanHangTaiQuay/NhanIDthanhToan', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(idthanhtoan),
        success: function (result) {
            if (value === 'bankcode_Vnbank') {
                ThanhToanOnline1(idthanhtoan);
                btnThanhToan.style.display = 'none';
                btn_online.style.display = 'block';
                tienkhachdua.style.display = 'none';
                tientrakhach.style.display = 'none';
            } else {
                btnThanhToan.style.display = 'block';
                btn_online.style.display = 'none';
                tienkhachdua.style.display = 'block';
                tientrakhach.style.display = 'block';
            }
        },
        error: function (result) {
            console.log("Error:" + result);
        },
    });
    return;
};
function ThanhToanOnline1(idthanhtoan) {
    const tongtien = document.getElementById("thanhtoanhd").innerText;
    if (tongtien == "" || tongtien == undefined || tongtien == null) {
        alert("Bạn không thể thanh toán vì chưa mua sản phẩm.");
        return;
    } else {
        const idhd = document.getElementById("idhd").innerText;
        const number = parseFloat(tongtien.replace(/[^\d]/g, ''));
        var data = {
            TongTien: number,
            IDHinhThucThanhToan: idthanhtoan,
            ID: idhd,
            TrangThai: 2
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
            }
        });
    }
    return;
};