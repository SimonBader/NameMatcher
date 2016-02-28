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
            updateSelectedCountries(countryCode);
        }

        $scope.mouseover = function (countryCode) {
            $scope.hoveredCountryCode = countryCode;
        }

        $scope.mouseleave = function (countryCode) {
            $scope.hoveredCountryCode = null;
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
    }]);