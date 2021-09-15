
       $(document).ready(function () {
           debugger;

           $("#AcceptByVender").hide();
           $("#DeclineByVender").hide();
           $("#CancelByVender").hide();
           $("#divafterdue").hide();
           $("#global-loader").show();
           $("#divbtns").hide();
           $("#divdanger").hide();
           $("#divalert").hide();

           LoadSortTableFill();
           LoadDocFile();
           $(".popback").hide();

           $("#BidRe").closest('li').addClass('active');
           $("#BidRe1").addClass('active');
           //$("#global-loader").hide();
       });
    function LoadMessages(msgstatus,isnoti,Bidid) {
        if ($("#HdnStatus").val() === 'Interested' || $("#HdnStatus").val() === 'Submitted') {
            $("#ViewBidrequest").hide();
        }
        LoadMessagesList(msgstatus);
        debugger;
        if (isnoti == 'true' && Bidid > 1) {
            notificationClick(Bidid, 'BidReqMsg');
        }
    }
    function BidDetails(msgstatus) {
        $("#ViewBidrequest").show();
    }

    function download(src) {

        var strWindowFeatures = "location=yes,height=570,width=520,scrollbars=yes,status=yes";
        window.open(src, "_blank", strWindowFeatures);
    }
    function viewimage(imgsrc) {
        debugger;
        $(".popback").show();
        $('#image').html("<img src='" + imgsrc + "' style= 'width:85%; margin-left:49px;margin-top:31px;' />");

    }
    function myclick() {

        $(".popback").hide();


    }

    function LoadSortTableFill() {
        $("#global-loader").show();
        $.ajax({
            url: '@Url.Action("GetVenderBidRequestListJson", "VenderBidrequest")',
            cache: false,
            data: {
                PageSize: 1,
                PageIndex: 1,
                Search: '',
                Sort: 'order by Title desc',
                BidVendorKey: $("#HdnObjectKey").val(),
                BidRequestStatus: 0,
                BiddueDateFrom: '',
                BiddueDateTo: '',
                ModuleController: $("#ModuleControllerhdn").val(),
            },
            success: function (response) {
                debugger;
                response = JSON.parse(response);
                $("#HdnStatus").val(response[0].Statuslookup);
                $("#HdnLoginResourceKey").val(response[0].LoginResourceKey);
                //Bid Details
                $("#Brservicename").html(response[0].Service);
                $("#Brtitle").html(response[0].Title);
                $("#BrDescription").html(response[0].Description);

                //$("#BrResponseDate").html((response[0].DefaultRespondByDate));
                $("#BrResponseDate").html((response[0].VendorResponseDueDate));
                $("#BrBidDueDate").html((response[0].VendorBidDueDate));
                //Property Details
                $("#BrCompanyName").html(response[0].CompanyName);
                $("#BrPropertyName").html(response[0].Property);
                $("#BrNoofUnit").html(formatNumber(response[0].NumberOfUnits,false));
                $("#BrAddress1").html(response[0].Address);
                $("#BrAddress2").html(response[0].Address2);
                $("#BrCity").html(response[0].City);
                $("#BrState").html(response[0].State);
                $("#BrZip").html(response[0].Zip);

                $("#BrContCompany").html(response[0].CompanyName);
                var a = "";
                a = response[0].WorkNumber;

                if (a === null || a === "" || a === "null") {

                }

                else {
                    $("#BrContPhone").html(formatPhoneNumber(response[0].WorkNumber));
                }


                $("#BrContName").html(response[0].ContactName);
                $("#BrContEmail").html(response[0].Email);
                if (response[0].isExpired.toLowerCase() == 'true' && response[0].Statuslookup != 'Submitted') {
                    $("#divbtns").hide();
                    $("#divdanger").show();
                    $("#errorText").html("Bid Response Due Date has expired");
                }
                else {
                    $("#divdanger").hide();
                    $("#divbtns").show();
                    $("#errorText").html('');
                }
                debugger;
                var isloaded = true;
                var msgcount = 0;
                if (response[0].Statuslookup === 'Interested' || response[0].Statuslookup === 'Submitted') {
                    isloaded = false;
                    if (response[0].NotificationType != null) {
                        var notitype = response[0].NotificationType.split(',');

                        for (var k = 0; k < notitype.length; k++) {
                            if (notitype[k] == 'BidReqMsg')
                                msgcount++;
                        }
                        debugger;
                    }

                    $.when(LoadSubmitBit($("#HdnObjectKey").val(), $("#HdnLoginResourceKey").val(), response[0].StartDate)).then(function () {
                        debugger;
                            if (msgcount > 0) {
                                $("#MsgTab").append(' <span class="badge badge-primary">' + msgcount + '</span>');
                                $("#MsgTab").attr('onclick', 'return LoadMessages("1","true",' + response[0].BidRequestKey + ');');
                            }

                        isloaded = true;
                    });
                }
                    UpdateButtonDisplay(response[0].isExpired.toLowerCase());
                    LoadMessagesList('0');

                //$("#Brtitle").html((response[i].Title));
                $("#global-loader").hide();


                if (response[0].NotificationType != null && isloaded) {
                    var notitype = response[0].NotificationType.split(',');
                    var msgcount = 0;
                    for (var k = 0; k < notitype.length; k++) {
                        if (notitype[k] == 'BidReqMsg')
                            msgcount++;
                    }
                    debugger;
                    if (msgcount > 0) {
                        $("#MsgTab").append(' <span class="badge badge-primary">' + msgcount + '</span>');
                        $("#MsgTab").attr('onclick', 'return LoadMessages("1","true",' + response[0].BidRequestKey + ');');
                    }
                }

                $(".loader-wrapper").hide();

            },
            error: function () {
                $("#global-loader").hide();
            }
        });
    }
      function LoadDocFile()
      {
          $("#RowDocBind table tbody tr:eq(0)").hide();
        $.ajax({
            url: '@Url.Action("GetVenderBidRequestDocumentListJson", "VenderBidrequest")',
            cache: false,
            data: {
                PageSize: 100,
                PageIndex: 1,
                Search:'',
                Sort: 'order by Filename desc',
                BidVendorKey: $("#HdnObjectKey").val(),
                TableName: $("#HdnModuleKeyNameDoc").val(),
            },
            success: function (response) {
                debugger;
                response = JSON.parse(response);
                if (parseInt(response.length) > 0) {

                    $("#RowimgBind div.table123:not(:first)").remove();
                    $("#RowimgBind div.table123:eq(0)").show();

                    $("#RowDocBind table tbody tr:not(:first)").remove();
                    //$("#RowDocBind table tbody tr:eq(0)").show();
                    for (var i = 0; i < response.length; i++) {
                        debugger;
                        var extension = response[i].FileName.substr((response[i].FileName.lastIndexOf('.') + 1));

                        var table = $("#RowDocBind table tbody tr:eq(0)").clone(true);
                        //$("#docname", table).html(response[i].FileName);



                        $("#docname", table).html(response[i].FileName1);
                        $("#docname", table).attr('href', "../Document/Properties/" + response[i].FileName);
                        $("#docname", table).attr('onclick', "download('../Document/Properties/" + response[i].FileName + "')");
                        //$("#docname", table).attr('onclick', "ViewDoc(" + response[i].DocumentKey + "," + response[i].ObjectKey + ",'" + response[i].FileName + "')");
                        $("#RowDocBind table").append(table);

                        if (extension === 'jpg' || extension === 'png' || extension === 'gif') {
                            var table = $("#RowimgBind div.table123:eq(0)").clone(true);
                            $("#docname", table).html(response[i].FileName);
                            $("#docimgurl", table).attr('src', "../Document/Properties/" + response[i].FileName);
                            $("#docimgurl", table).attr('onclick', "viewimage('../Document/Properties/" + response[i].FileName + "')");
                            $("#RowimgBind").append(table);
                            table.show()
                        }

                    }

                }

                $("#RowimgBind div.table123:eq(0)").hide();
                $("#RowDocBind table tbody tr:eq(0)").hide();
                $(".loader-wrapper").hide();
            }
        });
    }
    function ViewDoc(DocumentKey, ObjectKey, FileName) {
        window.location.href = '../Document/Properties/'+FileName;
    }
    function LoadSubmitBit(BidVendorKey, ResourceKey, BidStartDate) {
        debugger;
        $("#global-loader").show();
        $("#ViewBidrequestMoreClone").html("");
        var pageurl = '../VenderBidrequest/ViewBidrequestMoreClone';
        var mycoc = $("#ViewBidrequestMoreClone");
        mycoc.show();
        $(".Clonehide").hide();
        mycoc.load(pageurl,
            {
                BidVendorKey: BidVendorKey,
                ResourceKey: ResourceKey,
                BidStartDate: BidStartDate
            },

            function (data) {
                $(".Clonehide").hide();
                if (BidStartDate !== "01/01/1900") {
                   // $("#pdate").html(BidStartDate);
                }
                if ($("#HdnStatus").val() === 'Interested') {
                    $("#btnSubmitted").show();
                    $("#btnUpdateBid").hide();
                }
                //$("#btnSubmitted").text("Submit Bid");
                if ($("#HdnStatus").val() === 'Submitted') {
                    //$("#btnSubmitted").html($("#btncopy").html()+"Update");
                    $("#btnSubmitted").hide();
                    $("#btnUpdateBid").show();
                }
                $("#divafterdue").hide();
            });
    }
    function notificationClick(nid, type) {
        debugger;
        $.ajax({
            url: '/ABNotification/NotificationUpdate',
            cache: false,
            data: {
                NotiId: nid,
                Status: 'read',
                NotiType: type,
                ByObj: true
            },
            success: function (response) {
                debugger;
                if (response == 'true' || response == true) {
                    $("#MsgTab span").animate({ height: '0px', width: '0px', opacity: '0.4' }, 2000, function () {
                        $("#MsgTab span").remove()
                    });
                    $(".header-notify").load('@Url.Action("Notification","ABNotification")');
                }
            }
        });
    }
