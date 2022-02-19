$("#create-save").click(function () {
    CreateBlog();
});

function CreateBlog() {

    //var model = { "Name": $("#Name").val() };
    let model = $("#blogCreateForm").serialize();

    $.post("/Blogs/CreateJson", model).done(function (data) {
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