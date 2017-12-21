$(document).ready(function () {
    $('#parentList').select2({
        placeholder:"Select category"
    });
    $("#displayOrder").keypress(function (e) {
        if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
    });
});