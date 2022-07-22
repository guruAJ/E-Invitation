var memberTable;
$(document).ready(function () {

   

    GetAllOcassionBy($("#OcassionFilterId").val());
    GetAllInterview($("#OcassionFilterId").val());
    GetAll($("#OcassionFilterId").val(), $("#UserId").val());

    GetRAll($("#OcassionFilterId").val(), $("#UserId1").html());

    CheckStatus($("#OcassionFilterId").val(), $("#spnUserId").html());
    $("#OcassionFilterId").change(function () {
        GetAllOcassionBy($("#OcassionFilterId").val());
        GetAllInterview($("#OcassionFilterId").val());

        GetAll($("#OcassionFilterId").val(), $("#UserId").val());

        GetRAll($("#OcassionFilterId").val(), $("#UserId1").html());
        CheckStatus($("#OcassionFilterId").val(), $("#spnUserId").html());
    });
    $("#UserId").change(function () {


        GetAll($("#OcassionFilterId").val(), $("#UserId").val());

    });
    $("#btnstatus").click(function () {
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
                SaveStatus($("#OcassionFilterId").val(), $("#spnUserId").html());
            }
        })

    });
    //$("#btnstatus1").click(function () {
    //    Swal.fire({
    //        icon: 'error',
    //        title: 'Oops...',
    //        text: 'Allready Finalised!',

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
    ///GestList/PassGenerateAll"
    $("#Passgen").click(function () {

        //   window.location.href = '/GestList/PassGenerateAll?OcassionId=' + $("#OcassionFilterId").val();


        //  window.open('/GestList/PassGenerateAll?OcassionId=' + $("#OcassionFilterId").val(), "_blank");

        $.ajax({
            url: '/Home/InvitaionCard1',
            type: 'POST',
            data: { "OcassionId": $("#OcassionFilterId").val(), "Ocassionname": $("#OcassionFilterId option:selected").text() }, //get the search string
            success: function (result) {
                //  alert(result);
                if (result != "0") {

                    window.open('images/Card/' + result +".zip");
                    //window.open('images/Card/' + result + '_2.pdf', "_blank");
                    //window.open('images/Card/' + result + '3.pdf', "_blank");
                    //window.open('images/Card/' + result + '4.pdf', "_blank");
                 /*   window.open('images/Card/' + result + '5.pdf', "_blank");*/
                }
            }
        });

    });
    $("#EnclosurePass").click(function () {

        //   window.location.href = '/GestList/PassGenerateAll?OcassionId=' + $("#OcassionFilterId").val();




        window.open('/GestList/colorCodeAll?OcassionId=' + $("#OcassionFilterId").val(), "_blank");
    });
});
function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}


