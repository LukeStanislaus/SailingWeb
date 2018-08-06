function ajax1() {
    $.ajax({
        url: "/Folder/GetAll",
        data: { name: document.getElementById("autocomplete").value },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        }
    }).done(function (resulting) {
        document.getElementById("select").options.length = 0
        resulting.forEach(function (arrayElem) {
            var option = document.createElement("option");
            var str = arrayElem.boatName;
            var str1 = str.concat(", ")
            option.text = str1.concat(arrayElem.boatNumber);
            option.value = arrayElem.boatName;
            document.getElementById("select").add(option);
        })
        alert("resulting.length");

        var option = document.createElement("option");
        option.text = "New baoat";
        option.value = "test";
        document.getElementById("select").add(option);

    });
}


document.getElementById("autocomplete").onblur = function () {
    ajax1();

}

document.getElementById("autocomplete").onkeyup = function () {
    ajax1();
}