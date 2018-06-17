<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="OrderEdit.aspx.cs" Inherits="Modules_Freight_Job_OrderEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
<%--    <link rel="stylesheet" href="/Style/bootstrap/bootstrap.min.css" />--%>
    <%--<link rel="stylesheet" href="/Style/bootstrap/bootstrap-theme.min.css" />--%>
    <!-- jQuery文件。务必在bootstrap.min.js 之前引入 -->
    <script src="/Style/bootstrap/jquery.min.js"></script>
    <!-- 最新的 Bootstrap 核心 JavaScript 文件 -->
    <script src="/Style/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/jquery.js"></script>
    <script type="text/javascript">
        function OnCallBack(v) {
            if (v != null) {
                window.location = 'OrderEdit.aspx?no=' + v;
            }
        }
        function OnClickCallBack(v) {
            if (v != null) {
                grd_Stock.Refresh();
            }
        }
        function OnExcelCallBack() {

        }
        function PopupExcel() {
            popubCtr.SetHeaderText('Import Cargo and Select a Excel/从Excel批量导入货物');
            popubCtr.SetContentUrl('/PagesCfs/XW/Dongji/UploadCargo.aspx?no=' + txt_cargo_id.GetText());
            popubCtr.Show();
        }
        function GoList() {
            parent.navTab.openTab('订单列表', "/PagesCfs/XW/Dongji/OrderList.aspx", { title: '订单列表', fresh: false, external: true });
        }
        function AddNew(masterId) {
            parent.navTab.openTab('新单', "/PagesCfs/XW/Dongji/OrderEdit.aspx?no=" + masterId, { title: '新单', fresh: false, external: true });
        }
        function CheckConsignee() {
            console.log(ckb_IsHold.GetChecked());
            if (ckb_IsHold.GetChecked()) {
                document.getElementById("receiver").style.display = "none";
                document.getElementById("contact").style.display = "none";
                document.getElementById("address").style.display = "none";
            } else {
                document.getElementById("receiver").style.display = "table-row";
                document.getElementById("contact").style.display = "table-row";
                document.getElementById("address").style.display = "table-row";
            }
        }
        

        function CheckResponsible() {
            var vm = cmb_Responsible.GetText();
            console.log(vm);
            if (vm == "公司") {
                cmb_Duty_Payment.SetText("税新加坡付");
                cmb_Duty_Payment.SetEnabled(false) ;
            }
            if (vm == "个人") {
                cmb_Duty_Payment.SetEnabled(true);
            }
        }
        function CheckIncoTerm() {
            var vm = cmb_Incoterm.GetText();
            console.log(vm);
            if (vm == "CIF" || vm == "CFR") {
                spin_MiscFee.SetText(0.00);
                spin_MiscFee.SetEnabled(false);
            }
            else {
                spin_MiscFee.SetEnabled(true);
            }
        }
        function CheckInput() {
            console.log(ckb_Input.GetChecked());
            if (ckb_Input.GetChecked()) {
                document.getElementById("txt_party").style.display = "block";
                document.getElementById("ddl_party").style.display = "none";
            } else {
                document.getElementById("ddl_party").style.display = "block";
                document.getElementById("txt_party").style.display = "none";
            }
        }
        function CheckPrepaidInd() {
            console.log(ckb_Prepaid_Ind.GetChecked());
            if (ckb_Prepaid_Ind.GetChecked()) {
                spin_Collect_Amount2.SetText(0.00);
                spin_Collect_Amount2.SetEnabled(false);
            } else {
                spin_Collect_Amount2.SetEnabled(true);
            }
        }
        window.onload = function () {
            CheckConsignee();
            CheckResponsible();
        }
        function RowClickHandlers(s, e) {
            DropDownEdit_Party.SetText(gridParty.cpName[e.visibleIndex]);
            txt_Party.SetText(gridParty.cpName[e.visibleIndex]);
            txt_Party1.SetText(gridParty.cpName[e.visibleIndex]);
            if (cmb_Responsible.GetText() == "个人") {
                txt_ConsigneeRemark.SetText(gridParty.cpCrNo[e.visibleIndex]);
                txt_ConsigneeRemark1.SetText(gridParty.cpCrNo[e.visibleIndex]);
            }
            if (cmb_Responsible.GetText() == "公司") {
                txt_ConsigneeEmail.SetText(gridParty.cpCrNo[e.visibleIndex]);
                txt_ConsigneeEmail1.SetText(gridParty.cpCrNo[e.visibleIndex]);
            }
            txt_Email1.SetText(gridParty.cpEmail1[e.visibleIndex]);
            txt_Email1_1.SetText(gridParty.cpEmail1[e.visibleIndex]);
            txt_Email2.SetText(gridParty.cpEmail2[e.visibleIndex]);
            txt_Email2_1.SetText(gridParty.cpEmail2[e.visibleIndex]);
            txt_Tel1.SetText(gridParty.cpTel1[e.visibleIndex]);
            txt_Tel1_1.SetText(gridParty.cpTel1[e.visibleIndex]);
            txt_Tel2.SetText(gridParty.cpTel2[e.visibleIndex]);
            txt_Tel2_1.SetText(gridParty.cpTel2[e.visibleIndex]);
            memo_Desc1.SetText(gridParty.cpAddress[e.visibleIndex]);
            memo_Desc1_1.SetText(gridParty.cpAddress[e.visibleIndex]);
            DropDownEdit_Party.HideDropDown();
            
        }
        function AfterUploadExcel() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
        }
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        $(function () {
            loading.hide();
        })
        var config = {
            timeout: 0,
            gridview: 'grd_Det',
        }
        function save_order() {
            //loading.show();
            var res = cmb_Responsible.GetText();
            var ic = txt_ConsigneeRemark.GetText();
            var ic1 = txt_ConsigneeRemark1.GetText();
            var uen = txt_ConsigneeEmail.GetText();
            var uen1 = txt_ConsigneeEmail1.GetText();
            var email1 = txt_Email1.GetText();
            var email1_1 = txt_Email1_1.GetText();
            var email2 = txt_Email2.GetText();
            var email2_1 = txt_Email2_1.GetText();
            var tel1 = txt_Tel1.GetText();
            var tel1_1 = txt_Tel1_1.GetText();
            var tel2 = txt_Tel2.GetText();
            var tel2_1 = txt_Tel2_1.GetText();
            var mobile1 = txt_Mobile1.GetText();
            var mobile1_1 = txt_Mobile1_1.GetText();
            var mobile2 = txt_Mobile2.GetText();
            var mobile2_1 = txt_Mobile2_1.GetText();
            var address = memo_Desc1.GetText();
            var address_1 = memo_Desc1_1.GetText();
            //setTimeout(function () {
                if (email1 != email1_1 || email2 != email2_1 || tel1 != tel1_1 || tel2 != tel2_1 ||
                    mobile1 != mobile1_1 || mobile2 != mobile2_1 || address != address_1) {
                    grd_Det.GetValuesOnCustomCallback('Vali', save_order_callBack);

                }
                else if (ic != ic1 || uen != uen1) {
                    console.log(ic);
                    console.log(ic1);
                    console.log(uen);
                    console.log(uen1);
                    grd_Det.GetValuesOnCustomCallback('ValiUen', save_order_callBack);
                }
                else {
                    console.log('Save==============');
                    grd_Det.GetValuesOnCustomCallback('Save', OnCallBack);
                }
            //}, config.timeout);
        }
        function save_order_callBack(v) {
            if (v == "OK") {
                if (confirm("Confirm Update Consignee Information?")) {
                    grd_Det.GetValuesOnCustomCallback('UpdateParty', OnCallBack);
                }
            }
            else {
                grd_Det.GetValuesOnCustomCallback('Save', OnCallBack);
            }
        }
        function update_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grd_Stock.GetValuesOnCustomCallback('UpdateInline_' + rowIndex, update_inline_callBack);
            }, config.timeout);
        }
        function update_inline_callBack(v) {
            var miscFee = parseFloat(spin_MiscFee.GetText());
            var stockAmt = parseFloat(v);
            var total = FormatNumber(miscFee + stockAmt, 3);
            console.log(total);
            lbl_total.SetText(total);
            grd_Stock.CancelEdit();
        }
    </script>
    <script type="text/javascript">
        var isUpload = false;
    </script>
