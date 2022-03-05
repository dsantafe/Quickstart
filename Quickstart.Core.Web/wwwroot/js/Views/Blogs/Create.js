$("#create-save").click(function () {
    createBlog();
});

function createBlog() {

    //var model = { "Name": $("#Name").val() };
    let model = $("#blogCreateForm").serialize();

    $.post("/Blogs/CreateJson", model).done(function (data) {
        if (data.isSuccessful) {
            getBlogs();
            $("#modalBlogs").modal("hide");
        } else {
            swal("Notification", data.message, "error");
        }
    }).fail(function (data) {
        console.table(data);
    });
}