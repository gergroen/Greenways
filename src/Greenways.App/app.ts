/// <reference path="Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="Scripts/typings/jquerymobile/jquerymobile.d.ts" />
/// <reference path="Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="Scripts/typings/knockout/knockout.d.ts" />

module Greenways {

    export interface IApp {
        Initialize(): void;
        NavigateTo(page: string, options?: ChangePageOptions): void;
        InitializeViewModel(pageId: string, viewModel: IViewModel): void;
    }

    interface IPage {
        PageId: string;
        ViewModel: IViewModel;
        PageInit(): void;

    }

    export interface IViewModel {
        ResourceDictionary: IDictionary<IDictionary<string>>;
        Resources: IDictionary<string>;
        PageShow(params): void;
        PageHide(): void;
    }

    export interface IDictionary<T> {
        [key: string]: T;
    }

    class Page implements IPage {

        constructor(pageId: string, viewModel: IViewModel) {
            this.PageId = pageId;;
            this.ViewModel = viewModel;
        }

        PageId: string;
        ViewModel: IViewModel;

        PageInit(): void {
            
        }
    }

    class PageEventType {
        static pagebeforecreate: string = "pagebeforecreate";
        static pageinit: string = "pageinit";
        static pageshow: string = "pageshow";
        static pagehide: string = "pagehide";
    }

    class AppEngine implements IApp {

        public Initialize(): void {
            this.LoadViews();
        }

        public LoadViews(): void {
            $("head").find("link[rel='jquerymobile-view']").each((index, link) => {
                var url = $(link).attr("href");
                var data = this.LoadHtml(url);
                this.CreatView(data);
            });
        }

        private CreatView(dataView: any): void {
            $("body").append(dataView);
        }

        private LoadHtml(url: string): string {
            var result = "";
            $.ajax(url, {
                async: false,
                cache: false,
                dataType: 'html',
                success: (data) => {
                    result = data;
                }
            });
            return result;
        }

        public NavigateTo(page: string, options?: ChangePageOptions): void {
            $.mobile.changePage(page, options);
        }

        public CurrentLanguage: string = "en";

        public InitializeViewModel(pageId: string, viewModel: IViewModel): void {
            var page = new Page(pageId, viewModel);

            $(document).delegate(page.PageId, PageEventType.pagebeforecreate, () => {
                page.ViewModel.Resources = page.ViewModel.ResourceDictionary[this.CurrentLanguage];
                ko.applyBindings(page.ViewModel, $(page.PageId)[0]);
            });
            $(document).delegate(page.PageId, PageEventType.pageinit, () => {
                page.PageInit();
            });
            $(document).delegate(page.PageId, PageEventType.pageshow, () => {
                var params = Utils.GetURLParameters();
                page.ViewModel.PageShow(params);
            });
            $(document).delegate(page.PageId, PageEventType.pagehide, () => {
                page.ViewModel.PageHide();
            });
        }
    }

    class Utils {

        static GetURLParameter(name): string {
            var data;
            if (location.hash) {
                data = location.hash;
            } else {
                data = location.href;
            }
            return decodeURIComponent(decodeURI(
                (RegExp(name + '=' + '(.+?)(&|$)').exec(data) || [, null])[1]
                ));
        }

        static GetURLParameters(): any {
            var params = {};
            var data;
            if (location.hash) {
                data = location.hash;
            } else {
                data = location.href;
            }
            var queryIndex = data.indexOf("?");
            if (queryIndex > -1) {
                var queryStr = data.substr(queryIndex + 1, data.length - queryIndex - 1);
                var values = queryStr.split('&');
                $.each(values, (index, item) => {
                    var param = item.split('=');
                    if (param.length == 2) {
                        params[param[0]] = param[1];
                    }
                });
            }
            return params;
        }
    }

    export class Infra {
        public static App: IApp = new AppEngine();
    }

    $(document).ready(() => {
        jQuery.ajaxSetup({ cache: false });
        Greenways.Infra.App.Initialize();
        Greenways.Infra.App.NavigateTo("#start");
    });
}