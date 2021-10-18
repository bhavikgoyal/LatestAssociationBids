var geocoder;
var map;



$(document).ready(function ()
{
    document.getElementById('lblresult').innerHTML = '';
 
    $('#sucess').hide();
    $('#Error').hide();    
    $(".chosen-select").chosen();
    //$("#btnnextcard").attr("disabled", true);
    $("#btnAgreementtop").attr("disabled", true);
    $("#btnnextagreement").attr("disabled", true);
    $("#btnAccpeted").attr("disabled", true);
    $("#btnnextPricing").attr("disabled", true);
    $("#btnnextUSerAggr").attr("disabled", true);
    //$('#btnnextPricing').hide();
    $("#btnconfirmtop").attr("disabled", true);
    $("#btnPricingtop").attr("disabled", true);
    //alert($("#MapCheck").val());
});


function OnChangecard() {
    debugger;
    $('#sucess').hide();
    $('#Error').hide();
    $("#btnnextcard").attr("disabled", true);
    $("#SkipIcc").attr("disabled", false);
    $("#btnverifycard").attr("disabled", false);
}


function OnChangeCompany()
{
    debugger;

    var a = $("#CompanyKey").val();
    $('#Error').hide();
    $.ajax({

        url: "/Registration/IsCompanyNameExits",
        type: "GET",
        data: { Name: $('#CompanyName').val(), Id: a},
        success: function (response)
        {
            if (response === true)
            {
                $('.errormessage').html( "'"+ $('#CompanyName').val() +   "' already exist.");
                $('#Error').show();
                $(window).scrollTop(0);
                $('#CompanyName').focus();
                return false;
            }                        
        }       
    });

}
function OnRadiusChanges()
{
    setTimeout(function () {
     
        initMap();
    }, 1000);
}
function OnChangeEmail()
{

    var RKey = 0;
    RKey = $("#rid").val()
    if (RKey == null || RKey == 0 || RKey == '')
        RKey = 0;
    $.ajax({
        url: "/Registration/IsEmailExits",
        type: "GET",
        data: { Name: $('#EmailId').val(), ResourceKey: RKey,Id: 0 },
        async:false,
        success: function (response) {
            if (response === true) {                
                $('.errormessage').html("'" + $('#EmailId').val() + "' already exist.");
                $('#Error').show();
                $(window).scrollTop(0);
                $('#EmailId').val("");
                $('#EmailId').focus();
                return false;
            } else {
                return true;
            }
        }
    });

}

