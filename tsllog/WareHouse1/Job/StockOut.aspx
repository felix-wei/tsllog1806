<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockOut.aspx.cs" Inherits="WareHouse_Job_StockOut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
         <script type="text/javascript">
             var isUpload = false;
    </script>
    <script type="text/javascript">
        function PopupUploadItemPhoto(type, item, itemName) {
            if (type == "IMG") {
                popubCtr.SetHeaderText('Upload Photo');
            } else {
                popubCtr.SetHeaderText('Upload Attachment');
            }
            popubCtr.SetContentUrl('../UploadItem.aspx?Type=JO&Sn=' + lbl_RItemId.GetText() + '&AttType=' + type + '&item=' + item + '&itemName=' + itemName);
            popubCtr.Show();
        }
        function AfterUploadItemPhoto() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
     </script>
</head>
<body>
<form id="form1" runat="server">
        <wilson:DataSource ID="dsInventory" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobInventory" KeyMember="Id" FilterExpression="DoType='WO'" />
                    <wilson:DataSource ID="dsItemRImg" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobAttachment"
                KeyMember="Id" FilterExpression="1=0" />
        <div>
            <table>
                <tr>
                      <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Date">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                                        <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                                   grid_Inventory.AddNewRow();
                                                                 }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton10" runat="server" Text="Refresh" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                                   grid_Inventory.Refresh();
                                                                 }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>

            <dxwgv:ASPxGridView ID="grid_Inventory" ClientInstanceName="grid_Inventory" runat="server"
                KeyFieldName="Id" Width="1400px" AutoGenerateColumns="False" DataSourceID="dsInventory"
                OnBeforePerformDataSelect="grid_Inventory_BeforePerformDataSelect" OnInit="grid_Inventory_Init" OnInitNewRow="grid_Inventory_InitNewRow"
                OnRowInserting="grid_Inventory_RowInserting" OnRowDeleting="grid_Inventory_RowDeleting" OnRowUpdating="grid_Inventory_RowUpdating">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsText Title="Cargo Received" />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { grid_Inventory.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Inventory.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_update" runat="server" Text="Update" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"")!="Closed" %>'
                                            ClientSideEvents-Click='<%# "function(s) { grid_Inventory.UpdateEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { grid_Inventory.CancelEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Item No" FieldName="ItemNo" VisibleIndex="1"
                        Width="120">
                        <DataItemTemplate>
                            <%# Eval("ItemNo") %>
                            <div style="display: none">
                                <dxe:ASPxLabel runat="server" ID="lbl_RItemId" ClientInstanceName="lbl_RItemId" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="1"
                        Width="200">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="QTY" FieldName="Qty" VisibleIndex="2" Width="60">
                        <PropertiesSpinEdit NumberType="Integer" SpinButtons-ShowIncrementButtons="false" Width="60">
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Balance Qty" FieldName="BalQty" VisibleIndex="2" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="2" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2" Width="60">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Weight" FieldName="Weight" VisibleIndex="2" Width="60">
                        <PropertiesSpinEdit NumberType="Float" Increment="2" SpinButtons-ShowIncrementButtons="false" Width="40">
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Volume" FieldName="Volume" VisibleIndex="2" Width="60">
                        <PropertiesSpinEdit NumberType="Float" Increment="2" SpinButtons-ShowIncrementButtons="false" Width="40">
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="DoType" VisibleIndex="2" ReadOnly="true"
                        Width="60" Visible="false">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Location" FieldName="Location" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="6"
                        Width="260">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pallet No" FieldName="PalletNo" VisibleIndex="6" Width="100"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Stock Date" FieldName="DoDate" VisibleIndex="10" Width="140" SortIndex="0" SortOrder="Descending">
                        <PropertiesDateEdit EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy HH:mm" EditFormatString="dd/MM/yyyy HH:mm" Width="140"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="11">
                        <DataItemTemplate>
                            <a onclick='javascript:PopupUploadItemPhoto("<%# Eval("JobNo") %>","<%# Eval("Id") %>","<%# Eval("ItemNo") %>");' href="#">Upload Photo</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <a onclick='javascript:PopupUploadItemPhoto("<%# Eval("JobNo") %>","<%# Eval("Id") %>","<%# Eval("ItemNo") %>");' href="#">Upload Photo</a>
                        </EditItemTemplate>

                    </dxwgv:GridViewDataTextColumn>
                    <%-- <dxwgv:GridViewDataColumn Caption="Photo" Width="100px" VisibleIndex="12">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# SafeValue.SafeString("/Photos/" +SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format(@"select FilePath from Job_Attachment where JobNo='{0}' and RefNo='{1}'", Eval("Id"), Eval("JobNo")))).Replace("\\", "/"))%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# GetImgUrl(SafeValue.SafeString(Eval("Id")),SafeValue.SafeString(Eval("JobNo"))) %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <div style="display: none">
                                                                    <dxe:ASPxLabel runat="server" ID="lbl_RItemId" OnInit="lbl_RItemId_Init" ClientInstanceName="lbl_RItemId" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                                                                </div>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>--%>
                </Columns>
                <SettingsDetail ShowDetailRow="true" ShowDetailButtons="true" />
                <Templates>
                    <DetailRow>
                        <dxwgv:ASPxGridView ID="grid_img_R" ClientInstanceName="grid_img_R" runat="server" DataSourceID="dsItemRImg"
                            KeyFieldName="Id" Width="900px" EnableRowsCache="False" OnBeforePerformDataSelect="grid_img_R_BeforePerformDataSelect"
                            AutoGenerateColumns="false" OnRowDeleting="grid_img_R_RowDeleting" OnInit="grid_img_R_Init" OnInitNewRow="grid_img_R_InitNewRow" OnRowUpdating="grid_img_R_RowUpdating">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Columns>
                                <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                    <DataItemTemplate>
                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_img_R.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                        </dxe:ASPxButton>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                    <DataItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <a href='<%# Eval("Path")%>' target="_blank">
                                                        <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                        </dxe:ASPxImage>
                                                    </a>
                                                </td>
                                            </tr>
                                        </table>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </DetailRow>
                </Templates>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0}" />
                    <dxwgv:ASPxSummaryItem FieldName="ItemNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
                        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="970" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grid_img_R.Refresh();

                }" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
