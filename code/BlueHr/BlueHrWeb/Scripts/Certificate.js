var Certificate = {};

Certificate.image_upload = function (idStr, format, callback) {
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
        url: '/Certificate/UploadImage',
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
        //add: function (e, data) {
        //    data.context = $('<button/>').text('Upload')
        //        .appendTo(document.body)
        //        .click(function () {
        //            $(this).replaceWith($('<p/>').text('Uploading...'));
        //            data.submit();
        //        });
        //},
        beforeSend: function (xhr) {
            xhr.setRequestHeader('X-CSRF-Token', $('meta[name="csrf-token"]').attr('content'));
        },
        success: function (data) {

            console.log(data);

            Certificate.AddTmpAttachment(data.Content, "111", "1");

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
        },

        //显示上传进度条
        progressall: function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $('#progress .bar').css(
                'width',
                progress + '%'
            );
        },

        formData: {
            'staffNr': this.staffNr
        },
    });
}

Certificate.staffNr = "";

//根据QueryString参数名称获取值 
Certificate.getQueryStringByName = function (name) {

    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));

    if (result == null || result.length < 1) {
        return "";
    }

    return result[1];
}

Certificate.AddTmpAttachment = function (attachmentName, attachmentPath, attachmentId) {

    var Html = "";

    //http://localhost:15675/UploadCertificate/
    Html += "<tr>";
    Html += '<td><a id="' + attachmentName + '">' + attachmentName + '</a></td>';
    Html += '<td class="option-icon-primary"><i class="fa-download" id="' + attachmentId + '" style="margin-top:3px;"></i></td>';
    Html += '<td class="option-icon-danger"><i class="fa fa-close remove-family" id="' + attachmentId + '" style="margin-top:3px;"></i></td>';
    Html += '</tr>';

    $(Html).prependTo('.tbody-family');

    Certificate.FillHiddenInput(attachmentName);
}

Certificate.FillHiddenInput = function (attachmentName) {
    var theName = $("#athment").val();
    theName += attachmentName + ";";
    $("#athment").val(theName);
}