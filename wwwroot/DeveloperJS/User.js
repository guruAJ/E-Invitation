var memberTable;
$(document).ready(function () {
    //setTimeout(function () {
    //    Datatable();
    //}, 1000);

    /* Datatable();*/

    $("#btnAddNew").click(function () {
        ResetData();
      
        $("#AddNew").modal("show");
    });

    if ($("#editid").html() > 0) {

        $("#AddNew").modal("show");
    }
    $('#btnReset').click(function () {
        ResetData();
    });
    $('body').on('click', '.btnadmin', function () {

        /*  alert($(this).closest("tr").find(".spnid").html());*/
        Swal.fire({
            title: 'Are you sure?',
            text: "You won make admin!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, make it!'
        }).then((result) => {
            if (result.isConfirmed) {
                MakeAdmin($(this).closest("tr").find(".spnid").html());
            }
        })

        

    });

    $('body').on('click', '.btnremAdmin', function () {

        /*  alert($(this).closest("tr").find(".spnid").html());*/
        Swal.fire({
            title: 'Are you sure?',
            text: "You wanna remove admin!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, remove it!'
        }).then((result) => {
            if (result.isConfirmed) {
                RemoveAdmin($(this).closest("tr").find(".spnid").html());
            }
        })



    });

});

function RemoveAdmin(UserId) {

    debugger;
    $.ajax({
        url: '/User/RemoveAdmin',
        type: 'POST',
        data: { "UserId": UserId }, //get the search string
        success: function (response) {

            if (response != "null") {
                if (response == -3) {
                    window.location.href = "/UserLogin";//Session expired
                }


                else {
                    Swal.fire(
                        'Success',
                        '',
                        'success'
                    )
                    location.reload();
                }
            }

        },
        error: function (result) {

        }
    });
}

function MakeAdmin(UserId) {

    debugger;
    $.ajax({
        url: '/User/MakeAdmin',
        type: 'POST',
        data: { "UserId": UserId}, //get the search string
        success: function (response) {

            if (response != "null") {
                if (response == -3) {
                    window.location.href = "/UserLogin";//Session expired
                }


                else {
                    Swal.fire(
                        'Success',
                        '',
                        'success'
                    )
                    location.reload();
                }
            }

        },
        error: function (result) {

        }
    });
}

function ResetData() {
    $("#UserName").val("");
    $("#Password").val("");
    $("#ConfirmPassword").val("");
    $("#spnmessage").html("");

}
function Datatable() {
  
    feather.replace();
    memberTable = $('#tblDetail').DataTable({
        retrieve: true,
        lengthChange: true,
        ordering: false,
        "order": [[2, "asc"]],
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

 //memberTable.buttons().container().appendTo('#tblDetail_wrapper .col-md-6:eq(0)');

