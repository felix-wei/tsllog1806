<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Page_Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
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
        function OnCallback() {
            parent.AfterUploadPhoto();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
                    <wilson:DataSource ID="dsCategory" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='Category'" />
        <div>
            <table>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Sn" runat="server" BackColor="Control" ReadOnly="true" Text="">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div style="display: none">
                            Do No
                    <dxe:ASPxComboBox ID="cmb_DoNo" runat="server">
                    </dxe:ASPxComboBox>
                            Cont No
                    <dxe:ASPxComboBox ID="cmb_ContNo" runat="server">
                    </dxe:ASPxComboBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>Remark
                    </td>
                    <td>Category</td>
                    <td>Choose File
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk1" runat="server" Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Category1"
                            DataSourceID="dsCategory" TextField="Code" ValueField="Code"  Width="80">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload1" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk2" runat="server" Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Category2"
                            DataSourceID="dsCategory" TextField="Code" ValueField="Code"  Width="80">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload2" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk3" runat="server" Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Category3"
                            DataSourceID="dsCategory" TextField="Code" ValueField="Code"  Width="80">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload3" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk4" runat="server" Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Category4"
                            DataSourceID="dsCategory" TextField="Code" ValueField="Code"  Width="80">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload4" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk5" runat="server" Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Category5"
                            DataSourceID="dsCategory" TextField="Code" ValueField="Code"  Width="80">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload5" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk6" runat="server" Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                      <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Category6"
                            DataSourceID="dsCategory" TextField="Code" ValueField="Code"  Width="80">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload6" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
            </table>
            <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Upload" OnClick="ASPxButton1_Click">
            </dxe:ASPxButton>
            <dxe:ASPxLabel ID="lab" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red">
            </dxe:ASPxLabel>
            <div style="display: none">

                <dxe:ASPxTextBox ID="txt_Oid" runat="server" BackColor="Control" ReadOnly="true" Text="">
                </dxe:ASPxTextBox>
            </div>
        </div>
    </form>
</body>
</html>
