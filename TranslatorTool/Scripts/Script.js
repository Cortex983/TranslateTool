$('document').ready(function () {

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    $('input').keyup(function () {
        delay(function () {
            //
            var _text = $('#txtInput').val();
            $('#Id').val("");

            $.ajax({
                type: "POST",
                cache: false,
                url: "Home/TranslateText",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify({
                    text: _text
                })
            })
                .done(function (data) {
                    $('#forTranslate').html("");
                    $('#forTranslate').append(data.translateTxt);
                    $('#txtInput').val("");
                    $('#Id').val(data.Id);

                   

                    
                })
                .fail(function (jqXHR, textStatus, err) {
                    //if (textStatus == "parsererror") {
                    //    window.location.reload();
                    //} else {
                    //    bootbox.alert("Greška: " + err + " - [" + textStatus + "]");
                    //}
                });




        }, 3000);
    });


});