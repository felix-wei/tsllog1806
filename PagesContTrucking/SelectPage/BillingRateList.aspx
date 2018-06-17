<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillingRateList.aspx.cs" Inherits="PagesContTrucking_SelectPage_BillingRateList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js">
    </script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopub(); }
        }
        document.onkeydown = keydown;

        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function OnCallback(v) {
            if (v == "Success") {
                alert("Action Success!");
                parent.AfterPopub();
            }
            else if (v != null && v.length > 0) {
                alert(v);
            }
        }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td style="display:none">
                    <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" OnClick="btn_add_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                </td>
                <td>Client</td>
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
                <td>
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="300" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton10" Width="150" runat="server" Text="Copy Billing Rate To"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                if(btn_ClientId.GetText()!=''){
                                    grid1.GetValuesOnCustomCallback('OK',OnCallback);  
                                }else{
                                   alert('Pls select a new Client');
                                }            
                                                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobRate" KeyMember="Id" FilterExpression="1=0" />

        <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsClient" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsParty" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsAgent" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.RateType" KeyMember="Id" />
        <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="id" />
    <div>
        <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
            DataSourceID="ds1" KeyFieldName="Id" OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting"
            OnRowInserting="Grid1_RowInserting" OnInitNewRow="Grid1_InitNewRow" OnCustomDataCallback="grid1_CustomDataCallback"
            OnInit="Grid1_Init" Width="100%" AutoGenerateColumns="False">
            <SettingsEditing Mode="Inline" />
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsPager Mode="ShowAllRecords" />
            <Columns>
                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                    <EditButton Visible="false" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="999" Width="5%">
                    <DeleteButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
                 <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="1"
                    Width="40">
                    <DataItemTemplate>
                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                        </dxe:ASPxCheckBox>
                        <div style="display:none">
                            <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                    Text='<%# Eval("Id") %>' Width="150">
                                </dxe:ASPxTextBox>
                        </div>
                    </DataItemTemplate>
                    <EditItemTemplate>

                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                
                <dxwgv:GridViewDataTextColumn Caption="Update" FieldName="RowUpdateTime" VisibleIndex="998"
                    Width="140">
                    <DataItemTemplate>
                        <%# Eval("RowUpdateUser","{0}") %> <%# Eval("RowUpdateTime","{0:dd/MM#HH:mm}") %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
               
                <dxwgv:GridViewDataTextColumn FieldName="ClientId" Caption="Client" Width="150" VisibleIndex="1">
                    <DataItemTemplate>
                        <%# SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select Name from xxparty where PartyId='{0}'",Eval("ClientId")))) %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="txt_Client" ClientInstanceName="txt_Client" Width="150" DataSourceID="dsClient" TextField="Name" runat="server" ValueField="PartyId" ValueType="System.String" Value='<%# Bind("ClientId") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="txt_ChgCode" ClientInstanceName="txt_ChgCode" Width="100" DataSourceID="dsRate" TextField="ChgCodeDe" runat="server" ValueField="ChgCodeId" ValueType="System.String" Value='<%# Bind("ChgCode") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
 
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                    Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                    Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="ContSize" Caption="Cont Size" Width="50" VisibleIndex="1">
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
                <dxwgv:GridViewDataComboBoxColumn FieldName="BillType" Caption="Bill Type" Width="150" VisibleIndex="1">
                    <PropertiesComboBox DataSourceID="dsRateType" ValueType="System.String" TextField="Code" ValueField="Code">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BillScope" Caption="Scope" Width="80" VisibleIndex="1">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Text="Job" Value="JOB"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="Cont" Value="CONT"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="Adhoc" Value="ADHOC"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BillClass" Caption="Bill Class" Width="80" VisibleIndex="1">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Text="TRUCKING" Value="TRUCKING"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="SERVICES" Value="SERVICES"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="WHS" Value="WHS"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="VOL" Value="VOL"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="ZFREIGHTS" Value="ZFREIGHTS"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="LCL" Value="LCL"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Billing Rate" FieldName="Price" VisibleIndex="5"
                    Width="90">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="3"></PropertiesSpinEdit>
					<DataItemTemplate>
                         
                      
                            <dxe:ASPxSpinEdit ID="txt_Amt"   runat="server"
                                    Text='<%# Eval("Price") %>' Width="90">
                                </dxe:ASPxSpinEdit>
                         
                    </DataItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="GST" FieldName="GstType" VisibleIndex="5"
                    Width="150">
                </dxwgv:GridViewDataTextColumn>
                
            </Columns>
            <Settings ShowFooter="true" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
            </TotalSummary>

        </dxwgv:ASPxGridView>
         <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="600" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
