
var errormsg = "Error.Please use after some time or contact to Admin.";
var errormsg001 = "Error 001. Please use after some time or contact to Admin.";
var errormsg002 = "Error 002. Please use after some time or contact to Admin.";
var memberTable;
var Tot = 0;
var lol = 0;

$(document).ready(function () {

    if ($('#OcassionFilterId').val() > 0) {
       
        $("#btnAddNew").removeClass('d-none');
        if ($("#islock").html() == 1) {
            $("#btnAddNew").addClass('d-none');
        }
        else {
            $("#btnAddNew").removeClass('d-none');
        }
    }
    else {
        $("#btnAddNew").addClass('d-none');
        
    }


    $("#CategoryId").change(function () {

       
        $("#RankDesc").val($("#CategoryId option:selected").text());
    });
    Datatable();
    //$("#OcassionFilterId").addClass("form-control").val($("#drop").html());
    if ($("#drop").html()>0)
        $("#OcassionFilterId").addClass("form-control").val($("#drop").html());
    else
        $("#OcassionFilterId").addClass("form-control").val();
   
    $("#btnAddNew").click(function () {
        ResetData();

        $("#AddNew").modal("show");

        if ($('#OcassionFilterId').val() > 0) {
            $('#OcassionId').val($('#OcassionFilterId').val())
        }
    });

    if ($("#editid").html() > 0) {


        $("#AddNew").modal("show");
    }
    $('#btnReset').click(function () {
        ResetData();
    });
    /*BindDetail();*/
    //$("#OcassionId").change(function () {
    //    if ($("#OcassionId").val()>0)
    //    $("#OcassionFilterId").val($("#OcassionId").val());


    //});
  
    


    $('body').on('click', '.btnDelete', function () {

        /*  alert($(this).closest("tr").find(".spnid").html());*/

        Swal.fire({
            title: 'Are you sure?',
            text: "Do you want to Delete!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#072697',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Delete it!'
        }).then((result) => {
            if (result.value) {
                //alert($(this).closest("tr").find(".spnid").html());

                DeleteData($(this).closest("tr").find(".spnid").html());
            }
        });

    });

});
function DeleteData(Id) {


    $.ajax({
        url: '/Vacancy/DeleteData',
        type: 'POST',
        data: { "Id": Id }, //get the search string
        success: function (result) {

            if (result == 1) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'success',
                    showConfirmButton: false,
                    timer: 1500
                })

                setTimeout(function () { location.reload(); }, 1500)
            }


        }
    });
}
function ResetData() {
   
    $("#EnclosureId").val("");
    $("#CategoryId").val("");
    $("#RankId").val("");
    $("#Total").val("");
    $("#spnmessage").html("");
}
function Datatable() {
    //  $('#tblDetail').DataTable();

    if ($('#OcassionFilterId').val() > 0) {
        $('#OcassionId').val($('#OcassionFilterId').val())
    }
    if ($('#OcassionId').val() > 0) {
        $('#OcassionFilterId').val($('#OcassionId').val())
    }
    if ($("#islock").html() == 1) {
        $("#btnAddNew").addClass('d-none');
    }
    else {
        $("#btnAddNew").removeClass('d-none');
    }
    feather.replace();
    memberTable = $('#tblDetail').DataTable({
        retrieve: false,
        lengthChange: false,
        ordering: false,
        searching: false,
        "order": [[2, "asc"]],
       
        //buttons: [{
        //    extend: 'copy',
        //    exportOptions: {
        //        columns: "thead th:not(.noExport)"
        //    }
        //}, {
        //    extend: 'excel',
        //    exportOptions: {
        //        columns: "thead th:not(.noExport)"
        //    }
        //}, {
        //    extend: 'pdf',
        //    exportOptions: {
        //        columns: "thead th:not(.noExport)"
        //    }
        //    }]
       
         
    });
   
    //memberTable.buttons().container().appendTo('#tblDetail_wrapper .col-md-6:eq(0)');
}
//function BindDetail() {


//    $.ajax({
//        url: '/Vacancy/GetAll',
//        type: 'POST',
//        data: { "Id": 1 }, //get the search string
//        success: function (response) {
//            console.log(response);
//            if (response) {  // check if data is defined
//                if (response != null) {
//                    if (response == 1) {
                        
//                        setTimeout(function () { window.location.replace("/Vacancy/Index"); }, 1500)

//                    }
//                }
//            }
//        }
//    });
           
       


//    }

//function BindDetail() {
//    var listItem = "";
//    $("#lblTotal").html("0");
//    $("#tblDetail").DataTable().destroy();
//    $("#DataBody").html("");
   
