///<reference path="../../app.ts"/>
var Greenways;
(function (Greenways) {
    var ChatroomsPageViewModel = (function () {
        function ChatroomsPageViewModel() {
            this.ResourceDictionary = { en: { title: "Chats" } };
        }
        ChatroomsPageViewModel.prototype.PageShow = function (params) {
        };

        ChatroomsPageViewModel.prototype.PageHide = function () {
        };
        return ChatroomsPageViewModel;
    })();

    Greenways.Infra.App.InitializeViewModel("#chatrooms", new ChatroomsPageViewModel());
})(Greenways || (Greenways = {}));
//# sourceMappingURL=Chatrooms.js.map
