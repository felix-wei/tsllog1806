<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="ChartOfAccount_bank.aspx.cs" Inherits="SelectPage_ChartOfAccount_bank" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Chart Of Account</title>
    <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsGlAccChart" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartAcc" KeyMember="SequenceId" FilterExpression="AcBank='Y'"/>
            <wilson:DataSource ID="dsCurrency" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCurrency" KeyMember="CurrencyId"/>
            <wilson:DataSource ID="dsAcGroup" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartGroup" KeyMember="SequenceId" />
            
        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" DataSourceID="dsGlAccChart"
            Width="100%" KeyFieldName="SequenceId" AutoGenerateColumns="False" OnInit="ASPxGridView1_Init"
            OnInitNewRow="ASPxGridView1_InitNewRow" OnRowInserting="ASPxGridView1_RowInserting"
            OnRowInserted="ASPxGridView1_RowInserted"  OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="EditForm" />
            <Settings ShowFilterRow="true" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsBehavior AllowFocusedRow="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick='parent.PutValue("<%# Eval("Code") %>","<%# Eval("AcDesc") %>");'>Select</a>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="AcDesc" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="AcType" FieldName="AcType" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="AcDorc" FieldName="AcDorc" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Bank" FieldName="AcBank" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="AcCurrency" VisibleIndex="6">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="SubType" FieldName="AcSubType" VisibleIndex="7">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div style="padding: 4px 4px 3px 4px">
                        <table>
                            <tr>
                                <td width="100">
                                    Account Code:
                                </td>
                                <td width="150">
                                    <dxe:ASPxTextBox ID="txt_AcCode" Width="80%" runat="server" Text='<%# Bind("Code")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td width="100">
                                </td>
                                <td width="150">
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    Description:
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxTextBox ID="txt_AcDesc" Width="80%" runat="server" Text='<%# Bind("AcDesc")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    Type:
                                </td>
                                <td width="150">
                                    <dxe:ASPxComboBox ID="cbo_AcType" Width="80%" runat="server" Value='<%# Bind("AcType")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="B" Text="Balance Sheet" Selected="true" />
                                            <dxe:ListEditItem Value="P" Text="P & L" />
                                            <dxe:ListEditItem Value="R" Text="Ret. Earnings" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="100">
                                    Ac Source:
                                </td>
                                <td width="150">
                                    <dxe:ASPxComboBox ID="cbo_AcDorc" Width="80%" runat="server" Value='<%# Bind("AcDorc")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="DB" Text="DEBIT" Selected="true" />
                                            <dxe:ListEditItem Value="CR" Text="CREDIT" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    Bank:
                                </td>
                                <td width="150">
                                    <dxe:ASPxComboBox ID="cbo_AcBank" Width="80%" runat="server" Value='<%# Bind("AcBank")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" Selected="true" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td width="100">
                                    Sub Type:
                                </td>
                                <td width="150">
                                    <dxe:ASPxComboBox ID="cbo_AcSubtype" Width="80%" runat="server" Value='<%# Bind("AcSubType")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="FA" Text="FIXED ASSETS" />
                                            <dxe:ListEditItem Value="CA" Text="CURRENT ASSETS" />
                                            <dxe:ListEditItem Value="LL" Text="LONG TERM LIABILITIES" />
                                            <dxe:ListEditItem Value="CL" Text="CURRENT LIABILITIES" />
                                            <dxe:ListEditItem Value="SC" Text="SHARE CAPITAL" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Currency:
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" Width="80%" ID="cbo_CurrencyId" DataSourceID="dsCurrency" EnableIncrementalFiltering="true"
                                        TextField="CurrencyId" ValueField="CurrencyId" ValueType="System.String" value='<%# Bind("AcCurrency")%>'>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Group Desc:
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox runat="server" Width="80%" ID="cbo_AcGroup" DataSourceID="dsAcGroup" EnableIncrementalFiltering="true"
                                        TextField="Name" ValueField="Code" ValueType="System.String" value='<%# Bind("GNo")%>' TextFormatString="{1}"
                     EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" CallbackPageSize="30">
                                        <Columns>
                         <dxe:ListBoxColumn FieldName="Code" Width="130px" />
                         <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                     </Columns>

                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: right; padding: 2px 2px 2px 2px">
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                        runat="server">
                                    </dxwgv:ASPxGridViewTemplateReplacement>
                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                        runat="server">
                                    </dxwgv:ASPxGridViewTemplateReplacement>
                                </td>
                            </tr>
                        </table>
                    </div>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>