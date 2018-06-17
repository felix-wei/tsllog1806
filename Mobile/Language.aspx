<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Language.aspx.cs" Inherits="Mobile_Language" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">


        function CheckFile(s, e) {
            var fileType = (s.GetText().match(/^(.*)(\.)(.{1,8})$/)[3]).toLowerCase();
            //if ((ckb_Pdf.GetValue() && fileType == "pdf") || (ckb_OA.GetValue() && (fileType == "doc" || fileType == "docx" || fileType == "xls" || fileType == "xlsx" || fileType == "ppt" || fileType == "pptx")))
            if (fileType == "xls" || fileType == "xlsx")
                btn_upload.SetEnabled(true);
            else {
                btn_upload.SetEnabled(false);
                alert("This file extension isn't allowed,pls select Excel(.xls/.xlsx)");
            }
        }

    </script>

    <style type="text/css">
        body {
            min-width: 900px;
            background: #f8f8f8;
        }

        .main {
            width: 900px;
            position: relative;
            left: 50%;
            margin-left: -450px;
            background-color: white;
        }

        .head {
            font-size: 22px;
            font-weight: 500;
        }

        .content {
            width: 300px;
            position: relative;
            left: 50%;
            margin-left: -150px;
            min-height:30px;
        }
        .content1 {
            width: 300px;
            position: relative;
            left: 50%;
            margin-left: -150px;
        }

        .float_right {
            float: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <div class="head">
                <p>
                    <b>Rule:</b>
                    For upload, the code of the "code" column, must be exclusive of letter and number, and need to start with letter.
                </p>
            </div>
            <div class="content">
                <dxe:ASPxButton runat="server" ID="btn_upload" ClientInstanceName="btn_upload" Text="Upload" Width="120" ClientEnabled="false" OnClick="btn_upload_Click" CssClass="float_right"></dxe:ASPxButton>
                <dxuc:ASPxUploadControl ID="file_upload1" ClientInstanceName="file_upload1" runat="server" Width="150" ClientSideEvents-TextChanged="CheckFile" CssClass="float_right">
                </dxuc:ASPxUploadControl>
            </div>
            <div class="content">
            <dxe:ASPxButton runat="server" ID="btn_download_excel" Text="Download Excel" Width="120" OnClick="btn_download_excel_Click" CssClass="float_right"></dxe:ASPxButton>
            </div>
            <div class="content">
            <dxe:ASPxButton runat="server" ID="btn_download" Text="Download JS" Width="120" OnClick="btn_download_Click" CssClass="float_right"></dxe:ASPxButton>
            </div>
            <div class="content">
                <dxe:ASPxLabel ID="lb_txt" runat="server"></dxe:ASPxLabel>
            </div>

        </div>
    </form>
</body>
</html>
