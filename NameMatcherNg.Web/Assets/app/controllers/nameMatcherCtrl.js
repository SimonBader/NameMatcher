angular.module('nameMatcher', [])
    .controller('nameMatcherCtrl', ['$scope', '$http', function ($scope, $http) {

        $scope.getCountries = function () {
            $http.get('/api/WS_NameMatcher/Countries')
                .success(function (data, status, headers, config) {
                    $scope.countries = data;
                });
        }

        $scope.onSelectionChanged = function (countryCodes) {
            $scope.hideErrorMessage = true;
            var params = {
                CountryCodes: countryCodes
            };
            $http.post('/api/WS_NameMatcher/Names', params)
            .success(function (data, status, headers, config) {
                $scope.babyNames = data;
                $scope.filteredBabyNames = $scope.filterData(data);
            })
            .error(function (data, status, headers, config) {
                $scope.babyNames = [];
                $scope.filteredBabyNames = [];
                $scope.errorMessage = data['ExceptionMessage'];
                $scope.hideErrorMessage = false;
            });
        }

        $scope.filterChanged = function () {
            $scope.filteredBabyNames = $scope.filterData($scope.babyNames);
        };

        $scope.filterData = function (babyNames) {
            return babyNames.filter(function (babyName) {
                return $scope.filter == null ||
                    babyName.Name.toLowerCase().indexOf($scope.filter) > -1;
            });
        }

        $scope.getSimilarCountries = function (babyName) {
            $scope.hideErrorMessage = true;
            var params = {
                BabyNameFilter: babyName.Name
            };
            $http.post('/api/WS_NameMatcher/Countries', params)
            .success(function (data, status, headers, config) {
                $scope.similarCountryCodes = data.map(function (x) {
                    return x.CountryCode;
                });
            })
            .error(function (data, status, headers, config) {
                $scope.babyNames = [];
                $scope.filteredBabyNames = [];
                $scope.errorMessage = data['ExceptionMessage'];
                $scope.hideErrorMessage = false;
            });
        }
    }]);