<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountPrintView1.aspx.cs" Inherits="ReportFreightSea_AccountPrintView1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PrintView</title>
    <script type="text/javascript">
        function check() {
            var reyx = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/;
            var len = txt_to.GetText().length;
            if (!reyx.test(txt_to.GetText())) {
                alert('Please enter the correct email £¡');
                txt_to.SelectAll();
            }
            else if (txt_cc.GetText().length > 0 && !reyx.test(txt_cc.GetText())) {
                alert('Please enter the correct email £¡');
                txt_cc.SelectAll();
            }
            else if (txt_subject.GetText().length == 0) {
                if (confirm("Subject is empty,do you still want to send ?")) {
                    btn_send.DoClick();
                }
                else {
                    txt_subject.SelectAll();
                }
            }
            else {
                btn_send.DoClick();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table style="width: 960px; border-bottom: solid 2px #888888; background: #CCCCCC;">
                    <tr style="display:none">
                        <td>
                            <dxe:ASPxLabel runat="server" ID="lb" Text="Doc No" Width="80"></dxe:ASPxLabel>
                        </td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_no" Width="200" ClientInstanceName="txt_no" runat="server"
                                Text="">
                            </dxe:ASPxTextBox>
                        </td>
                        <td colspan="2">
                            <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                                <ClientSideEvents Click="function(s, e) {
				LoadReport(txt_no.GetText());
                        }" />
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton9" Width="100" runat="server" Text="Print" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) {
                                    fun_print();
                     //window.print();
                        }" />
                            </dxe:ASPxButton>
                        </td>
                        <td width="60%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>Email To</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_to" ClientInstanceName="txt_to" runat="server" Width="200"></dxe:ASPxTextBox>
                        </td>
                        <td>Attach</td>
                        <td>
                            <dxuc:ASPxUploadControl ID="file_upload1" runat="server" Width="200">
                            </dxuc:ASPxUploadControl>
                        </td>
                        <td style="display:none">Content</td>
                        <td rowspan="5" valign="top" style="display:none">
                            <dxe:ASPxMemo ID="txt_content" ClientInstanceName="txt_content" runat="server" Width="200" Rows="8"></dxe:ASPxMemo>
                        </td>
                        <td style="display:none">
                            <dxe:ASPxButton ID="btn_send" runat="server" ClientInstanceName="btn_send"  Text="Send Email">
                                <ClientSideEvents Click="function(s,e){
                                        lbl_message.SetText('Sending...');
                                    }" />
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" Text="Send Email">
                                <ClientSideEvents Click="function(s,e){
                                    check();
                                    }" />
                            </dxe:ASPxButton>
                        </td>
                        <td>
                            <dxe:ASPxLabel ID="lbl_message" ClientInstanceName="lbl_message" runat="server" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>Email Cc</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_cc" ClientInstanceName="txt_cc" runat="server" Width="200"></dxe:ASPxTextBox>
                        </td>
                        <td></td>
                        <td>
                            <dxuc:ASPxUploadControl ID="file_upload2" runat="server" Width="200">
                            </dxuc:ASPxUploadControl>
                        </td>
                    </tr>
                    <tr>
                        <td>Subject</td>
                        <td>
                            <dxe:ASPxTextBox ID="txt_subject" ClientInstanceName="txt_subject" runat="server" Width="200"></dxe:ASPxTextBox>
                        </td>
                        <td></td>
                        <td>
                            <dxuc:ASPxUploadControl ID="file_upload3" runat="server" Width="200">
                            </dxuc:ASPxUploadControl>
                        </td>
                        <td></td>
                    </tr>
                </table>
            <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" ClientInstanceName="ASPxDocumentViewer1" runat="server" CssClass="MyDocumentViewer">
            <SettingsRemoteSourceReportServer></SettingsRemoteSourceReportServer>
            <SettingsReportViewer UseIFrame="true"></SettingsReportViewer>
            <ToolbarItems>
                <%--<dx:ReportToolbarButton ItemKind="Search" ToolTip="Display the search window" Enabled="False" />
                <dx:ReportToolbarSeparator />--%>
                <dx:ReportToolbarButton ItemKind="PrintReport" ToolTip="Print the report" />
                <dx:ReportToolbarButton ItemKind="PrintPage" ToolTip="Print the current page" />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" ToolTip="First Page" />
                <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" ToolTip="Previous Page" />
                <dx:ReportToolbarLabel Text="Page" />
                <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                </dx:ReportToolbarComboBox>
                <dx:ReportToolbarLabel Text="of" />
                <dx:ReportToolbarTextBox ItemKind="PageCount" />
                <dx:ReportToolbarButton ItemKind="NextPage" ToolTip="Next Page" />
                <dx:ReportToolbarButton ItemKind="LastPage" ToolTip="Last Page" />
                <dx:ReportToolbarSeparator />
                <dx:ReportToolbarButton ItemKind="SaveToDisk" ToolTip="Export a report and save it to the disk" />
                <%--<dx:ReportToolbarButton ItemKind="SaveToWindow" ToolTip="Export a report and show it in a new window" />--%>
                <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                    <Elements>
                        <dx:ListElement Text="Pdf" Value="pdf" />
                        <dx:ListElement Text="Xls" Value="xls" />
                        <dx:ListElement Text="Xlsx" Value="xlsx" />
                        <dx:ListElement Text="Rtf" Value="rtf" />
                        <dx:ListElement Text="Mht" Value="mht" />
                        <%--<dx:ListElement Text="Html" Value="html" />--%>
                        <dx:ListElement Text="Text" Value="txt" />
                        <dx:ListElement Text="Csv" Value="csv" />
                        <dx:ListElement Text="Image" Value="png" />
                    </Elements>
                </dx:ReportToolbarComboBox>
                <%--<dx:ReportToolbarSeparator />
                <dx:ReportToolbarLabel Text="Zoom Factor" />
                <dx:ReportToolbarComboBox ItemKind="Custom" Name="ScaleFactor" Width="50px">
                    <Elements>
                        <dx:ListElement Text="100" Value="100" />
                        <dx:ListElement Text="200" Value="200" />
                        <dx:ListElement Text="250" Value="250" />
                    </Elements>
                </dx:ReportToolbarComboBox>
                <dx:ReportToolbarLabel Text="%" />
                <dx:ReportToolbarButton Name="ShowEditor" Text="Show HTML Editor" />--%>
            </ToolbarItems>
                <StylesViewer>
                    <BookmarkSelectionBorder BorderColor="Gray" BorderStyle="Dashed" BorderWidth="3px"></BookmarkSelectionBorder>
                </StylesViewer>

                <StylesSplitter>
                    <Pane>
                        <Paddings Padding="16px"></Paddings>
                    </Pane>
                </StylesSplitter>
                <ClientSideEvents ToolbarItemValueChanged="function(s, e) { UpdateScaleFactor(s,e) }" />
                        <ClientSideEvents ToolbarItemClick="function(s, e) {
	if (e.item.name == 'ShowEditor') 
	pc.Show();}" />
        </dx:ASPxDocumentViewer><dxpc:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="pc"
            Height="400px" CloseAction="CloseButton" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <HeaderTemplate>
                HTML editor
            </HeaderTemplate>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
        <dxe:ASPxHtmlEditor Height="300px" ID="ASPxHtmlEditor1" runat="server" Width="500px">
            <SettingsImageUpload>
                <ValidationSettings AllowedContentTypes="image/jpeg, image/pjpeg, image/gif, image/png, image/x-png">
                </ValidationSettings>
            </SettingsImageUpload>
        </dxe:ASPxHtmlEditor>
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Close Editor">
                        <ClientSideEvents Click="function(s, e) { pc.Hide();}" />
                    </dxe:ASPxButton>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        
    </div>
    </form>
</body>
</html>
