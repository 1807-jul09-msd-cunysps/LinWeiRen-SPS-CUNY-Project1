var product = [
    "iphoneX: 999.00",
    "Chips: 10.00",
    "Earphone: 100.00",
    "Case: 15.00",
    "gum: 10.00"
]

var newProduct="";
for (i = 0; i < product.length; i++) {
    if (i != product.length - 1) {
        newProduct += product[i].split(": ") + ",";
    }
    else {
        newProduct += product[i].split(": ");
    }
}
var myArray = newProduct.split(",");


var dictionary = {};

myArray.forEach(function (item, index) {
    if (index % 2 === 0) {
        dictionary[item] = Number(myArray[index + 1]);
    }
});

document.write("iphoneX: " + dictionary.iphoneX);

