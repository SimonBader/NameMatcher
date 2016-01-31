angular.module('worldmap', [])
    .controller('worldmapCtrl', ['$scope', '$http', function ($scope, $http) {
        this.initialize = function() {

        }

        var findValue = function (key, value) {
            var referObj = [];
            var obj = WorldMapGenerator.timeZoneValue.filter(function (object) {
                if (object[key] === value) {
                    referObj.push($.extend(true, {}, object));
                    return object;
                }
            });
            for (var i = 0; i < referObj.length; i++) {
                delete referObj[i].points;
                delete referObj[i].pin;
            }
            return referObj;
        }
    }]);