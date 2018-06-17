<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Booking_BatchAdd.aspx.cs" Inherits="Modules_Freight_SelectPage_Booking_BatchAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <link href="../script/f_dev.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.Popup_BookingBatchAdd_callback('false'); }
        }
        document.onkeydown = keydown;

        function autoCopy(par) {
            var curNo = txt_LotNo.GetText();
            var curNo_t = parseInt(curNo, 10) + 1;
            curNo = 'C' + curNo_t;
            txt_LotNo.SetText(curNo_t);
            document.getElementById('txt_BookingNo' + par).value = curNo;
            document.getElementById('txt_HblNo' + par).value = '';
        }
    </script>
    <style type="text/css">
        /*a, a:link, a:hover, a:disabled, a:after {
            text-decoration: underline;
            font-size: 13px;
            color: black;
        }*/
        a {
            text-decoration: underline;
            font-size: 13px;
            color: black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Container_Type" KeyMember="id" FilterExpression="" />
            <div style="display: none">
                <dxe:ASPxTextBox ID="txt_LotNo" ClientInstanceName="txt_LotNo" runat="server"></dxe:ASPxTextBox>
            </div>
            <table style="width: 700px;" class="bx_table_grid">

                <tr>
                    <td colspan="4" class="td_border0">
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Save All" OnClick="btn_Add_Click"></dxe:ASPxButton>
                    </td>
                </tr>
                <%--<tr>
                    <td style="border-top: 1px solid #808080" colspan="12">&nbsp;</td>
                </tr>--%>
                <tr style="text-align: left">
                     <th>Client</th>
                    <th>Booking No</th>
                    <th>Hbl No</th>
                    <th>Weight</th>
                    <th>Volume</th>
                    <th>Qty</th>
                    <th>DG</th>
                    <th>Send Mode</th>
                    <th>Consignee</th>
                    <th>Delivery Address</th>
                    <th>Remark</th>
                </tr>
                <tr>
                    <td>
                        <table style="width: 120px">
                            <tr>
                                <td class="lbl">Code
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientContact" ClientInstanceName="txt_ClientContact" runat="server"  Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl"><dxe:ASPxTextBox ID="txt_ClientEmail" ClientInstanceName="txt_ClientEmail" runat="server" Width="100%" ></dxe:ASPxTextBox></td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl"><dxe:ASPxTextBox ID="txt_ClientTel" ClientInstanceName="txt_ClientTel" runat="server" Width="100%" ></dxe:ASPxTextBox></td>
                            </tr>
                             <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl"><dxe:ASPxTextBox ID="txt_ClientZip" ClientInstanceName="txt_ClientZip" runat="server" Width="100%" ></dxe:ASPxTextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_BookingNo" ClientInstanceName="txt_BookingNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_HblNo" ClientInstanceName="txt_HblNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Weight" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Volume" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Qty" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_DgClass" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                <dxe:ListEditItem Text="Class 2" Value="Class 2" />
                                <dxe:ListEditItem Text="Class 3" Value="Class 3" />
                                <dxe:ListEditItem Text="Other Class" Value="Other Class" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_SendMode" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Delivery" Value="Delivery" Selected="true" />
                                <dxe:ListEditItem Text="Pickup" Value="Pickup" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table style="width: 180px">
                            <tr>
                                <td class="lbl">Code</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ConsigneeId" ClientInstanceName="btn_ConsigneeId" runat="server" Width="100%" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ConsigneeId,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeContact" ClientInstanceName="txt_ConsigneeContact" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeEmail" ClientInstanceName="txt_ConsigneeEmail" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeTel" ClientInstanceName="txt_ConsigneeTel" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeZip" ClientInstanceName="txt_ConsigneeZip" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxMemo ID="memo_ConsigneeAddress" ClientInstanceName="memo_ConsigneeAddress" Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxMemo ID="txt_Remark" Rows="6" runat="server" Width="165"></dxe:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td>
                       <table style="width: 120px">
                            <tr>
                                <td class="lbl">Code
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ClientId1" ClientInstanceName="btn_ClientId1" runat="server" Width="100" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId1,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientContact1" ClientInstanceName="txt_ClientContact1" runat="server"  Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl"><dxe:ASPxTextBox ID="txt_ClientEmail1" ClientInstanceName="txt_ClientEmail1" runat="server" Width="100%" ></dxe:ASPxTextBox></td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl"><dxe:ASPxTextBox ID="txt_ClientTel1" ClientInstanceName="txt_ClientTel1" runat="server" Width="100%" ></dxe:ASPxTextBox></td>
                            </tr>
                           <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl"><dxe:ASPxTextBox ID="txt_ClientZip1" ClientInstanceName="txt_ClientZip1" runat="server" Width="100%" ></dxe:ASPxTextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_BookingNo1" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_HblNo1" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Weight1" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Volume1" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Qty1" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_DgClass1" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                <dxe:ListEditItem Text="Class 2" Value="Class 2" />
                                <dxe:ListEditItem Text="Class 3" Value="Class 3" />
                                <dxe:ListEditItem Text="Other Class" Value="Other Class" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_SendMode1" ClientInstanceName="cbb_SendMode1" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Delivery" Value="Delivery" Selected="true" />
                                <dxe:ListEditItem Text="Pickup" Value="Pickup" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table style="width: 180px">
                            <tr>
                                <td class="lbl">Code</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ConsigneeId1" ClientInstanceName="btn_ConsigneeId1" runat="server" Width="100%" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ConsigneeId1,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeContact1" ClientInstanceName="txt_ConsigneeContact1" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeEmail1" ClientInstanceName="txt_ConsigneeEmail1" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeTel1" ClientInstanceName="txt_ConsigneeTel1" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeZip1" ClientInstanceName="txt_ConsigneeZip1" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxMemo ID="memo_ConsigneeAddress1" ClientInstanceName="memo_ConsigneeAddress1" Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxMemo ID="txt_Remark1" Rows="6" runat="server" Width="165"></dxe:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 120px">
                            <tr>
                                <td class="lbl">Code
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ClientId2" ClientInstanceName="btn_ClientId2" runat="server" Width="100" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId2,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientContact2" ClientInstanceName="txt_ClientContact2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientEmail2" ClientInstanceName="txt_ClientEmail2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientTel2" ClientInstanceName="txt_ClientTel2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientZip2" ClientInstanceName="txt_ClientZip2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_BookingNo2" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_HblNo2" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Weight2" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Volume2" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Qty2" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_DgClass2" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                <dxe:ListEditItem Text="Class 2" Value="Class 2" />
                                <dxe:ListEditItem Text="Class 3" Value="Class 3" />
                                <dxe:ListEditItem Text="Other Class" Value="Other Class" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_SendMode2" ClientInstanceName="cbb_SendMode2" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Delivery" Value="Delivery" Selected="true" />
                                <dxe:ListEditItem Text="Pickup" Value="Pickup" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table style="width: 180px">
                            <tr>
                                <td class="lbl">Code</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ConsigneeId2" ClientInstanceName="btn_ConsigneeId2" runat="server" Width="100%" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ConsigneeId2,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeContact2" ClientInstanceName="txt_ConsigneeContact2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeEmail2" ClientInstanceName="txt_ConsigneeEmail2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeTel2" ClientInstanceName="txt_ConsigneeTel2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeZip2" ClientInstanceName="txt_ConsigneeZip2" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxMemo ID="memo_ConsigneeAddress2" ClientInstanceName="memo_ConsigneeAddress2" Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxMemo ID="txt_Remark2" Rows="6" runat="server" Width="165"></dxe:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 120px">
                            <tr>
                                <td class="lbl">Code
                                </td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ClientId3" ClientInstanceName="btn_ClientId3" runat="server" Width="100" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId3,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientContact3" ClientInstanceName="txt_ClientContact3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientEmail3" ClientInstanceName="txt_ClientEmail3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientTel3" ClientInstanceName="txt_ClientTel3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ClientZip3" ClientInstanceName="txt_ClientZip3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_BookingNo3" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_HblNo3" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Volume3" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Weight3" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="spin_Qty3" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_DgClass3" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Normal" Value="Normal" Selected="true" />
                                <dxe:ListEditItem Text="Class 2" Value="Class 2" />
                                <dxe:ListEditItem Text="Class 3" Value="Class 3" />
                                <dxe:ListEditItem Text="Other Class" Value="Other Class" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_SendMode3" ClientInstanceName="cbb_SendMode3" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="Delivery" Value="Delivery" Selected="true" />
                                <dxe:ListEditItem Text="Pickup" Value="Pickup" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table style="width: 180px">
                            <tr>
                                <td class="lbl">Code</td>
                                <td class="ctl">
                                    <dxe:ASPxButtonEdit ID="btn_ConsigneeId3" ClientInstanceName="btn_ConsigneeId3" runat="server" Width="100%" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ConsigneeId3,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Contact</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeContact3" ClientInstanceName="txt_ConsigneeContact3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Email</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeEmail3" ClientInstanceName="txt_ConsigneeEmail3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">Tel</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeTel3" ClientInstanceName="txt_ConsigneeTel3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="lbl">PostCode</td>
                                <td class="ctl">
                                    <dxe:ASPxTextBox ID="txt_ConsigneeZip3" ClientInstanceName="txt_ConsigneeZip3" runat="server" Width="100%"></dxe:ASPxTextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxMemo ID="memo_ConsigneeAddress3" ClientInstanceName="memo_ConsigneeAddress" Rows="6" runat="server" Width="180"></dxe:ASPxMemo>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxMemo ID="txt_Remark3" Rows="6" runat="server" Width="165"></dxe:ASPxMemo>
                    </td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="450"
            Width="800" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
