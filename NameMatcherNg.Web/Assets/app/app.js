var app = angular.module('app', [
    'ngRoute',
    'ngCookies',
    'home',
    'signIn',
    'register',
    'todoManager',
    'nameMatcher',
    'worldmap'
]);

// TODO: SB: remove directive again!!
app.directive('appDraggable', function () {
    return function (scope, element) {
        // this gives us the native JS object
        var el = element[0];

        el.draggable = true;

        el.addEventListener(
            'dragstart',
            function (e) {
                e.dataTransfer.effectAllowed = 'move';
                e.dataTransfer.setData('Text', this.id);
                this.classList.add('drag');
                return false;
            },
            false
        );

        el.addEventListener(
            'dragend',
            function (e) {
                this.classList.remove('drag');
                return false;
            },
            false
        );
    }
});
app.directive('appDroppable', function () {
    return {
        scope: {
            drop: '&' // parent
        },
        link: function (scope, element) {
            // again we need the native object
            var el = element[0];

            el.addEventListener(
                'dragover',
                function (e) {
                    e.dataTransfer.dropEffect = 'move';
                    // allows us to drop
                    if (e.preventDefault) e.preventDefault();
                    this.classList.add('over');
                    return false;
                },
                false
            );
            el.addEventListener(
                'dragenter',
                function (e) {
                    this.classList.add('over');
                    return false;
                },
                false
            );
            el.addEventListener(
                'dragleave',
                function (e) {
                    this.classList.remove('over');
                    return false;
                },
                false
            );
            el.addEventListener(
                'drop',
                function (e) {
                    // Stops some browsers from redirecting.
                    if (e.stopPropagation) e.stopPropagation();

                    this.classList.remove('over');

                    var item = document.getElementById(e.dataTransfer.getData('Text'));
                    this.appendChild(item);

                    // call the drop passed drop function
                    scope.$apply(function (scope) {
                        var fn = scope.drop();
                        if ('undefined' !== typeof fn) {
                            fn(item.id);
                        }
                    });

                    return false;
                },
                false
            );
        }
    }
});
app.directive('appWorldmap', function () {
    return {
        scope: {
            selectionChanged: '&onSelectionChanged',
            highlightedCountryCodes: '='
        },
        templateUrl: "Assets/app/templates/worldmap.html",
        controller: "worldmapCtrl",
        link: function (scope, element, attributes, controller) {
            var options = $.extend({}, controller.DEFAULTS, element.data(), typeof option == 'object' && option);
            controller.initialize(element, options)
        }
    }
});


app.config(['$provide', '$routeProvider', '$httpProvider', function ($provide, $routeProvider, $httpProvider) {
    
    //================================================
    // Ignore Template Request errors if a page that was requested was not found or unauthorized.  The GET operation could still show up in the browser debugger, but it shouldn't show a $compile:tpload error.
    //================================================
    $provide.decorator('$templateRequest', ['$delegate', function ($delegate) {
        var mySilentProvider = function (tpl, ignoreRequestError) {
            return $delegate(tpl, true);
        }
        return mySilentProvider;
    }]);

    //================================================
    // Add an interceptor for AJAX errors
    //================================================
    $httpProvider.interceptors.push(['$q', '$location', function ($q, $location) {
        return {            
            'responseError': function (response) {
                if (response.status === 401)
                    $location.url('/signin');
                return $q.reject(response);
            }
        };
    }]);

        
    //================================================
    // Routes
    //================================================
    $routeProvider.when('/home', {
        templateUrl: 'App/Home',
        controller: 'homeCtrl'
    });
    $routeProvider.when('/register', {
        templateUrl: 'App/Register',
        controller: 'registerCtrl'
    });
    $routeProvider.when('/signin/:message?', {
        templateUrl: 'App/SignIn',
        controller: 'signInCtrl'
    });
    $routeProvider.when('/todomanager', {
        templateUrl: 'App/TodoManager',
        controller: 'todoManagerCtrl'
    });
    $routeProvider.when('/namematcher', {
        templateUrl: 'App/NameMatcher',
        controller: 'nameMatcherCtrl'
    });
    
    $routeProvider.otherwise({
        redirectTo: '/home'
    });    
}]);

app.run(['$http', '$cookies', '$cookieStore', function ($http, $cookies, $cookieStore) {
    //If a token exists in the cookie, load it after the app is loaded, so that the application can maintain the authenticated state.
    $http.defaults.headers.common.Authorization = 'Bearer ' + $cookieStore.get('_Token');
    $http.defaults.headers.common.RefreshToken = $cookieStore.get('_RefreshToken');
}]);






//GLOBAL FUNCTIONS - pretty much a root/global controller.
//Get username on each page
//Get updated token on page change.
//Logout available on each page.
app.run(['$rootScope', '$http', '$cookies', '$cookieStore', function ($rootScope, $http, $cookies, $cookieStore) {

    $rootScope.logout = function () {
        
        $http.post('/api/Account/Logout')
            .success(function (data, status, headers, config) {
                $http.defaults.headers.common.Authorization = null;
                $http.defaults.headers.common.RefreshToken = null;
                $cookieStore.remove('_Token');
                $cookieStore.remove('_RefreshToken');
                $rootScope.username = '';
                $rootScope.loggedIn = false;
                window.location = '#/signin';
            });

    }

    $rootScope.$on('$locationChangeSuccess', function (event) {
        if ($http.defaults.headers.common.RefreshToken != null) {
            var params = "grant_type=refresh_token&refresh_token=" + $http.defaults.headers.common.RefreshToken;
            $http({
                url: '/Token',
                method: "POST",
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                data: params
            })
            .success(function (data, status, headers, config) {
                $http.defaults.headers.common.Authorization = "Bearer " + data.access_token;
                $http.defaults.headers.common.RefreshToken = data.refresh_token;

                $cookieStore.put('_Token', data.access_token);
                $cookieStore.put('_RefreshToken', data.refresh_token);

                $http.get('/api/WS_Account/GetCurrentUserName')
                    .success(function (data, status, headers, config) {
                        if (data != "null") {
                            $rootScope.username = data.replace(/["']{1}/gi, "");//Remove any quotes from the username before pushing it out.
                            $rootScope.loggedIn = true;
                        }
                        else
                            $rootScope.loggedIn = false;
                    });


            })
            .error(function (data, status, headers, config) {
                $rootScope.loggedIn = false;
            });
        }
    });
}]);

