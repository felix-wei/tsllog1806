<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertRule.aspx.cs" Inherits="PagesMaster_AlertRule" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v13.2, Version=13.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v13.2, Version=13.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsSysAlertRule" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SysAlertRule" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl_Code" runat="server" Text="Code"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Code" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                    <td></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsSysAlertRule" Width="1500"
                KeyFieldName="Id" OnInit="grid_Init" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating"
                OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnCellEditorInitialize="grid_CellEditorInitialize" OnCustomDataCallback="grid_CustomDataCallback"
                OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="True">
                        </DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="Code" Caption="Code" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="MasterId" Caption="Master Id" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="AlertSql" Caption="SQL" Width="400" PropertiesMemoEdit-Rows="3"></dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="AlertColumns" Caption="Columns(split by , )" Width="100" PropertiesMemoEdit-Rows="3">
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataColumn FieldName="AlertTo" Caption="To" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="AlertCc" Caption="CC" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="AlertBcc" Caption="Bcc" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="AlertMobile" Caption="Mobile" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="AlertSubject" Caption="Subject ({{Subject}})" Width="230" PropertiesMemoEdit-Rows="3">
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="AlertBody" Caption="Body({{Body}})" Width="300" PropertiesMemoEdit-Rows="3">
                        <EditItemTemplate>
                            <dx:ASPxHtmlEditor ID="ASPxHtmlEditor1" runat="server" Html='<%# Bind("AlertBody") %>'>
                            </dx:ASPxHtmlEditor>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataColumn FieldName="AlertSms" Caption="SMS" Width="120"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="AlertSql" ShowInColumn="AlertSql"
                        SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
