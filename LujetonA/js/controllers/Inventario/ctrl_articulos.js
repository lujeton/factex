angular.module('MetronicApp').controller('ctrl_articulos', function ($host, $rootScope, $scope, $http, $timeout) {

    $scope.getHost = function(){
        return $host.getHost();
    };

    function loadArticulos() {
        $http.get($host.getHost() + "api/productoes/").then(function (response) {
            $scope.productos = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;

    function loadCategorias() {
        $http.get($host.getHost() + "api/categorias/").then(function (response) {
            $scope.categorias = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;

    $scope.$on('$viewContentLoaded', function () {
        loadArticulos();
        loadCategorias();
    });


    $scope.delete = function (id) {
        $http.delete($host.getHost() + "api/productoes/" + id).then(function (response) {

            loadArticulos();

        }, function (fail) {
            console.log(fail);
        });
    };

    $scope.nuevo = function () {
        $('#static').modal('show');
    };


    $scope.save = function () {
        save_data();
        save_image();
    };

    function save_data() {
        $scope.new_articulo.imagen =  document.getElementById('my_file').files[0].name;
        $http.post($host.getHost() + "api/productoes", $scope.new_articulo).then(function (response) {
            loadArticulos();
            $scope.new_articulo = {};
        }, function (fail) {
            console.log(fail);
        });
        console.log();
    };



    function save_image() {
        var fd = new FormData();
        var file = document.getElementById('my_file').files[0];                        
        fd.append('file', file);        

        $http.post($host.getHost() + 'api/productoes/imageManager/', fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type' : undefined }
        })
                .success(function (response) {
                    console.log("archivo gruardado exitosamente!");
                })
                .error(function (e) {
                    console.log("ocurrio un error mientras se guarda el archivo");
                    console.log(e);
                });
    }

});
