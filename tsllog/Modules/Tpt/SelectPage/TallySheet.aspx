<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TallySheet.aspx.cs" Inherits="Modules_Tpt_SelectPage_TallySheet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/script/jquery.js"></script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 12px;
        }
        a {
          color:blue;
          text-decoration:underline;
        }
        .td_border {
            border-top: solid #999999 1px;
            border-right: solid #999999 1px;
        }

        .td_border_1 {
            border-top: solid #000000 1px;
            border-right: solid #999999 1px;
        }

        .td_border_2 {
            border-top: solid #999999 1px;
            border-right: solid #999999 1px;
        }

        td.algin {
            text-align: center;
        }

        td.sn {
            text-align: center;
            width: 60px;
        }

        td.text {
            text-align: left;
            width: 260px;
        }

        td.text1 {
            text-align: left;
            width: 80px;
        }

        td.text2 {
            text-align: left;
            width: 130px;
        }

        td.text3 {
            text-align: left;
            width: 200px;
        }

        td.head {
            text-align: center;
            font-weight: bold;
            background-color: #CCCCCC;
            border-right: solid #999999 1px;
        }
        .hide {
            display: none;
        }
        .show {
          display:block;
        }
    </style>
    <script type="text/javascript">
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        var config = {
            timeout: 0,
            gridview: 'grid',
        }

        $(function () {
            loading.hide();
        })
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }
        function OnSaveCallback(v) {
            if (v != null) {
                alert(v);
                grid.Refresh();
            }
        }
        function ShowArInvoice(invN) {
            parent.navTab.openTab(invN, "/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=" + invN, { title: invN, fresh: false, external: true });
        }
        function goJob() {
            var jobno = lbl_JobNo.GetText();
            parent.parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?no=" + jobno, { title: jobno, fresh: false, external: true });
        }
        function OnCreateInvCallback(v) {
            if (v != null && v.indexOf('Action Error') >= 0) {
                alert(v);
            }
            else {
                alert("Create Invoice Success");
                grid.Refresh();
                document.location.reload();
                parent.parent.navTab.openTab(v, "/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=" + v, { title: v, fresh: false, external: true });
            }

        }
        function save_inline(rowIndex) {
            console.log(rowIndex);
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('SaveInvline_'+rowIndex, save_inline_callback);
            }, config.timeout);
        }
        function save_inline_callback(res){
            if(res=="Success"){
                if (parent.parent.notice) {
                    parent.parent.notice('Save Successful！');
                    grid.Refresh();
                }
            }
        }
        function open_inv_page(invNo){
            parent.navTab.openTab(invNo,"/PagesAccount/EditPage/ArInvoiceEdit.aspx?no="+invNo,{title:invNo, fresh:false, external:true});
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Job_Cost" KeyMember="Id" FilterExpression="1=1" />
            <wilson:DataSource ID="dsWhCosting" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.WhCosting" KeyMember="Id" />
            <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Container_Type" KeyMember="id" FilterExpression="1=1" />
            <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RateType" KeyMember="Id" />
            <wilson:DataSource ID="dsArInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XAArInvoice" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" />
            <div style="display: none">
                <dxe:ASPxLabel ID="lbl_JobNo" ClientInstanceName="lbl_JobNo" runat="server"></dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lbl_QuotedNo" ClientInstanceName="lbl_QuotedNo" runat="server"></dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lbl_Client" ClientInstanceName="lbl_Client" runat="server"></dxe:ASPxLabel>
                <dxe:ASPxLabel ID="lbl_Type" ClientInstanceName="lbl_Type" runat="server"></dxe:ASPxLabel>
            </div>
            <table style="width: 75%">
                <tr>
                     <td>
                    <dxe:ASPxButton ID="btnSelect" runat="server" Text="Select All" Width="100" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="ASPxButton3" Width="60" runat="server" Text="Save"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('Save',OnSaveCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                    <td>

                        <dxe:ASPxButton ID="btn_CreateInv" Width="60" runat="server" Text="Create Inv"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                                              if(confirm('Confirm Create Invoice for '+txt_DocDt.GetText()+' ?')){
                                    grid.GetValuesOnCustomCallback('OK',OnCreateInvCallback);   
                            }              
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                    <td style="color: red; width: 80px">Invoice Date
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_DocDt" ClientInstanceName="txt_DocDt" runat="server" Width="100"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 60px">Currency
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="cmb_CurrencyId" ClientInstanceName="cmb_CurrencyId" runat="server" Width="80" MaxLength="3">
                            <Buttons>
                                <dxe:EditButton Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCurrency(cmb_CurrencyId,spin_ExRate);
                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td style="width: 60px">ExRate</td>
                    <td>
                        <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="80"
                            DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_ExRate" ClientInstanceName="spin_ExRate" Increment="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 80px;display:none">BillType
                    </td>
                    <td style="display:none">
                        <dxe:ASPxComboBox ID="cbb_BillType" runat="server" Width="80" DataSourceID="dsRateType"
                            ValueField="Code" TextField="Code" CallbackPageSize="15"
                            EnableCallbackMode="True" EnableViewState="false" IncrementalFilteringMode="StartsWith" >
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                    <dxe:ASPxButton ID="ASPxButton6" Width="60" runat="server" Text="View Job" 
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    goJob();
                                         
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr style="background: #ccc; font-size: 14px; font-family: Arial; font-weight: bold">
                    <td>Costing List
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" OnCustomDataCallback="grid_CustomDataCallback"
                            DataSourceID="dsCosting" KeyFieldName="Id" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting"
                            OnRowInserting="grid_RowInserting" OnInitNewRow="grid_InitNewRow"
                            OnInit="grid_Init" Width="100%" AutoGenerateColumns="False">
                            <SettingsEditing Mode="EditForm" />
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager Mode="ShowAllRecords" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                                    <EditButton Visible="True" />
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" VisibleIndex="1" SortOrder="Descending" SortIndex="0" Settings-SortMode="Value" Settings-AllowSort="True"
                                    Width="40">
                                    <DataItemTemplate>
                                        <dxe:ASPxCheckBox ID="ack_IsPay" Visible='<%# IsCostCreated(Eval("Id")) %>' runat="server" Width="10">
                                        </dxe:ASPxCheckBox>
                                        <div style="display: none">
                                            <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                                Text='<%# Eval("Id") %>' Width="150">
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0"
                                    Width="120">
                                    <DataItemTemplate>
                                        <div class='<%# IsCostCreated(Eval("Id"))==false?"hide":"show" %>'>
                                            <input type="button" class="button" value="Save" onclick="save_inline(<%# Container.VisibleIndex %>);" />
                                        </div>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataSpinEditColumn Caption="Index" FieldName="LineIndex" VisibleIndex="1" SortOrder="Ascending" SortIndex="0"
                                    Width="50">
                                    <DataItemTemplate>
                                        <dxe:ASPxSpinEdit DisplayFormatString="0" runat="server" Width="40" NumberType="Integer"
                                            ID="spin_LineIndex" Height="21px" Value='<%# Bind("LineIndex")%>' DecimalPlaces="0" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataSpinEditColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="LineType" Caption="Line Type" Width="150" VisibleIndex="1" Visible="false">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="LineStatus" Caption="Optional" Width="50" VisibleIndex="98" Visible="false">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="Y" Value="Y" />
                                            <dxe:ListEditItem Text="N" Value="N" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1">
                                    <DataItemTemplate>
                                        <dxe:ASPxComboBox ID="cbb_ChgCode" ClientInstanceName="cbb_ChgCode" runat="server"
                                            Value='<%# Eval("ChgCode") %>' Width="100" DropDownWidth="100" DropDownStyle="DropDownList" TextField="ChgCodeDe"
                                            DataSourceID="dsRate" ValueField="ChgCodeId" ValueType="System.String"
                                            EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith"
                                            CallbackPageSize="100">
                                        </dxe:ASPxComboBox>
                                        <%--<dxe:ASPxLabel ID="lbl_ChgCode" runat="server" Text='<%# Eval("ChgCode") %>'></dxe:ASPxLabel>--%>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                                    Width="120">
                                    <DataItemTemplate>
                                        <dxe:ASPxTextBox runat="server" Width="200" ID="txt_ChgCodeDe" Text='<%# Bind("ChgCodeDe") %>'>
                                        </dxe:ASPxTextBox>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                    Width="150">
                                    <DataItemTemplate>
                                        <dxe:ASPxMemo runat="server" Rows="2" Width="200px" ID="txt_Remark" Text='<%# Bind("Remark") %>'>
                                        </dxe:ASPxMemo>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Group By" FieldName="GroupBy" VisibleIndex="1"
                                Width="150" >
                                <DataItemTemplate>
                                    <dxe:ASPxTextBox runat="server" Width="100" ID="txt_GroupBy" Text='<%# Bind("GroupBy") %>'>
                                    </dxe:ASPxTextBox>
                                </DataItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ContNo" Caption="Cont No" Width="150" VisibleIndex="1" Visible="false">
                                    <DataItemTemplate>
                                        <dxe:ASPxTextBox runat="server" Width="100" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
                                        </dxe:ASPxTextBox>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="ContType" Caption="Cont Type" Width="150" VisibleIndex="1" Visible="false">
                                    <DataItemTemplate>
                                        <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="cbbContType" runat="server" Width="80" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContType") %>'>
                                            <Items>
                                                <dxe:ListEditItem Text="20HC" Value="20HC" />
                                                <dxe:ListEditItem Text="20OT" Value="20OT" />
                                                <dxe:ListEditItem Text="20RF" Value="20RF" />
                                                <dxe:ListEditItem Text="20GP" Value="20GP" />
                                                <dxe:ListEditItem Text="20FT" Value="20FT" />
                                                <dxe:ListEditItem Text="20HD" Value="20HD" />
                                                <dxe:ListEditItem Text="40FR" Value="40FR" />
                                                <dxe:ListEditItem Text="40GP" Value="40GP" />
                                                <dxe:ListEditItem Text="40GPD" Value="40GPD" />
                                                <dxe:ListEditItem Text="40HC" Value="40HC" />
                                                <dxe:ListEditItem Text="40HCD" Value="40HCD" />
                                                <dxe:ListEditItem Text="40HCN" Value="40HCN" />
                                                <dxe:ListEditItem Text="40OT" Value="40OT" />
                                                <dxe:ListEditItem Text="40RF" Value="40RF" />
                                                <dxe:ListEditItem Text="40HD" Value="40HD" />
                                                <dxe:ListEditItem Text="45GP" Value="45GP" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </DataItemTemplate>
                                    <PropertiesComboBox DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Rate" FieldName="Price" VisibleIndex="5"
                                    Width="50">
                                    <DataItemTemplate>
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="80"
                                            ID="spin_Price" Height="21px" Value='<%# Bind("Price")%>' DecimalPlaces="2" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="5"
                                    Width="50">
                                    <DataItemTemplate>
                                        <dxe:ASPxSpinEdit DisplayFormatString="0.0" runat="server" Width="50"
                                            ID="spin_Qty" Height="21px" Value='<%# Bind("Qty")%>' DecimalPlaces="1" Increment="0">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="UOM" FieldName="Unit" VisibleIndex="5"
                                    Width="50">
                                    <DataItemTemplate>
                                        <dxe:ASPxTextBox Width="80px" ID="txt_Unit" ClientInstanceName="txt_Unit"
                                            runat="server" Text='<%# Bind("Unit") %>'>
                                        </dxe:ASPxTextBox>
                                        </td>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="5"
                                    Width="50">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="5"
                                    Width="50">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Amt" FieldName="LocAmt" VisibleIndex="5"
                                    Width="50">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Sub JobNo" FieldName="SubJobNo" VisibleIndex="5"
                                    Width="50">
                                    <DataItemTemplate>
                                        <a href='javascript: parent.parent.navTab.openTab("<%# Eval("SubJobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("SubJobNo") %>",{title:"<%# Eval("SubJobNo") %>", fresh:false, external:true});'><%# Eval("SubJobNo") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Inv No" VisibleIndex="5" SortIndex="0"
                                    Width="120">
                                    <DataItemTemplate>
                                        <div class='<%# IsCostCreated(Eval("Id"))==true?"show":"hide" %>' style="min-width: 70px;">
                                            <a href='javascript: parent.parent.navTab.openTab("<%# DocNo(Eval("Id")) %>","/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=<%# DocNo(Eval("Id")) %>",{title:"<%# DocNo(Eval("Id")) %>", fresh:false, external:true});'><%# DocNo(Eval("Id")) %></a>
                                        </div>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Templates>
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
                                                    runat="server" Text='<%# Bind("ChgCodeDe") %>'>
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
                                                <dxe:ASPxTextBox runat="server" BackColor="Control" Width="350" ID="txt_CostVendorName"
                                                    ClientInstanceName="txt_CostVendorName" ReadOnly="true" Text=''>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Remark
                                            </td>
                                            <td colspan="3">
                                                <dxe:ASPxTextBox runat="server" Width="100%" ID="spin_CostRmk" Text='<%# Bind("Remark") %>'>
                                                </dxe:ASPxTextBox>
                                            </td>
                                            <td colspan="4">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>Cont No</td>
                                                        <td>
                                                            <dxe:ASPxTextBox runat="server" Width="100%" ID="txt_ContNo" Text='<%# Bind("ContNo") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Cont Type</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbbContType" ClientInstanceName="txt_ContType" runat="server" Width="80" ValueField="containerType" TextField="containerType" Value='<%# Bind("ContType") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="20HC" Value="20HC" />
                                                                    <dxe:ListEditItem Text="20OT" Value="20OT" />
                                                                    <dxe:ListEditItem Text="20RF" Value="20RF" />
                                                                    <dxe:ListEditItem Text="20GP" Value="20GP" />
                                                                    <dxe:ListEditItem Text="20FT" Value="20FT" />
                                                                    <dxe:ListEditItem Text="20HD" Value="20HD" />
                                                                    <dxe:ListEditItem Text="40FR" Value="40FR" />
                                                                    <dxe:ListEditItem Text="40GP" Value="40GP" />
                                                                    <dxe:ListEditItem Text="40GPD" Value="40GPD" />
                                                                    <dxe:ListEditItem Text="40HC" Value="40HC" />
                                                                    <dxe:ListEditItem Text="40HCD" Value="40HCD" />
                                                                    <dxe:ListEditItem Text="40HCN" Value="40HCN" />
                                                                    <dxe:ListEditItem Text="40OT" Value="40OT" />
                                                                    <dxe:ListEditItem Text="40RF" Value="40RF" />
                                                                    <dxe:ListEditItem Text="40HD" Value="40HD" />
                                                                    <dxe:ListEditItem Text="45GP" Value="45GP" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                        <td>Line Type</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="ASPxComboBox1" ClientInstanceName="txt_ContType" runat="server" Width="60" Value='<%# Bind("LineType") %>'>
                                                                <Items>
                                                                    <dxe:ListEditItem Text="Job" Value="JOB" />
                                                                    <dxe:ListEditItem Text="Cont" Value="CONT" />
                                                                    <dxe:ListEditItem Text="Claim" Value="CL" />
                                                                    <dxe:ListEditItem Text="Transport" Value="TP" />
                                                                </Items>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="8">
                                                <table width="100%">
                                                    <tr>
                                                        <td></td>
                                                        <td>Qty
                                                        </td>
                                                        <td>UOM
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
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit SpinButtons-ShowIncrementButtons="false" runat="server" Width="100" ClientInstanceName="spin_CostCostQty"
                                                                ID="spin_CostCostQty" Text='<%# Bind("Qty")%>' Increment="0" DisplayFormatString="0.0" DecimalPlaces="1">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox Width="80px" ID="txt_det_Unit" ClientInstanceName="txt_det_Unit"
                                                                runat="server" Text='<%# Bind("Unit") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostPrice"
                                                                runat="server" Width="100" ID="spin_CostCostPrice" Value='<%# Bind("Price")%>' Increment="0" DecimalPlaces="2">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxButtonEdit ID="cmb_CostCostCurrency" ClientInstanceName="cmb_CostCostCurrency" runat="server" Width="80" Text='<%# Bind("CurrencyId") %>' MaxLength="3">
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
                                                                DisplayFormatString="0.000000" DecimalPlaces="6" ID="spin_CostCostExRate" ClientInstanceName="spin_CostCostExRate" Text='<%# Bind("ExRate")%>' Increment="0">
                                                                <ClientSideEvents ValueChanged="function(s, e) {
                                                           Calc(spin_CostCostQty.GetText(),spin_CostCostPrice.GetText(),spin_CostCostExRate.GetText(),2,spin_CostCostAmt);
	                                                   }" />
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxSpinEdit DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false" ClientInstanceName="spin_CostCostAmt"
                                                                BackColor="Control" ReadOnly="true" runat="server" Width="120" ID="spin_CostCostAmt"
                                                                Value='<%# Bind("LocAmt")%>'>
                                                            </dxe:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">

                                        <dxe:ASPxHyperLink ID="btn_UpdateCost" runat="server" NavigateUrl="#" Text="Update" Visible='<%# SafeValue.SafeString(Eval("JobStatusCode"),"USE")=="USE" %>'>
                                            <ClientSideEvents Click="function(s,e){grid.UpdateEdit();}" />
                                        </dxe:ASPxHyperLink>
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    </div>
                                </EditForm>
                            </Templates>
                            <Settings ShowFooter="true" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr style="background: #ccc; font-size: 14px; font-family: Arial; font-weight: bold">
                    <td>Stock Move
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_CustId" ClientInstanceName="txt_CustId" Width="150" runat="server">
                            </dxe:ASPxTextBox>
                        </div>
                        <table cellspacing="0" cellpadding="0" style="border-bottom: solid #000000 1px; width: 3000px; height: auto">
                            <tr style="background-color: #CCCCCC; text-align: center">
                                <td rowspan="2" class="td_border_1 sn">#</td>
                                <td rowspan="2" class="td_border_1 text3 head">JobNo</td>
                                <td rowspan="2" class="td_border_1 text3 head">Englee Do No</td>
                                <td rowspan="2" class="td_border_1 text1 head">JobDate</td>
                                <td rowspan="2" class="td_border_1 text1 head">Customer</td>
                                <td rowspan="2" class="td_border_1 text1 head">Status</td>
                                <td rowspan="2" class="td_border_1 text1 head">Warehouse</td>
                                <td rowspan="2" class="td_border_1 text1 head">Whs Type</td>
                                <td rowspan="2" class="td_border_1 text1 head">Permit No</td>
                                <td rowspan="2" class="td_border_1 text1 head">Location</td>
                                <td class="td_border_1 text1 head" rowspan="2">Lot No</td>
                                <td class="td_border_1 text head" rowspan="2">Hbl No</td>
                                <td class="td_border_1 text2 head" rowspan="2">Cont No</td>
                                <td class="td_border_1 text1 head" rowspan="2">Type</td>
                                <td class="td_border_1 text1 head" rowspan="2">SKU</td>
                                <td class="td_border_1 head" colspan="6">In Quantity</td>
                                <td class="td_border_1 head" colspan="6">Out Quantity</td>
                                <td class="td_border_1 head" colspan="6">Balance</td>
                                <td class="td_border_1 text3 head" rowspan="2">Marking</td>
                                <td class="td_border_1 text3 head" rowspan="2">Description</td>
                                <td class="td_border_1 text3 head" rowspan="2">Remark</td>
                            </tr>
                            <tr style="background-color: #CCCCCC; text-align: center">
                                <td class="td_border_1" width="100">Qty</td>
                                <td class="td_border_1" width="100">Uom</td>
                                <td class="td_border_1" width="100">Weight</td>
                                <td class="td_border_1" width="100">Volume</td>
                                <td class="td_border_1" width="150">Sku Qty</td>
                                <td class="td_border_1" width="100">Uom</td>

                                <td class="td_border_1" width="100">Qty</td>
                                <td class="td_border_1" width="100">Uom</td>
                                <td class="td_border_1" width="100">Weight</td>
                                <td class="td_border_1" width="100">Volume</td>
                                <td class="td_border_1" width="150">Sku Qty</td>
                                <td class="td_border_1" width="100">Uom</td>

                                <td class="td_border_1" width="100">Qty</td>
                                <td class="td_border_1" width="100">Uom</td>
                                <td class="td_border_1" width="100">Weight</td>
                                <td class="td_border_1" width="100">Volume</td>
                                <td class="td_border_1" width="150">Sku Qty</td>
                                <td class="td_border_1" width="100">Uom</td>
                            </tr>
                            <%
                                string dateFrom = "";
                                string dateTo = "";

                                DataTable tab = GetData(this.txt_CustId.Text, dateFrom, dateTo, "", "", "", "", "", "");
                                int n = 0;

                                string lastRefNo = "";
                                string lastContNo = "";
                                string lastBookingNo = "";
                                string lastSkuCode = "";
                                string lastHblNo = "";
                                string lastItemCode = "";
                                int lastLineId = 0;

                                decimal handQty = 0;
                                decimal skuQty = 0;

                                decimal inQty = 0;
                                decimal outQty = 0;
                                decimal skuIn = 0;
                                decimal skuOut = 0;

                                decimal inWeight = 0;
                                decimal outWeight = 0;
                                decimal inVolume = 0;
                                decimal outVolume = 0;

                                decimal handWeight = 0;
                                decimal handVolume = 0;

                                decimal totalQty = 0;
                                decimal totalWt = 0;
                                decimal totalM3 = 0;

                                for (int i = 0; i < tab.Rows.Count; i++)
                                {
                                    string refNo = SafeValue.SafeString(tab.Rows[i]["RefNo"]);
                                    string hblNo = SafeValue.SafeString(tab.Rows[i]["HblNo"]);
                                    string type = SafeValue.SafeString(tab.Rows[i]["CargoType"]);
                                    string bookingNo = SafeValue.SafeString(tab.Rows[i]["BookingNo"]);
                                    string skuCode = SafeValue.SafeString(tab.Rows[i]["SkuCode"]);
                                    string itemCode = SafeValue.SafeString(tab.Rows[i]["ActualItem"]);
                                    int lineId = SafeValue.SafeInt(tab.Rows[i]["LineId"], 0);
                                    int no = SafeValue.SafeInt(tab.Rows[i]["No"], 0);
                                    if (type == "IN")
                                    {
                                        inQty = SafeValue.SafeDecimal(tab.Rows[i]["QtyOrig"]);
                                        skuIn = SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]);
                                        inWeight = SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]);
                                        inVolume = SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]);
                                    }
                                    else
                                    {
                                        outQty = SafeValue.SafeDecimal(tab.Rows[i]["QtyOrig"]);
                                        skuOut = SafeValue.SafeDecimal(tab.Rows[i]["PackQty"]);
                                        outWeight = SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"]);
                                        outVolume = SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"]);
                                    }
                                    string contNo = SafeValue.SafeString(tab.Rows[i]["ContNo"]);
                                    if (bookingNo == lastBookingNo && (skuCode == lastSkuCode || itemCode == lastItemCode) && lineId == lastLineId)
                                    {

                                        if (n == 0)
                                        {
                                            handQty = inQty - outQty;
                                            skuQty = skuIn - skuOut;
                                            handWeight = inWeight - outWeight;
                                            handVolume = inVolume - outVolume;
                                        }
                                        else
                                        {
                                            handQty = handQty - outQty;
                                            skuQty = skuQty - skuOut;
                                            handWeight = handWeight - outWeight;
                                            handVolume = handVolume - outVolume;
                                        }
                                       
                            %>
                            <tr style="line-height: 30px;">
                                <td class="td_border_2 sn"><%=i + 1%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["JobNo"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["ManualDo"]) %></td>
                                <td class="td_border_2"><%=Helper.Safe.SafeDateStr(tab.Rows[i]["JobDate"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["ClientId"])%></td>
                                <td class="td_border_2 algin"><%=type%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["WareHouseCode"])%></td>
                                <td class="td_border_2"></td>
                                <td class="td_border_2"></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["Location"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["BookingNo"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["HblNo"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["ContNo"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["OpsType"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["SkuCode"])%></td>

                                <td class="td_border_2"></td>
                                <td class="td_border_2"></td>
                                <td class="td_border_2"></td>
                                <td class="td_border_2"></td>
                                <td class="td_border_2"></td>
                                <td class="td_border_2"></td>

                                <td class="td_border_2"><%=SafeValue.SafeDecimal(outQty)%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeDecimal(tab.Rows[i]["WeightOrig"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeDecimal(tab.Rows[i]["VolumeOrig"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeDecimal(outQty)%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"])%></td>


                                <td class="td_border_2"><%=SafeValue.SafeDecimal(handQty)%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeDecimal(handWeight)%></td>
                                <td class="td_border_2"><%=SafeValue.SafeDecimal(handVolume)%></td>
                                <td class="td_border_2"><%=SafeValue.SafeDecimal(skuQty)%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"])%></td>

                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["Marking1"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["Marking2"])%></td>
                                <td class="td_border_2"><%=SafeValue.SafeString(tab.Rows[i]["Remark1"])%></td>
                            </tr>
                            <%  

                                    n++;
                                }
                                else
                                {
                                    n = 0;


                                    handQty = inQty;
                                    skuQty = skuIn;
                                    handWeight = inWeight;
                                    handVolume = inVolume;


                            %>
                            <tr style="line-height: 30px;">
                                <td class="td_border_1 sn"><%=i+1 %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["JobNo"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ManualDo"]) %></td>
                                <td class="td_border_1"><%=Helper.Safe.SafeDateStr(tab.Rows[i]["JobDate"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ClientId"]) %></td>
                                <td class="td_border_1 algin"><%=type %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["WareHouseCode"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["WhsType"])%></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PermitNo"])%></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Location"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["BookingNo"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["HblNo"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["ContNo"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["OpsType"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["SkuCode"]) %></td>

                                <td class="td_border_1"><%=SafeValue.SafeDecimal(inQty) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(inWeight) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(inVolume) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(skuIn) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>

                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>

                                <td class="td_border_1"><%=SafeValue.SafeDecimal(handQty) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackTypeOrig"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(handWeight) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(handVolume) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(skuIn) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["PackUom"]) %></td>

                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking1"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Marking2"]) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeString(tab.Rows[i]["Remark1"]) %></td>
                            </tr>
                            <%  
                                }
                                    totalQty +=SafeValue.ChinaRound(handQty,2);
                                    totalWt +=SafeValue.ChinaRound(handWeight,3);
                                    totalM3 +=SafeValue.ChinaRound(handVolume,3);
                                    lastLineId = lineId;
                                    lastRefNo = refNo;
                                    lastBookingNo = bookingNo;
                                    lastHblNo = hblNo;
                                    lastSkuCode = skuCode;
                                    lastSkuCode = itemCode;
                                } %>
                             <tr style="line-height: 30px;">
                                <td class="td_border_1 sn"></td>
                                <td class="td_border_1"></td>
                                 <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1 algin"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>

                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>

                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>

                                <td class="td_border_1"><%=SafeValue.SafeDecimal(totalQty) %></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(totalWt) %></td>
                                <td class="td_border_1"><%=SafeValue.SafeDecimal(totalM3) %></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>

                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                                <td class="td_border_1"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr style="background: #ccc; font-size: 14px; font-family: Arial; font-weight: bold">
                    <td>Invoice History
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsArInvoice"
                            ClientInstanceName="ASPxGridView1" Width="850" KeyFieldName="SequenceId">
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <SettingsCustomizationWindow Enabled="True" />
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                                    <DataItemTemplate>
                                        <a onclick='ShowArInvoice("<%# Eval("DocNo") %>");'>Edit</a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                                    Width="8%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="3"
                                    Width="5%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                                    Width="8%">
                                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="6"
                                    Width="8%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="6"
                                    Width="8%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="7"
                                    Width="8%">
                                    <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="8"
                                    Width="8%">
                                    <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Bal Amt" FieldName="BalanceAmt" VisibleIndex="8"
                                    Width="8%">
                                    <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                                    </PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="PostInd" FieldName="ExportInd" VisibleIndex="9"
                                    Width="8%">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="CancelInd" FieldName="CancelInd" VisibleIndex="10"
                                    Width="8%">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
