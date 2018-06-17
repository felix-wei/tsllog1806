<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Import_PSA_billing.aspx.cs" Inherits="PSA_Import_PSA_billing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        function CheckFile(s, e) {
            var fileType = (s.GetText().match(/^(.*)(\.)(.{1,8})$/)[3]).toLowerCase();
            //if ((ckb_Pdf.GetValue() && fileType == "pdf") || (ckb_OA.GetValue() && (fileType == "doc" || fileType == "docx" || fileType == "xls" || fileType == "xlsx" || fileType == "ppt" || fileType == "pptx")))
            if (fileType == "xls" || fileType == "xlsx" || fileType == "csv")
                btn_upload.SetEnabled(true);
            else {
                btn_upload.SetEnabled(false);
                alert("This file extension isn't allowed,pls select Excel(.xls/.xlsx)");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Date From</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_from" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Width="100"></dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="search_to" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Width="100"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td ></td>
                    <td colspan="3">
                        <dxuc:ASPxUploadControl ID="file_upload1" ClientInstanceName="file_upload1" runat="server" Width="224" ClientSideEvents-TextChanged="CheckFile">
                        </dxuc:ASPxUploadControl>
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btn_upload" ClientInstanceName="btn_upload" Text="Upload" Width="100" ClientEnabled="false" OnClick="btn_upload_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lb_txt" runat="server"></dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gv" runat="server" AutoGenerateColumns="true">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>

                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="F1" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
