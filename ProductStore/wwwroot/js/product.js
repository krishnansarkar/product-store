$(function () {
    loadDataTable();
})

//$(document).ready(function () {
//    loadDataTable();
//})

function loadDataTable() {
    dataTable = $('#productTable').DataTable({
        ajax: { url: '/admin/product/getall' },
        columns: [
            { data: 'title', width: "15%" },
            { data: 'isbn' },
            { data: 'author' },
            { data: 'price' },
            { data: 'category.name' },
            {
                data: 'id',
                render: (data) => `<div class="btn-group w-100" role="group">
                            <a href="/admin/product/upsert/` + data + `" class="btn btn-primary">
                                <i class="bi bi-pencil-fill"></i> Edit
                            </a>
                            <a href="/admin/product/delete/` + data + `" class="btn btn-danger">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>`
            }
        ]
    });
}
