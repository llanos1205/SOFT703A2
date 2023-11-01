﻿$(document).ready(function () {

    function renderCatalog(products) {
        var tableBody = $('#product-table-body');
        tableBody.empty(); // Clear the existing product list

        $.each(products.$values, function (index, product) {
            var detailUrl = productDetailPath + '/' + product.Id;
            var deleteUrl = productDeletePath + '/' + product.Id;
            var promoteUrl = productPromotePath + '/' + product.Id;

            var row = '<tr>' +
                '<td>' + product.Name + '</td>' +
                '<td><img src="' + product.Photo + '" width="100" height="100" /></td>' +
                '<td>' + product.Stock + '</td>' +
                '<td>' + product.Price + '</td>' +
                '<td>' +
                '<a href="' + detailUrl + '" class="btn btn-primary">Edit</a>' +
                '<a href="' + deleteUrl + '" class="btn btn-danger">Delete</a>' +
                '<a href="' + promoteUrl + '" class="btn btn-primary">Promote</a>' +
                '</td>' +
                '</tr>';
            tableBody.append(row);
        });
    }


    $('#searchButton').click(function () {
        var productName = $('#productName').val();
        var categoryCheckbox = $('#categoryCheckbox').is(':checked');
        var promotedCheckbox = $('#promotedCheckbox').is(':checked');

        var token = $('input[name="__RequestVerificationToken"]').val();
        $.ajax({
            url: filterProductsPath,
            type: 'GET',
            data: {
                productName: productName,
                categoryCheckbox: categoryCheckbox,
                promotedCheckbox: promotedCheckbox
            },
            headers: {
                RequestVerificationToken: token
            },
            success: function (data) {
                renderCatalog(data); // Update the catalog with filtered products
            },
            error: function (xhr, status, error) {
                console.error(xhr.responseText);
            }
        });
    });
});