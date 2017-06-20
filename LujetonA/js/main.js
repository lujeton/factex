/***
 Metronic AngularJS App Main Script
 ***/

/* Metronic App */
var MetronicApp = angular.module("MetronicApp", [
    "ui.router",
    "ui.bootstrap",
    "oc.lazyLoad",
    "ngSanitize",
    "angucomplete"
]);

/* Configure ocLazyLoader(refer: https://github.com/ocombe/ocLazyLoad) */
MetronicApp.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        // global configs go here
    });
}]);

/********************************************
 BEGIN: BREAKING CHANGE in AngularJS v1.3.x:
 *********************************************/
/**
 `$controller` will no longer look for controllers on `window`.
 The old behavior of looking on `window` for controllers was originally intended
 for use in examples, demos, and toy apps. We found that allowing global controller
 functions encouraged poor practices, so we resolved to disable this behavior by
 default.
 
 To migrate, register your controllers with modules rather than exposing them
 as globals:
 
 Before:
 
 ```javascript
 function MyController() {
 // ...
 }
 ```
 
 After:
 
 ```javascript
 angular.module('myApp', []).controller('MyController', [function() {
 // ...
 }]);
 
 Although it's not recommended, you can re-enable the old behavior like this:
 
 ```javascript
 angular.module('myModule').config(['$controllerProvider', function($controllerProvider) {
 // this option might be handy for migrating old apps, but please don't use it
 // in new ones!
 $controllerProvider.allowGlobals();
 }]);
 **/

//AngularJS v1.3.x workaround for old style controller declarition in HTML
MetronicApp.config(['$controllerProvider', function ($controllerProvider) {
    // this option might be handy for migrating old apps, but please don't use it
    // in new ones!
    $controllerProvider.allowGlobals();
}]);

/********************************************
 END: BREAKING CHANGE in AngularJS v1.3.x:
 *********************************************/

/* Setup global settings */
MetronicApp.factory('settings', ['$rootScope', function ($rootScope) {
    // supported languages
    var settings = {
        layout: {
            pageSidebarClosed: false, // sidebar menu state
            pageContentWhite: true, // set page content layout
            pageBodySolid: false, // solid body color state
            pageAutoScrollOnLoad: 1000 // auto scroll to top on page load
        },
        assetsPath: 'Assets',
        globalPath: 'Assets/global',
        layoutPath: 'Assets/layouts/layout'
    };

    $rootScope.settings = settings;

    return settings;
}]);

/* Setup App Main Controller */
MetronicApp.controller('AppController', ['$scope', '$rootScope', function ($scope, $rootScope) {
    $scope.$on('$viewContentLoaded', function () {
        //App.initComponents(); // init core components
        //Layout.init(); //  Init entire layout(header, footer, sidebar, etc) on page load if the partials included in server side instead of loading with ng-include directive 
    });
}]);

/***
 Layout Partials.
 By default the partials are loaded through AngularJS ng-include directive. In case they loaded in server side(e.g: PHP include function) then below partial 
 initialization can be disabled and Layout.init() should be called on page load complete as explained above.
 ***/

/* Setup Layout Part - Header */
MetronicApp.controller('HeaderController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initHeader(); // init header
    });
}]);

/* Setup Layout Part - Sidebar */
MetronicApp.controller('SidebarController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initSidebar(); // init sidebar
    });
}]);

/* Setup Layout Part - Quick Sidebar */
MetronicApp.controller('QuickSidebarController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        setTimeout(function () {
            QuickSidebar.init(); // init quick sidebar        
        }, 2000)
    });
}]);

/* Setup Layout Part - Theme Panel */
MetronicApp.controller('ThemePanelController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Demo.init(); // init theme panel
    });
}]);

/* Setup Layout Part - Footer */
MetronicApp.controller('FooterController', ['$scope', function ($scope) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initFooter(); // init footer
    });
}]);

