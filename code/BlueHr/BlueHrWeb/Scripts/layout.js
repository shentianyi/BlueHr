var Layout = {};

Layout.init = function () {
    $('.nav-left dt').removeClass('active');
    var pathname = window.location.pathname.split('/');

    switch (pathname[1]) {
        case "Company":
            $('.nav-company').addClass('active');
            PageAction('#company', '新建公司', '编辑公司', '公司详情', '创建', '更新', '删除');
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