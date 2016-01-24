/**
 * 全选按钮
 * @returns {} 
 */
function allSelect() {
    var thisCheck = document.getElementById("AllSelect").checked;
    var ids = document.getElementsByName("ids");
    for (var i = 0; i < ids.length; i++) {
        ids[i].checked = thisCheck;
    }
}