// Variables
var residencesPageNumber = 1;
var residencesPagesTotal = 1;
var $residencesPageGoTo = $('#residencesPageGoTo');

$(document).ready(function () {
    if (document.location.pathname.indexOf('/Residences/Items') !== -1) {
        residencesPagination(residencesPageNumber);
    }

    if (residencesPagesTotal === 1 && document.location.pathname.indexOf('/Residences/Items') !== -1) {
        residencesTotal();
    }
});

// Function to retrieve residences and append the response to an id
function residencesPagination(residencesPageNumber) {
    var documentURL = document.location.pathname;
    if (documentURL.indexOf('Residences/Items') !== -1) {
        $.ajax({
            type: 'GET',
            data: {
                residencesPageNumber: residencesPageNumber - 1
            },
            url: "ItemsPartial",
            success: function (response) {
                $('#residencesPartial').html($(response));
                $('html, body').animate({ scrollTop: 0 }, 'slow');
            }
        });
    }
}

// Function to determine total of residences and show/hide the button controls
function residencesTotal() {
    var documentURL = document.location.pathname;
    if (documentURL.indexOf('/Residences/Items') !== -1) {
        $.ajax({
            type: 'GET',
            url: 'ItemsTotal',
            success: function (response) {
                residencesPagesTotal = Math.ceil(response / 8);

                residencesPaginationControls(residencesPagesTotal);

                $('#residencesTotal').html(response);
                $('#residencesPagesTotal').html(residencesPagesTotal);

                if (residencesPagesTotal === 0) {
                    $('#residencesPaginationButtons, residencesPageGoToInput').hide();
                } else {
                    $('#residencesPaginationButtons, residencesPageGoToInput').show();
                }

                if (residencesPagesTotal < residencesPageNumber) {
                    residencesPageNumber = residencesPagesTotal;
                    residencesPagination(residencesPageNumber);
                    residencesPaginationControls(residencesPagesTotal);
                }
            }
        });
    }
}

// Function determining display of button controls and values
function residencesPaginationControls(residencesPagesTotal) {
    $('#residencesButtonFirst, #residencesButtonLast, #residencesPageGoTo').show();
    $('#residencesButtonCurrent').html(residencesPageNumber);
    $('#residencesButtonCurrent').prop('disable', true);

    if (residencesPageNumber > 1) {
        $('#residencesButtonPrev, #residencesButtonLast').show();
        $('#residencesButtonPrev').html(residencesPageNumber - 1);
        $('#residencesButtonPrev').prop('disabled', false);
    }

    if (residencesPageNumber < residencesPagesTotal) {
        $('#residencesButtonNext').show();
        $('#residencesButtonNext').html(residencesPageNumber + 1);
        $('#residencesButtonNext').prop('disabled', false);
    } else if (residencesPageNumber === residencesPagesTotal) {
        $('#residencesButtonNext, #residencesButtonLast').hide();
    }

    if (residencesPageNumber === 1) {
        $('#residencesButtonFirst, #residencesButtonPrev').hide();
    }

    if (residencesPagesTotal === 0) {
        $('#residencesButtonFirst, #residencesButtonCurrent').hide();
    }
}

// Function to go to next page
function residencesPaginationNext() {
    residencesPageNumber += 1;

    residencesPagination(residencesPageNumber);
    residencesPaginationControls(residencesPagesTotal);
}

// Function to go to previous page
function residencesPaginationPrev() {
    residencesPageNumber -= 1;

    residencesPagination(residencesPageNumber);
    residencesPaginationControls(residencesPagesTotal);
}

// Function to go first page
function residencesPaginationFirst() {
    residencesPageNumber = 1;

    residencesPagination(residencesPageNumber);
    residencesPaginationControls(residencesPagesTotal);
}

// Function to go to last page
function residencesPaginationLast() {
    residencesPageNumber = residencesPagesTotal;

    residencesPagination(residencesPageNumber);
    residencesPaginationControls(residencesPagesTotal);
}

// Function to go to specific page
function residencesPageGoTo() {
    var residencesPageGoToValue = $('#residencesPageGoToInput').val();

    if (residencesPageGoToValue <= 0 || residencesPageGoToValue > residencesPagesTotal) {
        $.confirm({
            title: "Wrong value of input!",
            content: "Please insert a value between 1 and ".concat(residencesPagesTotal).toString().concat("!"),
            buttons: {
                ok: {}
            }
        });
    } else {
        residencesPageNumber = parseInt(residencesPageGoToValue);

        residencesPagination(residencesPageNumber);
        residencesPaginationControls(residencesPagesTotal);
    }
}

// Function of modal to delete residence and reload page
function deleteResidence(residenceId) {
    $.confirm({
        title: 'Remove residence',
        content: actionConfirmation,
        buttons: {
            confirm: function () {
                $.ajax({
                    type: 'POST',
                    data: { id: residenceId },
                    url: "Delete",
                    success: function () {
                        $.confirm({
                            title: '',
                            content: actionSuccessful,
                            buttons: {
                                ok: function () {
                                    residencesTotal();
                                    residencesPagination(residencesPageNumber);
                                }
                            }
                        });
                    },
                    error: function () {
                        $.alert(actionFailed);
                    }
                });
            },
            cancel: function () {
                $.alert(actionCancelled);
            }
        }
    });
}

function rateResidence(residenceId, ratingValue) {
    $.confirm({
        title: 'Confirm your rating!',
        content: 'You wanted to give a rating of '.concat(ratingValue).toString().concat('. Are you sure?'),
        buttons: {
            confirm: function () {
                $.ajax({
                    type: 'POST',
                    data: { residenceId: residenceId, ratingValue: ratingValue },
                    url: 'Rate',
                    success: function () {
                        $.confirm({
                            title: "",
                            content: actionSuccessful,
                            buttons: {
                                ok: function () {
                                    //$.ajax({
                                    //    type: 'GET',
                                    //    data: { residenceId: residenceId },
                                    //    url: '',
                                    //    success: function () {
                                    //        window.location.reload();
                                    //    }
                                    //});
                                    window.location.reload();
                                }
                            }
                        });
                    },
                    error: function () {
                        $.alert(actionFailed);
                    }
                });
            },
            cancel: function () {
                $.alert(actionCancelled);
            }
        }
    });
}

function searchResidences() {
    var name = $("#inputName").val();
    name = $.trim(name);
    var minPrice = $("#inputMinPrice").val();
    var maxPrice = $("#inputMaxPrice").val();

    $.ajax({
        type: 'POST',
        data: { name: name, minPrice: minPrice, maxPrice: maxPrice },
        url: 'Search',
        success: function (response) {
            $("#partialSearch").html(response);

            if (response.trim().length === 0) {
                $("#noResults").show();
                $("#nameAsc, #priceAsc, #nameDesc, #priceDesc").prop('disabled', true);
            } else {
                $("#noResults").hide();
                $("#nameAsc, #priceAsc, #nameDesc, #priceDesc").prop('disabled', false);
            }
        }
    });
}