function GetAllInterview(Id) {
    var listItem = "";


    $.ajax({
        url: '/Home/GetAll',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
        success: function (result) {
            listItem += "<thead>";
            listItem += "<tr>";
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span></th>";
            //listItem += "<th></th>";
            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancyPlanslist = result.vacancyPlanslist;
            var islock = 0;
            for (var i = 0; i < vacancieslist.length; i++) {
                var color = vacancieslist[i].enclosureColor;
             /*   style = 'background-color:#817DFF ;color:white;'*/
                listItem += "<th >" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "</th>"; // <br>" + vacancieslist[i].total + "
                //  //listItem += "<th style='background-color:" + color + ";color:white;'>" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "</th>"; // <br>" + vacancieslist[i].total + "

                islock = vacancieslist[i].isLock;
            }
            listItem += "</tr>";
            listItem += "</thead>";
            listItem += "<tbody>";
            for (var j = 0; j < userlist.length; j++) {
                listItem += "<tr>";
                //listItem += "<td>" + parseInt(i + 1) + "</td>";
                //listItem += "<td id='UserId'>" + userlist[j].userName + "</td>";

                for (var z = 0; z < vacancieslist.length; z++) {
                    var color = vacancieslist[z].enclosureColor;
                    var totalplan = 0;
                    //for (var x = 0; x<vacancyPlanslist.length; x++) {

                    //    if (vacancyPlanslist[x].userId == userlist[j].id && vacancyPlanslist[x].enclosureId == vacancieslist[z].enclosureId && vacancyPlanslist[x].rankId == vacancieslist[z].rankId) {
                    //        //listItem += "<td><span class='RankId'>" + vacancieslist[z].rankId + "</span><span class='Userid'>" + userlist[j].id + "</span><input type='text' class='Cls-Isplan' placeholder='' value='" + vacancyPlanslist[x].total+"' style='width: 40px'/><span class='EncId'>" + vacancieslist[z].id + "</span></td>";
                    //        totalplan = vacancyPlanslist[x].total;
                    //    }

                    //}
                    /*  alert(vacancieslist[z].ocassionId);*/
                   
                    if (vacancieslist[z].isLock == true)
                        listItem += "<td><a href='/GestList/Index?OcassionId=" + vacancieslist[z].ocassionId + "&EnclosureId=" + vacancieslist[z].enclosureId + "&RankId=" + vacancieslist[z].rankId + "' class='btn btn-success btn-icon-text' style=';color:white;'>View<i class='ti-user btn-icon-append'></i></a></td>";
                    else
                        listItem += "<td><a href='/GestList/Index?OcassionId=" + vacancieslist[z].ocassionId + "&EnclosureId=" + vacancieslist[z].enclosureId + "&RankId=" + vacancieslist[z].rankId + "' class='btn btn-success btn-icon-text' style=';color:white;'>Add<i class='ti-user btn-icon-append'></i></a></td>";

                }
                listItem += "</tr>";
            }
            if (islock == 1) {
                $('#Passgen').removeClass('d-none');
                $('#EnclosurePass').removeClass('d-none');

            }
            else {
                $('#Passgen').addClass('d-none');
                $('#EnclosurePass').addClass('d-none');
            }
            listItem += "</tbody>";

            $("#tblData").html(listItem);
            /*$("#lblTotal").html(userlist.length);*/
           
            feather.replace();
            memberTable = $('#tblDetail').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: false,
                search: false,
                "order": [[2, "asc"]],
              
            });







        }
    });
}

function GetRAll(Id, UserId) {
    var listItem = "";




    $.ajax({
        url: '/VacancyReport/GetAll',
        type: 'POST',
        data: { "OcassionFilterId": Id, "UserId": UserId }, //get the search string
        success: function (result) {

            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span></th>";
            //listItem += "<th></th>";
            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancieslistGest = result.vacancieslistGest;
            var totalv = 0;
            listItem += "<tbody>";
            for (var i = 0; i < vacancieslist.length; i++) {
                listItem += "<tr>";
                var color = vacancieslist[i].enclosureColor;
                /*    listItem += "<td style='background-color:" + color + ";color:white;'>" + vacancieslist[i].enclosureName + "</td>";*/
                listItem += '<td class="text-muted">' + vacancieslist[i].enclosureName + '(' + vacancieslist[i].rankName + ')</td>';
                listItem += '<td class="w-100 px-0">';
                listItem += '<div class="progress progress-md mx-4">';


                //listItem += "<td>" + vacancieslist[i].rankName + "</td>";
                //listItem += "<td>" + vacancieslist[i].total + "</td>";
                var count = 0;
                for (var x = 0; x < vacancieslistGest.length; x++) {

                    if (vacancieslist[i].ocassionId == vacancieslistGest[x].ocassionId && vacancieslist[i].enclosureId == vacancieslistGest[x].enclosureId && vacancieslist[i].rankId == vacancieslistGest[x].rankId)
                        count = count + 1;

                }
                //listItem += "<td>" + count + "</td>";
                var avi = 0;
                avi = vacancieslist[i].total - count;
                totalv = totalv + vacancieslist[i].total;
                var aviinper = (avi * 100 / vacancieslist[i].total);
                var filout = 100 - aviinper;

                //listItem += "<td>" + avi + "</td>";
                listItem += ' <div class="progress-bar" role="progressbar" style="width: ' + filout + '%;  background-color:' + color + '" aria-valuenow="' + filout + '" aria-valuemin="0" aria-valuemax="100"></div>';
                listItem += '</div>';
                listItem += '</td>';

                listItem += '<td><h5 class="font-weight-bold mb-0">' + vacancieslist[i].total + ' / ' + count + '</h5></td>';
                listItem += '</tr>';

                listItem += "</tr>";
            }


            //if (vacancieslist.length > 0) {
            //    $('#Mainrpt').removeClass('d-none');
            //} else {
            //    $('#Mainrpt').addClass('d-none');
            //}


            listItem += "</tbody>";
            // $(".totalvec").html(totalv);
            $("#tblRData").html(listItem);
            /*$("#lblTotal").html(userlist.length);*/
            //  $("#tblDataReport").DataTable().destroy();

         




        }
    });
}

