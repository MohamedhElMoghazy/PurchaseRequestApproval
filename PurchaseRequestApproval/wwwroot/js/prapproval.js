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

                { "data": "prApprovalId", "width ": "10%" },
                { "data": "prApprovalTitle", "width ": "10%" },
                { "data": "prApprovalDescription", "width ": "15%" },
                { "data": "workOrder", "width ": "10%" },
                { "data": "vendor.vendorName", "width ": "10%" },
                { "data": "purchaseType.purcahseTypeName", "width ": "10%" },
                { "data": "employee.employeeName", "width ": "10%" },
                { "data": "project.projectName", "width ": "10%" },




                {
                    "data": "id",
                    "render": function (data) {
                        return `

                            <div class="text-center">

                              <a href="/Employee/PRAQuote/CheckPRARev?PRIDRecords=${data}" class="btn btn-primary text-white" style="cursor:pointer">
                                <i class="fas fa-plus-circle"></i>  
                                </a>
                            

                                <a href="/Employee/PRApproval/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Employee/PRApproval/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                    }, "width": "15%"
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
