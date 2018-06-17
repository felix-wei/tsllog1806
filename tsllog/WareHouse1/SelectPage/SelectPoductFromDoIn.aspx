<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectPoductFromDoIn.aspx.cs" Inherits="WareHouse_SelectPage_SelectPoductFromDoIn" %>

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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="SKU">
                        </dxe:ASPxLabel>
                        
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text=" Lot No">
                        </dxe:ASPxLabel>
                       
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                   
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Do Date">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        WareHouse
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                            Width="80px" DropDownWidth="200" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                            ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Sch" runat="server" Text="Retrieve"
                            OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" AutoGenerateColumns="False" Width="100%">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="0" Width="5%">
                         <DataItemTemplate>
                            <a onclick='parent.PutSku(new Array("<%# Eval("Code") %>","","<%# Eval("Description") %>","<%# Eval("AvaibleQty") %>","<%# Eval("AvaibleQty") %>","<%# Eval("UomPacking") %>","<%# Eval("UomWhole") %>","<%# Eval("UomLoose") %>","<%# Eval("Att4")%>","<%# Eval("Att5") %>","<%# Eval("Att6") %>","<%# Eval("Att7") %>","<%# Eval("Att8") %>","<%# Eval("Att9") %>"));'>Select</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Product" FieldName="Code" Width="60" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="LotNo" FieldName="LotNo" Width="60" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" Width="60" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
<%--                    <dxwgv:GridViewDataTextColumn Caption="Lot No" FieldName="LotNo" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Packing" FieldName="Packing" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="On Hand" FieldName="HandQty" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Avaible Qty" FieldName="AvaibleQty" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Uom Pack" FieldName="UomPacking" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Uom Whole" FieldName="UomWhole" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Uom Loose" FieldName="UomLoose" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
