



$(function () {
    var regExp = /[a-zA-Z]/;
    $('.FormatPhoneNumber').on('keydown keyup', function (e) {
        //var value = String.fromCharCode(e.which) || e.key;
        var value = e.key;
       
        // No letters
        debugger;
        var counttxt = $(this).val();
        if (value != 'Tab') {
            if (value != 'Backspace') {
                if (counttxt.length > 13) {
                    e.preventDefault();
                    return false;
                }
                if (regExp.test(value)) {
                    e.preventDefault();
                    return false;
                }
            }
        }
        if (value != 'Backspace') {
            $(this).val(FormatPhoneNumber($(this).val()));
        }
    });
});

function FormatPhoneNumber(PhoneNumber) {

    var phonen = PhoneNumber;
    phonen = phonen.replace("(", "");
    phonen = phonen.replace(")", "");
    phonen = phonen.replace(" ", "");
    phonen = phonen.replace("-", "");

    if (phonen.length > 10) {
        phonen = phonen.substr(0, 10);
    }
    if (phonen.length < 3) {
    }
    else if (phonen.length >= 3 && phonen.length < 6) {
        PhoneNumber = phonen.replace(/(\d{3})/, "($1) ");
    }
    else if (phonen.length >= 6 && phonen.length < 10) {
        PhoneNumber = phonen.replace(/(\d{3})(\d{3})/, "($1) $2-");
    }
    else {
        PhoneNumber = phonen.replace(/(\d{3})(\d{3})(\d{4})/, "($1) $2-$3");
    }


    return PhoneNumber;
}







