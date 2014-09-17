

define(['/App/TheApp.js'], function (app) {

    app.registerController(
      'BoardController',
      ['$scope', '$state',
      function ($scope, $state) {

          $scope.boardMessage = "This is a board message";

      }]
    );
});