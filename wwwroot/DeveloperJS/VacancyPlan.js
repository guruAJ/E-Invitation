var memberTable;
var memberTablemap;
$(document).ready(function () {

    $('#tblData').dataTable({
        'bSort': false,
        'aoColumns': [
            { sWidth: "45%", bSearchable: false, bSortable: false },
            { sWidth: "45%", bSearchable: false, bSortable: false },
            { sWidth: "10%", bSearchable: false, bSortable: false }
        ],
        "scrollY": "200px",
        "scrollCollapse": true,
        "info": true,
        "paging": true
    });

    $("#OcassionFilterId").change(function () {
        if ($("#OcassionFilterId").val() != "-1") {
            $("#tblData").DataTable().destroy();
            GetAllInterview($("#OcassionFilterId").val());
            GetAllAvailable($("#OcassionFilterId").val());

            GetAllDirectorate($("#OcassionFilterId").val());
        }
        else {
            location.reload();
        }
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
    $('#btnMultiSend').click(function () {


        var HodIds = new Array();
        if (memberTablemap.$('input[type="checkbox"]:checked').length > 0) {
            memberTablemap.$('input[type="checkbox"]:checked').each(function () {

                var id = $(this).attr("Id");

                HodIds.push(id);

            });
        }
        if (memberTablemap.$('input[type="checkbox"]:checked').length > 0) {
            Swal.fire({
                title: 'Are you sure?',
                text: "Add Directorate ",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#072697',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Add it!'
            }).then((result) => {
                if (result.value) {
                    OcassionId = $("#OcassionFilterId").val();
                    MaapingSave(HodIds, OcassionId);

                }
            });
        }
        else {
            Swal.fire({
                text: "Please select atleast 1 data to Add."
            });
        }
    });

});
function GetAllInterview(Id) {
    var listItem1 = "";
    /*$("#tblData").html(listItem);*/
    $("#tblData").DataTable().destroy();
    //listItem += "<th>Interview Name</th>";
    //listItem += "<th>Interview Date</th>";
    //listItem += "<th>File NumberAPS</th>";
    //listItem += "<th>File NumberDR</th>";
    //listItem += "<th>File NumberDeputation</th>";
    //listItem += "<th>File NumberContract</th>";

    //listItem += "<th>Status</th>";

    //listItem += "<th>Action</th>";


    $.ajax({
        url: '/VacancyPlan/GetAll',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
        success: function (result) {
            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancyPlanslist = result.vacancyPlanslist;
            var OcassionStatus = result.ocassionStatus;
            $("#tblData").DataTable().destroy();
            listItem1 += "<thead>";
            listItem1 += "<tr>";
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span><br><span class='mdi mdi-arrow-down'>User</span></th>";
            listItem1 += "<th style='white;width:150px;'>Total Alloted</th>";

            for (var i = 0; i < vacancieslist.length; i++) {
                var color = vacancieslist[i].enclosureColor;
                /* style = 'background-color:" + color + ";color:white;'*/
                listItem1 += "<th >" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "<br>" + vacancieslist[i].total + "</th>";

            }
            listItem1 += "<th style='white;width:162px;'>Action</th>";
            listItem1 += "</tr>";
            listItem1 += "</thead>";
            listItem1 += "<tbody id='tbldetails'>";


            for (var j = 0; j < userlist.length; j++) {
                listItem1 += "<tr>";
                //listItem += "<td>" + parseInt(i + 1) + "</td>";
                listItem1 += "<td id='UserId'>" + userlist[j].userName + "</td>";
                var islock;
                var isFinal;
                for (var z = 0; z < vacancieslist.length; z++) {
                    var color = vacancieslist[z].enclosureColor;
                    var totalplan = 0;
                    isFinal = 0;
                    for (var x = 0; x < vacancyPlanslist.length; x++) {

                        if (vacancyPlanslist[x].userId == userlist[j].id && vacancyPlanslist[x].enclosureId == vacancieslist[z].enclosureId && vacancyPlanslist[x].rankId == vacancieslist[z].rankId) {
                            //listItem += "<td><span class='RankId'>" + vacancieslist[z].rankId + "</span><span class='Userid'>" + userlist[j].id + "</span><input type='text' class='Cls-Isplan' placeholder='' value='" + vacancyPlanslist[x].total+"' style='width: 40px'/><span class='EncId'>" + vacancieslist[z].id + "</span></td>";
                            totalplan = vacancyPlanslist[x].total;
                        }

                    }

                    for (var oc = 0; oc < OcassionStatus.length; oc++) {

                        if (OcassionStatus[oc].userId == userlist[j].id && OcassionStatus[oc].isStatus == 1) {

                            isFinal = 1;
                        }


                    }
                    if (vacancieslist[z].isLock == true || isFinal == 1) {
                        listItem1 += "<td><span class='RankId d-none'>" + vacancieslist[z].rankId + "</span><span class='Userid d-none'>" + userlist[j].id + "</span><input type='text' class='Cls-Isplan1' disabled onClick='this.setSelectionRange(0, this.value.length)' placeholder='' value='" + totalplan + "' style='width: 40px'/><span class='EncId d-none'>" + vacancieslist[z].enclosureId + "</span></td>";
                        islock = 1;
                    }
                    else {
                        listItem1 += "<td><span class='RankId d-none'>" + vacancieslist[z].rankId + "</span><span class='Userid d-none'>" + userlist[j].id + "</span><input type='text' class='Cls-Isplan' onClick='this.setSelectionRange(0, this.value.length)' placeholder='' value='" + totalplan + "' style='width: 40px'/><span class='EncId d-none'>" + vacancieslist[z].enclosureId + "</span><span class='currenttot d-none'>" + totalplan + "</span></td>";
                        islock = 0;
                    }


                }
                if (islock == 0 && isFinal == 0)
                    listItem1 += "<th width='100px'><button type='button' class='btn btn-outline-blue btn-sm btnupmapping' id='btnupmapping'><span class='mdi mdi-close'></span></button></th>";
                else
                    listItem1 += "<th width='100px'><button type='button' class='btn btn-outline-blue btn-sm' id=''>Locked</button></th>";

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
                listItem1 += "</tr>";
            }
            listItem1 += "</tbody>";

            $("#tblData").html(listItem1);
            /*$("#lblTotal").html(userlist.length);*/
            /*$("#tbldetails").DataTable().destroy();*/

            //feather.replace();
            //memberTable = $('#tblData').DataTable({
            //    retrieve: true,
            //    lengthChange: true,
            //    ordering: false,
            //    search: true,
            //    "order": [[2, "asc"]],
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
            //            columns: "thead th:not(.noExport)"
            //        }
            //    }]
            //});

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



            $('body').unbind().on('change', '.Cls-Isplan', function () {


                //  alert($(this).closest("td").find(".Cls-Isplan").val());
                // alert($(this).closest("td").find(".currenttot").html());
                //alert($(this).closest("td").find(".Cls-Isplan").val());
                //alert($(this).closest("td").find(".EncId").html());
                //alert($(this).closest("td").find(".RankId").html());
                var UserId = $(this).closest("td").find(".Userid").html();
                var Plan = $(this).closest("td").find(".Cls-Isplan").val();
                var EncId = $(this).closest("td").find(".EncId").html();
                var RankId = $(this).closest("td").find(".RankId").html();

                // alert("UserId-" + UserId + "EncId-" + EncId + "RankId-" + RankId + "Plan" + Plan );
                //var 

                if (parseInt($(this).closest("td").find(".currenttot").html()) < parseInt(Plan)) {

                    if ($('#autosave').prop('checked') == false) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "You won't be Save this!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, Save it!'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                SavePlan(Id, UserId, EncId, RankId, Plan);

                            }
                            else {

                            }
                        })


                    }
                    else {
                        SavePlan(Id, UserId, EncId, RankId, Plan);
                    }

                }
                else {

                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'You can not decrease the value!',

                    })

                }

            });

            $('body').on('click', '.btnupmapping', function () {
                //alert($(this).closest("tr").find(".Userid").html());


                Swal.fire({
                    title: 'Are you sure?',
                    text: "Remove Directorate from this event",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#072697',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, Removed it!'
                }).then((result) => {
                    if (result.value) {
                        var UserId = $(this).closest("tr").find(".Userid").html();
                        DeleteMapping(UserId, $("#OcassionFilterId").val());

                    }
                });


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
        url: '/VacancyPlan/GetAllAvailable',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
        success: function (result) {
            listItem += "<thead>";
            listItem += "<tr>";
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span><br><span class='mdi mdi-arrow-down'>User</span></th>";
            listItem += "<th style='white;width:162px;'>Available Seat</th>";
            var vacancieslist = result.vacancieslist;
            var userlist = result.userlist;
            var vacancyPlanslist = result.vacancyPlanslist;
            var userlist = result.userlist;
            for (var i = 0; i < vacancieslist.length; i++) {
                // var color = vacancieslist[i].enclosureColor;
                var color = "whilte";
                var totplan = 0;
                for (var x = 0; x < vacancyPlanslist.length; x++) {
                    if (vacancieslist[i].enclosureId == vacancyPlanslist[x].enclosureId && vacancieslist[i].rankId == vacancyPlanslist[x].rankId)
                        totplan = totplan + vacancyPlanslist[x].total
                }
                var tot = vacancieslist[i].total - totplan;
                //style = 'background-color:" + color + ";color:black;'
                listItem += "<th>" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "<br>" + tot + "</th>";

            }
            /* listItem += "<th>Action</th>";*/
            listItem += "<th style='white;width:162px;'>Action</th>";
            listItem += "</tr>";
            listItem += "</thead>";
            listItem += "<tbody>";


            listItem += "</tbody>";
            $("#tblData1").html(listItem);
            $("#tblData1").DataTable().destroy();





        }
    });
}

