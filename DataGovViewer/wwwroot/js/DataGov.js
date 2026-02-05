$(function () {
    $('#dataTable').DataTable({
        data: dataList,
        columns: columnsList.map(c => ({ data: c, title: c })),
        dom: '<"d-flex justify-content-between align-items-center mb-2"f l>t<"d-flex justify-content-between mt-2"i p>',
        pageLength: 5,
        lengthMenu: [5, 10, 25, 50, 100],
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.8/i18n/lv.json'
        }
    });
});