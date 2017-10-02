
$(document).ready(function () {
    calcstepheight();
    if ($('#temp_result').text() == "1") {
        registernextstep(4);
        AnimateImage();
        CatchResultVM($('#temp_result').text(), $('#temp_message').text());
    }
    else if ($('#temp_result').text() == "-1") {
        registernextstep(2);
        CatchResultVM($('#temp_result').text(), $('#temp_message').text());
    }
    else {
        registernextstep(1);
    }
});
var t = 5, animationwaittime = 2;
function registernextstep(nextstep) {
    console.log(nextstep);
    for (i = 1; i <= 4; i++) {
        $('#registerstep' + i).hide();
        $('#step' + i).css("border", "6px solid #dbdbdb");
        $('#step' + i).css("background-color", "#f9f9f9");
        $('#step' + i + ' h3').css("color", "#d3d3d3");
    }
    $('#registerstep' + nextstep).show();
    $('#step' + nextstep).css("border", "6px solid #34A881");
    $('#step' + nextstep).css("background-color", "#fffede");
    $('#step' + nextstep).css("z-index", nextstep);
    console.log(nextstep);
    $('#step' + nextstep + ' h3').css("color", "#34A881");
    $('html').animate({ scrollTop: 0 }, 600);
}

function AnimateImage() {
    animationwaittime -= 1;
    if (animationwaittime == 0) {
        $('#checkedblack').fadeOut("slow", CountingDownTime);
    }
    setTimeout("AnimateImage()", 1000);
}

function CountingDownTime() {
    console.log(t);
    t -= 1;
    $('.succeed h3').text('倒數' + t + '秒返回首頁');

    if (t == 0) {
        location.href = 'http://www.google.com';
    }

    //每秒執行一次,showTime()
    setTimeout("CountingDownTime()", 1000);
}

function gotonext(page1class, page2class) {
    $('.' + page1class).hide();
    $('.' + page2class).show();
}

function CheckEmail() { //先向後台確認是否有重複的帳號
    console.log("inputemail");
    if (IsEmail()) {
        $('#emailcheck').css("color", "green");
        $('#emailwarningword').fadeOut();
    }
    else {
        $('#emailcheck').css("color", "red");
        $('#emailwarningword').text('email格式錯誤');
        $('#emailwarningword').fadeIn();
    }
    if ($('#email').val() == "") {
        $('#emailcheck').css("color", "#9D9D9D");
        $('#emailwarningword').fadeOut();
    }
}

function IsEmail() {
    emailRule = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$/;
    var mail = $('#email').val();
    if (mail.search(emailRule) != -1)
        return true;
    else
        return false;
}

function IsEnglishOrNumber(string) {
    passwordRule = /^[a-zA-Z0-9!@#$%^&+=]+$/;
    console.log(string);
    if (string.search(passwordRule) != -1)
        return true;
    else
        return false;
}

function CheckConfirmPassword() {
    console.log("inputpassword");
    if (IsEnglishOrNumber($('#confirmpassword').val()) && ($('#confirmpassword').val() == $('#password').val())) {
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

function IsCellphoneNumber(string) {
    cellphoneRule = /^[\d|0-9]+$/;
    if (string.search(cellphoneRule) != -1)
        return true;
    else
        return false;
}

function CheckCellPhone() {
    var cellphone = $('#cellphone').val();
    if (cellphone.substring(0, 2) != '09' || cellphone.length < 10 || !IsCellphoneNumber(cellphone)) {
        $('#cellphonewarningword').text('cellphone格式錯誤');
        $('#cellphonewarningword').fadeIn();
        return false;
    }
    else
        $('#cellphonewarningword').fadeOut();
    return true;
}

function CheckPassword() {
    if ($('#password').val() != "") {
        $('#passwordcheck').css("color", "green");
    }
    else {
        $('#passwordcheck').css("color", "#9D9D9D");
    }
}

function submitregisterform(currentlystep, nextstep) {
    //未輸入rgb(153,153,153)
    //錯誤rgb(255,0,0)
    //成功rgb(0,128,0)
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
        //registernextstep(4);
    }
}
function CheckUpCheckIconColorGray(string) {
    if ($('#' + string + 'check').css("color") == 'rgb(153, 153, 153)') {
        $('#' + string + 'warningword').text(string + '未輸入');
        $('#' + string + 'warningword').fadeIn();
        $('#' + string).focus();
        return false;
    }
    return true;
}

function CheckUpInputValue(string) {
    if ($('#' + string).val() == "") {
        $('#' + string + 'warningword').text(string + '未輸入');
        $('#' + string + 'warningword').fadeIn();
        $('#' + string).focus();
        return false;
    }
    return true;
}

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

function CheckUpCheckIconColorGreen(string) {
    if ($('#' + string + 'check').css("color") == 'rgb(0, 128, 0)') {
        return true;
    }
    return false;
}

function ResetRegisterForm() {
    console.log("!!");
    $('.warningword').fadeOut();
    $('.checkicon').css('color', 'rgb(153, 153, 153)');
}

function CatchResultVM(result, message) {
    var notifyType = 'info';
    var title = '成功!';
    if (result == "-1") {
        notifyType = 'warning';
        title = '警告';
    }
    $.notify({
        title: '<strong>' + title + '</strong>',
        message: message
    }, {
        type: notifyType,
        delay: 100,
        z_index: 1031,
        animate: {
            enter: 'animated bounceIn',
            exit: 'animated bounceOut'
        }
    });
}