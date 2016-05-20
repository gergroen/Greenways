///<reference path="../../app.ts"/>
var Greenways;
(function (Greenways) {
    var ChatEditPageViewModel = (function () {
        function ChatEditPageViewModel() {
            this.ResourceDictionary = { en: { title: "New chat" } };
        }
        ChatEditPageViewModel.prototype.PageShow = function (params) {
        };
        ChatEditPageViewModel.prototype.PageHide = function () {
        };
        return ChatEditPageViewModel;
    }());
    Greenways.Infra.App.InitializeViewModel("#chatedit", new ChatEditPageViewModel());
})(Greenways || (Greenways = {}));
//# sourceMappingURL=ChatEdit.js.map