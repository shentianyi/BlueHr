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

Staff.createBankCard = function (cls, bank, bankCard, bankAddress, bankRemark) {
    var bankVal = $(bank).val();
    var bankCardVal = $(bankCard).val();
    var bankAddressVal = $(bankAddress).val();
    var bankRemarkVal = $(bankRemark).val();
    var staffNr = $('#nr').val();

    if (Layout.IsStringNull(bankVal)) {
        Layout.popMsg('popMsg-warning', '银行不能为空');
        return false;
    }

    if (Layout.IsStringNull(bankCardVal)) {
        Layout.popMsg('popMsg-warning', '银行卡号不能为空');
        return false;
    }

    var arr = [bankVal, bankCardVal, bankAddressVal, bankRemarkVal, staffNr];

    $.ajax({
        url: '/Staff/CreateBankCard',
        type: 'post',
        data: {
            bankCard: arr
        },
        success: function (data) {
            console.log(JSON.stringify(data));
            if (data.Success) {
                Staff.add_bankCard(cls, bank, bankCard, bankAddress, bankRemark, data.Content);
                Layout.popMsg('popMsg-success', '新建成功');
            } else {
                Layout.popMsg('popMsg-danger', data.Content);
            }
        },
        error: function () {
            console.log("Ajax 请求失败！");
            Layout.popMsg('popMsg-danger', "Ajax 请求失败！");
        }
    })
}

Staff.add_bankCard = function (cls, bank, bankCard, bankAddress, bankRemark, bankId) {
    var bankVal = $(bank).val();
    var bankCardVal = $(bankCard).val();
    var bankAddressVal = $(bankAddress).val();
    var bankRemarkVal = $(bankRemark).val();

    if (Layout.IsStringNull(bankVal)) {
        Layout.popMsg('popMsg-warning', '银行不能为空');
        return false;
    }

    if (Layout.IsStringNull(bankCardVal)) {
        Layout.popMsg('popMsg-warning', '银行卡号不能为空');
        return false;
    }

    var Html = "<div class='col-lg-3 col-md-3 col-sm-6 col-xs-12 " + cls + "'><div class='card-box'>" +
        "<div class='card-heading'><span>" + bankVal + "</span><input type='hidden' name='bank' value='"+bankVal+"' />" +
        "<i class='pull-right fa fa-close remove-card' id='"+bankId+"' style='color:#c0392b;'></i></div>" +
        "<div class='card-body' name='bankCard' title='"+bankCardVal+"'>" + bankCardVal + "<input type='hidden' name='bankCard' value='"+bankCardVal+"' /></div>" +
        "<div class='card-footer'><span name='bankAddress' >" + bankAddressVal + "</span><input type='hidden' name='bankAddress' value='"+bankAddressVal+"' /> " +
        "<i class='pull-right fa fa-question-circle' title='备注信息： " + bankRemarkVal + "'></i><input type='hidden' name='bankRemark' value='"+bankRemarkVal+"' /></div></div></div>";

    $(Html).prependTo('.bank-show');

    $(bank).val('');
    $(bankCard).val('');
    $(bankAddress).val('');
    $(bankRemark).val('');

    Staff.remove_bankCard_ById();
};

Staff.remove_bankCard_ById = function () {
    $('.bank-show').unbind('click').on('click', '.remove-card', function () {
        var this_card = $(this);
        var Id = $(this).attr('id');

        $.ajax({
            url: '/Staff/DeleteBankCardById',
            type: 'post',
            data: {
                id: Id
            },
            success: function (data) {
                if (data.Success) {
                    this_card.parent().parent().parent().remove();
                    //删除成功
                    Layout.popMsg('popMsg-success', data.Content);
                    console.log(data.Content);
                } else {
                    //删除失败
                    Layout.popMsg('popMsg-danger', data.Content);
                    console.log(data.Content);
                }
            },
            error: function () {
                console.log("ajax 请求失败!");
                Layout.popMsg('popMsg-danger', "Ajax 请求失败！");
            }
        })
    });
}

