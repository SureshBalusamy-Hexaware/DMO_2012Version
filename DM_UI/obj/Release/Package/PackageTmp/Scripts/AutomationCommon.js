
//----------------------------------------------------------------------Automation-----------------------------------------------------------


function LoadDataTemplate(value, callback) {

    $.ajax({
        type: "GET",
        url: baseUrl + "api/AutomatAPI/GetTemplateList",
        data: { client_Id: $("#hdnClientId").val(), Project_Id: $("#hdnProjectId").val(), Type: value },
        dataType: 'json',
        success: callback
    });
}

function LoadSourceTarget(value, callback) {

    $.ajax({
        type: "GET",
        url: baseUrl + "api/AutomatAPI/GetMetaDataConnectionList",
        data: { Client_ID: $("#hdnClientId").val(), Project_ID: $("#hdnProjectId").val(), Tool_ID: $("#hdnToolID").val(), SourceTarget: value },
        dataType: 'json',
        success: callback
    });
}

function GetTransformationType() {
    
    var length = $('#selecttransformation > option').length;
    if(length == 0)
    {

        $.ajax({
            type: "GET",
            url: baseUrl + "api/AutomatAPI/GetTransformationType",
            data: { Client_ID: $("#hdnClientId").val(), Project_ID: $("#hdnProjectId").val(), Tool_ID: $("#hdnToolID").val() },
            dataType: 'json',
            success: function (data) {
                $('#selecttransformation').html('');
                $('#selecttransformation').append($('<option>').text('Select').val('Select'));

                $.each(data, function (index, value) {
                    var TransDesc = value.split(':');
                    var transtype = TransDesc[0];
                    var tansid = TransDesc[1];
                    $('#selecttransformation').append($('<option>').text(transtype).val(transtype).attr("Key", tansid));
                });
                $("#btnrulehower").attr("title", $('#selecttransformation option:selected').attr("Key")); //first time assign.
            }
        });
    }

}



