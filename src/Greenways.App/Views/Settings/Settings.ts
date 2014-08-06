///<reference path="../../app.ts"/>

module Greenways {

    class SettingsPageViewModel implements IViewModel {
        ResourceDictionary: IDictionary<IDictionary<string>> = { en: { title: "Settings" } };
        Resources: IDictionary<string>;

        PageShow(params): void {

        }

        PageHide(): void {

        }
    }

    Greenways.Infra.App.InitializeViewModel("#settings", new SettingsPageViewModel());
} 