function Fillvalueview()
{
    debugger;
    $('#ServiceAddress').val($('#Address').val() + ' ' + $('#Address2').val());
    $('#vServiceAddress').html($('#Address').val() + ' ' + $('#Address2').val());
    $('#vserviceaddressc').html($('#Address').val() + ' ' + $('#Address2').val());
    $('#vRadius').html($('#Radius option:selected').text());
    $('#vCompanyName').html($('#CompanyName').val());
    $('#vLegalName').html($('#LegalName').val());
    $('#vTaxID').html($('#TaxID').val());
    if ($('#Address2').val() !== "") {
        $('#vServiceAddress').html($('#Address').val() + ', ' + $('#Address2').val() + ', ' + $('#City').val() + ', ' + $('#State option:selected').text() + '-' + $('#Zip').val());
        $('#vserviceaddressc').html($('#Address').val() + ', ' + $('#Address2').val() + ', ' + $('#City').val() + ', ' + $('#State option:selected').text() + '-' + $('#Zip').val());
    }
    else {

        $('#vServiceAddress').html($('#Address').val() + '' + $('#Address2').val() + ', ' + $('#City').val() + ', ' + $('#State option:selected').text() + '-' + $('#Zip').val());
        $('#vserviceaddressc').html($('#Address').val() + '' + $('#Address2').val() + ', ' + $('#City').val() + ', ' + $('#State option:selected').text() + '-' + $('#Zip').val());
    }
    $('#vAddress').html($('#Address').val());
    $('#vAddress2').html($('#Address2').val());
    $('#vCity').html($('#City').val());
    $('#vStateKey').html($('#StateKey').val());
    $('#vState').html($('#State').val());
    $('#vZip').html($('#Zip').val());
    $('#vWork').html($('#Work').val());
    $('#vWork2').html($('#Work2').val());
    $('#vWebsite').html($('#Website').val());
    $('#vFax').html($('#Fax').val());
    $('#vNameOfCard').html($('#NameOfCard').val());
    $('#vfirstname').html($('#FirstName').val());
    $('#vlastname').html($('#LastName').val());
    $('#vEmail').html($('#EmailId').val());

  
    var policyno = $("#PolicyNumber").val();
    var files = $("#Insurancefiles")[0].files;
    //if (files.length > 0 && policyno != '') 
    {
        $("#policyno").html(policyno);
        $("#insuranceamount").html($("#InsuranceAmount").val());
        $("#startdate").html($("#StartDate").val().split(" ")[0] == '1/1/0001' ? '' : $("#StartDate").val());
        $("#enddate").html($("#EndDate").val().split(" ")[0] == '1/1/0001' ? '' : $("#EndDate").val());
        $("#renewaldate").html($("#RenewalDate").val().split(" ")[0] == '1/1/0001' ? '' : $("#RenewalDate").val());
        var docnames = $("#InsurancefilesView").val();

        

        for (var i = 0; i < files.length; i++) {

            if (docnames.indexOf(files[i].name) != -1) {
                console.log(files[i].name + " found");
            }
            else
            {
                docnames += '<p>' + files[i].name + '</p>,';
            }
            
        }

        //if ($("#docname").text() != "" && docnames == "") {
        //}
        //else
        //{
        //    $("#docname").html($("#docname").text() + docnames);
        //}
        $("#InsurancefilesView").val(docnames);
        if (policyno != '')
        {
            $("#insDetails").show();
        }
        else {
            $("#insDetails").hide();
        }
    }

    if ($('#CardNumber').val() !== "")
    {
        const id = $('#CardNumber').val();
        var fourlastchar = id.substr(id.length - 5);
        $('#vCardNumber').html('XXXX XXXX XXXX ' + fourlastchar);
    }

    $('#vValidTillMM').html($('#ValidTillMM').val());
    $('#vValidTillYY').html($('#ValidTillYY').val());
    $('#vFullValidTill').html($('#ValidTillMM').val() + '/' + $('#ValidTillYY').val());
    if ($('#vFullValidTill').html() == "/")
    {
        $('#vFullValidTill').html('');
    }
    $('#vCVV').html($('#CVV').val());
    $('#sucesssave').hide();
    $('#errrorssave').hide();

    $("#ServiceTitle1v").val($("#Service1 option:selected").text());
    $("#ServiceTitle2v").val($("#Service2 option:selected").text());
    $("#ServiceTitle3v").val($("#Service3 option:selected").text());
    var service = "";

    if ($("#ServiceTitle1v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle1v").val() + "<br>";
    }
    if ($("#ServiceTitle2v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle2v").val() + "<br>";
    }
    if ($("#ServiceTitle3v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle3v").val();
    }
    $("#serviceName").html(service);
}

function onchangePricingbox() {
    debugger;
    if ($("#chkPricing").prop("checked") === true) {
        $("#btnconfirmtop").attr("disabled", false);
        
        $("#btnAccpeted").attr("disabled", false);
        
        
        $(window).scrollTop(0);
        return false;
    }
    else {
        
        $("#btnconfirmtop").attr("disabled", true);
        $("#btnAccpeted").attr("disabled", true);
        $("#btnnextPricing").attr("disabled", true);
        $(window).scrollTop(0);
        return false;

    }




}

function onchangecheckbox()
{
    
    if ($("#chkagreement").prop("checked") === true)
    {
        $('#btnnextUSerAggr').show();
        $("#btnnextUSerAggr").attr("disabled", false);
        $(window).scrollTop(0);
        return false;
    }
    else
    {   

        $('#btnnextUSerAggr').hide();
        $("#btnnextUSerAggr").attr("disabled", true);
            $(window).scrollTop(0);
            return false;
      
    }
 

    

}

