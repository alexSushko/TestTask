app.service("APIService", function ($http) {

    //Owners

    this.getOwners = function (page, items, filter) {
        return $http({
            method: 'get',
            url: '/api/owner/get/' + page + '/' + items + '/' + filter
        })
    },
    this.findOwners = function (name, page, items, filter) {
        return $http({
            method: 'get',
            url: '/api/owner/find/' + name + '/' + page + '/' + items + '/' + filter
        })
    },
    this.addOwner = function (owner) {
        return $http({
            method: 'put',
            data: { Id: 0, Name: owner, },
            url: '/api/owner/'
        })
    },
    this.deleteOwner = function (id) {
        return $http({
            method: 'post',
            data: id,
            url: '/api/owner/'
        })
    }

    //Pets


   
}); 