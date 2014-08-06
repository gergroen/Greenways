///<reference path="../../app.ts"/>

module Greenways {

    class ChatsPageViewModel implements IViewModel {
        ResourceDictionary: IDictionary<IDictionary<string>> = { en: { title: "Chats" } };
        Resources: IDictionary<string>;

        PageShow(params): void {

        }

        PageHide(): void {

        }
    }

    Greenways.Infra.App.InitializeViewModel("#chats", new ChatsPageViewModel());
} 