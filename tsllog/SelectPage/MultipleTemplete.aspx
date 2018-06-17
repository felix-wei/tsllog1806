<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MultipleTemplete.aspx.cs" Inherits="SelectPage_MultipleTemplete" %>

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
              if (e.keyCode == 27) { parent.ClosePopupCtr(); }
          }
          document.onkeydown = keydown;

          function SelectAll() {
              if (btnSelect.GetText() == "Select All")
                  btnSelect.SetText("UnSelect All");
              else
                  btnSelect.SetText("Select All");
              jQuery("input[id*='ack_IsPay']").each(function () {
                  this.click();
              });
          }
          function OnCallback(v) {
              if (v == "Success") {
                  parent.AfterPopubMultiInv2();
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
    <form id="form1" runat="server">
    <div>
           <table>
                <tr>
                     <td>
                          <dxe:ASPxButton ID="btnSelect" runat="server" Text="Invert Select" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                    </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="100%"
            KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="RefNo" FieldName="RefNo" Width="100" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Title" FieldName="Title" VisibleIndex="2" Width="200">
                    <DataItemTemplate>
                        <dxe:ASPxLabel runat="server" ID="lbl_Title" Text='<%# Eval("Title") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="CenterContent" FieldName="CenterContent" VisibleIndex="2" Width="300">
                    <DataItemTemplate>
                        <dxe:ASPxLabel runat="server" ID="lbl_CenterContent" Text='<%# Eval("CenterContent") %>'></dxe:ASPxLabel>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="3" Width="40">
                    <DataItemTemplate>
                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                        </dxe:ASPxCheckBox>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
