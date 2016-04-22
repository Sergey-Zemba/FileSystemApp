var animations = {
    spinner: "/Content/ajax-loader.gif"
}

var table = $("#table");

var spinner = {
    add: function () {
        $('<img></img>')
                .attr('src', animations.spinner)
                .attr('class', 'spinner')
                .hide()
                .load(function () {
                    $(this).fadeIn();
                })
                .appendTo(table);
    },
    remove: function () {
        $('.spinner').fadeOut('slow', function () {
            $(this).remove();
        });
    }
};

var element = $("#root");

var fileSystemApp = angular.module("fileSystemApp", []);
fileSystemApp.controller("fileSystemController", function ($scope, $http) {
    $scope.goInside = function(elem) {
        spinner.add();
        var id = $(elem).attr("data-item");
        $http.get('/api/values/' + id)
        .success(function (data) {
            MakeResponse($scope, data);
            spinner.remove();
        });
    }
    $scope.goInside(element);
});

function MakeResponse($scope, data) {
    if (data.FullName == "Unauthorized") {
        alert("You don't have permission to access this folder");
    } else {
        $scope.smallSize = data.NumberOfFiles.SmallSize;
        $scope.middleSize = data.NumberOfFiles.MiddleSize;
        $scope.largeSize = data.NumberOfFiles.LargeSize;
        $scope.fullName = data.FullName;
        $scope.parentId = data.ParentId;
        $scope.fileSystemItems = {
            items: []
        };
        $.each(data.FileSystemItems, function(index, fsi) {
            $scope.fileSystemItems.items.push({ name: fsi.Name, id: fsi.Id });
        });

    }
}