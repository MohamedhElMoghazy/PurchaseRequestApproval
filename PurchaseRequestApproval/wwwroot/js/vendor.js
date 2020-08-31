var dataTable;
 
$(document).ready(function () {
    loadDataTable();
});


    function loadDataTable() {
        dataTable = $('#tblData').DataTable({
            "ajax": {
                "url": "/Admin/Vendor/GetAll"
            },

            "columns": [
                { "data": "vendorName", "width ": "10%" },
                { "data": "vendoreCode", "width ": "10%" },
                { "data": "vendorAddress", "width ": "10%" },
                { "data": "vendorPhone", "width ": "10%" },
                { "data": "salesContactName", "width ": "10%" },
                { "data": "salesContactEmail", "width ": "10%" },
                { "data": "accountContactName", "width ": "10%" },
                { "data": "accContactEmail", "width ": "10%" },
                { "data": "registeredVendor", "width ": "10%" },



                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/Vendor/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/Vendor/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                    }, "width": "10%"
                }
            ]
        });
}
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}
