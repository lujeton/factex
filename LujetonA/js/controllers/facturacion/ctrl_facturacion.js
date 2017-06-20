angular.module('MetronicApp').controller('ctrl_facturacion', function ($host, $rootScope, $scope, $http, $timeout) {

    $scope.fecha = new Date().toLocaleDateString();
    $scope.articulos_factura = [];
    $scope.cliente_factura = [];
    $scope.selectedEmpleado = {};

    $scope.getHost = function () {
        return $host.getHost();
    };
    $scope.changecolor = function () {
        spn.innerHTML = spn.innerHTML == "Pasar a Cartera" ? "Enviado a Cartera" : "Pasar a Cartera";
        document.getElementById("butt").disabled = true;
        document.getElementById("butt").style.backgroundColor = "#900C3F";


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
    function loadEmpleados() {
        $http.get($host.getHost() + "api/empleadoes/").then(function (response) {
            $scope.empleados = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;
    function loadClientes() {
        $http.get($host.getHost() + "api/clientes/").then(function (response) {
            $scope.clientes = response.data;
            console.log(response);
        }, function (error) {
            console.log(error);
        });
    }
    ;

    $scope.$on('$viewContentLoaded', function () {

    });
    $scope.delete = function (id) {
        $http.delete($host.getHost() + "api/detalle_factura/" + id).then(function (response) {
        }, function (fail) {
            console.log(fail);
        });
    };
    $scope.nuevoempl = function () {
        $('#static').modal('show');
    };
    $scope.nuevocli = function () {
        $('#stati').modal('show');
    };
    $scope.nuevopro = function () {
        $('#statik').modal('show');

    };

    $scope.save_data = function() {
        $http.post($host.getHost() + "api/facturas", 
        { 
            header : {
                fecha : new Date(),
                sub_total : $scope.subTotal,
                total : $scope.subTotal,
                idEmpleado : $scope.selectedEmpleado.id_empleado,
                idCliente : $scope.selectedCliente.idcliente,
                descuento : $scope.Descuento
            } , 
            detalles : $scope.articulos_factura
        }).then(function (response) {
            
        }, function (fail) {
            console.log(fail);
        });
        console.log();
    }
    ;

    /**
     * aqui se agrega un item a la factura
     */
    $scope.addProduct = function () {
        $scope.selectedArticulo = $scope.selectedArticulo.originalObject;
        $scope.selectedArticulo.cantidad = $scope.articulo_cantidad;
        $scope.selectedArticulo.impuesto  = 0;
        $scope.selectedArticulo.descuento = 0;
        $scope.selectedArticulo.idProducto = $scope.selectedArticulo.id;
        $scope.selectedArticulo.precio = $scope.selectedArticulo.precio_venta;
        $scope.selectedArticulo.id = 0;
        console.log($scope.selectedArticulo);
        $scope.articulos_factura.push($scope.selectedArticulo);

        //limpia el campo
        document.querySelector('#ex2').value = "";
        $scope.selectedArticulo = {};
        $scope.articulo_cantidad = 1;        
        CalcularSubTotal();
        CalcularTotal();
        CalcularDescuento();
    };

    $scope.addCliente = function () {
        $scope.selectedCliente = $scope.selectedCliente.originalObject;
        console.log($scope.selectedCliente);
    };

    $scope.addEmpleado = function(){
        $scope.selectedEmpleado = JSON.parse($scope.selectedEmpleado);
        console.log($scope.selectedEmpleado);
    };

    function CalcularTotal() {
        $scope.Total = 0;

        $scope.articulos_factura.forEach(function (item) {
            console.log(item);
            $scope.Total += item.cantidad * item.precio_venta;
        });

    }
    ;
    function CalcularDescuento() {
        $scope.Descuento = 0;

        $scope.articulos_factura.forEach(function (item) {
            console.log(item);
            $scope.Descuento += item.descuento;
        });
    }
    ;
    function CalcularSubTotal() {
        $scope.subTotal = 0;

        $scope.articulos_factura.forEach(function (item) {            
            $scope.subTotal += (item.cantidad * item.precio_venta) - item.descuento;
        });
    }
    ;       

    loadClientes();
    loadEmpleados();
    loadArticulos();
    
});



