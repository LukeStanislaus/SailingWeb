import * as $ from "jquery";
function loaddatad(string: string, i: string) {

    $(i).data(JSON.parse(string));
    //tbl.rows[i].cells[0].data();

}

async function returnPlace(username: string) {
    let result: any = await $.ajax({
        url: "/Folder/ReturnPlace",
        data: {
            boat: username
        },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val().toString()
        },


    });
    return result;

}
async function updatePlaces() {

    let tbl: HTMLTableElement = <HTMLTableElement>document.getElementById('table1'); // table reference
    for (let i = 1; i < tbl.rows.length; i++) {
        let username: string = document.getElementById((i - 1).toString()).getAttribute("data-string");
        let h = await returnPlace(username);
        let y = <HTMLTableElement>document.getElementById('table1');
        y.rows[i].deleteCell(8);
        let x = <HTMLTableElement>document.getElementById('table1');
        x.rows[i].insertCell(8);
        x.innerHTML = h.toString();
    }
    document.getElementById("finishbutton").style.display = "inline";




}
function finishrace() {
    window.location = ""

}
function removerace() {
    bootbox.confirm({
        size: "small",
        message: "Are you sure you want to delete this race? You will not be able to get it back!",
        callback: function (result) {
            if (result) {
                $.ajax({
                    url: "/Folder/RemoveRace",
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    success: function (data) {
                        document.getElementById("submit").submit();
                    }

                });

            }
        }
    })


}
function editLap(username, lapNo, time) {
    document.getElementById("dialog-form").title = "Edit time for ".concat(username.name).concat(" on lap ").concat(lapNo.toString());
    document.getElementById("password").value = time;
    document.getElementById("dialog-form").style.display = "inline";
    $("#dialog-form").dialog({
        autoOpen: false,
        buttons: {
            "Remove Lap": function () {
                $.ajax({
                    url: "/Folder/RemoveTime",
                    data: {
                        name: JSON.stringify(username), lapNumber: lapNo
                    },
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    success: function (data) {
                        document.getElementById("submit").submit();
                        updatePlaces();
                    }

                });
            },
            "Enter": function () {
                $.ajax({
                    url: "/Folder/UpdateTime",
                    data: {
                        name: JSON.stringify(username), lapNumber: lapNo, lapTime: document.getElementById("password").value
                    },
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    success: function (data) {
                        document.getElementById("submit").submit();
                        updatePlaces()
                    }

                });
            },
            Cancel: function () {
                $("#dialog-form").dialog("close");
            }
        },
        close: function () {
        }
    });
    $("#dialog-form").dialog("open");
    /*
        .dialog({
        autoOpen: true,
        height: 400,
        width: 350,
        modal: true,
        buttons: {
            "Create an account": function () {
                $.ajax({
                    url: "/Folder/UpdateTime",
                    data: {
                        name: JSON.stringify(username), lapNumber: lapNo, lapTime: time
                    },
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    success: function (data) {
                        document.getElementById("submit").submit();
                    }

                });
            },
            Cancel: function () {
                dialog.dialog("close");
            }
        },
        close: function () {
        }
    });
    */
}
function newlap(boatin, rowNumber) {
    var lapno;
    rowNum = parseInt(rowNumber);
    $.ajax({
        url: "/Folder/GetNextLap",
        data: { boat: JSON.stringify(boatin) },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (resulting) {
            lapno = JSON.parse(resulting);
            var lap = lapno + 1;
            $.ajax({
                url: "/Folder/NoOfLaps",
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()
                },
                success: function (data) {
                    ajax = 0;
                    ajax = JSON.parse(data);
                    $.ajax({
                        url: "/Folder/NewLap",
                        data: { boat: JSON.stringify(boatin), lapTime: new Date().toJSON(), lapNumber: lap },
                        headers: {
                            RequestVerificationToken:
                                $('input:hidden[name="__RequestVerificationToken"]').val()
                        },
                        success: function (data) {
                            var tbl = document.getElementById('table1'), // table reference
                                i;
                            console.log(data);
                            muchFurther = ajax - lap;
                            if (muchFurther == (-1)) {
                                for (i = 0; i < tbl.rows.length; i++) {
                                    if (i == 0) {
                                        x = tbl.rows[i].insertCell(10 + muchFurther);
                                        x.innerHTML = "Lap ".concat(ajax + 1);

                                    }
                                    else {
                                        x = tbl.rows[i].insertCell(10 + muchFurther);

                                    }
                                }
                                tbl.rows[rowNum + 1].deleteCell(10 + muchFurther);
                                x = tbl.rows[rowNum + 1].insertCell(10 + muchFurther);
                                $.ajax({
                                    url: "/Folder/GetLapTime",
                                    data: { boat: JSON.stringify(boatin), lapNumber: lap },
                                    headers: {
                                        RequestVerificationToken:
                                            $('input:hidden[name="__RequestVerificationToken"]').val()
                                    },
                                    success: function (resulting) {
                                        x.onclick = function () { editLap(boatin, lap, resulting) };
                                        x.innerHTML = resulting.toString();
                                    }
                                });
                            }
                            else {

                                tbl.rows[rowNum + 1].deleteCell(9 + muchFurther);
                                x = tbl.rows[rowNum + 1].insertCell(9 + muchFurther);
                                $.ajax({
                                    url: "/Folder/GetLapTime",
                                    data: { boat: JSON.stringify(boatin), lapNumber: lap },
                                    headers: {
                                        RequestVerificationToken:
                                            $('input:hidden[name="__RequestVerificationToken"]').val()
                                    },
                                    success: function (resulting) {
                                        x.onclick = function () { editLap(boatin, lap, resulting) };
                                        x.innerHTML = resulting.toString();
                                    }
                                });
                            }
                            //
                        }

                    });







                    //document.getElementById("submit").submit();
                }

            });

        }
    });
    updatePlaces();

}

function myTimer(resulting) {

    console.log("Repeating function invoked.");
    var d = moment().tz("America/Los_Angeles");
    x = d.valueOf();

    f = x - resulting;

    // Create a new JavaScript Date object based on the timestamp
    // multiplied by 1000 so that the argument is in milliseconds, not seconds.
    var date = new Date(f).toLocaleTimeString();

    //var date = new moment(f).toLocaleTimeString();
    // Hours part from the timestamp
    //var hours = date.getHours();
    // Minutes part from the timestamp
    //var minutes = "0" + date.getMinutes();
    // Seconds part from the timestamp
    //var seconds = "0" + date.getSeconds();


    //var milliseconds = "0" + date.getMilliseconds();
    // Will display time in 10:30:23 format

    //var formattedTime = hours + ':' + minutes.substr(-2) + ':' + seconds.substr(-2);
    new CountUpTimer(date, function (times, parameters) {
        document.getElementById("demo").innerHTML = "Elapsed Time: ".concat(times);

    });


}
//var time;
function onloader() {
    $.ajax({
        url: "/Folder/GetStartTime",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (resulting) {
            if (resulting != 0) {
                console.log("Set repeating function");
                myTimer(resulting);
            }
            else {
                document.getElementById("startracebutton").style.visibility = "visible";
                document.getElementById("startracebutton").style.display = "block";
            }


        }
    });

}
function startrace() {

    start = Date.now();
    $.ajax({
        url: "/Folder/StartRace",
        data: { datetime: start },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            document.getElementById("submit").submit();
        }

    });

}