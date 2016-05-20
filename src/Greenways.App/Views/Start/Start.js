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
            self.connection = $.hubConnection("http://localhost/green");
            var testhub = self.connection.createHubProxy('TestHub');
            testhub.on('SendToClients', function (name) {
                jQuery.getJSON("http://localhost/green/api/test/now", function (data) {
                    self.DateTime(new Date(data).toString());
                });
            });
            self.connection.start();
        };
        StartPageViewModel.prototype.PageHide = function () {
            this.connection.stop();
        };
        return StartPageViewModel;
    }());
    Greenways.Infra.App.InitializeViewModel("#start", new StartPageViewModel());
})(Greenways || (Greenways = {}));
//# sourceMappingURL=Start.js.map