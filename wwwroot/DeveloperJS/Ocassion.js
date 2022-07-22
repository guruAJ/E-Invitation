
var memberTable;
$(document).ready(function () {
    //setTimeout(function () {
    //    Datatable();
    //}, 1000);
    Datatable();
    $("#btnAddNew").click(function () {
        ResetData();
      
        $("#AddNew").modal("show");
    });
    $("#autofill").click(function () {
        $("#OcassionName").val("Republic Day");
        $("#OcassionDate").html("26|01|2021");
        $("#ChiefName").val("Manoj Pande");
        $("#Venue").val("Rajpath,Delhi ");
        $("#Time").val("1030 h (To be Seated by 0950h)");
        $("#Dress").val("Dress No-1, Winter Ceremonial");
        $("#Dress1").val("National Dress / Lounge Suit");
        $("#ContactName").val("RSVP");
        $("#IssueBranch").val("AG/CW-1");
        $("#PhoneNo").val("8750278220");
        $("#ASCON").val("35140");
    });
    if ($("#editid").html() > 0) {

       
        $("#AddNew").modal("show");
    }
    $('#btnReset').click(function () {
        ResetData();
    });
    $('body').on('click', '.PassIsuue', function () {
        $("#AddPassIsuue").modal("show");
        GetAllData($(this).closest("tr").find(".spnid").html());
    });
    $('body').on('click', '.UnLock', function () {

      /*  alert($(this).closest("tr").find(".spnid").html());*/

        Swal.fire({
            title: 'Are You Sure You want to Generate Pass?',
            text: "You Can't make any changes after generated pass!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#072697',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.value) {
                Lock($(this).closest("tr").find(".spnid").html(),1)
            }
        });

    });
    $('body').on('click', '.Locked', function () {

        //alert($(this).closest("tr").find(".spnid").html());

        Swal.fire({
            title: 'Are You Sure You want to Rollback Generated Pass?',
            text: "You make Any changes after Rollback Generated Pass!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#072697',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.value) {

                Lock($(this).closest("tr").find(".spnid").html(), 0)
            }
        });

    });




    $('body').on('click', '.btn-sm', function () {

        /*  alert($(this).closest("tr").find(".spnid").html());*/

        Swal.fire({
            title: 'Fill the Details Carefully !',
            text: "THIS DETAIL  WILL  BE  USED  FOR   E-INVITATION CARD PRINTING",
            icon: 'warning',
            showCancelButton: false,
            confirmButtonColor: '#072697',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            //if (result.value) {
            //    Lock($(this).closest("tr").find(".spnid").html(), 3)
            //}
        });

    });




    $('body').on('click', '.Runing', function () {

        /*  alert($(this).closest("tr").find(".spnid").html());*/

        Swal.fire({
            title: 'Are you sure?',
            text: "Do you want to Stop Event !",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#072697',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.value) {
                Lock1($(this).closest("tr").find(".spnid").html(), 3)
            }
        });

    });

    $('body').on('click', '.Finished', function () {

        /*  alert($(this).closest("tr").find(".spnid").html());*/

        Swal.fire({
            title: 'Are you sure?',
            text: "Do you want to Start Event!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#072697',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.value) {
                Lock1($(this).closest("tr").find(".spnid").html(), 4)
            }
        });

    });
});
function ResetData() {
    $("#OcassionName").val("");
    $("#OcassionDate").val("");
    $("#ChiefName").val("");
    $("#Venue").val("");
    $("#Time").val("");
    $("#Dress").val("");
    $("#Dress1").val("");
    $("#ContactName").val("");
    $("#IssueBranch").val("");
    $("#PhoneNo").val("");
    $("#ASCON").val("");
   
}
function Datatable() {
    //  $('#tblDetail').DataTable();
    feather.replace();
    memberTable = $('.tblDetail').DataTable({
        retrieve: false,
        lengthChange: false,
        ordering: false,
        searching: false,
        paging: false,
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

    //memberTable.buttons().container().appendTo('#tblDetail_wrapper .col-md-6:eq(0)');
}

function Lock(Id,Status) {


    $.ajax({
        url: '/Ocassion/SaveStatus',
        type: 'POST',
        data: { "OcassionId": Id, "Status": Status}, //get the search string
        success: function (result) {

            if (result == 1) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Successfully saved',
                    showConfirmButton: false,
                    timer: 1500
                })

                setTimeout(function () { window.location.href = "/PassGeneration"; }, 1500)
            }
            else
                if (result == 0) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Please Upload Card Images!',
                        timer: 1500
                      
                    })

                  
                }
           
        }
    });
}

