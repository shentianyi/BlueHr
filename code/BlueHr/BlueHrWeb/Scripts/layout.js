function show_handle_dialog() {
    document.getElementById('handle-dialog-modal').style.display = 'block';
    document.getElementById('dialog-overlay').style.display = 'block';
}

function hide_handle_dialog() {
    document.getElementById('handle-dialog-modal').style.display = 'none';
    document.getElementById('dialog-overlay').style.display = 'none';
}


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
            case "Home":
                $('.nav-myHome').addClass('active');       
                break;
            case "SysRole":
                $('.nav-userAuthorization').addClass('active');
                $('.nav-sysRole').addClass('active');
                PageAction('#sysrole', '新建权限', '编辑权限', '权限详情', '创建', '更新', '删除');
                if(pathname[2]=="AssignAuth"||pathname[2]=="TableShow"){
                    $(".main-header").remove();
                    $(".main-sidebar").remove();
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                    $(window).resize(function(){
                        $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                    })
                }
                break;  
            case "Company":
                $('.nav-organization').addClass('active');
                if(pathname[2]=="Index"){
                    $('.nav-company-list').addClass('active');
                    PageAction('#company', '新建公司', '公司编辑', '公司详情', '创建', '更新', '删除');
                } 
                if(pathname[2]=="TreeShow"){
                    $('.nav-organizationTreeShow').addClass('active');
                }
                PageAction('#company', '新建公司', '公司编辑', '公司详情', '创建', '更新', '删除');
                break;
            case "Department":
                $('.nav-organ').addClass('active');
                PageAction('#department', '新建部门', '编辑部门', '部门详情', '创建', '更新', '删除');
                break;	
        	case "Period":
                $('.nav-person').addClass('active');
                PageAction('#period', '新建员工', '编辑员工', '员工详情', '创建', '更新', '删除');
                break;
            case "AttendanceRecordDetail":
                $('.nav-attendance').addClass('active');
                $('.nav-attendancerecorddetail').addClass('active');
                PageAction('#attendance_record', '新建考勤详细数据', '编辑考勤详细数据', '考勤详细数据详情', '创建', '更新', '删除');
                break;
            case "AttendanceRecordCal":
                if(pathname[2]=="Index"){
                    $('.nav-attendancerecordcal').addClass('active');
                    $('.nav-attendance').addClass('active');
                }
                if(pathname[2]=="ExceptionList"){
                    $('.nav-bugstatistic').addClass('active');
                    $('.nav-attendance').addClass('active');
                }  
                PageAction('#attendancerecordcal', '新建统计记录', '编辑统计记录', '统计记录详情', '创建', '更新', '删除');          
                break;
            case "User":
                if(pathname[2]=="Index"){
                    $('.nav-user').addClass('active');
                    $('.nav-userAuthorization').addClass('active');
                }
                PageAction('#user', '新建用户', '编辑用户', '用户详情', '创建', '更新', '删除');
                break;
        	case "AbsenceRecrod":
                $('.nav-attendance-record').addClass('active');
                PageAction('#attendancerecordcal', '新建统计记录', '编辑统计记录', '统计记录详情', '创建', '更新', '删除');
                break;
            case "CertificateType":
                $('.nav-basic-info').addClass('active');
                $('.nav-certificateType').addClass('active');
                PageAction('#certificatetype', '新建证照类型', '编辑证照类型', '证照类型详情', '创建', '更新', '删除');
                break;
            case "JobTitle":
                $('.nav-jobtitle').addClass('active');
                $('.nav-basic-info').addClass('active');
                PageAction('#jobtitle', '新建职位', '编辑职位', '职位详情', '创建', '更新', '删除');
                break;
            case "DegreeType":
                $('.nav-basic-info').addClass('active');
                $('.nav-degreeType').addClass('active');
                PageAction('#degreetype', '新建学历', '编辑学历', '学历详情', '创建', '更新', '删除');
                break;
            
            case "FullMemberRecord":
                $('.nav-regular').addClass('active');
                $('.nav-laborrelations').addClass('active');

                if(pathname[2] == "Index"){
                    $('.nav-regular').addClass('active');
                    $('.nav-laborrelations').addClass('active');
                }
                PageAction('#fullMemberRecord', '新建转正申请', '编辑转正申请', '转正申请详情', '创建', '更新', '删除');
                break;
            case "StaffType":
                $('.nav-basic-info').addClass('active');
                $('.nav-staffType').addClass('active');
                PageAction('#stafftype', '新建人员类型', '编辑人员类型', '人员类型详情', '创建', '更新', '删除');
                break;
        	case "Period":
                $('.nav-person').addClass('active');
                PageAction('#period', '新建人员类型', '编辑人员类型', '人员类型详情', '创建', '更新', '删除');
                break;
            case "InSureType":
                $('.nav-basic-info').addClass('active');
                $('.nav-inSureType').addClass('active');
                PageAction('#insuretype', '新建保险类别', '编辑保险类别', '保险类别详情', '创建', '更新', '删除');
                break;
            case "ResignType":
                $('.nav-basic-info').addClass('active');
                $('.nav-resignType').addClass('active');
                PageAction('#resigntype', '新建离职类型', '编辑离职类型', '离职类型详情', '创建', '更新', '删除');
                break;
            case "AbsenceType":
                $('.nav-basic-info').addClass('active');
                $('.nav-absenceType').addClass('active');
                PageAction('#absencetype', '新建缺勤类型', '编辑缺勤类型', '缺勤类型详情', '创建', '更新', '删除');
                break;                
            case "Certificate":
                $('.nav-certificate').addClass('active');
                PageAction('#certificate', '新建证照', '编辑证照', '证照详情', '创建', '更新', '删除');
                break;
            case "AbsenceRecrod":
                $('.nav-absencerecrod').addClass('active');
                PageAction('#absencerecord', '新建缺勤', '编辑缺勤', '缺勤详情', '创建', '更新', '删除');
                break;    
        	case "Shift":
                $('.nav-attendance').addClass('active');
                $('.nav-shift').addClass('active');
                PageAction('#shift', '新建班次', '编辑班次', '班次详情', '创建', '更新', '删除');
                break;
           
            case "QuartzJob":
                $('.nav-attendance').addClass('active');
                $('.nav-attendancesetting').addClass('active');
                PageAction('#quartzjob', '新建考勤计算设置', '编辑考勤计算设置', '考勤计算设置详情', '创建', '更新', '删除');
                break;
            case "SysAuthorization":
                $('.nav-userAuthorization').addClass('active');
                PageAction('#sysauthorization', '新建权限', '编辑权限', '权限详情', '创建', '更新', '删除');
                break;
            case "Staff":
                if(pathname[2] == "Idcard"){
                    $('.nav-manage').addClass('active');
                    $('.nav-idcard').addClass('active');
                }
                if(pathname[2] == "Ontrail"){
                    $('.nav-laborrelations').addClass('active');
                    $('.nav-ontrail').addClass('active');
                }
                if(pathname[2] == "Index"){
                    $('.nav-manage').addClass('active');
                    $('.nav-staff').addClass('active');
                }
                if(pathname[2] == "Create"){
                    $('.nav-laborrelations').addClass('active');
                    $('.nav-create').addClass('active');
                }
                PageAction('#staff', '新建员工', '编辑员工', '员工详情', '创建', '更新', '删除');

                break;
            case "ExtraWorkType":
                $('.nav-basic-info').addClass('active');
                $('.nav-extraWorkType').addClass('active');
                PageAction('#extraworktype', '新建加班类型', '编辑加班类型', '加班类型详情', '创建', '更新', '删除');
                break;
            case "WorkAndRests":
                $('.nav-attendance').addClass('active');
                $('.nav-rest').addClass('active');
                PageAction('#workandrests', '新建作息表', '编辑作息表', '作息表详情', '创建', '更新', '删除');
                break;
            case "ExtraWorkRecord":
                $('.nav-attendance').addClass('active');
                $('.nav-extraWorkRecord').addClass('active');       
                PageAction('#extrawordrecord', '新建加班申请', '编辑加班申请', '加班申请详情', '创建', '更新', '删除');
                break;
            case "RewardsAndPenalty":
                $('.nav-manage').addClass('active');
                $('.nav-rewardspenalty').addClass('active');
                PageAction('#rewardsandpenalties', '新建奖惩记录', '编辑奖惩记录', '奖惩记录详情', '创建', '更新', '删除');
                break;
            case "Recruit":
                $('.nav-manage').addClass('active');
                $('.nav-recruit').addClass('active');
                PageAction('#recruit', '新建招聘需求', '编辑招聘需求', '招聘需求详情', '创建', '更新', '删除');
                break;	
        	case "Personal":
                if(pathname[2] == "Application"){
                    $('.nav-apply').addClass('active');
                    $('.nav-myThing').addClass('active');
                }
                if(pathname[2] == "Approval"){
                    $('.nav-view').addClass('active');
                    $('.nav-myThing').addClass('active');
                }
                if(pathname[2] == "Schedule"){
                    $('.nav-day').addClass('active');
                    $('.nav-myThing').addClass('active');
                }
                if(pathname[2] == "Note"){
                    $('.nav-note').addClass('active');
                    $('.nav-myThing').addClass('active');
                }
                if(pathname[2] == "Finished"){
                    $('.nav-finished').addClass('active');
                    $('.nav-myThing').addClass('active');
                }
                break;
            case "SysRoleAuthorization":
                $('.nav-sysRole').addClass('active');
                PageAction('#sysauthorization', '新建角色权限', '编辑角色权限', '角色权限详情', '创建', '更新', '删除');
                break;
            case "TaskRound":
                $('.nav-taskround').addClass('active');
                break;
            case "SystemSetting":
                $('.nav-system-setting').addClass('active');
                break;
            case "InterApply":
                $('.nav-inter-apply').addClass('active');
                PageAction('#user', '新建离退申请', '编辑离退申请', '离退申请详情', '创建', '更新', '删除');
                break;
            case "Rehire":
                $('.nav-rehire').addClass('active');
                PageAction('#user', '新建回聘申请', '编辑回聘申请', '回聘申请详情', '创建', '更新', '删除');
                break;
            case "SysOpera":
                $('.nav-system-opera').addClass('active');
                PageAction('#user', '新建系统操作', '编辑系统操作', '系统操作详情', '创建', '更新', '删除');
                break;
            case "ResignRecord":
                if(pathname[2] == "Index"){
                    $('.nav-resignation').addClass('active');
                    $('.nav-laborrelations').addClass('active');
                }
                PageAction('#resignrecord', '新建离职申请', '编辑离职申请', '离职申请详情', '创建', '更新', '删除');
                break;
            case "LeaveRecord":
                $('.nav-leave').addClass('active');
                $('.nav-attendance').addClass('active');
                PageAction('#leaverecord', '新建请假申请', '编辑请假申请', '请假申请详情', '创建', '更新', '删除');
                break;
            case "ShiftSchedule":
                $('.nav-shiftschedule').addClass('active');
                $('.nav-attendance').addClass('active');
                break;
            case "ShiftJobRecord":
                $('.nav-transfer').addClass('active');
                $('.nav-laborrelations').addClass('active');
                PageAction('#shiftjobrecord', '新建调岗申请', '编辑调岗申请', '调岗申请详情', '创建', '更新', '删除');
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

            if (pathname[2] == "Edit") {
                $(".main-header").remove();
                $(".main-sidebar").remove();

                $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                $(window).resize(function(){
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                })

                vueName.action = editAction;
                vueName.actionBtn = editBtn;
            } else if (pathname[2] == "Delete") {
                $(".main-header").remove();
                $(".main-sidebar").remove();
                $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                $(window).resize(function(){
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                })
                vueName.action = deleteAction;
                vueName.actionBtn = deleteBtn;
            } else if(pathname[2] == "Create"){
                $(".main-header").remove();
                $(".main-sidebar").remove();
                $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                $(window).resize(function(){
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                })
            }else if(pathname[2] == "Create"){
                $(".main-header").remove();
                $(".main-sidebar").remove();
                $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                $(window).resize(function(){
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                })
            }else if(pathname[2] == "changepwd"){
                $(".main-header").remove();
                $(".main-sidebar").remove();
                $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                $(window).resize(function(){
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                })
                
            }else if(pathname[2] == "UserMsg"){
                $(".main-header").remove();
                $(".main-sidebar").remove();
                $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                $(window).resize(function(){
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                })
            }else if(pathname[2] == "Log"){
                $(".main-header").remove();
                $(".main-sidebar").remove();
                $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});

                $(window).resize(function(){
                    $(".content-wrapper").css({width: $(window).width(), height: $(window).height(), maxHeight: $(window).width(), paddingTop: 0, marginLeft: 0});
                })
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
        if (Object.keys(Department[companyID]).length > 0) {
            Html += "<option value=''></option>";
        }
        for (dep in Department[companyID]) {
            if (dep != null && dep!="") {
                Html += '<option value=' + dep + '>' + Department[companyID][dep] + '</option>';
            }
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
        //scrollInput: false,
        timepicker: false
    });
}

