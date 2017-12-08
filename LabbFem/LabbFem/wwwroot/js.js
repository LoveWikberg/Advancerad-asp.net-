$(document).ready(function () {

    $('#btn').click(function () {
        getOneCustomer(false);
    });
    $('#brief').click(function () {
        getOneCustomer(true);
    });

    $('#getAllCustomers').click(function () {
        $.ajax({
            url: 'api/values/allcustomers',
            method: 'GET'
        })
            .done(function (result) {
                $('#apiResult').empty();
                var html = "";
                result.forEach(function (element) {
                    html += printToTable(element);
                });
                $('.table tbody').html(html);
            })
            .fail(function (xhr, status, error) {
                $('#apiResult').html('<p style="color:red;">' + xhr.responseText + '</p>');
            });
    });

    $('#addForm').on("submit", function (event) {
        event.preventDefault();
        $.ajax({
            url: 'api/values/customer',
            method: 'POST',
            data: $(this).serialize()
        })
            .done(function (result) {
                $('#addedMessage').text(result);
            })
            .fail(function (xhr, status, error) {
                $('#addedMessage').text(xhr.responseText);
            });
    });

    $(document).on("click", ".fa-times", function (event) {
        event.stopPropagation();
        var id = $(this).parent().siblings('#customerId').text();
        $.ajax({
            url: 'api/values/customer',
            method: 'DELETE',
            data: { id: id }
        })
            .done(function (result) {
                var html = "";
                result.forEach(function (element) {
                    html += printToTable(element);
                });
                $('.table tbody').html(html);
            })
            .fail(function (xhl, status, error) {
                $('#apiResult').html('<p style="color:red;">' + xhr.responseText + '</p>');
            });
    });

    $(document).on("click", ".fa-pencil", function (event) {
        event.stopPropagation();
        var customer = $(this).parent().siblings().addBack().toArray();
        var customerInfo = {
            id: customer[0].textContent, firstName: customer[1].textContent
            , lastName: customer[2].textContent, email: customer[3].textContent
            , gender: customer[4].textContent, age: +customer[5].textContent
        };

        for (var key in customerInfo) {
            if (customerInfo.hasOwnProperty(key))
                $('#editForm [name="' + key + '"]').val(customerInfo[key]);
        }

        $('#editFormModalTitle').text("Edit " + customerInfo.firstName + " " + customerInfo.lastName);
        $('#editFormModal').modal('toggle');
    });

    $('#editForm').on('submit', function (event) {
        $('#editFormModal').modal('toggle');
        event.preventDefault();
        $.ajax({
            url: 'api/values/customer',
            method: 'OPTIONS',
            data: $(this).serialize()
        })
            .done(function (result) {
                var html = "";
                result.forEach(function (element) {
                    html += printToTable(element);
                });
                $('.table tbody').html(html);
            })
            .fail(function (xhr, status, error) {
                $('#apiResult').html('<p style="color:red;">' + xhr.responseText + '</p>');
            });
    });

    function getOneCustomer(isBriefSearch) {
        $.ajax({
            url: 'api/values/customer',
            method: 'GET',
            data: { id: $("#setId").val(), isBrief: isBriefSearch }
        })
            .done(function (result) {
                console.log(result);
                $('#apiResult').empty();
                var html = printToTable(result);
                $('.table tbody').html(html);
            })
            .fail(function (xhr, status, error) {
                $('.table tbody').empty();
                console.log(xhr, status, error);
                $('#apiResult').html('<p style="color:red;">' + xhr.responseText + '</p>');
            });
    }

    function printToTable(content) {
        var html = '<tr  data-toggle="collapse" data-target="#' + content.id + '" class="accordion-toggle">';
        html += '<th id="customerId">' + content.id + '</th>';
        html += '<td>' + content.firstName + '</td>';
        html += '<td>' + content.lastName + '</td>';
        html += '<td>' + content.email + '</td>';
        html += '<td>' + content.gender + '</td>';
        html += '<td>' + content.age + ' <span class="fa fa-times"></span> <span class="fa fa-pencil"></span></td>';
        html += '</tr>';
        html += '<tr>';
        html += '<td colspan="6" class="hiddenRow" style="margin:auto;">';
        html += '<div class="accordion-body collapse" id="' + content.id + '">';
        html += '<img src="CustomerImages/' + content.imageFileName + '" width="80" height="80"/>';
        html += '</div>';
        html += '</td>';
        html += '</tr>';
        return html;
    }

    $(document).on('show.bs.collapse', '.collapse', function () {
        $('.collapse').collapse('hide');
    });


});