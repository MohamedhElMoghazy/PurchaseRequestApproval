var dataTable;
 
$(document).ready(function () {
    loadDataTable();
});


    function loadDataTable() {
        dataTable = $('#tblData').DataTable({
            "ajax": {
                "url": "/Employee/PRApproval/GetAll"
            },

            "columns": [

                { "data": "prApprovalId", "width ": "11.25%" },
                { "data": "prApprovalTitle", "width ": "11.25%" },
                { "data": "prApprovalDescription", "width ": "11.25%" },
                { "data": "workOrder", "width ": "11.25%" },
                { "data": "vendor.vendorName", "width ": "11.25%" },
                { "data": "purchaseType.purcahseTypeName", "width ": "11.25%" },
                { "data": "employee.employeeName", "width ": "11.25%" },
                { "data": "project.projectName", "width ": "11.25%" },




                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="text-center">
                                <a href="/Employee/PRApproval/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Employee/PRApproval/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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