//    $.ajax({
//        url: '/Enclosure/GetAll',
//        type: 'POST',
//        data: { "Id": 1 }, //get the search string
//        success: function (response) {
//            debugger;
//            if (response != "null") {
//                if (response == -3) {
//                    window.location.href = "/Login";//Session expired
//                }
//                else if (response == -1) {
//                    $("#error-msg").removeClass("d-none");
//                    $("#error-msg").html(errormsg);
//                }
//                else if (response == 0) {
//                    $("#error-msg").addClass("d-none");
//                }
//                else {
//                    $("#error-msg").addClass("d-none");

//                    var cou = 1;
//                    //$("#tblDetail").DataTable().destroy();
//                    for (var i = 0; i < response.length; i++) {
//                        // alert(response[i].CourseId);
//                        listItem += "<tr>";
//                        listItem += "<td class='d-none'><span class='d-none' id='spnGId'>" + response[i].Id + "</span></td>";
//                        listItem += "<td>";
//                        listItem += "<div class='custom-control custom-checkbox small'>";
//                        listItem += "<input type='checkbox' class='custom-control-input' id='" + response[i].Id + "'>";
//                        listItem += "<label class='custom-control-label' for='" + response[i].Id + "'></label>";
//                        listItem += "</div>";
//                        listItem += "</td>";
//                        listItem += "<td>" + cou + "</td>";
//                        /*listItem += "<td>1</td>";*/
//                        listItem += "<td>" + response[i].title + "</td>";
//                        listItem += "<td> <select class='form-control ddlCatogy' name='ddlCatogy'></select></td>";
//                        listItem += "<td> <select class='form-control ddlRank' name='ddlRank'></select></td>";
//                        listItem += "<td><span id=''><input type='text' id='Total' class='form-control' /></span></td>";
//                        //listItem += "<td><span id='spnGMobile'>" + response[i].Mobile + "</span></td>";
//                        //listItem += "<td><span id='spnGEmailId'>" + response[i].EmailId + "</span></td>";
//                        //listItem += "<td><span id='spnGEmailId'>" + response[i].ChiefName + "</span></td>";
//                        listItem += "<td class='nowrap'><a class='btn btn-outline-cyan mr-1 btn-icon btn-sm'>Save</a></td>";
//                        listItem += "</tr>";
//                        cou++;
//                    }

//                    $("#DataBody").html(listItem);
//                    //$("#lblTotal").html($('#DataBody tr').length);
//                    //feather.replace();

//                    GetCatogy();
//                    GetRank();
                  
//                    //memberTable = $('#tblDetail').DataTable({
//                    //    retrieve: true,
//                    //    lengthChange: false,
//                    //    "order": [[2, "asc"]],
//                    //    buttons: [{
//                    //        extend: 'copy',
//                    //        exportOptions: {
//                    //            columns: "thead th:not(.noExport)"
//                    //        }
//                    //    }, {
//                    //        extend: 'excel',
//                    //        exportOptions: {
//                    //            columns: "thead th:not(.noExport)"
//                    //        }
//                    //    }, {
//                    //        extend: 'pdf',
//                    //        extend: 'pdfHtml5',
//                    //        orientation: 'landscape',
//                    //        pageSize: 'LEGAL',
//                    //        exportOptions: {
//                    //            columns: "thead th:not(.noExport)"
//                    //        }
//                    //    }]
//                    //});

//                    //memberTable.buttons().container().appendTo('#tblDetail_wrapper .col-md-6:eq(0)');

//                    //var rows;
//                    //$("#tblDetail #chkAll").click(function () {
//                    //    if ($(this).is(':checked')) {
//                    //        rows = memberTable.rows({ 'search': 'applied' }).nodes();
//                    //        $('input[type="checkbox"]', rows).prop('checked', this.checked);
//                    //    }
//                    //    else {
//                    //        rows = memberTable.rows({ 'search': 'applied' }).nodes();
//                    //        $('input[type="checkbox"]', rows).prop('checked', this.checked);
//                    //    }
//                    //});
//                    //$('#DetailBody').on('change', 'input[type="checkbox"]', function () {
//                    //    if (!this.checked) {
//                    //        var el = $('#chkAll').get(0);
//                    //        if (el && el.checked && ('indeterminate' in el)) {
//                    //            el.indeterminate = true;
//                    //        }
//                    //    }
//                    //});


