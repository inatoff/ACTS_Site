$(document).on('ready', function () {
    $("#input-image").fileinput({
        previewSettings: {
            image: { width: "auto", height: "300px" },
        },
        //allowedFileTypes:'image',
        previewFileType: "image",
        browseClass: "btn btn-success",
        browseLabel: "Pick Image",
        browseIcon: "<i class=\"glyphicon glyphicon-picture\"></i> ",
        removeClass: "btn btn-danger",
        removeLabel: "Delete",
        removeIcon: "<i class=\"glyphicon glyphicon-trash\"></i> ",
        showUpload: false,
        maxFileSize: 5120
    });
});