function SavePlan(Id, UserId, EncId, RankId, Plan) {



    $.ajax({
        url: '/VacancyPlan/SavePlan',
        type: 'POST',
        data: { "OcassionId": Id, "UserId": UserId, "EnclosureId": EncId, "RankId": RankId, "Total": Plan }, //get the search string
        success: function (result) {

            if (result.result == 4) {
                GetAllInterview($("#OcassionFilterId").val());
                GetAllAvailable($("#OcassionFilterId").val());

                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong or Invalid Entry!',

                })


            }
            if ($('#autosave').prop('checked') == false) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Data has been Save Successfully',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
            GetAllAvailable(Id);

        }
    });
}

function GetAllDirectorate(Id) {

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
        url: '/VacancyPlan/GetNotMappAll',
        type: 'POST',
        data: { "OcassionFilterId": Id }, //get the search string
        success: function (result) {
            $("#tbl_tbl_Detail").DataTable().destroy(); //Newly added
            var count = 1;
            for (var i = 0; i < result.length; i++) {
                listItem += "<tr>";
                listItem += "<td class='d-none'><span id='spnInsId' class='d-none'>" + result[i].id + "</span></td>";
                listItem += "<td>";
                listItem += "<div class='custom-control custom-checkbox small'>";
                listItem += "<input type='checkbox' class='custom-control-input' id='" + result[i].id + "'>";
                listItem += "<label class='custom-control-label' for='" + result[i].id + "'></label>";
                listItem += "</div>";
                listItem += "</td>";
                listItem += "<td >" + count + "</td>";
                listItem += "<td>" + result[i].userName + "</td>";


                listItem += "</tr>";
                count++;

                $('#mapping').removeClass('d-none');
            }

            $("#DetailBody").html(listItem);

            memberTablemap = $('#tbl_tbl_Detail').DataTable({
                retrieve: false,
                lengthChange: false,
                ordering: false,
                searching: false,
                paging: false,
                destroy: true,
            });


            //memberTablemap.buttons().container().appendTo('#tbl_tbl_Detail_wrapper .col-md-6:eq(0)');


            var rows;
            $("#tbl_tbl_Detail #chkAll").click(function () {
                if ($(this).is(':checked')) {
                    rows = memberTablemap.rows({ 'search': 'applied' }).nodes();
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                }
                else {
                    rows = memberTablemap.rows({ 'search': 'applied' }).nodes();
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                }
            });
            $('#DetailBody').on('change', 'input[type="checkbox"]', function () {
                if (!this.checked) {
                    var el = $('#chkAll').get(0);
                    if (el && el.checked && ('indeterminate' in el)) {
                        el.indeterminate = true;
                    }
                }
            });

        }
    });

}

function MaapingSave(HodIds, OcassionId) {


    $.ajax({
        url: '/VacancyPlan/SaveMappAll?OcassionId=' + OcassionId,
        type: 'POST',
        data: { "Arr": HodIds }, //get the search string
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
                    GetAllInterview($("#OcassionFilterId").val());
                    GetAllAvailable($("#OcassionFilterId").val());

                    GetAllDirectorate($("#OcassionFilterId").val());
                }
            }

        },
        error: function (result) {

        }
    });
}


function DeleteMapping(UserId, OcassionId) {


    $.ajax({
        url: '/VacancyPlan/DeleteMapping',
        type: 'POST',
        data: { "UserId": UserId, "OcassionId": OcassionId }, //get the search string
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

                    GetAllInterview($("#OcassionFilterId").val());
                    GetAllAvailable($("#OcassionFilterId").val());

                    GetAllDirectorate($("#OcassionFilterId").val());
                }
            }

        },
        error: function (result) {

        }
    });
}
