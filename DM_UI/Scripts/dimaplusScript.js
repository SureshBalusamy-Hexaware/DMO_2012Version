$(function () {
    //var baseUrl = '@Url.Content("~/")';
    var baseUrl = '/';
    var client_ID = $("#hdnClientId").val();
    var project_ID = $("#hdnProjectId").val();
    var ConfigID = $("#hdnConfigId").val();
    var ConstraintType = "PRIMARY KEY";

    var _criteria_btnSearch = "#btnSearch";
    var _criteria_ddlSlicingField = "#ddlSlicingField";
    var _criteria_ddlSlicingFieldValue = "#ddlSlicingFieldValue";
    var _criteria_lblSearchstr = "#lblSearchstr";
    var _criteria_ddltype = "#ddltype";

    //Fill source slicing field dropdown    
    $.ajax({
        type: "GET",
        url: baseUrl + "api/DIMAPLUSAPI/GetSlicingColumns",
        data: { client_ID: client_ID, project_ID: project_ID, constraint_Type: ConstraintType, config_ID: ConfigID },
        dataType: 'json',
        success: function (data) {
            $.each(data, function (index, value) {
                $('#ddlSlicingField').append($('<option>').text(value).val(value));
            });
            $(_criteria_ddlSlicingField).change();
        },
        error: function (xhr) {
            alert(xhr.statusText);
        }
    });
    //


    $(_criteria_btnSearch).click(function () {
        $(_criteria_lblSearchstr).text("Note:Selected Criteria Slicing Field - '"
            + $(_criteria_ddlSlicingField).val() + "', Slicing Value - '"
            + $(_criteria_ddlSlicingFieldValue).val() + "' And Type - '"
            + $(_criteria_ddltype).val() + "'");

        $("#grdCriteria").GridUnload();
        $grid = jQuery("#grdCriteria").jqGrid({
            postData: { client_ID: ClientId, project_ID: ProjectId },
            mytype: 'GET',
            datatype: 'json',
            url: ServiceUrl,
            editurl: baseUrl + 'Hexarule/RuleUpdate',            
            height: 250,
            width: 900,
            autowidth: true,
            shrinkToFit: true,
            reloadAfterSubmit: true,
            rowNum: 10,
            rowList: [10, 20, 30],
            //sortname: 'Rulename',
            colNames: ['Table Name', 'Criteria', 'Source Delete'],
            colModel: [
            { key: false, name: 'Tablename', index: 'Tablename', editable: false, width: 40 },
            { key: false, name: 'Criteria', index: 'Criteria', editable: true, width: 40, columntype: 'checkbox' },
            { key: false, name: 'SourceDelete', index: 'SourceDelete', editable: true, width: 20, columntype: 'checkbox' }
            ],
            pager: '#pgrCriteria',
            scrollOffset: 0,
            viewrecords: true,
            viewsortcols: true,
            loadonce: true,
            gridview: true,
            sortorder: 'asc',
            reloadAfterSubmit: true,
            //showButtonPanel:true,
            onSelectRow: function (rowId) {

            },
            gridComplete: function () {

            },
            loadComplete: function () {

            },
            // editurl: "UpdateRuleServlet",
            caption: 'Criteria'
        });
        jQuery("#grdCriteria").jqGrid('navGrid', '#pgrCriteria', { edit: true, add: false, del: false, search: false, refresh: false });
        jQuery("#grdCriteria").jqGrid('sortableRows');

    });

    $(_criteria_ddlSlicingField).change(function () {
        var _SlicingField = this.value;
        $.ajax({
            type: "GET",
            url: baseUrl + "api/DIMAPLUSAPI/GetSlicingColumnValues",
            data: { client_ID: client_ID, project_ID: project_ID, Column_name: _SlicingField, Config_ID: ConfigID },
            dataType: 'json',
            success: function (data) {
                $(_criteria_ddlSlicingFieldValue + ' option').remove();
                $.each(data, function (index, value) {
                    $(_criteria_ddlSlicingFieldValue).append($('<option>').text(value).val(value));
                });
            },
            error: function (xhr) {
                alert(xhr.statusText);
            }
        });

    });
});