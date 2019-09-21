"use strict";

document.addEventListener("DOMContentLoaded", init, false);
var AttachmentArray = [];
var arrCounter = 0;
var filesCounterAlertStatus = false;

var ul = document.createElement('ul');
ul.className = ("thumb-Images");
ul.id = "imgList";

function init() {
    document.querySelector('#files').addEventListener('change', handleFileSelect, false);
    handleFileLoad(JSON.parse("[" + $("#Pictures").val() + "]"));
}

function handleFileSelect(e) {
    if (!e.target.files) return;
    var files = e.target.files;
    for (var i = 0, f; f = files[i]; i++) {
        var fileReader = new FileReader();
        fileReader.onload = (function (readerEvt) {
            return function (e) {
                ApplyFileValidationRules(readerEvt)
                RenderThumbnail(e, readerEvt);
                FillAttachmentArray(e, readerEvt)
            };
        })(f);

        fileReader.readAsDataURL(f);
    }
    document.getElementById('files').addEventListener('change', handleFileSelect, false);
}

function handleFileLoad(files) {
    if (files === undefined || files.length == 0) return;
    var mainPicture = JSON.parse($("#MainPictureFile").val());
    for (var i = 0, f; f = files[0][i]; i++) {
        RenderThumbnailOnLoad(f, mainPicture);
        FillAttachmentOnLoad(f)
    }
}


jQuery(function ($) {
    $('div').on('click', '.img-wrap .close', function () {
        var id = $(this).closest('.img-wrap').find('img').data('id');
        var elementPos = AttachmentArray.map(function (x) { return x.FileName; }).indexOf(id);
        if (elementPos !== -1) {
            AttachmentArray.splice(elementPos, 1);
        }

        $(this).parent().find('img').not().remove();
        $(this).parent().find('div').not().remove();
        $(this).parent().parent().find('div').not().remove();

        var lis = document.querySelectorAll('#imgList li');
        for (var i = 0; li = lis[i]; i++) {
            if (li.innerHTML == "") {
                li.parentNode.removeChild(li);
            }
        }

    });
}
)

jQuery(function ($) {
    $('div').on('click', '.img-wrap', function () {
        $(".img-wrap").each(function () {
            $(this).removeClass("selectedThumbnail");
            $('.check').remove();
        });

        if (!$(this).hasClass("selectedThumbnail")) {
            $(this).addClass('selectedThumbnail');
            $(this).append("<span class='check'>&#10004;</span>");

            const imgSrc = $(this).find('img').attr('src');
            $("#MainPictureFile").val(imgSrc.substring(imgSrc.indexOf("base64") + 7, imgSrc.length));
        }
    });
}
)


function ApplyFileValidationRules(readerEvt) {
    if (CheckFileType(readerEvt.type) == false) {
        alert("The file (" + readerEvt.name + ") does not match the upload conditions, You can only upload jpg/png/gif files");
        e.preventDefault();
        return;
    }

    if (CheckFileSize(readerEvt.size) == false) {
        alert("The file (" + readerEvt.name + ") does not match the upload conditions, The maximum file size for uploads should not exceed 300 KB");
        e.preventDefault();
        return;
    }

    if (CheckFilesCount(AttachmentArray) == false) {
        if (!filesCounterAlertStatus) {
            filesCounterAlertStatus = true;
            alert("You have added more than 10 files. According to upload conditions you can upload 10 files maximum");
        }
        e.preventDefault();
        return;
    }
}

function CheckFileType(fileType) {
    if (fileType == "image/jpeg") {
        return true;
    }
    else if (fileType == "image/png") {
        return true;
    }
    else if (fileType == "image/gif") {
        return true;
    }
    else {
        return false;
    }
    return true;
}

function CheckFileSize(fileSize) {
    if (fileSize < 300000) {
        return true;
    }
    else {
        return false;
    }
    return true;
}

function CheckFilesCount(AttachmentArray) {
    var len = 0;
    for (var i = 0; i < AttachmentArray.length; i++) {
        if (AttachmentArray[i] !== undefined) {
            len++;
        }
    }
    if (len > 9) {
        return false;
    }
    else {
        return true;
    }
}

function RenderThumbnail(e, readerEvt) {
    var li = document.createElement('li');
    ul.appendChild(li);
    if (AttachmentArray.length == 0) {
        li.innerHTML = ['<div class="img-wrap selectedThumbnail"> <span class="check">&#10004;</span> <span class="close">&times;</span>' +
            '<img class="thumb" src="', e.target.result, '"/>' + '</div>'].join('');

        //todo SOLID!!
        $("#MainPictureFile").val(e.target.result.substring(e.target.result.indexOf("base64") + 7, e.target.result.length));
    }
    else {
        li.innerHTML = ['<div class="img-wrap"> <span class="close">&times;</span>' +
            '<img class="thumb" src="', e.target.result, '"/>' + '</div>'].join('');
    }

    var div = document.createElement('div');
    div.className = "FileNameCaptionStyle";
    li.appendChild(div);
    div.innerHTML = [readerEvt.name].join('');
    document.getElementById('Filelist').insertBefore(ul, null);
}

function RenderThumbnailOnLoad(file, mainPicture) {
    var li = document.createElement('li');
    ul.appendChild(li);

    debugger;
    if (file == mainPicture) {
        li.innerHTML = ['<div class="img-wrap selectedThumbnail"> <span class="check">&#10004;</span> <span class="close">&times;</span>' +
            '<img class="thumb" src="', "data:image/jpeg;base64,".concat(file), '" title="" data-id=""/>' + '</div>'].join('');
    }
    else {
        li.innerHTML = ['<div class="img-wrap"> <span class="close">&times;</span>' +
            '<img class="thumb" src="', "data:image/jpeg;base64,".concat(file), '" title="" data-id=""/>' + '</div>'].join('');
    }


    var div = document.createElement('div');
    div.className = "FileNameCaptionStyle";
    li.appendChild(div);
    div.innerHTML = '';
    document.getElementById('Filelist').insertBefore(ul, null);
}

function FillAttachmentOnLoad(file) {
    AttachmentArray[arrCounter] =
        {
            AttachmentType: 1,
            ObjectType: 1,
            FileName: "1",
            FileDescription: "Attachment",
            NoteText: "",
            MimeType: 'png',
            Content: file,
            FileSizeInBytes: file.length,
        };
    arrCounter = arrCounter + 1;
}


function FillAttachmentArray(e, readerEvt) {
    AttachmentArray[arrCounter] =
        {
            AttachmentType: 1,
            ObjectType: 1,
            FileName: readerEvt.name,
            FileDescription: "Attachment",
            NoteText: "",
            MimeType: readerEvt.type,
            Content: e.target.result.split("base64,")[1],
            FileSizeInBytes: readerEvt.size,
        };
    arrCounter = arrCounter + 1;
}