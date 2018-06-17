<%@ Page Language="C#"  EnableViewState="false" AutoEventWireup="true" CodeFile="SellingLCEdit.aspx.cs" Inherits="PagesMaster_SellingLCEdit" %>

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
        var isUpload = false;
    </script>
    <script type="text/javascript">
        function OnSaveCallBack(v) {
            if (v == "Success") {
                alert("Action Success");
                grid.Refresh();
            } if (v != null && v.indexOf("Fail") > -1) {
                alert(v);
            } else if (v != null && v != "Success") {
                window.location = 'SellingLCEdit.aspx?no=' + v;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsLc" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RefLc" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsAttachment" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.WhAttachment" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsLog" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.LogEvent"
                KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsLcActivity" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.LcActivity" KeyMember="Id" FilterExpression="1=0" />
         <table>
                <tr>
                    <td>Lc No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SchRefNo" Width="150" ClientInstanceName="txt_SchRefNo" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SellingLCEdit.aspx?no='+txt_SchRefNo.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New LC" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='SellingLCEdit.aspx?no=0';
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Go Search" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           window.location='SellingLCList.aspx';
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
    <div>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsLc"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init"
            OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating"
            OnCustomDataCallback="grid_CustomDataCallback" OnRowDeleting="grid_RowDeleting">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <Settings ShowColumnHeaders="false" />
            <Templates>
                <EditForm>
                    <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                        <tr>
                            <td width="90%"></td>
                            <td>
                                 <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save" >
                                            <ClientSideEvents Click="function(s,e) {
                                                    grid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                                 }" />
                                        </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>

                    <div style="display: none">
                        <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                    </div>
                    <table style="width:90%">
                        <tr>
                            <td></td>
                            <td style="width: 120px;"></td>
                            <td></td>
                            <td style="width: 120px;"></td>
                            <td></td>
                            <td style="width: 120px;"></td>
                            <td></td>
                            <td style="width: 120px;"></td>
                        </tr>
                        <tr>
                            <td >No</td>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="100%" ReadOnly="True" BackColor="Control" ID="txt_LcNo" Text='<%# Eval("LcNo") %>' ClientInstanceName="txt_LcNo">
                                </dxe:ASPxTextBox>
                            </td>
                            <td>Type</td>
                            <td>
                                <dxe:ASPxComboBox ID="cmb_Type" Width="100%" runat="server" Value='<%# Eval("LcType") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                                    <Items>
                                        <dxe:ListEditItem Text="IMPORT LC" Value="IMPORT LC" />
                                        <dxe:ListEditItem Text="EXPORT LC" Value="EXPORT LC" />
                                        <dxe:ListEditItem Text="STANDBY LC" Value="STANDBY LC" />
                                        <dxe:ListEditItem Text="BANK GUARANTEE" Value="BANK GUARANTEE" />
                                        <dxe:ListEditItem Text="SHIPPING GUARANTEE" Value="SHIPPING GUARANTEE" />
                                        <dxe:ListEditItem Text="OTHERS" Value="OTHERS" />
                                    </Items>
                                </dxe:ASPxComboBox>   
                            </td>
                            <td>TempNo</td>
                            <td><dxe:ASPxTextBox runat="server" Width="150" ID="txt_TempNo" Text='<%# Eval("LcTempNo") %>' ClientInstanceName="txt_TempNo">
                                </dxe:ASPxTextBox></td>
                            <td>Ref No</td>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="150" ID="txt_LcRefNo" Text='<%# Eval("LcRef") %>' ClientInstanceName="txt_LcRefNo">
                                </dxe:ASPxTextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>Entity Code</td>
                            <td colspan="3">
                                <table cellpadding="0" cellspacing="0">
                                    <td width="94">
                                        <dxe:ASPxButtonEdit ID="txt_EntityCode" ClientInstanceName="txt_EntityCode" runat="server"
                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("LcEntityCode") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(txt_EntityCode,txt_EntityName,null,null,null,null,null,memo_EntityAddress,'C','PARTNER');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="270" ReadOnly="true" Text='<%# Eval("LcEntityName") %>' BackColor="Control" ID="txt_EntityName" ClientInstanceName="txt_EntityName">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </table>
                            </td>
                            <td>App Date</td>
                            <td>
                                <dxe:ASPxDateEdit ID="date_AppDate" Width="150" runat="server" Value='<%# Eval("LcAppDate") %>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                             <td>Expirt Date</td>
                            <td>
                                <dxe:ASPxDateEdit ID="date_LcExpirtDate" Width="150" runat="server" Value='<%# Eval("LcExpirtDate") %>'
                                    EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">Address</td>
                            <td rowspan="2" colspan="3">
                                <dxe:ASPxMemo ID="memo_EntityAddress" Rows="4" Width="100%" ClientInstanceName="memo_EntityAddress"
                                    runat="server" Text='<%# Eval("LcEntityAddress") %>'>
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                             <td>Expirt Place
                            </td>
                            <td >
                                <dxe:ASPxTextBox ID="txt_LcExpirtPlace" runat="server" Value='<%# Eval("LcExpirtPlace") %>' Width="150">
                                </dxe:ASPxTextBox>
                            </td>
                                <td>Mode</td>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="150" Text='<%# Eval("LcMode") %>' ID="txt_LcMode" ClientInstanceName="txt_LcMode">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Bene Code
                            </td>
                             <td colspan="3">
                                <table cellpadding="0" cellspacing="0">
                                    <td width="94">
                                        <dxe:ASPxButtonEdit ID="btn_BeneCode" ClientInstanceName="btn_BeneCode" runat="server"
                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("LcBeneCode") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                            PopupParty(btn_BeneCode,txt_LcBeneName,null,null,null,null,null,memo_BeneAddress,'C','PARTNER');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="270" ReadOnly="true" Text='<%# Eval("LcBeneName") %>' BackColor="Control" ID="txt_LcBeneName" ClientInstanceName="txt_LcBeneName">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </table>
                            </td>
                            <td>Currency
                            </td>
                            <td>
                                <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" MaxLength="3" Text='<%# Eval("LcCurrency") %>' runat="server" Width="150" AutoPostBack="False">
                                    <Buttons>
                                        <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,spin_ExRate);
                                                                    }" />
                                </dxe:ASPxButtonEdit>
                            </td>
                            <td>ExRate
                            </td>
                            <td>
                                <dxe:ASPxSpinEdit ID="spin_ExRate" ClientInstanceName="spin_ExRate" DisplayFormatString="0.000000"
                                    runat="server" Width="150" Value='<%# Eval("LcExRate")%>' DecimalPlaces="6" Increment="0">
                                    <SpinButtons ShowIncrementButtons="false" />
                                </dxe:ASPxSpinEdit>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">Address</td>
                            <td rowspan="2" colspan="3">
                                <dxe:ASPxMemo ID="memo_BeneAddress" Rows="4" Width="100%" ClientInstanceName="memo_BeneAddress"
                                    runat="server" Text='<%# Eval("LcBeneAddress") %>'>
                                </dxe:ASPxMemo>
                            </td>
                           
                        </tr>
                        <tr>
                          <td>Credit Limit</td>
                           <td>
                               <dxe:ASPxSpinEdit DisplayFormatString="0" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_LcAmount"
                                    runat="server" Width="150" ID="spin_LcAmount" Value='<%# Eval("LcAmount")%>' Increment="0" DecimalPlaces="0">
                                </dxe:ASPxSpinEdit>
                           </td>
                            <td>Branch</td>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="150" Text='<%# Eval("BankBranch") %>' ID="txt_Branch" ClientInstanceName="txt_Branch">
                                </dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td >Bank Code
                            </td>
                            <td colspan="3">
                                <table cellpadding="0" cellspacing="0">
                                    <td width="94">
                                        <dxe:ASPxButtonEdit ID="btn_BankCode" ClientInstanceName="btn_BankCode" runat="server"
                                            Width="90" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("BankCode") %>'>
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                             PopupParty(btn_BankCode,txt_BankName,null,null,null,null,null,memo_BankAddress,'C','BANK');
                                                                    }" />
                                        </dxe:ASPxButtonEdit>
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox runat="server" Width="270" ReadOnly="true" Text='<%# Eval("BankName") %>' BackColor="Control" ID="txt_BankName" ClientInstanceName="txt_BankName">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </table>
                            </td>
                            <td>Bank Ref</td>
                            <td>
                                <dxe:ASPxTextBox runat="server" Width="150" Text='<%# Eval("BankRef") %>' ID="txt_BankRef" ClientInstanceName="txt_Branch">
                                </dxe:ASPxTextBox>
                            </td>
                              <td>Bank Tenor</td>
                            <td>
                                <dxe:ASPxSpinEdit DisplayFormatString="0" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_BankTenor"
                                    runat="server" Width="150" ID="spin_BankTenor" Value='<%# Eval("BankTenor")%>' Increment="0" DecimalPlaces="2">
                                </dxe:ASPxSpinEdit>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">Address</td>
                            <td rowspan="2" colspan="3">
                                <dxe:ASPxMemo ID="memo_BankAddress" Rows="4" Width="100%" ClientInstanceName="memo_BankAddress"
                                    runat="server" Text='<%# Eval("BankAddress") %>'>
                                </dxe:ASPxMemo>
                            </td>
                           <td>Status</td>
                            <td>
                                <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100%" ID="cmb_Status" ReadOnly="true" BackColor="Control"
                                    runat="server" OnCustomJSProperties="cmb_Status_CustomJSProperties" Text='<%# Eval("StatusCode") %>'>
                                    <Items>
                                        <dxe:ListEditItem Text="Buy" Value="Buy" />
                                        <dxe:ListEditItem Text="Sell" Value="Sell" />

                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <hr>
                                <table>
                                    <tr>
                                        <td style="width: 80px;">Creation</td>
                                        <td style="width: 200px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateTimeStr( Eval("CreateDateTime"))%></td>
                                        <td style="width: 100px;">Last Updated</td>
                                        <td style="width: 200px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateTimeStr(Eval("UpdateDateTime"))%></td>
                                        <td style="width: 160px; display: none;">
                                                </td>
                                    </tr>
                                </table>
                                <hr>
                            </td>
                        </tr>
                    </table>
                    <div style="padding: 2px 2px 2px 2px">
                        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="1000px" Height="500px">
                            <TabPages>
                                <dxtc:TabPage Text="Activity" Visible="false">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxwgv:ASPxGridView ID="grid_Activity" ClientInstanceName="grid_Activity" runat="server"
                                               KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Activity_BeforePerformDataSelect">
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                <Columns>
                                                   <dxwgv:GridViewDataTextColumn Caption="OrderNo" FieldName="DoNo" VisibleIndex="0">
                                                            <DataItemTemplate>
                                                                <a href='javascript: parent.navTab.openTab("<%# Eval("DoNo") %>","/WareHouse/Job/PoEdit.aspx?no=<%# Eval("DoNo") %>",{title:"<%# Eval("DoNo") %>", fresh:false, external:true});'><%# Eval("DoNo") %></a>
                                                            </DataItemTemplate>
                                                   </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="DoDate" VisibleIndex="2" Width="60">
                                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Supplier" FieldName="PartyName" VisibleIndex="3" Width="300">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Pay Term" FieldName="PayTerm" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="INCO Term" FieldName="IncoTerm" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Total Qty" FieldName="TotalQty" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Total Amt" FieldName="TotalAmt" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DoStatus" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <Settings  ShowFooter="true"/>
                                                <TotalSummary >
                                                    <dxwgv:ASPxSummaryItem  FieldName="TotalAmt" SummaryType="Sum" DisplayFormat="{0:00.00}"/>
                                                </TotalSummary>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                  <dxtc:TabPage Text="Sub SO">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <dxwgv:ASPxGridView ID="grid_So" ClientInstanceName="grid_So" runat="server"
                                                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_So_BeforePerformDataSelect">
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Order No" FieldName="DoNo" VisibleIndex="1" Width="100">
                                                        <DataItemTemplate>
                                                            <a href='javascript: parent.navTab.openTab("<%# Eval("DoNo") %>","/WareHouse/Job/SoEdit.aspx?no=<%# Eval("DoNo") %>",{title:"<%# Eval("DoNo") %>", fresh:false, external:true});'><%# Eval("DoNo") %></a>
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="DoDate" VisibleIndex="2" Width="60">
                                                        <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Supplier" FieldName="PartyName" VisibleIndex="3" Width="300">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Pay Term" FieldName="PayTerm" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="INCO Term" FieldName="IncoTerm" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="WareHouse" FieldName="WareHouseId" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Total Qty" FieldName="TotalQty" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Total Amt" FieldName="TotalAmt" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="DoStatus" VisibleIndex="4" Width="40">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>

                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Log" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl ID="ContentControl6" runat="server">
                                            <dxwgv:ASPxGridView ID="grid_Log" ClientInstanceName="grid_Log" runat="server"
                                                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" DataSourceID="dsLog" OnBeforePerformDataSelect="grid_Log_BeforePerformDataSelect">
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Action" VisibleIndex="2" Width="100">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="User" FieldName="CreateBy" VisibleIndex="3" Width="80">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Time" FieldName="CreateDateTime" VisibleIndex="4" Width="70">
                                                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy HH:mm}" />
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Note" Visible="true">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table width="450">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="ASPxButton5" Width="100" runat="server" Text="Add New" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'>
                                                            <ClientSideEvents Click="function(s,e){
                                grid_Note.AddNewRow();
                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <dxwgv:ASPxGridView ID="grid_Note" ClientInstanceName="grid_Note" runat="server" DataSourceID="dsLcActivity" OnRowInserting="grid_Note_RowInserting"
                                                    KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grid_Note_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grid_Note_RowDeleting" OnInit="grid_Note_Init" OnInitNewRow="grid_Note_InitNewRow" OnRowUpdating="grid_Note_RowUpdating">
                                                    <SettingsEditing  Mode="EditForm"/>
                                                   <SettingsPager  Mode="ShowPager"></SettingsPager>
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                    ClientSideEvents-Click='<%# "function(s) { grid_Note.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                    Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_Note.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="CreateDateTime" VisibleIndex="2" Width="100">
                                                            <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesTextEdit>
                                                        </dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Type" FieldName="RefType" Width="80px"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="ActionNote" FieldName="ActionNote" Width="200"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="InfoNote" FieldName="InfoNote" Width="200"></dxwgv:GridViewDataTextColumn>
                                                        <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="Status" Width="60"></dxwgv:GridViewDataTextColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <EditForm>
                                                            <div style="display: none">
                                                                <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                            </div>
                                                            <table width="980">
                                                                <tr>
                                                                    <td>Date
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxDateEdit ID="date_CreateDateTime" Width="180" runat="server" Value='<%# Bind("CreateDateTime") %>'
                                                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm:ss" DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                                                        </dxe:ASPxDateEdit>
                                                                    </td>
                                                                    <td>Type</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Type"
                                                                            runat="server" Text='<%# Bind("RefType") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Meeting" Value="Meeting" />
                                                                                <dxe:ListEditItem Text="Followup" Value="Followup" />
                                                                                <dxe:ListEditItem Text="Note" Value="Note" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                    <td>Status</td>
                                                                    <td>
                                                                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" Width="100" ID="cmb_Status"
                                                                            runat="server" Text='<%# Bind("Status") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="Pending" Value="Pending" />
                                                                                <dxe:ListEditItem Text="Closed" Value="Closed" />
                                                                                <dxe:ListEditItem Text="Cancel" Value="Cancel" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>ActionNote</td>
                                                                    <td colspan="3">
                                                                        <dxe:ASPxMemo ID="memo_ActionNote" Rows="5" Width="400" ClientInstanceName="memo_ActionNote"
                                                                            runat="server" Text='<%# Bind("ActionNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                    <td>InfoNote</td>
                                                                    <td colspan="2">
                                                                        <dxe:ASPxMemo ID="memo_InfoNote" Rows="5" Width="350" ClientInstanceName="memo_InfoNote"
                                                                            runat="server" Text='<%# Bind("InfoNote") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="6">
                                                                        <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update">
                                                                                <ClientSideEvents Click="function(s,e){grid_Note.UpdateEdit();}" />
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
                                 <dxtc:TabPage Text="Attachments">
                                    <ContentCollection>
                                        <dxw:ContentControl>
                                            <table>
                                                    <tr>
                                                        <td>
                                                            <td>
                                                                <dxe:ASPxButton ID="ASPxButton13" Width="100%" Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' runat="server" Text="Upload Attachments"
                                                                    AutoPostBack="false"
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
                                                <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsAttachment"
                                                    KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                                    AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                                    <Settings />
                                                    <Columns>
                                                        <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                    ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                            <DataItemTemplate>
                                                                <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                                    Text="Delete" Width="40" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("StatusCode"),"USE")=="USE" %>' ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                </dxe:ASPxButton>
                                                            </DataItemTemplate>
                                                        </dxwgv:GridViewDataColumn>
                                                        <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                            <DataItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <a href='<%# Eval("Path")%>' target="_blank">
                                                                                <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                                </dxe:ASPxImage>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </table>
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
                                                                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'>
                                                                                <ClientSideEvents Click="function(s,e){grd_Photo.UpdateEdit();}" />
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
                              
                            </TabPages>
                        </dxtc:ASPxPageControl>
                    </div>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>
                    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grd_Photo.Refresh();
                }" />
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(grid!=null)
	                    grid.Refresh();
	                    grid=null;
                        detailGrid.Refresh();
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
