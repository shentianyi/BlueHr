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

                    Certificate.FillHiddenInput(data.Content, "ADD");
                    Certificate.AddTmpAttachment(data.Content);

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
Certificate.ImgExtensionList = [".jpg", ".png", ".jpeg", ".bmp", ".gif"];


Certificate.RndNum = function (n) {
    var rnd = "";
    for (var i = 0; i < n; i++)
        rnd += Math.floor(Math.random() * 10);
    return rnd;
}

//根据QueryString参数名称获取值 
Certificate.getQueryStringByName = function (name) {

    var result = location.search.match(new RegExp("[\?\&]" + name + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }

    return result[1];
}

//添加上传文件到文件列表
Certificate.AddTmpAttachment = function (atchName) {

    console.log(atchName);

    var showAtchName = atchName.split('|').length > 0 ? atchName.split('|')[0] : "";
    var atchPath = "" + (atchName.split('|').length > 0 ? atchName.split('|')[1] : "");
    var downloadPath = "/UploadCertificate/" + Certificate.staffNr + "/" + (atchName.split('|').length > 0 ? atchName.split('|')[1] : "");
    var atchId = Certificate.RndNum(5);
    var theFileExtension = showAtchName.replace(showAtchName.substr(0, showAtchName.lastIndexOf(".")), "");
    console.log(theFileExtension);
    console.log(Certificate.ImgExtensionList);
    console.log(Certificate.ImgExtensionList.indexOf(theFileExtension));

    var Html = "";
    Html += '<tr>';

    //判断文件类型是否是图片-添加magnificPopup效果
    if (Certificate.ImgExtensionList.indexOf(theFileExtension) >= 0) {
        Html += '    <td><a class="img-popup-link" href="' + downloadPath + '" id="' + "atch_show_" + atchId + '">' + showAtchName + '</a></td>';
    }
    else {
        Html += '    <td><a href="/Certificate/DownFile?fileName=' + showAtchName + '&filePath=' + downloadPath + '" id="' + "atch_show_" + atchId + '">' + showAtchName + '</a></td>';
    }

    Html += '    <td class="option-icon-primary">';
    Html += '        <a href="' + '/Certificate/DownFile?fileName=' + showAtchName + '&filePath=' + downloadPath + '">';
    Html += '        <i class="fa fa-download" id="' + "atch_down_" + atchId + '" style="margin-top:3px;"></i>';
    Html += '        </a>';
    Html += '        <i class="fa fa-close" id="' + "atch_del_" + atchId + '" style="margin-top:3px;" onclick="Certificate.deleteAtch(' + "'" + "atch_del_" + atchId + "'" + ',' + "'" + atchName + "'" + ')"></i>';
    Html += '    </td>';
    Html += '</tr>';

    $(Html).prependTo('.tbody-family');

    $('.img-popup-link').magnificPopup({
        type: 'image'
        // other options
    });

    //$(".fa fa-download").bind('click', Certificate.downloadAtch(atchId, atchName));
    //$(".fa fa-close").bind('click', Certificate.deleteAtch(atchId, atchName));

    Certificate.FillHiddenInput(atchName);
}

String.prototype.replaceAll = function (s1, s2) {
    return this.replace(new RegExp(s1, "gm"), s2);
}

//上传文件的路径list
Certificate.FillHiddenInput = function (atchName, addOrDel) {

    var theName = $("#athment").val();

    if (addOrDel == "ADD") {
        //添加-拼接字符串
        theName += atchName + ";";
    }
    else if (addOrDel == "DEL") {
        console.log(theName);
        console.log("1---------------------------");

        console.log(atchName + ";");
        console.log("2---------------------------");

        //删除-替换字符串
        theName = theName.replaceAll(atchName + ";", "");

        console.log("3---------------------------");
        console.log(theName);
    }

    $("#athment").val(theName);
}

Certificate.downloadAtch = function (atchId, atchName, atchPath) {
    //onclick="Certificate.downloadAtch(' + "'" + "atch_down_" + atchId + "'" + ',' + "'" + showAtchName + "'" + ',' + "'" + downloadPath + "'" + ')"

    //$.get("/Certificate/DownFile", { fileName: atchName, filePath: atchPath }, function (response) {
    //    Layout.popMsg('popMsg-danger', "下载成功!");
    //});
}

Certificate.deleteAtch = function (atchId, atchName) {

    if (atchId.indexOf('atch_del_') < 0) {
        var atchDelIds = $("#atchDelIds").val();
        atchDelIds += atchId + ";";

        $("#atchDelIds").val(atchDelIds);
    }

    //重新存储需要保存的文件列表
    Certificate.FillHiddenInput(atchName, "DEL");

    $("#" + atchId).parent().parent().remove();
}