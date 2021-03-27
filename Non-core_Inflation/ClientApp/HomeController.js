// Interceptor of operations ajax
var InterceptorRpt = function ($q, $rootScope) {
    return {
        request: function (config) {
            $rootScope.loading = true;
            return config;
        },

        response: function (result) {
            $rootScope.loading = false;
            return result;
        },
        responseError: function (rejection) {
            $rootScope.loading = false;
            return $q.reject(rejection);
        }
    };
};

//Principal module
var module = angular.module('HomeApp', ['Service', 'ngMaterial', 'ngAnimate']).config(function ($httpProvider) { //'ui.bootstrap', 'ngTable','ngAnimate', 'ngMaterial'
    $httpProvider.interceptors.push(InterceptorRpt);
});

//angular
module.controller('HomeCtrl',
    ['$scope', 'HomeSvc', '$filter', '$timeout', '$mdDialog', '$animate', // '$uibModal', '$document', 'NgTableParams',
        function (s, hsvc, f, to, mdD, alert)
        {
            s.showAlert = function (message)
            {   // Appending dialog to document.body to cover sidenav in docs app Modal dialogs should fully cover application to prevent interaction outside of dialog
                mdD.show(mdD.alert()
                    .parent(angular.element(document.querySelector('#popupContainer')))
                    .clickOutsideToClose(true)
                    .title('¡Serie!')
                    .textContent(message)//'You can specify some description text in here.')
                    .ariaLabel('Alert Dialog Demo')
                    .ok('Aceptar') //.targetEvent(ev) 
                );
            };
            
            s.UserMessage = "";
            s.NotificationMessage = "";
            s.NonCoreInflation = "";

            s.SendMessage = function () {
                if (s.UserMessage === "") {
                    s.NotificationMessage = "El Mensaje de Usuario es requerido.";
                    return;
                }               

                hsvc.SendMessage(s.UserMessage).then(function (ReturnMessage) {
                    s.loading = true;
                    s.NotificationMessage = ReturnMessage.data;
                }, function (error) {
                    if (error.data === null) {
                        //s.showAlert('Error: ¡El servidor no responde!'); 
                        console.log(error);
                        s.NotificationMessage = "Error: ¡El servidor no responde!";
                    }
                    else {
                        //s.showAlert(error.data); 
                        console.log(error);
                        s.NotificationMessage = error.data;
                    }
                });
            };

            s.GetSerie = function () {

                hsvc.GetSerie().then(function (ReturnSerie) {
                    s.loading = true;
                    s.NonCoreInflation = ReturnSerie.data;
                }, function (error) {
                    if (error.data === null) {
                        //s.showAlert('Error: ¡El servidor no responde!'); 
                        console.log(error);
                        s.NotificationMessage = "Error: ¡El servidor no responde!";
                    }
                    else {
                        //s.showAlert(error.data); 
                        console.log(error);
                        //s.NotificationMessage = error.data;
                        s.NonCoreInflation = error.data;
                    }
                });
            };

        }
    ]);