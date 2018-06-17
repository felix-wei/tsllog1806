<%@ Page Title="" Language="C#" AutoEventWireup="true"
    CodeFile="CurrencyRate.aspx.cs" Inherits="MastData_CurrencyRate" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CurrencyRate</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function g_clear(g) { g.ClearFilter(); }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Save" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="dsCurrencyRate" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CurrencyRate" KeyMember="Id"/>
        <wilson:DataSource ID="dsCurrency" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCurrency" KeyMember="CurrencyId"/>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsCurrencyRate" Width="800"
            KeyFieldName="Id" OnRowUpdating="ASPxGridView1_RowUpdating"
             oninit="ASPxGridView1_Init"  OnRowDeleting="ASPxGridView1_RowDeleting"
             oninitnewrow="ASPxGridView1_InitNewRow" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline"/>
            <Settings ShowFilterRow="true" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="90">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="true" />
                                            <HeaderTemplate>
                                                <a href="javascript:g_clear(grid)">Clear</a>
                                            </HeaderTemplate>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="FromCurrency" FieldName="FromCurrencyId" VisibleIndex="1" Width="70">
                    <PropertiesComboBox DataSourceID="dsCurrency" TextField="CurrencyId" Width="100" IncrementalFilteringMode="StartsWith" ValueField="CurrencyId"></PropertiesComboBox>
                    <EditItemTemplate>
                        <dxe:ASPxComboBox ID="cmb_FromCurrency" ClientInstanceName="cmb_FromCurrency" runat="server" OnCustomJSProperties="cmb_FromCurrency_CustomJSProperties"
                            Value='<%# Bind("FromCurrencyId") %>' Width="100" DropDownStyle="DropDownList" ValidationSettings-RequiredField-ErrorText="dd"
                            DataSourceID="dsCurrency" ValueField="CurrencyId" TextField="CurrencyId"
                            EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                            <ClientSideEvents SelectedIndexChanged="function ClickHandler(s, e) {
                                
                                            txt_ArRate.SetValue(cmb_FromCurrency.cpYahooRate[s.GetSelectedIndex()]);
                                            txt_ApRate.SetValue(cmb_FromCurrency.cpYahooRate[s.GetSelectedIndex()]);
                                            txt_YahooRate.SetValue(cmb_FromCurrency.cpYahooRate[s.GetSelectedIndex()]);
                                }" />
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="ToCurrency" FieldName="ToCurrencyId" VisibleIndex="2" Width="70">
                    <PropertiesComboBox DataSourceID="dsCurrency" TextField="CurrencyId" Width="100" IncrementalFilteringMode="StartsWith" ValueField="CurrencyId"></PropertiesComboBox>
                    <%--<EditItemTemplate>
                        <dxe:ASPxComboBox ID="cmb_ToCurrency" ClientInstanceName="cmb_ToCurrency" runat="server"
                            Value='<%# Bind("ToCurrencyId") %>' Width="100" DropDownStyle="DropDownList" ValidationSettings-RequiredField-ErrorText="dd"
                            DataSourceID="dsCurrency" ValueField="CurrencyId" TextField="CurrencyId"
                            EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        </dxe:ASPxComboBox>
                    </EditItemTemplate>--%>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataDateColumn Caption="ExpireDate" FieldName="ExRateDate" VisibleIndex="3" SortOrder="Descending" SortIndex="0" Width="120">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    <EditItemTemplate>
                        <dxe:ASPxDateEdit runat="server" ID="Date_ExRateDate" EditFormat="Custom" Width="120" EditFormatString="dd/MM/yyyy" Value='<%# Bind("ExRateDate") %>'>
                        </dxe:ASPxDateEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Billing Rate" FieldName="ExRate1" VisibleIndex="4" Width="80">
                    <PropertiesSpinEdit Increment=0 DisplayFormatString="#,0.000000" DecimalPlaces="6">
                    <SpinButtons ShowIncrementButtons="false" />
                    </PropertiesSpinEdit>
                    <EditItemTemplate>
                        <dxe:ASPxSpinEdit Increment="0" ID="txt_ArRate" Width="120" ClientInstanceName="txt_ArRate"
                            DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Bind("ExRate1") %>'>
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="TS Rate" FieldName="ExRate2" PropertiesSpinEdit-DisplayFormatString="#,0.000000" PropertiesSpinEdit-DecimalPlaces="6" VisibleIndex="5" Width="80">
                    <PropertiesSpinEdit Increment=0 DisplayFormatString="#,0.000000" DecimalPlaces="6">
                    <SpinButtons ShowIncrementButtons="false" />
                    </PropertiesSpinEdit>
                    <EditItemTemplate>
                        <dxe:ASPxSpinEdit Increment="0" ID="txt_ApRate" Width="120" ClientInstanceName="txt_ApRate"
                            DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Bind("ExRate2") %>'>
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Web Rate" FieldName="ExRate3" VisibleIndex="6" Width="80">
                    <PropertiesSpinEdit Increment=0 DisplayFormatString="#,0.000000" DecimalPlaces="6">
                    <SpinButtons ShowIncrementButtons="false" />
                    </PropertiesSpinEdit>
                    <EditItemTemplate>
                        <dxe:ASPxSpinEdit Increment="0" ID="txt_YahooRate" Width="120" ClientInstanceName="txt_YahooRate"
                            DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Bind("ExRate3") %>'>
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </EditItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
