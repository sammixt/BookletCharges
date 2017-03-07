$(document).ready(function () {
    setInterval(function () {
        $.ajax({
            url: '/HssaDetails/countReq',
            contentType: 'application/html; charset=utf-8',
            type: 'GET',
            dataType: 'html'

        })
        .success(function (result) {
            $('#count').text(result);
        })
        .error(function (xhr, status) {
            $('#count').text('0');
        })
    },5000)
})
