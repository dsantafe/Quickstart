$(document).ready(function () {
    getBlogs();
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
        console.table(data);

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
                            return '<a href="javascript:void" class="btn btn-warning edit">Editar</a>';
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
        } else {
            console.table(data.message);
        }
    }).fail(function (data) {
        console.table(data);
    });
}