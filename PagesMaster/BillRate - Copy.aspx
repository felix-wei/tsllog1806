<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="BillRate.aspx.cs" Inherits="Page_BillRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js">
    </script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript">
        function PopupCharges() {
                popubCtr.SetHeaderText('Rate List');
                popubCtr.SetContentUrl('/PagesContTrucking/SelectPage/BillingRateList.aspx?client=' + btn_ClientId.GetText());
                popubCtr.Show();
            
        }
        function AfterPopub() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobRate" KeyMember="Id" FilterExpression="LineType='RATE'" />

            <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="" />
            <wilson:DataSource ID="dsClient" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="" />
            <wilson:DataSource ID="dsParty" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="" />
            <wilson:DataSource ID="dsAgent" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="" />
            <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RateType" KeyMember="Id" />
            <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Container_Type" KeyMember="id" />
            <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefProductClass" KeyMember="Id" />
            <wilson:DataSource ID="dsXXUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Id" FilterExpression="CodeType='2'" />
            <h2>Master Billing Rate</h2>
            <hr>
            <table>
                <tr>
                    <td>Charges
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CostChgCode" ClientInstanceName="txt_CostChgCode" Width="100" runat="server">
                            <Buttons>
                                <dxe:EditButton Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_CostChgCode,null);
                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    
                    <td>Bill Type
                    </td>
                    <td>
                        <dxe:ASPxComboBox DataSourceID="dsRateType" ValueType="System.String" TextField="Code" ValueField="Code" EnableIncrementalFiltering="True" runat="server" Width="100" ID="cbx_Rate">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Job Type</td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_JobType" ClientInstanceName="cbb_JobType" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Text="IMP" Value="IMP"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="EXP" Value="EXP"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="LOC" Value="LOC"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="GR" Value="GR"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="DO" Value="DO"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="TP" Value="TP"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="CRA" Value="CRA"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Scope</td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_Scope" ClientInstanceName="cbb_Scope" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Text="Job" Value="JOB"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Cont" Value="CONT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Trip" Value="TRIP"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Bill Class</td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_BillClass" ClientInstanceName="cbb_BillClass" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Text="Trucking" Value="TRUCKING"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Services" Value="SERVICES"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Warehouse" Value="WAREHOUSE"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Transport" Value="TRANSPORT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Freight" Value="FREIGHT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Crane" Value="CRANE"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Other" Value="OTHER"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="6">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_export_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" OnClick="btn_add_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton2" Width="110" runat="server" Text="Copy Rate" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e){
                                if(btn_ClientId.GetText()!=''){
                                      PopupCharges();
                                }else{
                                   alert('Pls select a Client');
                                }   
                                 
                               }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <hr>
            <table>
                <tr>
                    <td colspan="6">
                        <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
                            DataSourceID="ds1" KeyFieldName="Id" OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting"
                            OnRowInserting="Grid1_RowInserting" OnInitNewRow="Grid1_InitNewRow"
                            OnInit="Grid1_Init" Width="100%" AutoGenerateColumns="False" OnPageSizeChanged="grid1_PageSizeChanged" OnPageIndexChanged="grid1_PageIndexChanged">
                            <SettingsEditing Mode="Inline" />
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager Mode="ShowPager" PageSize="20" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                                    <EditButton Visible="True" />
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="999" Width="5%">
                                    <DeleteButton Visible="true" />
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Update" FieldName="RowUpdateTime" VisibleIndex="998"
                                    Width="140">
                                    <DataItemTemplate>
                                        <%# Eval("RowUpdateUser","{0}") %> <%# Eval("RowUpdateTime","{0:dd/MM#HH:mm}") %>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="LineStatus" Caption="Optional" Width="50" VisibleIndex="98">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="Y" Value="Y" />
                                            <dxe:ListEditItem Text="N" Value="N" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ClientId" Caption="Client" Width="150" VisibleIndex="1">
                                    <DataItemTemplate>
                                        <%# SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select Name from xxparty where PartyId='{0}'",Eval("ClientId")))) %>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dxe:ASPxButtonEdit ID="btn_Line_ClientId" ClientInstanceName="btn_Line_ClientId" Text='<%# Bind("ClientId") %>' runat="server" Width="100" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_Line_ClientId,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1">
                                    <EditItemTemplate>
                                        <dxe:ASPxButtonEdit ID="txt_Line_ChgCode" ClientInstanceName="txt_Line_ChgCode" Width="100" runat="server" Text='<%# Bind("ChgCode") %>'>
                                            <Buttons>
                                                <dxe:EditButton Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_Line_ChgCode,null);
                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </EditItemTemplate>
                                    <DataItemTemplate>
                                        <%# SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", Eval("ChgCode")))) %>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                                    Width="150">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                    Width="150">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="SKU" FieldName="SkuCode" VisibleIndex="1"
                                    Width="150">
                                    <EditItemTemplate>
                                        <dxe:ASPxButtonEdit ID="btn_SkuCode" ClientInstanceName="btn_SkuCode" runat="server" Text='<%# Bind("SkuCode") %>' AutoPostBack="False" Width="100px">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupSku(btn_SkuCode);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="SkuClass" Caption="SKU Class" Width="150" VisibleIndex="1">
                                    <PropertiesComboBox DataSourceID="dsProductClass" ValueType="System.String" TextField="Code" ValueField="Code">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                 <dxwgv:GridViewDataComboBoxColumn FieldName="SkuUnit" Caption="SKU Unit" Width="150" VisibleIndex="1">
                                    <PropertiesComboBox DataSourceID="dsXXUom" ValueType="System.String" TextField="Code" ValueField="Code">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="ContSize" Caption="Cont Size" Width="50" VisibleIndex="1" Visible="false">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="20" Value="20" />
                                            <dxe:ListEditItem Text="40" Value="40" />
                                            <dxe:ListEditItem Text="45" Value="45" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="ContType" Caption="Cont Type" Width="150" VisibleIndex="1">
                                    <PropertiesComboBox DataSourceID="dsContType" ValueType="System.String" TextField="containerType" ValueField="containerType">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="BillType" Caption="Bill Type" Width="150" VisibleIndex="1" Visible="false">
                                    <PropertiesComboBox DataSourceID="dsRateType" ValueType="System.String" TextField="Code" ValueField="Code">
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="JobType" Caption="Job Type" Width="80" VisibleIndex="1">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="IMP" Value="IMP"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="EXP" Value="EXP"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="LOC" Value="LOC"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="WGR" Value="WGR"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="WDO" Value="WDO"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="TPT" Value="TPT"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="CRA" Value="CRA"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="BillScope" Caption="Scope" Width="80" VisibleIndex="6">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="Job" Value="JOB"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Cont" Value="CONT"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Trip" Value="TRIP"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Adhoc" Value="ADHOC"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                 <dxwgv:GridViewDataComboBoxColumn FieldName="BillClass" Caption="Bill Class" Width="80" VisibleIndex="6">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="Trucking" Value="TRUCKING"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Services" Value="SERVICES"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Warehouse" Value="WAREHOUSE"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Transport" Value="TRANSPORT"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Freight" Value="FREIGHT"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Crane" Value="CRANE"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Other" Value="OTHER"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Documents" Value="DOCUMENTS"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Incentive" Value="INCENTIVE"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Claims" Value="CLAIMS"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="PSA" Value="PSA"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                 <dxwgv:GridViewDataComboBoxColumn FieldName="StorageType" Caption="Storage Type" Width="60" VisibleIndex="6">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="Daily" Value="Daily"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Weekly" Value="Weekly"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Monthly" Value="Monthly"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Yearly" Value="Yearly"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataSpinEditColumn Caption="Daily No" FieldName="DailyNo" VisibleIndex="6"
                                    Width="50">
                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" NumberType="Integer" Increment="0" DecimalPlaces="0"></PropertiesSpinEdit>
                                </dxwgv:GridViewDataSpinEditColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="VehicleType" Caption="Vehicle Type" Width="80" VisibleIndex="6">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="50Ton" Value="50Ton"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="70Ton" Value="70Ton"></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty" VisibleIndex="5"
                                    Width="50"  >
                                    <DataItemTemplate>
                                        <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Qty")),1) %>
                                    </DataItemTemplate>
                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="1"></PropertiesSpinEdit>
                                </dxwgv:GridViewDataSpinEditColumn>
                                <dxwgv:GridViewDataSpinEditColumn Caption="Billing Rate" FieldName="Price" VisibleIndex="5"
                                    Width="50">
                                    <DataItemTemplate>
                                        <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Price")),2) %>
                                    </DataItemTemplate>
                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="2"></PropertiesSpinEdit>
                                </dxwgv:GridViewDataSpinEditColumn>
                                <dxwgv:GridViewDataSpinEditColumn Caption="Our Cost" FieldName="Cost" VisibleIndex="5"
                                    Width="50">
                                     <DataItemTemplate>
                                        <%# SafeValue.ChinaRound(SafeValue.SafeDecimal(Eval("Cost")),2) %>
                                    </DataItemTemplate>
                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="2"></PropertiesSpinEdit>
                                </dxwgv:GridViewDataSpinEditColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="GstType" Caption="Gst Type" Width="80" VisibleIndex="5">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="S" Value="S"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Z" Value="Z"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="T" Value="T"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="E" Value="E"></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Min-Qty" FieldName="MinQty" VisibleIndex="5"
                                    Width="50" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                 <dxwgv:GridViewDataTextColumn Caption="Min-Price" FieldName="MinPrice" VisibleIndex="5"
                                    Width="50" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Min-Amt" FieldName="MinAmt" VisibleIndex="5"
                                    Width="50" Visible="false">
                                </dxwgv:GridViewDataTextColumn>

                            </Columns>
                            <Settings ShowFooter="true" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                            </TotalSummary>

                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid1">
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="900" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
