function ConvertJsonDateToDateTime(date) {
    if (date.length > 0) {
        var parsedDate = new Date(parseInt(date.substr(6)));
        var newDate = new Date(parsedDate);
        var month = newDate.getMonth() + 1;
        var day = newDate.getDate();
        var year = newDate.getFullYear();
        return day + "/" + month + "/" + year;
    }
    else {
        return "";
    }
}
function updatePaging(TotalRecs) {
    $('ul.pagination').html('');
    var liClone = $('ul.pagination');
    debugger;
    var pageSize = $('#PageSize').val();
    var NoPage = 1;
    if (parseInt(TotalRecs) > parseInt(pageSize)) {
        NoPage = parseInt(parseInt(TotalRecs) / parseInt(pageSize));
        if (parseFloat(parseInt(parseInt(TotalRecs) / parseInt(pageSize))) < parseFloat(parseInt(TotalRecs) / parseInt(pageSize))) {
            NoPage = (parseInt(parseInt(TotalRecs) / parseInt(pageSize)) + 1);
        }
    }

    var lp = 0;//parseFloat(parseInt($('#HdnPageIndex').val()) / 5);
    if (parseFloat(parseInt($('#HdnPageIndex').val()) % 5) === 0) {
        lp = ((parseInt(parseInt($('#HdnPageIndex').val()) / 5) - 1) * 5);
    } else {
        lp = parseInt(parseInt($('#HdnPageIndex').val()) / 5) * 5;
    }

    var ind = 0;
    for (var i = 1; i <= NoPage; i++) {
        if ((parseInt(lp)) < i) {
            if (parseInt($('#HdnPageIndex').val()) > 5 && $(liClone).find('li.previous').length === 0) {
                $(liClone).append('<li class="paginate_button previous" aria-controls="DataTables_Table_0" tabindex="0" onclick="LoadPagingSortTableFill(' + "'" + (i - 1) + "'" + ')"><a href="#">Previous</a></li>');
            }
            if (ind < 5) {
                if (i === parseInt($('#HdnPageIndex').val())) {
                    $(liClone).append('<li style=""><a href="#" onclick="LoadPagingSortTableFill(' + "'" + i + "'" + ')" class="current">' + i + '</a></li>');
                } else {
                    $(liClone).append('<li style=""><a href="#" onclick="LoadPagingSortTableFill(' + "'" + i + "'" + ')">' + i + '</a></li>');
                }
            } else if ($(liClone).find('li.next').length === 0) {
                $(liClone).append('<li class="paginate_button next " aria-controls="DataTables_Table_0" tabindex="0" onclick="LoadPagingSortTableFill(' + "'" + i + "'" + ')"><a href="#">Next</a></li>');
            }
            ind = ind + 1;
        }
    }
}





function OpenPopup(ButtonName) {
    debugger;
    $("#PopAcceptByVender").hide();
    $("#PopDeclineByVender").hide();
    $("#PopCancelByVender").hide();
    $("#PopSubmitted").hide();
    if (ButtonName !== "Submitted" && ButtonName !== "Accepted") {
        if (ButtonName === 'AcceptByVender') {
            //Bid Request accepted successfully.
            $("#popupmsg").html("Are you sure you want to Accept Work Order ?");
            $("#acceptbidrequestLabel").html("Accept Work Order");
        }
        if (ButtonName === 'DeclineByVender') {
            $("#popupmsg").html("Are you sure you want to Decline Work Order ?");
            $("#acceptbidrequestLabel").html("Decline Work Order");
        }
        if (ButtonName === 'CancelByVender') {
            $("#popupmsg").html("Are you sure want to cancel Work Order ?");
            $("#acceptbidrequestLabel").html("Cancel Work Order");
        }
       // $("#btnSubmitted").hide();
    }
    else {
        $("#popupmsg").html("Are you sure you want to Submit Work Order ?");
        $("#acceptbidrequestLabel").html("Submit Work Order");
        // $("#btnSubmitted").hide();
        $("#PopSubmitted").show();
    }
    $("#Pop"+ButtonName).show();
}


function AcceptByVender(needupdate) { debugger;ApceptRejectVender('Interested', needupdate); }
function DeclineByVender(needupdate) {
    ApceptRejectVender('Not Interested', needupdate);
    //$("#HdnStatus").val('Not Interested');
    //if ($("#HdnStatus").val() === 'Submitted') {
    //    $("#btnSubmitted").html($("#btncopy").html() + "Update");
    //}
    //else { $("#btnSubmitted").html($("#btncopy").html() + "Submit Bid"); }
}
function CancelByVender(needupdate) { ApceptRejectVender('In Progress', needupdate); }
function UpdateButtonDisplay(Status,isExpired = 'false') {
    debugger;
    if (isExpired == 'false' || Status == 'Submitted') {
        $("#AcceptByVender").show();
        $("#DeclineByVender").show();
        $("#CancelByVender").hide();
        $("#divafterDue").hide();
        if ($("#HdnStatus").val() === 'Interested') {
            $("#AcceptByVender").hide();
            $("#DeclineByVender").show();
            $("#CancelByVender").show();
        }
        else if ($("#HdnStatus").val() === 'Not Interested') {
            $("#AcceptByVender").show();
            $("#DeclineByVender").hide();
            $("#CancelByVender").show();
        }
    } else {
        $("#AcceptByVender").hide();
        $("#DeclineByVender").hide();
        $("#CancelByVender").hide();
        $("#HdnStatus").val('');
        $("#divafterDue").show();
        $("#btncanpop").click();
    }
}
function calltoDateExtend() {
    $(".tabs-menu1 a[href='#message']").tab('show');
    $("#txtMessageBody").focus();
};
function callttoClose() {
    ApceptRejectVender('Not Interested', '2');
};
function ApceptRejectVender(StatusName, needupdate) {
    debugger;
    $("#HdnStatus").val(StatusName);

    $.ajax({
        url: '../VenderBidrequest/ApceptRejectVenderBidrequest',
        async: false,
        cache: false,
        type: "POST",
        dataType: "JSON",
        data: { BidVendorKey: $("#HdnObjectKey").val(), Status: StatusName },
        success: function (result) {
            debugger;
            if (result === 'Success') {
                if (needupdate === '1') {
                    UpdateButtonDisplay();
                }
                if (needupdate === '2') {
                    window.location.href = "/VenderWorkOrders/VenderWorkorder";
                }
                $("#btncanpop").click();
                if (StatusName === 'Interested') {
                 
                   

                    $("#ErrorMsg").html("");
                    $("#ErrorMsg").hide();
                    $("#divalert").hide();
                    LoadSortTableFill();
                } else if (StatusName === "Not Interested") {
                    $("#divsucces").hide();
                    $("#SuccessMsg").html("");
                    $("#SuccessMsg").hide();

                    $("#divalert").show();
                    $("#ErrorMsg").html("Work Order decline successfully.");
                    $("#ErrorMsg").show();
                }
                else if (StatusName === "In Progress") {
                    $("#divsucces").hide();
                    $("#SuccessMsg").html("");
                    $("#SuccessMsg").hide();
                    $("#divalert").show();
                    $("#ErrorMsg").html("Work Order cancel successfully.");
                    $("#ErrorMsg").show();
                }
                else if (StatusName === "Submitted") {
                    $("#divsucces").show();
                    $("#SuccessMsg").html("Work Order submitted cancel successfully.");
                    $("#SuccessMsg").show();

                    $("#ErrorMsg").html("");
                    $("#ErrorMsg").hide();
                    $("#divalert").hide();

                }

            }
        }
    });
}