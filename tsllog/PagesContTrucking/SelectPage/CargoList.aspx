<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CargoList.aspx.cs" Inherits="PagesContTrucking_SelectPage_CargoList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopubDimension(); }
        }
        document.onkeydown = keydown;

        function PutAmt() {

            var qty = parseFloat(spin_Qty.GetText());
            var price = parseFloat(spin_Price.GetText());
            var gst = parseFloat(spin_det_GstP.GetText());
            var exRate = parseFloat(spin_det_ExRate.GetText());

            var amt = FormatNumber(qty * price, 2);
            var gstAmt = FormatNumber(amt * gst, 2);
            var docAmt = parseFloat(amt) + parseFloat(gstAmt);
            var locAmt = FormatNumber(docAmt * exRate, 2);

            spin_det_GstAmt.SetNumber(gstAmt);
            spin_det_DocAmt.SetNumber(docAmt);
            spin_det_LocAmt.SetNumber(locAmt);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none">
            <dxe:ASPxLabel runat="server" ID="lbl_Id"></dxe:ASPxLabel>
        </div>
        <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false">
            <ClientSideEvents Click="function(s,e){
                       grd_Stock.AddNewRow();
                    }" />
        </dxe:ASPxButton>
                     <wilson:DataSource ID="dsJobStock" runat="server" ObjectSpace="C2.Manager.ORManager"
           TypeName="C2.JobStock" KeyMember="Id" />
               <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
           KeyMember="Id" FilterExpression="CodeType='2'" />
        <dxwgv:ASPxGridView ID="grd_Stock" ClientInstanceName="grd_Stock" runat="server" DataSourceID="dsJobStock"
            KeyFieldName="Id" Width="980" OnBeforePerformDataSelect="grd_Stock_BeforePerformDataSelect" OnCustomDataCallback="grd_Stock_CustomDataCallback"
            OnInit="grd_Stock_Init" OnInitNewRow="grd_Stock_InitNewRow" OnRowInserting="grd_Stock_RowInserting" OnRowUpdating="grd_Stock_RowUpdating"
            OnRowDeleting="grd_Stock_RowDeleting">
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline" />
            <Settings />
            <Columns>
                <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="0">
                    <DataItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                        ClientSideEvents-Click='<%# "function(s) { grd_Stock.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton18" runat="server"
                                        Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Stock.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton19" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                        ClientSideEvents-Click='<%# "function(s) {grd_Stock.UpdateEdit("+Container.VisibleIndex+") }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton20" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                        ClientSideEvents-Click='<%# "function(s) { grd_Stock.CancelEdit() }"  %>'>
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="SortIndex" Caption="Index" VisibleIndex="1" Width="20"
                    SortIndex="0" SortOrder="Ascending">
                </dxwgv:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Description" FieldName="Marks1" VisibleIndex="1"  HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Uom1" Caption="Uom" VisibleIndex="1" Width="100">
                    <DataItemTemplate>
                        <%# Eval("Uom1") %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="txt_Uom1" ClientInstanceName="txt_Uom1" Width="100" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Uom1") %>' DropDownStyle="DropDown">
                            <Items>
                                <dxe:ListEditItem Text="CTN" Value="CTN" />
                                <dxe:ListEditItem Text="PKG" Value="PKG" />
                                <dxe:ListEditItem Text="BAG" Value="BAG" />
                                <dxe:ListEditItem Text="PAL" Value="PAL" />
                                <dxe:ListEditItem Text="TON" Value="TON" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Pipe No" FieldName="PipeNo" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Hint No" FieldName="HintNo" VisibleIndex="1" HeaderStyle-HorizontalAlign="Center">
                </dx:GridViewDataColumn>
                <dxwgv:GridViewDataSpinEditColumn FieldName="Qty1" Caption="PCS" VisibleIndex="1" Width="100">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="1" NumberType="Float"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>

                <dxwgv:GridViewDataSpinEditColumn FieldName="Price1" Caption="Price" VisibleIndex="2" Width="100">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="2" NumberType="Float"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>
            <Settings ShowFooter="True" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="Qty1" SummaryType="Sum" DisplayFormat="{0:#,##0}" />
                <dxwgv:ASPxSummaryItem FieldName="Price2" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
            </TotalSummary>
        </dxwgv:ASPxGridView>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="500" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
