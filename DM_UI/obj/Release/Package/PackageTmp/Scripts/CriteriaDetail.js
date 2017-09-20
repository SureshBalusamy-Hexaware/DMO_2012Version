$(function () {
    var baseUrl = '/';
    //baseUrl = $("#hdnfldVirtualDir").val();
    var ctrlName = $("#hdnfldVirtualDir").val();
    var pathName = window.location.pathname;
    var relativePath = pathName.split('/')[1];
    baseUrl = ctrlName == relativePath ? baseUrl : baseUrl + relativePath;

    $grid = jQuery("#grdCriteriaDetail").jqGrid({
        mtype: 'GET',
        url: baseUrl + "api/DIMAPLUSAPI/GetAllCriteria",
        datatype: 'json',
        height: 200,
        //width: 100,
        rowNum: 10,
        search: { caption: 'Verfiy criteria' },
        rowList: [5, 10, 20, 50],
        colNames: ['Object Name', 'Slicing Field', 'Slicing Value', 'Criteria', 'Source Delete'],
        colModel: [
            { name: 'ObjectName', search: false, sortable: true, editable: false, align: 'center', width: 20 },
            { name: 'SlicingField', search: false, sortable: true, editable: false, align: 'center', width: 20 },
            { name: 'SlicingValue', search: false, sortable: true, editable: false, align: 'center', width: 20 },
            {
                name: 'Criteria', search: false, sortable: false, editable: true, align: 'center', width: 20, formatter: "checkbox",
                formatoptions: { disabled: false }, edittype: "checkbox", editoptions: { value: "Yes:No", defaultValue: "Yes" },
                cellattr: function (rowId, value, rawObject, cm, rdata) {
                    //if (rawObject[1] == "Ds")
                    //    cm.formatoptions.disabled = true;

                    if (rawObject[1] == "Xn") cm.editoptions.defaultValue = "No";
                    else cm.editoptions.defaultValue = "Yes";
                }
            },
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
        pager: '#pgrCriteriaDetail',
        viewrecords: true,
        sortable: true,
        autowidth: true,
        showButtonPanel: true,
        closeAfterEdit: true,
        caption: 'Search results',
        scrollOffset: 0,
        onSelectRow: function (rowId) {
        },
    }).navGrid('#pgrCriteriaDetail', { edit: false, add: false, del: false, search: false });

});