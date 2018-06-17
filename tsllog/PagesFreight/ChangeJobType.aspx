<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeJobType.aspx.cs" Inherits="PagesFreight_ChangeJobType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="cmb_Type" Enabled="false" ReadOnly="true" BackColor="Control" >
                            <Items>
                                <dxe:ListEditItem Text="Sea" Value="Sea" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>JobType</td>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="cmb_refType" >
                            <Items>
                                <dxe:ListEditItem Text="Import" Value="Import" />
                                <dxe:ListEditItem Text="Export" Value="Export" />
                                <dxe:ListEditItem Text="CrossTrade" Value="CrossTrade" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>RefNo</td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" ReadOnly="false" ClientInstanceName="txt_RefNo" runat="server" Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>New JobType</td>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="ASPxComboBox1" EnableIncrementalFiltering="True">
                            <Items>
                                <dxe:ListEditItem Text="FCL" Value="FCL" />
                                <dxe:ListEditItem Text="LCL" Value="LCL" />
                                <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_change" Width="100" runat="server" Text="ChangeJobType" OnClick="btn_change_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
