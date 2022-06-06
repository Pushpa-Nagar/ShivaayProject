"use strict"

$(document).ready(function () {
        BindListing();
        });

    function BindListing() {
        if ($.fn.dataTable.isDataTable('#agreementTable')) {
            $('#agreementTable').DataTable().clear().destroy();
        }

        $("#agreementTable").DataTable({
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "aaSorting": [ [0, 'asc'], [1, 'desc'] ],
            "ajax": {
                "url": "/Home/GetAgreementList",
                "type": "POST",
                "datatype": "json",
                "dataSrc": "data"
            },
            "columnDefs": [
                {
                    "defaultContent": "-",
                    "targets": [0, 1, 3, 5, 6, 8],
                    "visible": false,
                    "searchable": [0, 1, 3, 5, 6, 8, 9, 10, 11, 12]
                }
            ],
            "columns": [
                { "data": "agreementId", "name": "AgreementId" },
                { "data": "userId", "name": "UserId"},
                { "data": "userName", "name": "User Name"},
                { "data": "productGroupId", "name": "ProductGroupId" },
                {
                    "data": "groupCode", "name": "Group Code",
                    "render": function (data, type, full, meta) {
                        return '<span data-toggle="tooltip" title="' + full['groupDescription'] + '"> ' + data; + ' </span>';
                    }
                },
                { "data": "groupDescription", "name": "Group Description" },
                { "data": "productId", "name": "ProductId" },
                {
                    "data": "productNumber", "name": "Product Number",
                    "render": function (data, type, full, meta) {
                        return '<span data-toggle="tooltip" title="' + full['productDescription'] + '">' + data; + '</span>';
                    }
                },
                { "data": "productDescription", "name": "Product Description"},
                {
                    "data": "effectiveDate", "name": "Effective Date",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        var day = date.getDate();
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + (day.toString().length > 1 ? day : "0" + day) + "/" + date.getFullYear();
                    }
                },
                {
                    "data": "expirationDate", "name": "Expiration Date",
                    "render": function (data) {
                        var date = new Date(data);
                        var month = date.getMonth() + 1;
                        var day = date.getDate();
                        return (month.toString().length > 1 ? month : "0" + month) + "/" + (day.toString().length > 1 ? day : "0" + day) + "/" + date.getFullYear();
                    }

                },
                { "data": "productPrice", "name": "Product Price"},
                { "data": "newPrice", "name": "New Price"},
                {
                    "render": function (data, type, row, meta) { return '<a class="btn btn-sm btn-info text-white mr-2" onclick="return OpenUpdatePopup(' + row.agreementId + ')"><i class="fa fa-edit"></i></a>' + "<a href='#' class='btn btn-sm btn-danger text-white' onclick='DeleleAgreement(" + row.agreementId + ")'><i class='fa fa-trash'></i></a>" }
                }
            ]
        });
    }

    function OpenAddPopup() {
        $.ajax({
            url: '/Home/LoadaddAgreementPopup',
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                $('#divcontent').empty();
                $('#divcontent').html(result);
                $('#AddUpdateModelPopup').modal('show');
            },
            error: function (xhr, status) {
                alert(status);
            }
        })
    }

    function OpenUpdatePopup(agreementId) {
        $.ajax({
            url: '/Home/LoadEditAgreementPopup?agreementId=' + agreementId,
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html',
            success: function (result) {
                $('#divcontent').empty();
                $('#divcontent').html(result);
                $('#AddUpdateModelPopup').modal('show');
            },
            error: function (xhr, status) {
                alert(status);
            }
        })
    }

        //Add Data Function
        function AddAgreement() {
            var res = ValidateForm();
            if (res == false) {
                return false;
            }
            var agreementObj = {
                productGroupId: $('#ddlProductGroup').val(),
                productId: $('#ddlProduct').val(),
                effectiveDate: $('#txtEffectiveDate').val(),
                expirationDate: $('#txtExpirationDate').val(),
                newPrice: $('#txtNewPrice').val(),
                active: $('#activeCheck').val(),
            };

            var data = new FormData();
            data = agreementObj;
            $.ajax({
                url: '/Home/AddAgreement',
                type: "POST",
                dataType: "json",
                data: data,
                success: function (result) {
                    if (result.isValid) {
                        BindListing();
                        $('#divcontent').empty('');
                        $('#divcontent').html('');
                        $('#AddUpdateModelPopup').modal('hide');
                    }
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }

        //function for updating Agreement's record
        function UpdateAgreement() {
            var res = ValidateForm();
            if (res == false) {
                return false;
            }
            var agreementObj = {
                agreementId: $('#agreementId').val(),
                productGroupId: $('#ddlProductGroup').val(),
                productId: $('#ddlProduct').val(),
                effectiveDate: $('#txtEffectiveDate').val(),
                expirationDate: $('#txtExpirationDate').val(),
                newPrice: $('#txtNewPrice').val(),
                active: $('#activeCheck').val(),
            };
            var data = new FormData();
            data = agreementObj;
            $.ajax({
                url: '/Home/UpdateAgreement',
                type: "POST",
                dataType: "json",
                data: data,
                success: function () {
                    BindListing();
                },
                error: function (errormessage) {
                    alert(errormessage.responseText);
                }
            });
        }

    //function for deleting Agreement's record
        function DeleleAgreement(agreementId) {
            var ans = confirm("Are you sure you want to delete?");
            if (ans) {
                $.ajax({
                    url: "/Home/DeleteAgreement?agreementId=" + agreementId,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    success: function () {
                        BindListing();
                    },
                    error: function (errormessage) {
                        alert(errormessage.responseText);
                    }
                });
            }
        }

    function ValidateForm() {
        var isValid = true;
        if ($('#ddlProductGroup').val().trim() == "") {
            $('#ddlProductGroup').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#ddlProductGroup').css('border-color', 'lightgrey');
        }
        if ($('#ddlProduct').val().trim() == "") {
            $('#ddlProduct').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#ddlProduct').css('border-color', 'lightgrey');
        }
        if ($('#txtEffectiveDate').val().trim() == "") {
            $('#txtEffectiveDate').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#txtEffectiveDate').css('border-color', 'lightgrey');
        }
        if ($('#txtExpirationDate').val().trim() == "") {
            $('#txtExpirationDate').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#txtExpirationDate').css('border-color', 'lightgrey');
        }

        if ($('#txtNewPrice').val().trim() == "") {
            $('#txtNewPrice').css('border-color', 'Red');
            isValid = false;
        }
        else {
            $('#txtNewPrice').css('border-color', 'lightgrey');
        }
        return isValid;
    }

    $('#agreementTable').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

$('#activeCheck').change(function () {
    if (this.checked == false) {
        $('#activeCheck').val($(this))
    } else {
        if ($(this).is(":checked")) {
            var returnVal = this.checked;
            $(this).attr("checked", returnVal);
            $('#activeCheck').val($(this).is(':checked'));
        }
    }
});
