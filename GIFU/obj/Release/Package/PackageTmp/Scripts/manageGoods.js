$(document).ready(function () {
    //調整物品高度
    ResizeGoodsBoxHeight();
});

$("#add-goodsTag1").change(function () { //新增商品大標籤對應小標籤
    if ($('#add-goodsTag1').val() == '3C') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="1">電腦</option> <option value="2">手機</option> <option value="3">相機</option> <option value="4">3C其他</option>');
    }
    else if ($('#add-goodsTag1').val() == 'GC') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="1">五金工具</option> <option value="2">食品</option> <option value="3">寵物用品</option> <option value="4">生活其他</option>');
    }
    else if ($('#add-goodsTag1').val() == 'CL') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="1">男裝</option> <option value="2">女裝</option> <option value="3">配件</option>');
    }
    else if ($('#add-goodsTag1').val() == 'SN') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="1">小說</option> <option value="2">漫畫</option> <option value="3">雜誌</option> <option value="4">其他</option>');
    }
    else if ($('#add-goodsTag1').val() == 'GM') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="1">遊戲機</option> <option value="2">遊戲光碟</option> <option value="3">電玩攻略</option> <option value="4">電玩其他</option>');
    }
    else if ($('#add-goods-tag1').val() == 'OT') {
        $('#add-goodsTag2').html('<option value="0" style="display: none;" selected></option>');
        $('#add-goodsTag2').disabled = true;
    }
});

$("#modify-goods-tag1").change(function () { //修改商品大標籤對應小標籤
    if ($('#modify-goods-tag1').val() == '3C') {
        $('#modify-goods-tag2').prop('disabled', false);
        $('#modify-goods-tag2').html('</option><option value="1">電腦</option> <option value="2">手機</option> <option value="3">相機</option> <option value="4">3C其他</option>');
    }
    else if ($('#modify-goods-tag1').val() == 'GC') {
        $('#modify-goods-tag2').prop('disabled', false);
        $('#modify-goods-tag2').html('<option value="1">五金工具</option> <option value="2">食品</option> <option value="3">寵物用品</option> <option value="4">生活其他</option>');
    }
    else if ($('#modify-goods-tag1').val() == 'CL') {
        $('#modify-goods-tag2').prop('disabled', false);
        $('#modify-goods-tag2').html('<option value="1">男裝</option> <option value="2">女裝</option> <option value="3">配件</option>');
    }
    else if ($('#modify-goods-tag1').val() == 'SN') {
        $('#modify-goods-tag2').prop('disabled', false);
        $('#modify-goods-tag2').html('</option> <option value="1">小說</option> <option value="2">漫畫</option> <option value="3">雜誌</option> <option value="4">其他</option>');
    }
    else if ($('#modify-goods-tag1').val() == 'GM') {
        $('#modify-goods-tag2').prop('disabled', false);
        $('#modify-goods-tag2').html('<option value="1">遊戲機</option> <option value="2">遊戲光碟</option> <option value="3">電玩攻略</option> <option value="4">電玩其他</option>');
    }
    else if ($('#modify-goods-tag1').val() == 'OT') {
        $('#modify-goods-tag2').html('<option value="0" style="display: none;" selected></option>');
        $('#modify-goods-tag2').prop('disabled', 'disabled');
    }
});

function isFillOut() { //增加商品的表單是否填寫完整
    if (($('#add-goodsTag1').val() == "OT"))
        $('#add-goodsTag2').attr('disabled', 'disabled');
    if (($('#add-goods-img').val() != "") && ($('#add-goods-name').val() != "") &&
        ($('#add-goods-new-degree').val() != "") && ($('#add-goods-amount').val() != "") &&
        ($('#add-goodsTag1').val() != "") && ($('#add-goods-is-reason').val() != "")
        && ($('#add-goodsTag2').val() != "" || $('#add-goodsTag2').val() != 0 || ($('#add-goodsTag1').val() == 'OT')) &&
        ($('#add-goods-intro').val() != ""))
        document.getElementById("add-goods-button").disabled = false;
    else
        document.getElementById("add-goods-button").disabled = true;
}

function isModify() {
    //document.getElementById("modify-goods-modify-button").disabled = false;
}

