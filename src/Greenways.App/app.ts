/// <reference path="Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="Scripts/typings/signalr/signalr.d.ts" />

interface SignalR {
    testHub: any;
}

class Greeter {
    element: HTMLElement;
    span: HTMLElement;
    timerToken: number;

    constructor(element: HTMLElement) {
        this.element = element;
        this.element.innerHTML += "The time is: ";
        this.span = document.createElement('span');
        this.element.appendChild(this.span);
        this.span.innerText = "";
    }

    start() {
        $(document).ready(function() {
            $.ajaxSetup({ cache: false });
        });

        var dataTimeSpan = this.span;

        $(function () {
            var connection = $.hubConnection("http://gerard-laptop:8080");
            var testhub = connection.createHubProxy('TestHub');


            testhub.on('SendToClients', function (name) {
                jQuery.getJSON("http://gerard-laptop/api/test/now", function (data) {
                    dataTimeSpan.innerHTML = data;
                });
            });

            connection.start().done(function () {
                alert("connected");
            }).fail(function () {
                    alert("failed connect");
                });
        });
    }

    stop() {
        //clearTimeout(this.timerToken);
    }

}

window.onload = () => {
    var el = document.getElementById('content');
    var greeter = new Greeter(el);
    greeter.start();
};