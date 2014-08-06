///<reference path="../../app.ts"/>
var Greenways;
(function (Greenways) {
    var ChatsPageViewModel = (function () {
        function ChatsPageViewModel() {
            this.ResourceDictionary = { en: { title: "Chats" } };
        }
        ChatsPageViewModel.prototype.PageShow = function (params) {
        };

        ChatsPageViewModel.prototype.PageHide = function () {
        };
        return ChatsPageViewModel;
    })();

    Greenways.Infra.App.InitializeViewModel("#chats", new ChatsPageViewModel());
})(Greenways || (Greenways = {}));
//# sourceMappingURL=Chats.js.map
