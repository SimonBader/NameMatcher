﻿angular.module('worldmap', [])
    .controller('worldmapCtrl', ['$scope', '$http', function ($scope, $http) {
        this.initialize = function () {
            $scope.selectedCountryCodes = new FixedQueue(2);
            $http.get('/api/WS_NameMatcher/States')
            .success(function (data, status, headers, config) {
                $scope.states = data;
                $scope.hoveredState;
            });
        }

        $scope.click = function (countryCode) {
            updateSelectedCountries(countryCode);
        }

        $scope.mouseover = function (countryCode) {
            var hoveredState = $scope.states.filter(function (state) { return state.CountryCode === countryCode; }).shift();
            $scope.hoveredCountryCode = countryCode;
            $scope.hoveredX = hoveredState.Pin.split(',')[0];
            $scope.hoveredY = hoveredState.Pin.split(',')[1];
        }

        $scope.mouseleave = function (countryCode) {
            $scope.hoveredCountryCode = null;
            $scope.hoveredX = null;
            $scope.hoveredY = null;
        }

        $scope.styles = function (countryCode) {
            var isHovered = $scope.hoveredCountryCode === countryCode;
            var isSelected = containsArrayItem(countryCode, $scope.selectedCountryCodes);
            var isHighlighted = containsArrayItem(countryCode, $scope.highlightedCountryCodes);

            return {
                "worldmap-state": true,
                "worldmap-state-hovered": isHovered,
                "worldmap-state-selected": !isHovered & isSelected,
                "worldmap-state-highlighted": !isHovered & isHighlighted,
            };
        };

        function updateSelectedCountries(countryCode) {
            if (containsArrayItem(countryCode, $scope.selectedCountryCodes)) {
                $scope.selectedCountryCodes.remove(countryCode);
            }
            else {
                $scope.selectedCountryCodes.enqueue(countryCode);
            }

            $scope.selectionChanged({ countryCodes: $scope.selectedCountryCodes });
        }

        function containsArrayItem(value, array) {
            return $.inArray(value, array) !== -1;
        }

        function inflateState(pin, points) {
            var factor = 1.0;
            var pinX = parseFloat(pin.split(',')[0]);
            var pinY = parseFloat(pin.split(',')[1]);
            var pointsArray = points.split(',');
            var inflatedArray = [];

            for (i = 0; i < points.split(',').length; i++) {
                if (i % 0) {
                    var x = parseFloat(pointsArray[i]);
                    var diff =  x - pinX;
                    var inflatedX = pinX + (diff * factor);
                    inflatedArray.push(inflatedX);
                }
                else {
                    var y = parseFloat(pointsArray[i]);
                    var diff = y - pinY;
                    var inflatedY = pinY + (diff * factor);
                    inflatedArray.push(inflatedY);
                }
            }

            return inflatedArray.join(',');
        }
    }]);