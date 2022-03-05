$("#edit-save").click(function () {
    editBlog();
});

function editBlog() {

    let model = $("#blogEditForm").serialize();

    $.post("/Blogs/EditJson", model).done(function (data) {
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