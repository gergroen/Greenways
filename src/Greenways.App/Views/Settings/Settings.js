///<reference path="../../app.ts"/>
var Greenways;
(function (Greenways) {
    var SettingsPageViewModel = (function () {
        function SettingsPageViewModel() {
            this.ResourceDictionary = { en: { title: "Settings" } };
        }
        SettingsPageViewModel.prototype.PageShow = function (params) {
        };
        SettingsPageViewModel.prototype.PageHide = function () {
        };
        return SettingsPageViewModel;
    }());
    Greenways.Infra.App.InitializeViewModel("#settings", new SettingsPageViewModel());
})(Greenways || (Greenways = {}));
//# sourceMappingURL=Settings.js.map