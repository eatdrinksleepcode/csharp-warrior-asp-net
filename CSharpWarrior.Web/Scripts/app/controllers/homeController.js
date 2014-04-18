define(['./module'], function (controllers) {
         'use strict';
         controllers.controller('HomeController', ['$scope', '$http',
        function($scope, $http) {
            $scope.name = 'Warrior, the angular is strong with you, but you must complete with your training.';
            $scope.code = "\n" +
    "using CSharpWarrior;\n" +
    "\n" +
    "public class Player : IPlayer {\n" +
    "    public WarriorAction Play() {\n" +
    "        return new WalkAction();\n" +
    "    }\n" +
    "}\n" +
    "";
            $scope.level = 1;

            $scope.run = function () {
                $scope.output = '';
                $http.post('/api/level', { levelIndex: $scope.level, code: $scope.code })
                .success(function (data) {
                    $scope.output = data.Log;
                });
            };
        }]
    );
});
             
 
