"use strict"
$(document).ready(function () {

    $.ajax({
        type: "GET",
        url: '../Home/GetAllProductGroup',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {

            $.each(result, function (index, data) {
                $("#ddlProductGroup").append("<option value=" + data.selectionRecordId + ">" + data.selectionRecordName + "</option>");
            });
        }
    });

    $('#ddlProductGroup').change(function () {
        var productGroupId = this.value;
        $("#ddlProduct").empty();
        $.ajax({
            type: "GET",
            url: '../Home/GetAllProductByGroupId',
            data: ({ 'ProductGroupId': parseInt(productGroupId) }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { "ProductGroupId": productGroupId },
            success: function (result) {
                $("#ddlProduct").append("<option value=''>Select Product</option>");
                $.each(result, function (index, data) {

                    $("#ddlProduct").append("<option value=" + data.selectionRecordId + ">" + data.selectionRecordName + "</option>");
                });
            }
        });
    });

    $(function () {
        var dtToday = new Date();
        var month = dtToday.getMonth() + 1;
        var day = dtToday.getDate();
        var year = dtToday.getFullYear();
        if (month < 10)
            month = '0' + month.toString();
        if (day < 10)
            day = '0' + day.toString();

        var minDate = year + '-' + month + '-' + day;
        $('#txtEffectiveDate').attr('min', minDate);
    });

    $(function () {

        $('#txtEffectiveDate').change(function (event) {
            var addEffectiveDate = event.target.value;

            var dtToday = new Date(addEffectiveDate);
            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var minDate = year + '-' + month + '-' + day;
            $('#txtExpirationDate').attr('min', minDate);
        });
    });

    //$('#activeCheck').change(function () {
    //    if ($(this).is(":checked")) {
    //        var returnVal = this.checked;
    //        $(this).attr("checked", returnVal);
    //        $('#activeCheck').val($(this).is(':checked'));
    //    }
    //});
});