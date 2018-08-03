

function AddPermanent() {
    if (document.getElementById("input_permanent").checked) {
        document.getElementById("perm").style.display = "none";
    }
    else {
        document.getElementById("perm").style.display = "block";
    }
}
function AutoFillOnZip(id, city, state) {
    var zipcode = $("#"+id).val();
    var clientKey = 'js-BrheeaMVG4cbTLoWP70rRbdE4Fd5APv5qhyoCrKxqWt0WRmqdlZTv9rDpFOSRjES';
    var url = "https://www.zipcodeapi.com/rest/" + clientKey + "/info.json/" + zipcode + "/radians";
    var request = new XMLHttpRequest();
    request.open('POST', url);
    request.onreadystatechange = function (e) {
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200) {
            var result = request.responseText;
            if (result !== null || result !== "" || result !== undefined) {
                if (result.charAt(0) === '"' && result.charAt(result.length - 1) === '"') {
                    result = result.substr(1, result.length - 2);
                }
                PopulateOnZip(result,city,state);
            }
        }
    }
    request.send();
}
function PopulateOnZip(jsonString, city, state) {
    Json = JSON.parse(jsonString);
    $("#"+state).val(Json.state);
    $("#"+city).val(Json.city);
}

function formValidation() {
    debugger;
    var pass = true;
    var fields = ["#firstName", "#lastName", "#age", "input[name='gender']", "#houseNum", "#street", "#city",
        "#state", "#country", "#zipcode", "#areaCode", "#phone", "#email"];
    if (!$("#input_permanent").is(":checked")) {
        fields.push("#perm_address1");
        fields.push("#perm_address2");
        fields.push("#perm_city");
        fields.push("#perm_state");
        fields.push("#perm_country");
        fields.push("#perm_zipcode");
    }
    for (var i = 0; i < fields.length; i++)
    {
        if ($(fields[i]).val() == null || $(fields[i]).val() == "")
        {
            pass = false;
            $(fields[i]).addClass("is-invalid");
        }
    }
    $(".is-invalid").first().focus();
    if (pass) {
        convertJson();
    }
    return pass;
}


$(document).on('blur', '#firstName,#lastName', function () {
    if ($("#firstName").val() == $("#lastName").val() && $("#name-alert").val()== undefined) {
        $("#lastName").after("<div id='name-alert' class='alert alert-warning col-4'>First and Last name cannot be the same</div>")
    }
});

$(document).on('blur', '#age', function () {
    if (($("#age").val() > 100 || $("#age").val() < 15) && $("#age-alert").val() == undefined) {
        $("#age").after("<div id='age-alert' class='alert alert-warning col-2'>Age must between 15 and 100</div>")
    }
});

$(document).on('blur', '#perm_zipcode', function () {
    if ($("#perm_zipcode").val().length != 5 && $("#perm_zip-alert").val() == undefined) {
        $("#perm_zipcode").after("<div id='perm_zip-alert' class='alert alert-warning'>Zipcode is 5 digits</div>")
    }
});

$(document).on('blur', '#zipcode', function () {
    if ($("#zipcode").val().length != 5 && $("#zip-alert").val() == undefined) {
        $("#zipcode").after("<div id='zip-alert' class='alert alert-warning'>Zipcode is 5 digits</div>")
    }
});

$(document).on('blur', '#phone', function () {
    if ($("#phone").val().length != 10 && $("#phone-alert").val() == undefined) {
        $("#phone").after("<div id='phone-alert' class='alert alert-warning col-2'>Phone number is 10 digits</div>")
    }
});

function convertJson() {
    var fields = ["#firstName", "#lastName", "#houseNum", "#street", "#city",
        "#state", "#country","#zipcode","#countryCode","#areaCode", "#phone", "#ext"];
    var jsObjct = {}; 
    for (var field in fields)
    {
        jsObjct[fields[field]] = $(fields[field]).val();
    }
    var JSON = JSON.stringify(jsObjct);
    registerRequest(JSON);
}

function registerRequest(JSON) {
    apiurl = "http://cunyspsproject1linweiapi.azurewebsites.net/api";
    $.post(apiurl, JSON, function () { alert("Success");})
}