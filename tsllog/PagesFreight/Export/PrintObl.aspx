<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintObl.aspx.cs" Inherits="PagesFreight_Export_PrintObl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                <table>
                <tr>
                    <td>RefNo
                    </td>
                    <td>
                     <dxe:ASPxMemo ID="txtRefNo" ClientInstanceName="txtRefNo" Rows="3" runat="server" Width="300" >
                     </dxe:ASPxMemo>
                    </td>
                    <td>
                                    <dxe:ASPxButton ID="btn_printObl" runat="server" Text="Print Ocean BL(Carrier)" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            if(txtRefNo.GetText().length>0)
    window.open('/ReportFreightSea/printview.aspx?document=OceanBl_multiple&master=' + txtRefNo.GetText() + '&house=0');
                        }" />
                                    </dxe:ASPxButton>
                    </td>
                    <td>
                                    <dxe:ASPxButton ID="btn_printObl1" runat="server" Text="Print Ocean BL(Nvocc)" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                            if(txtRefNo.GetText().length>0)
    window.open('/ReportFreightSea/printview.aspx?document=OceanBlNvocc_multiple&master=' + txtRefNo.GetText() + '&house=0');
                        }" />
                                    </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
