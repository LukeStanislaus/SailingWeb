import * as $ from "jquery";
//import "jquery";
//import "jquery-ui";
//import "bootbox";
//import "bootstrap";
//import * as bootbox from "bootbox";
//import "moment";
//import "countup-timer-js";
//import "jquery.countdown";


setTimeout(function () {
    console.log("check check");
}, 10000);
console.log("test");

function confirmcaller(text: string, func: Function) {
    return confirmer(text, func);
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
                $('input:hidden[name="__RequestVerificationToken"]').val()as any
        },


    });
    return result;

}




async function updatePlaces() {

    let tbl: HTMLTableElement = <HTMLTableElement>document.getElementById('table1'); // table reference
    for (let i = 1; i < tbl.rows.length; i++) {
        let username: string = document.getElementById((i - 1).toString()).getAttribute("data-string");
        let h = await $.ajax({
            url: "/Folder/ReturnPlace",
            data: {
                boat: username
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val() as any
            },


        });
        let y = <HTMLTableElement>document.getElementById('table1');
        y.rows[i].deleteCell(8);
        let z = y.rows[i].insertCell(8);
        z.innerHTML = h.toString();
    }
    try {
        document.getElementById("finishbutton").style.display = "inline";
    }
    catch{

    }



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
async function editLapCaller(username: any, lapNo: number, time: any) {
    editLap(username, lapNo, time);


}




function onloader() {
    console.log("hi");
    $.ajax({
        url: "/Folder/GetStartTime",
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val() as any
        },
        success: function (resulting) {
            if (resulting != 0) {
                console.log("Set repeating function");
                myTimer(resulting);
            }
            else {
                try {
                    document.getElementById("startracebutton").style.visibility = "visible";
                    document.getElementById("startracebutton").style.display = "block";
                }
                catch{
                    console.log("button doesnt exist")
                }
            }


        }
    });

}

async function newlap(boatin: any, rowNumber: string) {
    var lapno;
    let rowNum = parseInt(rowNumber);
    await $.ajax({
        url: "/Folder/GetNextLap",
        data: { boat: JSON.stringify(boatin) },
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val() as any
        },
        success: function (resulting) {
            lapno = JSON.parse(resulting);
            var lap = lapno;
            $.ajax({
                url: "/Folder/NoOfLaps",
                headers: {
                    RequestVerificationToken:
                        $('input:hidden[name="__RequestVerificationToken"]').val()as any
                },
                success: function (data) {
                    let ajax = JSON.parse(data) -1;
                    console.log(boatin);
                    $.ajax({
                        url: "/Folder/NewLap",
                        data: { boat: boatin, lapTime: new Date().toJSON(), lapNumber: lap },
                        headers: {
                            RequestVerificationToken:
                                $('input:hidden[name="__RequestVerificationToken"]').val()as any
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
                                        x.innerHTML = "Lap ".concat(ajax + 2);

                                    }
                                    else {
                                        let x = tbl.rows[i].insertCell(10 + muchFurther);

                                    }
                                }
                                tbl.rows[rowNum + 1].deleteCell(10 + muchFurther);
                                let x = tbl.rows[rowNum + 1].insertCell(10 + muchFurther);
                                $.ajax({
                                    url: "/Folder/GetLapTime",
                                    data: { boat: JSON.stringify(boatin), lapNumber: lap+1 },
                                    headers: {
                                        RequestVerificationToken:
                                            $('input:hidden[name="__RequestVerificationToken"]').val() as any
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
                                    data: { boat: JSON.stringify(boatin), lapNumber: lap+1 },
                                    headers: {
                                        RequestVerificationToken:
                                            $('input:hidden[name="__RequestVerificationToken"]').val()as any
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
    var x = d.valueOf();

    var f = x - resulting;

    // Create a new JavaScript Date object based on the timestamp
    // multiplied by 1000 so that the argument is in milliseconds, not seconds.
    var date = new Date(f).toLocaleTimeString();
    new CountUpTimer(date, function (times: any, parameters: any) {
        if (parameters.isNextDay) {
            document.getElementById("demo").innerHTML = "Elapsed Time: 1:".concat(times);
        }
        else {
            document.getElementById("demo").innerHTML = "Elapsed Time: ".concat(times);
        }
    });
}
//var time;

setTimeout(function () {
    try {
        document.getElementById("removerace").onclick = removerace;
        document.getElementById("startracebutton").onclick = startrace;
    }
    catch{ }

    var els = document.getElementsByClassName("btn");
    Array.prototype.forEach.call(els, function (item: Element) {
       
        item.addEventListener("click", function () {
            //console.log(item.getAttribute('data-item') + item.getAttribute('data-int'));
            newlap(item.getAttribute('data-item'),
                item.getAttribute('data-int'))
        })
    });
    var table = document.getElementById("table1") as HTMLTableElement;

    for (let i = 1; i < table.rows.length; i++) {
        for (let a = 9; a < table.rows[i].cells.length; a++) {
            let x = table.rows[i].cells[a];
            x.addEventListener("click", function () {
                editLap(x.getAttribute("data-0"), parseInt(x.getAttribute("data-1")), x.getAttribute("data-2"));
            }
            )
            
}
    }
    
    //document.getElementById("startracebutton").onclick = startrace;
    onloader();
    console.log("success of the onclick");
}, 1000)
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