$("#btnnextUSerAggr").click(function () {

    var value = $("#InsurancefilesView").val();
    var value1 = value.split(',');
    value = "";
    for (var i = 0; i < value1.length;i++)
    {
        if (value1[i - 1] != value1[i])
        {
            value += value1[i];
        }
    }
    $("#docname").html(value);
});

$("#btnAccpeted").click(function () {
    var a = "";
    //$("#btnAccpeted").hide();
    $('#btnnextPricing').show();
    $("#btnnextPricing").attr("disabled", false);
    //$('#lblresult').val(a);
    return ;
});

$("#btncompany").click(function () {
    var a = "";
    $('#lblresult').val(a);
    return validation();
});
$("#btncompanyinfotop").click(function () {
    return primarycontectvalidation();
});

$("#btnnextPricing").click(function () {
    $('#sucess').hide();
    $('#Error').hide();
});

$("#CardNumber").on("keypress keyup blur", function (event) {

    $(this).val(function (index, value)
    {
        return value.replace(/[^a-z0-9]+/gi, '').replace(/(.{4})/g, '$1 ');
    });
   
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
   
});



$("#btntopprimary").click(function () {
    
    return validation();

});

$("#btnbackk").click(function () {
    debugger;
   
    var a = null;
    $("#lblresult").val(a);

});
$("#btnnextemail").click(function () {

    return primarycontectvalidation();
});



