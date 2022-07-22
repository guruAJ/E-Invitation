var memberTable;
$(document).ready(function () {
    GetAllData($("#OcassionFilterId").val(), $("#UserId").val());
    GetAllOcassionBy($("#OcassionFilterId").val());
    $("#OcassionFilterId").change(function () {

        GetAllData($("#OcassionFilterId").val(), $("#UserId").val());
        GetAllOcassionBy($("#OcassionFilterId").val());
        $('#UserId').removeClass('d-none');
    });
    $("#UserId").change(function () {

        GetAllData($("#OcassionFilterId").val(), $("#UserId").val());
       

    });
  
   // $('body').addClass("sidebar-icon-only");

   
});

//function UploadImage(OutFile) {
    

    
//}
function getRandomColor() {
    var letters = '0123456789ABCDEF';
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}


function GetAllData(Id,UserId) {
    var listItem = "";

 
    $.ajax({
        url: '/AllVacancy/GetGestByUserId',
        type: 'POST',
      
        data: { "OcassionId": Id, "UserId": UserId }, //get the search string
        success: function (result) {
            listItem += "<thead>";
            listItem += "<tr>";
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span></th>";
            //listItem += "<th></th>";
          
            var addGestLists = result.addGestLists;
         
            var Total = 0;
            var isstatus = "";
            var color ="red";
               // var color = vacancieslist[i].enclosureColor;
                //listItem += "<th style='background-color:" + color + ";color:white;'>" + vacancieslist[i].enclosureName + "<br>" + vacancieslist[i].rankName + "<br>" + vacancieslist[i].total + "</th>";
              /*  $("#Vencancy").html(vacancieslist[i].enclosureName);*/
              
                //$("#lblTotal").html(vacancieslist[i].total);

                //Total = vacancieslist[i].total;

               
            isstatus = "disabled1";
            /*listItem += "<th class='d-none1'>Id</th>";*/
           /* background - color: " + color + "; color: white;*/
                listItem += "<th class='d-none' style='background-color:" + color + ";color:white;width:10px'></th>";
            listItem += "<th style='width:16.66%'>S/No</th>";
            listItem += "<th style='width:16.66%'>Rank Name</th>";
                listItem += "<th style='width:16.66%'>Army No</th>";
                listItem += "<th>Name</th>";
                listItem += "<th>Unit</th>";
                listItem += "<th>Fmn</th>";
                listItem += "<th>Email</th>";
                listItem += "<th>PhoneNo</th>";
                listItem += "<th>Name Of Guest</th>";
                listItem += "<th>Gender</th>";
                listItem += "<th>Relation</th>";
                listItem += "<th>Date of birth</th>";
            listItem += "<th>AadhaarNo</th>";
                listItem += "<th>Photo</th>";
                //listItem += "<th style='background-color:" + color + ";color:white;'>Action</th>";

         
            listItem += "</tr>";
            listItem += "</thead>";
            listItem += "<tbody>";
            var srl = 0;

            for (var z = 0; z < addGestLists.length; z++) {
                srl++;
                
                listItem += "<tr id='maindata'>";
              
                listItem += "<td class='d-none'> <input type='text' class='form-control form-control-sm spnId' placeholder='Armyno' id='spnId' value='" + addGestLists[z].id + "' /></td>";
                listItem += "<td><span>" + srl+"</span></td>";
                listItem += "<td><span id='RankName" + srl + "' class=' RankName'></span></td>"
                listItem += "<td> " + addGestLists[z].armyNo + "</td>";
                listItem += "<td> " + addGestLists[z].indlName+"</td>";
                listItem += "<td> " + addGestLists[z].unit+"</td>";
                listItem += "<td> " + addGestLists[z].fmn +"</td>";
                listItem += "<td> " + addGestLists[z].emailId +"</td>";
                listItem += "<td>" + addGestLists[z].phoneNo +"</td>";
                listItem += "<td> " + addGestLists[z].nameOfGest +"</td>";
                //listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Gender' id='Gender' value='" + addGestLists[z].gender +"'/></td>";
                //if (addGestLists[z].gender == "Male")
                //    listItem += "<td><select name='Gender' id='Gender' " + isstatus +"><option value='Male' selected>Male</option><option value='Female'>Female</option></select></td>"
                //else
                //    listItem += "<td><select name='Gender' id='Gender' " + isstatus +"><option value='Male'>Male</option><option value='Female' selected>Female</option></select></td>"
                listItem += "<td> " + addGestLists[z].gender + "</td>";
                //listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Relation' id='Relation' value='" + addGestLists[z].relation + "'/></td>";
               // if (addGestLists[z].relation =="Self")
               //     listItem += "<td><select name='Relation' id='Relation' " + isstatus +"><option value='Self' selected>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option></select></td>"
               //else if (addGestLists[z].relation == "Wife")
               //     listItem += "<td><select name='Relation' id='Relation' " + isstatus +"><option value='Self'>Self</option><option value='Wife' selected>Wife</option><option value='Brother'>Brother</option></select></td>"
               //else if (addGestLists[z].relation == "Brother")
               //     listItem += "<td><select name='Relation' id='Relation' " + isstatus +"><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother' selected>Brother</option></select></td>"
               // else
               //     listItem += "<td><select name='Relation' id='Relation' " + isstatus +"><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option></select></td>"
                listItem += "<td> " + addGestLists[z].relation + "</td>";
                listItem += "<td> " + addGestLists[z].dob + "</td>";
                listItem += "<td> " + addGestLists[z].adhaorNo + "</td>";
                if (isstatus == "disabled") {
                    if (addGestLists[z].photo != "")
                        listItem += "<td><div class='imgUp'><div class='imagePreview' style='background-image: url(/images/Profile/" + addGestLists[z].photo + ")'></div></div><span id='spnimage' class='d-none'>" + addGestLists[z].photo + "</span></div>";
                    else
                        listItem += "<td><div class='imgUp'><div class='imagePreview'></div></div><span id='spnimage' class='d-none'></span></div>";

                }
                else {
                    if (addGestLists[z].photo != "")
                        listItem += "<td><div class='imgUp'><div class='imagePreview' style='background-image: url(/images/Profile/" + addGestLists[z].photo + ")'></div></div><span id='spnimage' class='d-none'>" + addGestLists[z].photo + "</span></div>";
                    else
                        listItem += "<td><div class='imgUp'><div class='imagePreview'></div><label class='btn-ImageUpload' style='width: 50px;height: 20px;overflow;'></label></div><span id='spnimage' class='d-none'></span></div>";
                }
                //if (isstatus == "disabled") {
                //    listItem += "<td></td>";
                //}
                //else {
                //    listItem += "<td><button class='btn btn-info btn-icon-text btn-sm mb-1 btnsave'>Update</button><br><button class='btn btn-info btn-icon-text btn-sm mb-1 btnDelete'>Delete</button></td>";
                //}
                    listItem += "</tr>";


                $('#Gender').val(addGestLists[z].gender);
                Total = Total - 1;

                GetAllRank('RankName' + srl, addGestLists[z].indRankId);
            }


            //for (var Count=0; Total > Count; Count++) {
            //    srl++;

               
            //    listItem += "<tr>";
            //    listItem += "<td>" + srl + "</td>";
            //    listItem += "<td class='d-none'> <input type='text' " + isstatus +" class='form-control form-control-sm spnId' placeholder='Armyno' id='spnId' value='0' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm Armyno' placeholder='Armyno' id='Armyno' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm' placeholder='IndlName' id='IndlName' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm' placeholder='Unit Name' id='Unit' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm' placeholder='Fmn' id='Fmn' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm EmailId' placeholder='EmailId' id='EmailId' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm PhoneNo' placeholder='PhoneNo' id='PhoneNo' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm' placeholder='Name Of Gest' id='NameOfGest' /></td>";
            //    /*listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Gender' id='Gender' /></td>";*/
            //    listItem += "<td><select name='Gender' " + isstatus +" id='Gender'><option value='Male'>Male</option><option value='Female'>Female</option></select></td>"
            //    listItem += "<td><select name='Relation' " + isstatus +" id='Relation'><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option></select></td>"
            //   // listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Relation' id='Relation' /></td>";
            //    listItem += "<td> <input type='Date' " + isstatus +" class='form-control form-control-sm' style='width: 130px' placeholder='Date of birth' id='Dob' /></td>";
            //    listItem += "<td> <input type='text' " + isstatus +" class='form-control form-control-sm' placeholder='AdhaorNo' id='AdhaorNo' /></td>";
            //    if (isstatus == "disabled") {
            //        listItem += "<td></td>";
            //    }
            //    else {
            //        listItem += "<td><div class='imgUp'><div class='imagePreview'></div><label class='btn-ImageUpload' style='width: 50px;height: 20px;overflow;'>Upload<input type='file' class='uploadFile img' value='Upload Photo' style='width: 0px;height: 0px;overflow: hidden;'></label></div><span id='spnimage' class='d-none'></span></div>";
            //    }
            //    if (isstatus == "disabled") {
            //        listItem += "<td></td>";
            //    }
            //    else {
            //        listItem += "<td><button class='btn btn-info btn-icon-text btn-sm mb-1 btnsave'>Save</button></td>";
            //    }
            //    listItem += "</tr>";

                
            //}
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
function GetAllOcassionBy(Id) {
    var listItem = "";
    listItem = "<option value='0'>-- All guest vacancy --</option>";
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