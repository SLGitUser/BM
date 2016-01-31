$(function () {

    //RenderBreadcrumb(window.breadcrumb);

    $("span.field-validation-valid, span.field-validation-error").each(function () {
        $(this).addClass("help-inline");
    });

    $("form").each(function () {
        $(this).find("div.form-group").each(function () {
            if ($(this).find("span.field-validation-error").length > 0) {
                $(this).addClass("has-error");
            }
        });
    });

    $("form").submit(function () {
        if ($(this).valid()) {
            $(this).find("div.form-group").each(function () {
                if ($(this).find("span.field-validation-error").length === 0) {
                    $(this).removeClass("has-error");
                }
            });
        }
        else {
            $(this).find("div.form-group").each(function () {
                if ($(this).find("span.field-validation-error").length > 0) {
                    $(this).addClass("has-error");
                }
            });
        }
    });

    if ($('table.data'))
        $('table.data').DataTable({
            "pagingType": "full_numbers",
            "paging": true,
            "lengthChange": true,
            "searching": true,
            "ordering": true,
            "info": true,
            "autoWidth": true,
            "language": {
                "paginate": {
                    "first": "首页",
                    "previous": "上页",
                    "sNext": "下页",
                    "sLast": "末页"
                },
                "loadingRecords": "正在加载，请稍候...",
                "search": "搜索：",
                "lengthMenu": "每页&nbsp; _MENU_ &nbsp;条",
                "zeroRecords": "抱歉，没有匹配记录",
                "info": "匹配记录 _TOTAL_ 条，当前 _START_ - _END_ 条，第 _PAGE_ 页 / 共 _PAGES_ 页",
                "infoEmpty": "没有信息",
                "infoFiltered": "（总数据 _MAX_ 条）",
                "emptyTable": "无数据",
                //"infoPostFix": "All records shown are derived from real information.",
                "processing": "正忙",
                "aria": {
                    "sortAscending": " - 单击升序排列",
                    "sortDescending": " - 单击降序排列"
                },
                "language": {
                    "decimal": ",",
                    "thousands": "."
                }
            }
        });
});

    /*
        var breadcrumb = {
            title: "开发商信息",
            subtitle: null,
            moduleUrl: "@Url.Action("Index")",
            moduleName: "开发商信息",
            moduleIcon: "fa-book",
            actionName: "修改开发商信息"
        };
        */

//function RenderBreadcrumb(model) {
//    var pageHeader = $("#page-header");
//    if (pageHeader && model) {
//        pageHeader.empty();
//        var tpl = "<h1>" + model.title + "<small>" + model.subtitle + "</small></h1>"
//            + "<ol class=\"breadcrumb\">"
//            + "<li><a href=\"/\"><i class=\"fa fa-dashboard\"></i>首页</a></li>"
//            + "<li><a href=\"" + model.moduleUrl + "\"><i class=\"fa " + model.moduleIcon + "\"></i>" + model.moduleName + "</a></li>"
//            + "<li class=\"active\">" + model.actionName + "</li>"
//            + "</ol>";
//        pageHeader.html(tpl);
//    }
//}

/**
 * 全选按钮
 * @returns {} 
 */
function switchCheckAll(el, inputName) {
    var thisCheck = el.checked;
    var ids = document.getElementsByName(inputName);
    for (var i = 0; i < ids.length; i++) {
        ids[i].checked = thisCheck;
    }
}