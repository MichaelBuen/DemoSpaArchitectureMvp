
define(

  ['angular', 'angular-couch-potato', 'angular-ui-router', 'daAngularNgResource'],
  function (angular, couchPotato, uiRouter, ngResource) {

      
      var app = angular.module('app', ['scs.couch-potato', 'ui.router', 'ngResource']);

      
      // have Couch Potato set up the registerXXX functions on the app so that
      // registration of components is as easy as can be


      // Showdown comes from showdown.js
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




      couchPotato.configureApp(app);

      

      return app;

  }

);
