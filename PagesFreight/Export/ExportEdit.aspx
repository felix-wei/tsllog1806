<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ExportEdit.aspx.cs" Inherits="Pages_ExportEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Export Job</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript" src="/Script/Sea/ExportEdit.js"></script>
    <script type="text/javascript" src="/Script/Sea/Export_Doc.js"></script>
    <script type="text/javascript">
        var isUpload = false;
        function AddExportCertificate(gridId) {
            grid = gridId;
            popubCtr1.SetHeaderText('Certificate');
            popubCtr1.SetContentUrl('/PagesFreight/Certificate/ExportCertificate.aspx?no=0&JobType=SE&RefN=' + txtSchNo.GetText() + '&JobN=' + txtHouseNo.GetText());
            popubCtr1.Show();
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsRefCont" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExportMkg" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsExport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExport" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsFullJob" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExport" KeyMember="SequenceId" FilterExpression="1=0" />

            <wilson:DataSource ID="dsMarking" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExportMkg" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsBlMarking" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExportMkg" KeyMember="SequenceId" FilterExpression="1=0" />

            <wilson:DataSource ID="dsExpDetail" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExportDetail" KeyMember="SequenceId" FilterExpression="1=0" />

            <wilson:DataSource ID="dsInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsVoucher" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAApPayable" KeyMember="SequenceId" FilterExpression="1=0" />

            <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaAttachment" KeyMember="SequenceId" FilterExpression="1=0" />

            <wilson:DataSource ID="dsCommercial" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaCommercial" KeyMember="Id" FilterExpression="1=0" />
             <wilson:DataSource ID="dsPacking" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaPacking" KeyMember="Id" FilterExpression="1=0" />
         <wilson:DataSource ID="dsCertificate" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaCertificate" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>Export No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_JobNo" ClientInstanceName="txt_JobNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Bkg RefNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_BkgRefNo" ClientInstanceName="txt_BkgRefNo" Width="150" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
			                if(txt_JobNo.GetText().length>0)
                           window.location='ExportEdit.aspx?no='+txt_JobNo.GetText();
			else if(txt_BkgRefNo.GetText().length>0)
                           window.location='ExportEdit.aspx?bkgNo='+txt_BkgRefNo.GetText();

                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='ExportRefList.aspx?refType='+lab_RefType.GetText();
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Export" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="SequenceId" Width="850px" AutoGenerateColumns="False" DataSourceID="dsExport"
                OnInitNewRow="grid_Export_InitNewRow" OnInit="grid_Export_Init" OnCustomCallback="grid_Export_CustomCallback"
                OnCustomDataCallback="grid_Export_CustomDataCallback" OnHtmlEditFormCreated="grid_Export_HtmlEditFormCreated">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                        <div style="padding: 2px 2px 2px 2px">
                            <table style="width: 100%;">
                                <tr>
                                    <td width="70%">
                                    </td>
                                    <td style="display:none">
                        <dxe:ASPxTextBox ID="lab_RefType" ClientInstanceName="lab_RefType" runat="server" Text="">
                        </dxe:ASPxTextBox></td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_Save" Width="100" runat="server" Text="Save" AutoPostBack="false"
                                            Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>' UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e) {
                                            detailGrid.PerformCallback('Save');
                                            }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_CancelBkg" Width="100" ClientInstanceName="btn_CancelBkg"
                                            Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'
                                            runat="server" Text="Void" AutoPostBack="False">
                                            <ClientSideEvents Click="function(s, e) {
                                                                    if(confirm('Confirm '+btn_CancelBkg.GetText()+' this job?'))
                                                                    {
                                                                         detailGrid.GetValuesOnCustomCallback('CancelBkg',OnCancelCallback);
                                                                     }
                                                                               }" />
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Master" AutoPostBack="false"
                                            UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s,e) {
                                           window.location='ExportRefEdit.aspx?no='+txt_Hbl_RefN.GetText()+'&refType='+lab_RefType.GetText();}" />
                                        </dxe:ASPxButton>
                                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton7" Width="100" runat="server" Text="Add New" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='ExportEdit.aspx?no=0&masterNo='+txt_Hbl_RefN.GetText();
                        }" />
                        </dxe:ASPxButton>
                    </td>
                                    <td>
                                      <div style="display: none">
                                        <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txtHouseId" runat="server" ReadOnly="true"
                                            BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="100">
                                        </dxe:ASPxTextBox>
                                    </div>
                                    </td>

                                </tr>
                            </table>
                            <table style="border: solid 1px black; padding: 2px 2px 2px 2px; width: 100%">
                                <tr>
                                    <td>Print Document:
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintSo("<%# Eval("RefNo") %>","<%# Eval("JobNo") %>")'>ShippingOrder</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintBkgConfirm("<%# Eval("RefNo") %>","<%# Eval("JobNo") %>")'>Bkg Confirmation</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintBL("<%# Eval("RefNo") %>","<%# Eval("JobNo") %>")'>BL</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintDraftBL("<%# Eval("RefNo") %>","<%# Eval("JobNo") %>")'>Draft BL</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintPL("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>P/L</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintJobOrder1("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Job Order(IMF)</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <a href="#" onclick='PrintApprovedPermit("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Approved Permit</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintHaulierSheet("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Haulier</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintCommercial("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Commercial</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintPacking("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Packing</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintShippingRequest("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>ShippingRequest</a>
                                    </td>
                                    <td>
                                        <a href="#" onclick='PrintJobOrder("<%# Eval("RefNo")%>","<%# Eval("JobNo")%>")'>Job Order</a>
                                    </td>
                                </tr>
                            </table>
                            <table>
                                <tr>
                                                        <td>Export No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtHouseNo" ClientInstanceName="txtHouseNo" runat="server" ReadOnly="true"
                                                                BackColor="Control" Text='<%# Eval("JobNo") %>' Width="150">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Bkg Ref No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtBkgNo" runat="server" Text='<%# Eval("BkgRefNo") %>' Width="140">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>HblN
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtHouseBl" runat="server" Text='<%# Eval("HblNo") %>' Width="150">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Department
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Dept" Width="130" runat="server" Text='<%# Eval("Dept")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                </tr>
                            </table>
                            <dxtc:ASPxPageControl runat="server" ID="pageControl_Hbl" Width="100%" Height="440px" ActiveTabIndex="0">
                                <TabPages>
                                    <dxtc:TabPage Text="Booking Info" ClientVisible="false">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_bkg" runat="server" Visible="false">
                                                <div style="display: none">
                                                    <dxe:ASPxTextBox ID="txtOid" ClientInstanceName="txtOid" runat="server" ReadOnly="true"
                                                        BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="170">
                                                    </dxe:ASPxTextBox>
                                                    <dxe:ASPxTextBox ID="txtSchNo" ClientInstanceName="txtSchNo" runat="server" ReadOnly="true"
                                                        BackColor="Control" Text='<%# Eval("RefNo") %>' Width="170">
                                                    </dxe:ASPxTextBox>
                                                </div>
                                                <table border="0">
                                                    <tr>
                                                        <td>Final Dest
                                                        </td>
                                                        <td class="auto-style1">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txtFinalDest" ClientInstanceName="txtFinalDest" runat="server"
                                                                            Width="165" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("FinDest") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txtFinalDest);
                                                                                            }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>Tranship
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ReadOnly="true" BackColor="Control"
                                                                            ID="cmb_Hbl_Tranship" Width="60" runat="server" Text='<%# Eval("TsInd") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtTsImpNo" ReadOnly="true" BackColor="Control" runat="server"
                                                                            Text='<%# Eval("TsJobNo") %>' Width="100">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Value Currency
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="btn_ValueCurrency" ClientInstanceName="btn_ValueCurrency" runat="server" Width="170" MaxLength="3" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ValueCurrency") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupCurrency(btn_ValueCurrency,spin_ValueExRate);
                                                                                                }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Value ExRate
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="170" ID="spin_ValueExRate"
                                                                ClientInstanceName="spin_ValueExRate" runat="server" DisplayFormatString="0.000000" DecimalPlaces="6"
                                                                Value='<%# Bind("ValueExRate")%>' Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Value Amt
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="170" ID="spin_ValueAmt"
                                                                runat="server" DisplayFormatString="0.00" Value='<%# Bind("ValueAmt")%>' Increment="0" DecimalPlaces="2">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>As Agent</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbx_AsAgent" runat="server" Value='<%# Eval("AsAgent") %>' Width="170">
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Quote No 
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txtQuoteNo" ClientInstanceName="txtQuoteNo" runat="server"
                                                                Width="170" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("QuoteNo") %>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupQuote(txtQuoteNo);
                                                                                                }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">Collect From
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxMemo ID="txtCollection1" Rows="3" runat="server" Width="100%" Text='<%# Eval("CollectFrom") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">Delivery To
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxMemo Rows="3" ID="txtMarking1" runat="server" Width="100%" Text='<%# Eval("Marking") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="1">Remarks
                                                        </td>
                                                        <td colspan="5">
                                                            <dxe:ASPxMemo Rows="3" ID="txtRemarks1" runat="server" Width="100%" Text='<%# Eval("Remark") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                 <tr>
                                                   <td colspan="6">
												   <hr>
                                                        <table >
                                                            <tr>
                                                                <td style="width: 80px;">Creation</td>
                                                                <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                <td style="width: 80px;">Last Updated</td>
                                                                <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                <td style="width: 80px;">Job Status</td>
                                                                <td style="width: 160px"><dxe:ASPxLabel runat="server" ID="lb_JobStatus" Text="" /></td>
                                                            </tr>
                                                        </table>
												   <hr>
                                                    </td>
                                                </tr>
                                                </table>
                                                <hr />
                                                <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Add Booking" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_mkgsBl.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="grid_mkgsBl" ClientInstanceName="grid_mkgsBl" runat="server"
                                                    DataSourceID="dsBlMarking" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_mkgsBl_DataSelect"
                                                    OnInit="grid_mkgsBl_Init" OnInitNewRow="grid_mkgsBl_InitNewRow"
                                                    OnRowInserting="grid_mkgsBl_RowInserting" OnRowDeleting="grid_mkgsBl_RowDeleting">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_mkgsBl.StartEditRow("+Container.VisibleIndex+") }" %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_mkgsBl.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PACK TYPE" FieldName="PackageType" VisibleIndex="7">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="StatusCode" FieldName="StatusCode" VisibleIndex="8" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="Weight" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                <dxwgv:ASPxSummaryItem FieldName="Volume" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0:0}" />
                            </TotalSummary>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_Mkg_HouseNo" runat="server" Text='<%# Bind("JobNo") %>'>
                                                                </dxe:ASPxTextBox>
                                                                <dxe:ASPxTextBox ID="txt_Mkg_RefNo" runat="server" Text='<%# Bind("RefNo") %>'>
                                                                </dxe:ASPxTextBox>
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td>Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_wt" Value='<%# Bind("Weight")%>' InCrement="0"  DecimalPlaces="3">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Volume
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_m3" Value='<%# Bind("Volume")%>' InCrement="0"  DecimalPlaces="3">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Qty
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120"
                                                                            ID="spin_pkg" Text='<%# Bind("Qty")%>' InCrement="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Pack Type
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType" runat="server"
                                                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType,0);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Gross Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_BkgGrosswt" Value='<%# Bind("GrossWt")%>' InCrement="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Net Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_BkgNetwt" Value='<%# Bind("NetWt")%>' InCrement="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Remark
                                                                    </td>
                                                                    <td colspan="7">
                                                                        <dxe:ASPxMemo runat="server" Rows="6" Width="100%" ID="txt_mkg1" MaxLength="500" Text='<%# Bind("Marking")%>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="UpdateMkgs" runat="server" NavigateUrl="#" Text="Update"  Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                                <ClientSideEvents Click="function(s,e){grid_mkgsBl.UpdateEdit();}" />
                                                                            </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>

                                    <dxtc:TabPage Text="Bl Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl2" runat="server">
                                                <div style="display: none">
                                                    <dxe:ASPxTextBox ID="txtHouseId" ClientInstanceName="txtHouseId" runat="server" ReadOnly="true"
                                                        BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="160">
                                                    </dxe:ASPxTextBox>
                                                </div>
                                                <table border="0">
                                                    <tr>
                                                        <td>Client/Bill To
                                                        </td>
                                                        <td colspan="3">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="104">
                                                                        <dxe:ASPxButtonEdit ID="txtCustomerId" ClientInstanceName="txtCustomerId" runat="server"
                                                                            Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CustomerId") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupParty(txtCustomerId,txtCustomer,'C');
                                                                                                }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtCustomer" ClientInstanceName="txtCustomer" ReadOnly="true"
                                                                            BackColor="Control" runat="server" Text='' Width="314">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            Ref No
                                                        </td>
                                                        <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_Hbl_RefN" ClientInstanceName="txt_Hbl_RefN" runat="server" ReadOnly="true"
                                                                            Width="170" HorizontalAlign="Left" AutoPostBack="False" BackColor="Control" Text='<%# Eval("RefNo") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupExpRef(txt_Hbl_RefN);
                                                                                                }" />
                                                                        </dxe:ASPxButtonEdit>

                                                        </td>
                                                    </tr>
                                                    <tr><td colspan="6"><hr /></td></tr>
                                                    
                                                    <tr>
                                                        <td>Shipper
                                                        </td>
                                                        <td colspan="3">
                                                            <table cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="104">
                                                                        <dxe:ASPxButtonEdit ID="txtShipperId" ClientInstanceName="txtShipperId" runat="server" Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("ShipperId") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                            PopupShipper(txtShipperId,txtShipperName,txtContact,txtTel,txtFax,txtEmail,txt_Shipper2,'C');
                                                                                                }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox ID="txtShipperName" ClientInstanceName="txtShipperName" runat="server" Text='<%# Eval("ShipperName") %>'
                                                                            Width="314">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            Contact
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtContact" ClientInstanceName="txtContact" runat="server" Text='<%# Eval("ShipperContact") %>'
                                                                Width="170">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fax No
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtFax" runat="server" ClientInstanceName="txtFax" Width="140" Text='<%# Eval("ShipperFax") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Tel No </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtTel" runat="server" ClientInstanceName="txtTel" Width="170" Text='<%# Eval("ShipperTel") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Email </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtEmail" runat="server" ClientInstanceName="txtEmail" Width="170" Text='<%# Eval("ShipperEmail") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pol
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Hbl_Pol" ClientInstanceName="txt_Hbl_Pol" runat="server"
                                                                MaxLength="5" Width="140" Text='<%# Eval("Pol")%>' HorizontalAlign="Left" AutoPostBack="False">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_Pol,txt_pol_name);
                                                                                            }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Name
                                                        </td>
                                                        <td>

                                                            <dxe:ASPxTextBox ID="txt_pol_name" ClientInstanceName="txt_pol_name" Width="170" runat="server"
                                                                Text='<%# Eval("PlaceLoadingName")%>'>
                                                            </dxe:ASPxTextBox>

                                                        </td>
                                                        <td>Frt Term
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True"
                                                                ID="cmb_FrtTerm" Width="170" runat="server" Text='<%# Eval("FrtTerm") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="FP" Value="FP" />
                                                                    <dxe:ListEditItem Text="FC" Value="FC" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Pod
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Hbl_Pod" ClientInstanceName="txt_Hbl_Pod" runat="server" MaxLength="5" Text='<%# Eval("Pod")%>'
                                                                Width="140" HorizontalAlign="Left" AutoPostBack="False">
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_Pod,txt_pod_name);
                                                                                            }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Name
                                                        </td>
                                                        <td>

                                                            <dxe:ASPxTextBox ID="txt_pod_name" ClientInstanceName="txt_pod_name" Width="170" runat="server"
                                                                Text='<%# Eval("PlaceDischargeName")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Pre-Carriage
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Hbl_PreCarriage" Width="170" runat="server" Text='<%# Eval("PreCarriage")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Place Del
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Hbl_DelId" ClientInstanceName="txt_Hbl_DelId" runat="server" MaxLength="5"
                                                                Width="140" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PlaceDeliveryId")%>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_DelId,txt_Hbl_DelName);
                                                                                            }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Del Name
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Hbl_DelName" ClientInstanceName="txt_Hbl_DelName" Width="170"
                                                                runat="server" Text='<%# Eval("PlaceDeliveryname")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Del Terms
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Hbl_DelTerm" ClientInstanceName="txt_Hbl_DelTerm" runat="server"
                                                                Width="170" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PlaceDeliveryTerm")%>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupGeneralCode(txt_Hbl_DelTerm);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Place Rec
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Hbl_RecId" ClientInstanceName="txt_Hbl_RecId" runat="server" MaxLength="5"
                                                                Width="140" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PlaceReceiveId")%>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupPort(txt_Hbl_RecId,txt_Hbl_RecName);
                                                                                            }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Rec Name
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Hbl_RecName" ClientInstanceName="txt_Hbl_RecName"
                                                                Width="170" runat="server" Text='<%# Eval("PlaceReceiveName")%>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Rec Term
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_Hbl_RecTerm" ClientInstanceName="txt_Hbl_RecTerm" runat="server"
                                                                Width="170" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("PlaceReceiveTerm")%>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupGeneralCode(txt_Hbl_RecTerm);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Ship On Board
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="140" ID="cmb_Hbl_ShipOnBrd"
                                                                runat="server" Text='<%# Eval("ShipOnBoardInd")%>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>ShipOnBrd Date
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxDateEdit ID="date_Hbl_ShipOnBrdDate" Width="170" runat="server" Value='<%# Eval("ShipOnBoardDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                        <td>Express Bl
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="40" ID="cmb_Hbl_ExpressBl"
                                                                            runat="server" Text='<%# Eval("ExpressBl") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Surrender BL
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True"
                                                                            ID="cmb_SurrenderBl" Width="38" runat="server" Text='<%# Eval("SurrenderBl") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Y" Value="Y" />
                                                                                <dxe:ListEditItem Text="N" Value="N" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Imp Charge
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit ID="spin_Hbl_ImpChg" Width="140" runat="server" Text='<%# Eval("ImpCharge")%>' Increment="0">
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    <td>Portnet No
                                                    </td>
                                                    <td >
                                                        <dxe:ASPxTextBox runat="server" Width="170" ID="txt_PoNO" Text='<%# Eval("PoNo")%>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td width="100">
                                                                        <dxe:ASPxButton ID="btn_Shipper_Pick1" runat="server" HorizontalAlign="Left" Width="80" Text="Shipper" AutoPostBack="False">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                        PopupCustAdr(null,txt_Shipper2);
                                                                            }" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txt_Shipper2" Rows="5" ClientInstanceName="txt_Shipper2" runat="server"
                                                                            Width="300" Text='<%# Eval("SShipperRemark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td width="100">
                                                                        <dxe:ASPxButton ID="btn_Consignee_Pick" runat="server" Text="Consignee" AutoPostBack="False">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                        PopupAgentAdr(null,txt_Consigee2);
                                                                            }" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txt_Consigee2" Rows="5" ClientInstanceName="txt_Consigee2" runat="server"
                                                                            Width="300" Text='<%# Eval("SConsigneeRemark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6" style="border-bottom: solid 1px black;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td width="100">
                                                                        <dxe:ASPxButton ID="btn_Party_Pick" runat="server" Text="Notify Party" AutoPostBack="False">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                        PopupAgentAdr(null,txt_Party2);
                                                                            }" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo ID="txt_Party2" Rows="5" ClientInstanceName="txt_Party2" runat="server"
                                                                            Width="300" Text='<%# Eval("SNotifyPartyRemark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td colspan="3">
                                                            <table>
                                                                <tr>
                                                                    <td width="100">
                                                                        <dxe:ASPxButton ID="btn_Agent_Pick1" runat="server" HorizontalAlign="Left" Width="85" Text="Agent" AutoPostBack="False">
                                                                            <ClientSideEvents Click="function(s, e) {
                                                                        PopupAgentAdr(null,txt_Agt2);
                                                                            }" />
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo Rows="5" ID="txt_Agt2" ClientInstanceName="txt_Agt2" runat="server"
                                                                            Width="300" Text='<%# Eval("SAgentRemark") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Transport Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_tpt" runat="server">
                                                <table>
                                                    <tr>
                                                        <td colspan="6" style="background-color: Gray; color: White;">
                                                            <b>Transportatin Info</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="btn_Ref_H_Haulier" runat="server" HorizontalAlign="Left" Width="100" Text="Haulier" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupHaulier(txt_Ref_H_Haulier,txt_Ref_H_CrNo,txt_Ref_H_Fax,txt_Ref_H_Attention);
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_Hauler" Width="475" ClientInstanceName="txt_Ref_H_Haulier"
                                                                runat="server" Text='<%# Eval("HaulierName") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>UEN No</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_CrNo" Width="170" ClientInstanceName="txt_Ref_H_CrNo"
                                                                runat="server" Text='<%# Eval("HaulierCrNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="110">Attention
                                                        </td>
                                                        <td width="188">
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_Attention" Width="170" ClientInstanceName="txt_Ref_H_Attention"
                                                                runat="server" Text='<%# Eval("HaulierAttention") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="110">Fax
                                                        </td>
                                                        <td width="190">
                                                            <dxe:ASPxTextBox ID="txt_Ref_H_Fax" Width="170" ClientInstanceName="txt_Ref_H_Fax"
                                                                runat="server" Text='<%# Eval("HaulierFax") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td width="110">
                                                        </td>
                                                        <td width="190">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Driver Name</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_DriverName" Width="170" ClientInstanceName="txt_DriverName"
                                                                runat="server" Text='<%# Eval("DriverName") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Driver Mobile</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_DriverMobile" Width="170" ClientInstanceName="txt_DriverMobile"
                                                                runat="server" Text='<%# Eval("DriverMobile") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Driver License</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_DriverLicense" Width="170" ClientInstanceName="txt_DriverLicense"
                                                                runat="server" Text='<%# Eval("DriverLicense") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                         <td>VehicleNo</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_VehicleNo" Width="170" ClientInstanceName="txt_VehicleNo"
                                                                runat="server" Text='<%# Eval("VehicleNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Vehicle Type</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_VehicleType" Width="170" ClientInstanceName="txt_VehicleType"
                                                                runat="server" Text='<%# Eval("VehicleType") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Driver Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="me_DriverRemark" Width="475" ClientInstanceName="me_DriverRemark"
                                                                runat="server" Text='<%# Eval("DriverRemark") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="2">
                                                            <dxe:ASPxButton ID="ASPxButton11" runat="server" HorizontalAlign="Left" Width="100" Text="Collect From" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupPartyAdr(null,txt_Ref_H_CltFrm,'CV');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td rowspan="2" colspan="3">
                                                            <dxe:ASPxMemo ID="txt_Ref_H_CltFrm" Rows="4" Width="475" ClientInstanceName="txt_Ref_H_CltFrm"
                                                                runat="server" Text='<%# Eval("HaulierCollect") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Date</td>
                                                        <td>
                                                             <dxe:ASPxDateEdit ID="date_Ref_H_CltDate" Width="170" runat="server" Value='<%# Eval("HaulierCollectDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Time</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="date_Ref_H_CltTime" Width="170" runat="server" Text='<%# Eval("HaulierCollectTime") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td rowspan="2">
                                                            <dxe:ASPxButton ID="ASPxButton12" runat="server" HorizontalAlign="Left" Width="100" Text="Truck To" AutoPostBack="False">
                                                                <ClientSideEvents Click="function(s, e) {
                                                                PopupPartyAdr(null,txt_Ref_H_TruckTo,'CV');
                                                                    }" />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td rowspan="2" colspan="3">
                                                            <dxe:ASPxMemo ID="txt_Ref_H_TruckTo" Rows="4" Width="475" ClientInstanceName="txt_Ref_H_TruckTo"
                                                                runat="server" Text='<%# Eval("HaulierTruck") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Date</td>
                                                        <td>
                                                             <dxe:ASPxDateEdit ID="date_Ref_H_DlvDate" Width="170" runat="server" Value='<%# Eval("HaulierDeliveryDate") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Time</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="date_Ref_H_DlvTime" Width="170" runat="server" Text='<%# Eval("HaulierDeliveryTime") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>SENT TO</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_SendTo" Width="170" ClientInstanceName="txt_DriverName"
                                                                runat="server" Text='<%# Eval("HaulierSendTo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Stuff/Unstuff By</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_StuffBy" Width="170" ClientInstanceName="txt_DriverMobile"
                                                                runat="server" Text='<%# Eval("HaulierStuffBy") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Shipping coload</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_Coload" Width="170" ClientInstanceName="txt_DriverLicense"
                                                                runat="server" Text='<%# Eval("HaulierCoload") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Person</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_Person" Width="170" ClientInstanceName="txt_DriverName"
                                                                runat="server" Text='<%# Eval("HaulierPerson") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Telephone</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txt_H_PersonTel" Width="170" ClientInstanceName="txt_DriverMobile"
                                                                runat="server" Text='<%# Eval("HaulierPersonTel") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remarks
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo Rows="3" ID="txt_Ref_H_Rmk1" Width="475" row="3" runat="server" Text='<%# Eval("HaulierRemark") %>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" style="background-color: Gray; color: White;">
                                                            <b>Permit Information</b>
                                                        </td>
                                                    </tr>
                                                    <tr style="border-bottom: solid 1px black">
                                                        <td>Permit
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo runat="server" Rows="3" Width="475" ID="txt_Hbl_Permit" Text='<%# Eval("PermitRmk")%>'>
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                <dxtc:TabPage Text="DG Cargo" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl4" runat="server">
                                            <table>
                                                <tr>
                                                    <td width="120">IMDG Class</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgImdgClass" runat="server" Width="160" Text='<%# Eval("DgImdgClass") %>'>
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">UN Number</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgUnNo" runat="server" Text='<%# Eval("DgUnNo") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td rowspan="10" style="padding-left: 20px;" valign="top">
                                                        <a href="/__dg/dg_form.pdf" target="_blank">DG Cargo Form</a><br />
                                                        <a href="/__dg/dg_requirement.pdf" target="_blank">DG Cargo Requirement</a><br />
                                                        <br />
                                                        <a href="/__dg/dg_imdg_code.pdf" target="_blank">IMDG Code</a><br />
                                                        <a href="/__dg/un_packaging.pdf" target="_blank">UN Packaging Code</a><br />
                                                        <a href="/__dg/dg_class_coload.png" target="_blank">DG Cargo Coload Compliance</a><br />
                                                        <a href="/__dg/dg_chart.pdf" target="_blank">DG Chart Information</a><br />

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Proper Shipping Name</td>
                                                    <td colspan="3">
                                                        <dxe:ASPxMemo ID="memo_DgShippingName" runat="server" Text='<%# Eval("DgShippingName") %>' Width="450" Rows="4">
                                                        </dxe:ASPxMemo>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td width="120">Technical Name</td>
                                                    <td colspan="3">
                                                        <dxe:ASPxMemo ID="memo_DgTecnicalName" runat="server" Text='<%# Eval("DgTecnicalName") %>' Width="450" Rows="4">
                                                        </dxe:ASPxMemo>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td width="120">MFAG Number 1</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgMfagNo1" runat="server" Text='<%# Eval("DgMfagNo1") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">MFAG Number 2</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgMfagNo2"  runat="server" Text='<%# Eval("DgMfagNo2") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">EMS (fire)</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgEmsFire" runat="server" Text='<%# Eval("DgEmsFire") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">EMS (spill)</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgEmsSpill" runat="server" Text='<%# Eval("DgEmsSpill") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120"></td>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="ack_DgLimitedQtyInd" runat="server" Text='Limited Quantity' Width="160">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                    <td width="120"></td>
                                                    <td>
                                                        <dxe:ASPxCheckBox ID="ack_DgExemptedQtyInd" runat="server" Text='Exempted Quantity' Width="160">
                                                        </dxe:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Net Weight</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgNetWeight" runat="server" Text='<%# Eval("DgNetWeight") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Flashpoint</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgFlashPoint" runat="server" Text='<%# Eval("DgFlashPoint") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Radioactivity</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgRadio" runat="server" Text='<%# Eval("DgRadio") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Page Number</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgPageNo" runat="server" Text='<%# Eval("DgPageNo") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Packing Group</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgPackingGroup" runat="server" Text='<%# Eval("DgPackingGroup") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Packing Type Code</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgPackingTypeCode" runat="server" Text='<%# Eval("DgPackingTypeCode") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="120">Transport Code</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgTransportCode" runat="server" Text='<%# Eval("DgTransportCode") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                    <td width="120">Category</td>
                                                    <td>
                                                        <dxe:ASPxTextBox ID="txt_DgCategory" runat="server" Text='<%# Eval("DgCategory") %>' Width="160">
                                                        </dxe:ASPxTextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                    <dxtc:TabPage Text="Detail" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl51" runat="server">
                                                <dxe:ASPxButton ID="btn_AddDet" Width="150" runat="server" Text="Add Detail" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_ExpDet.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="grid_ExpDet" ClientInstanceName="grid_ExpDet" runat="server"
                                                    DataSourceID="dsExpDetail" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_ExpDet_DataSelect"
                                                    OnInit="grid_ExpDet_Init" OnInitNewRow="grid_ExpDet_InitNewRow" OnRowInserting="grid_ExpDet_RowInserting" OnRowDeleting="grid_ExpDet_RowDeleting"
                                                    OnRowUpdating="grid_ExpDet_RowUpdating">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" 
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_ExpDet.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE"%>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_ExpDet.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ChargeCode" FieldName="ChgCode" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Qty" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Price" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Amount" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="Currency" VisibleIndex="6">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="PrintTerm" VisibleIndex="7">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>Charge Code
                                                                    </td>
                                                                                <td>
                                                                                    <dxe:ASPxButtonEdit ID="txt_detChgCode" ClientInstanceName="txt_detChgCode" runat="server"
                                                                                        Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("ChgCode") %>'>
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupChgCode(txt_detChgCode,txt_detDes);
                                                                    }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                    <td>Description
                                                                    </td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_detDes" Text='<%# Bind("Description") %>'
                                                                            ClientInstanceName="txt_detDes">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Print Ind
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxCheckBox ID="ck_detPrintInd" runat="server" Value='<%# Bind("PrintInd") %>'>
                                                                        </dxe:ASPxCheckBox>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" ReadOnly="false"
                                                                            ID="cmbprintterm" Width="100" runat="server" Text='<%# Bind("PrintTerm") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="PREPAID" Value="PREPAID" />
                                                                                <dxe:ListEditItem Text="COLLECT" Value="COLLECT" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Qty
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" DecimalPlaces="3" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_detQty" ClientInstanceName="spin_detQty"
                                                                            Value='<%# Bind("Qty")%>' Increment="0">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_detQty.GetText(),spin_detPrice.GetText(),1,2,spin_detAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Price
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_detPrice" ClientInstanceName="spin_detPrice"
                                                                            Value='<%# Bind("Price")%>' Increment="0">
                                                                            <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_detQty.GetText(),spin_detPrice.GetText(),1,2,spin_detAmt);
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Amount
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120"
                                                                            DisplayFormatString="0.00" ReadOnly="true" BackColor="Control" ID="spin_detAmt"
                                                                            ClientInstanceName="spin_detAmt" Text='<%# Eval("Amount")%>'>
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Currency
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButtonEdit ID="txt_detCurrency" ClientInstanceName="txt_detCurrency" runat="server"
                                                                                        Width="122" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("Currency")%>' MaxLength="3">
                                                                                        <Buttons>
                                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                                        </Buttons>
                                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_detCurrency,null);
                                                                    }" />
                                                                                    </dxe:ASPxButtonEdit>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateMkg" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                    <ClientSideEvents Click="function(s,e){grid_ExpDet.UpdateEdit();}" />
                                                                </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Billing" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_Inv" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton16" Width="150" runat="server" Text="Add Invoice" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoice(Grid_Invoice_Import, "SE", txt_Hbl_RefN.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td style="display:none">
                                                            <dxe:ASPxButton ID="btn_AddNewInv" Width="150" runat="server" Text="Add Invoice-new" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddInvoiceNew(Grid_Invoice_Import, "SE", txt_Hbl_RefN.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton8" Width="150" runat="server" Text="Add CN" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddCn(Grid_Invoice_Import, "SE", txt_Hbl_RefN.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButton ID="ASPxButton9" Width="150" runat="server" Text="Add DN" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                                <ClientSideEvents Click='function(s,e) {
                                                       AddDn(Grid_Invoice_Import, "SE", txt_Hbl_RefN.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                            </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="Grid_Invoice_Import" ClientInstanceName="Grid_Invoice_Import"
                                                    runat="server" KeyFieldName="InvoiceN" DataSourceID="dsInvoice" Width="100%"
                                                    OnBeforePerformDataSelect="Grid_Invoice_Import_DataSelect">
                                                    <Columns>
                                                        <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                            <DataItemTemplate>
                                                                <a onclick='ShowInvoice(Grid_Invoice_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintInvoice("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1" Width="60">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1" Width="60">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2" Width="60">
                                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="4" Width="50">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="5" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="6" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                </dxwgv:ASPxGridView>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton5" Width="150" runat="server" Text="Add Voucher" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddVoucher(Grid_Payable_Import, "SE", txt_Hbl_RefN.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton10" Width="150" runat="server" Text="Add Payable" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                            AutoPostBack="false" UseSubmitBehavior="false">
                                                            <ClientSideEvents Click='function(s,e) {
                                                       AddPayable(Grid_Payable_Import, "SE", txt_Hbl_RefN.GetText(), txtHouseNo.GetText());
                                                        }' />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="Grid_Payable_Import" ClientInstanceName="Grid_Payable_Import"
                                                runat="server" KeyFieldName="SequenceId" DataSourceID="dsVoucher" Width="100%"
                                                OnBeforePerformDataSelect="Grid_Payable_Import_DataSelect">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                        <DataItemTemplate>
                                                            <a onclick='ShowPayable(Grid_Payable_Import,"<%# Eval("DocNo") %>","<%# Eval("DocType") %>");'>Edit</a>&nbsp; <a onclick='PrintPayable("<%# Eval("DocNo")%>","<%# Eval("DocType") %>","<%# Eval("MastType") %>")'>Print</a>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1" Width="60">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Doc No" FieldName="DocNo" VisibleIndex="1" Width="60">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2" Width="60">
                                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="4" Width="50">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="5" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="6" Width="50">
                                                            <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                                            </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="POD Info" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl_pod" runat="server">
                                                <table>
                                                    <tr>
                                                        <td>PODBy</td>
                                                        <td><dxe:ASPxButtonEdit ID="txtPODBy" ClientInstanceName="txtPODBy" Text='<%# Eval("PODBy") %>'
                                                                        runat="server" Width="250" HorizontalAlign="Left" AutoPostBack="False">
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                                        PopupCust(null,txtPODBy);
                                                                                                            }" />
                                                                    </dxe:ASPxButtonEdit></td>
                                                        <td>PODTime</td>
                                                        <td> <dxe:ASPxDateEdit ID="date_PodTime" Width="100" runat="server" Value='<%# Eval("PODTime") %>'
                                                                EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                                            </dxe:ASPxDateEdit></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remark</td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo Rows="3" ID="me_Remark" Width="440" runat="server" Text='<%# Eval("PODRemark") %>'>
                                                            </dxe:ASPxMemo></td>
                                                    </tr>
                                                     <tr>
                                                        <td>Update User</td>
                                                        <td><%# Eval("PODUpdateUser") %></td>
                                                        <td>Update Time</td>
                                                        <td><%# SafeValue.SafeDateStr(Eval("PODUpdateTime")) %></td>
                                                    </tr>
                                                    
                                                </table>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Commercial">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                 <dxe:ASPxButton ID="ASPxButton1" Width="150" runat="server" Text="Add Commercial" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_Commercial.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="grid_Commercial" runat="server" ClientInstanceName="grid_Commercial" 
                                                    OnInit="grid_Commercial_Init" OnInitNewRow="grid_Commercial_InitNewRow" OnRowInserting="grid_Commercial_RowInserting"  
                                                    OnRowUpdating="grid_Commercial_RowUpdating" Width="960" OnRowDeleting="grid_Commercial_RowDeleting" KeyFieldName="Id" DataSourceID="dsCommercial" OnBeforePerformDataSelect="grid_Commercial_DataSelect">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                         <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" 
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Commercial.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE"%>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Commercial.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PackageType" FieldName="PackageType" VisibleIndex="1" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="3" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="4" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="Amount" VisibleIndex="5" Width="100">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <table style="width:960px;">
                                                                <tr>
                                                                    <td>Code</td>
                                                                    <td><dxe:ASPxTextBox ID="txt_Code" runat="server" Text='<%#Bind("Code") %>' Width="150">
                                                                        </dxe:ASPxTextBox></td>
                                                                    <td>PackageType</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType" runat="server"
                                                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType,2);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                    <td>Qty</td>
                                                                    <td>
                                                                       <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="50"
                                                                            ID="spin_ComQty" ClientInstanceName="spin_ComQty" Text='<%# Bind("Qty")%>' InCrement="0">
                                                                             <ClientSideEvents ValueChanged="function(s, e) {
                                                           spin_ComAmount.SetText(Calculate(spin_ComQty.GetText(),spin_ComPrice.GetText(),'1.00',2));
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>

                                                                    <td>Price</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="80" ID="spin_ComPrice"
                                                                            ClientInstanceName="spin_ComPrice" runat="server" DisplayFormatString="0.00" DecimalPlaces="2"
                                                                            Value='<%# Bind("Price")%>' Increment="0">
                                                                             <ClientSideEvents ValueChanged="function(s, e) {
                                                           spin_ComAmount.SetText(Calculate(spin_ComQty.GetText(),spin_ComPrice.GetText(),'1.00',2));
	                                                   }" />
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Amount</td>
                                                                    <td><dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="80" ID="spin_ComAmount" ReadOnly="true" BackColor="Control"
                                                                            ClientInstanceName="spin_ComAmount" runat="server" DisplayFormatString="0.00" 
                                                                            Value='<%# Eval("Amount")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                        <div style="display:none">
                                                                            <dxe:ASPxLabel ID="lbl_RefType" runat="server" Text='<%# Eval("RefType")%>'></dxe:ASPxLabel>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Description</td>
                                                                    <td colspan="13">
                                                                        <dxe:ASPxMemo ID="memo_Des" runat="server" Text='<%# Bind("Description")%>' Width="100%" Rows="3"></dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="14">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                           <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update"  Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                                <ClientSideEvents Click="function(s,e){grid_Commercial.UpdateEdit();}" />
                                                                            </dxe:ASPxHyperLink>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>                                                   
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Packing">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <dxe:ASPxButton ID="btn_AddPacking" Width="150" runat="server" Text="Add Packing" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        gird_Packing.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                <dxwgv:ASPxGridView ID="gird_Packing" runat="server" ClientInstanceName="gird_Packing" 
                                                    OnInit="gird_Packing_Init" OnInitNewRow="gird_Packing_InitNewRow" OnRowInserting="gird_Packing_RowInserting"  
                                                    OnRowUpdating="gird_Packing_RowUpdating" Width="100%" OnRowDeleting="gird_Packing_RowDeleting" KeyFieldName="Id" DataSourceID="dsPacking" OnBeforePerformDataSelect="gird_Packing_DataSelect">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                         <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { gird_Packing.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){gird_Packing.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PackageType" FieldName="PackageType" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Dimension" FieldName="Dimension" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>Weight</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="100" ID="spin_Weight"
                                                                            ClientInstanceName="spin_Weight" runat="server" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                            Value='<%# Bind("Weight")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Volume</td>
                                                                    <td><dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="100" ID="spin_Volume"
                                                                            ClientInstanceName="spin_Volume" runat="server" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                            Value='<%# Bind("Volume")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                        <div style="display:none">
                                                                            <dxe:ASPxLabel ID="lbl_RefType" runat="server" Text='<%# Eval("RefType")%>'></dxe:ASPxLabel>
                                                                        </div>
                                                                    </td>
                                                                    <td>Qty</td>
                                                                    <td>
                                                                       <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100"
                                                                            ID="spin_Qty" Text='<%# Bind("Qty")%>' InCrement="0">
                                                                           
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>PackageType</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType" runat="server"
                                                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType,2);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Dimension</td>
                                                                    <td colspan="3"><dxe:ASPxTextBox ID="txt_Dimension" runat="server" Text='<%#Bind("Dimension") %>' Width="100%">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Description</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox ID="memo_Description" runat="server" Text='<%# Bind("Description")%>' Width="100%" ></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="12">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                           <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                                <ClientSideEvents Click="function(s,e){gird_Packing.UpdateEdit();}" />
                                                                            </dxe:ASPxHyperLink>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>                                                   
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                    <dxtc:TabPage Text="Shipping Request">
                                        <ContentCollection>
                                            <dxw:ContentControl>
                                                <table>
                                                    <tr>
                                                        <td>
                                                <dxe:ASPxButton ID="btn_AddShipping" Width="150" runat="server" Text="Add Request" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_shipping.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
                                                        </td>
                                                        <td>
                                                <dxe:ASPxButton ID="ASPxButton2" Width="150" runat="server" Text="Copy From BL" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_shipping.GetValuesOnCustomCallback('Transfer',OnRequestCallback);
                                                        }" />
                                                </dxe:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <dxwgv:ASPxGridView ID="grid_shipping" runat="server" ClientInstanceName="grid_shipping" 
                                                    OnInit="grid_shipping_Init" OnInitNewRow="grid_shipping_InitNewRow" OnRowInserting="grid_shipping_RowInserting"  
                                                    OnRowUpdating="grid_shipping_RowUpdating" Width="100%" OnRowDeleting="grid_shipping_RowDeleting" KeyFieldName="Id" DataSourceID="dsPacking" OnBeforePerformDataSelect="grid_shipping_DataSelect" OnCustomDataCallback="grid_shipping_CustomDataCallback">
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                         <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" 
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_shipping.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_det_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE"%>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_shipping.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="1">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="2">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="3">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PackageType" FieldName="PackageType" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Dimension" FieldName="Dimension" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <table>
                                                                <tr>
                                                                    <td>Weight</td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="100" ID="spin_Weight"
                                                                            ClientInstanceName="spin_Weight" runat="server" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                            Value='<%# Bind("Weight")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Volume</td>
                                                                    <td><dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" Width="100" ID="spin_Volume"
                                                                            ClientInstanceName="spin_Volume" runat="server" DisplayFormatString="0.000" DecimalPlaces="3"
                                                                            Value='<%# Bind("Volume")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                        <div style="display:none">
                                                                            <dxe:ASPxLabel ID="lbl_RefType" runat="server" Text='<%# Eval("RefType")%>'></dxe:ASPxLabel>
                                                                        </div>
                                                                    </td>
                                                                    <td>Qty</td>
                                                                    <td>
                                                                       <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100"
                                                                            ID="spin_Qty" Text='<%# Bind("Qty")%>' InCrement="0">
                                                                           
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>PackageType</td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType" runat="server"
                                                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType,2);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Dimension</td>
                                                                    <td colspan="3"><dxe:ASPxTextBox ID="txt_Dimension" runat="server" Text='<%#Bind("Dimension") %>' Width="100%">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td>Description</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxTextBox ID="memo_Description" runat="server" Text='<%# Bind("Description")%>' Width="100%" ></dxe:ASPxTextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="12">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                           <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                                <ClientSideEvents Click="function(s,e){grid_shipping.UpdateEdit();}" />
                                                                            </dxe:ASPxHyperLink>
                                                                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditForm>
                                                    </Templates>                                                   
                                                </dxwgv:ASPxGridView>
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                <dxtc:TabPage Text="Certificate">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxe:ASPxButton ID="btn_AddCertificate" Width="150" runat="server" Text="Add Certificate" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE"%>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        AddExportCertificate(gird_Certificate)
                                                        }" />
                                            </dxe:ASPxButton>
                                            <dxwgv:ASPxGridView ID="gird_Certificate" runat="server" ClientInstanceName="gird_Certificate"
                                                OnInit="gird_Certificate_Init" OnInitNewRow="gird_Certificate_InitNewRow" OnRowInserting="gird_Certificate_RowInserting"
                                                OnRowUpdating="gird_Certificate_RowUpdating" Width="100%" OnRowDeleting="gird_Certificate_RowDeleting" KeyFieldName="Id" DataSourceID="dsCertificate" OnBeforePerformDataSelect="gird_Certificate_DataSelect">

                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="10%">
                                                        <DataItemTemplate>
                                                            <a onclick='ShowExportCertificate(gird_Certificate,"<%# Eval("Id") %>");'>Edit</a>&nbsp; 
                                                            <a onclick='PrintCertificate("<%# Eval("Id") %>")'>Print</a>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="GstPermitNo" FieldName="GstPermitNo" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="GstPaidAmt" FieldName="GstPaidAmt" VisibleIndex="3">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="HandingAgent" FieldName="HandingAgent" VisibleIndex="4">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Certificate Date" FieldName="CerDate" VisibleIndex="5">
                                                         <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                                                        </PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Templates>
                                                    <EditForm>
                                                        <table>
                                                            <tr>
                                                                <td>Description</td>
                                                                <td>
                                                                    <dxe:ASPxTextBox ID="ASPxTextBox2" runat="server" Text='<%# Bind("Description")%>' Width="300"></dxe:ASPxTextBox>
                                                                </td>
                                                                <td>Qty</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100"
                                                                        ID="ASPxSpinEdit1" Text='<%# Bind("Qty")%>' Increment="0">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>PackageType</td>
                                                                <td>
                                                                    <dxe:ASPxButtonEdit ID="ASPxButtonEdit1" ClientInstanceName="txt_pkgType" runat="server"
                                                                        Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                        <Buttons>
                                                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                        </Buttons>
                                                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType,2);
                                                                    }" />
                                                                    </dxe:ASPxButtonEdit>
                                                                </td>
                                                                <td>Value</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100"
                                                                        ID="spin_Amt" Text='<%# Bind("Amt")%>' Increment="0" DecimalPlaces="2">
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="12">
                                                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                        <dxe:ASPxHyperLink ID="ASPxHyperLink1" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                            <ClientSideEvents Click="function(s,e){gird_Certificate.UpdateEdit();}" />
                                                                        </dxe:ASPxHyperLink>
                                                                        <dxwgv:ASPxGridViewTemplateReplacement ID="ASPxGridViewTemplateReplacement1" ReplacementType="EditFormCancelButton"
                                                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EditForm>
                                                </Templates>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                    <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                        <ContentCollection>
                                            <dxw:ContentControl ID="ContentControl8" runat="server">
                                               <table>
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton4" Width="150" runat="server" Text="Upload Attachments"
                                                            Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE"%>' AutoPostBack="false"
                                                            UseSubmitBehavior="false">
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
                                                KeyFieldName="SequenceId" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                 AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                <Settings  />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE" %>'
                                                                ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                        <DataItemTemplate>
                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"))=="USE" %>'
                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                            </dxe:ASPxButton>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="60" Height="60" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                    </td>
                                                                </tr>
                                                            </table> 
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Des" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                  <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("SequenceId") %>'></dxe:ASPxTextBox>
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
                                            </dxw:ContentControl>
                                        </ContentCollection>
                                    </dxtc:TabPage>
                                </TabPages>
                                <TabStyle BackColor="#F0F0F0">
                            </TabStyle>
                            <ContentStyle BackColor="#F0F0F0">
                            </ContentStyle>
                                <ClientSideEvents ActiveTabChanged="function(s, e) { 
                                var tit=s.GetActiveTab().GetText();
                                if(tit=='Attachments1')
                                    detailGrid.PerformCallback('Photo');
                                    }" />
                            </dxtc:ASPxPageControl>
