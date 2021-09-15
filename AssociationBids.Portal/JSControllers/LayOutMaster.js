function SendMessage() {
    debugger;
    var bodytext = "";
    bodytext= $("#txtMessageBody").val();
    if ($("#txtMessageBody").val() === "")
    {
        $("[id='txtMessageBody']").each(function () {
            bodytext = $(this).val();
            $(this).val('');
        });
    }
    if (bodytext !== '') {
        debugger;
        $.ajax({
            url: '../VenderBidrequest/SendInsertMessage',
            async: false,
            cache: false,
            type: "POST",
            dataType: "JSON",
            data: {
                Body: bodytext, Status: 'New', ObjectKey: $("#HdnObjectKey").val(), ResourceKey: $("#HdnLoginResourceKey").val(), ModuleKeyName: $("#HdnModuleKeyNameMsg").val(), Title: $("#HdnTitle").val(), VendorEmail: $("#HdnVendorEmail").val()
           },
            success: function (result) {
                if (result === 'Success') {
                    LoadMessagesList();
                    $("#txtMessageBody").val("");
                    //$("#divsucces").show();
                    //$("#SuccessMsg").html("Message Send successfully.");
                    //$("#SuccessMsg").show();
                    //$("#ErrorMsg").html("");
                    //$("#ErrorMsg").hide();
                    //$("#divalert").hide();
                }
            }
        });
    }
}


function LoadMessagesList(msgstatus) {
    $("#commentspm").html('');
    debugger;
    $.ajax({
        url: '../VenderBidrequest/SendInsertMessageList',
        cache: false,
        async:false,
        data: {
            ObjectKey: $("#HdnObjectKey").val(),
            ResourceKey: $("#HdnLoginResourceKey").val(),
            ModuleKeyName: $("#HdnModuleKeyNameMsg").val()
            , UpdatemsgStatus: msgstatus
        },
        success: function (response) {
            debugger;
            $("#MessageRawDatas div.table123:not(:first)").remove();
            $("#MessageRawDatas div.table123:eq(0)").show();
            response = JSON.parse(response);   
            if (parseInt(response.length) > 0) {
                $("[id='MsgTab']").each(function () {
                    $(this).val(response.length);
                });
                debugger;
                $("#MsgTab").html("Message (" + response.length + ")");
                var s = $(".modal #MsgTab");
                if (s != undefined)
                    s.html("Message (" + response.length + ")");
                for (var i = 0; i < response.length; i++) {

                    if (response[i].NewMessagecnt !== "0") {
                        $("#BidRequestsMenu").html("Bid Requests (" + response[i].NewMessagecnt + ")");
                    } else { $("#BidRequestsMenu").html("Bid Requests"); }
                    var table = $("#MessageRawDatas div.table123:eq(0)").clone(true);

                    if (response[i].RightLeft === 'Right') {
                        $("#DivRightSide", table).show();
                        $("#DivLeftSide", table).hide();
                        $("#DivLeftSide", table).attr("style", "display:none !important;");

                        $(".namefirstletter", table).html(response[i].FirstName);
                        $("#SendMesg", table).html(response[i].Body);
                        $("#SendTime", table).html(response[i].SendDate + ' ' + response[i].SendTime);
                    } else {
                        $("#DivLeftSide", table).show();
                        $("#DivRightSide", table).hide();
                        $("#DivRightSide", table).attr("style", "display:none !important;");

                        $(".namefirstletter", table).html(response[i].FirstName);
                        $("#ReceiveMesg", table).html(response[i].Body);
                        $("#ReceiveTime", table).html(response[i].SendDate + ' ' + response[i].SendTime);
                    }
                    //$(table).show();
                    $("#MessageRawDatas").append(table);
                }
                // $("#MessageRawDatas table tbody tr:eq(0)").hide();

                LoadCountNewMsg();
                // $("#message1").html('');
            } else {
                $("#MsgTab").html("Message");
                var s = $(".modal #MsgTab");
                if (s != undefined)
                    s.html("Message");
            }
            $("#MessageRawDatas div.table123:eq(0)").hide();
            $("#commentspm").html($("#message1").html());
            $(".loader-wrapper").hide();
        }
    });
}