//修改訂單狀態
function ChangeOrderStatus(orderId, status, e) {
    $.ajax({
        type: "POST",
        url: "/Store/UpdateOrderStatus",
        data: {orderId: orderId, status: status},
        dataType: "json",
        success: function (response) {
            ShowNotify(response.result, response.message);
            if (response.result > 0) {
                if (status == 2) {
                    alert($(this).html());
                    $(e).parent().children('.check-goods-applier-reject').hide();
                    $(e).addClass('disabled');
                    $(e).text('已贈予');
                }
                if (status == 3) {
                    $(e).parent().children('.check-goods-applier-accept').hide();
                    $(e).addClass('disabled');
                    $(e).text('已拒絕');
                }
                var goodsResquestAmount = $('#request-amount-' + $(e).parent().parent().find('.check-goods-applier-goodsId').text() +' p');
                if (goodsResquestAmount.text() <= 1) {
                    goodsResquestAmount.parent().hide();
                }
                else {
                    goodsResquestAmount.text(goodsResquestAmount.text() - 1);
                }
                console.log(goodsResquestAmount.text());
            }
        }, fail: function (error) {
            console.log(error);
        }
    });
}

function GetModifyGoodsInfo(goodsId) {
    //console.log(goodsId);
    $('#modify-goods-id').text(goodsId);
    $("#modify-goods-name").val($('#goods-box-' + goodsId + ' .goods-name').text());
    $("#modify-goods-new-degree").val($('#goods-box-' + goodsId + ' .goods-new-degree').text());
    $("#modify-goods-amount").val($('#goods-box-' + goodsId + ' .goods-amount').text());
    $("#modify-goods-tag1").val($('#goods-box-' + goodsId + ' .goods-tag1').text());
    $("#modify-goods-tag1").change();
    $("#modify-goods-tag2").val($('#goods-box-' + goodsId + ' .goods-tag2').text());
    $("#modify-goods-is-reason").val($('#goods-box-' + goodsId + ' .goods-is-reason').text());
    $("#modify-goods-status").val($('#goods-box-' + goodsId + ' .goods-status').text());
    $("#modify-goods-intro").val($('#goods-box-' + goodsId + ' .goods-intro').text());
}

function GetCheckGoodsInfo(goodsId) {
    ShowOrderManageList(goodsId);
    var image = $('#goods-box-' + goodsId + ' .goods-img-hide').text();
    $("#check-goods-img").css('backgroundImage', 'url('+image+')');
    $("#check-goods-name").text($('#goods-box-' + goodsId + ' .goods-name').text());
    $("#check-goods-new-degree").text($('#goods-box-' + goodsId + ' .goods-new-degree').text() + '成新');
    $("#check-goods-amount").text($('#goods-box-' + goodsId + ' .goods-amount').text() + '個');
    $("#check-goods-tag1-name").text($('#goods-box-' + goodsId + ' .goods-tag1-name').text());
    $("#check-goods-tag2-name").text($('#goods-box-' + goodsId + ' .goods-tag2-name').text());
    $("#check-goods-is-reason").text($('#goods-box-' + goodsId + ' .goods-is-reason').text());
    $("#check-goods-intro").text($('#goods-box-' + goodsId + ' .goods-intro').text());
    $("#check-goods-update-date").text($('#goods-box-' + goodsId + ' .goods-update-date').text());
}

$('#addGoodsModal').on('hidden.bs.modal', function () {
    $(this).find("textarea,input,select").val('').end();
    $('add-goods-button').prop('disabled', true);
});

$('#modifyGoodsModal').on('hidden.bs.modal', function () {
    $(this).find("textarea,input,select").val('').end();
    $('#input-number-notify').css('display', 'none');
    $('#input-name-notify').css('display', 'none');
});

$('#checkGoodsModal').on('hidden.bs.modal', function () {
    $(this).find("textarea,input,select").val('').end();
    $('#check-goods-applier-block').empty();
});

$('#addGoodsForm').submit(function (e) {
    e.preventDefault();
    var formData = new FormData(this);
    formData.append('GoodId', $('#modify-goods-id').text());
    formData.append('UserId', $('#userId').text());
    $('#add-goods-button').attr('disabled', 'true');
    $.ajax({
        type: "POST",
        url: "/Store/AddGoodsWithMultiPicture",
        data: formData,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function (response) {
            ShowNotify(response.result, response.message);
            ShowNewGoods();
            document.getElementById("addGoodsForm").reset();
            $('#add-goodsTag2').attr('disabled', 'disabled');
        }, fail: function (error) {
        }
    });
});

