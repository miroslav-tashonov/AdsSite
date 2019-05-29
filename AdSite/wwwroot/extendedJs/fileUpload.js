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
    li.innerHTML = ['<div class="img-wrap"> <span class="close">&times;</span>' +
        '<img class="thumb" src="', e.target.result, '" title="', escape(readerEvt.name), '" data-id="',
    readerEvt.name, '"/>' + '</div>'].join('');

    var div = document.createElement('div');
    div.className = "FileNameCaptionStyle";
    li.appendChild(div);
    div.innerHTML = [readerEvt.name].join('');
    document.getElementById('Filelist').insertBefore(ul, null);
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