var dataTable;
 
$(document).ready(function () {
    loadDataTable();
});


    function loadDataTable() {
        dataTable = $('#tblData').DataTable({
            "ajax": {
                "url": "/Admin/PRApproval/GetAll"
            },

            "columns": [

                { "data": "pRApprovald", "width ": "11.25%" },
                { "data": "pRApprovalTitle", "width ": "11.25%" },
                { "data": "pRApprovalDescription", "width ": "11.25%" },
                { "data": "workOrder", "width ": "11.25%" },
                { "data": "vendor.VendorName", "width ": "11.25%" },
                { "data": "purchaseType.PurcahseTypeName", "width ": "11.25%" },
                { "data": "employee.EmployeeName", "width ": "11.25%" },
                { "data": "project.projectName", "width ": "11.25%" },




                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/PRApproval/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/PRApproval/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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
