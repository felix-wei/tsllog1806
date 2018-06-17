<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverView_mp_Data.aspx.cs" Inherits="PagesTpt_Local_Viewer_DriverView_mp_Data" %>


    <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <table style="border: 1px solid black; width: 600px">
                        <tr>
                            <td>JobNo:</td>
                            <td><%# Eval("JobNo") %></td>
                            <td>Status:</td>
                            <td><%# Eval("JobProgress") %></td>
                            <td>Date:</td>
                            <td><%# SafeValue.SafeDateStr(Eval("TptDate"))+" "+SafeValue.SafeString(Eval("TptTime")) %></td>
                        </tr>
                        <tr>
                            <td>Client:</td>
                            <td><%# Eval("Cust") %></td>
                            <td>PIC:</td>
                            <td><%# Eval("CustPic") %></td>
                        </tr>
                        <tr>
                            <td>Qty:</td>
                            <td><%# Eval("BkgQty") %>&nbsp;<%# Eval("BkgPkgType") %></td>
                            <td>Weight:</td>
                            <td><%# Eval("BkgWt") %></td>
                            <td>Volume:</td>
                            <td><%# Eval("BkgM3") %></td>
                        </tr>
                        <tr>
                            <td>From:</td>
                            <td colspan="5"><%# Eval("PickFrm1") %></td>
                        </tr>
                        <tr>
                            <td>To:</td>
                            <td colspan="5"><%# Eval("DeliveryTo1") %></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                            <td><a href="#" onclick='AddTrip("<%# Eval("JobNo") %>","<%# Eval("Driver") %>");'>Trip Log</a></td>
                            <td>
                                <a href='#' onclick='PopupPhotoView("<%# Eval("JobNo") %>");'>Attachments</a></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
