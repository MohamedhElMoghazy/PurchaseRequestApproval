var dataTable;
 
$(document).ready(function () {
    loadDataTable();
});


    function loadDataTable() {
        dataTable = $('#tblData').DataTable({
            "ajax": {
                "url": "/Admin/PurchaseType/GetAll"
            },

            "columns": [
                { "data": "purcahseTypeName", "width ": "30%" },
                { "data": "purcahseCode", "width ": "30%" },
                {
                    "data": "id",
                    "render": function (data) {
                        return `
                            <div class="text-center">
                                <a href="/Admin/PurchaseType/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="fas fa-edit"></i> 
                                </a>
                                <a onclick=Delete("/Admin/PurchaseType/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="fas fa-trash-alt"></i> 
                                </a>
                            </div>
                           `;
                    }, "width": "40%"
                }
            ]
        });
    }