</head>
<body>
   <form id="form1" runat="server">
       <wilson:DataSource ID="dsJobDet" runat="server" ObjectSpace="C2.Manager.ORManager"
           TypeName="C2.JobHouse" KeyMember="Id" FilterExpression="1=0" />
       <wilson:DataSource ID="dsJobStock" runat="server" ObjectSpace="C2.Manager.ORManager"
           TypeName="C2.JobStock" KeyMember="Id" />
       <wilson:DataSource ID="dsPackageType" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXUom"
           KeyMember="Id" FilterExpression="CodeType='2'" />
       <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
           TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
        <div>
            <table>
                <tr>
                    <td>编号
                    </td>
                    <td>
                         <div style="display: none">
                            <dxe:ASPxLabel ID="lbl_t" ClientInstanceName="lbl_t" runat="server">
                            </dxe:ASPxLabel>
                        </div>
                        <dxe:ASPxTextBox ID="txt_No" Width="150" runat="server" ClientInstanceName="txt_No">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="重新加载" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                        window.location='OrderEdit.aspx?no='+txt_No.GetText();
                    }" />
                        </dxe:ASPxButton>&nbsp;&nbsp;
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton2" Width="100" runat="server" Text="返回列表" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                           GoList();
                        }" />
                        </dxe:ASPxButton>&nbsp;&nbsp;
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="重新下单" 
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                   AddNew('0');
                                    }" />
                        </dxe:ASPxButton>&nbsp;&nbsp;
                    </td>
                   <%-- <td>
                        <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="保存到Excel" OnClick="btn_Export_Click">
                        </dxe:ASPxButton>
                    </td>--%>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grd_Det" ClientInstanceName="grd_Det" runat="server" DataSourceID="dsJobDet"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="false"
                OnInit="grd_Det_Init" OnInitNewRow="grd_Det_InitNewRow" OnRowInserting="grd_Det_RowInserting"
                OnRowUpdating="grd_Det_RowUpdating" OnRowDeleting="grd_Det_RowDeleting" OnHtmlEditFormCreated="grd_Det_HtmlEditFormCreated" OnCustomDataCallback="grd_Det_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                    <EditForm>
                                                        <div style="display: none">
                                                            <dxe:ASPxTextBox ID="txt_cargo_id" ClientInstanceName="txt_cargo_id" Width="170" ReadOnly="true"
                                                                runat="server" Text='<%# Eval("Id") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </div>

                                                        <table style="width: 980px">
                                                            <tr>
                                                                <td width="90%"></td>
                                                                <td width="110">
                                                                    <dxe:ASPxButton ID="ASPxButton4" class="btn btn-primary" Width="100" runat="server" Text="Save&保存" Enabled='<%# SafeValue.SafeString(Eval("CargoStatus"))=="COMPLETED"?false:true %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false" ClientSideEvents-Click='<%# "function(s) { save_order() }"  %>'>
                                                                        <%--                                                                    <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.PerformCallback('UpdateClose');grd_Det.Refresh();
                                                                    }" />--%>
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td width="110">
                                                                    <dxe:ASPxButton ID="ASPxButton5" Width="60" runat="server" Text="Close" AutoPostBack="false"
                                                                        UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.CancelEdit();
                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table class="table table-bordered" style="width: 980px">
                                                            <tr>
                                                                <td colspan="8">
                                                                    <table>
                                                                        <tr>
                                                                            <td>唛头号：</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_ExpBkgN" Width="100%"
                                                                                    runat="server" Text='<%# Eval("BookingNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>单号：</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_ExJobOrder" Width="100%"
                                                                                    runat="server" Text='<%# Eval("DoNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td style="display: none">柜号：</td>
                                                                            <td style="display: none">
                                                                                <dxe:ASPxTextBox ID="txt_ContNo" Width="100%"
                                                                                    runat="server" Text='<%# Eval("ContNo") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                            </td>
                                                                            <td>当前状态:</td>
                                                                            <td style="text-underline-position: below; display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">
                                                                                <dxe:ASPxLabel ID="lbl_Status" runat="server"></dxe:ASPxLabel>
                                                                                &nbsp;&nbsp;

                                                <div style="display: none">
                                                    <dxe:ASPxButton ID="btn_Status" ClientInstanceName="btn_Status" Width="100" runat="server" Text="下单">
                                                        <ClientSideEvents Click="function(s,e) {
                                                        if(confirm('确定对该订单做'+btn_Status.GetText()+'操作吗?')){
                                                                    grd_Det.GetValuesOnCustomCallback('UpdateStatus',OnCallBack);
                                                                    }}" />
                                                    </dxe:ASPxButton>
                                                    <dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="80" ID="cmb_CargoStatus"
                                                        Value='<%# Bind("CargoStatus")%>'>
                                                        <Items>
                                                            <dxe:ListEditItem Text="待确认" Value="USE" />
                                                            <dxe:ListEditItem Text="已下单" Value="ORDER" />
                                                            <dxe:ListEditItem Text="已入库" Value="IN" />
                                                            <dxe:ListEditItem Text="已排库" Value="PICKED" />
                                                            <dxe:ListEditItem Text="已出库" Value="OUT" />
                                                            <dxe:ListEditItem Text="已装船" Value="SHIPMENT" />
                                                            <dxe:ListEditItem Text="已出港" Value="DEPARTURE" />
                                                        </Items>
                                                    </dxe:ASPxComboBox>
                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="180px"><font size="1" color="red">*</font>承运人/SHIPPER:</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxTextBox ID="txt_Carrier" runat="server" Width="100%" Text='<%# Bind("ShipperInfo") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>货主/CONSIGNEE:</td>
                                                                <td colspan="5">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div id="ddl_party">
                                                                                    <dxe:ASPxDropDownEdit ID="DropDownEdit_Party" runat="server" ClientInstanceName="DropDownEdit_Party"
                                                                                        EnableAnimation="False" Width="380px" AllowUserInput="False" Text='<%# Bind("ConsigneeInfo") %>'>
                                                                                        <DropDownWindowTemplate>
                                                                                            <dxwgv:ASPxGridView ID="gridParty" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridParty"
                                                                                                Width="100%" DataSourceID="dsCustomerMast" KeyFieldName="SequenceId" OnCustomJSProperties="gridParty_CustomJSProperties">
                                                                                                <Settings ShowFilterRow="true" />
                                                                                                <Columns>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="PartyId" VisibleIndex="0" Width="50px">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                    <dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="0" Width="100%">
                                                                                                    </dxwgv:GridViewDataTextColumn>
                                                                                                </Columns>
                                                                                                <ClientSideEvents RowClick="RowClickHandlers" />
                                                                                            </dxwgv:ASPxGridView>
                                                                                        </DropDownWindowTemplate>
                                                                                    </dxe:ASPxDropDownEdit>
                                                                                </div>
                                                                                <div id="txt_party" style="display: none">
                                                                                    <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txt_Party" runat="server" Width="380px" Text='<%# Bind("ConsigneeInfo") %>'>
                                                                                    </dxe:ASPxTextBox>

                                                                                </div>
                                                                                <div style="display: none">
                                                                                    <input type="text" id="partyId" value="<%# Eval("ConsigneeInfo") %>" />
                                                                                    <dxe:ASPxTextBox ID="txt_Party1" ClientInstanceName="txt_Party1" runat="server" Width="380px" Text='<%# Bind("ConsigneeInfo") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <dxe:ASPxCheckBox ID="ckb_Input" runat="server" Text="是否手动输入" ClientInstanceName="ckb_Input">
                                                                                    <ClientSideEvents CheckedChanged="function(s,e){
                                        CheckInput();
                                        }" />
                                                                                </dxe:ASPxCheckBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                                <td><font size="1" color="red">*</font>个人/公司</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ClientInstanceName="cmb_Responsible" ID="cmb_Responsible" Width="100%" runat="server" Value='<%# Bind("Responsible") %>' AutoPostBack="false">
                                                                        <ClientSideEvents TextChanged="function(s,e){
                                             CheckResponsible();
                                            }" />
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="个人" Value="PERSON" />
                                                                            <dxe:ListEditItem Text="公司" Value="COMPANY" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="person">
                                                                <td><font size="1" color="red">*</font>货主信息/CONSIGNEE UEN/IC</td>
                                                                <td width="40px">IC:</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxTextBox ID="txt_ConsigneeRemark" ClientInstanceName="txt_ConsigneeRemark" runat="server" Width="100%" Text='<%# Bind("ConsigneeRemark") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <input type="text" id="ic" value="<%# Eval("ConsigneeRemark") %>" />
                                                                        <dxe:ASPxTextBox ID="txt_ConsigneeRemark1" ClientInstanceName="txt_ConsigneeRemark1" runat="server" Width="380px" Text='<%# Bind("ConsigneeRemark") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </div>
                                                                </td>
                                                                <td width="40px">UEN:</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxTextBox ID="txt_ConsigneeEmail" ClientInstanceName="txt_ConsigneeEmail" runat="server" Width="100%" Text='<%# Bind("ConsigneeEmail") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxTextBox ID="txt_ConsigneeEmail1" ClientInstanceName="txt_ConsigneeEmail1" runat="server" Width="380px" Text='<%# Bind("ConsigneeEmail") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>邮箱地址/EMAIL：</td>
                                                                <td>EMAIL 1:</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxTextBox ID="txt_Email1" ClientInstanceName="txt_Email1" runat="server" Width="100%" Text='<%# Bind("Email1") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxTextBox ID="txt_Email1_1" ClientInstanceName="txt_Email1_1" runat="server" Width="380px" Text='<%# Bind("Email1") %>'>
                                                                        </dxe:ASPxTextBox>

                                                                    </div>
                                                                </td>
                                                                <td>EMAIL 2:</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxTextBox ID="txt_Email2" ClientInstanceName="txt_Email2" runat="server" Width="100%" Text='<%# Bind("Email2") %>'>
                                                                    </dxe:ASPxTextBox>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxTextBox ID="txt_Email2_1" ClientInstanceName="txt_Email2_1" runat="server" Width="380px" Text='<%# Bind("Email2") %>'>
                                                                        </dxe:ASPxTextBox>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>联系电话/CONTACT：<br />
                                                                </td>
                                                                <td colspan="7">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td width="100px">TEL 1/座机1:</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Tel1" ClientInstanceName="txt_Tel1" runat="server" Width="100%" Text='<%# Bind("Tel1") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Tel1_1" ClientInstanceName="txt_Tel1_1" runat="server" Width="380px" Text='<%# Bind("Tel1") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td width="100px">TEL 2/座机2:</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Tel2" ClientInstanceName="txt_Tel2" runat="server" Width="100%" Text='<%# Bind("Tel2") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Tel2_1" ClientInstanceName="txt_Tel2_1" runat="server" Width="380px" Text='<%# Bind("Tel2") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td width="110px">MOBILE 1/手机1：</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Mobile1" ClientInstanceName="txt_Mobile1" runat="server" Width="100%" Text='<%# Bind("Mobile1") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Mobile1_1" ClientInstanceName="txt_Mobile1_1" runat="server" Width="380px" Text='<%# Bind("Mobile1") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                            <td width="110px">MOBILE 2/手机2:</td>
                                                                            <td>
                                                                                <dxe:ASPxTextBox ID="txt_Mobile2" ClientInstanceName="txt_Mobile2" runat="server" Width="100%" Text='<%# Bind("Mobile2") %>'>
                                                                                </dxe:ASPxTextBox>
                                                                                <div style="display: none">
                                                                                    <dxe:ASPxTextBox ID="txt_Mobile2_1" ClientInstanceName="txt_Mobile2_1" runat="server" Width="380px" Text='<%# Bind("Mobile2") %>'>
                                                                                    </dxe:ASPxTextBox>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><font size="1" color="red">*</font>地址/ADDRESS：</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxMemo ID="memo_Desc1" runat="server" ClientInstanceName="memo_Desc1"
                                                                        Width="100%" Rows="2" Text='<%# Bind("Desc1") %>'>
                                                                    </dxe:ASPxMemo>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxMemo ID="memo_Desc1_1" ClientInstanceName="memo_Desc1_1" runat="server" Width="100%" Rows="2" Text='<%# Bind("Desc1") %>'>
                                                                        </dxe:ASPxMemo>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">货主/CONSIGNEE 是 收货人/RECEIVER PARTY ?</td>
                                                                <td colspan="6">
                                                                    <dxe:ASPxCheckBox ID="ckb_IsHold" runat="server" ClientInstanceName="ckb_IsHold" Checked='<%# SafeValue.SafeBool(Eval("IsHold"),false) %>'>
                                                                        <ClientSideEvents CheckedChanged="function(s,e){
                                        CheckConsignee();
                                        }" />
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="receiver">
                                                                <td><font size="1" color="red">*</font>收货人/RECEIVER PARTY:</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxTextBox ID="txt_ClientId" ClientInstanceName="txt_ClientId" runat="server" Text='<%# Bind("ClientId") %>' Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="contact">
                                                                <td><font size="1" color="red">*</font>联系电话/CONTACT NUMBER</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxTextBox ID="txt_ClientEmail" runat="server" Text='<%# Bind("ClientEmail") %>' Width="100%">
                                                                    </dxe:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="address">
                                                                <td><font size="1" color="red">*</font>送货地址/DELIVERY ADDRESS：</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxMemo ID="memo_Remark1" runat="server"
                                                                        Width="100%" Rows="2" Text='<%# Bind("Remark1") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-row" %>">
                                                                <td>付款/Payable：</td>
                                                                <td width="150">中国付/Freight prepaid:</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxCheckBox ID="ckb_Prepaid_Ind" ClientInstanceName="ckb_Prepaid_Ind" runat="server" Checked='<%# SafeValue.SafeString(Eval("PrepaidInd"))=="YES"?true:false %>'>
                                                                        <ClientSideEvents CheckedChanged="function(s,e){
                                            CheckPrepaidInd();
                                            }" />
                                                                    </dxe:ASPxCheckBox>
                                                                </td>
                                                                <td width="200">代收/COLLECT ON BEHALF RMB：</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_Collect_Amount1" ClientInstanceName="spin_Collect_Amount1" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="100" Value='<%# Bind("CollectAmount1") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td width="200">代收/COLLECT ON BEHALF SGD：</td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_Collect_Amount2" ClientInstanceName="spin_Collect_Amount2" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="100" Value='<%# Bind("CollectAmount2") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                            <tr style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-row" %>">
                                                                <td>GST/税</td>
                                                                <td colspan="2">
                                                                    <dxe:ASPxComboBox ID="cmb_Duty_Payment" ClientInstanceName="cmb_Duty_Payment" Width="100%" runat="server" Value='<%# Bind("DutyPayment") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="税中国付" Value="DUTY PAID" />
                                                                            <dxe:ListEditItem Text="税新加坡付" Value="DUTY UNPAID" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee1" ClientInstanceName="spin_OtherFee1" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("GstFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>是否单独清关/Permit Fee:</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_IsBill" ValueField='<%# Bind("IsBill") %>' runat="server" Width="100">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="是" Value="YES" />
                                                                            <dxe:ListEditItem Text="否" Value="NO" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_CheckIn" ClientInstanceName="cmb_CheckIn" Width="100" runat="server" Value='<%# Bind("PermitBy") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="中国付" Value="PAID" Selected="true" />
                                                                            <dxe:ListEditItem Text="新加坡付" Value="UNPAID" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>

                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee2" ClientInstanceName="spin_OtherFee2" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("PermitFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>

                                                            </tr>
                                                            <tr style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-row" %>">
                                                                <td>货运条款/INCO-TERM:</td>
                                                                <td colspan="3">
                                                                    <dxe:ASPxComboBox ID="cmb_Incoterm" ClientInstanceName="cmb_Incoterm" Width="100" runat="server" Value='<%# Bind("Incoterm") %>'>
                                                                        <ClientSideEvents TextChanged="function(s,e){
                                            CheckIncoTerm();
                                            }" />
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="EX-WORK" Value="EX-WORK " />
                                                                            <dxe:ListEditItem Text="FOB" Value="FOB" />
                                                                            <dxe:ListEditItem Text="CIF" Value="CIF" />
                                                                            <dxe:ListEditItem Text="CFR" Value="CFR" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td style="display: none">海运费/OCEAN FREIGHT：
                                                                </td>
                                                                <td style="display: none">
                                                                    <dxe:ASPxSpinEdit ID="spin_MiscFee" ClientInstanceName="spin_MiscFee" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" DecimalPlaces="2" Width="100" Value='<%# Bind("Ocean_Freight") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">手续费/Handling Fee</td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">
                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee3" ClientInstanceName="spin_OtherFee3" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("HandlingFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">其他/Other Fee</td>
                                                                <td style="display: <%# SafeValue.SafeString(Eval("Role"))=="Client"?"none":"table-cell" %>">
                                                                    <dxe:ASPxSpinEdit ID="spin_OtherFee4" ClientInstanceName="spin_OtherFee4" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="0" Width="100" Value='<%# Bind("OtherFee") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td>备注</td>
                                                                <td colspan="7">
                                                                    <dxe:ASPxMemo ID="memo_Remark2" ReadOnly="false" runat="server"
                                                                        Width="100%" Rows="2" Text='<%# Bind("Remark2") %>'>
                                                                    </dxe:ASPxMemo>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <dxe:ASPxButton ID="ASPxButton3" Width="80" runat="server" Text="添加货物" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                    grd_Stock.AddNewRow();
                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxButton ID="ASPxButton7" Width="90" runat="server" Text="从Excel导入货物" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                            isUpload=true;
                                    PopupExcel();
                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <td>总箱数
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_TotQty" ClientInstanceName="spin_TotQty" runat="server"
                                                                        ReadOnly="false" NumberType="Integer" Increment="2" Width="60" Value='<%# Bind("Qty") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="txt_PackType" ClientInstanceName="txt_PackType" Width="80" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("PackTypeOrig") %>' DropDownStyle="DropDown">
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="CTN/箱" Value="CTN" />
                                                                            <dxe:ListEditItem Text="PKG/件" Value="PKG" />
                                                                            <dxe:ListEditItem Text="BAG/包" Value="BAG" />
                                                                            <dxe:ListEditItem Text="PAL/卡板" Value="PAL" />
                                                                            <dxe:ListEditItem Text="TON/吨" Value="TON" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td>总量(KG)
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_Weight" ClientInstanceName="spin_Weight" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="80" Value='<%# Bind("Weight") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>体积(CBM/M3)
                                                                </td>
                                                                <td>
                                                                    <dxe:ASPxSpinEdit ID="spin_M3" ClientInstanceName="spin_M3" runat="server"
                                                                        ReadOnly="false" NumberType="Float" Increment="2" Width="80" Value='<%# Bind("Volume") %>'>
                                                                        <SpinButtons ShowIncrementButtons="false" />
                                                                    </dxe:ASPxSpinEdit>
                                                                </td>
                                                                <td>货币/Currency</td>
                                                                <td>
                                                                    <dxe:ASPxComboBox ID="cmb_Fee1CurrId" Width="60" runat="server" Value='<%# Bind("Currency") %>'>
                                                                        <Items>
                                                                            <dxe:ListEditItem Text="SGD" Value="SGD" Selected="true" />
                                                                            <dxe:ListEditItem Text="RMB" Value="RMB" />
                                                                            <dxe:ListEditItem Text="USD" Value="USD" />
                                                                        </Items>
                                                                    </dxe:ASPxComboBox>
                                                                </td>
                                                                <td width="110">
                                                                    <dxe:ASPxButton ID="ASPxButton1" Width="80" runat="server" Text="修改" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                        AutoPostBack="false" UseSubmitBehavior="false">
                                                                        <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.GetValuesOnCustomCallback('Update',OnCallBack);
                                                                    }" />
                                                                    </dxe:ASPxButton>
                                                                </td>
                                                                <%--  <td width="110">
                                                                <dxe:ASPxButton ID="ASPxButton6" Width="100" runat="server" Text="刷新" Enabled='<%# SafeValue.SafeString(Eval("Id"),"")!="" %>'
                                                                    AutoPostBack="false" UseSubmitBehavior="false">
                                                                    <ClientSideEvents Click="function(s,e) {
                                                                    grd_Det.GetValuesOnCustomCallback('Update',OnCallBack);
                                                                    }" />
                                                                </dxe:ASPxButton>
                                                            </td>--%>
                                                            </tr>
                                                        </table>
                                                        <dxwgv:ASPxGridView ID="grd_Stock" ClientInstanceName="grd_Stock" runat="server" DataSourceID="dsJobStock"
                                                            KeyFieldName="Id" Width="980" OnBeforePerformDataSelect="grd_Stock_BeforePerformDataSelect" OnCustomDataCallback="grd_Stock_CustomDataCallback"
                                                            OnInit="grd_Stock_Init" OnInitNewRow="grd_Stock_InitNewRow" OnRowInserting="grd_Stock_RowInserting" OnRowUpdating="grd_Stock_RowUpdating"
                                                            OnRowDeleting="grd_Stock_RowDeleting">
                                                            <SettingsBehavior ConfirmDelete="True" />
                                                            <SettingsPager Mode="ShowAllRecords">
                                                            </SettingsPager>
                                                            <SettingsEditing Mode="Inline" />
                                                            <Settings />
                                                            <Columns>
                                                                <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="0">
                                                                    <DataItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton17" runat="server" Text="修改" Width="40" AutoPostBack="false"
                                                                                        ClientSideEvents-Click='<%# "function(s) { grd_Stock.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton18" runat="server"
                                                                                        Text="删除" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Stock.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </DataItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton19" runat="server" Text="保存" Width="40" AutoPostBack="false"
                                                                                        ClientSideEvents-Click='<%# "function(s) {update_stock_inline("+Container.VisibleIndex+") }"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                                <td>
                                                                                    <dxe:ASPxButton ID="ASPxButton20" runat="server" Text="取消" Width="40" AutoPostBack="false"
                                                                                        ClientSideEvents-Click='<%# "function(s) { grd_Stock.CancelEdit() }"  %>'>
                                                                                    </dxe:ASPxButton>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </EditItemTemplate>
                                                                </dxwgv:GridViewDataColumn>
                                                                <dxwgv:GridViewDataColumn FieldName="SortIndex" Caption="序号" VisibleIndex="1" Width="20"
                                                                    SortIndex="0" SortOrder="Ascending">
                                                                </dxwgv:GridViewDataColumn>
                                                                <dx:GridViewBandColumn Caption="" VisibleIndex="1">
                                                                    <Columns>
                                                                        <dxwgv:GridViewDataColumn FieldName="Marks1" Caption="COMMODITY" VisibleIndex="1" Width="100">
                                                                        </dxwgv:GridViewDataColumn>
                                                                        <dxwgv:GridViewDataColumn FieldName="Marks2" Caption="品 名" VisibleIndex="1" Width="100">
                                                                        </dxwgv:GridViewDataColumn>
                                                                    </Columns>
                                                                </dx:GridViewBandColumn>
                                                                <dxwgv:GridViewDataColumn FieldName="Uom1" Caption="单位" VisibleIndex="1" Width="100">
                                                                    <DataItemTemplate>
                                                                        <%# ShowUom(Eval("Uom1")) %>
                                                                    </DataItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <dxe:ASPxComboBox ID="txt_Uom1" ClientInstanceName="txt_Uom1" Width="100" TextField="Code" runat="server" ValueField="Code" ValueType="System.String" Value='<%# Bind("Uom1") %>' DropDownStyle="DropDown">
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="CTN/箱" Value="CTN" />
                                                                                <dxe:ListEditItem Text="PKG/件" Value="PKG" />
                                                                                <dxe:ListEditItem Text="BAG/包" Value="BAG" />
                                                                                <dxe:ListEditItem Text="PAL/卡板" Value="PAL" />
                                                                                <dxe:ListEditItem Text="TON/吨" Value="TON" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </EditItemTemplate>
                                                                </dxwgv:GridViewDataColumn>
                                                                <dxwgv:GridViewDataSpinEditColumn FieldName="Qty2" Caption="数量(PCS)" VisibleIndex="1" Width="100">
                                                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer"></PropertiesSpinEdit>
                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                                <dxwgv:GridViewDataColumn FieldName="Uom2" Caption="货币" VisibleIndex="1" Width="30" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <dxe:ASPxComboBox ID="cmb_Incoterm" Width="100%" runat="server" Value='<%# Bind("Uom2") %>'>
                                                                            <Items>
                                                                                <dxe:ListEditItem Text="SGD" Value="SGD " />
                                                                                <dxe:ListEditItem Text="RMB" Value="RMB" />
                                                                                <dxe:ListEditItem Text="USD" Value="USD" />
                                                                            </Items>
                                                                        </dxe:ASPxComboBox>
                                                                    </EditItemTemplate>
                                                                </dxwgv:GridViewDataColumn>
                                                                <dxwgv:GridViewDataSpinEditColumn FieldName="Price1" Caption="单价" VisibleIndex="2" Width="100">
                                                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="3" NumberType="Float"></PropertiesSpinEdit>
                                                                </dxwgv:GridViewDataSpinEditColumn>

                                                                <dxwgv:GridViewDataSpinEditColumn FieldName="Price2" Caption="合计金额" VisibleIndex="5" Width="100">
                                                                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="3" NumberType="Float"></PropertiesSpinEdit>
                                                                </dxwgv:GridViewDataSpinEditColumn>
                                                            </Columns>
                                                            <Settings ShowFooter="True" />
                                                            <TotalSummary>
                                                                <dxwgv:ASPxSummaryItem FieldName="Price2" SummaryType="Sum" DisplayFormat="{0:#,##0.000}" />
                                                            </TotalSummary>
                                                        </dxwgv:ASPxGridView>
                                                        <table style="width: 980px; background-color: #e1e0e0; display: none">
                                                            <tr style="text-align: right">
                                                                <td width="95%"></td>
                                                                <td>
                                                                    <div style="display: none">
                                                                        <dxe:ASPxLabel ID="lbl_stockamt" ClientInstanceName="lbl_stockamt" runat="server" Text=""></dxe:ASPxLabel>
                                                                    </div>
                                                                    <dxe:ASPxLabel ID="lbl_total" ClientInstanceName="lbl_total" runat="server" Text=""></dxe:ASPxLabel>

                                                                </td>
                                                            </tr>
                                                        </table>

                                                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                AllowResize="True" Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload){
	    grd_Stock.Refresh();
        grd_Det.Refresh();
      }
}" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
