/// <reference path="Scripts/typings/jquery/jquery.d.ts" />
/// <reference path="Scripts/typings/jquerymobile/jquerymobile.d.ts" />
/// <reference path="Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="Scripts/typings/knockout/knockout.d.ts" />
var Greenways;
(function (Greenways) {
    var Page = (function () {
        function Page(pageId, viewModel) {
            this.PageId = pageId;
            ;
            this.ViewModel = viewModel;
        }
        Page.prototype.PageInit = function () {
        };
        return Page;
    })();

    var PageEventType = (function () {
        function PageEventType() {
        }
        PageEventType.pagebeforecreate = "pagebeforecreate";
        PageEventType.pageinit = "pageinit";
        PageEventType.pageshow = "pageshow";
        PageEventType.pagehide = "pagehide";
        return PageEventType;
    })();

    var AppEngine = (function () {
        function AppEngine() {
            this.CurrentLanguage = "en";
        }
        AppEngine.prototype.Initialize = function () {
            this.LoadViews();
        };

        AppEngine.prototype.LoadViews = function () {
            var _this = this;
            $("head").find("link[rel='jquerymobile-view']").each(function (index, link) {
                var url = $(link).attr("href");
                var data = _this.LoadHtml(url);
                _this.CreatView(data);
            });
        };

        AppEngine.prototype.CreatView = function (dataView) {
            $("body").append(dataView);
        };

        AppEngine.prototype.LoadHtml = function (url) {
            var result = "";
            $.ajax(url, {
                async: false,
                cache: false,
                dataType: 'html',
                success: function (data) {
                    result = data;
                }
            });
            return result;
        };

        AppEngine.prototype.NavigateTo = function (page, options) {
            $.mobile.changePage(page, options);
        };

        AppEngine.prototype.InitializeViewModel = function (pageId, viewModel) {
            var _this = this;
            var page = new Page(pageId, viewModel);

            $(document).delegate(page.PageId, PageEventType.pagebeforecreate, function () {
                page.ViewModel.Resources = page.ViewModel.ResourceDictionary[_this.CurrentLanguage];
                ko.applyBindings(page.ViewModel, $(page.PageId)[0]);
            });
            $(document).delegate(page.PageId, PageEventType.pageinit, function () {
                page.PageInit();
            });
            $(document).delegate(page.PageId, PageEventType.pageshow, function () {
                var params = Utils.GetURLParameters();
                page.ViewModel.PageShow(params);
            });
            $(document).delegate(page.PageId, PageEventType.pagehide, function () {
                page.ViewModel.PageHide();
            });
        };
        return AppEngine;
    })();

    var Utils = (function () {
        function Utils() {
        }
        Utils.GetURLParameter = function (name) {
            var data;
            if (location.hash) {
                data = location.hash;
            } else {
                data = location.href;
            }
            return decodeURIComponent(decodeURI((RegExp(name + '=' + '(.+?)(&|$)').exec(data) || [, null])[1]));
        };

        Utils.GetURLParameters = function () {
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
                $.each(values, function (index, item) {
                    var param = item.split('=');
                    if (param.length == 2) {
                        params[param[0]] = param[1];
                    }
                });
            }
            return params;
        };
        return Utils;
    })();

    var Infra = (function () {
        function Infra() {
        }
        Infra.App = new AppEngine();
        return Infra;
    })();
    Greenways.Infra = Infra;

    $(document).ready(function () {
        jQuery.ajaxSetup({ cache: false });
        Greenways.Infra.App.Initialize();
        Greenways.Infra.App.NavigateTo("#start");
    });
})(Greenways || (Greenways = {}));
//# sourceMappingURL=app.js.map
