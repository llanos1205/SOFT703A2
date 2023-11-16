function sortBy(opt) {
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
            promotedCheckbox: promotedCheckbox,
            sortBy: opt
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
}
function renderCatalog(products) {
    var catalogContainer = $('#catalog-container');
    catalogContainer.empty();
    if (products.length == 0) {
        $.ajax({
            url: '/Error/_404',
            type: 'GET',
            success: function (data) {
                catalogContainer.html(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error loading partial view:', textStatus, errorThrown);
            }
        });
    } else {
        $.each(products, function (index, product) {
            var detailUrlWithId = productDetailMarketUrl + '/' + product.Id;

            var card = '<div class="col-md-4 mb-4">' +
                '<div class="card" style="width: 250px; height: 300px;">' +
                '<a href=' + detailUrlWithId + '>' +
                '<img src="' + product.Photo + '" class="card-img-top" alt="Product Image" style="height: 190px;">' +
                '</a>' +
                '<div class="card-body">' +
                '<h6 class="card-title">' + product.Name + ' - $' + product.Price + '</h6>' +
                '<h7 class="card-title">' + product.Category.Name + '</h7>' +
                '</div>' +
                '<a href="javascript:void(0);" class="btn btn-primary addFromCatalog bg-green" data-product-id="' + product.Id + '">Add</a>' +
                '</div>' +
                '</div>';
            catalogContainer.append(card);
        });
    }
}
$(document).ready(function () {

    
    
        $('#searchButton').click(function () {
            sortBy();
        });

        function showOkMessage(message) {
            var $modal = $('#okModal');
            $modal.find('.modal-body').text(message);
            $modal.modal('show');
        }

        $('#catalog-container').on('click', '.addFromCatalog', function () {
            var productId = $(this).data('product-id').toString();
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: addToTrolleyPath,
                type: 'GET',
                data: {productId: productId},
                headers: {
                    RequestVerificationToken: token
                },
                success: function () {
                    fetchTrolleyData();
                }
            });
        });

        $('#trolley-container').on('click', '.addToTrolley', function () {
            var productId = $(this).data('product-id').toString();
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: addToTrolleyPath,
                type: 'GET',
                data: {productId: productId},
                headers: {
                    RequestVerificationToken: token
                },
                success: function () {

                    fetchTrolleyData();
                }
            });
        });

        // Use event delegation for removing items from the trolley
        $('#trolley-container').on('click', '.removeFromTrolley', function () {
            var productId = $(this).data('product-id').toString();
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: removeFromTrolleyPath,
                type: 'GET',
                data: {id: productId},
                headers: {
                    RequestVerificationToken: token
                },
                success: function () {

                    fetchTrolleyData();
                }
            });

        });

        function fetchTrolleyData() {
            var token = $('input[name="__RequestVerificationToken"]').val(); // Get the anti-forgery token from a hidden input field

            $.ajax({
                url: trolleyPath,
                type: 'GET',
                headers: {
                    RequestVerificationToken: token // Include the token in the request headers
                },
                success: function (data) {
                    // Clear the existing table content
                    var $tableBody = $('#trolley-container table tbody');
                    $tableBody.empty();

                    // Loop through the JSON data and build the table rows
                    $.each(data.ProductXTrolleys, function (index, item) {
                        var row = '<tr>' +
                            '<td>' + item.Product.Name + '</td>' +
                            '<td>' + item.Product.Price + '</td>' +
                            '<td>' + item.Quantity + '</td>' +
                            '<td>' + (item.Quantity * item.Product.Price) + '</td>' +
                            '<td>' +
                            '<a href="javascript:void(0);" class="btn btn-danger removeFromTrolley" data-product-id="' + item.ProductId + '" style="width: 40px">-</a>' +
                            '<a href="javascript:void(0);" class="btn btn-primary addToTrolley" data-product-id="' + item.ProductId + '" style="width: 40px">+</a>' +
                            '</td>' +
                            '</tr>';
                        $tableBody.append(row);
                    });
                    $('#trolley-container p').text('Total: $' + data.Total);

                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

        $('#trolley-container').on('click', '.checkoutTrolley', function () {
            var trolleyId = $(this).data('trolley-id').toString();
            var token = $('input[name="__RequestVerificationToken"]').val();
            $.ajax({
                url: checkOutPath,
                type: 'GET',
                data: {trolleyid: trolleyId},
                headers: {
                    RequestVerificationToken: token
                },
                success: function () {

                    fetchTrolleyData();
                    window.alert('Transaction Made');

                }
            });

        });
    });