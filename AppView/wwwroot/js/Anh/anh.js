$(document).ready(function () {
    var selectedImages = [];
    // Lắng nghe sự kiện click trên checkbox để chọn hoặc bỏ chọn
    $(".image-checkbox").on("click", function () {
        var imageId = $(this).data("image-id");
        var imageFilename = $(this).data("image-filename");
        if ($(this).prop("checked")) {
            // Nếu checkbox được chọn, thêm ảnh vào danh sách đã chọn
            selectedImages.push({ Id: imageId, Connect: imageFilename });
        } else {
            // Nếu checkbox bị bỏ chọn, loại bỏ ảnh khỏi danh sách đã chọn
            selectedImages = selectedImages.filter(function (image) {
                return image.Id !== imageId;
            });
        }
    });

    // Lắng nghe sự kiện click vào nút mở modal
    $("#selectImageBtn").on("click", function () {
        // Rút gọn và hiển thị tên ảnh đã chọn trong trường nhập
        $("#imageFilename").val(selectedImages.map(function (image) {
            var lastIndex = image.Connect.lastIndexOf("\\");
            if (lastIndex !== -1) {
                return "...\\" + image.Connect.substring(lastIndex + 1);
            } else {
                return image.Connect;
            }
        }).join(", "));


        var selectedFilenames = selectedImages.map(function (image) {
            return image.Connect;
        });

        // Gửi danh sách tên ảnh lên controller thông qua AJAX
        $.ajax({
            type: "POST", // Sử dụng phương thức POST
            url: "/Admin/SanPhamChiTiet/LuuAnh", // Thay thế ControllerName và ActionName bằng tên thực của controller và action của bạn
            contentType: "application/json; charset=utf-8", // Chỉ định loại dữ liệu gửi lên là JSON
            data: JSON.stringify(selectedFilenames), // Dữ liệu gửi lên controller dưới dạng JSON
            success: function (data) {
                // Xử lý phản hồi từ controller nếu cần
            },
            error: function () {
                // Xử lý lỗi nếu có
            }
        });
        $('#myModal').modal('hide');
    });
});