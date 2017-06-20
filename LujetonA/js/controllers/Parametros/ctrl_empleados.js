angular.module('MetronicApp').controller('ctrl_empleados', function ($host, $rootScope, $scope, $http, $timeout) {

    function loadEmpleados() {
        $http.get($host.getHost() + "api/empleadoes/").then(function (response) {
            $scope.empleados = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.$on('$viewContentLoaded', function () {
        
        loadEmpleados();
    });

    $scope.save = function(){        
        $http.post($host.getHost() + "api/empleadoes",$scope.new_empleado).then(function(response){
        
            loadEmpleados();
            $scope.new_empleado = {};
        },function(fail){
            console.log(fail);
        });
        console.log();
    };

    $scope.delete = function(id_empleado){
        $http.delete($host.getHost() + "api/empleadoes/" + id_empleado).then(function(response){
        
            loadEmpleados();
        
        },function(fail){
            console.log(fail);
        });
    };

    $scope.getEmpleados = function () {

    };

    $scope.nuevo = function () {
        $('#static').modal('show');
    };


});
