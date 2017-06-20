angular.module('MetronicApp').controller('ctrl_clientes', function ($host, $rootScope, $scope, $http, $timeout) {

    function loadClientes() {
        $http.get($host.getHost() + "api/clientes/").then(function (response) {
            $scope.clientes = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.$on('$viewContentLoaded', function () {
        
        loadClientes();
    });

    $scope.save = function(){        
        $http.post($host.getHost() + "api/clientes",$scope.new_cliente).then(function(response){
        
            loadClientes();
            $scope.new_cliente = {};
        },function(fail){
            console.log(fail);
        });
        console.log();
    };

    $scope.delete = function(idcliente){
        $http.delete($host.getHost() + "api/clientes/" + idcliente).then(function(response){
        
            loadClientes();
        
        },function(fail){
            console.log(fail);
        });
    };

    $scope.getClientes = function () {

    };

    $scope.nuevo = function () {
        $('#static').modal('show');
    };


});
