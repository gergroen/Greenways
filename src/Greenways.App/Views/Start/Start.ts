///<reference path="../../app.ts"/>

module Greenways {

    class StartPageViewModel implements  IViewModel {
        ResourceDictionary: IDictionary <IDictionary<string>> = { en: {
            title: "Greenways",
            chat: "Chats"
        } };
        Resources: IDictionary <string>;
        
        DateTime: KnockoutObservable<string> = ko.observable<string>("");

        PageShow(params): void {
            var self = this;
            var connection = $.hubConnection("http://localhost:8080");
            var testhub = connection.createHubProxy('TestHub');
            testhub.on('SendToClients', function(name) {
                jQuery.getJSON("http://localhost/api/test/now", function(data) {
                    self.DateTime(data);
                });
            });

            connection.start();
        }

        PageHide(): void {

        }
    }

    Greenways.Infra.App.InitializeViewModel("#start", new StartPageViewModel());
}