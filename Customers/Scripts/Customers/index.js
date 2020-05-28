$("#add-person-btn").click(function () {
    var url = '/customers/addcustomer';
    var targetDiv = $("#add-customer-modal");
    loadModal(targetDiv, url);
});

$("#add-company-btn").click(function () {
    var url = '/customers/addcompany';
    var targetDiv = $("#add-customer-modal");
    loadModal(targetDiv, url);

});

function loadModal(targetDiv, targetUrl) {

    $.ajax({
        url: targetUrl,
        type: 'GET',
        error: function (xhr) {
            //TODO Add error handling for JS
        },
        success: function (result) {
            targetDiv.html(result);
        }
    });
}

//when ajax call is done init tooltips and validation for form
$(document).ajaxComplete(function () {
    intFormsValidation();
    $("#master-loader").hide();
});

$(document).ajaxStart(function () {
    $("#master-loader").show();
});

function intFormsValidation() {
    var forms = document.forms;
    for (var i = 0; i < forms.length; i++) {
        $.validator.unobtrusive.parse(forms[i]);
    }
}