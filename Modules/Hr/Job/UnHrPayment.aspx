<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnHrPayment.aspx.cs" Inherits="Modules_Hr_Job_UnHrPayment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
       <script type="text/javascript">
           function PopupMultipleAddPayment() {
               popubCtr.SetContentUrl('/warehouse/SelectPage/MultipleAddPayment.aspx');
               popubCtr.SetHeaderText('Add HR ApPayment');
               popubCtr.Show();
           }
           function AfterPopubMultiInv() {
               popubCtr.Hide();
               popubCtr.SetContentUrl('about:blank');
               grid.Refresh();
           }
           function PrintHr() {
               window.open("/ReportJob/PrintPaymentVoucher.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_to.GetText());
           }
           function OnCallback(v) {
               if (v == "Success") {
                   grid.Refresh();
               }
               else if (v == "Fail") {
                   alert("Fail!Pls try again");
               }
           }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td width="50px">
                <dxe:ASPxLabel ID="Label1" runat="server" Text="From">
                </dxe:ASPxLabel>
            </td>
            <td width="100px">
                <dxe:ASPxDateEdit ID="txt_from" ClientInstanceName="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
                <div style="display:none">
                    <dxe:ASPxDateEdit ID="txt_date" ClientInstanceName="txt_date" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
                </div>
            </td>
            <td width="50px">
                <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="To">
                </dxe:ASPxLabel>
            </td>
            <td width="100px">
                <dxe:ASPxDateEdit ID="txt_to" ClientInstanceName="txt_to" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
           <%-- <td>
                <dxe:ASPxLabel ID="lbl_pay" runat="server" Text="Payment">
                </dxe:ASPxLabel>
            </td>
            <td>
                <dxe:ASPxComboBox ID="cmb_Payment" runat="server" Width="100" DropDownStyle="DropDown">
                    <Items>
                        <dxe:ListEditItem Text="STD" Value="STD" />
                        <dxe:ListEditItem Text="REF" Value="REF" />
                        <dxe:ListEditItem Text="OTHER" Value="OTHER" />
                        <dxe:ListEditItem Text="OT" Value="OT" />
                        <dxe:ListEditItem Text="DEC" Value="DEC" />
                    </Items>
                </dxe:ASPxComboBox>
            </td>--%>
            <td>
                <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                </dxe:ASPxButton>
            </td>
                        <td>
                 <dxe:ASPxButton ID="ASPxButton3" Width="120px" runat="server" Text="Hr Print"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                  PrintHr();
                                    }" />
                        </dxe:ASPxButton>
            </td>
            <td>

                <dxe:ASPxButton ID="btn_AddNew" Width="180" runat="server" Text="Multiple Add ApPayment" AutoPostBack="false">
                    <ClientSideEvents Click="function(s, e) {
                                                        PopupMultipleAddPayment();
                                                        }" />
                </dxe:ASPxButton>
            </td>
                        <td>
                <dxe:ASPxButton ID="btn_export" Width="120" runat="server" Text="Save To Excel" OnClick="btn_export_Click">
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>

    <div>
    <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" Styles-Cell-HorizontalAlign="Left"
                KeyFieldName="Id" AutoGenerateColumns="False" 
                Width="960px">
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="false" />
                <SettingsBehavior ConfirmDelete="True" />
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2" Width="120" Visible="false">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="ICNO" FieldName="Code" VisibleIndex="2" Width="120" Visible="false">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="RefNo" VisibleIndex="2" Width="160" Visible="false">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataDateColumn Caption="JobDate" FieldName="JobTime" VisibleIndex="3" Width="120">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy"></PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="STD Amt" FieldName="Amount1" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="REF Amt" FieldName="Amount2" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="OTH Amt" FieldName="Amount3" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="OT Amt" FieldName="Amount4" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="DEC Amt" FieldName="Amount5" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="Total Amt" FieldName="TotalAmt" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="Payment Amt" FieldName="PayAmt" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn Caption="Difference Amt" FieldName="DiffAmt" VisibleIndex="4" Width="60">
                <PropertiesSpinEdit Increment="0" NumberType="Float" DecimalPlaces="2" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
        </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>

			<Settings ShowFooter="True" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="Name" SummaryType="Count" DisplayFormat="{0}" />
                <dxwgv:ASPxSummaryItem FieldName="TotalAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount1" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount2" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount3" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount4" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
                <dxwgv:ASPxSummaryItem FieldName="Amount5" SummaryType="Sum" DisplayFormat="{0:#,##0.00}"  />
            </TotalSummary>


				</dxwgv:ASPxGridView>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
         <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="800" EnableViewState="False">
               
            </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
