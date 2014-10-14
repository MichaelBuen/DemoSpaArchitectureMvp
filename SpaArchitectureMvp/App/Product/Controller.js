

define(['/App/TheApp.js'], function (app) {

    app.registerController(
      'ProductController',
      ['$scope', '$state', '$resource',
      function ($scope, $state, $resource) {
          var self = this;
          $scope.prod = self;


          self.productDto = {
              ProductName: '',
              ProductCategoryId : null
          };

          self.productCategories = [];
          self.productMessage = "This is a product messagex";

          var productCategoryRest = $resource('/api/productCategory');
                    
          productCategoryRest.query({}, function (categories) {
              self.categories = categories;              
          });

          
          var productRest = $resource('/api/product');

          self.save = function () {
              productRest.save(self.productDto, function (data) {
                  self.productId = data;
                  
              }, function (err) {
                  alert(err.data.ExceptionMessage)
              });
          };

      }]
    );
});