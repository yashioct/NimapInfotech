var table = null;
$(document).ready(function () {
    table = $('#example').DataTable({
        "processing": true,
        "serverSide": true,
        "ajax": {
            "url": "/Category/LoadCategoryMaster",
            "type": "POST"
        },
        "columns": [
            { "data": "id", title: "Category ID", name: "ID" },
            { "data": "name", title: "Category Name", name: "Name" },
            {
                "data": "id", title: "Action", name: "Action", "render":
                    function (data, type, full, meta) {
                        return "<button class='btnUpdate btn btn-secondary'>Update</button> <button class=' btn btn-danger btnDelete'>Delete</button>"
                    }
            },
        ],
    });
});

$(document).on("click", ".btnDelete", function () {
    var data = table.row($(this).parents('tr')).data();
    console.log(data);
    $('#myModal').modal('show');
});

$(document).on("click", ".btnUpdate", function () {
    var data = table.row($(this).parents('tr')).data();
    console.log(data);
    $.ajax({
        type: "POST",
        url: "/Category/AddCategory",
        data: { Id: data.id, Name: data.name },
        cache: false,
        success: function (data) {
            $("#myModal").html(data);
            $('#myModal').modal('show');
        }
    });
});

$(document).on("click", "#btnCreate", function () {
    $.ajax({
        type: "POST",
        url: "/Category/AddCategory",
        data: { Id: 0, Name: '' },
        cache: false,
        success: function (data) {
            $("#myModal").html(data);
            $('#myModal').modal('show');
        }
    });
});