$(document).ready(function () {
    console.log("hello");
    getSeasonByFilmId();
})
$("#filmId").change(function () {
    getSeasonByFilmId();
})

var getSeasonByFilmId = function () {
    $.ajax({
        url: "/admin/Episode/GetSeasonByFilmId",
        type: 'Get',
        data: {
            filmId: $('#filmId').val(),
        },
        success: function (data) {
            $('#seasonId').find('option').remove()
            $(data).each(
                function (index, item) {
                    $('#seasonId').append(`<option value="${item.id}">${item.name}</option>`)
                }
            );
        }
    })
}