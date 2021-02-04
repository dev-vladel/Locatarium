// Variables
var usersPageNumber = 1;
var usersPagesTotal = 0;
var $usersPageGoTo = $('#usersPageGoTo');

$(document).ready(function () {
    if (document.location.pathname.indexOf('/Users/Items') !== -1) {
        usersPagination(usersPageNumber);
    }

    if (usersPagesTotal === 0 && document.location.pathname.indexOf('/Users/Items') !== -1) {
        usersTotal();
    }
});

// Function to retrieve users and append the response to an id
function usersPagination(usersPageNumber) {
    var documentURL = document.location.pathname;
    if (documentURL.indexOf('Users/Items') !== -1) {
        $.ajax({
            type: 'GET',
            data: {
                usersPageNumber: usersPageNumber - 1
            },
            url: "ItemsPartial",
            success: function (response) {
                $('#usersPartial').html($(response));
                $('html, body').animate({ scrollTop: 0 }, 'slow');
            }
        });
    }
}

// Function to determine total of users and show/hide the button controls
function usersTotal() {
    var documentURL = document.location.pathname;
    if (documentURL.indexOf('/Users/Items') !== -1) {
        $.ajax({
            type: 'GET',
            url: 'ItemsTotal',
            success: function (response) {
                usersPagesTotal = Math.ceil(response / 16);

                usersPaginationControls(usersPagesTotal);

                $('#usersTotal').html(response);
                $('#usersPagesTotal').html(usersPagesTotal);

                if (usersPagesTotal === 0) {
                    $('#usersPaginationButtons, usersPageGoToInput').hide();
                } else {
                    $('#usersPaginationButtons, usersPageGoToInput').show();
                }
            }
        });
    }
}

// Function determining display of button controls and values
function usersPaginationControls(usersPagesTotal) {
    $('#usersButtonFirst, #usersButtonLast, #usersPageGoTo').show();
    $('#usersButtonCurrent').html(usersPageNumber);
    $('#usersButtonCurrent').prop('disable', true);

    if (usersPageNumber > 1) {
        $('#usersButtonPrev, #usersButtonLast').show();
        $('#usersButtonPrev').html(usersPageNumber - 1);
        $('#usersButtonPrev').prop('disabled', false);
    }

    if (usersPageNumber < usersPagesTotal) {
        $('#usersButtonNext').show();
        $('#usersButtonNext').html(usersPageNumber + 1);
        $('#usersButtonNext').prop('disabled', false);
    } else if (usersPageNumber === usersPagesTotal) {
        $('#usersButtonNext, #usersButtonLast').hide();
    }

    if (usersPageNumber === 1) {
        $('#usersButtonFirst, #usersButtonPrev').hide();
    }

    if (usersPageNumber === 0) {
        $('#usersButtonFirst, #usersButtonCurrent').hide();
    }
}

// Function to go to next page
function usersPaginationNext() {
    usersPageNumber += 1;

    usersPagination(usersPageNumber);
    usersPaginationControls(usersPagesTotal);
}

// Function to go to previous page
function usersPaginationPrev() {
    usersPageNumber -= 1;

    usersPagination(usersPageNumber);
    usersPaginationControls(usersPagesTotal);
}

// Function to go first page
function usersPaginationFirst() {
    usersPageNumber = 1;

    usersPagination(usersPageNumber);
    usersPaginationControls(usersPagesTotal);
}

// Function to go to last page
function usersPaginationLast() {
    usersPageNumber = usersPagesTotal;

    usersPagination(usersPageNumber);
    usersPaginationControls(usersPagesTotal);
}

// Function to go to specific page
function usersPageGoTo() {
    var usersPageGoToValue = $('#usersPageGoToInput').val();

    if (usersPageGoToValue <= 0 || usersPageGoToValue > usersPagesTotal) {
        $.confirm({
            title: "Wrong value of input!",
            content: "Please insert a value between 1 and ".concat(usersPagesTotal).toString().concat("!"),
            buttons: {
                ok: {}
            }
        });
    } else {
        usersPageNumber = parseInt(usersPageGoToValue);

        usersPagination(usersPageNumber);
        usersPaginationControls(usersPagesTotal);
    }
}

// User details live verification of password and confirm password
function checkConfirmPassword(password, confirmPassword) {
    if (password !== confirmPassword && confirmPassword !== null && password !== null) {
        $('#confirmPasswordSpan').html("Passwords do not match");
    }
    else {
        $('#confirmPasswordSpan').html("");
    }
}

// Function of modal to ban user and reload page
function banUser(userId) {
    $.confirm({
        title: "Ban User",
        content: actionConfirmation,
        buttons: {
            confirm: function () {
                $.ajax({
                    type: "POST",
                    data: { id: userId },
                    url: "Ban",
                    success: function () {
                        $.confirm({
                            title: "",
                            content: actionSuccessful,
                            buttons: {
                                ok: function () {
                                    usersPagination(usersPageNumber);
                                }
                            }
                        });
                    },
                    error: function (l) {
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

// Function of modal to unban user and reload page
function unbanUser(userId) {
    $.confirm({
        title: "Unban User",
        content: actionConfirmation,
        buttons: {
            confirm: function () {
                $.ajax({
                    type: "POST",
                    data: { id: userId },
                    url: "Unban",
                    success: function () {
                        $.confirm({
                            title: "",
                            content: actionSuccessful,
                            buttons: {
                                ok: function () {
                                    usersPagination(usersPageNumber);
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

