///<reference path="../Data/DataStorage.ts"/>

module Greenways.Models {

    export class ChatViewModel implements Greenways.Data.IData {

        Id: KnockoutObservable<string> = ko.observable<string>()
        Name: KnockoutObservable<string> = ko.observable<string>()

        constructor(jsData?: any) {
            if (jsData) {
                this.fromJs(jsData);
            } else {
                this.Id(Greenways.Data.Guid.NewGuid());
            }
        }

        fromJs(jsData: any): void {
            this.Id(jsData.Id);
            this.Name(jsData.Name);
        }

        toJs(): any {
            return {
                Id: this.Id(),
                Name: this.Name()
            };
        }

    }
} 