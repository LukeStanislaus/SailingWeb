import * as $ from "jquery";
import "jquery";
import "jquery-ui";
//import "bootbox";
import "bootstrap";
import * as bootbox from "bootbox";
//import "moment";
setTimeout(function () {
    console.log("check check");
    bootbox.alert("hey");
}, 10000);
console.log("test");

function confirmcaller(text: string, func: Function) {
    return confirmer(text, func);
}
if (1 == 0 ) {
    function confirmer(text, func) {
        return true;
    }
}
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
/*
function finishrace() {
    window.location = "/Index";

}
*/
function removerace() {
    console.log("bootboxing");
    let x = confirmcaller("Are you sure you want to delete this race? You will not be able to get it back!", removeraceajax);

}
function removeraceajax(result: boolean) {
    if (result) {
        $.ajax({
            url: "/Folder/RemoveRace",
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val() as any
            },
            success: function (data) {
                location = location.href as any;

                console.log("after");
            }

        });
    }
}


function editLap(username: any, lapNo: Int16Array, time: Date) {
    document.getElementById("dialog-form").title = "Edit time for ".concat(username.name).concat(" on lap ").concat(lapNo.toString());
    let x = <HTMLInputElement>document.getElementById("password")
    x.value = time.toDateString();
    document.getElementById("dialog-form").style.display = "inline";
    let form = document.getElementById("dialog-form") as any;
    form.dialog({
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
                            $('input:hidden[name="__RequestVerificationToken"]').val().toString()
                    },
                    success: function (data) {
                        let submit = document.getElementById("submit") as any;
                        submit.submit();
                        updatePlaces();
                    }

                });
            },
            "Enter": function () {
                let timeresponse = document.getElementById("password") as HTMLInputElement;
                $.ajax({
                    url: "/Folder/UpdateTime",
                    data: {
                        name: JSON.stringify(username), lapNumber: lapNo, lapTime: timeresponse.value
                    },
                    headers: {
                        RequestVerificationToken:
                            $('input:hidden[name="__RequestVerificationToken"]').val().toString()
                    },
                    success: function (data) {
                        let submit = document.getElementById("submit") as any
                        submit.submit();
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
}


function newlap(boatin: any, rowNumber: string) {
    var lapno;
    let rowNum = parseInt(rowNumber);
    $.ajax({
        url: "/Folder/GetNextLap",
        data: { boat: JSON.stringify(boatin) },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val().toString()
        },
        success: function (resulting) {
            lapno = JSON.parse(resulting);
            var lap = lapno + 1;
            $.ajax({
                url: "/Folder/NoOfLaps",
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val().toString()
                },
                success: function (data) {
                    let ajax = JSON.parse(data);
                    $.ajax({
                        url: "/Folder/NewLap",
                        data: { boat: JSON.stringify(boatin), lapTime: new Date().toJSON(), lapNumber: lap },
                        headers: {
                            RequestVerificationToken:
                                $('input:hidden[name="__RequestVerificationToken"]').val().toString()
                        },
                        success: function (data) {
                            var tbl = document.getElementById('table1') as HTMLTableElement // table reference
                            let i;
                            console.log(data);
                            let muchFurther = ajax - lap;
                            if (muchFurther == (-1)) {
                                for (i = 0; i < tbl.rows.length; i++) {
                                    if (i == 0) {
                                        let x = tbl.rows[i].insertCell(10 + muchFurther);
                                        x.innerHTML = "Lap ".concat(ajax + 1);

                                    }
                                    else {
                                        let x = tbl.rows[i].insertCell(10 + muchFurther);

                                    }
                                }
                                tbl.rows[rowNum + 1].deleteCell(10 + muchFurther);
                                let x = tbl.rows[rowNum + 1].insertCell(10 + muchFurther);
                                $.ajax({
                                    url: "/Folder/GetLapTime",
                                    data: { boat: JSON.stringify(boatin), lapNumber: lap },
                                    headers: {
                                        RequestVerificationToken:
                                            $('input:hidden[name="__RequestVerificationToken"]').val().toString()
                                    },
                                    success: function (resulting) {
                                        x.onclick = function () { editLap(boatin, lap, resulting) };
                                        x.innerHTML = resulting.toString();
                                    }
                                });
                            }
                            else {

                                tbl.rows[rowNum + 1].deleteCell(9 + muchFurther);
                                let x = tbl.rows[rowNum + 1].insertCell(9 + muchFurther);
                                $.ajax({
                                    url: "/Folder/GetLapTime",
                                    data: { boat: JSON.stringify(boatin), lapNumber: lap },
                                    headers: {
                                        RequestVerificationToken:
                                            $('input:hidden[name="__RequestVerificationToken"]').val().toString()
                                    },
                                    success: function (resulting) {
                                        x.onclick = function () { editLap(boatin, lap, resulting) };
                                        x.innerHTML = resulting.toString();
                                    }
                                });
                            }
                        }
                    });
                }
            });
        }
    });
    updatePlaces();
}

function myTimer(resulting: number) {

    console.log("Repeating function invoked.");
    var d = new Date();
    let x = d.valueOf();

    let f = x - resulting;
    var date = new Date(f).toLocaleTimeString();


    //let cd = countdown(new Date(f), function (times: any) {
    //    document.getElementById("demo").innerHTML = "Elapsed Time: ".concat(times.toString());

    //});


}
//var time;

setTimeout(function () {

    document.getElementById("removerace").onclick = removerace;
    document.getElementById("startracebutton").onclick = startrace;
    console.log("success of the onclick");
}, 100)
function startrace() {

    let start = Date.now();
    $.ajax({
        url: "/Folder/StartRace",
        data: { datetime: start },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val() as any
        },
        success: function (data) {
            location = location.href as any;
        }

    });

}