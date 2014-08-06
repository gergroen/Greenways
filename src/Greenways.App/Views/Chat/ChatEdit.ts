///<reference path="../../app.ts"/>

module Greenways {

    class ChatEditPageViewModel implements IViewModel {
        ResourceDictionary: IDictionary<IDictionary<string>> = { en: { title: "New chat" } };
        Resources: IDictionary<string>;

        PageShow(params): void {

        }

        PageHide(): void {

        }
    }

    Greenways.Infra.App.InitializeViewModel("#chatedit", new ChatEditPageViewModel());
}  