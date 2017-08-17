
//----------------------------------------------------------------------Automation-----------------------------------------------------------

function GetTemplateList() {
    $.ajax({
        type: "GET",
        url: baseUrl + "api/AutomatAPI/GetTemplateList",
        data: { client_Id: $("#hdnClientId").val(), Project_Id: $("#hdnProjectId").val(), Type: "Transformation" },
        dataType: "json",
        success: function (Result) {
            $('#select_existing_Template').append($('<option>').text("Select"));
            $.each(Result, function (index, value) {
                var template = value.split(':');
                var tempname = template[0];
                var tempid = template[1];
                if (tempname != '')
                    $('#select_existing_Template').append($('<option>').text(tempname).val(tempid));
            });
        }
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

