<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddLocation.aspx.cs" Inherits="Modules_WareHouse_MastData_AddLocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsRefWarehouse" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefWarehouse" KeyMember="Id" />
        <div>
            <table width="100%">
                <tr>
                    <td>WareHouse</td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_WareHouse" ClientInstanceName="cmb_WareHouse" runat="server"
                            Width="100px" DropDownWidth="100" DropDownStyle="DropDownList" DataSourceID="dsRefWarehouse"
                            ValueField="Code" ValueType="System.String" TextFormatString="{0}" EnableCallbackMode="true"
                            EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <Columns>
                                <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="40px" />
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>

                    </td>
                    <td width="90%"></td>
                </tr>
            </table>
            <table width="50%" id="main">
                <tbody>
                    <tr>
                        <td>Rack
                        </td>
                        <td>
                            <input id="Rack" runat="server"  style="width:50px"/>
                        </td>
                        <td>Column
                        </td>
                        <td>
                            <input type="number" min="1" id="Column" runat="server" style="width:50px"/>
                        </td>
                        <td>Level
                        </td>
                        <td>
                            <input type="number" min="1" id="Level" runat="server" style="width:50px"/>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="btnAdd" runat="server" Width="100" Text="Add" OnClick="btnAdd_Click"></dxe:ASPxButton>
                        </td>
                        <td width="90%"></td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <dxe:ASPxLabel ID="lab" runat="server" Font-Bold="true" Font-Size="Small" ForeColor="Red">
                            </dxe:ASPxLabel>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
