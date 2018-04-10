var app = angular.module('angularjscrud', [])

app.controller('indexCtrl', function($scope, $http){

	$scope.apiBaseURL = "http://myapi/";

	$scope.model = {
		name: ""
	}

	// items fake (remover quando tivermos server)
	$scope.items = [
		{id: 1, name: "github"},
		{id: 2, name: "bitbucket"},
		{id: 3, name: "gitlab"}
	];

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
			name: ""
		}
	}

	$scope.save = function (){

		// se tiver id atualizamos, senao gravamos novo

		if($socope.model.id) {
			$http.put($scope.apiBaseURL + 'endpoint___put/' + id, $scope.model).then(function(resp){

				//limpamos o model do form
				clearModel();

			})	
		} else {
			$http.post($scope.apiBaseURL + 'endpoint___add', $scope.model).then(function(resp){
				// adicionar o item na lista
				$scope.items.push(resp);

				//limpamos o model do form
				clearModel();

			})
		}
	}

	$scope.edit = function(id) {
		// setamos o model do form para o item q queremos atualizar
		console.log("id ", id)
		$scope.model = getItem(id);
		console.log("model ", $scope.model)
	}

	$scope.delete = function(id) {
		$http.delete($scope.apiBaseURL + 'endpoint___delete/' + id).then(function(resp){

			var item = getItem(id);

			// removemos o item da lista apos remoçao do server
			if(item) {
				$scope.items.splice($scope.items.indexOf(item), 1);
			}

		})
	}

	$scope.getItems = function() {
		$http.get($scope.apiBaseURL + 'all_items').then(function(resp){

			// atençao ao formato do resp

			$scope.items = resp;
		})
	}

	// DESCOMENTAR PARA POPULAR A TELA QUANDO A MESMA CARREGAR, DEIXEI COMENTRADO PORQUE N TENHO SERVER

	//$scope.getItems();

})