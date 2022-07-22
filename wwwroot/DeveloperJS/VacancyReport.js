var memberTable;
$(document).ready(function () {
    GetAllInterview($("#OcassionFilterId").val(), $("#UserId").val());

    GetAllOcassionBy($("#OcassionFilterId").val());

    GetAllStatus($("#OcassionFilterId").val())
    CheckStatus($("#OcassionFilterId").val(), $("#UserId").val());
    $("#OcassionFilterId").change(function () {

        GetAllInterview($("#OcassionFilterId").val(), $("#UserId").val());
        CheckStatus($("#OcassionFilterId").val(), $("#UserId").val());
        GetAllOcassionBy($("#OcassionFilterId").val());
        GetAllStatus($("#OcassionFilterId").val())

        $('#UserId').removeClass('d-none');

    });
    $("#UserId").change(function () {

        GetAllInterview($("#OcassionFilterId").val(), $("#UserId").val());
        CheckStatus($("#OcassionFilterId").val(), $("#UserId").val());

    });
    $("#btnAllstatus").click(function () {
      
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be All User Finalise!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Finalise it!'
            }).then((result) => {
                if (result.isConfirmed) {

                    $("#UserId > option").each(function () {
                       // alert(this.text + ' ' + this.value);
                        if (this.value>0)
                        SaveStatus($("#OcassionFilterId").val(), this.value,1);
                    });
                    
                }
            })
    
    });



    $("#btnstatus").click(function () {
        if ($("#UserId").val() > 0) {
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be Finalise!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Finalise it!'
        }).then((result) => {
            if (result.isConfirmed) {
                SaveStatus($("#OcassionFilterId").val(), $("#UserId").val(),0);
            }
        })
    }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Please select Directorate!',

            })
        }
    });
    $("#btnstatus1").click(function () {
        if ($("#UserId").val() > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: "",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Revoke finalise!'
            }).then((result) => {
                if (result.isConfirmed) {
                    SaveStatus($("#OcassionFilterId").val(), $("#UserId").val(),0);
                }
            })
        }
        else {
            Swal.fire({
            icon: 'error',
            title: 'Oops...',
                text: 'Please select Directorate!',

        })
        }

    });
    //$("#btnstatus1").click(function () {
    //    Swal.fire({
    //        icon: 'error',
    //        title: 'Oops...',
    //        text: 'Allready Finalized!',

    //    })
    //});
    //$("#tbDisplayOrder").val("0");
    //$("#btnSubmit").click(function () {
    //    SaveRecord();
    //});
    //$("#btnUpdate").click(function () {
    //    UpdateRecord();
    //});
    //$("#btnReset").click(function () {
    //    resetFormControls();
    //});
    //$("#btnAddNew").click(function () {
    //    resetFormControls();
    //});


});
function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}


function GetAllInterview(Id, UserId) {
    var listItem = "";

    $("#tblData").html(listItem);
    $.ajax({
        url: '/VacancyReport/GetAll',
        type: 'POST',
        data: { "OcassionFilterId": Id, "UserId": UserId }, //get the search string
        success: function (result) {
            listItem += "<thead>";
            listItem += "<tr>";
            
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span></th>";
            //listItem += "<th></th>";
            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancieslistGest = result.vacancieslistGest;
            listItem += "<th class='text-center'>Enclosure Name</th>";
            listItem += "<th class='text-center'>Category</th>";
            listItem += "<th class='text-center'>Total Alloted</th>";
            listItem += "<th class='text-center'>Total Reserved</th>";
            listItem += "<th class='text-center'>Available Seat</th>";
          /*  listItem += "<th>Action</th>";*/
            listItem += "</tr>";
            listItem += "</thead>";
            listItem += "<tbody>";

            for (var i = 0; i < vacancieslist.length; i++) {
                listItem += "<tr>";
                var color = vacancieslist[i].enclosureColor;
                //style = 'background-color:" + color + ";color:white;'
                listItem += "<td class='text-center'>" + vacancieslist[i].enclosureName + "</td>";
                listItem += "<td class='text-center'>" + vacancieslist[i].rankName+"</td>";
                listItem += "<td class='text-center'>" + vacancieslist[i].total + "</td>";
                var count = 0;
                for (var x = 0; x < vacancieslistGest.length; x++) {

                    if (vacancieslist[i].ocassionId == vacancieslistGest[x].ocassionId && vacancieslist[i].enclosureId == vacancieslistGest[x].enclosureId && vacancieslist[i].rankId == vacancieslistGest[x].rankId)
                        count = count + 1;
                    
                }
                listItem += "<td class='text-center'>" + count + "</td>";
                var avi = 0;
                avi = vacancieslist[i].total - count;
                listItem += "<td class='text-center'>" + avi + "</td>";
               /* listItem += "<td><button type='button' id='btnFinalize' class='btn btn-warning'>Finalize</button></td>";*/
                listItem += "</tr>";
            }

          
          
           
            listItem += "</tbody>";

            $("#tblData").html(listItem);
            /*$("#lblTotal").html(userlist.length);*/
           
            feather.replace();
             $('#tblData').DataTable({
                retrieve: false,
                lengthChange: false,
                ordering: false,
                searching: false,
                paging: false,
                destroy: true,
            });

          

          



        }
    });
}


function CheckStatus(Id, UserId) {
   
    $.ajax({
        url: '/VacancyReport/GetStatus',
        type: 'POST',
        data: { "OcassionFilterId": Id, "UserId": UserId }, //get the search string
        success: function (result) {
           
            if (!result) {
                //  $('#vstatus').html('<button type="button" id="btnstatus" class="btn btn-warning">Finalize</button>');
                $('#btnstatus').removeClass('d-none');
                $('#btnstatus1').addClass('d-none');
            }
            else {
                $('#btnstatus').addClass('d-none');
                $('#btnstatus1').removeClass('d-none');
              ////  $('#vstatus').html('<button type="button" class="btn btn-danger ">Finalized</button>');
            }
           // GetAllStatus($("#OcassionFilterId").val());
        }
    });
}
function GetAllStatus(Id) {
    $.ajax({
        url: '/VacancyReport/GetAllStatus',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
        success: function (response) {

            if (response == 1) {

                $("#btnAllstatus").addClass('d-none');
               
            }
            else if (response == 2) {
                $("#btnAllstatus").removeClass('d-none');
            }
            GetAllInterview($("#OcassionFilterId").val(), $("#UserId").val());
           
        }
    });
}
function SaveStatus(Id, UserId,status) {
    $.ajax({
        url: '/VacancyReport/SaveStatus',
        type: 'POST',
        data: { "OcassionFilterId": Id, "UserId": UserId, "status": status }, //get the search string
        success: function (response) {

            if (response == 2) {
                Swal.fire(
                    'Alert!',
                    'successfully!',
                    'success'
                )
              
               
            }
            else if (response == 3) {
                //Swal.fire({
                //    icon: 'error',
                //    title: 'Oops...',
                //    text: 'AllreadyFinalised!',
                   
                //})
            }
            GetAllInterview($("#OcassionFilterId").val(), $("#UserId").val());
            CheckStatus($("#OcassionFilterId").val(), $("#UserId").val());
            GetAllStatus($("#OcassionFilterId").val());
        }
    });
}
function GetAllOcassionBy(Id) {
    var listItem = "";
    listItem = "<option value='0'>-- Select One --</option>";
    $("#ddlFileNo").html(listItem);
   
    $.ajax({
        url: '/User/GetAllOcassionBy',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
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
                        listItem += '<option value="' + response[i].id + '">' + response[i].userName + '</option>';
                    }
                    $("#UserId").html(listItem);
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