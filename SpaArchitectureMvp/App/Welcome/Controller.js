

define(['/App/TheApp.js'], function (app) {

    app.registerController(
      'WelcomeController',
      ['$scope', '$state',
      function ($scope, $state) {

          $scope.sampleMessage = "Hello";

          $scope.sampleMarkdown = "***Welcome***";

      }]
    );
});