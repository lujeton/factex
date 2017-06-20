angular.module('MetronicApp').controller('ctrl_inventario', function ($host, $rootScope, $scope, $http, $timeout) {

    $scope.getHost = function(){
        return $host.getHost();
    };
    
    $scope.imprimir = function() {
        var doc = new jsPDF();
        
        var specialElementHandlers = {
            '#editor': function (element, renderer) {
                return true;
            }
        };
         
        doc.fromHTML($('#content_printable').html(), 15, 15, {
            'width': 200,
            'elementHandlers': specialElementHandlers
            
            
            
            
        });
        
        doc.save('samples-file.pdf');
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

  

});
