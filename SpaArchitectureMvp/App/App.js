var app = angular.module('App', ['ui.bootstrap', 'ngAnimate', 'ui.router']);


app.directive('markdown', function () {
    var converter = new Showdown.converter();
    var link = function (scope, element, attrs, model) {
        var render = function () {
            var htmlText = converter.makeHtml(model.$modelValue);
            element.html(htmlText);
        };
        scope.$watch(attrs['ngModel'], render);
        render();
    };
    return {
        restrict: 'E',
        require: 'ngModel',
        link: link
    };
});



app.config(['$stateProvider','$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise("/");


    $stateProvider

       //////////
       // Home //
       //////////

       .state("welcome", {

           // Use a url of "/" to set a states as the "index".
           url: "/",

           // Example of an inline template string. By default, templates
           // will populate the ui-view within the parent state's template.
           // For top level states, like this one, the parent template is
           // the index.html file. So this template will be inserted into the
           // ui-view within index.html.

           controller : function($scope) {
               $scope.sampleMessage = "Kumusta";
               $scope.sampleMarkdown = "\
***Hello***\r\r\
- Soft\r\
- Ware\r\
- Mint";
           },

           templateUrl: "App/Welcome/Template.html"

       });


}]);