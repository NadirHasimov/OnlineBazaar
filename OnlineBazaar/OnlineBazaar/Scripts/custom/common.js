$(document).ready(function () {
    $('.sidebar-menu a[data-rel="collapse-sidebar"]').click(function () {
        sidebarStateHandler();
    });
    $('#main-menu a').each(function (n) {
        $(this).attr('id', n);
    });
    var activeLinkId = sessionStorage.getItem("activeLinkId");
    if (activeLinkId != null) {
        $('#' + activeLinkId + '').parents('li').addClass('active');
        $('#' + activeLinkId + '').parent().parents('li').addClass('opened');
        $('#' + activeLinkId + '').parents('ul').addClass('visible');
    }
    sidebarMenuStateHandler();
});

/* Toast nofitication options for top right position */
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
    $('#main-menu li a[href]').on('click', function (e) {
        var activeLinkId = $(this).attr('id');
        sessionStorage.setItem("activeLinkId", activeLinkId);
    });
}