$(function () {
    var baseUrl = '/';
    //baseUrl = $("#hdnfldVirtualDir").val();
    var ctrlName = $("#hdnfldVirtualDir").val();
    var pathName = window.location.pathname;
    var relativePath = pathName.split('/')[1];
    baseUrl = ctrlName == relativePath ? baseUrl : baseUrl + relativePath+"/";

    var _purge_btnProcess = "#btnProcess";
    
    var client_ID = $("#hdnClientId").val();
    var project_ID = $("#hdnProjectID").val();
    var _purge_ddlTemplateName = "#ddlTemplateName";
    var _purge_ddlRunID = "#ddlRunId";

    //Fill TemplateName field dropdown
    var templateNames = $.ajax({
        url: baseUrl + "api/DIMAPLUSAPI/GetAllTemplates",
        data: {
            client_ID: client_ID
            , project_ID: project_ID
        },
        async: false,
        success: function (data, result) {
            if (!result)
                alert('Failure to retrieve the existing template names.');
        },
        error: function (err) {

            alert(err.statusText);
        }
    }).responseText;
    //debugger;
    var tempNamelist = jQuery.parseJSON(templateNames);

    for (i = 0; i < tempNamelist.length; i++) {
        $(_purge_ddlTemplateName).append($('<option>').text(tempNamelist[i].Template_Name).val(tempNamelist[i].Template_ID));
    }

    $(_purge_ddlTemplateName).change(function () {

        //Fill TemplateName field dropdown
        var runIDs = $.ajax({
            url: baseUrl + "api/DIMAPLUSAPI/GetRunIDList",
            data: {
                client_ID: client_ID
                , project_ID: project_ID
                , template_ID: $(this).find("option:selected").val()
            },
            async: false,
            success: function (data, result) {
                if (!result)
                    alert('Failure to retrieve the existing runIds.');
            },
            error: function (err) {

                alert(err.statusText);
            }
        }).responseText;
        //debugger;
        var runIdlist = jQuery.parseJSON(runIDs);
        $(_purge_ddlRunID).html('');
        $(_purge_btnProcess).hide();
        //$(_purge_ddlRunID).append($('<option>').text('--Select--').val(-1));
        for (i = 0; i < runIdlist.length; i++) {
            $(_purge_ddlRunID).append($('<option>').text(runIdlist[i].Run_ID).val(runIdlist[i].Run_ID));
        }
        $(_purge_ddlRunID).change();
    });
        //////////
    $(_purge_ddlRunID).change(function () {

        $("#grdPurge").GridUnload();

        $grid = jQuery("#grdPurge").jqGrid({
            mtype: 'GET',
            url: baseUrl + "api/DIMAPLUSAPI/GetDeleteList",
            postData: { TemplateId: $(_purge_ddlTemplateName).val(), Run_ID: $(_purge_ddlRunID).val() },
            datatype: 'json',
            height: 200,
            //width: 100,
            rowNum: 10,
            //search: { caption: 'Pur' },
            rowList: [5, 10, 20, 50],
            colNames: ['Criteria_ID', 'Objects', 'Slicing Field', 'Slicing Value', 'Source Delete'],
            colModel: [
                { name: 'Criteria_ID', search: false, sortable: true, editable: false, hidden: true, key: true },
                { name: 'Objects', search: false, sortable: true, editable: false, align: 'center', width: 20 },
                { name: 'SlicingField', search: false, sortable: true, editable: false, align: 'center', width: 20 },
                { name: 'SlicingFieldValue', search: false, sortable: true, editable: false, align: 'center', width: 20 },
                //{
                //    name: 'Criteria', search: false, sortable: false, editable: true, align: 'center', width: 20, formatter: "checkbox",
                //    formatoptions: { disabled: false }, edittype: "checkbox", editoptions: { value: "Yes:No", defaultValue: "Yes" },
                //    cellattr: function (rowId, value, rawObject, cm, rdata) {
                //        //if (rawObject[1] == "Ds")
                //        //    cm.formatoptions.disabled = true;

                //        if (rawObject[1] == "Xn") cm.editoptions.defaultValue = "No";
                //        else cm.editoptions.defaultValue = "Yes";
                //    }
                //},
                {
                    name: 'SourceDelete', search: false, sortable: false, editable: true, align: 'center', width: 20,
                    formatter: "checkbox", formatoptions: { disabled: false }, edittype: "checkbox", editoptions: { value: "Yes:No", defaultValue: "Yes" },
                    cellattr: function (rowId, value, rawObject, cm, rdata) {
                        //if (rawObject[2] == "Y")
                        //    cm.formatoptions.disabled = true;

                        if (rawObject[2] == "0")
                            cm.editoptions.defaultValue = "No";
                        else cm.editoptions.defaultValue = "Yes";
                    }
                }
            ],
            pager: '#pgrPurge',
            viewrecords: true,
            sortable: true,
            autowidth: true,
            showButtonPanel: true,
            closeAfterEdit: true,
            caption: 'Purge Details',
            //scrollOffset: 0,
            onSelectRow: function (rowId) {
            },
        }).navGrid('#pgrPurge', { edit: false, add: false, del: false, search: false });
        $(_purge_btnProcess).show();
    });
    $(_purge_btnProcess).click(function () {
        ShowProgress();
        var CriteriaEntity = $("#grdPurge").jqGrid('getRowData');
        for (var i = 0; i < CriteriaEntity.length; i++)
        {
            CriteriaEntity[i].Run_ID = $(_purge_ddlRunID).find("option:selected").val();
            CriteriaEntity[i].Template_ID = $(_purge_ddlTemplateName).find("option:selected").val();
        }
        // JQUERY TO SELECT ONLY PARTICULAR RUNID
        $.ajax({
            type: "POST",
            url: baseUrl + "api/DIMAPLUSAPI/PurgeData",
            data: JSON.stringify(CriteriaEntity),
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                if (data == "" || data == undefined)
                {
                    alert('No records found.');
                    return;
                }
                $('#grdPurge').trigger('reloadGrid');
                alert(data);
            },
            error: function (xhr) {
                alert(xhr.statusText);
            }
        });
        HideProgress();
    });
    $(_purge_ddlTemplateName).change();
});