<table><tr><td>
                                                <dxe:ASPxButton ID="ASPxButton15" Width="150" runat="server" Text="Add Cargo" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                        grid_Mkgs.AddNewRow();
                                                        }" />
                                                </dxe:ASPxButton>
</td><td style="display:none">
                                                <dxe:ASPxButton ID="btn_transfer" Width="170" runat="server" Text="Transfer Fromg Booking" Enabled='<%# SafeValue.SafeInt(Eval("SequenceId"),0)>0&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e) {
                                                                         detailGrid.GetValuesOnCustomCallback('Transfer',OnTransferCallback);
                                                        }" />
                                                </dxe:ASPxButton>
</td></tr></table>
                                                <dxwgv:ASPxGridView ID="grid_Mkgs" ClientInstanceName="grid_Mkgs" runat="server"
                                                    DataSourceID="dsMarking" KeyFieldName="SequenceId" Width="100%" OnBeforePerformDataSelect="grid_Mkgs_DataSelect"
                                                    OnRowUpdating="grid_Mkgs_RowUpdating" OnInit="grid_Mkgs_Init" OnInitNewRow="grid_Mkgs_InitNewRow"
                                                    OnRowInserting="grid_Mkgs_RowInserting" OnRowDeleting="grid_Mkgs_RowDeleting" OnHtmlEditFormCreated="grid_Mkgs_HtmlEditFormCreated"
                                                    OnRowInserted="grid_Mkgs_RowInserted" OnRowUpdated="grid_Mkgs_RowUpdated" OnRowDeleted="grid_Mkgs_RowDeleted"
                                                    >
                                                    <SettingsBehavior ConfirmDelete="True" />
                                                    <SettingsEditing Mode="EditForm" />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                                ClientSideEvents-Click='<%# "function(s) { grid_Mkgs.StartEditRow("+Container.VisibleIndex+") }" %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                        <td>
                                                                            <dxe:ASPxButton ID="btn_mkg_del" runat="server" Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"))=="USE" %>'
                                                                                Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Mkgs.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                            </dxe:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="ContainerType" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="PACK TYPE" FieldName="PackageType" VisibleIndex="7">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="JobNo" VisibleIndex="7" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn FieldName="MkgType" VisibleIndex="8" Visible="false">
                                                        </dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="Weight" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                <dxwgv:ASPxSummaryItem FieldName="Volume" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                <dxwgv:ASPxSummaryItem FieldName="Qty" SummaryType="Sum" DisplayFormat="{0:0}" />
                            </TotalSummary>
                                                    <Templates>
                                                        <EditForm><div style="display:none">
                                                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_mkg_jobNo"
                                                                            Text='<%# Eval("JobNo") %>'>
                                                                        </dxe:ASPxTextBox></div>
                                                            <table>
                                                                <tr>
                                                                    <td>Cont No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDropDownEdit ID="DropDownEdit2" runat="server" ClientInstanceName="DropDownEdit"
                                                                            Text='<%# Bind("ContainerNo") %>' Width="120px" AllowUserInput="True">
                                                                            <DropDownWindowTemplate>
                                                                                <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                                                                    Width="300px" DataSourceID="dsRefCont" KeyFieldName="ContainerNo" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                                                    <Columns>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" VisibleIndex="1">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="SealNo" VisibleIndex="2">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                        <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="Type" VisibleIndex="2">
                                                                                        </dxwgv:GridViewDataTextColumn>
                                                                                    </Columns>
                                                                                    <ClientSideEvents RowClick="RowClickHandler" />
                                                                                </dxwgv:ASPxGridView>
                                                                            </DropDownWindowTemplate>
                                                                        </dxe:ASPxDropDownEdit>
                                                                    </td>
                                                                    <td>Seal No
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxTextBox runat="server" Width="120" ID="txt_sealN2"
                                                                            Text='<%# Bind("SealNo") %>' ClientInstanceName="txt_sealN">
                                                                        </dxe:ASPxTextBox>
                                                                    </td>
                                                                    <td Width="70">Cont Type
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_contType" ClientInstanceName="txt_contType" runat="server"
                                                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Value='<%# Bind("ContainerType") %>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_contType,1);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_wt2" Value='<%# Bind("Weight")%>' Increment="0"  DecimalPlaces="3">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Volume
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_m4" Value='<%# Bind("Volume")%>' Increment="0"  DecimalPlaces="3">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Qty
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="120"
                                                                            ID="spin_pkg2" Text='<%# Bind("Qty")%>' Increment="0">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td width="70">Pack Type
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButtonEdit ID="txt_pkgType2" ClientInstanceName="txt_pkgType2" runat="server"
                                                                            Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                            <Buttons>
                                                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                            </Buttons>
                                                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType2,2);
                                                                    }" />
                                                                        </dxe:ASPxButtonEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Gross Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_BlGrosswt" Value='<%# Bind("GrossWt")%>' InCrement="0" DecimalPlaces="3">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                    <td>Net Weight
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false"
                                                                            runat="server" Width="120" ID="spin_BlNetwt" Value='<%# Bind("NetWt")%>' InCrement="0"  DecimalPlaces="3">
                                                                        </dxe:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td width="85">Marking
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo runat="server" Rows="6" Width="320" ID="txt_mkg2" MaxLength="500" Text='<%# Bind("Marking")%>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>Description
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxMemo runat="server" Rows="6" Width="320" ID="txt_des2" MaxLength="500" Text='<%# Bind("Description")%>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="UpdateMkgs" runat="server" NavigateUrl="#" Text="Update"  Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>'>
                                                                                <ClientSideEvents Click="function(s,e){grid_Mkgs.UpdateEdit();}" />
                                                                            </dxe:ASPxHyperLink>
                                                                <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                    runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                            </div>
                                                        </EditForm>
                                                    </Templates>
                                                </dxwgv:ASPxGridView>
                            <hr />
                            Full House Job
                        <dxwgv:ASPxGridView ID="grid_Export1" runat="server"
                            KeyFieldName="SequenceId" Width="900px" AutoGenerateColumns="False" DataSourceID="dsFullJob"
                            OnBeforePerformDataSelect="grid_Export1_BeforePerformDataSelect">
                            <SettingsPager Mode="ShowAllRecords" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                    <DataItemTemplate>
                                        <a onclick="ShowHouse('<%# Eval("RefNo") %>','<%# Eval("JobNo") %>');">Edit</a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Export No" FieldName="JobNo" VisibleIndex="1" Width="150"
                                    SortIndex="1" SortOrder="Ascending">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Bkg Ref No" FieldName="BkgRefNo" VisibleIndex="3" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Hbl No" FieldName="HblNo" VisibleIndex="3" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="CustomerName" VisibleIndex="3">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" VisibleIndex="4" Width="40">
                                    <PropertiesTextEdit DisplayFormatString="0.000">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="5" UnboundType="Decimal" Width="40">
                                    <PropertiesTextEdit DisplayFormatString="0.000">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="6" Width="40">
                                    <PropertiesTextEdit DisplayFormatString="f0">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="User" FieldName="UpdateBy" VisibleIndex="40">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                        </div>
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
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
