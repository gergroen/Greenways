///<reference path="../Data/DataStorage.ts"/>
var Greenways;
(function (Greenways) {
    var Models;
    (function (Models) {
        var ChatViewModel = (function () {
            function ChatViewModel(jsData) {
                this.Id = ko.observable();
                this.Name = ko.observable();
                if (jsData) {
                    this.fromJs(jsData);
                }
                else {
                    this.Id(Greenways.Data.Guid.NewGuid());
                }
            }
            ChatViewModel.prototype.fromJs = function (jsData) {
                this.Id(jsData.Id);
                this.Name(jsData.Name);
            };
            ChatViewModel.prototype.toJs = function () {
                return {
                    Id: this.Id(),
                    Name: this.Name()
                };
            };
            return ChatViewModel;
        }());
        Models.ChatViewModel = ChatViewModel;
    })(Models = Greenways.Models || (Greenways.Models = {}));
})(Greenways || (Greenways = {}));
//# sourceMappingURL=ChatViewModel.js.map