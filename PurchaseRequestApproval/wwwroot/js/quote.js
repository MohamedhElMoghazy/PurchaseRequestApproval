﻿var dataTable;
 
$(document).ready(function () {
    loadDataTable();
});


    function loadDataTable() {
        dataTable = $('#tblData').DataTable({
            "ajax": {
                "url": "/Employee/Quote/GetAll"
            },

            "columns": [

                { "data": "quoteDescription", "width ": "15%" },
                { "data": "quoteAmount", "width ": "15%" },
                { "data": "etaDays", "width ": "15%" },
                { "data": "quoteDate", "width ": "15%" },
                { "data": "vendor.vendorName", "width ": "15%" },
                { "data": "shipping.shippingName", "width ": "15%" },




                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="text-center">
                                <a href="/Employee/Quote/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Employee/Quote/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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
