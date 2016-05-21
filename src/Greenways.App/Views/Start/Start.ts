///<reference path="../../app.ts"/>

module Greenways {

    class StartPageViewModel implements  IViewModel {
        ResourceDictionary: IDictionary <IDictionary<string>> = { en: {
            title: "Greenways",
            chat: "Chats"
        } };
        Resources: IDictionary <string>;
        
        DateTime: KnockoutObservable<string> = ko.observable<string>("");

        connection: SignalR.Hub.Connection;

        PageShow(params): void {
            var self = this;
            self.connection = $.hubConnection("http://localhost/green");
            var testhub = self.connection.createHubProxy('TestHub');
            testhub.on('SendToClients', function(name) {
                jQuery.getJSON("http://localhost/green/api/test/now", function(data) {
                    self.DateTime(new Date(data).toString());
                });
            });

            self.connection.start();
        }

        PageHide(): void {
            this.connection.stop();
        }
    }

    Greenways.Infra.App.InitializeViewModel("#start", new StartPageViewModel());
}