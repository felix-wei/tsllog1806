<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookingOrder.aspx.cs" Inherits="Modules_Freight_SelectPages_BookingOrder" %>

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
              if (e.keyCode == 27) { parent.ClosePopupCtr(); }
          }
          document.onkeydown = keydown;

          function SelectAll() {
              if (btnSelect.GetText() == "Select All/全选")
                  btnSelect.SetText("UnSelect All/取消全选");
              else
                  btnSelect.SetText("Select All/全选");
              jQuery("input[id*='ack_IsPay']").each(function () {
                  this.click();
              });
          }
          function OnCallback(v) {
              if (v == "Success") {
                  parent.AfterPopubMultiple();
              }
              else if (v != null && v.length > 0)
                  alert(v)
          }
    </script>

    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <wilson:DataSource ID="dsJobCont" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.CtmJobDet1" KeyMember="Id"  />
    <form id="form1" runat="server">
        <div>
            <table style="width:1000px">
                <tr>
                    <td><dxe:ASPxLabel ID="lbl_booking" runat="server" Text="唛头号(Bkg RefNo)" Width="120px"></dxe:ASPxLabel> </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_No" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td><dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Shipper/承运人" Width="100px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve/查询"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All/全选" Width="160" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok/确定" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                      <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="ContNo/货柜号" Width="100px"></dxe:ASPxLabel>  
                    </td>
                    <td>
                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" DataSourceID="dsJobCont" Width="160" ID="cmb_ContNo"
                            runat="server" TextField="ContainerNo" ValueField="ContainerNo" DropDownStyle="DropDown" SelectedIndex="0" >
                            <ClientSideEvents  />
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                      <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="SealNo/封条号" Width="100px"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="lbl_SealNo" runat="server" ReadOnly="true" BackColor="Control" Width="100px"></dxe:ASPxTextBox>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" OnCustomDataCallback="grid_CustomDataCallback"
                Width="100%" KeyFieldName="Id" AutoGenerateColumns="False">
                <SettingsPager PageSize="50">
                </SettingsPager>
                <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
                <SettingsCustomizationWindow Enabled="True" />
                <Columns>
                     <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Oid" VisibleIndex="0" Width="40">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                            <div style="display: none">
                                <dxe:ASPxLabel ID="lbl_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="BookingNo" Caption="唛头号/Bkg RefNo" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ShipperInfo" Caption="承运人/SHIPPER" VisibleIndex="1">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientId" Caption="收货人/RECEIVER PARTY" VisibleIndex="1">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientEmail" Caption="联系电话/CONTACT NUMBER" VisibleIndex="1">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark1" Caption="送货地址/DELIVERY ADDRESS" VisibleIndex="1">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ConsigneeInfo" Caption="货主/CONSIGNEE" VisibleIndex="2">
                    </dxwgv:GridViewDataColumn>
                     <dxwgv:GridViewDataColumn FieldName="ConsigneeRemark" Caption="货主IC" VisibleIndex="4">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ConsigneeEmail" Caption="货主UEN" VisibleIndex="4">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Prepaid_Ind" Caption="中国付/Freight prepaid" VisibleIndex="4">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="货物状态" FieldName="CargoStatus" VisibleIndex="5">
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
