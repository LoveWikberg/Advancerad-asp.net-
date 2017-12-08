$(function () {

    getAllCustomers();

    $("#addForm").on("submit", function (event) {
        event.preventDefault();
        editDatabaseContent("post", $(this).serialize(), getAllCustomers());
        $("#addForm").trigger("reset");
        $('#customers-tab').trigger("click");
        getAllCustomers();
    });

    $('#editForm').on("submit", function () {
        event.preventDefault();
        editDatabaseContent("put", $(this).serialize(), getAllCustomers());
        $('#editFormModal').modal('toggle');
        getAllCustomers();
    });

    $(document).on("click", ".fa-times", function (event) {
        event.stopPropagation();
        var id = $(this).parent().siblings('#customerId').text();
        removeCustomerAnimation(id);
        editDatabaseContent("delete", { id: id }, removeCustomerAnimation(id));
    });

    $(document).on("click", ".fa-pencil", function (event) {
        event.stopPropagation();
        var customer = $(this).parent().siblings().addBack().toArray();
        var customerInfo = populateEditForm(customer);
        $('#editFormModalTitle').text("Edit " + customerInfo.firstName + " " + customerInfo.lastName);
        $('#editFormModal').modal('toggle');
    });

    $(document).on("click", "#btn", function () {
        $(this).parent().parent().remove();
    });

});

function populateEditForm(customer) {
    var customerInfo = {
        id: customer[0].textContent, firstName: customer[1].textContent
        , lastName: customer[2].textContent, email: customer[3].textContent
        , gender: customer[4].textContent, age: +customer[5].textContent
    };

    for (var key in customerInfo) {
        if (customerInfo.hasOwnProperty(key))
            $('#editForm [name="' + key + '"]').val(customerInfo[key]);
    }
    return customerInfo;
}

function editDatabaseContent(method, data, doAfterAjaxCall) {
    $.ajax({
        url: '/api/Customers',
        method: method.toUpperCase(),
        data: data
    })
        .done(function (result) {
            console.log(data);
            doAfterAjaxCall;
            //getAllCustomers();
        })
        .fail(function (xhr, status, error) {
            console.log("Error", xhr, status, error);
        });
}

function removeCustomerAnimation(id) {
    //var test = $('table').children('tr');
    var array = $('table #customerId').toArray();
    var element = {};
    for (var i = 0; i < array.length; i++) {
        if (array[i].textContent === id) {
            array[i].parentElement.remove();
            break;
        }
    }
}

function testApiCall() {

    $.ajax({
        url: "api/test/index",
        method: "GET"
    })
        .done(function (result) {
            console.log(result);
        })
        .fail(function (xhr, status, error) {
            console.log(xhr, status, error);
        });
}


function getAllCustomers() {
    $.ajax({
        url: '/api/customers',
        method: "GET"
    })
        .done(function (result) {
            var html = "";
            result.forEach(function (customer) {
                html += printTableRow(customer);
            });
            $('.table tbody').html(html);
        })
        .fail(function (xhr, status, error) {
            console.log("funkar inte | ajax fail");
            console.log(xhr, status, error);
        });
}

function printTableRow(customer) {
    var html = '<tr  data-toggle="collapse" data-target="#' + customer.id + '" class="accordion-toggle">';
    //var html = '<tr>';
    html += '<th id="customerId">' + customer.id + '</th>';
    html += '<td contenteditable="true">' + customer.firstName + '</td>';
    html += '<td>' + customer.lastName + '</td>';
    html += '<td>' + customer.email + '</td>';
    html += '<td>' + customer.gender + '</td>';
    html += '<td>' + customer.age + ' <span class="fa fa-times"></span> <span class="fa fa-pencil"></span></td>';
    html += '</tr>';
    html += '<tr>';
    html += '<td colspan="6" class="hiddenRow" style="margin:auto;">';
    html += '<div class="accordion-body collapse" id="' + customer.id + '">';
    html += '<p>Created: ' + customer.created + ' Updated: ' + customer.mostRecentUpdate + '</p>';
    html += '</div>';
    html += '</td>';
    html += '</tr>';
    return html;

    //This is for image
}


