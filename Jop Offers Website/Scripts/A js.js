$(document).ready(function () {


    $(window).scroll(function () {
        if ($(this).scrollTop() > 1) {
            $('header').addClass("header-shrink");
            $('.main-header .logo').addClass("header-shrink-logo");
        }
        else {
            $('header').removeClass("header-shrink");
            $('.main-header .logo').removeClass("header-shrink-logo");

        }
    });

    $.fn.checkDirection = function () {
        var dir = checkRTL(this.val()[0]) ? 'RTL' : 'LTR';
        this.css("direction", dir);

        function checkRTL(s) {
            var ltrChars = 'A-Za-z\u00C0-\u00D6\u00D8-\u00F6\u00F8-\u02B8\u0300-\u0590\u0800-\u1FFF' + '\u2C00-\uFB1C\uFDFE-\uFE6F\uFEFD-\uFFFF',
                rtlChars = '\u0591-\u07FF\uFB1D-\uFDFD\uFE70-\uFEFC',
                rtlDirCheck = new RegExp('^[^' + ltrChars + ']*[' + rtlChars + ']');
            return rtlDirCheck.test(s);
        }
    };

    $("#searchNameAutoDir").on('input', function () {
        $(this).checkDirection();
    });


    $("#page_scroller").click(function () {
        $('html, body').animate({ scrollTop: 0 }, 800); return false;
    });
    //#region check input search is empty
    $('#searchNameAutoDir').blur(function () {
        if (!$(this).val()) {
            $(this).css("text-align", "center");
        }
    });
    //#endregion



    $('.col-md-8 p').each(function () {
        if (isRTL($(this).text()))
            $(this).addClass('rtl');
        else
            $(this).addClass('ltr');
    });

    function isRTL(str) {
        return /^[\u0600-\u06FF]/.test(str);
    }


});

document.getElementById('searchNameAutoDir').addEventListener('keypress', function (e) {
    if (isEnglish(e.charCode)) {
        console.log('English');
        document.getElementById("searchNameAutoDir").style.textAlign = "left";
    }
    else if (isArabic(e.key)) {
        console.log('Arabic');
        document.getElementById("searchNameAutoDir").style.textAlign = "right";
    }
    //else {
    //    console.log('Others');
    //    document.getElementById("searchNameAutoDir").style.textAlign = "center";
    //}
});

function isEnglish(charCode) {
    return (charCode >= 97 && charCode <= 122)
        || (charCode >= 65 && charCode <= 90);
}

function isArabic(key) {
    var p = /^[\u0600-\u06FF\s]+$/;
    return p.test(key) && key != ' ';
}