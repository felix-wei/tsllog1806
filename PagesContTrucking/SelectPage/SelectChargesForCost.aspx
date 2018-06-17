<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="SelectChargesForCost.aspx.cs" Inherits="PagesContTrucking_SelectPage_SelectChargesForCost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            function $(s) {
                return document.getElementById(s) ? document.getElementById(s) : s;
            }
            function keydown(e) {
                if (e.keyCode == 27) { parent.AfterPopub(); }
            }
            document.onkeydown = keydown;
            function SelectAll() {
                if (btnSelect.GetText() == "Select All")
                    btnSelect.SetText("UnSelect All");
                else
                    btnSelect.SetText("Select All");
                jQuery("input[id*='ack_IsOk']").each(function () {
                    this.click();
                });
            }
            function OnCallback(v) {
                if (v == "Success") {
                    parent.AfterPopub();
                }
                else if (v != null && v.length > 0) {
                    alert(v);
                }
            }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Code
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_Code" runat="server" Width="80"></dxe:ASPxTextBox>
                </td>
                <td>Group By</td>
                 <td>
                     <dxe:ASPxComboBox ID="cbb_GroupBy" runat="server" Width="100">
                         <Items>
                             <dxe:ListEditItem Text="TRUCKING" Value="TRUCKING"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="WAREHOUSE" Value="WAREHOUSE"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="TRANSPORT" Value="TRANSPORT"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="DOCUMENTS" Value="DOCUMENTS"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="CRANE" Value="CRANE"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="OTHER" Value="OTHER"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="FREIGHT" Value="FREIGHT"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="INCENTIVE" Value="INCENTIVE"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="CLAIMS" Value="CLAIMS"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="PSA" Value="PSA"></dxe:ListEditItem>
                         </Items>
                     </dxe:ASPxComboBox>
                </td>
                <td>
                     <dxe:ASPxButton ID="btn_Retrieve" runat="server" Text="Retrieve" AutoPostBack="false" OnClick="btn_Retrieve_Click">
                              
                            </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="btn_CreateInv" Width="60" runat="server" Text="Keyin for All Container"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('Cont',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton1" Width="60" runat="server" Text="Keyin for Job"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('Job',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                                 <td style="display:none">

                    <dxe:ASPxButton ID="ASPxButton2" Width="60" runat="server" Text="Keyin for All Trip"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('Trip',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <div style="overflow-y:auto;height:450px">
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" Width="100%"
         KeyFieldName="SequenceId"  OnCustomDataCallback="grid_CustomDataCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"  />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="1"
                    Width="40">
                    <DataItemTemplate>
                        <dxe:ASPxCheckBox ID="ack_IsOk" runat="server" Width="10">
                        </dxe:ASPxCheckBox>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("SequenceId") %>' Width="150">
                            </dxe:ASPxTextBox>
                        </div>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgcodeId" VisibleIndex="1" >
                    <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_ChgcodeId" runat="server" Text='<%# Eval("ChgcodeId") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgcodeDes" VisibleIndex="2">
                    <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_ChgcodeDes" runat="server" Text='<%# Eval("ChgcodeDes") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="2"
                    Width="50">
                    <DataItemTemplate>
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="50"
                            ID="spin_Price" Height="21px" Value='<%# Bind("Price")%>' DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="ChgUnit" VisibleIndex="3">
                    <DataItemTemplate>
                        <dxe:ASPxTextBox Width="80px" ID="txt_Unit" ClientInstanceName="txt_Unit"
                            runat="server" Text='<%# Bind("ChgUnit") %>'>
                        </dxe:ASPxTextBox>
                        </td>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Gst Type" FieldName="GstTypeId" VisibleIndex="4">
                    <DataItemTemplate>
                        <dxe:ASPxLabel ID="lbl_GstTypeId" runat="server" Text='<%# Eval("GstTypeId") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Gst P" FieldName="GstP" VisibleIndex="5">
                     <DataItemTemplate>
                         <dxe:ASPxLabel ID="lbl_GstP" runat="server" Text='<%# Eval("GstP") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        </div>
        
    </div>
    </form>
</body>
</html>
