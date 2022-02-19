$("#edit-save").click(function () {
    EditBlog();
});

function EditBlog() {

    let model = $("#blogEditForm").serialize();

    $.post("/Blogs/EditJson", model).done(function (data) {
        if (data.isSuccessful) {
            getBlogs();
            $("#modalBlogs").modal("hide");
        } else {
            console.table(data.message);
        }
    }).fail(function (data) {
        console.table(data);
    });
}