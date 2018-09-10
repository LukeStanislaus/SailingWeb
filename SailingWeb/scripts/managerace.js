"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var $ = require("jquery");
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
function confirmcaller(text, func) {
    return confirmer(text, func);
}
function loaddatad(string, i) {
    $(i).data(JSON.parse(string));
    //tbl.rows[i].cells[0].data();
}
function returnPlace(username) {
    return __awaiter(this, void 0, void 0, function () {
        var result;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, $.ajax({
                        url: "/Folder/ReturnPlace",
                        data: {
                            boat: username
                        },
                        headers: {
                            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                        },
                    })];
                case 1:
                    result = _a.sent();
                    return [2 /*return*/, result];
            }
        });
    });
}
function updatePlaces() {
    return __awaiter(this, void 0, void 0, function () {
        var tbl, i, username, h, y, z;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    tbl = document.getElementById('table1');
                    i = 1;
                    _a.label = 1;
                case 1:
                    if (!(i < tbl.rows.length)) return [3 /*break*/, 4];
                    username = document.getElementById((i - 1).toString()).getAttribute("data-string");
                    return [4 /*yield*/, $.ajax({
                            url: "/Folder/ReturnPlace",
                            data: {
                                boat: username
                            },
                            headers: {
                                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                            },
                        })];
                case 2:
                    h = _a.sent();
                    y = document.getElementById('table1');
                    y.rows[i].deleteCell(8);
                    z = y.rows[i].insertCell(8);
                    z.innerHTML = h.toString();
                    _a.label = 3;
                case 3:
                    i++;
                    return [3 /*break*/, 1];
                case 4:
                    try {
                        document.getElementById("finishbutton").style.display = "inline";
                    }
                    catch (_b) {
                    }
                    return [2 /*return*/];
            }
        });
    });
}
/*
function finishrace() {
    window.location = "/Index";

}
*/
function removerace() {
    console.log("bootboxing");
    var x = confirmcaller("Are you sure you want to stop managing this race?", removeraceajax);
}
function removeraceajax(result) {
    if (result) {
        $.ajax({
            url: "/Folder/RemoveRace",
            headers: {
                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (data) {
                location = location.href;
                console.log("after");
            }
        });
    }
}
function editLapCaller(username, lapNo, time) {
    return __awaiter(this, void 0, void 0, function () {
        return __generator(this, function (_a) {
            editLap(username, lapNo, time);
            return [2 /*return*/];
        });
    });
}
function onloader() {
    console.log("hi");
    $.ajax({
        url: "/Folder/GetStartTime",
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
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
                catch (_a) {
                    console.log("button doesnt exist");
                }
            }
        }
    });
}
function newlap(boatin, rowNumber) {
    return __awaiter(this, void 0, void 0, function () {
        var lapno, rowNum;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0:
                    rowNum = parseInt(rowNumber);
                    return [4 /*yield*/, $.ajax({
                            url: "/Folder/NewLap",
                            data: { boat: boatin, lapTime: new Date().toJSON() },
                            headers: {
                                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                            },
                            success: function (data) {
                                $.ajax({
                                    url: "/Folder/GetNextLap",
                                    data: { boat: JSON.stringify(boatin) },
                                    headers: {
                                        RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                                    },
                                    success: function (resulting) {
                                        lapno = JSON.parse(resulting);
                                        var lap = lapno;
                                        $.ajax({
                                            url: "/Folder/NoOfLaps",
                                            headers: {
                                                RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                                            },
                                            success: function (data) {
                                                var ajax = JSON.parse(data);
                                                console.log(boatin);
                                                var tbl = document.getElementById('table1'); // table reference
                                                var i;
                                                console.log(data);
                                                var muchFurther = ajax - lap;
                                                if (muchFurther == (0)) {
                                                    for (i = 0; i < tbl.rows.length; i++) {
                                                        if (i == 0) {
                                                            var x_1 = tbl.rows[i].insertCell(9);
                                                            x_1.innerHTML = "<b>Lap ".concat((ajax).toString()).concat("</b>");
                                                        }
                                                        else {
                                                            var x_2 = tbl.rows[i].insertCell(9);
                                                        }
                                                    }
                                                    tbl.rows[rowNum + 1].deleteCell(9 + muchFurther);
                                                    var x_3 = tbl.rows[rowNum + 1].insertCell(9 + muchFurther);
                                                    $.ajax({
                                                        url: "/Folder/GetLapTime",
                                                        data: { boat: JSON.stringify(boatin), lapNumber: lap },
                                                        headers: {
                                                            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                                                        },
                                                        success: function (resulting) {
                                                            x_3.onclick = function () { editLap(boatin, lap, resulting); };
                                                            x_3.innerHTML = resulting.toString();
                                                        }
                                                    });
                                                }
                                                else {
                                                    tbl.rows[rowNum + 1].deleteCell(9 + muchFurther);
                                                    var x_4 = tbl.rows[rowNum + 1].insertCell(9 + muchFurther);
                                                    $.ajax({
                                                        url: "/Folder/GetLapTime",
                                                        data: {
                                                            boat: JSON.stringify(boatin), lapNumber: lap
                                                        },
                                                        headers: {
                                                            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
                                                        },
                                                        success: function (resulting) {
                                                            x_4.onclick = function () { editLap(boatin, lap, resulting); };
                                                            x_4.innerHTML = resulting.toString();
                                                        }
                                                    });
                                                }
                                            }
                                        });
                                    }
                                });
                            }
                        })];
                case 1:
                    _a.sent();
                    updatePlaces();
                    return [2 /*return*/];
            }
        });
    });
}
function myTimer(resulting) {
    console.log("Repeating function invoked.");
    var d = new Date();
    var x = d.valueOf();
    var f = x - resulting;
    // Create a new JavaScript Date object based on the timestamp
    // multiplied by 1000 so that the argument is in milliseconds, not seconds.
    var date = new Date(f).toLocaleTimeString();
    new CountUpTimer(date, function (times, parameters) {
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
    catch (_a) { }
    var els = document.getElementsByClassName("btn");
    Array.prototype.forEach.call(els, function (item) {
        item.addEventListener("click", function () {
            //console.log(item.getAttribute('data-item') + item.getAttribute('data-int'));
            newlap(item.getAttribute('data-item'), item.getAttribute('data-int'));
        });
    });
    var table = document.getElementById("table1");
    for (var i = 1; i < table.rows.length; i++) {
        var _loop_1 = function (a) {
            var x = table.rows[i].cells[a];
            x.addEventListener("click", function () {
                editLap(x.getAttribute("data-0"), parseInt(x.getAttribute("data-1")), x.getAttribute("data-2"));
            });
        };
        for (var a = 9; a < table.rows[i].cells.length; a++) {
            _loop_1(a);
        }
    }
    //document.getElementById("startracebutton").onclick = startrace;
    onloader();
    console.log("success of the onclick");
}, 1000);
function startrace() {
    var start = Date.now();
    $.ajax({
        url: "/Folder/StartRace",
        data: { datetime: start },
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        success: function (data) {
            location = location.href;
        }
    });
}
//# sourceMappingURL=managerace.js.map