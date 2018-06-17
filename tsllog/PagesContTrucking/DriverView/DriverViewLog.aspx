<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverViewLog.aspx.cs" Inherits="PagesContTrucking_DriverView_DriverViewLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none">
            <dxe:ASPxLabel ID="lb_tripId" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="ASPxLabel1" ClientInstanceName="date1" runat="server"></dxe:ASPxLabel>
            <dxe:ASPxLabel ID="ASPxLabel2" ClientInstanceName="date2" runat="server"></dxe:ASPxLabel>
        </div>
        <div>
            <table>
                <tr>
                    <td>JobNo:</td>
                    <td>
                        <dxe:ASPxLabel ID="lb_JobNo" runat="server"></dxe:ASPxLabel>
                    </td>
                    <td>&nbsp;ContNo:</td>
                    <td>
                        <dxe:ASPxLabel ID="lb_ContNo" runat="server"></dxe:ASPxLabel>
                    </td>
                    <td>&nbsp;Driver:</td>
                    <td>
                        <dxe:ASPxLabel ID="lb_Driver" ClientInstanceName="txt_Driver" runat="server"></dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" runat="server" Width="100" Text="Add" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e) {
        document.getElementById('div').style.display='block';
        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_GoBack" runat="server" Width="100" Text="Go Back" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){window.location='DriverView.aspx?driver='+txt_Driver.GetText()+ '&date1='+date1.GetText()+'&date2='+date2.GetText();}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <div id="div" style="display: none">
                <table style="width: 600px">
                    <tr>
                        <td>Status</td>
                        <td>
                            <dxe:ASPxComboBox ID="cmb_JobStatus" Width="100" runat="server">
                                <Items>
                                    <dxe:ListEditItem Value="U" Text="Use" />
                                    <dxe:ListEditItem Value="S" Text="Start" />
                                    <dxe:ListEditItem Value="D" Text="Doing" />
                                    <dxe:ListEditItem Value="W" Text="Waiting" />
                                    <dxe:ListEditItem Value="P" Text="Pending" />
                                    <dxe:ListEditItem Value="C" Text="Completed" />
                                    <dxe:ListEditItem Value="X" Text="Cancel" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                        <td>Date</td>
                        <td>
                            <dxe:ASPxDateEdit ID="date_Date" runat="server" Width="100" Value='<%# Bind("TptDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                        <td>Time</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_Time" Width="100" runat="server">
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Remark</td>
                        <td colspan="5">
                            <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="100%">
                            </dxe:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: right;"></td>
                        <td colspan="2">
                            <dxe:ASPxButton ID="ASPxButton21" Width="110" runat="server" Text="Save" OnClick="ASPxButton21_Click">
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton2" Width="110" runat="server" Text="Cancel" AutoPostBack="false" UseSubmitBehavior="false">
                                <ClientSideEvents Click="function(s,e) {
        document.getElementById('div').style.display='none';
        }" />
                            </dxe:ASPxButton>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <table style="border: 1px solid black; width: 600px">
                        <tr>
                            <td style="width: 60px">Status:</td>
                            <td><%# Eval("Status") %></td>
                            <td style="width: 60px">Date:</td>
                            <td><%# SafeValue.SafeDateStr(Eval("LogDate"))+" "+SafeValue.SafeString(Eval("LogTime")) %></td>
                        </tr>
                        <tr>
                            <td>Remark:</td>
                            <td colspan="3"><%# Eval("Remark") %></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
