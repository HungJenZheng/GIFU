//判斷是否有登入
function IsUserLogin() {
    if ($('#userId').text() === '') return false;
    else return true;
}

//訊息提示
function ShowNotify(result, message) {
    var msgType;
    if (result > 0) {
        msgType = { name: '成功：', type: 'success' };
    }
    else if (result < 0) {
        msgType = { name: '失敗：', type: 'danger' };
    } else {
        msgType = { name: '', type: 'info' };
    }

    $.notify({
        title: '<strong>' + msgType.name + '</strong>',
        message: message
    }, {
        type: msgType.type,
        animate: {
        enter: 'animated bounceIn',
        exit: 'animated bounceOut'
        },
        mouse_over: 'pause'
    });
}