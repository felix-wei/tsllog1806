<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckError.aspx.cs" Inherits="PagesAccount_CheckError" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checking Error</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Year
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_Year" runat="server">
                        <Items>
                            <dxe:ListEditItem Text="2013" Value="2013" />
                            <dxe:ListEditItem Text="2014" Value="2014" />
                            <dxe:ListEditItem Text="2015" Value="2015" />
                            <dxe:ListEditItem Text="2016" Value="2016" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    Period
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_Period" runat="server">
                        <Items>
                            <dxe:ListEditItem Text="1" Value="1" />
                            <dxe:ListEditItem Text="2" Value="2" />
                            <dxe:ListEditItem Text="3" Value="3" />
                            <dxe:ListEditItem Text="4" Value="4" />
                            <dxe:ListEditItem Text="5" Value="5" />
                            <dxe:ListEditItem Text="6" Value="6" />
                            <dxe:ListEditItem Text="7" Value="7" />
                            <dxe:ListEditItem Text="8" Value="8" />
                            <dxe:ListEditItem Text="9" Value="9" />
                            <dxe:ListEditItem Text="10" Value="10" />
                            <dxe:ListEditItem Text="11" Value="11" />
                            <dxe:ListEditItem Text="12" Value="12" />
                            <dxe:ListEditItem Text="13" Value="13" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
        </table>
        <dxe:ASPxButton ID="btn_ar" runat="server" Text="Check AR Error" Width="140" OnClick="btn_ar_click">
        </dxe:ASPxButton>
        <dxe:ASPxButton ID="btn_ap" runat="server" Text="Check AP Error" Width="140" OnClick="btn_ap_click">
        </dxe:ASPxButton>
        <dxe:ASPxButton ID="btn_gl" runat="server" Text="Check GL Error" Width="140" OnClick="btn_gl_click">
        </dxe:ASPxButton>
    </div>
    </form>
</body>
</html>