/* Setup Rounting For All Pages */
MetronicApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {//        

    // Redirect any unmatched url
    $urlRouterProvider.otherwise("/dashboard.html");

    $stateProvider
            .state("cartera", {
                url: "/facturacion/cartera",
                templateUrl: "Template/Facturacion/Cartera.html",
                controller: "ctrl_cartera",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/facturacion/ctrl_cartera.js'
                            ]
                        }]);
                    }]
                }
            })

            .state("/main", {
                templateUrl: "Template/main.html"
            })
            /* parametros */
            .state("Bodega", {
                url: "/parametros/bodega",
                templateUrl: "Template/Parametros/bodega.html",
                controller: "ctrl_bodegas",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Parametros/ctrl_bodegas.js'
                            ]
                        }]);
                    }]
                }
            })
            .state("Clientes", {
                url: "/parametros/clientes",
                templateUrl: "Template/Parametros/clientes.html",
                controller: "ctrl_clientes",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Parametros/ctrl_clientes.js'
                            ]
                        }]);
                    }]
                }
            })
            .state("Datos Basicos", {
                url: "/parametros/datos_basicos",
                templateUrl: "Template/Parametros/datos_basicos.html",
                controller: "ctrl_empresa",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Parametros/ctrl_empresa.js'
                            ]
                        }]);
                    }]
                }
            })
            .state("Empleados", {
                url: "/parametros/empleados",
                templateUrl: "Template/Parametros/empleados.html",
                controller: "ctrl_empleados",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Parametros/ctrl_empleados.js'
                            ]
                        }]);
                    }]
                }
            })
            .state("Proveedores", {
                url: "/parametros/proveedores",
                templateUrl: "Template/Parametros/proveedores.html",
                controller: "ctrl_proveedores",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Parametros/ctrl_proveedores.js'
                            ]
                        }]);
                    }]
                }
            })

            /* Inventario */
            .state("Articulos", {
                url: "/inventario/articulos",
                templateUrl: "Template/Inventario/Articulos.html",
                controller: "ctrl_articulos",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Inventario/ctrl_articulos.js'
                            ]
                        }]);
                    }]
                }
            })
            .state("Inventarios", {
                url: "/inventario/inventarios",
                templateUrl: "Template/Inventario/Inventario.html",
                controller: "ctrl_inventario",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Inventario/ctrl_inventario.js'
                            ]
                        }]);
                    }]
                }
            })
            .state("Kardex", {
                url: "/inventario/kardex",
                templateUrl: "Template/Inventario/Kardex.html"
            })

            .state("Translados", {
                url: "/inventario/translados",
                templateUrl: "Template/Inventario/Translados.html",
                controller: "ctrl_translados",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Inventario/ctrl_translados.js'
                            ]
                        }]);
                    }]
                }
            })


            .state("Movimientos", {
                url: "/inventario/movimientos",
                templateUrl: "Template/Inventario/Movimientos.html",
                controller: "ctrl_movimientos",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Inventario/ctrl_movimientos.js'
                            ]
                        }]);
                    }]
                }
            })

               .state("Categorias", {
                   url: "/inventario/categorias",
                   templateUrl: "Template/Inventario/categorias.html",
                   controller: "ctrl_categorias",
                   resolve: {
                       deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                           return $ocLazyLoad.load([{
                               name: 'MetronicApp',
                               files: [
                                   'js/controllers/Inventario/ctrl_categorias.js'
                               ]
                           }]);
                       }]
                   }
               })
            /* facturacion */
            .state("Consultas", {
                url: "/facturacion/consultas",
                templateUrl: "Template/Facturacion/Consultas.html"
            })
            .state("Detalle_factura", {
                url: "/facturacion/detallefactura",
                templateUrl: "Template/Facturacion/detalle_factura.html",
                controller: "ctrl_facturacion",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Facturacion/ctrl_facturacion.js'
                            ]
                        }]);
                    }]
                }
            })
            .state("Facturas", {
                url: "/facturacion/facturas",
                templateUrl: "Template/Facturacion/Facturas.html"
            })
            .state("Resumen", {
                url: "/facturacion/resumen",
                templateUrl: "Template/Facturacion/Resumen_utilidades.html",
                controller: "ctrl_resumen",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load([{
                            name: 'MetronicApp',
                            files: [
                                'js/controllers/Facturacion/ctrl_resumen.js'
                            ]
                        }]);
                    }]
                }
            })

            // Dashboard
            .state('dashboard', {
                url: "/dashboard.html",
                templateUrl: "Template/dashboard.html",
                data: { pageTitle: 'Admin Dashboard Template' },
                controller: "DashboardController",
                resolve: {
                    deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            name: 'MetronicApp',
                            insertBefore: '#ng_load_plugins_before', // load the above css files before a LINK element with this ID. Dynamic CSS files must be loaded between core and theme css files
                            files: [
                                'Assets/global/plugins/morris/morris.css',
                                'Assets/global/plugins/morris/morris.min.js',
                                'Assets/global/plugins/morris/raphael-min.js',
                                'Assets/global/plugins/jquery.sparkline.min.js',
                                'Assets/pages/scripts/dashboard.min.js',
                                'js/controllers/DashboardController.js', 
                            ]
                        });
                    }]
                }
            });

}]);
MetronicApp.factory("$host", function () {
    return {
        getHost: function () {
            //return "";
            return "http://localhost:55786/";
        }
    };
});
/* Init global settings and run the app */
MetronicApp.run(["$rootScope", "settings", "$state", function ($rootScope, settings, $state) {
    $rootScope.$state = $state; // state to be accessed from view
    $rootScope.$settings = settings; // state to be accessed from view
}]);