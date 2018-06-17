<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function LockJob(partyId) {
            grid.GetValuesOnCustomCallback(partyId, onCallback)
        }
        function onCallback(v) {
            grid.Refresh(); 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div STYLE="DISPLAY:NONE">
        
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="PartyId"
                AutoGenerateColumns="False" OnHtmlDataCellPrepared="grid_HtmlDataCellPrepared" OnHtmlRowPrepared="grid_HtmlRowPrepared" OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowAllRecords" >
                </SettingsPager>
                <SettingsBehavior AllowSort="false" AllowDragDrop="false" AllowGroup="false" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Short Name" FieldName="Code" Width="200" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Full Name" FieldName="Name" Width="200" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WarningQty" FieldName="WarningQty" Width="80" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WarningAmt" FieldName="WarningAmt" Width="80" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="BlockQty" FieldName="BlockQty" Width="80" VisibleIndex="6">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="BlockAmt" FieldName="BlockAmt" Width="80" VisibleIndex="7">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Actual Qty" FieldName="Cnt" Width="80" VisibleIndex="21">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Balance Amt" FieldName="Amt" Width="80" VisibleIndex="22">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" Width="80" VisibleIndex="22">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Status" Width="80" VisibleIndex="22">
                        <DataItemTemplate>
                            <dxe:ASPxButton ID="btn_block" runat="server" Width="60" Enabled='<%# EzshipHelper.GetUseRole()=="Admin"||EzshipHelper.GetUseRole()=="Account" %>' Text='<%# Eval("BtnTxt") %>' UseSubmitBehavior="false" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { LockJob("+Container.VisibleIndex+");}"   %>'>
                            </dxe:ASPxButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
