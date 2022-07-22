var memberTable;
$(document).ready(function () {
    GetAllData(0);
    document.body.style.zoom = "90%"
    $(".datepicker").datepicker({
        dateFormat: "yy-mm-dd"
    });

    $('body').addClass("sidebar-icon-only");

  

    //$("#OcassionFilterId").change(function () {

    //    GetAllInterview($("#OcassionFilterId").val());
   

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
    $('body').on("blur", ".PhoneNo", function () {

       
        var mobNum = $(this).val();
        $(this).addClass("border border-danger");
        var filter = /^\d*(?:\.\d{1,2})?$/;

        if (filter.test(mobNum)) {
            if (mobNum.length == 10) {
               
                $(this).removeClass("border border-danger");
            } else {
                Swal.fire('Please put 10  digit Phone number');
                $(this).addClass("border border-danger");
                return false;
            }
        }
        else {
            
            Swal.fire('Not a valid Phone number');
            $(this).addClass("border border-danger");
            return false;
        }

    });
    $('body').on("blur", ".EmailId", function () {
        var valueToTest = $(this).val();
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (testEmail.test(valueToTest))
            $(this).removeClass("border border-danger");
        else {
            Swal.fire('Please Put Valid  EmailId');
            $(this).addClass("border border-danger");
        }
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

function isNumber(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function GetAllData(Id) {
    var listItem = "";

    $.ajax({
        url: '/GestList/GetGestByUserId',
        type: 'POST',
        data: { "Id": Id }, //get the search string
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
                $("#Rank").html(vacancieslist[i].rankName + " (" + vacancieslist[i].enclosureName+")");
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
                listItem += "<th style='color:white;'>Unit Name</th>";
                listItem += "<th style='color:white;'>Formation</th>";
                listItem += "<th style='color:white;'>Email Address</th>";
                listItem += "<th style='color:white;'>Ph/Mobile</th>";
                listItem += "<th style='color:white;'>Relation</th>";
                listItem += "<th style='color:white;'>Name Of Guest</th>";
                listItem += "<th style='color:white;'>Gender</th>";
                
                listItem += "<th style='color:white;'>DOB</th>";
                listItem += "<th style='color:white;'>AadhaarNo</th>";
                listItem += "<th style='color:white;'>Photo</th>";

                listItem += "<th style='color:white;' class='d-none'>Vch Pass</th>";

                listItem += "<th style='color:white;'>Action</th>";

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
                
                listItem += "<td> <input type='text' class='form-control form-control-sm Armyno' " + isstatus + " onClick='this.setSelectionRange(0, this.value.length)' data-toggle='tooltip' title='" + addGestLists[z].armyNo + "' placeholder='Armyno' id='Armyno' value='" + addGestLists[z].armyNo + "' /></td>";
                listItem += "<td><select name='RankName' " + isstatus + " id='RankName" + srl+"' class='form-control form-control-sm RankName'></select></td>"
                listItem += "<td> <input type='text' class='form-control form-control-sm' " + isstatus + " onClick='this.setSelectionRange(0, this.value.length)' placeholder='Indl Name' id='IndlName' data-toggle='tooltip' title='" + addGestLists[z].indlName + "' value='" + addGestLists[z].indlName + "' /></td>";
                listItem += "<td> <input type='text' class='form-control form-control-sm' " + isstatus +" onClick='this.setSelectionRange(0, this.value.length)' placeholder='Unit Name' id='Unit' data-toggle='tooltip' title='" + addGestLists[z].unit +"' value='" + addGestLists[z].unit +"' /></td>";
                listItem += "<td> <input type='text' class='form-control form-control-sm' " + isstatus +" onClick='this.setSelectionRange(0, this.value.length)' placeholder='Fmn' id='Fmn' data-toggle='tooltip' title='" + addGestLists[z].fmn +"' value='" + addGestLists[z].fmn +"' /></td>";
                listItem += "<td> <input type='text' class='form-control form-control-sm EmailId' " + isstatus +" onClick='this.setSelectionRange(0, this.value.length)' EmailId' placeholder='Email Address' id='EmailId' data-toggle='tooltip' title='" + addGestLists[z].emailId +"' value='" + addGestLists[z].emailId +"' /></td>";
                listItem += "<td> <input type='text' class='form-control form-control-sm PhoneNo' " + isstatus + " onClick='this.setSelectionRange(0, this.value.length)' placeholder='PhoneNo' id='PhoneNo' data-toggle='tooltip' title='" + addGestLists[z].phoneNo + "' value='" + addGestLists[z].phoneNo + "'/></td>";
                
                //listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Relation' id='Relation' value='" + addGestLists[z].relation + "'/></td>";
                if (addGestLists[z].relation == "Self")
                    listItem += "<td><select name='Relation' id='Relation' class='Relation' " + isstatus + "><option value='Self' selected>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option><option value='Other'>Other</option><option value='Mother'>Mother</option><option value='Father'>Father</option></select></td>"
                else if (addGestLists[z].relation == "Wife")
                    listItem += "<td><select name='Relation' id='Relation' class='Relation' " + isstatus + "><option value='Self'>Self</option><option value='Wife' selected>Wife</option><option value='Brother'>Brother</option><option value='Other'>Other</option><option value='Mother'>Mother</option><option value='Father'>Father</option></select></td>"
                else if (addGestLists[z].relation == "Brother")
                    listItem += "<td><select name='Relation' id='Relation' class='Relation' " + isstatus + "><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother' selected>Brother</option><option value='Other'>Other</option><option value='Mother'>Mother</option><option value='Father'>Father</option></select></td>"
                else if (addGestLists[z].relation == "Other")
                    listItem += "<td><select name='Relation' id='Relation' class='Relation' " + isstatus + "><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option><option value='Other' selected>Other</option><option value='Mother'>Mother</option><option value='Father'>Father</option></select></td>"
                else if (addGestLists[z].relation == "Father")
                    listItem += "<td><select name='Relation' id='Relation' class='Relation' " + isstatus + "><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option><option value='Other'>Other</option> <option value='Mother'>Mother</option><option value='Father' selected>Father</option></select></td>"
                else if (addGestLists[z].relation == "Mother")
                    listItem += "<td><select name='Relation' id='Relation' class='Relation' " + isstatus + "><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option><option value='Other'>Other</option> <option value='Mother' selected>Mother</option><option value='Father' >Father</option></select></td>"

                  

                listItem += "<td> <input type='text' class='form-control form-control-sm' " + isstatus + " onClick='this.setSelectionRange(0, this.value.length)' placeholder='Name Of Guest' id='NameOfGest' data-toggle='tooltip' title='" + addGestLists[z].nameOfGest + "' value='" + addGestLists[z].nameOfGest + "'/></td>";
                //listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Gender' id='Gender' value='" + addGestLists[z].gender +"'/></td>";
                if (addGestLists[z].gender == "Male")
                    listItem += "<td><select name='Gender' id='Gender' " + isstatus +"><option value='Male' selected>M</option><option value='Female'>F</option></select></td>"
                else
                    listItem += "<td><select name='Gender' id='Gender' " + isstatus + "><option value='Male'>M</option><option value='Female' selected>F</option></select></td>"
               
              
                listItem += "<td> <input type='Date'  class='form-control form-control-sm dob' " + isstatus +" style='width: 130px' placeholder='Date of birth' id='Dob' value='" + addGestLists[z].dob + "'/></td>";
                listItem += "<td> <input type='text' class='form-control form-control-sm AdhaorNo' " + isstatus +" onClick='this.setSelectionRange(0, this.value.length)' placeholder='AdhaorNo' onkeypress='return isNumber(event)' pattern='\d * ' maxlength='12' id='AdhaorNo' data-toggle='tooltip' title='" + addGestLists[z].adhaorNo + "' value='" + addGestLists[z].adhaorNo + "'/></td>";
                if (isstatus == "disabled") {
                    if (addGestLists[z].photo != "")
                        listItem += "<td><div class='imgUp'><div class='imagePreview' style='background-image: url(/images/Profile/" + addGestLists[z].photo + ")'></div></div><span id='spnimage' class='d-none'>" + addGestLists[z].photo + "</span></div>";
                    else
                        listItem += "<td><div class='imgUp'><div class='imagePreview'></div></div><span id='spnimage' class='d-none'></span></div>";

                }
                else {
                    if (addGestLists[z].photo != "")
                        listItem += "<td><div class='imgUp'><div class='imagePreview' style='background-image: url(/images/Profile/" + addGestLists[z].photo + ")'></div><label class='btn-ImageUpload' style='width: 50px;height: 20px;overflow;'>Upload<input type='file' class='uploadFile img' value='Upload Photo' style='width: 0px;height: 0px;overflow: hidden;'></label></div><span id='spnimage' class='d-none'>" + addGestLists[z].photo + "</span></div>";
                    else
                        listItem += "<td><div class='imgUp'><div class='imagePreview'></div><label class='btn-ImageUpload' style='width: 50px;height: 20px;overflow;'>Upload<input type='file' class='uploadFile img' value='Upload Photo' style='width: 0px;height: 0px;overflow: hidden;'></label></div><span id='spnimage' class='d-none'></span></div>";
                }

                listItem += "<td class='d-none'>";
                listItem += "<div class='custom-control custom-checkbox small'>";
                if (addGestLists[z].isPass) {
                   
                    listItem += "<input type='checkbox' checked='true' class='custom-control-input checkboxPass' id='" + addGestLists[z].id+"'>";
                    listItem += "<label class='custom-control-label' for='" + addGestLists[z].id +"'></label>";
                }
                else {
                    listItem += "<input type='checkbox' class='custom-control-input checkboxPass' id='" + addGestLists[z].id +"'>";
                    listItem += "<label class='custom-control-label' for='" + addGestLists[z].id +"'></label>";
                }
                listItem += "</div>";
                listItem += "</td>";


                if (isstatus == "disabled") {
                    listItem += "<td></td>";
                }
                else {
                    listItem += "<td><button class='btn btn-info btn-icon-text btn-sm mb-1 btnsave'>Update</button><br><button class='btn btn-info btn-icon-text btn-sm mb-1 btnDelete'>Delete</button></td>";
                }
                    listItem += "</tr>";


                $('#Gender').val(addGestLists[z].gender);
                Total = Total - 1;

                GetAllRank('RankName' + srl, addGestLists[z].indRankId);
            }


            for (var Count=0; Total > Count; Count++) {
                srl++;

                if (isstatus == "") {
                    listItem += "<tr>";
                    listItem += "<td>" + srl + "</td>";
                    listItem += "<td class='d-none'> <input type='text' " + isstatus + " class='form-control form-control-sm spnId' placeholder='Army no' id='spnId' value='0' /></td>";

                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm Armyno' placeholder='Army no' id='Armyno' /></td>";
                    listItem += "<td><select name='RankName' " + isstatus + " id='RankName" + srl + "' class='form-control form-control-sm RankName'></select></td>"
                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm IndlName' placeholder='Indl Name' id='IndlName' /></td>";
                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm' placeholder='Unit Name' id='Unit' /></td>";
                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm' placeholder='Fmn' id='Fmn' /></td>";
                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm EmailId' placeholder='EmailId' id='EmailId' /></td>";
                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm PhoneNo' placeholder='PhoneNo' id='PhoneNo' /></td>";
                    listItem += "<td><select name='Relation' " + isstatus + " id='Relation' class='Relation'><option value='Self'>Self</option><option value='Wife'>Wife</option><option value='Brother'>Brother</option><option value='Mother'>Mother</option><option value='Father'>Father</option><option value='Other'>Other</option></select></td>"
                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm' placeholder='Name Of Guest' id='NameOfGest' /></td>";
                    /*listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Gender' id='Gender' /></td>";*/
                    listItem += "<td><select name='Gender' " + isstatus + " id='Gender'><option value='Male'>M</option><option value='Female'>F</option></select></td>"

                    // listItem += "<td> <input type='text' class='form-control form-control-sm' placeholder='Relation' id='Relation' /></td>";
                    listItem += "<td> <input type='Date' " + isstatus + " class='form-control form-control-sm datepicker dob' style='width: 130px' placeholder='Date of birth' id='Dob' /></td>";
                    listItem += "<td> <input type='text' " + isstatus + " class='form-control form-control-sm AadhaarNo' placeholder='AadhaarNo' onkeypress='return isNumber(event)' pattern='\d * ' maxlength='12' id='AdhaorNo' /></td>";
                    if (isstatus == "disabled") {
                        listItem += "<td></td>";
                    }
                    else {
                        listItem += "<td><div class='imgUp'><div class='imagePreview'></div><label class='btn-ImageUpload' style='width: 50px;height: 20px;overflow;'>Upload<input type='file' class='uploadFile img' value='Upload Photo' style='width: 0px;height: 0px;overflow: hidden;'></label></div><span id='spnimage' class='d-none'></span></div>";
                    }
                    listItem += "<td class='d-none'>";
                    listItem += "<div class='custom-control custom-checkbox small'>";
                    listItem += "<input type='checkbox' class='custom-control-input checkboxPass' id='checkboxPass'>";
                    listItem += "<label class='custom-control-label' for='checkboxPass'></label>";
                    listItem += "</div>";
                    listItem += "</td>";
                    if (isstatus == "disabled") {
                        listItem += "<td></td>";
                    }
                    else {
                        listItem += "<td><button class='btn btn-info btn-icon-text btn-sm mb-1 btnsave'>Save</button></td>";
                    }
                    listItem += "</tr>";

                    GetAllRank('RankName' + srl, 0);
                }
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

          

            $('body').on("change", ".Relation", function () {

                var value = $(this).val();
                if (value == "Self")
                    $(this).closest("tr").find("#NameOfGest").val($(this).closest("tr").find("#IndlName").val())
                else
                    $(this).closest("tr").find("#NameOfGest").val("");

            });
            $('body').on("change", ".IndlName", function () {

                var value = $(this).closest("tr").find(".Relation").val();
                if (value == "Self")
                    $(this).closest("tr").find("#NameOfGest").val($(this).closest("tr").find("#IndlName").val())

            });
            //$('body').on("keyup", ".AdhaorNo", function () {

                
            //    var value = $(this).val();

            //    if (value >= 16) {
            //        if ($(this).closest("tr").find(".Relation").val() != "Other") {
            //            //value = value.replace(/\D/g, "").split(/(?:([\d]{4}))/g).filter(s => s.length > 0).join("-");

            //            if (value.length >= 16) {
            //                $(this).val(value.substring(0, 15));
                           
            //            }
            //            else {
            //                $(this).val(value);
            //            }
            //        }
            //        else
            //            $(this).val(value);
            //    }
            //    else
            //        $(this).val(value.substring(0, 19));

            //});
            $('body').on("keyup", ".PhoneNo", function () {


                var value = $(this).val();
                value = value.replace(/\D/g, "").split(/(?:([\d]{4}))/g).filter(s => s.length > 0).join("");


                $(this).val(value);

            });
                $('body').on("change", ".uploadFile", function () {
                    var uploadFile = $(this);
                    var files = !!this.files ? this.files : [];
                    if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

                    if (/^image/.test(files[0].type)) { // only image file
                        var reader = new FileReader(); // instance of the FileReader
                        reader.readAsDataURL(files[0]); // read the local file

                        reader.onloadend = function () { // set image data as background of div
                            //alert(uploadFile.closest(".upimage").find('.imagePreview').length);
                            uploadFile.closest(".imgUp").find('.imagePreview').css("background-image", "url(" + this.result + ")");


                            // uploadFile.closest(".imgUp").find('#spnimage').html("Ok");
                        }
                        var OutFile = uploadFile.get(0).files;

                        var data = new FormData();

                        data.append("files", OutFile[0]);

                        $.ajax({
                            url: '/GestList/Compare',
                            contentType: false,
                            processData: false,
                            type: 'POST',
                            data: data,
                            success: function (response) {
                                // ; debugger
                                if (response != "null") {
                                    if (response == -3) {
                                        window.location.href = "/UserLogin";//Session expired
                                    }
                                    else if (response == -1) {

                                    }

                                    else {
                                        uploadFile.closest("tr").find("#spnimage").html(response);
                                        // $(this).closest("tr").find("#Armyno").val()
                                        //  uploadFile.closest(".imgUp").find($('#spnimage').html(response));
                                    }
                                }

                            },
                            error: function (result) {


                            }
                        });
                    }
                    else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Only image file!',
                          
                        })
                    }
                });
            $('body').on('change', '#Armyno', function () {

                var vv = $(this);
                var arno = $(this).val();
                var IndlName = "";
                var Unit = "";
                var Fmn = "";
                $('#tblData tbody tr').each(function () {
                    if ($(this).closest("tr").find("#Armyno").val() != "") {
                        if ($(this).closest("tr").find("#Armyno").val() == arno) {

                            IndlName = $(this).closest("tr").find("#IndlName").val();
                            Unit = $(this).closest("tr").find("#Unit").val();
                            Fmn = $(this).closest("tr").find("#Fmn").val();


                            vv.closest("tr").find("#IndlName").val(IndlName);
                            vv.closest("tr").find("#Unit").val(Unit);
                            vv.closest("tr").find("#Fmn").val($(this).closest("tr").find("#Fmn").val());
                            vv.closest("tr").find("#EmailId").val($(this).closest("tr").find("#EmailId").val());
                            vv.closest("tr").find("#PhoneNo").val($(this).closest("tr").find("#PhoneNo").val());

                            //   break;
                        }
                       
                    }
                   // alert($(this).closest("tr").find("#Armyno").val());
                
                });

                $.ajax({
                    url: '/GestList/GetbyArmyno',
                    type: 'POST',
                    data: { "ArmyNo": arno }, //get the search string
                    success: function (result) {
                       
                        if (result != null) {

                            if (result.indRankId > 0) {
                                vv.closest("tr").find(".RankName").val(result.indRankId);
                                vv.closest("tr").find("#IndlName").val(result.indlName);
                                vv.closest("tr").find("#Unit").val(result.unit);
                                vv.closest("tr").find("#Fmn").val(result.fmn);
                                vv.closest("tr").find("#EmailId").val(result.emailId);
                                vv.closest("tr").find("#PhoneNo").val(result.phoneNo);
                            }
                        }
                    }
                });
            });
            
            $('body').on('click', '.btnDelete', function () {

              
                Swal.fire({
                    title: 'Are you sure?',
                    text: "Do You Want To Delete this records",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, Delete it!'
                }).then((result) => {
                    if (result.isConfirmed) {

                        var Id = $(this).closest("tr").find("#spnId").val();;
                      

                        DeleteData(Id);

                    }
                })
            });

            $('body').on('click','.dob', function () {
                var d = new Date();

                var month = d.getMonth() + 1;
                var day = d.getDate();

                var output = d.getFullYear() + '-' +
                     (month < 10 ? '0' : '') + month + '-' +
                      (day < 10 ? '0' : '') + day;
             


                   
                $(this).closest("tr").find(".dob").attr({

                    "max": output
                   /* "min": '2022-04-20'*/
                });
                
            });

            $('body').on('click', '.btnsave', function () {

                //alert($(this).closest("td").find(".Userid").html());
                //alert($(this).closest("td").find(".Cls-Isplan").val());
                //alert($(this).closest("td").find(".EncId").html());
                //alert($(this).closest("td").find(".RankId").html());

                //var UserId = $(this).closest("td").find(".Userid").html();
                //var Plan = $(this).closest("td").find(".Cls-Isplan").val();
                //var EncId = $(this).closest("td").find(".EncId").html();
                //var RankId = $(this).closest("td").find(".RankId").html();
               

                var Save = 0;
                if ($(this).closest("tr").find("#Armyno").val() == "") {
                    $(this).closest("tr").find("#Armyno").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#Armyno").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#IndlName").val() == "") {
                    $(this).closest("tr").find("#IndlName").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#IndlName").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#Unit").val() == "") {
                    $(this).closest("tr").find("#Unit").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#Unit").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#Fmn").val() == "") {
                    $(this).closest("tr").find("#Fmn").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#Fmn").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#EmailId").val() == "") {
                    $(this).closest("tr").find("#EmailId").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#EmailId").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#PhoneNo").val() == "") {
                    $(this).closest("tr").find("#PhoneNo").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#PhoneNo").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#NameOfGest").val() == "") {
                    $(this).closest("tr").find("#NameOfGest").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#NameOfGest").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#Gender").val() == "") {
                    $(this).closest("tr").find("#Gender").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#Gender").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#Relation").val() == "") {
                    $(this).closest("tr").find("#Relation").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#Relation").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#Dob").val() == "") {
                    $(this).closest("tr").find("#Dob").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find("#Dob").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#AdhaorNo").val() == "" || $(this).closest("tr").find("#AdhaorNo").val().length<12) {
                    $(this).closest("tr").find("#AdhaorNo").addClass("border border-danger");
                    Save = 1;
                }
                if ($(this).closest("tr").find(".RankName").val() == "0") {
                    $(this).closest("tr").find(".RankName").addClass("border border-danger");
                    Save = 1;
                }
                else {
                    $(this).closest("tr").find(".RankName").removeClass("border border-danger");
                }
                if ($(this).closest("tr").find("#PhoneNo").val())
                    //alert($(this).closest("tr").find("#Armyno").val());

                    //alert("UserId-" + UserId + "EncId-" + EncId + "RankId-" + RankId + "Plan" + Plan );
                    //var 
                   // alert($(this).closest("tr").find("#AdhaorNo").val().length);
                if (Save == 1) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Something went wrong Please fill all fields!',
                        
                    })
                }
                else {
                    Swal.fire({
                        title: 'Are you sure?',
                        text: "You won't be " + $(this).closest("tr").find(".btnsave").html()+" this!",
                        icon: 'warning',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'Yes, Save it!'
                    }).then((result) => {
                        if (result.isConfirmed) {

                            var Id = $(this).closest("tr").find("#spnId").val();;
                            var ArmyNo = $(this).closest("tr").find("#Armyno").val();
                            var IndlName = $(this).closest("tr").find("#IndlName").val();
                            var Unit = $(this).closest("tr").find("#Unit").val();
                            var Fmn = $(this).closest("tr").find("#Fmn").val();
                            var EmailId = $(this).closest("tr").find("#EmailId").val();
                            var PhoneNo = $(this).closest("tr").find("#PhoneNo").val();
                            var NameOfGest = $(this).closest("tr").find("#NameOfGest").val();
                            var Gender = $(this).closest("tr").find("#Gender").val();
                            var Relation = $(this).closest("tr").find("#Relation").val();
                            var Dob = $(this).closest("tr").find("#Dob").val();
                            var AdhaorNo = $(this).closest("tr").find("#AdhaorNo").val();
                            var Photo = $(this).closest("tr").find("#spnimage").html();
                            var RankId = $(this).closest("tr").find(".RankName").val();
                            /* var IsPass = $(this).closest("tr").find("#checkboxPass").val();*/
                            var IsPass = false;
                            if ($(this).closest("tr").find(".checkboxPass").is(":checked")) {
                                IsPass = true;
                            }
                            else {
                                IsPass = false;
                            }
                          
                           
                            SaveData(Id, ArmyNo, IndlName, Unit, Fmn, EmailId, PhoneNo, NameOfGest, Gender, Relation, Dob, AdhaorNo, Photo, RankId, IsPass);
                           
                        }
                    })
                }

            });
          



        }
    });
}


