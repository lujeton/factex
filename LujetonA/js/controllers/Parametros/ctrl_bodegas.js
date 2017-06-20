angular.module('MetronicApp').controller('ctrl_bodegas', function ($host, $rootScope, $scope, $http, $timeout) {

    function loadBodegas() {
        $http.get($host.getHost() + "api/bodegas/").then(function (response) {
            $scope.bodegas = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    };

    $scope.$on('$viewContentLoaded', function () {
        
        loadBodegas();
    });

    $scope.save = function(){        
        $http.post($host.getHost() + "api/bodegas",$scope.new_bodegas).then(function(response){
        
            loadBodegas();
            $scope.new_bodegas = {};
        },function(fail){
            console.log(fail);
        });
        console.log();
    };

    $scope.delete = function(id){
        $http.delete($host.getHost() + "api/bodegas/" + id).then(function(response){
        
            loadBodegas();
        
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
