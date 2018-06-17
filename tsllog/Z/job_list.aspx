<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="job_list.aspx.cs" Inherits="Z_job_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function showJobOrder(id, jobNo) {
            parent.navTab.openTab(jobNo, "/Z/job_edit.aspx?id=" + id, { title: jobNo, fresh: false, external: true });
            //window.location = 'JobOrderEdit.aspx?id=' + id;
        }
        function SetPCVisible(doShow) {
            if (doShow) {
                ASPxPopupClientControl.Show();
            }
            else {
                ASPxPopupClientControl.Hide();
            }
        }
        //Save job
        function OnSaveCallBack(v) {
            if (v != null && v.length > 0) {
                var sequenceId=v.substring(0,v.indexOf('|'));
                var jobNo = v.substring(v.indexOf('|') + 1, v.length);
                txt_Remark.SetText();
                cmb_PackType.SetText();
                spin_Pkgs.SetText();
                spin_Volume.SetText();
                spin_Weight.SetText();
                cmb_Carrier.SetText();
                cmb_Customer.SetText();
                cbx_jobCate.SetText();
                ASPxPopupClientControl.Hide();
                parent.navTab.openTab(jobNo, '/Z/job_edit.aspx?id=' + sequenceId, { title: jobNo, fresh: false, external: true });
            }
        }

    </script>


