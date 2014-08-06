///<reference path="../../app.ts"/>
var Greenways;
(function (Greenways) {
    var StartPageViewModel = (function () {
        function StartPageViewModel() {
            this.ResourceDictionary = { en: {
                    title: "Greenways",
                    chat: "Chats"
                } };
            this.DateTime = ko.observable("");
        }
        StartPageViewModel.prototype.PageShow = function (params) {
            var self = this;
            var connection = $.hubConnection("http://localhost:8080");
            var testhub = connection.createHubProxy('TestHub');
            testhub.on('SendToClients', function (name) {
                jQuery.getJSON("http://localhost/api/test/now", function (data) {
                    self.DateTime(data);
                });
            });

            connection.start();
        };

        StartPageViewModel.prototype.PageHide = function () {
        };
        return StartPageViewModel;
    })();

    Greenways.Infra.App.InitializeViewModel("#start", new StartPageViewModel());
})(Greenways || (Greenways = {}));
//# sourceMappingURL=Start.js.map
