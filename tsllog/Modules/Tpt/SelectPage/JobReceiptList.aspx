<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobReceiptList.aspx.cs" Inherits="Modules_Tpt_SelectPage_JobReceiptList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function PopupJobHouse() {
            popubCtr.SetHeaderText('Stock List');
            popubCtr.SetContentUrl('/Modules/Tpt/SelectPage/StockListForJobReceipt.aspx?no=' + lbl_Id.GetText() + "&JobNo=" + lbl_JobNo.GetText());
            popubCtr.Show();
        }
        function AfterPopub() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_receipt.Refresh();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsReceipt" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobReceipt"
            KeyMember="Id" FilterExpression="1=0" />
        <div style="display: none">
            <dxe:ASPxLabel ID="lbl_Id" ClientInstanceName="lbl_Id" runat="server"></dxe:ASPxLabel>
             <dxe:ASPxLabel ID="lbl_JobNo" ClientInstanceName="lbl_JobNo" runat="server"></dxe:ASPxLabel>
        </div>
        <div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btn_cont_newTrip" runat="server" Text="New Receipt" CssClass="add_carpark" AutoPostBack="false" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'>
                            <ClientSideEvents Click="function(s,e){PopupJobHouse();}" />
                        </dxe:ASPxButton>
                    </td>
                    <td width="90%"></td>
                </tr>
            </table>

            <dxwgv:ASPxGridView ID="grid_receipt" ClientInstanceName="grid_receipt" runat="server" DataSourceID="dsReceipt" OnInit="grid_receipt_Init" OnInitNewRow="grid_receipt_InitNewRow"
                OnRowInserting="grid_receipt_RowInserting" OnRowUpdating="grid_receipt_RowUpdating" OnRowDeleting="grid_receipt_RowDeleting"
                OnBeforePerformDataSelect="grid_receipt_BeforePerformDataSelect"
                KeyFieldName="Id" Width="1300px" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Width="60px">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true"></DeleteButton>
                    </dx:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="BookingNo" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Hbl No" FieldName="HblNo" VisibleIndex="0" Width="260" SortIndex="1" SortOrder="Descending">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Marking" FieldName="Marking1" VisibleIndex="2" Width="180" Visible="true">
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Marking1" ClientInstanceName="memo_Marking1" Text='<%# Bind("Marking1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Marking2" VisibleIndex="2" Width="180" Visible="true">
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Marking2" ClientInstanceName="memo_Marking2" Text='<%# Bind("Marking2") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty" VisibleIndex="2">
                        <PropertiesSpinEdit SpinButtons-ShowLargeIncrementButtons="false" NumberType="Float" DecimalPlaces="3" Increment="0"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Weight" FieldName="Weight" VisibleIndex="2">
                        <PropertiesSpinEdit SpinButtons-ShowLargeIncrementButtons="false" NumberType="Float" DecimalPlaces="3" Increment="0"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Volume" FieldName="Volume" VisibleIndex="2">
                        <PropertiesSpinEdit SpinButtons-ShowLargeIncrementButtons="false" NumberType="Float" DecimalPlaces="3" Increment="0"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="SKU Qty" FieldName="PackQty" VisibleIndex="2">
                        <PropertiesSpinEdit SpinButtons-ShowLargeIncrementButtons="false" NumberType="Float" DecimalPlaces="3" Increment="0"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" VisibleIndex="3" Width="180">
                        <EditItemTemplate>
                            <dxe:ASPxMemo ID="memo_Remark1" ClientInstanceName="memo_Remark1" Text='<%# Bind("Remark1") %>' Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>

        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="900" EnableViewState="False">
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
