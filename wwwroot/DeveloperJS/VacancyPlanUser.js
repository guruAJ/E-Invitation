var memberTable;
$(document).ready(function () {

    $("#OcassionFilterId").change(function () {

        GetAllInterview($("#OcassionFilterId").val());
        GetAllAvailable($("#OcassionFilterId").val());

    });

   
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
function GetAllInterview(Id) {
    var listItem = "";

    //listItem += "<th>Interview Name</th>";
    //listItem += "<th>Interview Date</th>";
    //listItem += "<th>File NumberAPS</th>";
    //listItem += "<th>File NumberDR</th>";
    //listItem += "<th>File NumberDeputation</th>";
    //listItem += "<th>File NumberContract</th>";

    //listItem += "<th>Status</th>";

    //listItem += "<th>Action</th>";


    $.ajax({
        url: '/VacancyPlan/GetAllByUserId',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
        success: function (result) {
            listItem += "<thead>";
            listItem += "<tr>";
            //listItem += "<th class=''><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span><br><span class='mdi mdi-arrow-down'>User</span></th>";
            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancyPlanslist = result.vacancyPlanslist;
            for (var i = 0; i < vacancieslist.length; i++) {
                var color = vacancieslist[i].enclosureColor;
              /*  style = 'background-color:" + color + ";color:white;'*/
                listItem += "<th>" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "<br>" + vacancieslist[i].total + "</th>";

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
                    for (var x = 0; x<vacancyPlanslist.length; x++) {

                        if (vacancyPlanslist[x].userId == userlist[j].id && vacancyPlanslist[x].enclosureId == vacancieslist[z].enclosureId && vacancyPlanslist[x].rankId == vacancieslist[z].rankId) {
                            //listItem += "<td><span class='RankId'>" + vacancieslist[z].rankId + "</span><span class='Userid'>" + userlist[j].id + "</span><input type='text' class='Cls-Isplan' placeholder='' value='" + vacancyPlanslist[x].total+"' style='width: 40px'/><span class='EncId'>" + vacancieslist[z].id + "</span></td>";
                            totalplan = vacancyPlanslist[x].total;
                        }
                        
                    }

                    listItem += "<td><span class='RankId d-none'>" + vacancieslist[z].rankId + "</span><span class='Userid d-none'>" + userlist[j].id + "</span><input type='text' class='Cls-Isplan' placeholder='' value='" + totalplan + "' style='width: 40px'/><span class='EncId d-none'>" + vacancieslist[z].enclosureId + "</span></td>";

                }
                //listItem += "<td>" + result[i].InterviewDateString + "</td>";
                //listItem += "<td>" + result[i].FileNumberAPS + "</td>";
                //listItem += "<td>" + result[i].FileNumberDR + "</td>";
                //listItem += "<td>" + result[i].FileNumberDep + "</td>";
                //listItem += "<td>" + result[i].FileNumberCon + "</td>";


                ////listItem += "<td>" + result[i].InterviewTimeString + "</td>";
                //if (result[i].IsActive == true) {
                //    listItem += "<td><button type='button' class='btn btn-outline-blue btn-sm' onClick='ActiveDeActive(" + result[i].Id + ");'>Active</button></td>";
                //}
                //else {
                //    listItem += "<td><button type='button' class='btn btn-outline-blue btn-sm' onClick='ActiveDeActive(" + result[i].Id + ");'>Deactive</button></td>";
                //}
                ////listItem += "<td><a href='/AssignMembers?ScheduleId=" + result[i].Id + "' class='btn btn-sm btn-shadow font-weight-600 mr-2 btn-warning'>Assign Members</a></td>";
                ////listItem += "<td><a href='/AssignCandidates?ScheduleId=" + result[i].Id + "' class='btn btn-sm btn-shadow btn-focus  mr-lg-2'>Assign Candidates</a></td>";

                //listItem += "<td>" + "<button type='button' class='btn btn-icon btn-outline-blue btn-sm'  data-toggle='modal'  data-target='#mdAddInterview' onClick='GetById(" + result[i].Id + ");'><i class='fas fa-pen f11'></i>" + "</button></td>";
                listItem += "</tr>";
            }
            listItem += "</tbody>";

            $("#tblData").html(listItem);
            /*$("#lblTotal").html(userlist.length);*/
            $("#tblData").DataTable().destroy();

            feather.replace();
            memberTable = $('#tblDetail').DataTable({
                retrieve: true,
                lengthChange: true,
                ordering: false,
                search: true,
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

            //memberTable = $('#tblData').DataTable({
            //    orderCellsTop: true,
            //    fixedHeader: true,
            //    "scrollY": true,
            //    "scrollX": true,
            //    "scrollCollapse": true,
            //    retrieve: true,
            //    //stateSave: true,
            //    "searching": true,
            //    lengthMenu: [[25, 10, 50, 100, -1], [25, 10, 50, 100, "All"]],
            //    lengthChange: true,
            //    responsive: false,
            //    paging: false,
            //    "order": [[0, "asc"]],
            //    buttons: [{
            //        extend: 'copy',
            //        exportOptions: {
            //            columns: "thead th:not(.noExport)"
            //        }
            //    }, {
            //        extend: 'excel',
            //        exportOptions: {
            //            columns: "thead th:not(.noExport)"
            //        }
            //    }, {
            //        extend: 'pdf',
            //        exportOptions: {
            //            header: true,
            //            columns: "thead th:not(.noExport)"
            //        }
            //    }]
            //});

            //memberTable.buttons().container().appendTo('#tblData_wrapper .col-md-6:eq(0)');

            $('body').on('change', '.Cls-Isplan', function () {
                
                //alert($(this).closest("td").find(".Userid").html());
                //alert($(this).closest("td").find(".Cls-Isplan").val());
                //alert($(this).closest("td").find(".EncId").html());
                //alert($(this).closest("td").find(".RankId").html());
                var UserId = $(this).closest("td").find(".Userid").html();
                var Plan = $(this).closest("td").find(".Cls-Isplan").val();
                var EncId = $(this).closest("td").find(".EncId").html();
                var RankId = $(this).closest("td").find(".RankId").html();

                //alert("UserId-" + UserId + "EncId-" + EncId + "RankId-" + RankId + "Plan" + Plan );
                //var 
                SavePlan(Id,UserId, EncId, RankId, Plan);
                
            });




        }
    });
}

function GetAllAvailable(Id) {
    var listItem = "";

    //listItem += "<th>Interview Name</th>";
    //listItem += "<th>Interview Date</th>";
    //listItem += "<th>File NumberAPS</th>";
    //listItem += "<th>File NumberDR</th>";
    //listItem += "<th>File NumberDeputation</th>";
    //listItem += "<th>File NumberContract</th>";

    //listItem += "<th>Status</th>";

    //listItem += "<th>Action</th>";


    $.ajax({ 
        url: '/VacancyPlan/GetAllAvailableByUserId',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
        success: function (result) {
            listItem += "<thead>";
            listItem += "<tr>";
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span><br><span class='mdi mdi-arrow-down'>User</span></th>";

            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancyPlanslist = result.vacancyPlanslist;
            var userlist = result.userlist;
            for (var i = 0; i < vacancieslist.length; i++) {
                var color = vacancieslist[i].enclosureColor;
                var totplan = 0;
                for (var x = 0; x < vacancyPlanslist.length; x++) {
                    if (vacancieslist[i].enclosureId == vacancyPlanslist[x].enclosureId && vacancieslist[i].rankId == vacancyPlanslist[x].rankId)
                        totplan = totplan + vacancyPlanslist[x].total
                }
                var tot = vacancieslist[i].total - totplan;

                listItem += "<th style='background-color:" + color + ";color:white;'>" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "<br>" + tot + "</th>";

            }
            listItem += "</tr>";
            listItem += "</thead>";
            listItem += "<tbody>";
           
        
            listItem += "</tbody>";
            $("#tblData1").html(listItem);
            $("#tblData1").DataTable().destroy();
          
          



        }
    });
}

function SavePlan(Id,UserId, EncId, RankId, Plan) {
   


    $.ajax({
        url: '/VacancyPlan/SavePlan',
        type: 'POST',
        data: { "OcassionId": Id, "UserId": UserId, "EnclosureId": EncId, "RankId": RankId, "Total": Plan }, //get the search string
        success: function (result) {

            if (result.result == 4) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong or Invalid Entry!',
                    
                })
            }

            GetAllAvailable(Id);
        }
    });
}
