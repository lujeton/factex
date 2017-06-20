angular.module('MetronicApp').controller('ctrl_movimientos', function ($host, $rootScope, $scope, $http, $timeout) {

    $scope.getHost = function () {
        return $host.getHost();
    };

    function loadMovimientos() {
        $http.get($host.getHost() + "api/movimientoes/").then(function (response) {

            $scope.movimientos = response.data;

            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;

    $scope.imprimir = function() {
        var doc = new jsPDF();
        var specialElementHandlers = {
            '#editor': function (element, renderer) {
                return true;
            }
        };

        doc.fromHTML($('#content_printable').html(), 15, 15, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });
        
        doc.save('sample-file.pdf');
    };

    function loadProductos() {
        $http.get($host.getHost() + "api/productoes/").then(function (response) {
            $scope.productos = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;

    function loadBodegas() {
        $http.get($host.getHost() + "api/bodegas/").then(function (response) {
            $scope.bodegas = response.data;
            $scope.bodegas2 = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;
    function loadTipoMovimiento() {
        $http.get($host.getHost() + "api/tipomovimientoes/").then(function (response) {
            $scope.tipomovimiento = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;

    function loadProveedor() {
        $http.get($host.getHost() + "api/proveedors/").then(function (response) {
            $scope.proveedores = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;

    $scope.$on('$viewContentLoaded', function () {
        loadMovimientos();
        loadProveedor();
        loadTipoMovimiento();
        loadBodegas();
        loadProductos();
    });

    $scope.save = function () {
        $http.post($host.getHost() + "api/movimientoes", $scope.new_movimiento).then(function (response) {
            loadMovimientos();
            $scope.new_movimiento = {};
        }, function (fail) {
            console.log(fail);
        });
        console.log();
    };

    $scope.save_traslado = function () {
        $http.post($host.getHost() + "api/movimientoes/traslado", $scope.new_traslado).then(function (response) {
            loadMovimientos();
            $scope.new_traslado = {};
        }, function (fail) {
            console.log(fail);
        });
    };


    $scope.delete = function (id) {
        $http.delete($host.getHost() + "api/movimientoes/" + id).then(function (response) {
            loadMovimientos();
        }, function (fail) {
            console.log(fail);
        });
    };

    $scope.nuevo = function () {
        $('#static').modal('show');
    };

    $scope.translado = function () {
        $('#stati').modal('show');
    };
});
