var Layout = {};

Layout.init = function () {
    $('.nav-left dt').removeClass('active');
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
            $('.nav-user').addClass('active');
            $('.nav-staff').addClass('active');
            PageAction('#staff', '新建员工', '编辑员工', '员工详情', '创建', '更新', '删除');
            break;
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

        if(pathname[pathname.length - 2] == "Edit")
        {
            vueName.action = editAction;
            vueName.actionBtn = editBtn;
        } else if (pathname[pathname.length - 2] == "Delete") {
            vueName.action = deleteAction;
            vueName.actionBtn = deleteBtn;
        }
    }
}

Layout.TbodyHeight = function (cls, height) {
    $(cls).css({ height: $(window).height() - height + 'px' });

    $(window).resize(function () {
        $(cls).css({ height: $(window).height() - height + 'px' });
    });

    $(cls).mCustomScrollbar({
        scrollInertia: 600,
        autoDraggerLength: false
    });
}

Layout.datepicker = function (date_picker) {
    $(date_picker).datetimepicker({
        format: 'Y-m-d',
        timepicker: false
    });
}

Layout.datetimepicker = function (date_time_picker) {
    $(date_time_picker).datetimepicker();
}

Layout.rangedatepicker = function (date_picker_start, date_picker_end) {
    $(date_picker_start).datetimepicker({
        format: 'Y-m-d',
        onShow: function (ct) {
            this.setOptions({
                maxDate: $(date_picker_end).val() ? $(date_picker_end).val() : false
            })
        },
        timepicker: false
    });

    $(date_picker_end).datetimepicker({
        format: 'Y-m-d',
        onShow: function (ct) {
            this.setOptions({
                minDate: $(date_picker_start).val() ? $(date_picker_start).val() : false
            })
        },
        timepicker: false
    });
}