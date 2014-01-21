angular
    .module('warrior', [])
    .controller('warriorController', ['$scope', '$http', function ($scope, $http) {

        $scope.name = 'Warrior, the angular is strong with you, but you must complete with your training.';
        $scope.code = "\n" +
"public class Player : CSharpWarrior.IPlayer {\n" +
"    public string Play() {\n" +
"        return \"Hello, world!\";\n" +
"    }\n" +
"}";

        $scope.run = function () {
            $http.post('/api/level', { code: $scope.code } )
            .success(function (data) {
                $scope.output = data;
            });
        };
    }]);