$('#modifyForm').submit(function (e) {
    e.preventDefault();
    var formData = new FormData(this);
    formData.append('GoodId', $('#modify-goods-id').text());
    formData.append('UserId', $('#userId').text());
    console.log(formData);
    var goodsId = $('#modify-goods-id').text();
    //document.getElementById("#modifyForm").reset();
    $.ajax({
        type: "POST",
        url: "/Store/UpdateGoods",
        data: formData,
        dataType: "json",
        contentType: false,
        cache: false,
        processData: false,
        success: function (response) {
            ShowNotify(response.result, response.message);
            $('#goods-box-' + goodsId + ' .goods-name').text($('#modify-goods-name').val());
            $('#goods-box-' + goodsId + ' .goods-new-degree').text($('#modify-goods-new-degree').val());
            $('#goods-box-' + goodsId + ' .goods-amount').text($('#modify-goods-amount').val());
            $('#goods-box-' + goodsId + ' .goods-tag1').text($('#modify-goods-tag1').val());
            $('#goods-box-' + goodsId + ' .goods-tag1-name').text($('#modify-goods-tag1 :selected').text());
            $('#goods-box-' + goodsId + ' .goods-tag2').text($('#modify-goods-tag2').val());
            $('#goods-box-' + goodsId + ' .goods-tag2-name').text($('#modify-goods-tag2 :selected').text());
            $('#goods-box-' + goodsId + ' .goods-is-reason').text($('#modify-goods-is-reason').val());
            $('#goods-box-' + goodsId + ' .goods-status').text($('#modify-goods-status').val());
            $('#goods-box-' + goodsId + ' .goods-intro').text($('#modify-goods-intro').val());
            $("#modifyGoodsModal").modal('hide');
            if ($('#modify-goods-status').val() == 'F') {
                $('#goods-box-' + goodsId).addClass('discontinued');
                $('.goods-area').append('')
            }
            if ($('#modify-goods-status').val() == 'T') {
                $('#goods-box-' + goodsId).removeClass('discontinued');
            }
        }, fail: function (error) {
        }
    });
});

//取得訂單清單HTML
function ShowOrderManageList(goodId) {
    $.ajax({
        url: '/Store/GetOrderManageList',
        //contentType: 'application/html ; charset:utf-8',
        type: 'POST',
        dataType: 'html',
        data: { goodId: goodId }
    }).success(function (result) {
        $('#check-goods-applier-block').empty().append(result);
    }).error(function (error) {
        console.log(error);
    });
}

function ShowNewGoods() {
    var userId = $('#userId').text();
    $.ajax({
        url: '/Store/GetNewestGoodsByUserId',
        type: 'POST',
        dataType: 'json',
        data: { userId: userId }
    }).success(function (result) {
        $('.goods-area').prepend(CreateGoodBox(result)).hide().fadeIn();
    }).error(function (error) {
        console.log(error);
    });
}

function CreateGoodBox(obj) {
    var box = '<div class="goods-box" id="goods-box-' + obj.GoodId + '">' + 
                //'<img class="goods-img" src="' + obj.PicPath + '"/>' +
                //'<div class="goods-img" style="background-image: url(\'' + obj.PicPath + '\');"></div>' +
                '<a href="/Store/GoodsDetail/'+ obj.GoodId +'" class="goods-img" style="background-image: url(\'' + obj.PicPath + '\');"></a>' +
                '<div class="modify-overlay" data-toggle="modal" data-target="#modifyGoodsModal" onclick="GetModifyGoodsInfo('+obj.GoodId+')">' +
                    '<div class="text">編輯</div>' +
                '</div>' +
                '<div class="check-overlay" data-toggle="modal" data-target="#checkGoodsModal" onclick="GetCheckGoodsInfo('+obj.GoodId+')">' +
                    '<div class="text">查看</div>'+
                '</div>'+
                '<div class="goods-info">'+
                    '<span class="goods-name">'+obj.Title+'</span>'+
                    '<span class="goods-new-degree">'+obj.NewDegree+'</span>'+
                    '<span class="goods-amount">'+obj.Amount+'</span>'+
                    '<span class="goods-tag1">'+obj.Tag1+'</span>'+
                    '<span class="goods-tag1-name">'+obj.Tag1Name+'</span>'+
                    '<span class="goods-tag2">'+obj.Tag2+'</span>'+
                    '<span class="goods-tag2-name">'+obj.Tag2Name+'</span>'+
                    '<span class="goods-is-reason">'+obj.IsReason+'</span>'+
                    '<span class="goods-update-date">'+obj.UpdateDate+'</span>'+
                    '<span class="goods-intro">'+obj.Introduction+'</span>'+
                '</div>'
    '</div>'
    return box;
}

window.addEventListener('resize', ResizeGoodsBoxHeight);
function ResizeGoodsBoxHeight(){
    $('.goods-box').height($('.goods-box').width());
}