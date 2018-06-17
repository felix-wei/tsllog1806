<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="rate_edit.aspx.cs" Inherits="Z_rate_edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Invoice</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Edi.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript">
        
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr1(); }
        }
        document.onkeydown = keydown;

    </script>
    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        var clientType = null;
        var clientAcCode = null;
        function PutValue(s, name) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                clientId = null;
                clientName = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
                PutDetAmt();
            }
        }
        function PutValue(s, name, type1) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                if (clientType != null) {
                    clientType.SetText(type1);
                }
                clientId = null;
                clientName = null;
                clientAcCode = null;
                clientType = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PutValue(s, name, type1, acCode) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                if (clientType != null) {
                    clientType.SetText(type1);
                }
                if (acCode != null) {
                    clientAcCode.SetText(acCode);
                }
                clientId = null;
                clientName = null;
                clientAcCode = null;
                clientType = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function OnCallback(v) {
            if (v == "Success") {
                alert(v);
                grid_det.Refresh();
            }
            grid_DropDownEdit1.HideDropDown();
        }

        function OnbookCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function onDetCallback(v) {
            if (v == "Success") {
                grid_det.Refresh();
            }
        }
        function AddInvoiceDet_quote() {
            //popubCtr.SetHeaderText('Quotation');
            //popubCtr.SetContentUrl('QuoteList.aspx?id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.SetHeaderText('STD Rate');
            popubCtr.SetContentUrl('/SelectPage/Account/StdRateList.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AddInvoiceDet() {
            popubCtr.SetHeaderText('AR/AP Invoice');
            //popubCtr.SetContentUrl('BillList.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.SetContentUrl('/selectpage/account/BillList.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AddInvoiceDet_Multi() {
            popubCtr.SetHeaderText('Multiple ChgCode');
            popubCtr.SetContentUrl('/SelectPage/ChgCodeList_Ar_multi.aspx?typ=AR&id=' + txt_Oid.GetText() + "&no=" + txt_DocNo.GetText());
            popubCtr.Show();
        }
        function AfterPopubMultiInv() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
            grid_det.Refresh();
        }
        function OnPostCallback(v) {
            alert(v);
            ASPxGridView1.Refresh();
        }
        function ShowMoreInfoInd() {
            var moreinfo = document.getElementById("divMoreInfo");
            var linkmoreinfo = document.getElementById("LinkMoreInfo");
            if (moreinfo.style.display == "") {
                moreinfo.style.display = "none";
                linkmoreinfo.innerText = "More";
                var grd = ASPxClientGridView.Cast();
                grid_det.SetHeight(270);
            }
            else {
                moreinfo.style.display = "";
                linkmoreinfo.innerText = "Hide";
                grid_det.SetHeight(240);
            }
        }
        function ShowPayList() {
            popubCtr.SetHeaderText('Payment List');
            popubCtr.SetContentUrl('/SelectPage/PayList.aspx?mastId=' + txt_Oid.GetText());
            popubCtr.Show();
        }
        function ItemClickHandler(s, e) {
            SetLookupKeyValue(s.GetSelectedIndex());
        }
        function SetLookupKeyValue(rowIndex) {
            txt_det_AcCode.SetText(cmb_ChgCode.cpAcCode[rowIndex]);
            txt_det_Des1.SetText(cmb_ChgCode.cpDes1[rowIndex]);
            txt_det_Unit.SetText(cmb_ChgCode.cpUnit[rowIndex]);
            txt_det_GstType.SetText(cmb_ChgCode.cpGstType[rowIndex]);
            spin_det_GstP.SetText(cmb_ChgCode.cpGst[rowIndex]);
            if (cmb_ChgCode.cpQty[rowIndex] !== undefined && cmb_ChgCode.cpQty[rowIndex] != null)
                spin_det_Qty.SetText(cmb_ChgCode.cpQty[rowIndex]);
            if (cmb_ChgCode.cpPrice[rowIndex] !== undefined && cmb_ChgCode.cpPrice[rowIndex] != null)
                spin_det_Price.SetText(cmb_ChgCode.cpPrice[rowIndex]);
        }
        function ChangeBackColor(s) {
            s.GetMainElement().style.backgroundColor = "PapayaWhip";
            s.GetInputElement().style.background = "content-box";
        }
        function RowEdit(s, e) {
            var changed=false;
            $("*").each(function () {
                if (this.style.backgroundColor == "rgb(255, 239, 213)") {//rgb(255,239,213)
                    changed = true;
                }
            });
            if (changed && PresentDetId.GetText() != "") {
                PresentDetId.SetText(e.visibleIndex);
                if(confirm("Data modified,do u want to save ?")){
                    grid_det.PerformCallback("UPDATELASTROW");
                }
                else {
                    grid_det.StartEditRow(e.visibleIndex);
                }
            }
            else if (btn_Post.GetEnabled()) {
                PresentDetId.SetText(e.visibleIndex);
                grid_det.StartEditRow(e.visibleIndex);
            }
            
        }
        $(function () {
            $(":input").keyup(function () { $(this).css("background", "PapayaWhip"); })
            $(":input").change(function () { $(this).css("background", "PapayaWhip"); })
            $(":text").keyup(function () { $(this).css("background", "PapayaWhip"); })
            $(":text").change(function () { $(this).css("background", "PapayaWhip"); })
            $("textarea").keyup(function () { $(this).css("background", "PapayaWhip"); })
            $("textarea").change(function () { $(this).css("background", "PapayaWhip"); })

            
        })
        
    </script>
