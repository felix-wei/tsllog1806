<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="WareHouse_MastData_Product" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Product</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" >
     function ShowHouse(masterId) {
         window.location = "ProductEdit.aspx?no=" + masterId;
     }
     function DeleteRow(id) {
         if (confirm("Confirm Delete?"))
         {
             detailGrid.GetValuesOnCustomCallback('Delete', OnSaveCallBack);
         }
     }
     function OnSaveCallBack(v) {
          if (v == "Success") {
             detailGrid.Refresh();
         }
     }
    </script>
</head>
<body>

    <wilson:DataSource ID="dsProduct" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProduct" KeyMember="Id" />
    <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProductClass" KeyMember="Id" />
    <wilson:DataSource ID="dsWhMastData" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhMastData" KeyMember="Id" FilterExpression="Type='Attribute'" />
        <wilson:DataSource ID="dsPhoto" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhAttachment"
        KeyMember="Id" FilterExpression="1=0" />
    <form id="form1" runat="server">
        <div>
            <table width="450">
                <tr>
                    <td>Name
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="120" runat="server" ClientInstanceName="txt_Name">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
  
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                ShowHouse('0');	
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="detailGrid" DataSourceID="dsProduct"
                    KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback"
                    Width="100%" OnInit="grid_Init" OnRowDeleting="grid_RowDeleting">
                    <SettingsPager Mode="ShowPager" PageSize="20">
                    </SettingsPager>
                    <SettingsEditing Mode="EditForm" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior ConfirmDelete="True" />
                    <Columns>
<%--                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="80">
                            <DataItemTemplate>
                                <a onclick="ShowHouse('<%# Eval("Code") %>');"  class="sku">Edit</a>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>--%>
                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                            <DataItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                ClientSideEvents-Click='<%# "function(s) { window.location =\"ProductEdit.aspx?no=" + Eval("Code")+"\" }"  %>'>
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){detailGrid.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="1" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" Width="100">
                            <DataItemTemplate>
                                <dxe:ASPxLabel ID="lbl_Code" ClientInstanceName="lbl_Code" runat="server" Text='<%# Eval("Code") %>'></dxe:ASPxLabel>
                            </DataItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Pack" FieldName="Att1" VisibleIndex="4" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="5" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att1" FieldName="Att4" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att2" FieldName="Att5" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att3" FieldName="Att6" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att4" FieldName="Att7" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att5" FieldName="Att8" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att6" FieldName="Att9" VisibleIndex="6" Width="50">
                        </dxwgv:GridViewDataTextColumn>
<%--                        <dxwgv:GridViewDataTextColumn Caption="Att7" FieldName="Att9" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att8" FieldName="Att10" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att9" FieldName="Att11" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Att10" FieldName="Att12" VisibleIndex="6" Width="100">
                        </dxwgv:GridViewDataTextColumn>--%>
                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="StatusCode" VisibleIndex="6" Width="50" Visible="false">
                        </dxwgv:GridViewDataTextColumn>

                    </Columns>
                    
                </dxwgv:ASPxGridView>
                 <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grd_Photo.Refresh();
                }" />
            </dxpc:ASPxPopupControl>
            </div>
        </div>

    </form>
</body>
</html>