function GetAll(Id, UserId) {
    var listItem = "";


    $.ajax({
        url: '/VacancyReport/GetAll',
        type: 'POST',
        data: { "OcassionFilterId": Id, "UserId": UserId }, //get the search string
        success: function (result) {

            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span></th>";
            //listItem += "<th></th>";
            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancieslistGest = result.vacancieslistGest;
            var totalv = 0;
            listItem += "<tbody>";
            for (var i = 0; i < vacancieslist.length; i++) {
                listItem += "<tr>";
                var color = vacancieslist[i].enclosureColor;
                /*    listItem += "<td style='background-color:" + color + ";color:white;'>" + vacancieslist[i].enclosureName + "</td>";*/
                listItem += '<td class="text-muted">' + vacancieslist[i].enclosureName + '</td>';
                listItem += '<td class="w-100 px-0">';
                listItem += '<div class="progress progress-md mx-4">';


                //listItem += "<td>" + vacancieslist[i].rankName + "</td>";
                //listItem += "<td>" + vacancieslist[i].total + "</td>";
                var count = 0;
                for (var x = 0; x < vacancieslistGest.length; x++) {

                    if (vacancieslist[i].ocassionId == vacancieslistGest[x].ocassionId && vacancieslist[i].enclosureId == vacancieslistGest[x].enclosureId && vacancieslist[i].rankId == vacancieslistGest[x].rankId)
                        count = count + 1;

                }
                //listItem += "<td>" + count + "</td>";
                var avi = 0;
                avi = vacancieslist[i].total - count;
                totalv = totalv + vacancieslist[i].total;
                var aviinper = (avi * 100 / vacancieslist[i].total);
                var filout = 100 - aviinper;

                //listItem += "<td>" + avi + "</td>";
                listItem += ' <div class="progress-bar" role="progressbar" style="width: ' + filout + '%;  background-color:' + color + '" aria-valuenow="' + filout + '" aria-valuemin="0" aria-valuemax="100"></div>';
                listItem += '</div>';
                listItem += '</td>';

                listItem += '<td><h5 class="font-weight-bold mb-0">' + vacancieslist[i].total + ' / ' + count + '</h5></td>';
                listItem += '</tr>';

                listItem += "</tr>";
            }


            //if (vacancieslist.length > 0) {
            //    $('#Mainrpt').removeClass('d-none');
            //} else {
            //    $('#Mainrpt').addClass('d-none');
            //}


            listItem += "</tbody>";
            $(".totalvec").html(totalv);
            $("#tblDataReport").html(listItem);
            /*$("#lblTotal").html(userlist.length);*/
            //  $("#tblDataReport").DataTable().destroy();

            feather.replace();








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
                if ($("#OcassionFilterId").val() == "-1") {
                    $("#btnstatus1").addClass("d-none");
                }

                ////  $('#vstatus').html('<button type="button" class="btn btn-danger ">Finalized</button>');
            }
        }
    });
}
function SaveStatus(Id, UserId) {
    $.ajax({
        url: '/VacancyReport/SaveStatus',
        type: 'POST',
        data: { "OcassionFilterId": Id, "UserId": UserId }, //get the search string
        success: function (response) {
           
            if (response == 1) {
                Swal.fire(
                    'Alert!',
                    'Finalised successfully!',
                    'success'
                )
                $('#btnstatus').addClass('d-none');
                $('#btnstatus1').removeClass('d-none');
               
                CheckStatus($("#OcassionFilterId").val(), $("#spnUserId").html());
                
            }
            else if (response == 3) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'AllreadyFinalised!',

                })
            }

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