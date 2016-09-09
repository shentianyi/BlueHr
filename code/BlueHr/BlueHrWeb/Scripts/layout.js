var Layout = {};

Layout.init = function () {
    //IE 提示console找不到 解决办法
    window.console = window.console || (function () {
        var c = {}; c.log = c.warn = c.debug = c.info = c.error = c.time = c.dir = c.profile = c.clear = c.exception = c.trace = c.assert = function () { };
        return c;
    })();

    $('.sidebar-menu li').removeClass('active');
    var pathname = window.location.pathname.split('/');

    switch (pathname[1]) {
        case "Company":
            $('.nav-company').addClass('active');
            PageAction('#company', '新建公司', '编辑公司', '公司详情', '创建', '更新', '删除');
            break;
        case "Department":
            $('.nav-department').addClass('active');
            PageAction('#department', '新建部门', '编辑部门', '部门详情', '创建', '更新', '删除');
            break;
        case "Staff":
            $('.nav-staff').addClass('active');
            PageAction('#staff', '新建员工', '编辑员工', '员工详情', '创建', '更新', '删除');
            break;
        case "AttendanceRecordDetail":
            $('.nav-attendance-record').addClass('active');
            PageAction('#attendance_record', '新建考勤数据', '编辑考勤数据', '考勤数据详情', '创建', '更新', '删除');
            break;
        case "AttendanceRecordCal":
            $('.nav-attendance-record').addClass('active');
            PageAction('#staff', '新建统计记录', '编辑统计记录', '统计记录详情', '创建', '更新', '删除');
            break;
        case "CertificateType":
            $('.nav-basic').addClass('active');
            PageAction('#certificatetype', '新建证照类型', '编辑证照类型', '证照类型详情', '创建', '更新', '删除');
            break;
        case "JobTitle":
            $('.nav-basic').addClass('active');
            PageAction('#jobtitle', '新建职位', '编辑职位', '职位详情', '创建', '更新', '删除');
            break;
        case "DegreeType":
            $('.nav-basic').addClass('active');
            PageAction('#degreetype', '新建学历', '编辑学历', '学历详情', '创建', '更新', '删除');
            break;
        case "StaffType":
            $('.nav-basic').addClass('active');
            PageAction('#stafftype', '新建人员类型', '编辑人员类型', '人员类型详情', '创建', '更新', '删除');
            break;
        case "InSureType":
            $('.nav-basic').addClass('active');
            PageAction('#insuretype', '新建保险类别', '编辑保险类别', '保险类别详情', '创建', '更新', '删除');
            break; 
        case "ResignType":
            $('.nav-basic').addClass('active');
            PageAction('#resigntype', '新建离职类型', '编辑离职类型', '离职类型详情', '创建', '更新', '删除');
            break;
        case "AbsenceType":
            $('.nav-basic').addClass('active');
            PageAction('#absencetype', '新建缺勤类型', '编辑缺勤类型', '缺勤类型详情', '创建', '更新', '删除');
            break;
        case "Certificate":
            $('.nav-basic').addClass('active');
            PageAction('#certificate', '新建证照', '编辑证照', '证照详情', '创建', '更新', '删除');
            break;
        case "ExtraWorkType":
            $('.nav-basic').addClass('active');
            PageAction('#extraworktype', '新建加班类型', '编辑加班类型', '加班类型详情', '创建', '更新', '删除');
            break;
        case "AbsenceRecrod":
            $('.nav-basic').addClass('active');
            PageAction('#absencerecord', '新建缺勤', '编辑缺勤', '缺勤详情', '创建', '更新', '删除');
            break;
            
        case "TaskRound":
            $('.nav-taskround').addClass('active');
        default:
            break;
    }

    function PageAction(id, newAction, editAction, deleteAction, newBtn, editBtn, deleteBtn) {
        var vueName = new Vue({
            el: id,
            data: {
                action: newAction,
                actionBtn: newBtn
            }
        });

        if (pathname[pathname.length - 2] == "Edit") {
            vueName.action = editAction;
            vueName.actionBtn = editBtn;
        } else if (pathname[pathname.length - 2] == "Delete") {
            vueName.action = deleteAction;
            vueName.actionBtn = deleteBtn;
        }
    }
}

Layout.CompanyAndDepartment = function (companyId, departmentId) {
    var Department = new Array();

    $.ajax({
        url: '/Staff/GetCompanyAndDepartment',
        type: 'get',
        async: false,
        success: function (data) {
            Department = data;
        },
        error: function () {
            console.log("获取公司和部门信息出错");
        }
    });

    $(companyId).change(function () {
        $(departmentId).empty();

        var companyID = $(companyId).find("option:selected").val();
        var Html = "";

        for (dep in Department[companyID]) {
            Html += '<option value=' + dep + '>' + Department[companyID][dep] + '</option>';
        }

        $(Html).appendTo(departmentId);
    })
}

Layout.popMsg = function (cls, content) {
    var Html = "<div class='" + cls + "'><div class='popMsg-body'> " + content + "</div></div>";

    $(Html).appendTo($('body'));

    window.setTimeout(function () {
        $("." + cls).slideUp();
    }, 3000);
}

//用来设置 body 的高度
Layout.TbodyHeight = function (cls, height) {
    $(cls).css({ height: $(window).height() - height + 'px' });

    $(window).resize(function () {
        $(cls).css({ height: $(window).height() - height + 'px' });
    });

    $(cls).mCustomScrollbar({
        scrollInertia: 600,
        autoDraggerLength: false,
        advanced: { autoScrollOnFocus: false }
    });
}

Layout.datepicker = function (date_picker) {
    $(date_picker).datetimepicker({
        format: 'Y-m-d',
        scrollInput:false,
        timepicker: false
    });
}

Layout.datetimepicker = function (date_time_picker) {
    $(date_time_picker).datetimepicker({
        scrollInput: false
    });
}

Layout.rangedatepicker = function (date_picker_start, date_picker_end) {
    $(date_picker_start).datetimepicker({
        format: 'Y-m-d',
        onShow: function (ct) {
            this.setOptions({
                maxDate: $(date_picker_end).val() ? $(date_picker_end).val() : false
            })
        },
        scrollInput: false,
        timepicker: false
    });

    $(date_picker_end).datetimepicker({
        format: 'Y-m-d',
        onShow: function (ct) {
            this.setOptions({
                minDate: $(date_picker_start).val() ? $(date_picker_start).val() : false
            })
        },
        scrollInput: false,
        timepicker: false
    });
}

//可以扩展， 使用typeof 进行判断然后进行判断
Layout.IsStringNull = function (str) {
    if (str == null || str == "") {
        return true;
    } else {
        return false;
    }
}