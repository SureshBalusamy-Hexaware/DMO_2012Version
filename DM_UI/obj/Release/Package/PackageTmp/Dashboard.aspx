<%@ Page Title="" Language="C#" MasterPageFile="~/Dashboard.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DM_UI.Dashboard1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {

            var showRpt = $('#<%=hdnRpt.ClientID%>').val();

            showRpt == '1' ? $('.frame').show() : $('.frame').hide();

            //$("#testLoad").hide();

            $('#lnkTgtSpt').click(function () {

                $("#Menu li").each(function (i, e) {
                    $(this).removeClass('ui-tabs-selected')
                })

                $("#liTgtSpt").addClass('ui-tabs-selected');

                $("#testLoad").show();

                $('#<%=rptViewer.ClientID%>').hide();
                $('.frame').hide()
                $('.rpt').hide();
            });


            <%-- $(".frame").hide();
            $("#lnkAuditLog").click(function () {

                $("#Menu li").each(function (i, e) {
                    $(this).removeClass('ui-tabs-selected')
                })

                $("#<%=liAuditLog.ClientID%>").addClass('ui-tabs-selected');

                $('.frame').show();
                $('.rpt').hide();
                $('#<%=rptViewer.ClientID%>').hide();

            })--%>
        });

    </script>

    <style type="text/css">
        .frame {
            text-align: center;
            padding-left: 5px;
        }

            .frame iframe {
                position: absolute;
                margin: auto;
                width: 99%;
                height: 67%;
                border-width: 0px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_mid contact" id="divMenu">
        <div class="ui-tabs">
            <ul id="Menu" class="ui-tabs-nav" style="font-weight: normal;">
                <asp:Repeater ID="rptParentMenu" runat="server">
                    <ItemTemplate>
                        <li id="li" runat="server" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                            <asp:LinkButton ID="lnkbtnParentMenu" runat="server"
                                Text='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).MenuName.ToUpper() %>'
                                ToolTip='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).MenuName %>'
                                MenuID='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).MenuId %>'
                                MenuName='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).ReportPath %>'
                                ServerPath='<%#((DM_BusinessEntities.MenuEntity)Container.DataItem).Url %>'
                                OnClick="lnkbtnParentMenu_Click"></asp:LinkButton>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
                <%--<li id="liTgtSpt" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                    <a id="lnkTgtSpt">THOUGHT SPOT</a>
                </li>--%>
                <%--                
                <li id="liDataProfiler" runat="server" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                    <asp:LinkButton ID="lnkbtnDataProfiler" runat="server" OnClick="lnkbtnDataProfiler_Click" Text="DATA PROFILER"></asp:LinkButton>
                </li>
                <li id="liHexarule" runat="server" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                    <asp:LinkButton ID="lnkbtnHexarule" runat="server" OnClick="lnkbtnHexarule_Click" Text="HEXA RULE"></asp:LinkButton>
                </li>
                <li id="liDIMAPlus" runat="server" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                    <asp:LinkButton ID="lnkbtnDimaPlus" runat="server" OnClick="lnkbtnDimaPlus_Click" Text="DASEM"></asp:LinkButton>
                </li>
                <li id="liDatarecon" runat="server" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                    <asp:LinkButton ID="lnkbtnDatarecon" runat="server" OnClick="lnkbtnDatarecon_Click" Text="DATA RECON"></asp:LinkButton>
                </li>
                <li id="liDart" runat="server" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                    <asp:LinkButton ID="lnkbtnDart" runat="server" OnClick="lnkbtnDart_Click" Text="DART"></asp:LinkButton>
                </li>                

                <li id="liAutomaton" runat="server" class="ui-state-default ui-corner-top" style="width: 13.9%;">
                    <asp:LinkButton ID="lnkbtnAutomaton" runat="server" OnClick="lnkbtnAutomaton_Click" Text="AUTOMATON"></asp:LinkButton>
                </li>
                
               
                <li id="liDima" runat="server" class="ui-state-default ui-corner-top" style="width: 13.9%;">
                    <asp:LinkButton ID="lnkbtnDima" runat="server" OnClick="lnkbtnDima_Click" Text="DIMA"></asp:LinkButton>
                </li>--%>
                <%--<li id="liAuditLog" runat="server" class="ui-state-default ui-corner-top shadow" style="width: 13.9%;">
                    <a id="lnkAuditLog">AUDIT LOG (Tableau Report)</a>
                </li>--%>
            </ul>
        </div>

    </div>

    <div class="content-body" style="width: 100%;">
        <div class="group">
            <div class="form-group width-per-100">
                <div class="rpt">
                    <ul>
                        <asp:Repeater ID="rptReports" runat="server">
                            <ItemTemplate>
                                <li>
                                    <asp:LinkButton ID="lnkbtnReports" runat="server"
                                        Text='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).MenuName %>'
                                        ToolTip='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).MenuName %>'
                                        RptID='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).MenuId %>'
                                        RptName='<%# ((DM_BusinessEntities.MenuEntity)Container.DataItem).ReportPath %>'
                                        ServerPath='<%#((DM_BusinessEntities.MenuEntity)Container.DataItem).Url %>'
                                        OnClick="lnkbtnReports_Click"></asp:LinkButton>
                                    <span class="devider"></span>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ul>
                </div>
            </div>


            <div class="form-group width-per-100">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <rsweb:ReportViewer ID="rptViewer" Font-Names="Verdana" Font-Size="8pt" Height="400px"
                    ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%"
                    runat="server" Visible="false">
                </rsweb:ReportViewer>
            </div>

        </div>
    </div>
    <asp:HiddenField ID="hdnRpt" runat="server" />
</asp:Content>