function Lock1(Id, Status) {


    $.ajax({
        url: '/Ocassion/SaveStatus',
        type: 'POST',
        data: { "OcassionId": Id, "Status": Status }, //get the search string
        success: function (result) {

            if (result == 1) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Successfully saved',
                    showConfirmButton: false,
                    timer: 1500
                })

                setTimeout(function () { window.location.href = "/EventClosing"; }, 1500)
            }


        }
    });
}

function GetAllData(Id) {
    var listItem = "";
   
    $.ajax({
        url: '/GestList/GetGestByOcassionId',
        type: 'POST',
        data: { "OcassionId": Id }, //get the search string
        success: function (result) {
            listItem += "<thead>";
            listItem += "<tr>";
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span></th>";
            //listItem += "<th></th>";
            var vacancieslist = result.vacancies;
            var addGestLists = result.addGestLists;
            var finlized = result.ocassionStatuses;
            var Total = 0;
            var isstatus = "";
            for (var i = 0; i < 1; i++) {
                var color = vacancieslist[i].enclosureColor;
                //listItem += "<th style='background-color:" + color + ";color:white;'>" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "<br>" + vacancieslist[i].total + "</th>";
                /*  $("#Vencancy").html(vacancieslist[i].enclosureName);*/
                $("#Rank").html(vacancieslist[i].rankName + " (" + vacancieslist[i].enclosureName + ")");
                $("#lblTotal").html(vacancieslist[i].total);

                Total = vacancieslist[i].total;

                if (vacancieslist[i].isLock == true || finlized == true)
                    isstatus = "disabled";
                else
                    isstatus = "";
                /*listItem += "<th class='d-none1'>Id</th>";*/
                listItem += "<th class='d-none' style='color:white;width:10px'></th>";
                listItem += "<th style='color:white;'>SN</th>";
                listItem += "<th style='color:white;'>Army No</th>";
                listItem += "<th style='color:white;'>Army Rank</th>";
                listItem += "<th style='color:white;'>Ind Name</th>";
                //listItem += "<th style='color:white;'>Unit Name</th>";
                //listItem += "<th style='color:white;'>Formation</th>";
               // listItem += "<th style='color:white;'>Email address</th>";
                listItem += "<th style='color:white;'>Ph/Mobile</th>";
                listItem += "<th style='color:white;'>Relation</th>";
                listItem += "<th style='color:white;'>Name Of Guest</th>";
                listItem += "<th style='color:white;'>Gender</th>";

               // listItem += "<th style='color:white;'>AadhaarNo</th>";
                listItem += "<th style='color:white;'>Photo</th>";

                listItem += "<th style='color:white;'>Vch Pass</th>";

              

            }
            listItem += "</tr>";
            listItem += "</thead>";
            listItem += "<tbody>";
            var srl = 0;

            for (var z = 0; z < addGestLists.length; z++) {
                srl++;

                listItem += "<tr id='maindata'>";
                listItem += "<td>" + srl + "</td>";
                listItem += "<td class='d-none'> <input type='text' class='form-control form-control-sm spnId' placeholder='Armyno' id='spnId' value='" + addGestLists[z].id + "' /></td>";

                listItem += "<td>" + addGestLists[z].armyNo+"</td>";
                listItem += "<td><span id='RankName" + srl + "' class=' RankName'></span></td>"
                listItem += "<td> " + addGestLists[z].indlName + " </td>";
                //listItem += "<td> <input type='text' class='form-control form-control-sm' " + isstatus + " onClick='this.setSelectionRange(0, this.value.length)' placeholder='Unit Name' id='Unit' data-toggle='tooltip' title='" + addGestLists[z].unit + "' value='" + addGestLists[z].unit + "' /></td>";
                //listItem += "<td> <input type='text' class='form-control form-control-sm' " + isstatus + " onClick='this.setSelectionRange(0, this.value.length)' placeholder='Fmn' id='Fmn' data-toggle='tooltip' title='" + addGestLists[z].fmn + "' value='" + addGestLists[z].fmn + "' /></td>";
               // listItem += "<td>" + addGestLists[z].emailId + "</td>";
                listItem += "<td>" + addGestLists[z].phoneNo + "</td>";

                //listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Relation' id='Relation' value='" + addGestLists[z].relation + "'/></td>";
             
                listItem += "<td>" + addGestLists[z].relation+"</td>"
               

                listItem += "<td>" + addGestLists[z].nameOfGest + "</td>";
                //listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Gender' id='Gender' value='" + addGestLists[z].gender +"'/></td>";
              
                listItem += "<td>" + addGestLists[z].gender+"</td>"


               // listItem += "<td> <input type='Date'  class='form-control form-control-sm dob' " + isstatus + " style='width: 130px' placeholder='Date of birth' id='Dob' value='" + addGestLists[z].dob + "'/></td>";
              //  listItem += "<td> " + addGestLists[z].adhaorNo + "/></td>";
               
                    if (addGestLists[z].photo != "")
                        listItem += "<td><div class='imgUp'><div class='imagePreview' style='background-image: url(/images/Profile/" + addGestLists[z].photo + ")'></div></div><span id='spnimage' class='d-none'>" + addGestLists[z].photo + "</span></div>";
                    else
                        listItem += "<td><div class='imgUp'><div class='imagePreview'></div></div><span id='spnimage' class='d-none'></span></div>";

               

                listItem += "<td>";
                listItem += "<div class='custom-control custom-checkbox small'>";
                if (addGestLists[z].isPass) {

                    listItem += "<input type='checkbox' checked='true' class='custom-control-input checkboxPass' id='" + addGestLists[z].id + "'>";
                    listItem += "<label class='custom-control-label' for='" + addGestLists[z].id + "'></label>";
                }
                else {
                    listItem += "<input type='checkbox' class='custom-control-input checkboxPass' id='" + addGestLists[z].id + "'>";
                    listItem += "<label class='custom-control-label' for='" + addGestLists[z].id + "'></label>";
                }
                listItem += "</div>";
                listItem += "</td>";


                listItem += "</tr>";


                $('#Gender').val(addGestLists[z].gender);
                Total = Total - 1;

                GetAllRank('RankName' + srl, addGestLists[z].indRankId);
            }


            
            listItem += "</tbody>";

            $("#tblData").html(listItem);
            /*$("#lblTotal").html(userlist.length);*/
            $("#tblData").DataTable().destroy();

            feather.replace();
            memberTable = $('#tblData').DataTable({
                retrieve: false,
                lengthChange: false,
                ordering: false,
                searching: false,
                paging: false,
                destroy: true,
            });



        


          

        

            $('body').on('click', '.checkboxPass', function () {

                var IsPass = false;
                var mess = "";
                        if ($(this).closest("tr").find(".checkboxPass").is(":checked")) {
                            IsPass = true;
                            mess = "Veh Pass Include";
                        }
                        else {
                            IsPass = false;
                            mess = "Cancel Veh Pass ";
                        }

                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Do you want be " + mess + "?",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, Save it!'
                        }).then((result) => {
                            if (result.isConfirmed) {

                                var Id = $(this).closest("tr").find("#spnId").val();;
                               

                                savepassdate(Id, IsPass);
                               
                               
                            }
                        })
                    

            });




        }
    });
}
function GetAllRank(Id, rankId) {

    var listItem = "";
   
    $("#RankName").html(listItem);

    $.ajax({
        url: '/Rank/GetAll',
        type: 'POST',
        data: {}, //get the search string
        success: function (response) {

            if (response != "null") {
                if (response == -3) {
                    window.location.href = "/Login";//Session expired
                }
                else if (response == -1) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: errormsg
                    });
                }
                else if (response == 0) {
                    //
                }
                else {

                    for (var i = 0; i < response.length; i++) {
                        if (rankId == response[i].id) {


                            listItem += response[i].title;
                            $("#" + Id).html(listItem);
                        }
                       
                    }

                  

                }
            }
            else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: errormsg001
                });
            }
        },
        error: function (result) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: errormsg002
            });
        }
    });
}
function savepassdate(Id, IsPass) {



    $.ajax({
        url: '/GestList/UpdatePassDataById',
        type: 'POST',
        data: { "Id": Id, "IsPass": IsPass }, //get the search string
        success: function (result) {

            if (result == 3) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Event Locked!',

                })
                setTimeout(function () {
                    location.reload();
                }, 2000);
            }
            if (result == 1) {
                Swal.fire(
                    'Save!',
                    'Your Data has been Save.',
                    'success'
                )
               
            }

        }
    });
}