//                    //$("body").on("click", "#btnDel", function () {
//                    //    Swal.fire({
//                    //        title: 'Are you sure?',
//                    //        text: "You want to Delete thesis.",
//                    //        icon: 'warning',
//                    //        showCancelButton: true,
//                    //        confirmButtonColor: '#072697',
//                    //        cancelButtonColor: '#d33',
//                    //        confirmButtonText: 'Yes, Delete this!'
//                    //    }).then((result) => {
//                    //        if (result.value) {
//                    //            var _a = $(this).closest("tr").find("#spnGId").html();
//                    //            DelThesis(_a);
//                    //        }
//                    //    });
//                    //});

//                    //$("body").on("click", "#btnConfirm", function () {
//                    //    Swal.fire({
//                    //        title: 'Are you sure?',
//                    //        text: "You want to send to Examination section.",
//                    //        icon: 'warning',
//                    //        showCancelButton: true,
//                    //        confirmButtonColor: '#072697',
//                    //        cancelButtonColor: '#d33',
//                    //        confirmButtonText: 'Yes, send this!'
//                    //    }).then((result) => {
//                    //        if (result.value) {
//                    //            lol = 0;
//                    //            Tot = 1;
//                    //            var _a = $(this).closest("tr").find("#spnGId").html();
//                    //            ConThesis(_a);
//                    //        }
//                    //    });
//                    //});
//                }
//            }
//            else {


//                //Swal.fire({
//                //    icon: 'error',
//                //    title: 'Oops...',
//                //    text: errormsg001
//                //});
//                $("#error-msg").removeClass("d-none");
//                $("#error-msg").html(errormsg001);
//            }
//        },
//        error: function (result) {
//            //Swal.fire({
//            //    icon: 'error',
//            //    title: 'Oops...',
//            //    text: errormsg002
//            //});
//            $("#error-msg").removeClass("d-none");
//            $("#error-msg").html(errormsg002);
//        }
//    });
//}

//function GetCatogy() {
//    var listItem = "";
  
//    listItem = "<option value=''>-- All Category --</option>";
//    $(".ddlCatogy").html(listItem);

//    $.ajax({
//        url: '/Category/GetAll',
//        type: 'POST',
//        data: { "Id": 1 }, //get the search string
//        success: function (response) {
//            if (response != "null") {
//                if (response == -3) {
//                    window.location.href = "/Login";//Session expired
//                }
//                else if (response == -1) {
//                    Swal.fire({
//                        icon: 'error',
//                        title: 'Oops...',
//                        text: errormsg
//                    });
//                }
//                else if (response == 0) {
//                    //
//                }
//                else {
//                    for (var i = 0; i < response.length; i++) {
//                        //if (Id == response[i].UId) {
//                        //    listItem += '<option value="' + response[i].Id + '" selected>' + response[i].title + '</option>';
//                        //}
//                        //else
//                            listItem += '<option value="' + response[i].id + '">' + response[i].title + '</option>';
//                    }
//                    $(".ddlCatogy").html(listItem);
                    
//                }
//            }
//            else {
//                Swal.fire({
//                    icon: 'error',
//                    title: 'Oops...',
//                    text: errormsg001
//                });
//            }
//        },
//        error: function (result) {
//            Swal.fire({
//                icon: 'error',
//                title: 'Oops...',
//                text: errormsg002
//            });
//        }
//    });
//}
//function GetRank() {
//    var listItem = "";

//    listItem = "<option value=''>-- All Rank --</option>";
//    $(".ddlRank").html(listItem);

//    $.ajax({
//        url: '/Rank/GetAll',
//        type: 'POST',
//        data: { "Id": 1 }, //get the search string
//        success: function (response) {
//            if (response != "null") {
//                if (response == -3) {
//                    window.location.href = "/Login";//Session expired
//                }
//                else if (response == -1) {
//                    Swal.fire({
//                        icon: 'error',
//                        title: 'Oops...',
//                        text: errormsg
//                    });
//                }
//                else if (response == 0) {
//                    //
//                }
//                else {
//                    for (var i = 0; i < response.length; i++) {
//                        //if (Id == response[i].UId) {
//                        //    listItem += '<option value="' + response[i].Id + '" selected>' + response[i].title + '</option>';
//                        //}
//                        //else
//                        listItem += '<option value="' + response[i].id + '">' + response[i].title + '</option>';
//                    }
//                    $(".ddlRank").html(listItem);

//                }
//            }
//            else {
//                Swal.fire({
//                    icon: 'error',
//                    title: 'Oops...',
//                    text: errormsg001
//                });
//            }
//        },
//        error: function (result) {
//            Swal.fire({
//                icon: 'error',
//                title: 'Oops...',
//                text: errormsg002
//            });
//        }
//    });
//}