var ReportService = angular.module('Service', []);

ReportService.factory('HomeSvc',
    ['$http', '$filter', '$q', '$location',
        function (h, f, q, l) {
            var service = {};
            var servername = l.protocol() + "://" + l.host() + ":" + l.port();
            var pathName = location.pathname === undefined || location.pathname === "/" ? "" : location.pathname;
            pathName = pathName + '/Home/';
            var defaultServer = pathName;
            var moduleName = 'Serie';

            service.SendMessage = function (UserMessage) {
                var deferred = q.defer();
                var config = {
                    headers: { 'Content-Type': false }
                };
                var method = 'SendMessage/';
                var params = {
                    UserMessage: UserMessage
                };
                h({
                    url: defaultServer + method,
                    method: 'POST',
                    config: config,
                    params: params
                })
                    .then(function (dataevents) {
                        //console.log('en el servicio');
                        deferred.resolve(dataevents);
                    })
                    .catch(function (err) {
                        //console.log('catch error');
                        deferred.reject(err);
                    })
                return deferred.promise;
            };

            service.GetSerie = function () {
                var deferred = q.defer();
                var config = {
                    headers: { 'Content-Type': false }
                };
                var method = 'GetSerie/';
                var params = { };
                h({
                    url: defaultServer + method,
                    method: 'GET',
                    config: config,
                    params: params
                })
                    .then(function (dataevents) {
                        //console.log('en el servicio');
                        deferred.resolve(dataevents);
                    })
                    .catch(function (err) {
                        //console.log('catch error');
                        deferred.reject(err);
                    })
                return deferred.promise;
            };

            return service;
        }]);