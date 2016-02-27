angular.module('worldmap', [])
    .controller('worldmapCtrl', ['$scope', '$http', function ($scope, $http) {
        this.initialize = function () {
            $scope.selectedCountryCodes = new FixedQueue(2);
            $http.get('/api/WS_NameMatcher/States')
            .success(function (data, status, headers, config) {
                $scope.states = data;
            });
        }

        $scope.click = function (countryCode) {
            $scope.selectedCountryCodes.unshift(countryCode);
            $scope.selectionChanged({ countryCodes: $scope.selectedCountryCodes })
        }

        $scope.mouseover = function (countryCode) {
            $scope.hoveredCountryCode = countryCode;
        }

        $scope.mouseleave = function (countryCode) {
            $scope.hoveredCountryCode = null;
        }

        $scope.styles = function (countryCode) {
            return {
                "worldmap-state": true,
                "worldmap-state-hovered": $scope.hoveredCountryCode === countryCode,
                "worldmap-state-selected": $.inArray(countryCode, $scope.selectedCountryCodes) >= 0,
                "worldmap-state-highlighted": $.inArray(countryCode, $scope.highlightedCountryCodes) >= 0
            };
        };
    }]);