module Greenways.Data {

    export interface IData {
        fromJs(jsData: any): void;
        toJs(): any;
    }

    export interface IDataStorage<T extends IData> {
        Insert(value: T, onReady: () => void): void;
        Select(key: any, onReady: (viewModel: T) => void): void;
        Update(viewModel: T, onReady: () => void): void;
        Delete(key: any, onReady: () => void): void;
    }

    export class DataStorageFactory {
        public static Create<T extends IData>(storeName: string, dataCreateInstance: { new (): T; }, onReady: () => void): IDataStorage<T> {
            return new IndexedDbDataStorage("App", 1.0, storeName, dataCreateInstance, onReady);
        }
    }

    export class Guid {
        private static s4(): string {
            return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
        }

        public static NewGuid(): string {
            return this.s4() + this.s4() + '-' + this.s4() + '-' + this.s4() + '-' +
                this.s4() + '-' + this.s4() + this.s4() + this.s4();
        }
    }

    class IndexedDbDataStorage<T extends IData> implements IDataStorage<T> {

        private dbName: string;
        private dbVersion: number;
        private storeName: string;
        private DataCreateInstance: new()=> T;

        constructor(dbName: string, dbVersion: number, storeName: string, dataCreateInstance: { new(): T; }, onReady: () => void) {
            var self = this;
            self.dbName = dbName;
            self.dbVersion = dbVersion;
            self.storeName = storeName;
            self.DataCreateInstance = dataCreateInstance;

            window.indexedDB.deleteDatabase(this.dbName);
            var request = window.indexedDB.open(this.dbName, this.dbVersion);
            request.onsuccess = ()=> {
                var db = request.result;
                db.close();
                onReady();
            };
            request.onupgradeneeded = e=> {
                var db = request.result;
                db.createObjectStore(storeName, { keyPath: "Id" });
            };
        }

        public Insert(data: T, onReady: () => void): void {
            this.databaseFunction((db)=> {
                var tx = db.transaction(this.storeName, "readwrite");
                var store = tx.objectStore(this.storeName);
                var jsData = data.toJs();
                store.put(jsData);
                db.close();
                onReady();
            });
        }

        public Select(key: any, onReady: (data: T) => void): void {
            this.databaseFunction((db) => {
                var tx = db.transaction(this.storeName, "readwrite");
                var store = tx.objectStore(this.storeName);
                var request = store.get(key);
                request.onsuccess = ()=> {
                    var jsData = request.result;
                    var data = new this.DataCreateInstance();
                    data.fromJs(jsData);
                    db.close();
                    onReady(data);
                };
            });
        }

        public Update(data: T, onReady: () => void): void {
            this.databaseFunction((db)=> {
                var tx = db.transaction(this.storeName, "readwrite");
                var store = tx.objectStore(this.storeName);
                var jsData = data.toJs();
                store.put(jsData);
                db.close();
                onReady();
            });
        }

        public Delete(key: any, onReady: () => void): void {
            this.databaseFunction((db)=> {
                var tx = db.transaction(this.storeName, "readwrite");
                var store = tx.objectStore(this.storeName);
                store.delete(key);
                db.close();
                onReady();
            });
        }

        private databaseFunction(dbFunction: (db: IDBDatabase) => void): void {
            var request = window.indexedDB.open(this.dbName, this.dbVersion);
            request.onsuccess = () => {
                var db = <IDBDatabase>request.result;
                dbFunction(db);
            };
        }
    }
} 