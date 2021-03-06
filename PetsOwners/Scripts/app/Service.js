﻿app.service("APIService", function ($http) {

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
            method: 'post',
            data: { Id: 0, Name: owner,Pets:[] },
            url: '/api/owner/'
        })
    },
    this.deleteOwner = function (id) {
        return $http({
            method: 'delete',
            data: id,
            url: '/api/owner/'
        })
    }

    //Pets


    
}); 
app.service("POHelperService", function ($http) {

});
app.filter('highLight', function ($sce) {
    return function (input, highliteWord) {
        var input = input || '';
        var tag = '<span class="highLight">';
        var closetag = '</span>';
        var result = input;
        if (highliteWord != '' && input.includes(highliteWord)) {

            result = result.replace(RegExp(highliteWord,'g'), tag + highliteWord + closetag);
        }
       
        return $sce.trustAsHtml(result);

    };
});

    