function SaveData(Id, ArmyNo, IndlName, Unit, Fmn, EmailId, PhoneNo, NameOfGest, Gender, Relation, Dob, AdhaorNo, Photo, RankId, IsPass) {

    

    $.ajax({
        url: '/GestList/SaveData',
        type: 'POST',
        data: { "Id": Id, "ArmyNo": ArmyNo, "IndlName": IndlName, "Unit": Unit, "Fmn": Fmn, "EmailId": EmailId, "PhoneNo": PhoneNo, "NameOfGest": NameOfGest, "Gender": Gender, "Relation": Relation, "Dob": Dob, "AdhaorNo": AdhaorNo, "Photo": Photo, "IndRankId": RankId, "IsPass": IsPass }, //get the search string
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
                setTimeout(function () {
                    location.reload();
                }, 2000);
              
            }
            
        }
    });
}
function DeleteData(Id) {



    $.ajax({
        url: '/GestList/DeleteData',
        type: 'POST',
        data: { "Id": Id }, //get the search string
        success: function (result) {

            if (result == 4) {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'Something went wrong or Invalid Entry!',

                })
            }
            if (result == 1) {
                Swal.fire(
                    'Deleted!',
                    'Your Data has been Delete Successfully.',
                    'success'
                )

                location.reload();
            }

        }
    });
}

//function setDate() {
//    alert("Ajit");
//    $(".myDate").datepicker({ maxDate: new Date() });

//    $(".myDate").val();

//    $(".myDate").on("change", function () {
//        var selected = $(this).val();
//        alert(selected);
//    });
//   // $(".datepicker").datepicker({ maxDate: new Date() });
//}

function GetAllRank(Id, rankId) {
   
    var listItem = "";
    listItem = "<option value='0'>Select One</option>";
    $("#RankName").html(listItem);

    $.ajax({
        url: '/Rank/GetAll',
        type: 'POST',
        data: { }, //get the search string
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
                           

                            listItem += '<option value="' + response[i].id + '" selected>' + response[i].title + '</option>';
                        }
                        else {
                            listItem += '<option value="' + response[i].id + '">' + response[i].title + '</option>';
                        }
                    }

                    $("#" + Id).html(listItem);

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
