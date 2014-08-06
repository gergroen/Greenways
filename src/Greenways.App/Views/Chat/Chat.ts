///<reference path="../../app.ts"/>

module Greenways {

    class ChatPageViewModel implements IViewModel {
        ResourceDictionary: IDictionary<IDictionary<string>> = { en: { title: "Chat" } };
        Resources: IDictionary<string>;

        PageShow(params): void {

        }

        PageHide(): void {

        }
    }

    Greenways.Infra.App.InitializeViewModel("#chat", new ChatPageViewModel());
}   