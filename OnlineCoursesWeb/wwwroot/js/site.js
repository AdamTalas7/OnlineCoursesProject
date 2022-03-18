// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(window).on('load && resize', function () {
    if ($(window).width() > 768) {
        $('#teachers').addClass('table');
        $('#teachers').addClass('table-blur');
        $('#teachers').addClass('box-shadow');
        $('#teachers').addClass('table-hover');
    }
    else {
        $('#teachers').removeClass('table');
        $('#teachers').addClass('table-blur');
        $('#teachers').addClass('box-shadow');
        $('#teachers').addClass('table-hover');
    }
})