<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessForCargo.aspx.cs" Inherits="PagesContTrucking_SelectPage_ProcessForCargo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function BatchAdd() {
            popubCtr.SetHeaderText('Batch Add');
            popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/Process_BatchAdd.aspx?no=' + lbl_Id.GetText());
            popubCtr.Show();
        }
        function AfterPopub() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid.Refresh();
        }
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
            <dxe:ASPxLabel runat="server" ID="lbl_Id"  ClientInstanceName="lbl_Id"></dxe:ASPxLabel>
        </div>
        <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false">
            <ClientSideEvents Click="function(s,e){
                       grid.AddNewRow();
                    }" />
        </dxe:ASPxButton>
         <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Batch Select" AutoPostBack="false">
            <ClientSideEvents Click="function(s,e){
                       BatchAdd();
                    }" />
        </dxe:ASPxButton>
                <wilson:DataSource ID="dsProcess" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobProcess" KeyMember="Id"  />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
            DataSourceID="dsProcess" KeyFieldName="Id" Width="100%" 
            OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnBeforePerformDataSelect="grid_BeforePerformDataSelect"
            OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting" OnRowDeleted="grid_RowDeleted" OnRowInserted="grid_RowInserted" OnRowUpdated="grid_RowUpdated">
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsEditing Mode="Inline"/>
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Settings  ShowFooter="true"/>
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60px">
                    <EditButton Visible="true"></EditButton>
                    <DeleteButton Visible="true"></DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="Entry Date" FieldName="DateEntry" VisibleIndex="1" Visible="false">
                    <DataItemTemplate>
                        <%# SafeValue.SafeDateStr(Eval("DateEntry")) %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_DateEntry" runat="server" Value='<%# Bind("DateEntry") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataTextColumn Caption="Plan Date" FieldName="DatePlan" VisibleIndex="1">
                    <DataItemTemplate>
                        <%# SafeValue.SafeDateStr(Eval("DatePlan")) %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_DatePlan" runat="server" Value='<%# Bind("DatePlan") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Inspect Date" FieldName="DateInspect" VisibleIndex="1">
                    <DataItemTemplate>
                        <%# SafeValue.SafeDateStr(Eval("DateInspect")) %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_DateInspect" runat="server" Value='<%# Bind("DateInspect") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Process Date" FieldName="DateProcess" VisibleIndex="1">
                    <DataItemTemplate>
                        <%# SafeValue.SafeDateStr(Eval("DateProcess")) %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit ID="date_DateProcess" runat="server" Value='<%# Bind("DateProcess") %>' Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="ProcessType" Caption="Process Type" Width="50" VisibleIndex="1">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Text="" Value="" />
                            <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                            <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                            <dxe:ListEditItem Text="Painting" Value="Painting" />
                            <dxe:ListEditItem Text="Washing" Value="Washing" />
                            <dxe:ListEditItem Text="Others" Value="Others" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="ProcessStatus" Caption="Process Status" Width="50" VisibleIndex="1">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Text="Pending" Value="Pending" />
                            <dxe:ListEditItem Text="Scheduled" Value="Scheduled" />
                            <dxe:ListEditItem Text="Started" Value="Started" />
                            <dxe:ListEditItem Text="Completed" Value="Completed" />
                            <dxe:ListEditItem Text="Cancelled" Value="Cancelled" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Plan Qty" FieldName="Qty" VisibleIndex="2">
                    <EditItemTemplate>
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty" ClientInstanceName="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" VisibleIndex="2" Width="120">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="LocationCode" FieldName="LocationCode" VisibleIndex="2" Width="80">
                </dxwgv:GridViewDataTextColumn>                 
                <dxwgv:GridViewDataTextColumn Caption="Process Qty" FieldName="ProcessQty1" VisibleIndex="2" Width="60">
                    <EditItemTemplate>
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_ProcessQty1" ClientInstanceName="spin_ProcessQty1" Height="21px" Value='<%# Bind("ProcessQty1")%>' DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Specs 1" FieldName="Specs1" Width="200px" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Specs 2" FieldName="Specs2" Width="200px" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Specs 3" FieldName="Specs3" Width="200px" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Specs 4" FieldName="Specs4" Width="200px" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark1" Width="200px" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark1" FieldName="Remark2" Width="200px" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <TotalSummary>
                <dxwgv:ASPxSummaryItem  FieldName="TotalVol" SummaryType="Sum" DisplayFormat="0.000"/>
                <dxwgv:ASPxSummaryItem  FieldName="TotalWt" SummaryType="Sum" DisplayFormat="0.000"/>
                <dxwgv:ASPxSummaryItem  FieldName="SkuQty" SummaryType="Sum" DisplayFormat="0.000"/>
            </TotalSummary>
        </dxwgv:ASPxGridView>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="600" EnableViewState="False">
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
