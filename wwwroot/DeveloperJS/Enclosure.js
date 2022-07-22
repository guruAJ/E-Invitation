
var memberTable;
$(document).ready(function () {
    //setTimeout(function () {
    //    Datatable();
    //}, 1000);




  /*  Datatable();*/
    $("#btnAddNew").click(function () {

        ResetData();
        $("#addModalTitle").html("Add New Enclosure ");
        $("#AddNew").modal("show");
    });

    if ($("#editid").html() > 0) {

        $("#addModalTitle").html("Add New Enclosure ");
        $("#AddNew").modal("show");
    }
    $('#btnReset').click(function () {
        ResetData();
    });
});
function ResetData() {
    $("#Title").val("");
    $("#spnmessage").html("");
    }


function Datatable() {
    //  $('#tblDetail').DataTable();
    feather.replace();
    memberTable = $('#tblDetail').DataTable({
        retrieve: true,
        lengthChange: true,
        ordering: false,
        "order": [[1, "asc"]],
        buttons: [{
            extend: 'copy',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }, {
            extend: 'excel',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }, {
            extend: 'pdf',
            exportOptions: {
                columns: "thead th:not(.noExport)"
            }
        }]
    });


}

function myFunction(Id) {


    Swal.fire({
        title: 'Are you sure?',
        text: "Do you want to delete!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, save it!'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: '/Enclosure/Delete',
                type: 'POST',
                data: { "Id": Id }, //get the search string
                success: function (response) {
                    console.log(response);
                    if (response) {  // check if data is defined
                        if (response != null) {
                            if (response == 1) {
                                Swal.fire({
                                    position: 'top-end',
                                    icon: 'success',
                                    title: 'Record Deleted successfully',
                                    showConfirmButton: false,
                                    timer: 1500
                                })
                                setTimeout(function () { window.location.replace("/Enclosure/Index"); }, 1500)

                            }
                        }
                    }
                }
            });
        }
    })



}