var dataTable;
 
$(document).ready(function () {
    loadDataTable();
});


    function loadDataTable() {
        dataTable = $('#tblData').DataTable({
            "ajax": {
                "url": "/Admin/Employee/GetAll"
            },

            "columns": [

                { "data": "employeeName", "width ": "15%" },
                { "data": "employeeCode", "width ": "15%" },
                { "data": "employeePosition", "width ": "15%" },
                { "data": "employeePhone", "width ": "15%" },
                { "data": "employeeEmail", "width ": "15%" },
                { "data": "employeeSite", "width ": "15%" },




                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            
                            <div class="text-center">
                                <a href="/Admin/Employee/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/Employee/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
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
