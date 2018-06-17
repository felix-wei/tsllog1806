<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="NewsEdit.aspx.cs" Inherits="PagesHr_Job_News" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>News</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/Pages.js"></script>
    
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
    <script type="text/javascript">
        function OnPostCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsNews" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPersonNews" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
            <div style="display:none">
                <dxe:ASPxTextBox runat="server" ID="txt_Id"></dxe:ASPxTextBox>
            </div>
            <table style="text-align:center">
                <tr>
                    <td colspan="10">
                        <dxe:ASPxTextBox runat="server" ID="txt_Title" HelpText="Title" HelpTextSettings-Position="Top" Width="550"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="10">
                        <dxe:ASPxMemo runat="server" ID="memo_Note" Width="550" Rows="5" Height="150" HelpText="Note" HelpTextSettings-Position="Top"></dxe:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td>ExpireDateTime</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_ExpireDateTime" Width="168" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm">
                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
