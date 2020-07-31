$(document).ready(function () {

    $.ajaxSetup({ cache: false });

    $(".createCategoryDialog").on("click", function (e) {
        e.preventDefault();
        let url = $(this).attr("href");
        let buttons = {
            "Create": function () {
                $.ajax({
                    url: url,
                    type: "POST",
                    data: $('form').serialize(),
                    datatype: "json",
                    beforeSend: function (xhr) {//<--- This is important
                        xhr.setRequestHeader("RequestVerificationToken",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    success: function (result) {
                        $("#dialogContent").html(result);
                    }
                });
            }
        }
        dialogWindow(this, buttons);
    });

    $(".editCategoryDialog").on("click", function (e) {
        e.preventDefault();
        let buttons = {
            "Edit": function () {
                $.ajax({
                    url: "/Category/EditCategory",
                    type: "POST",
                    data: $('.categoryEditForm').serialize(),
                    datatype: "json",
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader("RequestVerificationToken",
                            $('input:hidden[name="__RequestVerificationToken"]').val());
                    },
                    success: function (result) {
                        $("#dialogContent").html(result);
                    }
                });
            }
        }
        dialogWindow(this, buttons);
    });

    $(".previewImgFile").on("change", function (e) {
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $("#preview").prop("src", e.target.result)
            };
            reader.readAsDataURL(this.files[0]);
        }
    });
});

function dialogWindow(obj, buttons) {
    $("<div id='dialogContent'></div>")
        .addClass("dialog")
        .appendTo("body")
        .load(obj.href)
        .dialog({
            title: $(obj).attr("data-dialog-title"),
            close: function () { $(obj).remove() },
            modal: true,
            buttons: buttons
        });
}
