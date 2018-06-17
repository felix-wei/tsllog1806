<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="CostingList.aspx.cs" Inherits="WareHouse_SelectPage_CostingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.CloseCostingPage(); }
        }
        document.onkeydown = keydown;
    </script>

</head>
<body>
    <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.WhCosting" KeyMember="Id" FilterExpression="1=0" />
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel runat="server" ID="lbl_No" Text="DoNo:"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxLabel runat="server" ID="lbl_DoNo" Text=""></dxe:ASPxLabel>
                </td>
            </tr>
        </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsCosting"
                KeyFieldName="Id" AutoGenerateColumns="False" Width="100%" OnRowUpdating="ASPxGridView1_RowUpdating" 
                OnRowDeleting="ASPxGridView1_RowDeleting" OnRowInserting="ASPxGridView1_RowInserting" 
                OnBeforePerformDataSelect="ASPxGridView1_DataSelect" OnInit="ASPxGridView1_Init" OnInitNewRow="ASPxGridView1_InitNewRow">
                <SettingsBehavior ConfirmDelete="True" />
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <SettingsPager  Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewCommandColumn>
                        <EditButton Visible="true"></EditButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ChgCode" FieldName="ChgCode" Width="120" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ChgCodeDes" FieldName="ChgCodeDes" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="CostQty" VisibleIndex="2">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="CostPrice" VisibleIndex="2" >

                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CostCurrency" VisibleIndex="2" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="CostExRate" VisibleIndex="2" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="CostLocAmt" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Invoice No" VisibleIndex="5" Width="50">
                        <DataItemTemplate>
                            <a href='javascript: parent.parent.navTab.openTab("<%# Helper.Sql.One(string.Format(@"select top 1 DocNo from XAArInvoiceDet where MastRefNo='{0}' and ChgCode='{1}' ",Eval("RefNo"),Eval("ChgCode"))) %>","/opsAccount/ArInvoiceEdit.aspx?no=<%# Helper.Sql.One(string.Format(@"select top 1 DocNo from XAArInvoiceDet where MastRefNo='{0}' and ChgCode='{1}' ",Eval("RefNo"),Eval("ChgCode"))) %>",{title:"<%# Helper.Sql.One(string.Format(@"select top 1 DocNo from XAArInvoiceDet where MastRefNo='{0}' and ChgCode='{1}' ",Eval("RefNo"),Eval("ChgCode"))) %>", fresh:false, external:true});'><%# Helper.Sql.One(string.Format(@"select top 1 DocNo from XAArInvoiceDet where MastRefNo='{0}' and ChgCode='{1}' ",Eval("RefNo"),Eval("ChgCode"))) %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <div style="display: none">
                        </div>
                        <table>
                            <tr>
                                <td>Chge Code
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_ChgCode" runat="server" Text='<%# Bind("ChgCode")%>' BackColor="Control" ReadOnly="true"></dxe:ASPxTextBox>
                                </td>
                                <td>Description</td>
                                <td colspan="2">
                                    <dxe:ASPxTextBox ID="txt_ChgCodeDes" runat="server" Text='<%# Bind("ChgCodeDes")%>' BackColor="Control" ReadOnly="true"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <table width="100%">
                                        <tr>
                                            <td></td>
                                            <td>Qty
                                            </td>
                                            <td>Price
                                            </td>
                                            <td>Currency
                                            </td>
                                            <td>ExRate
                                            </td>
                                            <td>GST
                                            </td>
                                            <td>Amount
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                    ID="spin_CostCostQty" Text='<%# Bind("CostQty")%>' Increment="4" DisplayFormatString="0.000" DecimalPlaces="3">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                    runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("CostPrice")%>' Increment="0" DecimalPlaces="2">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" MaxLength="3" runat="server" Width="80" Text='<%# Bind("CostCurrency") %>'>
                                                    <Buttons>
                                                        <dxe:EditButton Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostCostCurrency,spin_CostCostExRate);
                        }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                    DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("CostExRate")%>' Increment="0">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="txt_det_GstType" ClientInstanceName="txt_det_GstType" Width="60" Text='<%# Bind("CostGstType")%>'>
                                                    <Items>
                                                        <dxe:ListEditItem Value="S" Text="S" />
                                                        <dxe:ListEditItem Value="Z" Text="Z" />
                                                        <dxe:ListEditItem Value="E" Text="E" />
                                                        <dxe:ListEditItem Value="N" Text="N" />
                                                    </Items>
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                                       PutGst(txt_det_GstType,spin_CostCostGst);
                                                                       Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                               }" />
                                                </dxe:ASPxComboBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                    DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostGst" ClientInstanceName="spin_CostCostGst" Text='<%# Bind("CostGst")%>' Increment="0">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc1(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt,spin_CostCostGst.GetText());
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                    BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                    Value='<%# Eval("CostLocAmt")%>'>
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Remark
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxMemo ID="memo_Remark" runat="server" Width="100%" Text='<%# Bind("Remark") %>' Rows="2"></dxe:ASPxMemo>
                                </td>
                            </tr>

                            <tr>
                                <td colspan="8">
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("IsInv"),"none")=="none" %>'>
                                            <ClientSideEvents Click="function(s,e){grid.UpdateEdit();}" />
                                        </dxe:ASPxHyperLink>
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div style="color: red; font-size: 12px; display: <%# SafeValue.SafeString(Eval("IsInv"),"none")%>">
                             It has already been created the bill.If you want to modify, please delete the invoice line 
                        </div>
                    </EditForm>
                </Templates>
                 <Settings ShowFooter="true" />
                    <TotalSummary>
                        <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="count" DisplayFormat="{0:#,##0}" />
                        <dxwgv:ASPxSummaryItem FieldName="CostQty" SummaryType="Sum" DisplayFormat="{0:#,##0}" />
                        <dxwgv:ASPxSummaryItem FieldName="CostLocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                    </TotalSummary>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
