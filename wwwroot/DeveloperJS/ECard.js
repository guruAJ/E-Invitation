var memberTable;
$(document).ready(function () {

   
    $("#OcassionId").change(function () {
        $("#btnAddNew").removeClass("d-none");
        DataBind();
    });
    $("#btnAddNew").click(function () {
        $("#spnInsId").html("0");
        $(".imgUp1").find('.imagePreview1').css("background-image", "url()");
        $(".imgUp2").find('.imagePreview2').css("background-image", "url()");
        $(".imgUp3").find('.imagePreview3').css("background-image", "url()");
        $("#AddNew").modal("show");
    });
    $("#btnsave").click(function () {

        if ($("#OcassionId").val() == "-1") {
            $("#spanmsg").html("Please Select Events");
        }
        else if ($("#spnimage1").html() == "") {
            $("#spanmsg").html("Please Upload Cover Page");
        }
        else if ($("#spnimage2").html() == "") {
            $("#spanmsg").html("Please Upload Back Page");
        }
        else if ($("#spnimage3").html() == "") {
            $("#spanmsg").html("Please Upload Root Info");
        }
        else {
            Save();
        }
    });




    $('body').on("change", ".uploadFile1", function () {
        var uploadFile = $(this);
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div
                //alert(uploadFile.closest(".upimage").find('.imagePreview').length);
                uploadFile.closest(".imgUp1").find('.imagePreview1').css("background-image", "url(" + this.result + ")");


                // uploadFile.closest(".imgUp").find('#spnimage').html("Ok");
            }
            var OutFile = uploadFile.get(0).files;

           
            UploadFile(OutFile,"spnimage1")
           
        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Only image file!',

            })
        }
    });


    $('body').on("change", ".uploadFile2", function () {
        var uploadFile = $(this);
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div
                //alert(uploadFile.closest(".upimage").find('.imagePreview').length);
                uploadFile.closest(".imgUp2").find('.imagePreview2').css("background-image", "url(" + this.result + ")");


                // uploadFile.closest(".imgUp").find('#spnimage').html("Ok");
            }
            var OutFile = uploadFile.get(0).files;


            UploadFile(OutFile, "spnimage2")

        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Only image file!',

            })
        }
    });

    $('body').on("change", ".uploadFile3", function () {
        var uploadFile = $(this);
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div
                //alert(uploadFile.closest(".upimage").find('.imagePreview').length);
                uploadFile.closest(".imgUp3").find('.imagePreview3').css("background-image", "url(" + this.result + ")");


                // uploadFile.closest(".imgUp").find('#spnimage').html("Ok");
            }
            var OutFile = uploadFile.get(0).files;


            UploadFile(OutFile, "spnimage3")

        }
        else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Only image file!',

            })
        }
    });
});

function UploadFile(OutFile,spanname) {

    var data = new FormData();
    data.append("files", OutFile[0]);

    $.ajax({
        url: '/ECard/Compare',
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
                    $("#" + spanname).html(response);
                    // $(this).closest("tr").find("#Armyno").val()
                    //  uploadFile.closest(".imgUp").find($('#spnimage').html(response));
                }
            }

        },
        error: function (result) {


        }
    });
}


function Save() {


    $.ajax({
        url: '/ECard/Save',
        type: 'POST',
        data: { "Id": $("#spnInsId").html(),"OcassionId": $("#OcassionId").val(), "Card1": $("#spnimage1").html(), "Card2": $("#spnimage2").html(), "Card3": $("#spnimage3").html() }, //get the search string
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

                    DataBind();
                    $("#AddNew").modal("hide");
                }
            }

        },
        error: function (result) {

        }
    });
}
function DataBind()
{
    var listItem = "";

    $("#DetailBody").html(listItem);
    $.ajax({
        url: '/ECard/GetAll',
        type: 'POST',
        data: { "OcassionId": $("#OcassionId").val() }, //get the search string
        success: function (result) {
            $("#tbl_tbl_Detail").DataTable().destroy(); //Newly added
            var count = 1;
            for (var i = 0; i < result.length; i++) {
                listItem += "<tr>";
                //listItem += "<td class='d-none'><span id='spnInsId' class='d-none'>" + result[i].id + "</span></td>";
                //listItem += "<td>";
                //listItem += "<div class='custom-control custom-checkbox small'>";
                //listItem += "<input type='checkbox' class='custom-control-input' id='" + result[i].id + "'>";
                //listItem += "<label class='custom-control-label' for='" + result[i].id + "'></label>";
                //listItem += "</div>";
                //listItem += "</td>";
               /* listItem += "<td >" + count + "</td>";*/
                listItem += "<td><span class='d-none' id='spnInsId'>" + result[i].id +"</span><span class='d-none' id='card1'>" + result[i].card1+"</span><div class='imagePreview1' style='background-image: url(/images/ECard/" + result[i].card1 + ")'></div></td>";
                listItem += "<td><span class='d-none' id='card2'>" + result[i].card2 +"</span><div class='imagePreview1' style='background-image: url(/images/ECard/" + result[i].card2 + ")'></div></td>";
                listItem += "<td><span class='d-none' id='card3'>" + result[i].card3 +"</span><div class='imagePreview1' style='background-image: url(/images/ECard/" + result[i].card3 + ")'></div></td>";
                listItem += "<td ><button class='btn btn-info btn-icon-text btn-sm mb-1 btnEdit'>Edit</button></td>";
                listItem += "</tr>";
                count++;

                $("#btnAddNew").addClass("d-none");
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
            $('body').on('click', '.btnEdit', function () {

                //alert($(this).closest("tr").find("#card1").html());
                $("#spnimage1").html($(this).closest("tr").find("#card1").html());
                $("#spnimage2").html($(this).closest("tr").find("#card2").html());
                $("#spnimage3").html($(this).closest("tr").find("#card3").html());
                $(".imgUp1").find('.imagePreview1').css("background-image", "url(/images/ECard/" + $(this).closest("tr").find("#card1").html() + ")");
                $(".imgUp2").find('.imagePreview2').css("background-image", "url(/images/ECard/" + $(this).closest("tr").find("#card2").html() + ")");
                $(".imgUp3").find('.imagePreview3').css("background-image", "url(/images/ECard/" + $(this).closest("tr").find("#card3").html() + ")");
                
                $("#spnInsId").html($(this).closest("tr").find("#spnInsId").html());
                $("#AddNew").modal("show");
              
            });

            //memberTablemap.buttons().container().appendTo('#tbl_tbl_Detail_wrapper .col-md-6:eq(0)');


        }
    });
}