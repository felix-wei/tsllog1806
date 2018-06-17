﻿<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ExportCertifiCate.aspx.cs" Inherits="PagesFreight_Account_ExportCertifiCate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
 <title>Certificate</title>
     <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/sea/Export_Doc.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr1(); }
        }
        document.onkeydown = keydown;

    </script>
</head>
<body>
    <wilson:DataSource ID="dsCertificateDet" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.SeaCertificateDet" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsCertificate" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.SeaCertificate" KeyMember="Id" FilterExpression="1=0" />
    <wilson:DataSource ID="dsAgentMast" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsAgent='true'" />
    <form id="form1" runat="server">
        <div>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server"
                DataSourceID="dsCertificate" Width="100%" KeyFieldName="Id" OnInit="ASPxGridView1_Init"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback"
                AutoGenerateColumns="False">
                <SettingsPager PageSize="50">
                </SettingsPager>
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <SettingsCustomizationWindow Enabled="True" />
                <Templates>
                    <EditForm>
                        <table border="0">
                            <tr>
                                <td colspan="4" style="display: none">
                                    <dxe:ASPxTextBox runat="server" ID="txt_Id" ClientInstanceName="txt_Id" ReadOnly="true" BackColor="Control"
                                        Width="100" Text='<%# Eval("Id")%>'>
                                    </dxe:ASPxTextBox>                              

                                </td>
                            </tr>
                            <tr>
                                <td>Date
                                </td>
                                <td width="170">
                                    <dxe:ASPxDateEdit ID="txt_CerDt" runat="server" Width="100%" Value='<%# Eval("CerDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>Export Permit No
                                </td>
                                <td width="170">
                                    <dxe:ASPxTextBox runat="server" ID="txt_GstPermitNo" ClientInstanceName="txt_GstPermitNo"
                                        Width="100%" Text='<%# Eval("GstPermitNo")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Consignee
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                                        Value='<%# Eval("HandingAgent") %>' Width="100%" DropDownWidth="380" DropDownStyle="DropDownList"
                                        DataSourceID="dsAgentMast" ValueField="PartyId" ValueType="System.String" TextFormatString="{1}"
                                        EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                        CallbackPageSize="100">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                        </Columns>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            
                         <tr>
                                <td colspan="4" style="background-color: Gray; color: White;display:none ">
                                    <b>Certificate Det</b>
                                </td>
                            </tr>
                            <tr style="display:none">
                                <td>Ref No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_MastRefNo" runat="server" Text='<%# Eval("RefNo") %>' Width="130" ClientInstanceName="txt_MastRefNo">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Job No </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_JobRefNo" runat="server" Text='<%# Eval("JobNo") %>' Width="155">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>
                                    Job Type </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_MastType" runat="server" BackColor="Control" ClientInstanceName="txt_MastType" ReadOnly="true" Text='<%# Eval("RefType")%>' Width="100">
                                    </dxe:ASPxTextBox>
                                    <dxe:ASPxTextBox ID="txt_SerialNo" runat="server" Text='<%# Eval("SerialNo")%>' Width="120"></dxe:ASPxTextBox>

                                    <dxe:ASPxSpinEdit Increment="0" ID="spin_GstPaidAmt" Width="100" ClientInstanceName="spin_GstPaidAmt"
                                        DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Eval("GstPaidAmt") %>'>
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>  
                                    <dxe:ASPxButtonEdit ID="btn_RefCurrency" ClientInstanceName="btn_RefCurrency" runat="server"
                                                                Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("Currency")%>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(btn_RefCurrency,txt_RefExRate);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                    <dxe:ASPxSpinEdit Increment="0" ID="txt_RefExRate" Width="130" ClientInstanceName="txt_RefExRate"
                                        DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Eval("ExRate") %>'>
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <dxe:ASPxButton ID="btn_Save" runat="server" Text="Update" AutoPostBack="false" UseSubmitBehavior="false"
                                        Enabled='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE"  %>'>
                                        <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Line" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE"  %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                grid_CerDet.AddNewRow();
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Print" runat="server" Text="Print" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>'
                                        AutoPostBack="false" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e){
                                PrintCertificate(txt_Id.GetText());
                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <table width="800">
                            <tr>
                                <td colspan="6">
                                    <dxwgv:ASPxGridView ID="grid_CerDet" ClientInstanceName="grid_CerDet" runat="server"
                                        DataSourceID="dsCertificateDet" KeyFieldName="Id" OnBeforePerformDataSelect="grid_CerDet_BeforePerformDataSelect"
                                        OnRowUpdating="grid_CerDet_RowUpdating" OnRowInserting="grid_CerDet_RowInserting"
                                        OnInitNewRow="grid_CerDet_InitNewRow" OnInit="grid_CerDet_Init" OnRowDeleting="grid_CerDet_RowDeleting"
                                         Width="100%" AutoGenerateColumns="False">
                                        <SettingsEditing Mode="EditForm" />
                                        <SettingsPager Mode="ShowAllRecords" />
                                        <Columns>
                                            <dxwgv:GridViewDataColumn Caption="#">
                                                <DataItemTemplate>
                                                        <a href="#" onclick='<%# "grid_CerDet.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                        <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_CerDet.DeleteRow("+Container.VisibleIndex+");"  %>}'>Del</a>
                                                </DataItemTemplate>
                                            </dxwgv:GridViewDataColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="3">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="PackageType" FieldName="PackageType" VisibleIndex="4">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="Amt" VisibleIndex="6">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="5">
                                            </dxwgv:GridViewDataTextColumn>
                                            <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="5">
                                            </dxwgv:GridViewDataTextColumn>
                                        </Columns>  
                                        <Templates>
                                            <EditForm>
                                                <table>
                                                    <tr>
                                                        <td>Qty</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="50"
                                                                ID="spin_Qty" Text='<%# Bind("Qty")%>' Increment="0">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>PackageType</td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="txt_pkgType" ClientInstanceName="txt_pkgType" runat="server"
                                                                Width="100" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("PackageType")%>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupUom(txt_pkgType,2);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Currency
                                                        </td>
                                                        <td>
                                                              <dxe:ASPxButtonEdit ID="btn_Currency" ClientInstanceName="btn_Currency" runat="server" MaxLength="3"
                                                                Width="60" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Bind("Currency")%>'>
                                                                <Buttons>
                                                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                                                </Buttons>
                                                                <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(btn_Currency,txt_ExRate);
                                                                    }" />
                                                            </dxe:ASPxButtonEdit>
                                                        </td>
                                                        <td>Ex Rate
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit Increment="0" ID="txt_ExRate" Width="80" ClientInstanceName="txt_ExRate"
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Bind("ExRate") %>'>
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>Amt</td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                                ID="spin_Amt" Text='<%# Bind("Amt")%>' Increment="0" DecimalPlaces="2">
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Invoice No
                                                        </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memo_Marking" runat="server" Text='<%# Bind("Marking") %>' Width="100%" Rows="3">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                        <td>Description</td>
                                                        <td colspan="5">
                                                            <dxe:ASPxMemo ID="memo_Description" runat="server" Text='<%# Bind("Description")%>' Width="100%"  Rows="3"></dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="12">
                                                            <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE"&&SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                                                    <ClientSideEvents Click="function(s,e){grid_CerDet.UpdateEdit();}" />
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
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="800" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
