<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import_PSA_GIRO_history.aspx.cs" Inherits="PSA_Import_PSA_GIRO_history" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxDateEdit ID="search_from" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_to" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gv" runat="server" AutoGenerateColumns="false" Width="800">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dx:GridViewDataColumn FieldName="CreateDateTime" Caption="Date">
                        <DataItemTemplate>
                            <div style="white-space: nowrap;">
                                <%# SafeValue.SafeDate(Eval("CreateDateTime"),new DateTime(1900,1,1)).ToString("dd/MM/yyyy HH:mm") %>
                            </div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Controller" Caption="Controller"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Remark" Caption="Note"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Note1" Caption="Pay No"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Note2" Caption="Pay Date"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Value1" Caption="Total Amt"></dx:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem  FieldName="Value1" DisplayFormat="{0:#,##0.00}" SummaryType="Sum"/>
                </TotalSummary>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