Layout.timepicker = function (time_picker) {
    $(time_picker).datetimepicker({
        format: 'H:i',
        defaultTime: '00:00',
        datepicker: false
    });
}

Layout.datetimepicker = function (date_time_picker) {
    $(date_time_picker).datetimepicker({
        //scrollInput: false
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
        //scrollInput: false,
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

Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,               //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

function FutureDate(day) {
    var now = new Date();
    var FutureDate = new Date(now.getTime() - day * 24 * 60 * 60 * 1000).Format("yyyy-MM-dd hh:mm");
    return FutureDate;
}

//自定义搜索按钮控制内容的上下显示
Layout.advancedfilter = function () {
    $('.advanced-filter-btn').click(function () {

        var Display = $('.advanced-filter').css('display');

        if (Display === "none") {
            $('.advanced-filter').slideDown();
        } else {
            $('.advanced-filter').slideUp();
        }
    });
}

Layout.TransferTableToGrid = function (width, height, editable, divID, title, rPP, gridID) {
    var tb = $("#" + divID + ">table");
    var obj = $.paramquery.tableToArray(tb);

    var newObj = {
        width: width,
        height: height,
        title: title,
        scrollModel: { pace: 'fast', autoFit: true, theme: true },
        editable: editable
    };

    newObj.dataModel = { data: obj.data };
    newObj.colModel = obj.colModel;
    newObj.pageModel = { rPP: rPP, type: "local" };

    $("#" + gridID).pqGrid(newObj);
}

// 在新的页面打开任意界面的内容
Layout.openNewWindow = function(pageURL, height, width, top, left, toolbar, menubar, scrollbars, resizable, location, status, alwaysRaised, zLook){
    NewWindow = window.open (pageURL, 'newwindow', 'height='+height+', width='+width+', top = '+top+',left= '+left+
    ', toolbar='+toolbar+', menubar='+menubar+', scrollbars='+scrollbars+', resizable='+resizable+',location='+location+', status='+status+
    ', alwaysRaised='+alwaysRaised+', zLook='+zLook+' ');

    NewWindow.focus();
}