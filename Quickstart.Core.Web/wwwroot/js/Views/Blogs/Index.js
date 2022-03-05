$(document).ready(function () {
    getBlogs();

    //load dropdownlist ddlBlogs
    getBlogsSelect();
});

$("#create").click(function () {
    $("#modalBlogs .modal-body", this).empty();
    $("#modalBlogs .modal-body").load("/Blogs/Create");
    $("#modalBlogs").modal("show");
});

function getBlogs() {

    var $row = $('#rowBlogs');
    $('#divBlogs').remove();
    var $div = $('<div></div>');
    $div.addClass('table-responsive mb-5');
    $div.attr({ id: 'divBlogs' });
    $div.addClass('divBlogs mb-5');
    var $table = $("<table></table>");
    $table.addClass('table table-bordered');
    $table.attr({ id: 'tableBlogs', width: '100%' });
    $div.append($table);
    $row.append($div);

    $.get("/Blogs/IndexJson", function (data) {
        $.notify("Load data", "info");

        if (data.isSuccessful) {
            $('#tableBlogs').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'excel', 'pdf'
                ],
                data: data.data,
                columns: [
                    { title: 'Id', data: "id" },
                    { title: 'Name', data: "name" },
                    { title: 'Actions', data: null },
                ],
                columnDefs: [
                    {
                        targets: 2,
                        render: function (data, type, row) {
                            return '<a href="javascript:void" class="btn btn-warning edit">Edit</a>' +
                                '<a href="javascript:void" class="btn btn-danger delete">Delete</a>';
                        }
                    }
                ]
            });

            $('#tableBlogs').on('click', 'tbody a.edit', function (data) {
                let id = $(this).parent().siblings('td')[0].innerText;
                $("#modalBlogs .modal-body", this).empty();
                $("#modalBlogs .modal-body").load("/Blogs/Edit/" + id);
                $("#modalBlogs").modal("show");
            });

            $('#tableBlogs').on('click', 'tbody a.delete', function (data) {
                console.log("Here");
                let id = $(this).parent().siblings('td')[0].innerText;
                deleteBlogs(id);                
            });
        } else {
            swal("Notification", data.message, "error");
        }
    }).fail(function (data) {
        console.table(data);
    });
}

function deleteBlogs(id) {
    swal({
        title: "Are you sure?",
        text: "You will not be able to recover this register!",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes, delete it!",
        cancelButtonText: "No, cancel plx!",
        closeOnConfirm: false,
        closeOnCancel: false
    },
        function (isConfirm) {
            if (isConfirm) {

                $.get("/Blogs/DeleteJson/" + id, function (data) {
                    if (data.isSuccessful) {
                        getBlogs();
                        swal("Deleted!", "Your register has been deleted.", "success");
                    }
                }).fail(function (data) {
                    swal("Notification", data.message, "error");
                });
            } else {
                swal("Cancelled", "Your register is safe :)", "error");
            }
        });
}

function getBlogsSelect() {
    $.get("/Blogs/GetBlogsSelect", function (data) {
        $("#ddlBlogs").empty();
        $("#ddlBlogs").select2({
            placeholder: "Seleccione",
            data: JSON.parse(data.data)
        });
        $("#ddlBlogs").val("").trigger("change");
    });
}