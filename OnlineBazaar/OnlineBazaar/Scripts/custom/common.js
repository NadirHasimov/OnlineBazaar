/* Toast nofitication options for top right position */
$(document).ready(function () {
    $('.sidebar-menu a[data-rel="collapse-sidebar"]').click(function () {
        sidebarStateHandler();
    });
    sidebarMenuStateHandler();
    console.log(localStorage.getItem("index1"));
    console.log(localStorage.getItem("index2"));
});

var toastOpts = {
    "closeButton": true,
    "debug": false,
    "positionClass": "toast-top-right",
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "6000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};



function showSuccessNotification(message) {
    toastr.success(message, "", toastOpts);
}

function showErrorNotification(message) {
    toastr.error(message, "", toastOpts);
}

function showInfoNotification(message) {
    toastr.info(message, "", toastOpts);
}

function showInfoNotification(message, title) {
    toastr.info(message, title, toastOpts);
}

function sidebarStateHandler() {
    var currentState = $.cookie('sidebarCollapsed');
    $.cookie('sidebarCollapsed', currentState == 1 ? 0 : 1);
}

function sidebarMenuStateHandler() {
    $('#main-menu li ul a[href]').on('click', function (e) {
        $('#main-menu .active').removeClass('active');
        var parent = $(this).parent();
        $(parent).addClass('active');
        $(parent).parent().parent().addClass('active');
        localStorage.setItem("index1", $(this).parent().parent().index());
        localStorage.setItem("index2", $(this).index());
        e.preventDefault();
    });
}