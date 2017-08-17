var _criteria_btnSearch = "#btnSearch";
var _criteria_btnSave = "#btnSave";
var _criteria_ddlSlicingField = "#ddlSlicingField";
var _criteria_ddlSlicingFieldValue = "#ddlSlicingFieldValue";
var _criteria_lblSearchstr = "#lblSearchstr";
var _criteria_ddlOperator = "#ddlOperator";
var _criteria_ddltype = "#ddltype";
var _criteria_divbuttons = "#divbuttons";
var _criteria_ddlTemplateName = "#ddlTemplateName";
var _criteria_lblTemplateName = "#lblTemplateName";
var _criteria_txtTemplateName = "#txtTemplateName";
var _criteria_divdbl = "#divdbl";
var _criteria_lnkNewTemplate = "#lnkNewTemplate";
var selectedSlicingVal = '';
var _criteria_chkSlice = "#chkSlice";
var _criteria_chkSource = "#chkSource"
var _criteria_divgridbox = "#divgridbox";
var _criteria_divSlicingFieldValue = "#divSlicingFieldValue";
var ddl, SlicingColumnValues;
$(function () {
    var baseUrl = '/';
    //baseUrl = $("#hdnfldVirtualDir").val();
    //var baseUrl = '@Url.Content("~/")';  
    var ctrlName = $("#hdnfldVirtualDir").val();
    var pathName = window.location.pathname;
    var relativePath = pathName.split('/')[1];
    baseUrl = ctrlName == relativePath ? baseUrl : baseUrl + relativePath + "/";

    var client_ID = $("#hdnClientId").val();
    var project_ID = $("#hdnProjectId").val();
    var ConfigID = $("#hdnConfigId").val();
    var ConstraintType = "PRIMARY KEY";


    $(_criteria_divbuttons).hide();
    $(_criteria_ddlTemplateName).hide();
    $(_criteria_lnkNewTemplate).hide();
    $(_criteria_divgridbox).hide();

    $(_criteria_ddltype).change(function () {
        console.log($(this).val());

    }).multipleSelect({
        width: '100%', position: 'bottom', filter: true, selectAll: false
    });

    PopulateTemplates(baseUrl, client_ID, project_ID);

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
    $(_criteria_ddlOperator).change(function () {
        ddlSlicingColumnValues();
        setMultipleSelect();
    });
    $(_criteria_btnSearch).click(function () {

        if ($(_criteria_txtTemplateName).val().trim() == null
            || $(_criteria_txtTemplateName).val().trim() == '' && !$(_criteria_ddlTemplateName).is(':visible')) {
            alert('Please enter template name');
            return;
        }

        ShowProgress();
        $(_criteria_divbuttons).show();
        $(_criteria_divgridbox).show();

        $(_criteria_lblSearchstr)
            .text("Note:Selected Criteria Slicing Field - '"
            + $(_criteria_ddlSlicingField).val() + "', Condition - '"
            + $(_criteria_ddlOperator).val() + "', Slicing Value - '"
            + $(_criteria_ddlSlicingFieldValue).val() + "' And Type(s) - '"
            + $(_criteria_ddltype).val() + "'");
        $(_criteria_lblSearchstr).show();

        var _Template_Name = $(_criteria_txtTemplateName).val();

        var _Expression = $(_criteria_ddlOperator).val();
        var _ObjType = $(_criteria_ddltype).multipleSelect("getSelects").join(",");

        GetTemplateDetails(client_ID, project_ID, _ObjType, _Template_Name, ConfigID, _Expression);
        HideProgress();
    });
    $(_criteria_ddlSlicingField).change(function () {
        if (this.value == '' || this.value == ' ' || this.value == null || this.value == undefined) return false;
        var _SlicingField = this.value;


        $.ajax({
            type: "GET",
            url: baseUrl + "api/DIMAPLUSAPI/GetSlicingColumnValues",
            data: { client_ID: client_ID, project_ID: project_ID, Column_name: _SlicingField, Config_ID: ConfigID },
            dataType: 'json',
            success: function (data) {
                SlicingColumnValues = data;
                ddlSlicingColumnValues();
                setMultipleSelect();

                //Set slicing value for selected template.
                if (selectedSlicingVal != '' || selectedSlicingVal != undefined) {
                    $(_criteria_ddlSlicingFieldValue).val(selectedSlicingVal);
                }
            },
            error: function (xhr) {
                alert(xhr.statusText);
            }
        });

    });
    $(_criteria_btnSave).click(function () {
        ShowProgress();
        //var mydata = $("#grdCriteria").jqGrid('getGridParam', 'data');

        //var CriteriaEntity = $("#grdCriteria").jqGrid('getRowData');
        var CriteriaEntity = $("#grdCriteria").jqGrid('getGridParam', 'data');

        $(CriteriaEntity).each(function (key, value) {
            value.ClientId = client_ID;
            value.ProjectId = project_ID;

            value.Criteria = value.Criteria ? 1 : 0;
            value.SourceDelete = value.SourceDelete ? 1 : 0;
            //value.Criteria = value.Criteria == "Yes" ? 1 : 0;
            //value.SourceDelete = value.SourceDelete == "Yes" ? 1 : 0;

            value.ObjectType = $(_criteria_ddltype).multipleSelect("getSelects").join(","); //$(_criteria_ddltype).val();
            value.SlicingField = $(_criteria_ddlSlicingField).find("option:selected").text();
            //value.SlicingFieldValue = $(_criteria_ddlSlicingFieldValue).val();

            //value.SlicingValue = $(_criteria_ddlSlicingFieldValue).val();
            value.SlicingValue = $(_criteria_ddlSlicingFieldValue).multipleSelect("getSelects").join(",");

            value.Condition = $(_criteria_ddlOperator).val();
            if (($(_criteria_ddlTemplateName).is(':visible'))) {
                value.Template = $(_criteria_ddlTemplateName).find("option:selected").text();
            }
            else {
                if ($(_criteria_txtTemplateName).val().trim() == null
           || $(_criteria_txtTemplateName).val().trim() == '' && !$(_criteria_ddlTemplateName).is(':visible')) {
                    alert('Please enter template name');
                    return;
                }
                value.Template = $(_criteria_txtTemplateName).val();
            }
        });
        $.ajax({
            type: "POST",
            url: baseUrl + "api/DIMAPLUSAPI/SaveUpdateCriteria",
            data: JSON.stringify(CriteriaEntity),
            contentType: "application/json",
            dataType: "json",
            success: function (data) {
                alert("Saved successfully.");
            },
            error: function (xhr) {
                alert(xhr.statusText);
            }
        });
        HideProgress();
    });

    $(_criteria_divdbl).dblclick(function () {
        $(_criteria_txtTemplateName).val('');

        if (!$(_criteria_ddlTemplateName).is(':visible')) {
            $(_criteria_ddlTemplateName).show();
            $(_criteria_txtTemplateName).hide();
            PopulateTemplates(baseUrl, client_ID, project_ID);
            $(_criteria_ddlTemplateName).focus();
            $(_criteria_lblTemplateName).text("Select Template");
            $(_criteria_lnkNewTemplate).show();
            var _Template_Name = $(this).find("option:selected").text();
            GetTemplateDetails(client_ID, project_ID, 'NA', _Template_Name, 0, 'NA');
            $(_criteria_divbuttons).show();
            $(_criteria_divgridbox).show();

        }
        else {
            $(_criteria_lblTemplateName).text("Enter Template Name*");
            $(_criteria_ddlTemplateName).hide();
            $(_criteria_txtTemplateName).show();
            $(_criteria_lnkNewTemplate).hide();
            $("#grdCriteria").GridUnload();
            $(_criteria_divgridbox).hide();
        }
    });
    $(_criteria_lnkNewTemplate).click(function () {
        $(_criteria_lblTemplateName).text("Enter Template Name");
        $(_criteria_ddlTemplateName).hide();
        $(_criteria_txtTemplateName).show();
        $(_criteria_txtTemplateName).val('');
        $(_criteria_lnkNewTemplate).hide();
        $(_criteria_txtTemplateName).focus();
        $("#grdCriteria").GridUnload();
        $(_criteria_divbuttons).hide();
        $(_criteria_divgridbox).hide();

    });

    $(_criteria_ddlTemplateName).change(function () {
        var _Template_Name = $(this).find("option:selected").text();
        GetTemplateDetails(client_ID, project_ID, 'NA', _Template_Name, ConfigID, 'NA');
        var dataFromTheRow = jQuery('#grdCriteria').jqGrid('getRowData', 1);

    });

    $(_criteria_chkSlice).change(function () {
        var cbxValue = "No";
        if ($(this).is(':checked'))
            cbxValue = "Yes";
        var rowsCount = jQuery("#grdCriteria").jqGrid('getGridParam', 'records');
        for (var rowId = 0; rowId < rowsCount; rowId++) {
            $("#grdCriteria").jqGrid("setCell", rowId, "Criteria", cbxValue);

            //$("#grdCriteria").setLabel("Criteria <input type='checkbox' id='chkSlice1'/>", "Criteria <input type='checkbox' id='chkSlice1' value=/>", { "font": "bold" });
        };
    })

    $(_criteria_chkSource).change(function () {
        var cbxValue = "No";
        if ($(this).is(':checked'))
            cbxValue = "Yes";
        var rowsCount = jQuery("#grdCriteria").jqGrid('getGridParam', 'records');
        for (var rowId = 0; rowId < rowsCount; rowId++) {
            $("#grdCriteria").jqGrid("setCell", rowId, "SourceDelete", cbxValue);
        };
    })

});
function GetTemplateDetails(_ClientId, _ProjectId, _ObjType, _Template_Name, ConfigID, _Expression) {

    var baseUrl = '/';
    //baseUrl = $("#hdnfldVirtualDir").val();
    var ctrlName = $("#hdnfldVirtualDir").val();
    var pathName = window.location.pathname;
    var relativePath = pathName.split('/')[1];
    baseUrl = ctrlName == relativePath ? baseUrl : baseUrl + relativePath + "/";

    //Get Columns
    var ColumnModel = [];
    var _Columns =
            $.ajax({
                url: baseUrl + "api/DIMAPLUSAPI/GetCriteria",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                data: { Type: $(_criteria_ddltype).val() },
                dataType: "json",
                async: false,
                success: function (data, result) { if (!result) alert('Failure to retrieve columns.'); },
                error: function (xhr) {
                    alert("Failed");
                }
            }).responseText;

    _Columns = jQuery.parseJSON(_Columns);
    var cols = _Columns.ColNames;
    var uFields = cols.split(',');
    var columns = [];
    for (var i = 0; i < uFields.length ; i++) {
        columns.push(uFields[i]);
    }
    var ColumnNames = columns;
    //for (var i = 0; i < ColumnNames.length; i++)
    //{
    //    if(ColumnNames[i]=="Criteria")
    //    {
    //        ColumnNames[i] = "Criteria <input type='checkbox' id='chkSlice1'/>";
    //    }
    //}
    var uFields = cols.split(',');
    var columns = [];


    for (var i = 0; i < uFields.length ; i++) {
        switch (uFields[i]) {
            case "Criteria":
                columns.push({
                    name: uFields[i], index: uFields[i], search: false, sortable: false, editable: true, align: 'center',
                    formatter: "checkbox", formatoptions: { disabled: false }, edittype: "checkbox", editoptions: {
                        value: "Yes:No", defaultValue: "Yes"
                    },
                    cellattr: function (rowId, value, rawObject, cm, rdata) {
                        if (rawObject[8] == "Ds")
                            cm.formatoptions.disabled = true;
                        //if (rawObject[8] == "Xn") cm.editoptions.defaultValue = "No";
                        //else cm.editoptions.defaultValue = "Yes";
                    }
                });
                break;
            case "SourceDelete":
                columns.push({
                    name: uFields[i], index: uFields[i], search: false, sortable: false, editable: true, align: 'center',
                    formatter: "checkbox", formatoptions: { disabled: false }, edittype: "checkbox", editoptions: {
                        value: "Yes:No", defaultValue: "Yes"
                    },
                    cellattr: function (rowId, value, rawObject, cm, rdata) {
                        //debugger;
                        if (rawObject[9] == "Y")
                            cm.formatoptions.disabled = true;

                        //if (rawObject[9] == "0")
                        //    cm.editoptions.defaultValue = "No";
                        //else cm.editoptions.defaultValue = "Yes";
                    }
                });
                break;
            case "Template_ID":
            case "Criteria_ID":
                columns.push({ name: uFields[i], index: uFields[i], search: false, sortable: false, editable: false, hidden: true });
                break;
            case "Objects":
                columns.push({ name: uFields[i], index: uFields[i], search: false, sortable: false, editable: false });
                break;
            case "ObjectType":
                columns.push({ name: uFields[i], index: uFields[i], search: false, sortable: false, editable: false, align: 'center' });
                break;
            default:
                columns.push({ name: uFields[i], index: uFields[i], search: false, sortable: false, editable: false, align: 'center', hidden: true });
                break;

        }
    }


    ColumnModel = columns;
    var mydata = [];
    var objType = []; // for getting unique values object type.

    var _Results =
            $.ajax({
                url: baseUrl + "api/DIMAPLUSAPI/GetCriteria",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                data: {
                    client_ID: _ClientId,
                    project_ID: _ProjectId,
                    Object_Type: _ObjType,
                    Template_Name: _Template_Name,
                    config_ID: ConfigID,
                    Column_name: $(_criteria_ddlSlicingField).val(),
                    //SlicingValue: $(_criteria_ddlSlicingFieldValue).val(),
                    SlicingValue: $(_criteria_ddlSlicingFieldValue).multipleSelect("getSelects").join(","),
                    Expression: _Expression
                },
                dataType: "json",
                async: false,
                success: function (data, result) {
                    if (!result) alert('Failure to retrieve columns.');

                },
                error: function (xhr) {
                    alert("Failed");
                }
            }).responseText;

    _Results = JSON.parse(_Results);

    for (var i = 0; i < _Results.rows.length; i++) {
        mydata.push(
            {
                id: i,
                Template_ID: _Results.rows[i].cell[0],
                Criteria_ID: _Results.rows[i].cell[1],
                Template: _Results.rows[i].cell[2],
                SlicingField: _Results.rows[i].cell[3],
                Condition: _Results.rows[i].cell[4],
                SlicingValue: _Results.rows[i].cell[5],
                ObjectType: _Results.rows[i].cell[6],
                Objects: _Results.rows[i].cell[7],
                Criteria: _Results.rows[i].cell[8], //false,
                SourceDelete: _Results.rows[i].cell[9]
            });
        if (objType.indexOf(_Results.rows[i].cell[6]) < 0) {
            objType.push(_Results.rows[i].cell[6]);
        }
    }

    //To fill all the fields for selected template.
    if (_Expression == 'NA' && mydata.length > 0) {

        $(_criteria_ddlSlicingField).val(mydata[0].SlicingField);
        $(_criteria_ddlOperator).val(mydata[0].Condition);
        selectedSlicingVal = mydata[0].SlicingValue;
        $(_criteria_ddlSlicingField).trigger('change');

        $(_criteria_ddltype).multipleSelect("setSelects", objType)

    }

    $("#grdCriteria").GridUnload();
    $grid = jQuery("#grdCriteria").jqGrid({
        data: mydata,
        datatype: 'local',
        height: 240,
        //width: 980,
        rowNum: 10,
        search: { caption: 'Search Record' },
        rowList: [5, 10, 20, 50],
        colNames: ColumnNames,
        colModel: ColumnModel,
        pager: '#pgrCriteria',
        viewrecords: true,
        sortable: true,
        autowidth: true,
        showButtonPanel: true,
        closeAfterEdit: true,
        caption: 'Search results',
        loadui: "block",
        loadtext: "Loading...",
        rownumbers: true,
        ignoreCase: true,
        autoencode: true,
        onSelectRow: function (rowId) { },
        gridComplete: function () {
            //var lista = jQuery("#grdCriteria").getDataIDs();            
            //var rowData = jQuery("#grdCriteria").getRowData(lista[0]);
        },
        loadComplete: function () {
        },
        beforeSelectRow: function (rowid, e) {
            var $self = $(this),
                iCol = $.jgrid.getCellIndex($(e.target).closest("td")[0]),
                cm = $self.jqGrid("getGridParam", "colModel"),
                localData = $self.jqGrid("getLocalRow", rowid);
            if (cm[iCol].name === "Criteria") {
                localData.Criteria = $(e.target).is(":checked");
            }
            if (cm[iCol].name === "SourceDelete") {
                localData.SourceDelete = $(e.target).is(":checked");
            }
            return true; // allow selection
        }
    }).navGrid('#pgrCriteria', { edit: false, add: false, del: false, search: false, refresh: false });
    //jQuery("#grdCriteria").jqGrid('navGrid', '#pgrCriteria', { edit: false, add: false, del: false, search: false, refresh: false });
    //jQuery("#grdCriteria").jqGrid('sortableRows');

}
function PopulateTemplates(baseUrl, client_ID, project_ID) {
    //Fill TemplateName field dropdown
    var templateNames = $.ajax({
        url: baseUrl + "api/DIMAPLUSAPI/GetAllTemplates",
        data: { client_ID: client_ID, project_ID: project_ID },
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
    $(_criteria_ddlTemplateName).find('option').remove();
    for (i = 0; i < tempNamelist.length; i++) {
        $(_criteria_ddlTemplateName).append($('<option>').text(tempNamelist[i].Template_Name).val(tempNamelist[i].Template_ID));
    }
}
function ddlSlicingColumnValues() {
    ddl = $('<select id="ddlSlicingFieldValue" multiple="multiple" class="dropdown" style="width: 99%;"><select />');
    $(_criteria_divSlicingFieldValue).html('');
    $(_criteria_ddlSlicingFieldValue + ' option').remove();
    $.each(SlicingColumnValues, function (index, value) {
        $('<option>').text(value).val(value).appendTo(ddl);
    });
    $(_criteria_divSlicingFieldValue).html(ddl);
}
function setMultipleSelect() {
    var isSingle = $(_criteria_ddlOperator).val() != 'IN' ? true : false;    
    $(_criteria_ddlSlicingFieldValue).change(function (event) {
        console.log($(this).val());
    }).multipleSelect({
        width: '100%', position: 'bottom', filter: true, selectAll: false, single: isSingle
    });
}
function ShowProgress() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modal");
        var loading = $(".loadingprogress");
        loading.show();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
    }, 200);
}
function HideProgress() {
    setTimeout(function () {
        var modal = $('<div />');
        modal.addClass("modal");
        var loading = $(".loadingprogress");
        loading.hide();
        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
        loading.css({ top: top, left: left });
    }, 200);
}