angular
    .module('warrior', [])
    .controller('warriorController', ['$scope', '$http', function ($scope, $http) {

        $scope.name = 'Warrior, the angular is strong with you, but you must complete with your training.';
        $scope.code = "public class Player { }";

        $scope.run = function () {
            $http.post('/api/level/1', { code: $scope.code })
            .success(function (data) {
                $scope.output = data;
            });
        };
    }]);