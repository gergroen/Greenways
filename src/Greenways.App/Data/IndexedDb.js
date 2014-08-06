var Greenways;
(function (Greenways) {
    (function (Data) {
        var IndexedDbDataStorage = (function () {
            function IndexedDbDataStorage(dbName, dbVersion, storeName, dataCreateInstance, onReady) {
                var self = this;
                self.dbName = dbName;
                self.dbVersion = dbVersion;
                self.storeName = storeName;
                self.DataCreateInstance = dataCreateInstance;

                window.indexedDB.deleteDatabase(this.dbName);
                var request = window.indexedDB.open(this.dbName, this.dbVersion);
                request.onsuccess = function () {
                    var db = request.result;
                    db.close();
                    onReady();
                };
                request.onupgradeneeded = function (e) {
                    var db = request.result;
                    db.createObjectStore(storeName, { keyPath: "Id" });
                };
            }
            IndexedDbDataStorage.prototype.Insert = function (data, onReady) {
                var _this = this;
                this.databaseFunction(function (db) {
                    var tx = db.transaction(_this.storeName, "readwrite");
                    var store = tx.objectStore(_this.storeName);
                    var jsData = data.toJs();
                    store.put(jsData);
                    db.close();
                    onReady();
                });
            };

            IndexedDbDataStorage.prototype.Select = function (key, onReady) {
                var _this = this;
                this.databaseFunction(function (db) {
                    var tx = db.transaction(_this.storeName, "readwrite");
                    var store = tx.objectStore(_this.storeName);
                    var request = store.get(key);
                    request.onsuccess = function () {
                        var jsData = request.result;
                        var data = new _this.DataCreateInstance();
                        data.fromJs(jsData);
                        db.close();
                        onReady(data);
                    };
                });
            };

            IndexedDbDataStorage.prototype.Update = function (data, onReady) {
                var _this = this;
                this.databaseFunction(function (db) {
                    var tx = db.transaction(_this.storeName, "readwrite");
                    var store = tx.objectStore(_this.storeName);
                    var jsData = data.toJs();
                    store.put(jsData);
                    db.close();
                    onReady();
                });
            };

            IndexedDbDataStorage.prototype.Delete = function (key, onReady) {
                var _this = this;
                this.databaseFunction(function (db) {
                    var tx = db.transaction(_this.storeName, "readwrite");
                    var store = tx.objectStore(_this.storeName);
                    store.delete(key);
                    db.close();
                    onReady();
                });
            };

            IndexedDbDataStorage.prototype.databaseFunction = function (dbFunction) {
                var request = window.indexedDB.open(this.dbName, this.dbVersion);
                request.onsuccess = function () {
                    var db = request.result;
                    dbFunction(db);
                };
            };
            return IndexedDbDataStorage;
        })();
        Data.IndexedDbDataStorage = IndexedDbDataStorage;

        var Guid = (function () {
            function Guid() {
            }
            Guid.s4 = function () {
                return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
            };

            Guid.NewGuid = function () {
                return this.s4() + this.s4() + '-' + this.s4() + '-' + this.s4() + '-' + this.s4() + '-' + this.s4() + this.s4() + this.s4();
            };
            return Guid;
        })();
        Data.Guid = Guid;

        var ChatViewModel = (function () {
            function ChatViewModel(jsData) {
                this.Id = ko.observable();
                this.Name = ko.observable();
                if (jsData) {
                    this.fromJs(jsData);
                } else {
                    this.Id(Guid.NewGuid());
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
        })();
        Data.ChatViewModel = ChatViewModel;

        $(document).ready(function () {
            var indexedDb = new IndexedDbDataStorage("App", 1.0, "Chat", ChatViewModel, function () {
                var chat = new ChatViewModel();
                var id = chat.Id();
                chat.Name("Test");
                alert(chat.Name());
                indexedDb.Insert(chat, function () {
                    indexedDb.Select(id, function (chat2) {
                        var id2 = chat2.Id();
                        var name2 = chat2.Name();
                        alert(name2);
                        chat2.Name("test3");
                        indexedDb.Update(chat2, function () {
                            indexedDb.Select(id, function (chat3) {
                                var id3 = chat3.Id();
                                var name3 = chat3.Name();
                                alert(name3);
                                indexedDb.Delete(id, function () {
                                    alert("Removed");
                                });
                            });
                        });
                    });
                });
            });
        });
    })(Greenways.Data || (Greenways.Data = {}));
    var Data = Greenways.Data;
})(Greenways || (Greenways = {}));
//# sourceMappingURL=IndexedDb.js.map
