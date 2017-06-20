angular.module('MetronicApp').controller('ctrl_proveedores', function ($host, $rootScope, $scope, $http, $timeout) {

    function loadProveedores() {
        $http.get($host.getHost() + "api/proveedors/").then(function (response) {
            $scope.proveedores = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.$on('$viewContentLoaded', function () {
        
        loadProveedores();
    });

    $scope.save = function(){        
        $http.post($host.getHost() + "api/proveedors",$scope.new_proveedores).then(function(response){
        
            loadProveedores();
            $scope.new_proveedores = {};
        },function(fail){
            console.log(fail);
        });
        console.log();
    };

    $scope.delete = function(nit){
        $http.delete($host.getHost() + "api/proveedors/" + nit).then(function(response){
        
            loadProveedores();
        
        },function(fail){
            console.log(fail);
        });
    };

    $scope.getProveedores = function () {

    };

    $scope.nuevo = function () {
        $('#static').modal('show');
    };


});



