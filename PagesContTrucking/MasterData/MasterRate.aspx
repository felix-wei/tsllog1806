<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterRate.aspx.cs" Inherits="PagesContTrucking_MasterData_MasterRate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript">

        function CheckFile(s, e) {
            var fileType = (s.GetText().match(/^(.*)(\.)(.{1,8})$/)[3]).toLowerCase();
            //if ((ckb_Pdf.GetValue() && fileType == "pdf") || (ckb_OA.GetValue() && (fileType == "doc" || fileType == "docx" || fileType == "xls" || fileType == "xlsx" || fileType == "ppt" || fileType == "pptx")))
            if (fileType == "zip" || fileType == "csv" || fileType == "xls")
                btn_upload.SetEnabled(true);
            else {
                btn_upload.SetEnabled(false);
                alert("This file extension isn't allowed,pls select zip file");
            }
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
        <div>
            <table>
                <tr style="display:none">
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
                    <td>
                        Client
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_Code" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td colspan="3">
                        <dxuc:ASPxUploadControl ID="file_upload1" ClientInstanceName="file_upload1" runat="server" Width="224" ClientSideEvents-TextChanged="CheckFile">
                        </dxuc:ASPxUploadControl>
                    </td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btn_upload" ClientInstanceName="btn_upload" Text="Upload" Width="100" ClientEnabled="false" OnClick="btn_upload_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                    <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                        <ClientSideEvents Click="function(s,e){
                                gv.AddNewRow();
                                }" />
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
            <wilson:DataSource ID="dsMasterRate" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.MastertRate" KeyMember="Id" />
            <dxwgv:ASPxGridView ID="gv" runat="server" Width="100%" KeyFieldName="Id" ClientInstanceName="gv" AutoGenerateColumns="false"  DataSourceID="dsMasterRate"
                 OnInit="gv_Init" OnInitNewRow="gv_InitNewRow" OnRowInserting="gv_RowInserting" OnRowUpdating="gv_RowUpdating" OnRowDeleting="gv_RowDeleting" >
                <SettingsPager Mode="ShowPager" PageSize="20">
                </SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior  ConfirmDelete="true"/>
                <Columns>
                    <dx:GridViewCommandColumn VisibleIndex="0" Caption="#" Width="2%">
                        <EditButton Text="Edit" Visible="true"></EditButton>
                        <DeleteButton Text="Delete" Visible="true"></DeleteButton>
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataTextColumn Caption="Customer Code" FieldName="CustomerId" VisibleIndex="1" Width="8%">
                        <EditItemTemplate>
                            <dxe:ASPxButtonEdit ID="txt_CustomerId" ClientInstanceName="txt_CustomerId" runat="server"
                                Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("CustomerId") %>'>
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
                                   PopupParty(txt_CustomerId,txt_CustomerName,null,null,null,null,null,null,'CV');
                                }" />
                            </dxe:ASPxButtonEdit>
                        </EditItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer Name" FieldName="CustomerName" VisibleIndex="1" Width="30%">
                        <EditItemTemplate>
                             <dxe:ASPxTextBox runat="server" Width="260" Text='<%# Bind("CustomerName") %>' ID="txt_CustomerName" ClientInstanceName="txt_CustomerName">
                           </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" Width="10%">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="2" Width="10%">
                    </dxwgv:GridViewDataTextColumn>
                    <dx:GridViewDataSpinEditColumn Caption="Rate" FieldName="Price" VisibleIndex="2" Width="10%">
                        <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dx:GridViewDataSpinEditColumn>
                </Columns>
                <TotalSummary>

                    <dxwgv:ASPxSummaryItem FieldName="Code" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
             <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
}" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
