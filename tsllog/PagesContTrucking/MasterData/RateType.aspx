<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RateType.aspx.cs" Inherits="PagesContTrucking_MasterData_RateType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                <ClientSideEvents Click="function(s,e){
                                gv.AddNewRow();
                                }" />
            </dxe:ASPxButton>
            <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RateType" KeyMember="Id" />
            <dxwgv:ASPxGridView ID="gv" runat="server" Width="100%" KeyFieldName="Id" ClientInstanceName="gv" AutoGenerateColumns="false" DataSourceID="dsRateType"
                OnInit="gv_Init" OnInitNewRow="gv_InitNewRow" OnRowInserting="gv_RowInserting" OnRowUpdating="gv_RowUpdating" OnRowDeleting="gv_RowDeleting">
                <SettingsPager Mode="ShowPager" PageSize="20">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="#" Width="2%">
                        <EditButton Text="Edit" Visible="true"></EditButton>
                        <DeleteButton Text="Delete" Visible="true"></DeleteButton>
                    </dx:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" Width="10%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="10%">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gv">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
}" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