</head>
<body style="padding: 0px; border-spacing: 0; margin: 0px; border: 0px; overflow: hidden; background-color: #F0F0F0">
    <form id="form1" runat="server">
        
            <wilson:DataSource ID="dsArInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.rate_doc" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsArInvoiceDet" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.rate_line" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXTerm" KeyMember="SequeceId" />
            <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
        <wilson:DataSource ID="dsChgCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="1=1 Order by ChgcodeId" />
        <wilson:DataSource ID="dsUom" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="SequenceId" FilterExpression="CodeType in('1','2')" />
        <wilson:DataSource ID="dsCurrency" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXCurrency" KeyMember="CurrencyId" />
        <table style="float: left; position: absolute; top: 7px;">
            <tr style="display: none">
                <td>
                    <dxe:ASPxTextBox runat="server" ID="PresentDetId"></dxe:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 6px"></td>
                <td>Doc No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txtSchNo" ClientInstanceName="txtSchNo" Width="150" runat="server"
                            Text="">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" Width="90" runat="server" Text="Retrieve" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s, e) {
                     window.location='rate_edit.aspx?no='+txtSchNo.GetText()
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="90" runat="server" Text="Go Search" AutoPostBack="false"
                                        UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s,e) {
                                           window.location='rate_list.aspx';
                        }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" Styles-EditForm-Border-BorderWidth="0" Styles-EditForm-Paddings-PaddingTop="0" Styles-EditForm-Paddings-PaddingBottom="0"
                DataSourceID="dsArInvoice" Width="100%" KeyFieldName="Id" OnInit="ASPxGridView1_Init" Border-BorderWidth="0"
                OnInitNewRow="ASPxGridView1_InitNewRow" OnCustomCallback="ASPxGridView1_CustomCallback" SettingsText-EmptyDataRow=" "
                OnHtmlEditFormCreated="ASPxGridView1_HtmlEditFormCreated" OnCustomDataCallback="ASPxGridView1_CustomDataCallback1"
                AutoGenerateColumns="False" OnRowInserting="ASPxGridView1_RowInserting" OnRowUpdating="ASPxGridView1_RowUpdating">
                <SettingsPager PageSize="50">
                </SettingsPager>
                <Settings ShowColumnHeaders="false" />
                <SettingsEditing Mode="EditForm" />
                <SettingsCustomizationWindow Enabled="True" />
                <Templates>
                    <EditForm>
                        <table style="border:0px">
                            <tr>
                                <td colspan="6" style="display: none">
                                    <dxe:ASPxTextBox runat="server" ID="txt_Oid" ClientInstanceName="txt_Oid" ReadOnly="true" BackColor="Control"
                                        Width="100" Text='<%# Eval("Id")%>'>
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100"></td>
                                <td width="160"></td>
                                <td width="100"></td>
                                <td width="290"></td>
                                <td width="100"></td>
                                <td width="120"></td>
                            </tr>
                            <tr style="text-align: right;">
                                <td colspan="7" style="width: 900px;">
                                    <table>
                                        <tr>
                                            <td width="53.5%"></td>
                                            <td>
                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print Simple" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false" ClientVisible="false">
                                                    <ClientSideEvents Click="function(s,e){
                                PrintInvoiceA4(txt_DocNo.GetText(),'IV', txt_MastType.GetText());
                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_Post" ClientInstanceName="btn_Post" runat="server" Width="70" AutoPostBack="false" UseSubmitBehavior="false"
                                                    Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("ExportInd"),"N")!="Y"&&SafeValue.SafeString(Eval("CancelInd"),"N")!="Y" %>' Text="Post">
                                                    <ClientSideEvents Click="function(s,e) {
                                    if(confirm('Confirm post it?')) {
                                                    ASPxGridView1.GetValuesOnCustomCallback('P,' + txt_DocNo.GetText(),OnPostCallback);
                                                    }
                                                 }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Print" Width="70" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e){
                                PrintInvoice(txt_DocNo.GetText(),'IV', txt_MastType.GetText());
                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_Email" runat="server" Text="Email" Width="70" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'
                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s,e){
                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_EDI" runat="server" Width="70" AutoPostBack="false"
                                                    Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>' UseSubmitBehavior="false"
                                                    Text="EDI">
                                                    <ClientSideEvents Click="function(s,e){
                                                        EdiAr(txt_DocNo.GetText());
                                                        }" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td width="30"></td>
                                            <td>
                                                <dxe:ASPxButton ID="btn_Save" runat="server" Text="Save" AutoPostBack="false" UseSubmitBehavior="false"
                                                    Enabled='<%# SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>' Width="85">
                                                    <ClientSideEvents Click="function(s, e) {
                                ASPxGridView1.PerformCallback('');
                            }" />
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td>Doc No
                                </td>
                                <td>
                                    <table cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="104">
                                                <dxe:ASPxTextBox runat="server" ID="txt_DocNo" ClientInstanceName="txt_DocNo" ReadOnly="true"
                                                    BackColor="Control" Border-BorderWidth="0" Width="100" Text='<%# Eval("DocNo")%>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="cbo_DocType" ClientInstanceName="cbo_DocType" ReadOnly='<%# SafeValue.SafeString(Eval("Id"),"0")!="0" %>'
                                                    BackColor="Control" Width="22" Text='<%# Eval("DocType")%>' Border-BorderWidth="0" DropDownButton-Visible="false">
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>Customer
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server" OnCustomJSProperties="cmb_PartyTo_CustomJSProperties"
                                        Value='<%# Eval("PartyTo") %>' Width="250" DropDownWidth="380" DropDownStyle="DropDownList" ValidationSettings-RequiredField-ErrorText="dd"
                                        DataSourceID="dsCustomerMast" ValueField="PartyId" ValueType="System.String" TextFormatString="{1}"
                                        EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                        CallbackPageSize="100">
                                        <Columns>
                                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                                        </Columns>
                                        <ClientSideEvents SelectedIndexChanged="function ClickHandler(s, e) {
                                            txt_TermId.SetValue(cmb_PartyTo.cpTerm[s.GetSelectedIndex()]);}" />
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Currency
                                </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_Currency" ClientInstanceName="txt_Currency" runat="server" MaxLength="3"
                                        Width="120" HorizontalAlign="Left" AutoPostBack="False" Text='<%# Eval("CurrencyId") %>'>
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupCurrency(txt_Currency,txt_DocExRate);
                                                                    }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>Doc Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="100" Value='<%# Eval("DocDate")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>Customer Ref</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_SpecialNote" ClientInstanceName="txt_SpecialNote" runat="server" Text='<%# Eval("SpecialNote") %>' Width="250">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Ex Rate
                                </td>
                                <td>
                                    <dxe:ASPxSpinEdit Increment="0" ID="txt_DocExRate" Width="120" ClientInstanceName="txt_DocExRate"
                                        DisplayFormatString="0.000000" DecimalPlaces="6" runat="server" Value='<%# Eval("ExRate") %>'>
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                            </tr>
                            <tr>
                                <td>Term
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_TermId" ClientInstanceName="txt_TermId"
                                        DataSourceID="dsTerm" TextField="Code" ValueField="Code" Width="100" Value='<%# Eval("Term")%>' ValueType="System.String">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Remark</td>
                                <td rowspan="2">
                                    <dxe:ASPxMemo runat="server" ID="txt_Remarks1" Rows="3" Width="250" Text='<%# Eval("Description")%>'>
                                    </dxe:ASPxMemo>
                                </td>
                                <td style="font-size: 20px; font-weight: bolder; width: 90px;">Total</td>
                                <td style="font-size: 20px; font-weight: bolder"><%# Eval("LocAmt")%></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align: center; width: 20px; font-weight: bolder"></td>
                                <td style="font-weight: bolder;" colspan="2">
                                    <a href="#" onclick='ShowPayList();' style="color: <%# SafeValue.SafeDecimal(Eval("BalanceAmt"),0) == 0?"green":(SafeValue.SafeDecimal(Eval("LocAmt"),0)==SafeValue.SafeDecimal(Eval("BalanceAmt"),0)?"red":"orange")%>; text-decoration: <%# SafeValue.SafeDecimal(Eval("LocAmt"),0) == SafeValue.SafeDecimal(Eval("BalanceAmt"),0) ? "none" : "underline"%>;"><%# SafeValue.SafeDecimal(Eval("LocAmt"),0)-SafeValue.SafeDecimal(Eval("BalanceAmt"),0)%>&nbsp;&nbsp;&nbsp;<%# (SafeValue.SafeDecimal(Eval("LocAmt"),0) != 0&&SafeValue.SafeDecimal(Eval("BalanceAmt"),0) == 0)?"Paid":(SafeValue.SafeDecimal(Eval("LocAmt"),0)==SafeValue.SafeDecimal(Eval("BalanceAmt"),0)?"":"Partial Paid")%></a>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>Due Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_DueDt" runat="server" Enabled="false" BackColor="Control"
                                        Width="100" Value='<%# Eval("DocDueDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                        DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td>GL Code
                                </td>
                                <td>
                                    <table cellspacing="0" cellpading="0">
                                        <tr>
                                            <td width="104">
                                                <dxe:ASPxTextBox runat="server" ID="txt_AcCode" ClientInstanceName="txt_AcCode" BackColor="Control"
                                                    ReadOnly="true" Width="100" Text='<%# Eval("AcCode")%>' Border-BorderWidth="0">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="txt_AcSource" Width="22" ReadOnly="true" BackColor="Control"
                                                    Text='<%# Eval("AcSource")%>' DropDownButton-Visible="false" DropDownStyle="DropDown" Border-BorderWidth="0">
                                                    <Items>
                                                        <dxe:ListEditItem Value="CR" Text="CR" />
                                                        <dxe:ListEditItem Value="DB" Text="DB" />
                                                    </Items>
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>Ref No
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_MastRefNo" runat="server" Text='<%# Eval("MastRefNo") %>' Width="250" ClientInstanceName="txt_MastRefNo">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Last Updated</td>
                                <td><dxe:ASPxLabel Text='<%#Eval("UserId") %>' runat="server"></dxe:ASPxLabel></td>
                            </tr>
                            <tr>
                                <td>GL Period
                                </td>
                                <td>
                                    <table cellspacing="0" cellpading="0">
                                        <tr>
                                            <td width="104">
                                                <dxe:ASPxTextBox runat="server" Width="100" ReadOnly="true" BackColor="Control" ID="txt_AcYear"
                                                    Text='<%# Eval("AcYear")%>' Border-BorderWidth="0">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox runat="server" Width="22" ReadOnly="true" BackColor="Control" ID="txt_AcPeriod"
                                                    Text='<%# Eval("AcPeriod")%>' Border-BorderWidth="0">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>Job No </td>
                                <td>
                                    <table cellspacing="0" cellpading="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_JobRefNo" runat="server" Text='<%# Eval("JobRefNo") %>' Width="250">
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="txt_MastType" runat="server" BackColor="Control" Border-BorderWidth="0" ClientInstanceName="txt_MastType" ReadOnly="true" Text='<%# Eval("MastType")%>' Width="30">
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td></td>
                                <td>
                                    <dxe:ASPxDateEdit ID="ASPxDateEdit1" runat="server" BackColor="#F0F0F0" Border-BorderWidth="0" DropDownButton-Visible="false" ReadOnly="true"
                                        Width="100%" Value='<%# Eval("EntryDate")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm:ss"
                                        DisplayFormatString="dd/MM/yyyy HH:mm:ss">
                                    </dxe:ASPxDateEdit>
                            </tr>
                            <%  ASPxTextBox Oid = ASPxGridView1.FindEditFormTemplateControl("txt_Oid") as ASPxTextBox;
                                string innerText = ""; string display = "";
                                string sql = string.Format("Select count(Id) from bill_line where docId='{0}'", Oid.Text); %>
                            <% int c_LineId = SafeValue.SafeInt(C2.Manager.ORManager.ExecuteScalar(sql), 0);
                               if (c_LineId == 0) { innerText = "Hide"; display = ""; }
                               else { innerText = "More"; display = "None"; } %>
                            <tr>
                                <td colspan="6">
                                    <hr>
                                </td>
                                <td><a id="LinkMoreInfo" href="#" onclick='ShowMoreInfoInd();' style="text-decoration: none"><%=innerText %></a>
                                </td>
                            </tr>
                            <tr id="divMoreInfo" style="display: <%=display%>">
                                <td colspan="6">
                                    <table style="padding-top:0px;border-spacing:0px">
                                        <td>
                                            <dxe:ASPxButton ID="btn_DetAdd" runat="server" Text="Add Line" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e){
                                grid_det.AddNewRow();
                            }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td style="display:none">
                                            <dxe:ASPxButton ID="btn_DetAdd_Multi" runat="server" Text="Multi" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet_Multi();
                            }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_DetAdd_quote" runat="server" Text="STD Rate" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false" Width="80">
                                                <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet_quote();
                            }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_DetAdd1" ClientInstanceName="btn_DetAdd1" runat="server" Text="Copy" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false" Width="60">
                                                <ClientSideEvents Click="function(s,e){
                                AddInvoiceDet();
                            }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td width="30"></td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode1" ClientInstanceName="btn_ShortcutCode1" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode1.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode2" ClientInstanceName="btn_ShortcutCode2" runat="server" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode2.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode3" ClientInstanceName="btn_ShortcutCode3" runat="server" Text="PSA" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode3.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode4" ClientInstanceName="btn_ShortcutCode4" runat="server" Text="FRTOC20" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode4.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode5" ClientInstanceName="btn_ShortcutCode5" runat="server" Text="FRTOC40" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode5.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode6" ClientInstanceName="btn_ShortcutCode6" runat="server" Text="THC" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode6.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode7" ClientInstanceName="btn_ShortcutCode7" runat="server" Text="FRTOC" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode7.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode8" ClientInstanceName="btn_ShortcutCode8" runat="server" Text="LCL" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode8.GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode9" ClientInstanceName="btn_ShortcutCode9" runat="server" Text="DEPOT" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode9.GetText().GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                        <td>
                                            <dxe:ASPxButton ID="btn_ShortcutCode10" ClientInstanceName="btn_ShortcutCode10" runat="server" Text="DEPOT" Enabled='<%# SafeValue.SafeString(Eval("Id"),"0")!="0"&&SafeValue.SafeString(Eval("ExportIndStr"),"N")!="Y" %>'
                                                AutoPostBack="false" UseSubmitBehavior="false">
                                                <ClientSideEvents Click="function(s,e) {
                                                        grid_det.GetValuesOnCustomCallback(btn_ShortcutCode10.GetText().GetText(),onDetCallback);
                                                        }" />
                                            </dxe:ASPxButton>
                                        </td>
                                    </table>
                                </td>
                            </tr>
                            <%
                                sql = "select top(10) ArShortcutCode from XXChgCode where isnull(ArShortcutCode,'')<>''";
                                DataTable tab = C2.Manager.ORManager.GetDataSet(sql).Tables[0];
                                for (int i = 1; i <= 10; i++)
                                {
                                    ASPxButton btn_ShortcutCode = this.ASPxGridView1.FindEditFormTemplateControl("btn_ShortcutCode" + i) as ASPxButton;
                                    if (i < tab.Rows.Count)
                                        btn_ShortcutCode.Text = SafeValue.SafeString(tab.Rows[i - 1][0]);
                                    else
                                        btn_ShortcutCode.ClientVisible = false;
                                } %>
                        </table>
                            <dxwgv:ASPxGridView ID="grid_InvDet" ClientInstanceName="grid_det" runat="server" EnableRowsCache="false" Styles-Header-Paddings-PaddingLeft="1.5"
                                DataSourceID="dsArInvoiceDet" KeyFieldName="Id" OnBeforePerformDataSelect="grid_InvDet_BeforePerformDataSelect" Styles-Header-SortingImageSpacing="0" Images-HeaderSortDown-Width="0" Images-HeaderSortUp-Width="0"
                                OnRowUpdating="grid_InvDet_RowUpdating" OnRowInserting="grid_InvDet_RowInserting" Styles-InlineEditCell-VerticalAlign="Top" Styles-Cell-VerticalAlign="Top" Styles-InlineEditCell-Paddings-Padding="1" Styles-Cell-Paddings-Padding="2"
                                OnInitNewRow="grid_InvDet_InitNewRow" OnInit="grid_InvDet_Init" OnRowDeleting="grid_InvDet_RowDeleting" SettingsCommandButton-CancelButton-Text="C" SettingsCommandButton-DeleteButton-Text="X" SettingsCommandButton-UpdateButton-Text="U"
                                OnRowInserted="grid_InvDet_RowInserted" OnRowUpdated="grid_InvDet_RowUpdated" OnCustomDataCallback="grid_InvDet_CustomDataCallback"
                                OnRowDeleted="grid_InvDet_RowDeleted" Width="900" AutoGenerateColumns="False" OnAfterPerformCallback="grid_InvDet_AfterPerformCallback">
                                <Settings ShowFooter="true" VerticalScrollableHeight="180" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Visible" />
                                <SettingsEditing Mode="Inline" />
                                <SettingsPager Mode="ShowAllRecords" PageSize="100" />
                                <SettingsBehavior ConfirmDelete="true" />
                                <SettingsPopup CustomizationWindow-VerticalAlign="Below" CustomizationWindow-HorizontalAlign="LeftSides" EditForm-VerticalAlign="Below"></SettingsPopup>
                                <Columns>
                                    <dxwgv:GridViewDataColumn Caption="#" Width="50" CellStyle-Paddings-Padding="0" CellStyle-HorizontalAlign="Center" VisibleIndex="0" FixedStyle="Left">
                                            <DataItemTemplate>
                                                <div style='display: <%# Eval("Display")%>'>
                                                    <a href="#" onclick='<%# "grid_det.StartEditRow("+Container.VisibleIndex+"); " %>'>E</a>
                                                    <a href="#" onclick='if(confirm("Confirm Delete"))  {<%# "grid_det.DeleteRow("+Container.VisibleIndex+");"  %>}'>D</a>
                                                </div>
                                            </DataItemTemplate>
                                            <EditItemTemplate>
                                                <div style='display: <%# Eval("Display")%>'>
                                                    <a href="#" onclick='<%# "grid_det.UpdateEdit("+Container.VisibleIndex+"); " %>'>U</a>
                                                    <a href="#" onclick='<%# "grid_det.CancelEdit("+Container.VisibleIndex+");"  %>'>C</a>
                                                </div>
                                            </EditItemTemplate>
                                        </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="No" FieldName="DocLineNo" VisibleIndex="1" PropertiesTextEdit-DisplayFormatInEditMode="true"
                                        Width="30" SortIndex="0" SortOrder="Ascending" ReadOnly="true" PropertiesTextEdit-ValidationSettings-EnableCustomValidation="true" FixedStyle="Left">
                                        <PropertiesTextEdit Style-Border-BorderWidth="0"></PropertiesTextEdit>
                                        <DataItemTemplate>
                                            <a href="#" style="text-decoration:none;"><%# Eval("DocLineNo") %></a>
                                            <div style="display: none">
                                                <dxe:ASPxTextBox ID="txt_LineId" runat="server" Text='<%#Eval("Id") %>'>
                                                </dxe:ASPxTextBox>
                                                <dxe:ASPxTextBox Width="100%" ID="txt_AcSource" ClientInstanceName="txt_AcSource"
                                                    ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("AcSource") %>'>
                                                </dxe:ASPxTextBox>
                                            </div>
                                        </DataItemTemplate>
                                        <EditItemTemplate>
                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_LineN" runat="server" ReadOnly="true" BackColor="Control"
                                                Text='<%# Eval("DocLineNo") %>'>
                                            </dxe:ASPxTextBox>
                                            <div style="display: none">
                                                <dxe:ASPxTextBox ID="txt_LineId" runat="server" Text='<%#Eval("Id") %>'>
                                                </dxe:ASPxTextBox>
                                                <dxe:ASPxTextBox Width="100%" ID="txt_AcSource" ClientInstanceName="txt_AcSource"
                                                    ReadOnly="true" BackColor="Control" runat="server" Text='<%# Bind("AcSource") %>'>
                                                </dxe:ASPxTextBox>
                                            </div>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn Caption="Item" FieldName="ChgCode" VisibleIndex="2" Width="80" FixedStyle="Left" PropertiesComboBox-ClientSideEvents-SelectedIndexChanged="ItemClickHandler">
                                        <EditItemTemplate>
                                            <dxe:ASPxComboBox ID="cmb_ChgCode" ClientInstanceName="cmb_ChgCode" runat="server" OnCustomJSProperties="cmb_ChgCode_CustomJSProperties"
                                                Width="100%" DropDownWidth="100%" DropDownStyle="DropDownList" DropDownButton-Visible="false" Text='<%# Bind("ChgCode") %>'
                                                DataSourceID="dsChgCode" ValueField="SequenceId" TextField="ChgcodeId" ValueType="System.Int32" TextFormatString="{0}"
                                                EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                                CallbackPageSize="100" ListBoxStyle-BackgroundImage-VerticalPosition="bottom">
                                                <Columns>
                                                    <dxe:ListBoxColumn Caption="ChgcodeId" FieldName="ChgcodeId" />
                                                    <dxe:ListBoxColumn Caption="Description" FieldName="ChgcodeDe" />
                                                    <dxe:ListBoxColumn Caption="Unit" FieldName="ChgUnit" />
                                                    <dxe:ListBoxColumn Caption="Gst Type" FieldName="GstTypeId" />
                                                    <dxe:ListBoxColumn Caption="Gst P" FieldName="GstP" />
                                                </Columns>
                                                <ClientSideEvents SelectedIndexChanged="ItemClickHandler" KeyUp="function(s, e) {ChangeBackColor(s)}" TextChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxComboBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="GL Code" FieldName="AcCode" VisibleIndex="3" Width="50">
                                        <PropertiesTextEdit ClientInstanceName="txt_det_AcCode"></PropertiesTextEdit>
                                        <EditItemTemplate>
                                                <dxe:ASPxTextBox Width="100%" ID="txt_det_AcCode" ReadOnly="true" BackColor="Control"
                                                    ClientInstanceName="txt_det_AcCode" runat="server" Text='<%# Bind("AcCode") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" TextChanged="function(s, e) {ChangeBackColor(s)}" />
                                                </dxe:ASPxTextBox>
                                            </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="DB/CR" FieldName="AcSource" VisibleIndex="3" Width="100%" Visible="false">
                                        <DataItemTemplate>
                                        </DataItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgDes1" VisibleIndex="4" Width="160">
                                        <EditItemTemplate>
                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Des1" ClientInstanceName="txt_det_Des1" runat="server" Text='<%# Bind("ChgDes1") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" TextChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="Unit" VisibleIndex="5" Width="55">
                                        <EditItemTemplate>
                                            <dxe:ASPxComboBox runat="server" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit" DataSourceID="dsUom" TextField="Description" ValueField="Code"
                                                Width="100%" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Text='<%# Bind("Unit") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" TextChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxComboBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="7" Width="60">
                                        <EditItemTemplate>
                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Qty" ClientInstanceName="spin_det_Qty"
                                                runat="server" Value='<%# Bind("Qty") %>' DisplayFormatString="0.000" DecimalPlaces="3">
                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutBillingAmt();
	                                                   }" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" NumberChanged="function(s, e) {ChangeBackColor(s)}" />
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Price" FieldName="Price" VisibleIndex="8" Width="50">
                                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_Price" ClientInstanceName="spin_det_Price"
                                                DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="false" runat="server" Text='<%# Bind("Price") %>'>
                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutBillingAmt();
	                                                   }" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" NumberChanged="function(s, e) {ChangeBackColor(s)}" />
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="Currency" VisibleIndex="6" Width="55">
                                        <EditItemTemplate>
                                            <dxe:ASPxComboBox runat="server" ID="txt_det_Currency" ClientInstanceName="txt_det_Currency" DataSourceID="dsCurrency" TextField="CurrencyId" ValueField="CurrencyId"
                                                Width="100%" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Text='<%# Bind("Currency") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" TextChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxComboBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="6" Width="70">
                                        <EditItemTemplate>
                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_ExRate" ClientInstanceName="spin_det_ExRate"
                                                runat="server" Value='<%# Bind("ExRate") %>' DisplayFormatString="0.000000" DecimalPlaces="6">
                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           PutBillingAmt();
	                                                   }" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" NumberChanged="function(s, e) {ChangeBackColor(s)}" />
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="OtherAmt" Width="70" VisibleIndex="9">
                                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                        </PropertiesTextEdit>
                                        <DataItemTemplate>
                                            <%#SafeValue.SafeDecimal(Eval("LocAmt"))-SafeValue.SafeDecimal(Eval("GstAmt")) %>
                                        </DataItemTemplate>
                                        <EditItemTemplate>
                                            <dxe:ASPxSpinEdit ID="spin_det_Amt" ClientInstanceName="spin_det_Amt" Increment="0" Width="100%" runat="server"
                                                DisplayFormatString="0.00" DecimalPlaces="2" ReadOnly="true" BackColor="Control" Value='<%#SafeValue.SafeDecimal(Eval("LocAmt"))-SafeValue.SafeDecimal(Eval("GstAmt")) %>'>
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="OtherAmt" FieldName="OtherAmt" Width="60" VisibleIndex="9">
                                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <dxe:ASPxSpinEdit ID="txt_OtherAmt" Increment="0" Width="100%" runat="server" Text='<%# Bind("OtherAmt")%>'
                                                DisplayFormatString="0.00" DecimalPlaces="2">
                                                <SpinButtons ShowIncrementButtons="false" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Gst" FieldName="GstType" VisibleIndex="10" Width="25">
                                        <EditItemTemplate>
                                            <dxe:ASPxComboBox runat="server" ID="txt_det_GstType" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" ClientInstanceName="txt_det_GstType" DropDownButton-Visible="false" Width="100%" Text='<%# Bind("GstType")%>'>
                                                <Items>
                                                    <dxe:ListEditItem Value="S" Text="S" />
                                                    <dxe:ListEditItem Value="Z" Text="Z" />
                                                    <dxe:ListEditItem Value="E" Text="E" />
                                                </Items>
                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                       PutGst(txt_det_GstType,spin_det_GstP);
                                                                       PutBillingAmt();
	                                                               }" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" TextChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxComboBox>
                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstP" ClientInstanceName="spin_det_GstP"
                                                ReadOnly="true" BackColor="Control" runat="server" Value='<%# Bind("Gst") %>' ClientVisible="false"
                                                DisplayFormatString="0.00">
                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                                    PutBillingAmt();
	                                                   }" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" NumberChanged="function(s, e) {ChangeBackColor(s)}" />
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="GstAmt" FieldName="GstAmt" VisibleIndex="10" Width="55">
                                        <EditItemTemplate>
                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_GstAmt" ClientInstanceName="spin_det_GstAmt"
                                                DisplayFormatString="0.00" runat="server" Text='<%# Bind("GstAmt") %>' ReadOnly="true"
                                                BackColor="Control">
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Total" FieldName="LocAmt" VisibleIndex="11" Width="60">
                                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_DocAmt" ClientInstanceName="spin_det_DocAmt"
                                                DisplayFormatString="0.00" ReadOnly="true" BackColor="Control" runat="server" ClientVisible="false"
                                                Text='<%# Bind("DocAmt") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>
                                            <dxe:ASPxSpinEdit Increment="0" Width="100%" ID="spin_det_LocAmt" ClientInstanceName="spin_det_LocAmt"
                                                DisplayFormatString="0.00" ReadOnly="true" BackColor="Control" runat="server"
                                                Text='<%# Bind("LocAmt") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                                <SpinButtons ShowIncrementButtons="false" />
                                            </dxe:ASPxSpinEdit>


                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Split" FieldName="SplitType" VisibleIndex="11" Width="45">
                                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                        </PropertiesTextEdit>
                                        <EditItemTemplate>
                                            <dxe:ASPxComboBox ID="cbo_SplitType" runat="server" DropDownButton-Visible="false" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Text='<%# Bind("SplitType")%>' Width="100%">
                                                <Items>
                                                    <dxe:ListEditItem Text="Set" Value="Set" />
                                                    <dxe:ListEditItem Text="WtM3" Value="WtM3" />
                                                </Items>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxComboBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Ref No" FieldName="MastRefNo" VisibleIndex="13" Width="110">
                                        <EditItemTemplate>
                                            <dxe:ASPxButtonEdit ID="btn_MastRefNo" ReadOnly="true" BackColor="Control" ClientInstanceName="btn_MastRefNo" runat="server" Text='<%# Bind("MastRefNo") %>' Width="100%">
                                                <Buttons>
                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s,e){
                                                                                 PopupRefNo(btn_MastRefNo,txt_DetMastType);
                                                                                }" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxButtonEdit>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobRefNo" VisibleIndex="13" Width="60">
                                        <EditItemTemplate>
                                            <dxe:ASPxButtonEdit ID="btn_JobRefNo" ReadOnly="true" BackColor="Control" ClientInstanceName="btn_JobRefNo" runat="server" Text='<%# Bind("JobRefNo") %>' Width="100%">
                                                <Buttons>
                                                    <dxe:EditButton Text=".."></dxe:EditButton>
                                                </Buttons>
                                                <ClientSideEvents ButtonClick="function(s,e){
                                                                               PopupJobNo(btn_MastRefNo.GetText(),txt_DetMastType.GetText(),btn_JobRefNo);
                                                                                }" />
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxButtonEdit>
                                            <dxe:ASPxTextBox ID="txt_DetMastType" runat="server" BackColor="Control" ClientInstanceName="txt_DetMastType" ReadOnly="true" ClientVisible="false" Text='<%# Bind("MastType")%>' Width="100%">
                                            </dxe:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="ChgDes2" VisibleIndex="13" Width="150">
                                        <EditItemTemplate>
                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks2" ClientInstanceName="txt_det_Remarks2" runat="server" Enabled="true" Text='<%# Bind("ChgDes2") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxTextBox>
                                            <dxe:ASPxTextBox Width="100%" ID="txt_det_Remarks3" ClientInstanceName="txt_det_Remarks3" runat="server" ClientVisible="false" Text='<%# Bind("ChgDes3") %>'>
                                                <ClientSideEvents KeyUp="function(s, e) {ChangeBackColor(s)}" ValueChanged="function(s, e) {ChangeBackColor(s)}" />
                                            </dxe:ASPxTextBox>
                                        </EditItemTemplate>
                                    </dxwgv:GridViewDataTextColumn>
                                </Columns>
                                <ClientSideEvents RowClick='function RowClick(s,e) { RowEdit(s,e)}' />
                                <TotalSummary>
                                    <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                                    <dxwgv:ASPxSummaryItem FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                    <dxwgv:ASPxSummaryItem FieldName="GstAmt" SummaryType="Sum" DisplayFormat="{0:#,##0.00}" />
                                </TotalSummary>
                            </dxwgv:ASPxGridView>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
        
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="900" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
