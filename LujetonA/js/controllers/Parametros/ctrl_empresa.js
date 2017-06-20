angular.module('MetronicApp').controller('ctrl_empresa', function ($host, $rootScope, $scope, $http, $timeout) {

  

    function loadDatosempresa() {
        $http.get($host.getHost() + "api/datos_empresa/").then(function (response) {
            $scope.empresas = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.$on('$viewContentLoaded', function () {
        
        loadDatosempresa();
    });



    $scope.nuevo = function () {
        $('#static').modal('show');
    };


});
