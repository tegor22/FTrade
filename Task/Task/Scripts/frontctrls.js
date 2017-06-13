var mApp = angular.module("App", []);
mApp.controller('PreviewBaseCtrl', function ($scope, ServiceConnect, ServiceByDays) {
        $scope.sortdata = {
            model: null,
            availableOpt: [
                { id: 'Day', sort: 'SalesPerDays' },
                { id: 'Week', sort: 'SalesPerWeeks' },
                { id: 'Month', sort: 'SalesPerMonth' },
                { id: 'Year', sort: 'SalesPerYears' },
                { id: 'Quarter', sort: 'SalesPerQuarters' }
            ]
    };
        $scope.GetDataSort = function () {
            var FirstDate = $scope.FDate.getFullYear() + '-' + ($scope.FDate.getMonth() + 1) + '-' + $scope.FDate.getDate();
            var SecondDate = $scope.SDate.getFullYear() + '-' + ($scope.SDate.getMonth() + 1) + '-' + $scope.SDate.getDate();
            switch ($scope.sortdata.model)
            {
                case 'SalesPerDays':
                    ServiceConnect.get($scope.sortdata.model, FirstDate, SecondDate).then(function(response) {
                        var DataforDrow = ServiceByDays.PrepareData(response.data);
                        DrowScript(DataforDrow);
                    });
                    break;
                case 'SalesPerWeeks':
                    break;
                case 'SalesPerMonth':
                    break;
                case 'SalesPerYears':
                    break;
                case 'SalesPerQuarters':
                    break;
            }
        }
});
mApp.factory('ServiceConnect', function ($http) {
    return {
        get: function (ctrl, startdate, enddate) {
            return $http({
                method: 'GET',
                url: '/api/' + ctrl + '/' + startdate + '/' + enddate
            });
        }
    }
});
mApp.factory('ServiceByDays', function () {
    var LableDate = [];
    var Data1 = [];
    var Data2 = [];
    var DataArr = [];
    return {
        PrepareData: function (DataByDays)
        {
            DataByDays.forEach(function (item, i) {
                LableDate.push({ "label": item.Date.replace(/T00:00:00/, "") });
                Data1.push({ "value": String(item.Price) + "$" });
                Data2.push({ "value": String(item.CountPrice) });
            });
            DataArr.push(LableDate);
            DataArr.push(Data1);
            DataArr.push(Data2);
            return DataArr;
        }
    }
});
function DrowScript(DataArr) {

    var dataSource = {
        "chart": {
            "caption": "Harry's SuperMart",
            "subCaption": "Sales analysis of last year",
            "xAxisname": "Month",
            "yAxisName": "Amount (In USD)",
            "numberPrefix": "",
            "showBorder": "0",
            "showValues": "0",
            "paletteColors": "#0075c2,#1aaf5d,#f2c500",
            "bgColor": "#ffffff",
            "showCanvasBorder": "0",
            "canvasBgColor": "#ffffff",
            "captionFontSize": "14",
            "subcaptionFontSize": "14",
            "subcaptionFontBold": "0",
            "divlineColor": "#999999",
            "divLineIsDashed": "1",
            "divLineDashLen": "1",
            "divLineGapLen": "1",
            "showAlternateHGridColor": "0",
            "usePlotGradientColor": "0",
            "toolTipColor": "#ffffff",
            "toolTipBorderThickness": "0",
            "toolTipBgColor": "#000000",
            "toolTipBgAlpha": "80",
            "toolTipBorderRadius": "2",
            "toolTipPadding": "5",
            "legendBgColor": "#ffffff",
            "legendBorderAlpha": '0',
            "legendShadow": '0',
            "legendItemFontSize": '10',
            "legendItemFontColor": '#666666'
        },
        "categories": [
            {
                category: DataArr[0]
            }
        ],
            "dataset": [
                {
                    "seriesName": "Sales amount",
                    "showValues": "1",
                    data: DataArr[1]
                },
                {
                    "seriesName": "Number of Sales",
                    "renderAs": "line",
                    data : DataArr[2]
                }
            ]
    }

    console.log(dataSource);

    FusionCharts.ready(function () {
        
        var salesAnlysisChart = new FusionCharts({
            type: 'mscombi2d',
            renderAt: "chart-container",
            width: '800',
            height: '400',
            dataFormat: 'json',
            dataSource
        }).render();
        delete dataSource;
    });
}