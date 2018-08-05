
{
    var address = [];
    var phone = [];
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
            }
            PopulateOnZip(result, city, state);
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
    var pass = true;
    var fields = ["#firstName", "#lastName", "#age", "input[name='gender']", "#houseNum", "#street", "#address_city",
        "#address_state", "#zipcode", "#areaCode", "#number", "#email"];
    for (var i = 0; i < fields.length; i++)
    {
        if ($(fields[i]).val() == null || $(fields[i]).val() == "")
        {
            pass = false;
            $(fields[i]).addClass("is-invalid");
        }
        if ($("#firstName").val() == $("#lastName").val()) {
            pass = false;
        }
        if ($("#age").val() > 100 || $("#age").val() < 15) {
            pass = false;
        }
        if ($("#zipcode").val().length != 5) {
            pass = false;
        }
        if ($("#number").val().length != 10) {
            pass = false;
        }
    }
    if (pass) {
        addAddress();
        addPhone();
        createObject();
    }
    else {
        $(".is-invalid").first().focus();
    }
}

$(document).on('click', '#address_btn', function () {
    var pass = true;
    var fields = ["#houseNum", "#street", "#address_city",
        "#address_state", "#zipcode"];
    for (var i = 0; i < fields.length; i++) {
        if ($(fields[i]).val() == null || $(fields[i]).val() == "") {
            pass = false;
            $(fields[i]).addClass("is-invalid");
        }
    }
    if ($("#zipcode").val().length != 5) {
        pass = false;
    }
    if (pass) {
        addAddress();
        printAddress();
    }
    return false;
});

$(document).on('click', '#phone_btn', function () {
    var pass = true;
    var fields = ["#areaCode", "#number", "#email"];
    for (var i = 0; i < fields.length; i++) {
        if ($(fields[i]).val() == null || $(fields[i]).val() == "") {
            pass = false;
            $(fields[i]).addClass("is-invalid");
        }
    }
    if ($("#number").val().length != 10) {
        pass = false;
    }
    if (pass) {
        addPhone();
        printPhone();
    }
    return false;
})

$(document).on('blur', '#firstName, #lastName, #age, input[name="gender"], #houseNum, #street, #address_city, #address_state, #zipcode, #areaCode, #number, #email',
    function () {
        if ($(this).hasClass("is-invalid") && ($(this).val() != "" && $(this).val() != null)) {
            $(this).removeClass("is-invalid");
        }
    });


$(document).on('blur', '#firstName,#lastName', function () {
    if ($("#firstName").val() == $("#lastName").val() && $("#name-alert").val() == undefined) {
        $("#lastName").after("<div id='name-alert' class='alert alert-warning col-4 text-center'>First and Last name cannot be the same</div>")
    }
    else if ($("#name-alert").val() != undefined && $("#firstName").val() != $("#lastName").val()) {
        $("#name-alert").remove();
    }
});

$(document).on('blur', '#age', function () {
    if (($("#age").val() > 100 || $("#age").val() < 15) && $("#age-alert").val() == undefined) {
        $("#age").after("<div id='age-alert' class='alert alert-warning col-2 text-center'>Age must between 15 and 100</div>")
    }
    else if (($("#age").val() <= 100 || $("#age").val() >= 15) && $("#age-alert").val() != undefined) {
        $("#age-alert").remove();
    }
});

$(document).on('blur', '#zipcode', function () {
    if ($("#zipcode").val().length != 5 && $("#zip-alert").val() == undefined) {
        $("#zipcode").after("<div id='zip-alert' class='alert alert-warning col-3 text-center'>Zipcode is 5 digits</div>")
    }
    else if ($("#zipcode").val().length == 5 && $("#zip-alert").val() != undefined) {
        $("#zip-alert").remove();
    }
});

$(document).on('blur', '#number', function () {
    if ($("#number").val().length != 10 && $("#phone-alert").val() == undefined) {
        $("#number").after("<div id='phone-alert' class='alert alert-warning col-3 text-center'>Phone number is 10 digits</div>")
    }
    else if ($("#number").val().length == 10 && $("#phone-alert").val() != undefined) {
        $("#phone-alert").remove();
    }
});

function createObject() {
    debugger;
    var Person = {
        firstName: $("#firstName").val(),
        lastName: $("#lastName").val(),
        Person_address: address,
        Person_phone: phone
    }
    apiurl = "http://cunyspsproject1linweiapi.azurewebsites.net/api";
    $.post(apiurl, Person)
        .done(function () {
            if (!alert("Send successful!")) { window.location.reload(); };
        })
        .fail(function () {
            alert("Oops! Message fail to send, try again later")
        })
}

function sendContactMessage() {
    apiurl = "http://cunyspsproject1linweiapi.azurewebsites.net/api/ContactMessage";
    var fields = ["full_name", "email", "full_message"];
    var jsObjct = {};
    for (var field in fields) {
        jsObjct[fields[field]] = $("#" + fields[field]).val();
    }
    $.post(apiurl, jsObjct)
        .done(function () {
            if (!alert("Send successful!")) { window.location.reload();};
        })
        .fail(function () {
            alert("Oops! Message fail to send, try again later")
        })
}

function addAddress() {
    var addressFields = ["houseNum", "street", "address_city", "address_state", "address_country", "zipcode"];
    var obj = {};
    for (var field in addressFields) {
       obj[addressFields[field]] = $("#" + addressFields[field]).val();
    }
    address.push(obj);
}

function addPhone() {
    var phoneFields = ["countryCode", "areaCode", "number", "ext"];
    var obj = {};
    for (var field in phoneFields) {
        obj[phoneFields[field]] = $("#" + phoneFields[field]).val();
    }
    phone.push(obj);
}

function printAddress() {
    var addressFields = ["houseNum", "street", "address_city", "address_state", "zipcode", "input_permanent"];
    var obj = {};
    for (var field in addressFields) {
        obj[addressFields[field]] = $("#" + addressFields[field]).val();
        $("#" + addressFields[field]).val('');
    }
    var yes = (obj.input_permanent == "on") ? 'Yes' : 'No'
    var html = '<tr>' + '<th scope="row">' + obj.houseNum + ' ' + obj.street + ', ' + obj.address_city + ' ' + obj.address_state +
        ' ' + obj.zipcode +', '+ $("#address_country").val() + '</th>' + '<th>' + yes + '</th>' + '</tr>'
    $("#address_table tbody").append(html);
}

function printPhone() {
    var phoneFields = ["areaCode", "number", "ext", "email"];
    var obj = {};
    for (var field in phoneFields) {
        obj[phoneFields[field]] = $("#" + phoneFields[field]).val();
        $("#" + phoneFields[field]).val('');
    }
    var html = '<tr>' + '<th scope="row">' + "+" + $("#countryCode").val() + '-' + obj.number + ' ext:' + obj.ext +
        '</th>' + '<th>' + obj.email + '</th>' + '</tr>'
    $("#phone_table tbody").append(html);
}
