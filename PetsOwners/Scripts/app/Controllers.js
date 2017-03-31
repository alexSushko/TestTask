app.config([
    '$routeProvider', '$locationProvider',
    function ($routeProvide, $locationProvider) {
        $locationProvider.html5Mode(true)
        $routeProvide

            .when('/owner', {
                templateUrl: '/Static/template/owners.html',
                controller: 'ownersController'
            })
            .when('/pet', {
                templateUrl: '/Static/template/pets.html',
                controller: 'petsController'
            })

            .otherwise({
                redirectTo: '/owner'
            });
    }]);

app.controller('ownersController', function ($scope, APIService) {
    //Initialize
    $scope.currentPage = 1;
    $scope.itemsOnPage = 3;
    $scope.filter = "Id-Down";
    $scope.data;
    $scope.ownerName = '';

    
    reloadData();
    function reloadData() {
        if ($scope.ownerName == '' || $scope.ownerName ==null) getAll();
        else findAll();
    }
    function getAll() {
        var servCall = APIService.getOwners($scope.currentPage, $scope.itemsOnPage, $scope.filter);
        servCall.then(function (d) {
            $scope.data = d.data;
            $scope.currentPage = d.data.currentPage;
            $scope.countOfPages = d.data.countOfPages;
            $scope.ownersCount = d.data.ownersCount;
            $scope.itemsOnPage = d.data.itemsOnPage;
            $scope.owners = d.data.list;
        }, function (response) {
        $scope.errors = parseErrors(response);
        })
    };
    function findAll() {
        var servCall = APIService.findOwners($scope.ownerName, $scope.currentPage, $scope.itemsOnPage, $scope.filter);
        servCall.then(function (d) {
            $scope.data = d.data;
            $scope.currentPage = d.data.currentPage;
            $scope.countOfPages = d.data.countOfPages;
            $scope.ownersCount = d.data.ownersCount;
            $scope.itemsOnPage = d.data.itemsOnPage;
            $scope.owners = d.data.list;
        }, function (response) {
            console.log(response);
            $scope.errors = parseErrors(response);
        })
    }
    function parseErrors(response) {
        var errors = [];
        for (var key in response.data.ModelState) {
            for (var i = 0; i < response.data.ModelState[key].length; i++) {
                errors.push(response.data.ModelState[key][i]);
            }
        }
        return errors;
    }
    //$scope.saveOwner = function () {
    //    var owner = {
    //        Name: $scope.ownername,
    //    };
    //    var saveOwner = APIService.saveOwner(owner);
    //    saveOwner.then(function (d) {
    //        getAll();
    //    }, function (error) {
    //        console.log('Oops! Something went wrong while saving the data.')
    //    })
    //};
    $scope.addOwner = function(){
        var servCall = APIService.addOwner($scope.ownerName);
        servCall.then(function (success) {
           
        }, function (error) {
            console.log(error);
            $scope.errors = parseErrors(error);
        });
        reloadData();
    }
    $scope.deleteOwner = function (id) {
        var servCall = APIService.deleteOwner(id);
        servCall.then(function (error) {
            $log.error('Oops! Something went wrong while fetching the data.')
        })

    }
    $scope.filtering = function (name) {
        if ($scope.filter.split('-')[0] == name) {
            $scope.filter = name + '-' + ($scope.filter.split('-')[1] == "Up" ? "Down" : "Up");
        } else {
            $scope.filter = name + "-Down";
        }
        getAll();
    };
    $scope.filterArrow = function (name) {
        if ($scope.filter.split('-')[0] == name) {
            return $scope.filter.split('-')[1] == "Up" ? 'glyphicon-chevron-up' : 'glyphicon-chevron-down';
            
        
        } else {
            return '';
        }
    }

    

    $scope.$watch("currentPage + itemsOnPage+ownerName", function () {
        reloadData();
    });
})   
app.controller('petsСontroller', function ($scope, APIService) {

});