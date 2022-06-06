"use strict"
$(document).ready(function () {

    var productGroupId = $("#productGroupId").val();
    var productId = $("#productId").val();
    var dateEffective = $("#dateEffective").val();
    var dateExpiration = $("#dateExpiration").val();

    $.ajax({
        type: "GET",
        url: '../Home/GetAllProductGroup', //'@Url.Action("GetAllProductGroup", "Home")',
        async: true,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $.each(result, function (index, data) {
                $("#ddlProductGroup").append("<option value=" + data.selectionRecordId + ">" + data.selectionRecordName + "</option>");
            });
            $("#ddlProductGroup").val(productGroupId);
        }
    });

    $.ajax({
        type: "GET",
        url: '../Home/GetAllProductByGroupId', //'@Url.Action("GetAllProductByGroupId", "Home")',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { "productGroupId": productGroupId },
        success: function (result) {
            $.each(result, function (index, data) {
                $("#ddlProduct").append("<option value=" + data.selectionRecordId + ">" + data.selectionRecordName + "</option>");
            });
            $("#ddlProduct").val(productId);
        }
    });

    $('#ddlProductGroup').change(function () {
        productGroupId = this.value;
        $("#ddlProduct").empty();
        $.ajax({
            type: "GET",
            url: '../Home/GetAllProductByGroupId',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { "productGroupId": productGroupId },
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
        var dtTodayNew = new Date(dateEffective);
        if (dtTodayNew > dtToday) {
            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var minDate = year + '-' + month + '-' + day;
            $('#txtEffectiveDate').attr('min', minDate);
        }
        else {
            var month = dtTodayNew.getMonth() + 1;
            var day = dtTodayNew.getDate();
            var year = dtTodayNew.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var minDate = year + '-' + month + '-' + day;
            $('#txtEffectiveDate').attr('min', minDate);
        }
    });

    $(function () {
        var dtToday = new Date();
        var dtTodayNew = new Date(dateExpiration);
        if (dtTodayNew > dtToday) {
            var month = dtToday.getMonth() + 1;
            var day = dtToday.getDate();
            var year = dtToday.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var minDate = year + '-' + month + '-' + day;
            $('#txtExpirationDate').attr('min', minDate);
        }
        else {
            var month = dtTodayNew.getMonth() + 1;
            var day = dtTodayNew.getDate();
            var year = dtTodayNew.getFullYear();
            if (month < 10)
                month = '0' + month.toString();
            if (day < 10)
                day = '0' + day.toString();

            var minDate = year + '-' + month + '-' + day;
            $('#txtExpirationDate').attr('min', minDate);
        }
    });
});
