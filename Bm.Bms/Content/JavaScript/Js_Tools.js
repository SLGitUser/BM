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