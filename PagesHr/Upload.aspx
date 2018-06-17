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
        <div>
            <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>Person
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_Sn" ClientInstanceName="txt_Sn" TextFormatString="{1}"
                                        EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList" DropDownButton-Visible="false" ReadOnly="true"
                                        DataSourceID="dsPerson" ValueField="Id" Width="100%" ValueType="System.Int32">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="display: none">
                            PersonId
                    <dxe:ASPxComboBox ID="cmb_PersonId" runat="server">
                    </dxe:ASPxComboBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>Remark
                    </td>
                    <td>Choose File
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk1" runat="server" Width="300">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload1" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk2" runat="server" Width="300">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload2" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk3" runat="server" Width="300">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload3" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk4" runat="server" Width="300">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload4" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk5" runat="server" Width="300">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxuc:ASPxUploadControl ID="file_upload5" runat="server" Width="250">
                        </dxuc:ASPxUploadControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Rmk6" runat="server" Width="300">
                        </dxe:ASPxTextBox>
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
