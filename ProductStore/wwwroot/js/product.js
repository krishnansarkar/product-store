$(function () {
    loadDataTable();
})

function confirmDelete(url) {
    swal.fire({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this product!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes, delete it!",
    }).then((result) => {
        if(result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if(data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    })
}

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
                            <a onClick=confirmDelete("/admin/product/delete?id=`+data+`") class="btn btn-danger">
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>`
            }
        ]
    });
}
