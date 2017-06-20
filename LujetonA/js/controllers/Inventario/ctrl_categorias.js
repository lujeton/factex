angular.module('MetronicApp').controller('ctrl_categorias', function ($host, $rootScope, $scope, $http, $timeout) {

    function loadCategorias() {
        $http.get($host.getHost() + "api/categorias/").then(function (response) {
            $scope.categorias = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.$on('$viewContentLoaded', function () {
        
        loadCategorias();
    });

    $scope.save = function(){        
        $http.post($host.getHost() + "api/categorias",$scope.new_categoria).then(function(response){
        
            loadCategorias();
            $scope.new_categoria = {};
        },function(fail){
            console.log(fail);
        });
        console.log();
    };

    $scope.delete = function(id){
        $http.delete($host.getHost() + "api/categorias/" + id).then(function(response){
        
            loadCategorias();
        
        },function(fail){
            console.log(fail);
        });
    };

    $scope.getBodegas = function () {

    };

    $scope.nuevo = function () {
        $('#static').modal('show');
    };


});
