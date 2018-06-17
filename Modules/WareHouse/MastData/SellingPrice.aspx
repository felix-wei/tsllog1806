<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SellingPrice.aspx.cs" Inherits="WareHouse_MastData_SellingPrice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Selling Price</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowHouse(masterId) {
            window.location = "SellingPriceEdit.aspx?no=" + masterId;
        }
    </script>

</head>
<body>
       <form id="form1" runat="server">
        <wilson:DataSource ID="dsPrice" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhTrans"
                KeyMember="Id"   FilterExpression="DoType='SQ'"/>
        <div>
            <table width="450">
                <tr>
                    <td>Customer
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_Supplier" ClientInstanceName="txt_Supplier" runat="server" Width="80" HorizontalAlign="Left" AutoPostBack="False">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                                               PopupParty(txt_Supplier,txt_SupplierName,null,null,null,null,null,null,'C');
                                                                    }" />
                            </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                         <dxe:ASPxTextBox ID="txt_SupplierName" ClientInstanceName="txt_SupplierName" ReadOnly="true" BackColor="Control" Width="280" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SellingPrice.aspx?name='+txt_SupplierName.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                       <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                         window.location='SellingPriceEdit.aspx?no=0';
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div>
              <dxwgv:ASPxGridView ID="grid_Price" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="false" DataSourceID="dsPrice" >
                  <SettingsPager Mode="ShowAllRecords">
                  </SettingsPager>
                  <SettingsEditing Mode="EditForm" />
                  <SettingsCustomizationWindow Enabled="True" />
                  <SettingsBehavior ConfirmDelete="True" />
                  <Columns>
                      <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="20">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("DoNo") %>');">Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                      <dxwgv:GridViewDataTextColumn Caption="No" FieldName="DoNo" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Company" FieldName="PartyName" VisibleIndex="2" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Remark" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="FromDate" FieldName="DoDate" VisibleIndex="9" Width="80">
                             <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataDateColumn Caption="ToDate" FieldName="ExpectedDate" VisibleIndex="9" Width="90">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CreateBy" FieldName="CreateBy" VisibleIndex="9" Width="80">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Create Date" ReadOnly="true" FieldName="CreateDateTime" VisibleIndex="9" Width="60">
                             <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataTextColumn Caption="UpdateBy" FieldName="UpdateBy" VisibleIndex="9" Width="60">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataDateColumn Caption="Update Date" ReadOnly="true"  FieldName="UpdateDateTime" VisibleIndex="9" Width="80">
                             <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                        </dxwgv:GridViewDataDateColumn>
                      <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DoStatus" VisibleIndex="11" Width="60">
                      </dxwgv:GridViewDataTextColumn>
                  </Columns>
            </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="800" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>
    </form>
</body>
</html>
