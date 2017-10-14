$("#add-goodsTag1").change(function () { //新增物品大標籤對應小標籤
    if ($('#add-goodsTag1').val() == '3C') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="0" style="display: none;" selected></option> <option value="1">電腦</option> <option value="2">手機</option> <option value="3">相機</option> <option value="4">3C其他</option>');
    }
    else if ($('#add-goodsTag1').val() == 'GC') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="0" style="display: none;" selected></option> <option value="1">五金工具</option> <option value="2">食品</option> <option value="3">寵物用品</option> <option value="4">生活其他</option>');
    }
    else if ($('#add-goodsTag1').val() == 'CL') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="0" style="display: none;" selected></option> <option value="1">男裝</option> <option value="2">女裝</option> <option value="3">配件</option>');
    }
    else if ($('#add-goodsTag1').val() == 'SN') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="0" style="display: none;" selected></option> <option value="1">小說</option> <option value="2">漫畫</option> <option value="3">雜誌</option> <option value="4">其他</option>');
    }
    else if ($('#add-goodsTag1').val() == 'GM') {
        $('#add-goodsTag2').prop('disabled', false);
        $('#add-goodsTag2').html('<option value="0" style="display: none;" selected></option> <option value="1">遊戲機</option> <option value="2">遊戲光碟</option> <option value="3">電玩攻略</option> <option value="4">電玩其他</option>');
    }
    else {
        $("#add-goodsTag2").empty();
        $('#add-goodsTag2').disabled = true;
    }
});

$("#modify-goods-tag1").change(function () { //修改物品大標籤對應小標籤
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
    else {
        $('#modify-goods-tag2').empty();
        $('#modify-goods-tag2').prop('disabled', 'disabled');
    }
});

function isFillOut() { //增加物品的表單是否填寫完整
    if (($('#add-goods-img').val() != "") && ($('#add-goods-name').val() != "") &&
        ($('#add-goods-new-degree').val() != "") && ($('#add-goods-amount').val() != "") &&
        ($('#add-goodsTag1').val() != "") && ($('#add-goodsTag2').val() != "" && $('#add-goodsTag2').val() != 0) &&
        ($('#add-goods-is-reason').val() != ""))
        document.getElementById("add-goods-button").disabled = false;
    else
        document.getElementById("add-goods-button").disabled = true;
}

function isModify() {
    //document.getElementById("modify-goods-modify-button").disabled = false;
}

function GetModifyGoodsInfo(goodsId) {
    $('#modify-goods-id').text(goodsId);
    $("#modify-goods-name").val($('#goods-box-' + goodsId + ' .goods-name').text());
    $("#modify-goods-new-degree").val($('#goods-box-' + goodsId + ' .goods-new-degree').text());
    $("#modify-goods-amount").val($('#goods-box-' + goodsId + ' .goods-amount').text());
    $("#modify-goods-tag1").val($('#goods-box-' + goodsId + ' .goods-tag1').text());
    $("#modify-goods-tag1").change();
    $("#modify-goods-tag2").val($('#goods-box-' + goodsId + ' .goods-tag2').text());
    var goodsIsReason = $('#goods-box-' + goodsId + ' .goods-is-reason').text();
    if (goodsIsReason == "是") goodsIsReason = 'T';
    else if (goodsIsReason == "否") goodsIsReason = 'F';
    $("#modify-goods-is-reason").val(goodsIsReason);
    $("#modify-goods-intro").val($('#goods-box-' + goodsId + ' .goods-intro').text());
}

function GetCheckGoodsInfo(goodsId) {
    $("#check-goods-img").attr('src', $('#goods-box-' + goodsId + ' .goods-img').attr('src'));
    $("#check-goods-name").text($('#goods-box-' + goodsId + ' .goods-name').text());
    $("#check-goods-new-degree").text($('#goods-box-' + goodsId + ' .goods-new-degree').text() + '成新');
    $("#check-goods-amount").text($('#goods-box-' + goodsId + ' .goods-amount').text() + '個');
    $("#check-goods-tag1-name").text($('#goods-box-' + goodsId + ' .goods-tag1-name').text());
    $("#check-goods-tag2-name").text($('#goods-box-' + goodsId + ' .goods-tag2-name').text());
    $("#check-goods-is-reason").text(GetIsReason($('#goods-box-' + goodsId + ' .goods-is-reason').text()));
    $("#check-goods-intro").text($('#goods-box-' + goodsId + ' .goods-intro').text());
    //$("#check-goods-place-time").text($('#goods-box-' + goodsId + ' .goods-place-time').text());
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
            var goodsTag2 = $('#add-goodsTag2').val();
            document.getElementById("addGoodsForm").reset();
        }, fail: function (error) {
        }
    });
});

$('#modifyForm').submit(function (e) {
    e.preventDefault();
    var formData = new FormData(this);
    formData.append('GoodId', $('#modify-goods-id').text());
    formData.append('UserId', $('#userId').text());
    var goodsId = $('#modify-goods-id').text();
    //document.getElementById("#modifyForm").reset();
    $.ajax({
        type: "POST",
        url: "/Store/UpdateGood",
        data: formData,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function (response) {
            ShowNotify(response.result, response.message);
            $('#goods-box-' + goodsId + ' .goods-name').text($('#modify-goods-name').val());
            $('#goods-box-' + goodsId + ' .goods-new-degree').text($('#modify-goods-new-degree').val());
            $('#goods-box-' + goodsId + ' .goods-amount').text($('#modify-goods-amount').val());
            $('#goods-box-' + goodsId + ' .goods-tag1').text($('#modify-goods-tag1').val());
            $('#goods-box-' + goodsId + ' .goods-tag1-name').text(GetTag1Name($('#modify-goods-tag1').val()));
            $('#goods-box-' + goodsId + ' .goods-tag2').text($('#modify-goods-tag2').val());
            $('#goods-box-' + goodsId + ' .goods-tag2-name').text(GetTag2Name($('#modify-goods-tag1').val(), $('#modify-goods-tag2').val()));
            $('#goods-box-' + goodsId + ' .goods-is-reason').text(GetIsReason($('#modify-goods-is-reason').val()));
            $('#goods-box-' + goodsId + ' .goods-intro').text($('#modify-goods-intro').val());
            $("#modifyGoodsModal").modal('hide');
        }, fail: function (error) {
        }
    });
});

function GetIsReason(goodsIsReason) {
    if (goodsIsReason == "T") return '是';
    else if (goodsIsReason == "F") return '否';
}