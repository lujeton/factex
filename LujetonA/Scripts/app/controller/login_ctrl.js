app.controller("login_ctrl", function ($scope,usuarioPvr) {
    /////////////////////////////////////////////////////////
    ///>                    eventos
    /////////////////////////////////////////////////////////
    $scope.evt_iniciar_sesion = function () {
        usuarioPvr.login($scope.usuario);
       
    };

});