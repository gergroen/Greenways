var Greenways;
(function (Greenways) {
    (function (Data) {
        var DataStorageFactory = (function () {
            function DataStorageFactory() {
            }
            DataStorageFactory.Create = function (storeName, dataCreateInstance, onReady) {
                return new IndexedDbDataStorage("App", 1.0, storeName, dataCreateInstance, onReady);
            };
            return DataStorageFactory;
        })();
        Data.DataStorageFactory = DataStorageFactory;

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
    })(Greenways.Data || (Greenways.Data = {}));
    var Data = Greenways.Data;
})(Greenways || (Greenways = {}));
//# sourceMappingURL=DataStorage.js.map
