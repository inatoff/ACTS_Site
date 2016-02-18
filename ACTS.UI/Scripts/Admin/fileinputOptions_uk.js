$(document).on('ready', function () {
    $("#input-image").fileinput({
        language: "uk",
        previewSettings: {
            image: { width: "auto", height: "300px" },
        },
        //allowedFileTypes:'image',
        previewFileType: "image",
        browseClass: "btn btn-success",
        browseLabel: "Підібрати Зображення",
        browseIcon: "<i class=\"glyphicon glyphicon-picture\"></i>&nbsp;&nbsp;",
        removeClass: "btn btn-danger",
        removeLabel: "Видалити",
        removeIcon: "<i class=\"glyphicon glyphicon-trash\"></i>&nbsp;&nbsp;",
        showUpload: false,
        maxFileSize: 5120
    });
});