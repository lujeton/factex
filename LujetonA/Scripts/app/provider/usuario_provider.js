app.factory("usuarioPvr", function ($http,$rootScope) {

    var host = "http://localhost:55786";
    var dashboard = "";
    
    function inicializacion() {
        $http.defaults.headers.common['Authorization'] = 'Basic ' + localStorage.getItem("token");
        console.log("inicializado correctamente." + $http.defaults.headers.common['Authorization']);
    }

    return {
        login: function (usuario) {
            $http.post(host + '/usuario/login', usuario).then(function (response) {
                console.log(response);
                if (response.data === "bad") {
                    alert("codigo incorrecto");
                } else {                    
                    location.href = "index.html";                    
                    console.log("entro correctamente");
                    localStorage.setItem('token', response.data);
                }
            },function(err){
                console.log(err);
            });            
        },
        init: inicializacion,
        menu: function () {           
//            $http.post(host + '/usuario/getmenu/').then(function (response) {
//                $rootScope.menus = response.data;      
//                
//                var grupo = [];
//                $rootScope.menus.forEach(function(x){                    
//                    x.grupo = x.url.split("/")[1];
//                    if(!grupo[x.grupo]){ grupo[x.grupo] = []; }
//                    grupo[x.grupo].push(x);                    
//                });
//                grupo.forEach(function(item){
//                    //item.name = item.key;
//                    console.log(item);
//                });
//                $rootScope.grupos = grupo;
//                
//                console.log($rootScope.grupo);
//            });
        }
    };
});