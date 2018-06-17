<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobBook.aspx.cs" Inherits="WareHouse_Job_JobBook" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;

        function OnSaveCallBack(v) {
            if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            }
            else if (v != null) {
                parent.AfterPopubMultiInv1(v);
            }
        }

        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_SKULine.Refresh();
            grid_PoRequest.Refresh();
            grid_DoIn.Refresh();
        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
         <wilson:DataSource ID="dsIssue" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobInfo"
                KeyMember="Id" FilterExpression="1=0" />
        <div>
            <dxwgv:ASPxGridView ID="grid_Issue" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="800px" AutoGenerateColumns="false" DataSourceID="dsIssue"
                OnInitNewRow="grid_Issue_InitNewRow"
                OnInit="grid_Issue_Init" OnCustomDataCallback="grid_Issue_CustomDataCallback" OnCustomCallback="grid_Issue_CustomCallback"
                OnHtmlEditFormCreated="grid_Issue_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px;width:970px">
                            <div style="display: none">
                                <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" BackColor="Control" ID="txt_DoNo" Text='<%# Eval("JobNo") %>' ClientInstanceName="txt_DoNo">
                                </dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                           <table style="text-align: left; padding: 2px 2px 2px 2px; width: 900px">
                                <tr>
                                     <td>Customer</td>
                                    <td colspan="3">
                                        <table cellpadding="0" cellspacing="0">
                                            <td width="90">
                                                <dxe:ASPxButtonEdit ID="txt_CustomerId" ClientInstanceName="txt_CustomerId" runat="server"
                                                    Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CustomerId") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_CustomerId,txt_CustomerName,txt_Contact,txt_Tel,txt_Fax,txt_Email,txt_PostalCode,memo_Address,null,null,'C');
                                                                    }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="250" ReadOnly="true" Text='<%# Eval("CustomerName") %>' BackColor="Control" ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </table>
                                    </td>
                                  
                                      <td>Job Date</td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_IssueDate" Width="160px" runat="server" Value='<%# Eval("JobDate") %>'
                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>JobType</td>
                                    <td>
                                        <dxe:ASPxComboBox ID="cmb_JobType" ClientInstanceName="cmb_JobType" runat="server" Width="150" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" Value='<%# Eval("JobType")%>'>
                                            <Items>
                                                <dxe:ListEditItem Text="Local Move" Value="Local Move" />
                                                <dxe:ListEditItem Text="Office Move" Value="Office Move" />
                                                <dxe:ListEditItem Text="International Move" Value="International Move" />
                                                <dxe:ListEditItem Text="Store & Move" Value="Store & Move" />
                                                <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                                <dxe:ListEditItem Text="Project" Value="Project" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                </tr>
                                <tr>
                                   <td rowspan="3">Address</td>
                                    <td rowspan="3" colspan="3">
                                        <dxe:ASPxMemo ID="memo_Address" Rows="5" Width="340" ClientInstanceName="memo_Address"
                                            runat="server" Text='<%# Eval("CustomerAdd") %>'>
                                        </dxe:ASPxMemo>
                                    </td>
                                     <td>Contact</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Contact" Width="160px" runat="server" ClientInstanceName="txt_Contact" Text='<%# Eval("Contact") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Tel</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Tel" Width="150" runat="server" ClientInstanceName="txt_Tel" Text='<%# Eval("Tel") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr style="text-align:left;">
                                    <td>Email</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Email" Width="160px" runat="server" ClientInstanceName="txt_Email" Text='<%# Eval("Email") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Fax</td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Fax" Width="150px" runat="server" ClientInstanceName="txt_Fax" Text='<%# Eval("Fax") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>

                                  
                                </tr>
                                <tr>
                                <tr>
                                    <td rowspan="2">Remark</td>
                                    <td colspan="3" rowspan="2">
                                        <dxe:ASPxMemo runat="server" Width="340" Rows="4" ID="txt_Remark" Text='<%# Eval("Remark") %>' ClientInstanceName="txt_Remark3">
                                        </dxe:ASPxMemo>
                                    </td>
                                      <td>PostalCode</td>
                                    <td>

                                        <dxe:ASPxTextBox runat="server" Width="160px" ID="txt_PostalCode" Text='<%# Eval("Postalcode") %>' ClientInstanceName="txt_PostalCode">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                             
                            </table>
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 970px">
                                 <tr>
                                   <td colspan="7" style="width:80%"></td>
                                    <td >

                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save" Enabled='<%# SafeValue.SafeString(Eval("JobStatus"),"Draft")!="Closed" %>'>
                                            <ClientSideEvents Click="function(s,e) {
                                                    detailGrid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                                 }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1100" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grd_Photo.Refresh();
                        grd_Attach.Refresh();
                }" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
