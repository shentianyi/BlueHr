var Staff = {};

Staff.image_upload = function (idStr, format, callback) {
    var valid = true;
    var reg = /(\.|\/)(jpg|png|gif)$/i;
    if (format != null) {
        if (format != false) {
            reg = new RegExp('(\.|\/)(' + format + ')$', 'i');
        } else {
            reg = null;
        }
    }

    $(idStr).fileupload({
        url: '/Staff/UploadImage',
        type: 'post',
        singleFileUploads: false,
        acceptFileTypes: /(\.|\/)(jpg|png|gif)$/i,
        dataType: 'json',
        change: function (e, data) {
            valid = true;
            $.each(data.files, function (index, file) {
                var msg = "上传中 ... ...";
                if (reg) {
                    if (!reg.test(file.name)) {
                        msg = '格式错误';
                        alert(msg);
                        valid = false;
                        return;
                    }
                    //show_handle_dialog();
                }

                $('#photoName').html(file.name);
            });
        },
        add: function (e, data) {
            if (valid)
                if (data.submit != null)
                    data.submit();
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader('X-CSRF-Token', $('meta[name="csrf-token"]').attr('content'));
        },
        success: function (data) {
            if (callback) {
                callback(data);
                //hide_handle_dialog();
            } else {
                //hide_handle_dialog();
                if (data.Success) {
                    $('#photo').val(data.Content);
                } else {
                    if (data.ErrorFileFeed) {
                        console.log("上传失败" + data.Content)
                    } else {
                        console.log("上传失败" + data.Content)
                    }
                }
            }
        },
        done: function (e, data) {
        }
    });
}

Staff.create_image_preview = function (file) {
    var MAXWIDTH = 200;
    var MAXHEIGHT = 200;

    var div = document.getElementById('photopreview');
    if (file.files && file.files[0]) {
        div.innerHTML = '<img id=imghead>';
        var img = document.getElementById('imghead');
        img.style.maxHeight = MAXHEIGHT + 'px';
        img.style.maxWidth = MAXWIDTH + 'px';

        var reader = new FileReader();
        reader.onload = function (evt) { img.src = evt.target.result; }
        reader.readAsDataURL(file.files[0]);
    }
    else //兼容IE
    {
        file.select();
        var src = document.selection.createRange().text;
        div.innerHTML = '<img id="imghead" style="max-width: 260px; max-height: 180px;" src="' + src + '">';
    }
}

Staff.other_image_preview = function (photo_name, real_name, photo_preview) {
    //去掉日期前缀
    $(photo_name).html($(real_name).val().substring($(real_name).val().indexOf('_') + 1));

    var div = $(photo_preview);
    div.html('<img id=imghead>');
    var img = $('#imghead');
    img.css({ maxHeight: '180px', maxWidth: '260px' });
    img.attr('src', '../../UploadImage/' + $(real_name).val());
}

