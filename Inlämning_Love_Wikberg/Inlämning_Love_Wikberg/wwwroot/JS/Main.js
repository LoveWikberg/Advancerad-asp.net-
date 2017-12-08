$(function () {

    getAllCustomers();

    //Add customer
    $("#addForm").on("submit", function (event) {
        event.preventDefault();
        editDatabaseContent("post", $(this).serialize());
        $("#addForm").trigger("reset");
        $('#customers-tab').trigger("click");
    });

    //Edit customer
    $('#editForm').on("submit", function () {
        event.preventDefault();
        var id = $('#editForm input[name="id"]').val();

        var tableData = $('th').filter(function () {
            return $(this).text() === id;
        });

        editDatabaseContent("put", $(this).serialize(), tableData);
        $('#editFormModal').modal('toggle');
    });

    // Remove customer
    $(document).on("click", ".fa-times", function (event) {
        event.stopPropagation();
        var id = $(this).parent().siblings('#customerId').text();
        editDatabaseContent("delete", { id: id }, $(this));
    });

    //Fill edit form
    $(document).on("click", ".fa-pencil", function (event) {
        event.stopPropagation();
        var customer = $(this).parent().parent().children();
        populateEditForm(customer);

        $('#editFormModal').modal('toggle');
    });

    $('#seed').click(function () {
        replaceAllDatabaseCustomersWithTextFileCustomers();
    });

    //Close other open collapses when a collapse is clicked
    $(document).on('show.bs.collapse', '.collapse', function () {
        $('.collapse').collapse('hide');
    });

    $('.fa-refresh').click(function () {
        var thisElement = $(this);
        $(this).addClass("spin");
        getAllCustomers();
        setTimeout(function () {
            $(thisElement).removeClass("spin");
        }, 1000);
    })

});

function replaceAllDatabaseCustomersWithTextFileCustomers() {
    $.ajax({
        url: "../api/customer/replaceDatabaseCustomersWithTextFileCustomers",
        method: "POST"
    })
        .done(function (result) {
            console.log(result);
            getAllCustomers();
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

function isInt(value) {
    return !isNaN(value) &&
        parseInt(Number(value)) == value &&
        !isNaN(parseInt(value, 10));
}

function populateEditForm(customer) {
    customer.each(function (index, cust) {
        if (isInt(cust.textContent)) {
            $('#editForm [name="' + cust.getAttribute("name") + '"]').val(+cust.textContent);
        }
        else {
            $('#editForm [name="' + cust.getAttribute("name") + '"]').val(cust.textContent);
        }
    });
}

function editDatabaseContent(method, data, tableData) {
    $.ajax({
        url: '/api/customer',
        method: method.toUpperCase(),
        data: data
    })
        .done(function (result) {
            if (method.toUpperCase() === "DELETE")
                removeCustomerFromTable(tableData.parent().parent());
            else if (method.toUpperCase() === "PUT")
                updateCustomerInTable(tableData.parent(), data.split('&'));
            else if (method.toUpperCase() === "POST")
                getAllCustomers();
        })
        .fail(function (xhr, status, error) {
            console.log("Error", xhr, status, error);
        });
}

function removeCustomerFromTable(customerToRemove) {
    customerToRemove.remove();
}

function updateCustomerInTable(rowToUpdate, serializedData) {
    for (var i = 0; i < serializedData.length; i++) {
        keyValuePair = serializedData[i].split('=');

        keyValuePair[1] = fixSerializedString(keyValuePair[1]);

        if (keyValuePair[0].toLowerCase() === "age") {
            var html = keyValuePair[1] + '<span class="fa fa-times"></span> <span class="fa fa-pencil"></span></td>';
            rowToUpdate.children('[name="' + keyValuePair[0] + '"]').html(html);
        }
        else
            rowToUpdate.children('[name="' + keyValuePair[0] + '"]').text(keyValuePair[1]);
    }
}

function getAllCustomers() {
    $.ajax({
        url: '/api/customer',
        method: "GET"
    })
        .done(function (result) {
            var html = "";
            result.forEach(function (customer) {
                html += setAndGetTableRow(customer);
            });
            $('.table tbody').html(html);
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}

function setAndGetTableRow(customer) {
    var html = '<tr  data-toggle="collapse" data-target="#' + customer.id + '" class="accordion-toggle">';
    html += '<th name="id" id="customerId">' + customer.id + '</th>';
    html += '<td name="firstName">' + customer.firstName + '</td>';
    html += '<td name="lastName">' + customer.lastName + '</td>';
    html += '<td name="email">' + customer.email + '</td>';
    html += '<td name="gender">' + customer.gender + '</td>';
    html += '<td name="age">' + customer.age + ' <span class="fa fa-times"></span> <span class="fa fa-pencil"></span></td>';
    html += '</tr>';
    html += '<tr>';
    html += '<td colspan="6" class="hiddenRow" style="margin:auto;">';
    html += '<div class="accordion-body collapse" id="' + customer.id + '">';
    html += '<p>Created: <b>' + customer.creationDate + '</b> | Most recent update: <b>' + customer.mostRecentUpdate + '</b></p>';
    html += '</div>';
    html += '</td>';
    html += '</tr>';
    return html;
}

function fixSerializedString(stringToFix) {
    if (stringToFix.includes("%C3%B6"))
        stringToFix = stringToFix.replace("%C3%B6", "ö");

    if (stringToFix.includes("%C3%A5"))
        stringToFix = stringToFix.replace("%C3%A5", "å");

    if (stringToFix.includes("%C3%A4"))
        stringToFix = stringToFix.replace("%C3%A4", "ä");

    if (stringToFix.includes("%40"))
        stringToFix = stringToFix.replace("%40", "@");

    if (stringToFix.includes("%20"))
        stringToFix = stringToFix.replace("%20", " ");

    return stringToFix;
}
