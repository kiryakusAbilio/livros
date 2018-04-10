var app = angular.module('angularjscrud', [])

app.controller('indexCtrl', function($scope, $http){

	$scope.apiBaseURL = "http://localhost:57171/api/v1/public/";

	$scope.model = {
		title: "",
		price: "",
		isactive: "",
		categoryid: ""
	}

	$scope.categories= [{
		id: 1,
		title: 'Ação'
	}, 
	{
		id: 2,
		label: 'Aventura',
	}];
	
	$scope.selectedCategory = {};


	
	
	/**
	 * Obtem um ítem da lista local, por ID
	 * @param  {[type]} id ID
	 * @return {[type]}    [description]
	 */
	 var getItem = function(id){
	 	return _.find($scope.items, function(item){ return item.id == id });

	 }

	 var clearModel = function(){
	 	$scope.model = {
	 		title: ""
	 	}
	 }

	 $scope.save = function (){
	 	console.log("$scope.selectedCategory",$scope.selectedCategory);
	 	$scope.model.categoryid = $scope.selectedCategory;
	 	if($scope.model.id) {
	 		$http.put($scope.apiBaseURL + 'category/' + $scope.model.id, $scope.model).then(function(resp){

	 			clearModel();

	 		})  
	 	} else {
	 		$http.post($scope.apiBaseURL + 'categories', $scope.model).then(function(resp){
	 			
	 			$scope.items.push(resp);
	 			
	 			clearModel();

	 		})
	 	}
	 }

	 $scope.edit = function(item) {              
	 	$scope.model = item;
	 }

	 $scope.delete = function(id) {
	 	$http.delete($scope.apiBaseURL + 'category/' + id).then(function(resp){

	 		var item = getItem(id);
	 		if(item) {
	 			$scope.items.splice($scope.items.indexOf(item), 1);
	 		}

	 	})
	 }

	 $scope.getItems = function() {  
	 	$http.get($scope.apiBaseURL + 'categories').then(function(resp){

	 		$scope.items = resp.data;
	 	})
	 }


	 $scope.getItems();

	})