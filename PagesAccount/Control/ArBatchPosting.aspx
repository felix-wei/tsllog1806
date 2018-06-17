<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArBatchPosting.aspx.cs"
    Inherits="PagesAccount_ArBatchPosting" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>AR Batch Posting</title>

    <script type="text/javascript">
        function ShowTab(tabInd) {
            var tabDate = document.getElementById("tab_Date");
            var tabNo = document.getElementById("tab_No");
            var tabPeriod = document.getElementById("tab_Period");
            if (tabInd == "0") {
                tabDate.style.display = "block";
                tabNo.style.display = "none";
                tabPeriod.style.display = "none";
            } else if (tabInd == "1") {
                tabDate.style.display = "none";
                tabNo.style.display = "block";
                tabPeriod.style.display = "none";
            } else if (tabInd == "2") {
                tabDate.style.display = "none";
                tabNo.style.display = "none";
                tabPeriod.style.display = "block";
            }
        }
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table><tr><td>
        <dxe:ASPxRadioButtonList ID="rbt" ClientInstanceName="rbt" runat="server">
            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ShowTab(s.GetValue());
}" />
            <Items>
                <dxe:ListEditItem Text="Post By Date" Value="0" />
                <dxe:ListEditItem Text="Post By No" Value="1" />
                <dxe:ListEditItem Text="Post By Period" Value="2" />
            </Items>
        </dxe:ASPxRadioButtonList>
        </td><td>
        <div id="tab_Date">
            <table>
                <tr>
                    <td>
                        From Date
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_From" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                        End Date
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_End" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" runat="server">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tab_No" style="display: none">
            <table>
                <tr>
                    <td>
                        From No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="no_From" runat="server" Width="170px">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        To No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="no_End" runat="server" Width="170px">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div id="tab_Period" style="display: none">
            <table>
                <tr>
                    <td>
                        Year
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_Year" runat="server">
                            <Items>
                                <dxe:ListEditItem Text="2008" Value="2008" />
                                <dxe:ListEditItem Text="2009" Value="2009" />
                                <dxe:ListEditItem Text="2010" Value="2010" />
                                <dxe:ListEditItem Text="2011" Value="2011" />
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
        </div>
        </td></tr></table>
        <table>
            <tr>
                <td valign="top">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxCheckBox ID="cb_ArInvoice" runat="server" Text="Invoice & Debit Note">
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="cb_ArCashInvoice" runat="server" Text="Cash Invoie">
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="cb_ArCn" runat="server" Text="Credit Note">
                                </dxe:ASPxCheckBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dxe:ASPxCheckBox ID="cb_ArReceipt" runat="server" Text="Receipt">
                                </dxe:ASPxCheckBox>
                            </td>
                            <td>
                                <dxe:ASPxCheckBox ID="cb_ArPc" runat="server" Text="RE Refund">
                                </dxe:ASPxCheckBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2" align="center">
        <dxe:ASPxButton ID="btn_Post" runat="server" Text="Step Post" Width="100" OnClick="btn_Post_click">
        </dxe:ASPxButton>
        </td></tr>
        </table>
        
    </div>
    </form>
</body>
</html>
