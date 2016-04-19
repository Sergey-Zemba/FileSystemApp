var animations = {
    spinner: "/Content/ajax-loader.gif"
}
var table = $("#sizes");

function GetContent(el) {
    var id = $(el).attr('data-item');
    $.ajax({
        url: "/api/values/" + id,
        type: "GET",
        beforeSend: function() {
            spinner.add();
        },
        dataType: "json",
        success: function (data) {
            WriteResponse(data);
            spinner.remove();
        },
        error: function () {
            alert("Error");
        }
    });
}

function WriteResponse(folder) {
    if (folder.FullName == "Unauthorized") {
        alert("You don't have permission to access this folder");
    } else {
        var table = "<tr><td>" + folder.NumberOfFiles.SmallSize + "</td>" +
        "<td>" + folder.NumberOfFiles.MiddleSize + "</td>" +
        "<td>" + folder.NumberOfFiles.LargeSize + "</td></tr>";
        var result = "<span><b>Current path: </b>" + folder.FullName + "</span><br/><br/>";
        if (folder.FullName != "My Computer") {
            result += "<a data-item='" + folder.ParentId + "' onclick='GetContent(this);' href='#'>..</a><br/>";
        }
        $.each(folder.FileSystemItems, function (index, fsi) {
            if (fsi.FileSystemItemType == 1) {
                result += "<a data-item='" + fsi.Id + "' onclick='GetContent(this);' href='#'>" +
                    fsi.Name + "</a><br/>";
            } else {
                result += "<span>" + fsi.Name + "</span><br/>";
            }
        });
        $("#fileSystem").html(result);
        $("#sizes").html(table);
    }
}

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