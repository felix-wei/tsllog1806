﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportOrder.aspx.cs" Inherits="Modules_Freight_Job_ImportOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">

        function CheckFile(s, e) {
            var fileType = (s.GetText().match(/^(.*)(\.)(.{1,8})$/)[3]).toLowerCase();
            //if ((ckb_Pdf.GetValue() && fileType == "pdf") || (ckb_OA.GetValue() && (fileType == "doc" || fileType == "docx" || fileType == "xls" || fileType == "xlsx" || fileType == "ppt" || fileType == "pptx")))
            if (fileType == "zip" || fileType == "csv" || fileType == "xlsx" || fileType == "xls")
                btn_upload.SetEnabled(true);
            else {
                btn_upload.SetEnabled(false);
                alert("This file extension isn't allowed,pls select zip file");
            }
        }
    </script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterUploadPhoto(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="3">
                        <dxuc:ASPxUploadControl ID="file_upload1" ClientInstanceName="file_upload1" runat="server" Width="224" ClientSideEvents-TextChanged="CheckFile">
                        </dxuc:ASPxUploadControl>
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btn_upload" ClientInstanceName="btn_upload" Text="Upload" Width="100" ClientEnabled="false" OnClick="btn_upload_Click" AutoPostBack="false"></dxe:ASPxButton>
                    </td>
                    <td width="110">
                        <dxe:ASPxButton ID="btn_download" Width="100" runat="server" Text="Download(下载模板)" OnClick="btn_download_Click"
                            AutoPostBack="false" UseSubmitBehavior="false">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lb_txt" runat="server"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lb_docno" runat="server"></dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
