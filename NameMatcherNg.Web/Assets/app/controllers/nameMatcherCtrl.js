angular.module('nameMatcher', [])
    .controller('nameMatcherCtrl', ['$scope', '$http', function ($scope, $http) {

        $scope.getCountries = function () {
            $http.get('/api/WS_NameMatcher/Countries')
                .success(function (data, status, headers, config) {
                    $scope.countries = data;
                });
        }

        $scope.getNames = function () {
            var params = {
                CountryIdOne: $scope.countryIdOne,
                CountryIdTwo: $scope.countryIdTwo
            };
            $http.post('/api/WS_NameMatcher/Names', params)
            .success(function (data, status, headers, config) {
                $scope.babyNames = data;
            })
            .error(function (data, status, headers, config) {
                if (angular.isArray(data))
                    $scope.errorMessages = data;
                else
                    $scope.errorMessages = new Array(data.replace(/["']{1}/gi, ""));

                $scope.showSuccessMessage = false;
                $scope.showErrorMessage = true;
            });
       } 

        $scope.onCountrySelectionChanged = function (countryCodeOne, countryCodeTwo){ 
            $scope.countryIdOne = countryId;

            if ($scope.countryIdTwo !== undefined) {
                $scope.getNames();
            }
        }

        $scope.setCountryTwo = function (countryId) {
            $scope.countryIdTwo = countryId;

            if ($scope.countryIdOne !== undefined) {
                $scope.getNames();
            }
        }
    }]);