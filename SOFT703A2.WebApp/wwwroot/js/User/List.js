$(document).ready(function () {
    function sortBy(opt) {
        var userName = $('#userName').val();
        var visitsCheckbox = $('#visitsCheckbox').is(':checked');
        var emailCheckbox = $('#emailCheckbox').is(':checked');
        var phoneCheckbox = $('#phoneCheckbox').is(':checked');

        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            url: filterUsersPath,
            type: 'GET',
            data: {
                userName: userName,
                visitsCheckbox: visitsCheckbox,
                emailCheckbox: emailCheckbox,
                phoneCheckbox: phoneCheckbox,
                sortBy: opt
            },
            headers: {
                RequestVerificationToken: token
            },
            success: function (data) {
                renderUserList(data); // Update the catalog with filtered products
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    }

    function renderUserList(users) {
        var tableBody = $('#user-table-body');
        tableBody.empty(); // Clear the existing product list
        if (users.length == 0) {
            $.ajax({
                url: '/Error/_404',
                type: 'GET',
                success: function (data) {
                    tableBody.html(data);
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error loading partial view:', textStatus, errorThrown);
                }
            });
        } else {
            $.each(users, function (index, user) {
                var detailUrl = detailsPath + '/' + user.Id;
                var deleteUrl = deletePath + '/' + user.Id;

                var row = '<tr>' +
                    '<td>' + user.UserName + '</td>' +
                    '<td>' + user.Email + '</td>' +
                    '<td>' + user.PhoneNumber + '</td>' +
                    '<td>' + user.FirstName + '</td>' +
                    '<td>' + user.LastName + '</td>' +
                    '<td>' + user.Logins.length + '</td>' +
                    '<td>' +
                    '<a href="' + detailUrl + '" class="btn btn-primary">Detail</a>  ' +
                    '<a href="' + deleteUrl + '" class="btn btn-danger">Delete</a>  ' +
                    '</td>' +
                    '</tr>';
                tableBody.append(row);
            });
        }

    }
    $('#loginSort').click(function () {
        sortBy('loginSort');
    });

    $('#usernameSort').click(function () {
        sortBy('usernameSort');
    });

    $('#nameSort').click(function () {
        sortBy('nameSort');
    });

    $('#searchButton').click(function () {
        sortBy();
    });
});