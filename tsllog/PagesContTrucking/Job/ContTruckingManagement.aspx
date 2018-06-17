<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContTruckingManagement.aspx.cs" Inherits="PagesContTrucking_Job_ContTruckingManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function goJob(jobno) {
            //window.location = "JobEdit.aspx?jobNo=" + jobno;
        }
        function PopupAssignDriver() {
            //alert(btn_DriverCode.GetText());
            popubCtrAD.SetContentUrl('AssignDriverEdit.aspx?DriverCode=' + btn_DriverCode.GetText());
            popubCtrAD.Show();
        }
        function AfterPopupAD() {
            popubCtrAD.Hide();
            popubCtrAD.SetContentUrl('about:blank');
            document.getElementById("txt_Driver_Search").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--<wilson:DataSource ID="dsTripList" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsUnAssignTrip" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsDriverTrip" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmJobDet2" KeyMember="Id" FilterExpression="1=0" />--%>

            <table style="width: 100%">
                <tr>
                    <td style="width: 49%; vertical-align: top">
                        <table>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <p style="font-size: 13px; font-weight: bold">Using Job</p>
                                </td>
                            </tr>
                            <tr>
                                <td>Job No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Using_JobNo" runat="server" Width="100">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Date From</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Using_From" runat="server" Width="100"></dxe:ASPxDateEdit>
                                </td>
                                <td>Date To</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Using_To" runat="server" Width="100"></dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Using_Search" runat="server" Text="Retrieve" OnClick="btn_Using_Search_Click"></dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dxwgv:ASPxGridView ID="grid_usingJob" ClientInstanceName="grid_usingJob" runat="server"
                            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="JobNo" Caption="Job No" Width="150" SortOrder="Descending">
                                    <DataItemTemplate>
                                        <a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Job Date">
                                    <DataItemTemplate>
                                        <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Eta">
                                    <DataItemTemplate>
                                        <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Etd">
                                    <DataItemTemplate>
                                        <%# SafeValue.SafeDateStr(Eval("EtdDate")) %>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Completion Rate">
                                    <DataItemTemplate>
                                        <div style='background-color: #f52626'>
                                            <div style='background-color: #44b272; width: <%# Eval("PercentAmt") %>'><%# Eval("PercentAmt2") %></div>
                                        </div>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td style="width: 2%"></td>
                    <td style="width: 49%; vertical-align: top">
                        <table>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <p style="font-size: 13px; font-weight: bold">Trip List</p>
                                </td>
                            </tr>
                            <tr>
                                <td>Trip Status</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Trip_StatusCode" runat="server" Width="100">
                                        <Items>
                                            <dxe:ListEditItem Value="Processing" Text="Processing" Selected="true" />
                                            <dxe:ListEditItem Value="Pending" Text="Pending" />
                                            <dxe:ListEditItem Value="Completed" Text="Completed" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Job No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_TripList_JobNo" runat="server" Width="100">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Container No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_TripList_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_TripList_Search" runat="server" Text="Retrieve" OnClick="btn_TripList_Search_Click"></dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dxwgv:ASPxGridView ID="grid_TripList" runat="server" Width="100%" KeyFieldName="Id" AutoGenerateColumns="false">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="JobNo" Caption="Job No" Width="150" SortOrder="Descending"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="DriverCode" Caption="Driver Code"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="6" Caption="From Date">
                                    <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("FromDate")) %></DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="7" Caption="To Date">
                                    <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("ToDate")) %></DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 49%; vertical-align: top">
                        <table>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <p style="font-size: 13px; font-weight: bold">UnAssign Trip</p>
                                </td>
                            </tr>
                            <tr>
                                <td>Job No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_UnAssign_JobNo" runat="server" Width="100">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Container No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_UnAssign_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_UnAssign_Search" runat="server" Text="Retrieve" OnClick="btn_UnAssign_Search_Click"></dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dxwgv:ASPxGridView ID="grid_UnAssign_Trip" runat="server" Width="100%" KeyFieldName="Id" AutoGenerateColumns="false">
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="JobNo" Caption="Job No" Width="150" SortOrder="Descending"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="DriverCode" Caption="Driver Code"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="6" Caption="From Date">
                                    <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("FromDate")) %></DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="7" Caption="To Date">
                                    <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("ToDate")) %></DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </td>
                    <td></td>
                    <td style="width: 49%; vertical-align: top">
                        <table>
                            <tr>
                                <td colspan="3" style="text-align: center">
                                    <p style="font-size: 13px; font-weight: bold">Driver Trip</p>
                                </td>
                            </tr>
                            <tr>
                                <td>DriverCode</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" AutoPostBack="False" Width="100">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_DriverCode,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>Container No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Driver_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="txt_Driver_Search" runat="server" Text="Retrieve" OnClick="txt_Driver_Search_Click"></dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="txt_Driver_Assign" runat="server" Text="Assign" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                                            PopupAssignDriver();
                                        }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dxwgv:ASPxGridView ID="grid_DriverTrip" runat="server" Width="100%" KeyFieldName="Id" AutoGenerateColumns="false">
                            <Settings VerticalScrollableHeight="300" VerticalScrollBarMode="Auto" />
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn FieldName="JobNo" Caption="Job No" Width="150" SortOrder="Descending"></dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="2" FieldName="ContainerNo" Caption="Container No"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="3" FieldName="DriverCode" Caption="Driver Code"></dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="6" Caption="From Date">
                                    <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("FromDate")) %></DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataColumn VisibleIndex="7" Caption="To Date">
                                    <DataItemTemplate><%# SafeValue.SafeDateStr(Eval("ToDate")) %></DataItemTemplate>
                                </dxwgv:GridViewDataColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>






        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
            Width="900" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtrAD" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrAD"
                HeaderText="Assign Driver" AllowDragging="True" EnableAnimation="False" Height="550"
                AllowResize="True" Width="800" EnableViewState="False">
            </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