$("#ValidTillMM").on("keypress keyup blur", function (eventfv) {

    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$("#CVV").on("keypress keyup blur", function (event) {

    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});
$("#btnverifycard").click(function () {

    debugger;
    var fname = $('#FirstName').val();
    var lname = $('#LastName').val();
    $("#global-loader").show();
     if ($('#CardNumber ').val() === "" || $('#ValidTillMM ').val() === "" || $('#ValidTillYY').val() === "" || $('#CVV').val() === "") {
        $('.errormessage').html('Please enter mandatory fields.');
        $('#Error').show();
        $(window).scrollTop(0);
        event.preventDefault();
        $("#global-loader").hide();
        return false;
     }

    $.ajax({

        url: "/Registration/PaymentVerificationAsync",
        type: "GET",
        data: { Fname: fname, Lname: lname, CardNumber: $('#CardNumber').val(), Month: $('#ValidTillMM').val(), Year: $('#ValidTillYY').val(), CVV: $('#CVV').val(), value: 0, name: $('#CompanyName ').val(), addressline1: $('#Address').val(), addressline2: $('#Address2').val(), zip: $('#Zip').val(), city: $('#City').val(), state: $('#State').val() },
        success: function (response) {
            if (response === true) {

                debugger;
                $('.sucessmessage').html('Card has been verified sucessfully.');
                $("#btnnextcard").attr("disabled", false);
                $("#btnAgreementtop").attr("disabled", false);
                $("#SkipIcc").attr("disabled", true);             
                $("#btnverifycard").attr("disabled", true);   
                $('#Error').hide();
                $('#sucess').show();
                $(window).scrollTop(0);
                $('#CardNumber').focus();
                $("#global-loader").hide();
                event.preventDefault();
                return false;
            }
            if (response === false) {
                $('.errormessage').html('Invalid Credit Card number.');
                $("#btnnextcard").attr("disabled", true);
                $("#SkipIcc").attr("disabled", false);
                $("#btnverifycard").attr("disabled", false);   
                $('#sucess').hide();
                $('#Error').show();
                $(window).scrollTop(0);
                $('#CardNumber').focus();
                $("#global-loader").hide();
                event.preventDefault();
                return false;

            }


        }
    });
});





$("#btnseviceareatop").click(function ()
{
    $('#Error').hide();
    $('#sucess').hide();
    validation();
    validationservice();
    return validationRadius();
});

$("#btninsurancetop").click(function () {
    $('#Error').hide();
    $('#sucess').hide();
    var s = true;
    s = validation();
    if (s == false)
        return s;
    s = primarycontectvalidation();
    if (s == false)
        return s;
    debugger;
    s = validationservice();
    if (s == false)
        return s;
    
    return validationRadius();
});

$("#btnserviceareatop").click(function () {

    $("#ServiceTitle1v").val($("#Service1 option:selected").text());
    $("#ServiceTitle2v").val($("#Service2 option:selected").text());
    $("#ServiceTitle3v").val($("#Service3 option:selected").text());

    var service = "";

    if ($("#ServiceTitle1v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle1v").val() + "<br>";
    }
    if ($("#ServiceTitle2v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle2v").val() + "<br>";
    }
    if ($("#ServiceTitle3v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle3v").val();
    }
    $("#serviceName").html(service);

    setTimeout(function () {
        debugger;
        initMap();
    }, 1000);
    validation();
    return validationservice();
});
$("#btncreadtiocardtop").click(function () {
   
    validation();
    validationservice();    
    validationRadius();
    return validationInsurance();
});
$("#btnPricingtop").click(function () {
    $('#Error').hide();
    $('#sucess').hide();
    validation();
    validationservice();
    validationRadius();
    return validationInsurance();
});
$("#btnconfirmtop").click(function () {
    fillserivces();
    validation();
    validationservice();    
    return validationRadius();
});
$("#btnAgreementtop").click(function () {
    $('#Error').hide();
    $('#sucess').hide();
    validation();
    validationservice();
    return validationRadius();
});

$("#btnnextcompnay").click(function () {
   debugger;
    return validation();
});

$("#btnnextservice").click(function () {
   
    validation();
    
    $("#ServiceTitle1v").val($("#Service1 option:selected").text());
    $("#ServiceTitle2v").val($("#Service2 option:selected").text());
    $("#ServiceTitle3v").val($("#Service3 option:selected").text());
    var service = "";

    if ($("#ServiceTitle1v").val() != "--- Select Service ---")
    {
        service += $("#ServiceTitle1v").val() + "<br>";
    }
    if ($("#ServiceTitle2v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle2v").val() + "<br>";
    }
    if ($("#ServiceTitle3v").val() != "--- Select Service ---") {
        service += $("#ServiceTitle3v").val();
    }
    $("#serviceName").html(service);

    //$("#serviceName").html($("#ServiceTitle1v").val());
    
         
    setTimeout(function () {
    
        initMap();
    },1000);
    return validationservice();
});
$("#btnnextservicearea").click(function () {

    validation();
    validationservice();
    return validationRadius();
});

$("#btnsubmit").click(function ()
{
  
    validationservice();
    validationRadius();
    return validation();
});

$("#btnnextagreement").click(function ()
{
    debugger;
    $('#sucess').hide();
    $('#Error').hide();
    $('#sucesssave').hide();
    $('#errrorssave').hide();
    fillserivces();
    validation();
    validationservice();
    return validationRadius();
     
});


   function initMap()
   {
       map = new google.maps.Map(document.getElementById('map'), { zoom: 10});
       map1 = new google.maps.Map(document.getElementById('map1'), { zoom: 10});
       geocoder = new google.maps.Geocoder();       
       codeAddress(geocoder, map);
       codeAddressview(geocoder, map1);   
}


function codeAddress(geocoder, map)
{
  
    geocoder.geocode({ 'address': $("#vServiceAddress").html() }, function (results, status) {
       
        if (status === 'OK') {
            var str = $("#Radius option:selected").text().split(" ");
            if (str[0] === 'Please') {
                str = '0';
            }

            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
            var circle = new google.maps.Circle({
                map: map,
                radius: str[0] * 1609.344,
                fillOpacity: 0.25,               
                fillColor: '#AA0000'
            });
            circle.bindTo('center', marker, 'position');
            map.setCenter(results[0].geometry.location);
        }
       
    });
}
function codeAddressview(geocoder, map1) {

    geocoder.geocode({ 'address': $("#vServiceAddress").html() }, function (results, status)
    {

        if (status === 'OK')
        {
            var str = $("#Radius option:selected").text().split(" ");
            if (str[0] === 'Please')
            {
                str = '0';
            }
           
            var marker = new google.maps.Marker({
                map: map1,                
                position: results[0].geometry.location
            });
            var circle = new google.maps.Circle({
                map: map1,
                radius: str[0] * 1609.344,
                fillOpacity: 0.25,  
                fillColor: '#AA0000'
            });
            circle.bindTo('center', marker, 'position');
            map1.setCenter(results[0].geometry.location);
        }

    });
}



function primarycontectvalidation()
{
    debugger;
    var email = $('#EmailId').val();
    var reg = /^([A-Za-z0-9_\-\.]+)@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*(\.[A-Za-z]{2,3})$/;
    if (!reg.test(email))
    {
        $('.errormessage').html('Invalid Email Address.');
        $('#Error').show();
        $(window).scrollTop(0);
        event.preventDefault();
        return false;
    }
    

    $('#sucess').hide();
    $('#Error').hide();
    Fillvalueview();
    $('#sucesssave').hide();
    $('#errrorssave').hide();

    if ($('#FirstName ').val() === "" || $('#LastName ').val() === "" || $('#EmailId').val() === "")
    {
        $('.errormessage').html('Please enter mandatory fields.');
        $('#Error').show();
        $(window).scrollTop(0);
        event.preventDefault();
        return false;
    }
    $('#Error').hide();
    return true;  

}


function validation()
{
    debugger;
    $('#sucess').hide();
    $('#Error').hide();
    Fillvalueview();
    $('#sucesssave').hide();
    $('#errrorssave').hide();
    if ($('#CompanyName ').val() === "" || $('#Address').val() === "" || $('#Zip').val() === "" || $('#City').val() === "" || $('#State').val() === "0") {
        $('.errormessage').html('Please enter mandatory fields.');
        $('#Error').show();
        $(window).scrollTop(0);

        return false;
    }            
    
    var lat = $('#lblresult').val();
    if (lat == null || lat == "") {

        //var Isvalid = false;
        var Isvalid = true;
        $('.errormessage').html('');
        $("#Error").hide();
        //var MapCheck = '@System.Configuration.ConfigurationManager.AppSettings["MapCheck"].ToString()';
        //var MapCheck = '@System.Configuration.ConfigurationManager.AppSettings["MapCheck"].ToString()';
        //alert(MapCheck);
        var MapCheck = $("#MapCheck").val();
        var geocoder = new google.maps.Geocoder();
        var con = $('#Address').val();
        var Adde = $('#Address2').val();
        var city = $('#City').val();
        var zip = $('#Zip').val();
        var res = document.getElementById('lblresult');
        var com = city + ',' + $('#State').val();
        if (MapCheck == "Active") {
            geocoder.geocode({
                componentRestrictions: {

                    country: 'USA',
                    postalCode: zip
                }
            },

                function (results, status) {
                    debugger;
                    if (status == 'OK') {

                        var marker = new google.maps.Marker({

                            position: results[0].geometry.location


                        });
                        res.innerHTML = "Latitude : " + results[0].geometry.location.lat() + "<br/>Longitude :" +
                            results[0].geometry.location.lng();
                        $('#lblresult').val(results[0].geometry.location.lat());
                        $('#Latitude').val(results[0].geometry.location.lat());
                        $('#Longitude').val(results[0].geometry.location.lng());
                        $("#btnnextcompnay").click();

                    }
                    else {
                        $('.errormessage').html('Invalid address, please enter accurate address');
                        $('#Error').show();
                        $(window).scrollTop(0);
                        Isvalid = false;
                    }
                })
            return false;
        }
    }
    else {
        return true;
    }



    $('#Error').hide();

    return true;   
}
function validationservice()
{
    debugger;
    $('#sucess').hide();
    $('#Error').hide();
    $('#sucesssave').hide();
    $('#errrorssave').hide();
    var ser1 = $("#Service1").val();
    var ser2 = $("#Service2").val();
    var ser3 = $("#Service3").val();
    if (ser1 == 0 && ser2 == 0 && ser3 == 0){
        $('.errormessage').html("Please select at least 1 service.");
        $('#Error').show();
        $(window).scrollTop(0);
        event.preventDefault();
        return false;
    }
    else if ((ser1 == ser2 && ser1 !=0 ) || (ser1 == ser3 && ser1 != 0) || (ser2 == ser3 && ser2 != 0)) {
        $('.errormessage').html("You can not re-select a service");
        $('#Error').show();
        $(window).scrollTop(0);
        event.preventDefault();
        return false;
    }
    $('#Error').hide();
    return true;   
}

$("#btninsurance").click(function () {
    return validationInsurance();
});



function validationInsurance() {

    debugger;
    $("#PolicyNumber").removeClass("validation-error");
    $(".dropify-wrapper").removeClass("validation-error");
    $("#StartDate").removeClass("validation-error");
    $("#EndDate").removeClass("validation-error");
    $("#PolicyNumber").removeClass("validation-error");
    $("#InsuranceAmount").removeClass("validation-error");
    $('#sucess').hide();
    $('#Error').hide();
    $('#sucesssave').hide();
    $('#errrorssave').hide();
    Fillvalueview();
    var value = false;
    //$("#Insurancefiles")[0].files.length > 0 && 
    if ($("#PolicyNumber").val() != '') {
        if ($("#StartDate").val() != "" && $("#StartDate").val().split(" ")[0].split("/")[2] > '1900') {
            var start = $("#StartDate").val();
            if ($("#EndDate").val() != "") {
                if ($("#RenewalDate").val() != "") {
                    if ($("#InsuranceAmount").val() != "" && $("#InsuranceAmount").val() != null && $("#InsuranceAmount").val() > 0) {
                        var valuel = true;
                        value = valuel;
                    }
                    else {
                        $('.errormessage').html('Please enter mandatory fields.');
                        $('#Error').show();
                        $(window).scrollTop(0);
                        return false;
                    }
                }
                else {
                    $('.errormessage').html('Please enter mandatory fields.');
                    $('#Error').show();
                    $(window).scrollTop(0);
                    return false;
                }
            }
            else {
                $('.errormessage').html('Please enter mandatory fields.');
                $('#Error').show();
                $(window).scrollTop(0);
                return false;
            }
            //value = false;
        }
        else {
            $('.errormessage').html('Please enter Valid Start Date');
            $('#Error').show();
            $(window).scrollTop(0);
            return false;
        }
        
    }
    else {
        //if ($("#PolicyNumber").val() == '')
        {
            $('.errormessage').html('Please enter mandatory fields.');
            $('#Error').show();
            $(window).scrollTop(0);
            return false;

        }
    }
    debugger;
    if (value == true) {
        var EndDate = new Date($("#EndDate").val());
        var StartDate = new Date($("#StartDate").val());
        var RenewalDate = new Date($("#RenewalDate").val());

        StartDate.setHours(0, 0, 0, 0)
        EndDate.setHours(0, 0, 0, 0)
        RenewalDate.setHours(0, 0, 0, 0)

        if ($("#PolicyNumber").val() == '') {

            $('.errormessage').html('Please enter mandatory fields.');
            $('#Error').show();
            $(window).scrollTop(0);
            return false;
        }
        else if ($("#InsuranceAmount").val() <= 0) {

            $('.errormessage').html('Please enter mandatory fields.');
            $('#Error').show();
            $(window).scrollTop(0);
            return false;
        }
        else if ($("#StartDate").val() == "") {

            $('.errormessage').html('Please enter mandatory fields.');
            $('#Error').show();
            $(window).scrollTop(0);
            return false;
        }
        else if ($("#EndDate").val() == "") {

            $('.errormessage').html('Please enter mandatory fields.');
            $('#Error').show();
            $(window).scrollTop(0);
            return false;
        }
        else if ($("#RenewalDate").val() == "") {

            $('.errormessage').html('Please enter mandatory fields.');
            $('#Error').show();
            $(window).scrollTop(0);
            return false;
        }

        else if (StartDate == "Invalid Date") {

            $(".errormessage").html("Invalid Date.");
            $('#Error').show();
            $(window).scrollTop(0);
            return false;

        }
        else if (EndDate == "Invalid Date") {

            $(".errormessage").html("Invalid Date.");
            $('#Error').show();
            $(window).scrollTop(0);
            return false;

        }
        else if (RenewalDate == "Invalid Date") {

            $(".errormessage").html("Invalid Date.");
            $('#Error').show();
            $(window).scrollTop(0);
            return false;

        }
        else if (EndDate < StartDate || RenewalDate < StartDate) {
            $('.errormessage').html('Start Date must be less then End Date and Renewal Date.');

            $('#Error').show();
            $("#Insurance_StartDate").val("");
            $(window).scrollTop(0);
            //event.preventDefault();
            return false;
        }

        if (EndDate < StartDate) {
            $('.errormessage').html('End Date must be greater than Start Date.');
            $('#Error').show();
            $("#Insurance_EndDate").val("");
            $(window).scrollTop(0);
            //event.preventDefault();
            return false;
        }

        if (RenewalDate <= StartDate) {
            $('.errormessage').html('Renewal Date must be greater than Start Date and less than or equal to End date.');
            $('#Error').show();
            $("#Insurance_RenewalDate").val("");
            $(window).scrollTop(0);
            //event.preventDefault();
            return false;
        }
        else if (RenewalDate > EndDate) {
            $('.errormessage').html('Renewal Date must be equal or less than End Date.');
            $('#Error').show();
            $("#Insurance_RenewalDate").val("");
            $(window).scrollTop(0);
            //event.preventDefault();
            return false;
        }
        //$('.errormessage').html('Please fill all fileds.');
        //$('#Error').show();

        event.preventDefault();
        return true;

    }
}


function fillserivces()
{
    debugger;
    $('#sucess').hide();
    $('#Error').hide();
    $('#sucesssave').hide();
    $('#errrorssave').hide();
    $("#Servicediv table tbody tr:not(:first)").remove();
    $("#Servicediv table tbody tr:eq(0)").show();
    var SelectedManager = $("#ServiceChoosen option:selected").map(function ()
    {
        var cln = $("#Servicediv table tbody tr:eq(0)").clone(true);
        $(".Servicespan", cln).html($(this).text());
        $("#Servicediv table").append(cln);
        event.preventDefault();
        return $(this).val();
    }).get().join(',');
    $('#ServiceTitle1').val(SelectedManager);
    $("#Servicediv table tbody tr:eq(0)").hide();
    var ser1 = $("#Service1");
    var ser2 = $("#Service2");
    var ser3 = $("#Service3");
    var arr = new Array(3);
    var count = 0;
    if (ser1.val() > 0) {
        arr[count] = ser1.children("option:selected")[0].text;
        count++;
    }
    if (ser2.val() > 0) {
        arr[count] = ser2.children("option:selected")[0].text;
        count++;
    }
    if (ser3.val() > 0) {
        arr[count] = ser3.children("option:selected")[0].text;
        count++;
    }
    $(".divRow:not(:first)").remove()
    $(".divRow:eq(0)").show();
    for (var i = 0; i < count; i++) {
        var clone = $(".divRow:eq(0)").clone(true);
        $("#serviceName", clone).html(arr[i]);
        $(".cln").append(clone);
    }
    $(".divRow:eq(0)").hide();

}

function validationRadius()
{
    $('#sucess').hide();
    $('#Error').hide();
    $('#sucesssave').hide();
    $('#errrorssave').hide();
    if ($('#Radius').val() === "0")
    {
        $('.errormessage').html('Please enter mandatory fields.');
        $('#Error').show();
        $(window).scrollTop(0);
        event.preventDefault();
        return false;
    }
   
}

//function clear()
//{

//    $('#ServiceAddress').val("");
//    $('#ServiceAddress').val("");
//    $('#serviceaddressc').val("");    
//    $('#CompanyName').val("");
//    $('#LegalName').val("");
//    $('#TaxID').val("");
//    $('#Address').val("");
//    $('#Address2').val("");
//    $('#City').val("");
//    $('#StateKey').val("");
//    $('#State').val("0");
//    $('#Zip').val("");
//    $('#Work').val("");
//    $('#Work2').val("");
//    $('#Fax').val("");
//    $('#CardNumber').val("");
//    $('#ValidTillMM').val("");
//    $('#ValidTillYY').val("");
//    $('#FullValidTill').val("");
//    $('#CVV').val("");
//    $('#Website').val("");
//    $('#Radius').val("0");
//    $('#vServiceAddress').html("");
    
//}