</head>
<body>
    <form id="form1" runat="server">

        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobOrder" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
        <wilson:DataSource ID="dsVendorMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsVendor='true'" />
        <wilson:DataSource ID="dsJobCate" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXMastData" KeyMember="Id" FilterExpression="CodeType='JobCate'" />
        <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="SequenceId" FilterExpression="CodeType='2'" />

        <div>
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="ASPxPopupClientControl" SkinID="None" Width="240px"
                ShowOnPageLoad="false" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                AllowDragging="True" CloseAction="None" PopupElementID="popupArea"
                EnableViewState="False" runat="server" PopupHorizontalOffset="0"
                PopupVerticalOffset="0" EnableHierarchyRecreation="True">
                <HeaderTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">New Order
                            </td>
                            <td>
                                <a id="a_X" onclick="SetPCVisible(false)" onmousedown="event.cancelBubble = true;" style="width: 15px; height: 14px; cursor: pointer;">X</a>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table align="center">
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="JobType" />
                                </td>
                                <td style="width: 185px">
                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="180" ID="txt_JobType">
                                        <Items>
                                            <dxe:ListEditItem Text="DIRECT" Value="DIRECT" Selected="true" />
                                            <dxe:ListEditItem Text="FCL" Value="FCL" />
                                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                                            <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td style="width: 65px">Category</td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="cbx_jobCate" ClientInstanceName="cbx_jobCate" DataSourceID="dsJobCate" TextField="Code" ValueField="Code"
                                        Width="148" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Customer
                                </td>
                                <td colspan="3">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="cmb_Customer" ClientInstanceName="cmb_Customer" DataSourceID="dsCustomerMast" ValueField="PartyId" TextField="Name"
                                                    Width="405" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Carrier
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox runat="server" ID="cmb_Carrier" ClientInstanceName="cmb_Carrier" DataSourceID="dsVendorMast" ValueField="PartyId" TextField="Name"
                                        Width="405" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px">Cargo</td>
                                <td colspan="3">
                                    <fieldset style="padding: 0px; width: 400px; border-spacing: 0px">
                                        <table>
                                            <tr>
                                                <td>Weight
                                                </td>
                                                <td>
                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" ClientInstanceName="spin_Weight" SpinButtons-ShowIncrementButtons="false"
                                                        runat="server" Width="120" ID="spin_Weight" Increment="0" DecimalPlaces="3">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>Volume
                                                </td>
                                                <td>
                                                    <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                        runat="server" Width="120" ID="spin_Volume" ClientInstanceName="spin_Volume" Increment="0" DecimalPlaces="3">
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Qty</td>
                                                <td>
                                                    <dxe:ASPxSpinEdit runat="server" Width="120" ID="spin_Pkgs" ClientInstanceName="spin_Pkgs" Height="21px">
                                                        <SpinButtons ShowIncrementButtons="false" />
                                                    </dxe:ASPxSpinEdit>
                                                </td>
                                                <td>Pack Type
                                                </td>
                                                <td>
                                                    <dxe:ASPxComboBox runat="server" ID="cmb_PackType" ClientInstanceName="cmb_PackType" DataSourceID="dsUom" TextField="Description" ValueField="Code"
                                                        Width="120" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                                    </dxe:ASPxComboBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Remark" />
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="txt_Remark" ClientInstanceName="txt_Remark" Rows="3" runat="server" Width="405">
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="4">
                                    <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="100" UseSubmitBehavior="false" Text="Save" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s,e) {
                                               grid1.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>Job No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_SchJobNo" runat="server" Width="145"></dxe:ASPxTextBox>
                                </td>
                                <td>Job Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_SchJobDate" Width="120" runat="server"
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>Category
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="cbx_SchjobCate" DataSourceID="dsJobCate" TextField="Code" ValueField="Code"
                                        Width="120" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Customer
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox runat="server" ID="cmb_SchCustomer" DataSourceID="dsCustomerMast" ValueField="PartyId" TextField="Name"
                                        Width="323" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Status
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="cmb_SchStatus" Width="120">
                                        <Items>
                                            <dxe:ListEditItem Text="USE" Value="USE" />
                                            <dxe:ListEditItem Text="CLOSED" Value="CLOSED" />
                                            <dxe:ListEditItem Text="CANCEL" Value="CANCEL" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <table style="width: 100px; height: 20px;" id="popupArea">
                                        <tr>
                                            <td id="addnew" style="text-align: center; vertical-align: middle;">
                                                <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td colspan="6">
                        <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
                            DataSourceID="ds1" KeyFieldName="SequenceId" Width="100%"
                            OnInit="grid1_Init" OnCustomDataCallback="grid1_CustomDataCallback"
                            AutoGenerateColumns="False">
                            <SettingsEditing Mode="EditForm" NewItemRowPosition="Top" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <Columns>
                                <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0" Visible="false">
                                    <DataItemTemplate>
                                        <table>
                                            <tr>
                                                <td><a onclick="showJobOrder('<%# Eval("SequenceId") %>','<%# Eval("JobNo") %>')">Edit</a>
                                                </td>
                                                <td style="display: none">
                                                    <a onclick='if(confirm("Confirm Delete")) {<%# "grid1.DeleteRow("+Container.VisibleIndex+");" %>}'>Delete</a></td>
                                                <td style="display: none">
                                                    <dxe:ASPxButton ID="btn_cont_del" runat="server"
                                                        Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid1.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <a href="#" onclick='<%# "grid1.UpdateEditRow("+Container.VisibleIndex+"); " %>'>Update</a></td>
                                                <td style="display: none">
                                                    <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                        ClientSideEvents-Click='<%# "function(s) { grid1.UpdateEdit() }"  %>'>
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                        ClientSideEvents-Click='<%# "function(s) { grid1.CancelEdit() }"  %>'>
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataTextColumn Caption="JobNo" ReadOnly="true" FieldName="JobNo" VisibleIndex="2" Width="60">
                                    <DataItemTemplate>
                                        <a onclick="showJobOrder('<%# Eval("SequenceId") %>','<%# Eval("JobNo") %>')"><%#Eval("JobNo") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Hbl No" ReadOnly="true" FieldName="HblNo" VisibleIndex="3" Width="60" />
                                <dxwgv:GridViewDataTextColumn Caption="Client" ReadOnly="true" FieldName="CustomerId" VisibleIndex="4">
                                    <DataItemTemplate>
                                        <dxe:ASPxComboBox ID="cmb_Client" ClientInstanceName="cmb_PartyTo" runat="server" DropDownButton-Visible="false" Border-BorderWidth="0"
                                            Value='<%# Eval("CustomerId") %>' DropDownStyle="DropDown" ReadOnly="true" Width="100%"
                                            DataSourceID="dsCustomerMast" ValueField="PartyId" ValueType="System.String" TextFormatString="{1}"
                                            EnableCallbackMode="true"
                                            CallbackPageSize="100">
                                            <Columns>
                                                <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                                                <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                            </Columns>
                                        </dxe:ASPxComboBox>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Pol" ReadOnly="true" FieldName="Pol" VisibleIndex="5" Width="60" />
                                <dxwgv:GridViewDataTextColumn Caption="Pod" ReadOnly="true" FieldName="Pod" VisibleIndex="6" Width="60" />
                                <dxwgv:GridViewDataTextColumn Caption="Service Type" ReadOnly="true" FieldName="ServiceType" VisibleIndex="7" Width="60" />
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
