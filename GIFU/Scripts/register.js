$(document).ready(function () {
    //調整步驟圖形高度
    ResizeStepGraphHeight();
    //決定註冊步驟
    DetermineRegisterStep();
    CheckEmail();
});

//網頁Resize事件註冊 實作ResizeStepGraphHeight
window.addEventListener("resize", ResizeStepGraphHeight);

//調整步驟圖形高度
function ResizeStepGraphHeight() {
    $('.step').height($('.step').width() + 12);
    var margin = $('.step').css('margin-top');
    $('.steparea').height($('.step').height() + (margin.substr(0, 2)) * 2 + 12);
}

//決定註冊步驟
function DetermineRegisterStep() {
    //if ($('#temp_result').text() == "1") {
    //    NextRegisterStep(4);
    //    AnimateRegisterSuccessImage();
    //    ShowNotify($('#temp_result').text(), $('#temp_message').text());
    //}
    //else if ($('#temp_result').text() == "-1") {
    if ($('#temp_result').text() == "-1") {
        NextRegisterStep(2);
        ShowNotify($('#temp_result').text(), $('#temp_message').text());
    }
    else {
        NextRegisterStep(1);
    }
}

var downTime = 5, animationWaitTime = 2;

//前往下一個註冊步驟
function NextRegisterStep(nextStep) {
    for (i = 1; i <= 4; i++) {
        $('#registerstep' + i).hide();
        $('#step' + i).css("border", "6px solid #dbdbdb");
        $('#step' + i).css("background-color", "#f9f9f9");
        $('#step' + i + ' h3').css("color", "#d3d3d3");
    }
    $('#registerstep' + nextStep).show();
    $('#step' + nextStep).css("border", "6px solid #34A881");
    $('#step' + nextStep).css("background-color", "#fffede");
    $('#step' + nextStep).css("z-index", nextStep);
    $('#step' + nextStep + ' h3').css("color", "#34A881");
    $('html').animate({ scrollTop: 0 }, 600);
}

//檢查Email格式
function CheckEmail() {
    if ($('#email').val() == "") {
        $('#emailcheck').css("color", "#9D9D9D");
        $('#emailwarningword').fadeOut();
        return;
    }
    if (IsEmail()) {
        $('#emailcheck').css("color", "green");
        $('#emailwarningword').fadeOut();
    }
    else {
        $('#emailcheck').css("color", "red");
        $('#emailwarningword').text('email格式錯誤');
        $('#emailwarningword').fadeIn();
    }
}

//是否為Email
function IsEmail() {
    emailRule = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/;
    var mail = $('#email').val();
    if (mail.search(emailRule) != -1)
        return true;
    else
        return false;
}

//是否為英文或數字或特殊符號
function IsEnglishOrNumberOrSymbol(string) {
    passwordRule = /^[a-zA-Z0-9!@#$%^&+=]+$/;
    console.log(string);
    if (string.search(passwordRule) != -1)
        return true;
    else
        return false;
}

//檢查密碼
function CheckPassword() {
    if ($('#password').val() != "") {
        $('#passwordcheck').css("color", "green");
    }
    else {
        $('#passwordcheck').css("color", "#9D9D9D");
    }
}

//檢查確認密碼
function CheckConfirmPassword() {
    console.log("inputpassword");
    if (IsEnglishOrNumberOrSymbol($('#confirmpassword').val()) && ($('#confirmpassword').val() == $('#password').val())) {
        $('#confirmpasswordcheck').css("color", "green");
        $('#confirmpasswordwarningword').fadeOut();
    }
    else {
        $('#confirmpasswordcheck').css("color", "red");
        $('#confirmpasswordwarningword').text('與密碼不一致');
        $('#confirmpasswordwarningword').fadeIn();
    }
    if ($('#confirmpassword').val() == "") {
        $('#confirmpasswordcheck').css("color", "#9D9D9D");
        $('#confirmpasswordwarningword').fadeOut();
    }
}

//是否為手機電話號碼
function IsCellphoneNumber(cellPhoneNumber) {
    cellphoneRule = /^[\d|0-9]+$/;
    if (cellPhoneNumber.search(cellphoneRule) != -1)
        return true;
    else
        return false;
}

//檢查手機電話
function CheckCellPhone() {
    var cellPhone = $('#cellphone').val();
    if (cellPhone.substring(0, 2) != '09' || cellPhone.length < 10 || !IsCellphoneNumber(cellPhone)) {
        $('#cellphonewarningword').text('cellphone格式錯誤');
        $('#cellphonewarningword').fadeIn();
        return false;
    }
    else
        $('#cellphonewarningword').fadeOut();
    return true;
}

//送出註冊表單
function SubmitRegisterForm() {
    //未輸入(灰)rgb(153,153,153)
    //錯誤(紅)rgb(255,0,0)
    //成功(綠)rgb(0,128,0)
    event.preventDefault();
    $('.warningword').fadeOut();
    CheckCellPhone();
    CheckUpInputValue('address');
    CheckUpInputValue('cellphone');
    CheckUpInputValue('birthday');
    CheckUpInputValue('name');
    CheckUpCheckIconColorGray('confirmpassword');
    CheckUpCheckIconColorGray('password');
    CheckUpCheckIconColorGray('email');
    CheckUpCheckIconColorRed('confirmpassword');
    CheckUpCheckIconColorRed('password');
    CheckUpCheckIconColorRed('email');

    if (CheckUpCheckIconColorGreen('confirmpassword') &&
    CheckUpCheckIconColorGreen('password') &&
    CheckUpCheckIconColorGreen('email') &&
    CheckUpInputValue('address') &&
    CheckUpInputValue('cellphone') &&
    CheckCellPhone() &&
    CheckUpInputValue('birthday') &&
    CheckUpInputValue('name')) {
        $('#register-form').submit();
    }
}

//檢查確認Icon顏色(灰)
function CheckUpCheckIconColorGray(string) {
    if ($('#' + string + 'check').css("color") == 'rgb(153, 153, 153)') {
        $('#' + string + 'warningword').text(string + '未輸入');
        $('#' + string + 'warningword').fadeIn();
        $('#' + string).focus();
        return false;
    }
    return true;
}

//檢查Input的值
function CheckUpInputValue(input) {
    if ($('#' + input).val() == "") {
        $('#' + input + 'warningword').text(input + '未輸入');
        $('#' + input + 'warningword').fadeIn();
        $('#' + input).focus();
        return false;
    }
    return true;
}

//檢查確認Icon顏色(紅)
function CheckUpCheckIconColorRed(string) {
    if ($('#' + string + 'check').css("color") == 'rgb(255, 0, 0)') {
        if (string == 'email')
            $('#' + string + 'warningword').text(string + '格式錯誤');
        else if (string == 'confirmpassword')
            $('#' + string + 'warningword').text('與密碼不一致');
        $('#' + string + 'warningword').fadeIn();
        $('#' + string).focus();
        return false;
    }
    return true;
}

//檢查確認Icon顏色(綠)
function CheckUpCheckIconColorGreen(string) {
    if ($('#' + string + 'check').css("color") == 'rgb(0, 128, 0)') {
        return true;
    }
    return false;
}

//重置註冊表單
function ResetRegisterForm() {
    $('.warningword').fadeOut();
    $('.checkicon').css('color', 'rgb(153, 153, 153)');
}