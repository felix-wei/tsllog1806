<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectChgCode.aspx.cs" Inherits="PagesContTrucking_SelectPage_SelectChgCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopubRate(); }
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
                alert("Action Success!");
                parent.AfterPopubRate();
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
                    <dxe:ASPxTextBox ID="txt_Code" runat="server"></dxe:ASPxTextBox>
                </td>
                <td>Group By</td>
                 <td>
                     <dxe:ASPxComboBox ID="cbb_GroupBy" runat="server" Width="100">
                         <Items>
                             <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                             <dxe:ListEditItem Text="TRUCKING" Value="TRUCKING"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="WAREHOUSE" Value="WAREHOUSE"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="TRANSPORT" Value="TRANSPORT"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="CRANE" Value="CRANE"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="OTHER" Value="OTHER"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="FREIGHT" Value="FREIGHT"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="INCENTIVE" Value="INCENTIVE"></dxe:ListEditItem>
                             <dxe:ListEditItem Text="CLAIMS" Value="CLAIMS"></dxe:ListEditItem>
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

                    <dxe:ASPxButton ID="btn_CreateInv" Width="60" runat="server" Text="OK"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('OK',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" Width="100%" 
         KeyFieldName="SequenceId"  OnCustomDataCallback="grid_CustomDataCallback" OnPageIndexChanged="grid_PageIndexChanged">
            <SettingsPager Mode="ShowPager" PageSize="20">
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
                    Width="100">
                    <DataItemTemplate>
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="100"
                            ID="spin_Price" Height="21px" Value='<%# Bind("Price")%>' DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="ChgUnit" VisibleIndex="3" Width="60">
                    <DataItemTemplate>
                        <dxe:ASPxTextBox ID="txt_Unit" runat="server" Text='<%# Eval("ChgUnit") %>' Width="60px"></dxe:ASPxTextBox>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="2"
                    Width="80">
                    <DataItemTemplate>
                        <dxe:ASPxSpinEdit DisplayFormatString="0" runat="server" Width="80"
                            ID="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="0" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Gst Type" FieldName="GstTypeId" VisibleIndex="4" Width="60">
                    <DataItemTemplate>
                        <dxe:ASPxComboBox ID="cbb_GstTypeId" runat="server" Width="60" Value='<%# Bind("GstTypeId")%>'>
                            <Items>
                                <dxe:ListEditItem Text="S" Value="S"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Z" Value="Z"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="E" Value="E"></dxe:ListEditItem>
                            </Items>
                        </dxe:ASPxComboBox>
                    </DataItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Gst P" FieldName="GstP" VisibleIndex="5" Visible="false">
                    <PropertiesSpinEdit DisplayFormatString="g" >
                    <SpinButtons ShowIncrementButtons="false" />
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
