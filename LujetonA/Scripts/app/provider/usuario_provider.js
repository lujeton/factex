app.factory("usuarioPvr", function ($http) {
    function inicializacion() {
        $http.defaults.headers.common['Authorization'] = 'Basic ' + localStorage.getItem("token");
        console.log("inicializado correctamente." + $http.defaults.headers.common['Authorization']);
    }

    return {
        login: function (usuario) {
            $http.post('/usuario/login', usuario).then(function (response) {
                console.log(response);
                if (response.data === "bad") {
                    alert("codigo incorrecto");
                } else {                    
                    location.href = "http://localhost:55786/Home/Dashboard";
                    localStorage.setItem('token', response.data);
                }
            });            
        },
        init: inicializacion,
        menu: function () {           
            $http.post('/usuario/getmenu/').then(function (response) {
                console.log(response);                
            });
        }
    };
})