define(['./module'], function (controllers) {
         'use strict';
         controllers.controller('HomeController', ['$scope', '$http',
        function($scope, $http) {
            $scope.name = 'Warrior, the angular is strong with you, but you must complete with your training.';
            $scope.code = "\n" +
    "using CSharpWarrior;                         \n" +
    "                                             \n" +
    "public class Player : IPlayer {              \n" +
    "    public Action Play() {                   \n" +
    "        return new WalkAction();             \n" +
    "    }                                        \n" +
    "}                                            \n" +
    "";
            $scope.run = function () {
                $http.post('/api/level', { code: $scope.code })
                .success(function (data) {
                    $scope.output = data;
                });
            };
        }]
    );
});
             
 
