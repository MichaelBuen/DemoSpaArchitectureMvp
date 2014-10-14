

require.config({

    baseurl: '/',

    paths: {

        'angular': '/Scripts/angular',

        'daAngularNgResource': '/Scripts/angular-resource',
        'angular-ui-router': '/Scripts/angular-ui-router',
        'angular-couch-potato': '/Scripts/angular-couch-potato',
        'showdown' : '/Scripts/showdown',
        'views': '/views'

    },

    shim: {

        'angular': {
            exports: 'angular'
        },

        'angular-ui-router': {
            deps: ['angular']
        }

    }

});


/*
Can't do this:
'/App/app-init'

But can do this:
'/App/app-init.js'

And this:
'App/app-init'


*/

require(['/App/TheApp.js', 'angular', 'showdown', '/App/app-init.js'], function (app, angular) {


    angular.element(document).ready(function () {

        angular.bootstrap(document, [app['name'], function () {

            // for good measure, put ng-app on the html element
            // studiously avoiding jQuery because angularjs.org says we shouldn't
            // use it.  In real life, there are a ton of reasons to use it.
            // karma likes to have ng-app on the html element when using requirejs.
            angular.element(document).find('html').addClass('ng-app');

        }]);

    });

});