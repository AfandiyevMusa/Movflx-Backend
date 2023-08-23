$(document).on('keyup', '#input-search', function () {
    console.log($(this).val());
    let inputVal = $(this).val().trim();
    $("#searchList li").slice(0).remove();
    $.ajax({
        url: "/Common/SearchMovie?search=" + inputVal,
        method: "get",
        success: function (res) {
            $("#searchList").append(res)
        }
    })
});


$(document).on('keyup', '#inputBlog-search', function () {
    console.log($(this).val());
    let inputVal = $(this).val().trim();
    $("#searchBlogList li").slice(0).remove();
    $.ajax({
        url: "/Common/SearchBlog?search=" + inputVal,
        method: "get",
        success: function (res) {
            $("#searchBlogList").append(res)
        }
    })
});
