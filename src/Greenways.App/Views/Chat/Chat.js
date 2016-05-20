///<reference path="../../app.ts"/>
var Greenways;
(function (Greenways) {
    var ChatPageViewModel = (function () {
        function ChatPageViewModel() {
            this.ResourceDictionary = { en: { title: "Chat" } };
        }
        ChatPageViewModel.prototype.PageShow = function (params) {
        };
        ChatPageViewModel.prototype.PageHide = function () {
        };
        return ChatPageViewModel;
    }());
    Greenways.Infra.App.InitializeViewModel("#chat", new ChatPageViewModel());
})(Greenways || (Greenways = {}));
//# sourceMappingURL=Chat.js.map