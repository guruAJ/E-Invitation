var memberTable;
$(document).ready(function () {
    GetAllData(1);

});


function GetAllData(Id) {
    var listItem = "";

 
    $.ajax({
        url: '/GestList/GetGestPass',
        type: 'POST',
        data: { "OcassionId": Id }, //get the search string
        success: function (result) {
         
            //listItem += "<th><span class='mdi mdi-arrow-right'>Enclosure</span><br><span class='mdi mdi-arrow-right'>Rank<br><span class='mdi mdi-arrow-right'>Vacancies</span></th>";
            //listItem += "<th></th>";
           
          
            for (var i = 0; i < result.length; i++) {
                var color = result[i].enclosureColor;
                listItem += '<div class="col-md-8 grid-margin stretch-card">';
                listItem += '<div class="card">';
                listItem += '<div class="card-body">';
                listItem += '<div class="row">';
                listItem += '<div class="col-md-6 mb-3 text-right"><div class="mr-4">';
                listItem += '<img src="/images/Profile/' + result[i].photo+'" width="200px" height="150px" class="text-center" />';
                listItem += '</div></div>';
                listItem += '<div class="col-md-6 mb-3">';
                listItem += '<img src="/images/qrcode.png" width="200px" height="150px" class="text-center" />';
                listItem += ' </div>';
                listItem += '<div class="col-md-6 mb-3 text-right">';
                listItem += '<div class="mr-4">';
                listItem += '<img src="/images/armylogo.jpg" width="200px" height="150px" class="text-center" />';
                listItem += '</div>';
                listItem += '</div>';
                listItem += '<div class="col-md-6 mb-3 mx-auto">';
                listItem += '<div style="background-color:' + color+';color:white;width:200px;height:150px" class="text-center align-middle"><br /><br />';
                listItem += '<span class="font-weight-800 f27">' + result[i].enclosureName+'</span>';
                listItem += '<h4 style="color:white">' + result[i].ocassionDate +'</h4>';
                listItem += '</div></div></div>';
                listItem += '<div class="row"><div class="col-md-12 mt-3 text-center">';
                listItem += '<h1 style="font-family:Gabriola; font-size:50px;font-weight:900">';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '';
                listItem += '</div></div></div>';
               
            }

            $("#Allpass").html(listItem);
            /*$("#lblTotal").html(userlist.length);*/
          




        }
    });
}


