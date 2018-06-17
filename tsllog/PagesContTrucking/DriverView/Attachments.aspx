<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Attachments.aspx.cs" Inherits="PagesTpt_Local_Viewer_Attachments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var isUpload = false;
        function PopupUploadPhoto() {
            popubCtr.SetHeaderText('Upload Attachment');
            popubCtr.SetContentUrl('../Upload.aspx?Type=CTM&Sn=' + txt_JobNo.GetText());
            popubCtr.Show();
        }
        function AfterUploadPhoto() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmAttachment" KeyMember="Id" FilterExpression="1=0" />
            <div style="display: none">
                <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" runat="server"></dxe:ASPxTextBox>
            </div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton12" Width="150" runat="server" Text="Upload Attachments"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                             isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                <Settings />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                        <DataItemTemplate>
                            <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                            </dxe:ASPxButton>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                        </div>
                        <table width="100%">
                            <tr>
                                <td>Remark
                                </td>
                                <td>
                                    <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                    </dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload)
	    grd_Photo.Refresh();
}" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
