<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContainerInquery.aspx.cs" Inherits="PagesContTrucking_Job_ContainerInquery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style>
        .mainDiv {
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="mainDiv">
            <table>
                <tr>
                    <td>From Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_SearchFromDate" runat="server" Width="120" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_SearchToDate" runat="server" Width="120" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxe:ASPxMemo ID="txt_containers" runat="server" Rows="5" Width="500"></dxe:ASPxMemo>
            <br />
            <%--<dxe:ASPxListBox ID="list_context" runat="server" Width="600" >
                <Columns>
                    <dxe:ListBoxColumn FieldName="JobNo" Caption="JobNo" />
                    <dxe:ListBoxColumn FieldName="ContainerNo" Caption="ContainerNo" />
                    <dxe:ListBoxColumn FieldName="TripCode" Caption="TripCode" />
                    <dxe:ListBoxColumn FieldName="DriverCode" Caption="Driver" />
                    <dxe:ListBoxColumn FieldName="TowheadCode" Caption="Towhead" />
                    <dxe:ListBoxColumn FieldName="ChessisCode" Caption="Chessis" />
                    <dxe:ListBoxColumn FieldName="FromDate" Caption="FromDate" />
                    <dxe:ListBoxColumn FieldName="FromTime" Caption="FromTime" />
                    <dxe:ListBoxColumn FieldName="ToDate" Caption="ToDate" />
                    <dxe:ListBoxColumn FieldName="ToTime" Caption="ToTime" />
                </Columns>
            </dxe:ASPxListBox>--%>
            <dxwgv:ASPxGridView ID="gv_context" runat="server" Width="100%"
                KeyFieldName="Id"
                AutoGenerateColumns="False">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="JobNo">
                        <DataItemTemplate>
                            <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?jobNo=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerNo" FieldName="ContainerNo">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Trip Type" FieldName="TripCode">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Driver" FieldName="DriverCode">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PrimeMover" FieldName="TowheadCode">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Trailer" FieldName="ChessisCode">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Destination" FieldName="ToCode">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ParkingLot" FieldName="ToParkingLot">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="StartDate" FieldName="FromDate">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate(Eval("FromDate"),new DateTime(1900,1,1)).ToString("dd/MM/yyyy")+' '+Eval("FromTime") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="FromTime" FieldName="FromTime">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="EndDate" FieldName="ToDate">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDate(Eval("ToDate"),new DateTime(1900,1,1)).ToString("dd/MM/yyyy")+' '+Eval("ToTime") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="ToTime" FieldName="ToTime">
                    </dxwgv:GridViewDataTextColumn>--%>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