Staff.remove_bankCard = function () {
    $('.remove-card').click(function () {
        $(this).parent().parent().parent().remove();
    });
}

Staff.createFamily = function (familyName, familyType, familyBirthday) {
    var familyNameVal = $(familyName).val();
    var familyTypeVal = $(familyType).val();
    var familyBirthdayVal = $(familyBirthday).val();
    var staffNr = $('#nr').val();

    if (Layout.IsStringNull(familyNameVal)) {
        Layout.popMsg('popMsg-warning', '子女姓名不能为空');
        return false;
    }

    if (Layout.IsStringNull(familyTypeVal)) {
        Layout.popMsg('popMsg-warning', '成员关系不能为空，请填写 女儿/儿子');
        return false;
    }

    if (Layout.IsStringNull(familyBirthdayVal)) {
        Layout.popMsg('popMsg-warning', '子女出生日期不能为空');
        return false;
    }

    var arr = [familyNameVal, familyTypeVal, familyBirthdayVal, staffNr];

    $.ajax({
        url: '/Staff/CreateFamily',
        type: 'post',
        data: {
            family: arr
        },
        success: function (data) {
            console.log(JSON.stringify(data));
            if (data.Success) {
                Staff.add_family(familyName, familyType, familyBirthday, data.Content);
                $('.tbody-family input').attr('disabled', 'disabled');
                Layout.popMsg('popMsg-success', '新建成功');
            } else {
                Layout.popMsg('popMsg-danger', data.Content);
            }
        },
        error: function () {
            console.log("Ajax 请求失败！");
            Layout.popMsg('popMsg-danger', "Ajax 请求失败！");
        }
    })

}

Staff.add_family = function (familyName, familyType, familyBirthday, familyId) {
    var familyNameVal = $(familyName).val();
    var familyTypeVal = $(familyType).val();
    var familyBirthdayVal = $(familyBirthday).val();

    if (Layout.IsStringNull(familyNameVal)) {
        Layout.popMsg('popMsg-warning', '子女姓名不能为空');
        return false;
    }

    if (Layout.IsStringNull(familyTypeVal)) {
        Layout.popMsg('popMsg-warning', '成员关系不能为空，请填写 女儿/儿子');
        return false;
    }

    if (Layout.IsStringNull(familyBirthdayVal)) {
        Layout.popMsg('popMsg-warning', '子女出生日期不能为空');
        return false;
    }

    var Html = "<tr><td><input type='text' class='marco-input-primary' name='familyName' value='" + familyNameVal + "' /></td> " +
        "<td><input type='text' class='marco-input-primary' name='familyType' value='" + familyTypeVal + "' /></td>" +
        "<td><input type='text' class='marco-input-primary date-picker' name='familyBirthday' value='" + familyBirthdayVal + "' /></td>" +
        "<td class='option-icon-danger'><i class='fa fa-close remove-family' id='" + familyId + "' style='margin-top:3px;'></i></td></tr>";

    $(Html).prependTo('.tbody-family');

    $(familyName).val('');
    $(familyType).val('');
    $(familyBirthday).val('');

    Layout.datepicker('.date-picker');

    Staff.remove_family_ById();
}

Staff.remove_family_ById = function () {
    $('.tbody-family').unbind('click').on('click', '.remove-family', function () {
        var this_family = $(this);
        var Id = $(this).attr('id');

        $.ajax({
            url: '/Staff/DeleteFamilyById',
            type: 'post',
            data: {
                id: Id
            },
            success: function (data) {
                if (data.Success) {
                    this_family.parent().parent().remove();
                    //删除成功
                    Layout.popMsg('popMsg-success', data.Content);
                    console.log(data.Content);
                } else {
                    //删除失败
                    Layout.popMsg('popMsg-danger', data.Content);
                    console.log(data.Content);
                }
            },
            error: function () {
                Layout.popMsg('popMsg-danger', "Ajax 请求失败！");

                console.log("ajax 请求失败！")
            }
        })
    });
}

Staff.remove_family = function () {
    $('.remove-family').click(function () {
        $(this).parent().parent().remove();
    });
}