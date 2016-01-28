$(function () {
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