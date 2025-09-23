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
            { data: 'category.id'}
        ]
    });
}
