<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectBillRate.aspx.cs" Inherits="PagesContTrucking_SelectPage_SelectBillRate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
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
              if (v == "Success"||v=="") {
                  parent.AfterPopub();
              }
              else if (v != null && v.length > 0) {
                  alert(v);
                  parent.AfterPopub();
              }
          }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />
    <script type="text/javascript">
        $.noConflict();
    </script>
    <style type="text/css">
        .show {
           display:table-cell;
        }
        .hide {
          display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td id="lbl2" runat="server">Charges Description </td>
                 <td id="cbb2" runat="server">
                     <dxe:ASPxTextBox ID="txt_Charge" runat="server" Width="150" />
                 </td>
                <td id="lbl" runat="server">Scope </td>
                 <td id="cbb" runat="server">
                     <dxe:ASPxComboBox ID="cbb_BillScope" runat="server" Width="100">
                         <Items>
                             <dxe:ListEditItem Text="Job" Value="JOB"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="Cont" Value="CONT"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="Trip" Value="TRIP"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="Adhoc" Value="ADHOC"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                         </Items>
                     </dxe:ASPxComboBox>
                 </td>
                <td style="padding-right:20px;">
                    <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" AutoPostBack="false" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
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
                <td>

                    <dxe:ASPxButton ID="btn_CreateInv" Width="60" runat="server" Text="Keyin for All Container"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid1.GetValuesOnCustomCallback('Cont',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton2" Width="60" runat="server" Text="Keyin for Job"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid1.GetValuesOnCustomCallback('Job',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                                 <td style="display:none">

                    <dxe:ASPxButton ID="ASPxButton3" Width="60" runat="server" Text="Keyin for All Trip"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid1.GetValuesOnCustomCallback('Trip',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobRate" KeyMember="Id" />

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
            OnInit="Grid1_Init" Width="100%" AutoGenerateColumns="False"  >
            <SettingsEditing Mode="Inline" />
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsPager Mode="ShowAllRecords" />
            <Columns>
                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                    <EditButton Visible="True" />
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
                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="LineIndex" VisibleIndex="1" SortOrder="Ascending" SortIndex="0"
                    Width="40">
                    </dxwgv:GridViewDataTextColumn>
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
                <dxwgv:GridViewDataTextColumn FieldName="ClientId" Caption="Client" Width="150" VisibleIndex="1" SortOrder="Descending" SortIndex="0" >
                    <DataItemTemplate>
                        <%# SafeValue.SafeString(ConnectSql.ExecuteScalar(string.Format(@"select Name from xxparty where PartyId='{0}'",Eval("ClientId")))) %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="txt_Client" ClientInstanceName="txt_Client" Width="150" DataSourceID="dsClient" TextField="Name" runat="server" ValueField="PartyId" ValueType="System.String" Value='<%# Bind("ClientId") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1" Settings-FilterMode="Value">
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="txt_ChgCode" ClientInstanceName="txt_ChgCode" Width="100" DataSourceID="dsRate" TextField="ChgCodeDe" runat="server" ValueField="ChgCodeId" ValueType="System.String" Value='<%# Bind("ChgCode") %>' DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith">
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                    <DataItemTemplate>
                        <%# SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", Eval("ChgCode")))) %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                    Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                    Width="100">
                </dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataSpinEditColumn Caption="Qty" FieldName="Qty" VisibleIndex="5"
                    Width="50">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="3"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Billing Rate" FieldName="Price" VisibleIndex="5"
                    Width="50">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="3"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="GST" FieldName="GstType" VisibleIndex="5"
                    Width="50" />
                <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="5"
                    Width="50">
                    <Settings AllowAutoFilterTextInputTimer="Default" />
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Our Cost" FieldName="Cost" VisibleIndex="5"
                    Width="50">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" DecimalPlaces="3"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BillType" Caption="Bill Type" Width="150" VisibleIndex="6">
                    <PropertiesComboBox DataSourceID="dsRateType" ValueType="System.String" TextField="Code" ValueField="Code">
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="BillScope" Caption="Scope" Width="80" VisibleIndex="6" >
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
                            <dxe:ListEditItem Text="Psa" Value="PSA"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="ContSize" Caption="Cont Size" Width="50" VisibleIndex="6" >
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Text="20" Value="20" />
                            <dxe:ListEditItem Text="40" Value="40" />
                            <dxe:ListEditItem Text="45" Value="45" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="ContType" Caption="Cont Type" Width="150" VisibleIndex="6">
                    <PropertiesComboBox DataSourceID="dsContType" ValueType="System.String" TextField="containerType" ValueField="containerType">
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
    </div>
    </form>
</body>
</html>
