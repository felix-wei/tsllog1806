<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostingEdit.aspx.cs" Inherits="WareHouse_CostingEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function PickBill() {
            var sn = txt_DoNo.GetText();
            var jt = cmb_JobType.GetText();
            var cc = txt_CustomerName.GetText();
            var ac = "";//txt_AgentCode.GetText();
            ac = btn_OriginPort.GetText();
            popubCtr1.SetHeaderText('Select Charges');
            popubCtr1.SetContentUrl('/SelectPage/PickRate.aspx?g=BILL&o=' + sn + '&t=' + jt + '&c=' + cc + '&a=' + ac);
            popubCtr1.Show();
        }

        function PickCost() {
            var sn = txt_DoNo.GetText();
            var jt = cmb_JobType.GetText();
            var cc = txt_CustomerName.GetText();
            var ac = "";
            ac = btn_OriginPort.GetText();
            popubCtr1.SetHeaderText('Select Cost');
            popubCtr1.SetContentUrl('/SelectPage/PickRate.aspx?g=COST&o=' + sn + '&t=' + jt + '&c=' + cc + '&a=' + ac);
            popubCtr1.Show();
        }

        function OnRateCallBack(v) {
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
            grid_Cost.GetValuesOnCustomCallback('Refresh', OnRateLocalCallback);
        }
        function OnPickCallback(v) {
            parent.PickRate("/SelectPage/PickRate.aspx?g=BILL&id="+v);
        }
        function CalCostAmt() {
            Calc1(spin_SaleQty.GetText(), spin_SalePrice.GetText(), spin_SaleExRate.GetText(), 2, spin_SaleDocAmt, 0);
        }
        function Calc1(qtyV, priceV, exRateV, num, totControl, gstV) {
            var qty = parseFloat(qtyV);
            var price = parseFloat(priceV);
            var exRate = parseFloat(exRateV);
            var gst = parseFloat(gstV);
            var amt = FormatNumber(qty * price, num);
            var gstAmt = 0;
            if (gst > 0)
                gstAmt = FormatNumber(amt * gst, num)
            var docAmt = parseFloat(amt) + parseFloat(gstAmt);
            if (exRate == 1){
                totControl.SetNumber(docAmt);
            }
            else {
                totControl.SetText(FormatNumber(docAmt * exRate, num));
            }
        }
        function PutBillingAmt() {
            Calc(spin_det_Qty.GetText(), spin_det_Price.GetText(), spin_det_GstP.GetText(), 2, spin_det_GstAmt, 0);
            Calc(spin_det_Qty.GetText(), spin_det_Price.GetText(), 1, 2, spin_det_DocAmt, spin_det_GstAmt.GetText());
            Calc(spin_det_DocAmt.GetText(), 1, spin_det_ExRate.GetText(), 2, spin_det_LocAmt, 0);
        }
        function FormatNumber(srcStr, nAfterDot) {
            var srcStr, nAfterDot;
            var resultStr, nTen;
            srcStr = "" + srcStr + "";
            strLen = srcStr.length;
            dotPos = srcStr.indexOf(".", 0);
            if (dotPos == -1) {
                resultStr = srcStr + ".";
                for (i = 0; i < nAfterDot; i++) {
                    resultStr = resultStr + "0";
                }
            }
            else {
                if ((strLen - dotPos - 1) >= nAfterDot) {
                    nAfter = dotPos + nAfterDot + 1;
                    nTen = 1;
                    for (j = 0; j < nAfterDot; j++) {
                        nTen = nTen * 10;
                    }
                    resultStr = Math.round(parseFloat(srcStr) * nTen) / nTen;
                }
                else {
                    resultStr = srcStr;
                    for (i = 0; i < (nAfterDot - strLen + dotPos + 1) ; i++) {
                        resultStr = resultStr + "0";
                    }

                }
            }
            return resultStr;

        }
        function ItemClickHandler(s, e) {
            SetLookupKeyValue(s.GetSelectedIndex());
        }
        function SetLookupKeyValue(rowIndex) {
            //txt_det_AcCode.SetText(cmb_ChgCode.cpAcCode[rowIndex]);
            txt_det_Des1.SetText(cmb_ChgCode.cpDes1[rowIndex]);
            txt_det_Unit.SetText(cmb_ChgCode.cpUnit[rowIndex]);
            //txt_det_GstType.SetText(cmb_ChgCode.cpGstType[rowIndex]);
            //spin_det_GstP.SetText(cmb_ChgCode.cpGst[rowIndex]);
            if (cmb_ChgCode.cpQty[rowIndex] !== undefined && cmb_ChgCode.cpQty[rowIndex] != null)
                spin_SaleQty.SetText(cmb_ChgCode.cpQty[rowIndex]);
            if (cmb_ChgCode.cpPrice[rowIndex] !== undefined && cmb_ChgCode.cpPrice[rowIndex] != null)
                spin_det_Price.SetText(cmb_ChgCode.cpPrice[rowIndex]);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.CostDet"
                KeyMember="SequenceId"  />
            <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" />
        <wilson:DataSource ID="dsChgCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="1=1 Order by ChgcodeId" />
            <table cellspacing="2">
                <tr>
                    <td><dxe:ASPxTextBox runat="server" ID="txt_Id" ClientVisible="false"></dxe:ASPxTextBox></td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton2b" Width="150" runat="server" Text="Add Charges" ClientVisible="false"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
            parent.PickRate('/SelectPage/PickRate.aspx?g=BILL&id='+txt_Id.GetText());
                                parent.rptFrameset.rows='0,*';
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton2d" Width="150" runat="server" Text="Add Cost" ClientVisible="false"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                grid_CostDet.AddNewRow();
                                parent.rptFrameset.rows='0,*';
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_CostDet" ClientInstanceName="grid_CostDet" runat="server" KeyFieldName="SequenceId" DataSourceID="dsCosting"
                Width="100%" OnBeforePerformDataSelect="grid_CostDet_DataSelect" OnInit="grid_CostDet_Init" OnInitNewRow="grid_CostDet_InitNewRow" OnRowInserting="grid_CostDet_RowInserting" OnRowDeleted="grid_CostDet_RowDeleted"
                OnRowUpdating="grid_CostDet_RowUpdating" OnRowInserted="grid_CostDet_RowInserted" OnRowUpdated="grid_CostDet_RowUpdated" OnHtmlEditFormCreated="grid_CostDet_HtmlEditFormCreated" OnRowDeleting="grid_CostDet_RowDeleting">
                <SettingsBehavior ConfirmDelete="True" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="100"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <%--<dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Type" FieldName="Status1" Width="50" VisibleIndex="1" Visible="false">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" ID="txtCost_Type" ClientInstanceName="txtCost_Type" Text='<%#Bind("Status1") %>' ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>

                    <dxwgv:GridViewDataComboBoxColumn Caption="ChgCode" FieldName="ChgCode" VisibleIndex="1" Width="80">
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="cmb_ChgCode" ClientInstanceName="cmb_ChgCode" runat="server" OnCustomJSProperties="cmb_ChgCode_CustomJSProperties"
                                Width="100%" DropDownWidth="300" DropDownStyle="DropDownList" DropDownButton-Visible="false" Text='<%# Bind("ChgCode") %>'
                                DataSourceID="dsChgCode" ValueField="SequenceId" TextField="ChgcodeId" ValueType="System.Int32" TextFormatString="{0}"
                                EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                CallbackPageSize="100" ListBoxStyle-BackgroundImage-VerticalPosition="bottom">
                                <Columns>
                                    <dxe:ListBoxColumn Caption="ChgcodeId" FieldName="ChgcodeId" Width="50" />
                                    <dxe:ListBoxColumn Caption="Description" FieldName="ChgcodeDe" />
                                    <dxe:ListBoxColumn Caption="Unit" FieldName="ChgUnit" Width="20" />
                                    <%--<dxe:ListBoxColumn Caption="Gst Type" FieldName="GstTypeId" />
                                    <dxe:ListBoxColumn Caption="Gst P" FieldName="GstP" />--%>
                                </Columns>
                                <ClientSideEvents SelectedIndexChanged="ItemClickHandler" />
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgCodeDes" Width="150" VisibleIndex="1">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" ID="txt_det_Des1" ClientInstanceName="txt_det_Des1" Text='<%#Bind("ChgCodeDes") %>'
                                Width="150"></dxe:ASPxTextBox>
                        </EditItemTemplate>
                            
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Qty" FieldName="SaleQty" VisibleIndex="3" Width="50">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit runat="server" Value='<%#Bind("SaleQty") %>' ID="spin_SaleQty" ClientInstanceName="spin_SaleQty"
                                DecimalPlaces="3" DisplayFormatString="0.000" SpinButtons-ShowIncrementButtons="false" Increment="0" Width="50">
                                <ClientSideEvents ValueChanged="function(s,e){
                                    CalCostAmt();
                                    }" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Price" FieldName="SalePrice" VisibleIndex="3" Width="50">
                        <DataItemTemplate>
                            <%# Eval("SalePrice","{0:0.00}") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit runat="server" Value='<%#Bind("SalePrice") %>' ID="spin_SalePrice" ClientInstanceName="spin_SalePrice" 
                                DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Increment="0" Width="50">
                                <ClientSideEvents ValueChanged="function(s,e){
                                    CalCostAmt();
                                    }" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="Unit" Caption="Unit" Width="80" VisibleIndex="3" PropertiesComboBox-DropDownButton-Visible="false">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Text="20FT" Value="20FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40FT" Value="40FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40HQ" Value="40HQ"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="45FT" Value="45FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="4M3" Value="4M3"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SET" Value="SET"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SHPT" Value="SHPT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="WT/M3" Value="WT/M3"></dxe:ListEditItem>
                            </Items>
                        </PropertiesComboBox>
                        <EditItemTemplate>
                            <dxe:ASPxComboBox runat="server" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit" Value='<%#Bind("Unit") %>'
                                Width="80">
                            <Items>
                                <dxe:ListEditItem Text="VOL" Value="VOL"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="20FT" Value="20FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40FT" Value="40FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40HQ" Value="40HQ"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="45FT" Value="45FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="4M3" Value="4M3"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SET" Value="SET"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SHPT" Value="SHPT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="WT/M3" Value="WT/M3"></dxe:ListEditItem>
                            </Items></dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataColumn Caption="Currency" FieldName="SaleCurrency" VisibleIndex="3" Width="50">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="ExRate" FieldName="SaleExRate" VisibleIndex="3" Width="50">
                        <DataItemTemplate>
                            <%# Eval("SaleExRate","{0:0.000000}") %>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit runat="server" Value='<%#Bind("SaleExRate") %>' ID="spin_SaleExRate" ClientInstanceName="spin_SaleExRate" 
                                DecimalPlaces="6" DisplayFormatString="0.000000" SpinButtons-ShowIncrementButtons="false" Increment="0" Width="70">
                                <ClientSideEvents ValueChanged="function(s,e){
                                    CalCostAmt();
                                    }" />
                            </dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Total" FieldName="SaleDocAmt" VisibleIndex="3" Width="80" ReadOnly="true">
                        <EditItemTemplate>
                            <dxe:ASPxSpinEdit runat="server" Value='<%# Eval("SaleDocAmt") %>' ID="spin_SaleDocAmt" ClientInstanceName="spin_SaleDocAmt"
                                DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" Increment="0" Width="50" BackColor="Control" ReadOnly="true"></dxe:ASPxSpinEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Remark" FieldName="Remark" VisibleIndex="3" Width="80">
                    </dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn FieldName="RowUpdateUser" Caption="Update By" VisibleIndex="9"
                        Width="100">
                        <DataItemTemplate><%# Eval("RowUpdateUser") %></DataItemTemplate>
                        <EditItemTemplate><%# Eval("RowUpdateUser") %></EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="RowUpdateTime" Caption="Time" VisibleIndex="9" Width="100">
                        <DataItemTemplate><%# Eval("RowUpdateTime","{0:dd/MM HH:mm}") %></DataItemTemplate>
                        <EditItemTemplate><%# Eval("RowUpdateTime","{0:dd/MM HH:mm}") %></EditItemTemplate>
                    </dxwgv:GridViewDataColumn>

                </Columns>

                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="ChgCode" ShowInGroupFooterColumn="ChgCode" SummaryType="Count"
                        DisplayFormat="{0:0}" />
                    <dxwgv:ASPxSummaryItem FieldName="SaleDocAmt" ShowInColumn="SaleDocAmt" SummaryType="Sum"
                        DisplayFormat="{0:0.00}" />
                </TotalSummary>

                <%--<Templates>
                    <EditForm>
                        <div style="display: none">
                        </div>
                        <table>
                            <tr>
                                <td>Charge Code
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_CostChgCode" ClientInstanceName="txt_CostChgCode" Width="100" runat="server" Text='<%# Bind("ChgCode")%>' ReadOnly='True'>
                                        <Buttons>
                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_CostChgCode,txt_CostDes);
                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td colspan="2">
                                    <dxe:ASPxTextBox Width="265" ID="txt_CostDes" ClientInstanceName="txt_CostDes"
                                        ReadOnly="true" runat="server" Text='<%# Bind("ChgCodeDes") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Vendor
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_CostVendorId" ClientInstanceName="txt_CostVendorId" Width="80" runat="server" Text='<%# Bind("VendorId")%>'>
                                        <Buttons>
                                            <dxe:EditButton Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                    PopupVendor(txt_CostVendorId,txt_CostVendorName);
                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td colspan="2">
                                    <dxe:ASPxTextBox runat="server" Width="200" ID="txt_CostVendorName"
                                        ClientInstanceName="txt_CostVendorName" ReadOnly="true" Text=''>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Job No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox Width="100" ID="txt_CostJobNo" ClientInstanceName="txt_CostJobNo"
                                        runat="server" Text='<%# Bind("JobNo") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Split Type
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbo_SplitType" runat="server" Text='<%# Bind("SplitType")%>' Width="205">
                                        <Items>
                                            <dxe:ListEditItem Text="Set" Value="Set" />
                                            <dxe:ListEditItem Text="WtM3" Value="WtM3" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Remark
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Doc No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_DocNo" ClientInstanceName="txt_DocNo" Text='<%# Bind("DocNo") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td colspan="2">
                                    <table>
                                        <td>Pay on Behalf
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_PayInd" Width="60" runat="server"
                                                Text='<%# Bind("PayInd") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                        <td>Verify Ind
                                        </td>
                                        <td>
                                            <dxe:ASPxComboBox EnableIncrementalFiltering="True" ID="txt_VerifryInd" Width="60" runat="server"
                                                Text='<%# Bind("VerifryInd") %>'>
                                                <Items>
                                                    <dxe:ListEditItem Text="Y" Value="Y" />
                                                    <dxe:ListEditItem Text="N" Value="N" />
                                                </Items>
                                            </dxe:ASPxComboBox>
                                        </td>
                                    </table>
                                </td>
                                <td>Salesman
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Salesman"
                                        DataSourceID="dsSalesman" TextField="Code" ValueField="Code" Width="100%" Value='<%# Bind("Salesman")%>'>
                                    </dxe:ASPxComboBox>
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
                                            <td>Amount
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Sale
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostSaleQty"
                                                    ID="spin_CostSaleQty" Text='<%# Bind("SaleQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSalePrice"
                                                    runat="server" Width="100" ID="spin_CostRevPrice" Value='<%# Bind("SalePrice")%>' Increment="0" DecimalPlaces="2">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="cmb_CostSaleCurrency" ClientInstanceName="cmb_CostSaleCurrency" runat="server" Width="80" Text='<%# Bind("SaleCurrency") %>' MaxLength="3">
                                                    <Buttons>
                                                        <dxe:EditButton Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostSaleCurrency,null);
                        }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                    DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostSaleExRate" ClientInstanceName="spin_CostSaleExRate" Text='<%# Bind("SaleExRate")%>' Increment="0">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostSaleQty.GetText(),spin_CostSalePrice.GetText(),spin_CostSaleExRate.GetText(),2,spin_CostSaleAmt);
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostSaleAmt"
                                                    ReadOnly="true" runat="server" Width="120" ID="spin_CostSaleAmt"
                                                    Value='<%# Eval("SaleLocAmt")%>'>
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Cost
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                    ID="spin_CostCostQty" Text='<%# Bind("CostQty")%>' Increment="0" DisplayFormatString="0.000" DecimalPlaces="3">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                    runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("CostPrice")%>' Increment="0" DecimalPlaces="2">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" runat="server" Width="80" Text='<%# Bind("CostCurrency") %>' MaxLength="3">
                                                    <Buttons>
                                                        <dxe:EditButton Text=".."></dxe:EditButton>
                                                    </Buttons>
                                                    <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CostCostCurrency,null);
                        }" />
                                                </dxe:ASPxButtonEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                                                    DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("CostExRate")%>' Increment="0">
                                                    <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                            <td>
                                                <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                    ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                    Value='<%# Eval("CostLocAmt")%>'>
                                                </dxe:ASPxSpinEdit>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right; padding: 2px 2px 2px 2px">

                            <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("RefStatusCode"),"USE")=="USE" %>'>
                                <ClientSideEvents Click="function(s,e){grid_Cost.UpdateEdit();}" />
                            </dxe:ASPxHyperLink>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
                </Templates>--%>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
