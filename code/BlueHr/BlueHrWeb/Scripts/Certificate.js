var Certificate = {};

Certificate.image_upload = function (idStr, format, callback) {
    var valid = true;
    var reg = /(\.|\/)(jpg|png|jpeg|bmp|gif|txt|pdf|docx|doc|xls|xlsx)$/i;
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
        acceptFileTypes: /(\.|\/)(jpg|png|jpeg|bmp|gif|txt|pdf|docx|doc|xls|xlsx)$/i,
        dataType: 'json',
        change: function (e, data) {
            valid = true;
            $.each(data.files, function (index, file) {
                var msg = "上传中 ... ...";
                if (reg) {
                    if (!reg.test(file.name)) {
                        msg = '格式错误';
                        Layout.popMsg('popMsg-danger', msg);
                        valid = false;
                        return;
                    }
                }
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
            } else {
                if (data.Success) {
                    Certificate.AddTmpAttachment(data.Content, "", "");
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
        //progressall: function (e, data) {
        //    var progress = parseInt(data.loaded / data.total * 100, 10);
        //    $('#progress .bar').css(
        //        'width',
        //        progress + '%'
        //    );
        //},

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

//添加上传文件到文件列表
Certificate.AddTmpAttachment = function (atchName, atchPath, atchId) {

    var showAtchName = atchName.split('|').length > 0 ? atchName.split('|')[0] : "";

    var Html = "";

    Html += "<tr>";
    Html += '<td><a id="' + showAtchName + '">' + showAtchName + '</a></td>';
    Html += '<td class="option-icon-primary"><i class="fa-download" id="' + atchId + '" style="margin-top:3px;"></i></td>';
    Html += '<td class="option-icon-danger"><i class="fa fa-close remove-family" id="' + atchId + '" style="margin-top:3px;"></i></td>';
    Html += '</tr>';

    $(Html).prependTo('.tbody-family');

    Certificate.FillHiddenInput(atchName);
}

//上传文件的路径list
Certificate.FillHiddenInput = function (atchName) {
    var theName = $("#athment").val();
    theName += atchName + ";";
    $("#athment").val(theName);
}