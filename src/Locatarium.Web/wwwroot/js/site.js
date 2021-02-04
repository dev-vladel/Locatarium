// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Modal Variables
var actionSuccessful = "Action successful!";
var actionFailed = "Action failed!";
var actionCancelled = "Action cancelled!";
var actionConfirmation = "Are you sure you want to perform this action?";

// Infinite Scrolling Variables
var loadMore = false;
var endOfData = false;
var toSkipNumber = 0;

$(document).ready(function () {
    if (window.location.href === window.location.origin.concat("/")) {
        infiniteScrollingGetResidences(toSkipNumber);
    }
});

function infiniteScrollingGetResidences(toSkipNumber) {
    if (window.location.href === window.location.origin.concat("/")) {
        $.ajax({
            type: 'GET',
            data: {
                toSkipNumber: toSkipNumber
            },
            url: 'Home/ResidencesCards',
            success: function (response) {
                if ($.trim(response)) {
                    $('#residencesCardsPartial').append($(response));
                }
                else endOfData = true;
            }
        });

        setTimeout(function () {
            loadMore = false;
        }, 1500);
    }
}

$(window).scroll(function () {
    if (endOfData === false) {
        if ($(this).scrollTop() + 1 >= $('body').height() - $(window).height()) {
            if (loadMore === false) {
                loadMore = true;
                toSkipNumber += 8;
                infiniteScrollingGetResidences(toSkipNumber);
            }
        }
    }
});