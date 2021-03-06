﻿$(document).ready(function () {
    $('.sidebar-menu a[data-rel="collapse-sidebar"]').click(function () {
        sidebarStateHandler();
    });
    $('#main-menu a').each(function (id) {
        $(this).attr('id', id);
    });
    var activeLinkId = sessionStorage.getItem("activeLinkId");
    if (activeLinkId != null) {
        $('#' + activeLinkId + '').parent().parents('li').addClass('opened');
        $('#' + activeLinkId + '').parents('li').addClass('active');
        $('#' + activeLinkId + '').parentsUntil('#main-menu','ul').addClass('visible');
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
    $('#main-menu li a[href]').on('click', function () {
        var activeLinkId = $(this).attr('id');
        sessionStorage.setItem("activeLinkId", activeLinkId);
    });
}