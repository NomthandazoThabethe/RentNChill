// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$(".js-modal-btn").modalVideo();

$(document).ready(function () {
    $('#modal-video').on('hidden.bs.modal', function () {
        $("#modal-video iframe").attr("src", $("#modal-video iframe").attr("src"));
    });
    $(".js-modal-btn")
    
});