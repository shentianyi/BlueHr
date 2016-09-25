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
    div.html('<img id=imghead alt="员工照片">');
    var img = $('#imghead');
    img.css({ maxHeight: '180px', maxWidth: '260px' });
    img.attr('src', $(real_name).val());
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
    } else {
        if (!Staff.CheckBankCardValid(bankCardVal)) {
            return false;
        }
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

Staff.CheckIdValid = function (code) {
    //身份证号合法性验证 
    //支持15位和18位身份证号
    //支持地址编码、出生日期、校验位验证
    var city = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江 ", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北 ", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏 ", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外 " };
    var tip = "";
    var pass = true;

    if (code == "" || code == null) {
        tip = "身份证号不能为空";
        pass = false;
    } else {
        //if (!code || !/^\d{6}(18|19|20)?\d{2}(0[1-9]|1[12])(0[1-9]|[12]\d|3[01])\d{3}(\d|X)$/i.test(code)) {
        //    tip = "身份证号格式错误,请检查";
        //    pass = false;
        //}
        //else if (!city[code.substr(0, 2)]) {
        //    tip = "地址编码错误";
        //    pass = false;
        //}
        //else {
        //    //18位身份证需要验证最后一位校验位
        //    if (code.length == 18) {
        //        code = code.split('');
        //        //∑(ai×Wi)(mod 11)
        //        //加权因子
        //        var factor = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
        //        //校验位
        //        var parity = [1, 0, 'X', 9, 8, 7, 6, 5, 4, 3, 2];
        //        var sum = 0;
        //        var ai = 0;
        //        var wi = 0;
        //        for (var i = 0; i < 17; i++) {
        //            ai = code[i];
        //            wi = factor[i];
        //            sum += ai * wi;
        //        }
        //        var last = parity[sum % 11];
        //        if (parity[sum % 11] != code[17]) {
        //            tip = "最后一位错误,请检查";
        //            pass = false;
        //        }
        //    }
        //}
    }

    if (!pass) Layout.popMsg('popMsg-danger', tip);
    return pass;
}

Staff.CheckBankCardValid = function (bankno) {
    var lastNum = bankno.substr(bankno.length - 1, 1);//取出最后一位（与luhm进行比较）

    var first15Num = bankno.substr(0, bankno.length - 1);//前15或18位
    var newArr = new Array();
    for (var i = first15Num.length - 1; i > -1; i--) {    //前15或18位倒序存进数组
        newArr.push(first15Num.substr(i, 1));
    }
    var arrJiShu = new Array();  //奇数位*2的积 <9
    var arrJiShu2 = new Array(); //奇数位*2的积 >9

    var arrOuShu = new Array();  //偶数位数组
    for (var j = 0; j < newArr.length; j++) {
        if ((j + 1) % 2 == 1) {//奇数位
            if (parseInt(newArr[j]) * 2 < 9)
                arrJiShu.push(parseInt(newArr[j]) * 2);
            else
                arrJiShu2.push(parseInt(newArr[j]) * 2);
        }
        else //偶数位
            arrOuShu.push(newArr[j]);
    }

    var jishu_child1 = new Array();//奇数位*2 >9 的分割之后的数组个位数
    var jishu_child2 = new Array();//奇数位*2 >9 的分割之后的数组十位数
    for (var h = 0; h < arrJiShu2.length; h++) {
        jishu_child1.push(parseInt(arrJiShu2[h]) % 10);
        jishu_child2.push(parseInt(arrJiShu2[h]) / 10);
    }

    var sumJiShu = 0; //奇数位*2 < 9 的数组之和
    var sumOuShu = 0; //偶数位数组之和
    var sumJiShuChild1 = 0; //奇数位*2 >9 的分割之后的数组个位数之和
    var sumJiShuChild2 = 0; //奇数位*2 >9 的分割之后的数组十位数之和
    var sumTotal = 0;
    for (var m = 0; m < arrJiShu.length; m++) {
        sumJiShu = sumJiShu + parseInt(arrJiShu[m]);
    }

    for (var n = 0; n < arrOuShu.length; n++) {
        sumOuShu = sumOuShu + parseInt(arrOuShu[n]);
    }

    for (var p = 0; p < jishu_child1.length; p++) {
        sumJiShuChild1 = sumJiShuChild1 + parseInt(jishu_child1[p]);
        sumJiShuChild2 = sumJiShuChild2 + parseInt(jishu_child2[p]);
    }
    //计算总和
    sumTotal = parseInt(sumJiShu) + parseInt(sumOuShu) + parseInt(sumJiShuChild1) + parseInt(sumJiShuChild2);

    //计算Luhm值
    var k = parseInt(sumTotal) % 10 == 0 ? 10 : parseInt(sumTotal) % 10;
    var luhm = 10 - k;

    if (lastNum == luhm) {
        return true;
    }
    else {
        Layout.popMsg('popMsg-danger', "银行卡号不正确");
        return false;
    }
}