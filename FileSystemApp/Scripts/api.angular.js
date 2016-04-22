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
    $scope.goInside = function ($event) {
        var elem = $event.target;
        spinner.add();
        var id = $(elem).attr("data-item");
        $http.get('/api/values/' + id)
        .success(function (data) {
            MakeResponse($scope, data);
            spinner.remove();
        });
    }
    spinner.add();
    $http.get('/api/values/' + $(element).attr("data-item"))
    .success(function (data) {
        MakeResponse($scope, data);
        spinner.remove();
    });
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
            directories: [],
            files: []
        };
        $.each(data.FileSystemItems, function (index, fsi) {
            if (fsi.FileSystemItemType == 1) {
                $scope.fileSystemItems.directories.push({ name: fsi.Name, id: fsi.Id });
            } else {
                $scope.fileSystemItems.files.push({ name: fsi.Name });
            }
            
        });
        if (data.FullName == "My Computer") {
                $("#parent").addClass("hidden");
        } else {
            $("#parent").removeClass("hidden");
        }
    }
}

