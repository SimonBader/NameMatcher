angular.module('nameMatcher', [])
    .controller('nameMatcherCtrl', ['$scope', '$http', function ($scope, $http) {

        $scope.getCountries = function () {
            $http.get('/api/WS_NameMatcher/Countries')
                .success(function (data, status, headers, config) {
                    $scope.countries = data;
                });
        }

        $scope.onSelectionChanged = function (countryCodes) {
            $scope.similarCountryCodes = [];
            $scope.countryCode = countryCodes;
            getNamesByCountries($scope.countryCode);
        }

        $scope.filterChanged = function () {
            if ($scope.countryCode != null) {
                $scope.filteredBabyNames = filterData($scope.babyNames);
            }
            else {
                getNamesByNameFilter($scope.filter);
            }
        };

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

        function getNamesByCountries(countryCodes) {
            var params = { CountryCodes: countryCodes };
            getNames(params)
        }


        function getNamesByNameFilter(nameFilter) {
            var params = { NameFilter: nameFilter };
            getNames(params)
        }

        function getNames(params) {
            $scope.hideErrorMessage = true;
            $http.post('/api/WS_NameMatcher/Names', params)
            .success(function (data, status, headers, config) {
                $scope.babyNames = data;
                $scope.filteredBabyNames = filterData(data);
            })
            .error(function (data, status, headers, config) {
                $scope.babyNames = [];
                $scope.filteredBabyNames = [];
                $scope.errorMessage = data['ExceptionMessage'];
                $scope.hideErrorMessage = false;
            });
        }

        function filterData(babyNames) {
            return babyNames.filter(function (babyName) {
                return $scope.filter == null ||
                    babyName.Name.toLowerCase().indexOf($scope.filter.toLowerCase()) > -1;
            });
        }
    }]);