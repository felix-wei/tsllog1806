<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Container_BatchAdd1.aspx.cs" Inherits="PagesContTrucking_SelectPage_Container_BatchAdd1" %>

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
            if (e.keyCode == 27) { parent.Popup_ContainerBatchAdd_callback('false'); }
        }
        document.onkeydown = keydown;

        function autoCopy(par) {
            var curNo = txt_CurNO.GetText();
            var curNo_t = parseInt(curNo, 10) + 1;
            curNo = 'C' + curNo_t;
            //btn_ContNo.SetText(curNo);
            txt_CurNO.SetText(curNo_t);
            document.getElementById('btn_ContNo' + par + '_I').value = curNo;
            document.getElementById('txt_ContType' + par + '_I').value = '40HC';
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
                <dxe:ASPxTextBox ID="txt_CurNO" ClientInstanceName="txt_CurNO" runat="server"></dxe:ASPxTextBox>
            </div>
            <table style="width: 800px;" class="bx_table_grid">

                <tr>
                    <td class="td_border0">JobNo:</td>
                    <td class="td_border0">
                        <dxe:ASPxTextBox ID="txt_JobNo" runat="server" ReadOnly="true" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td colspan="4" class="td_border0">
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Save All" OnClick="btn_Add_Click"></dxe:ASPxButton>
                    </td>
                </tr>
                <%--<tr>
                    <td style="border-top: 1px solid #808080" colspan="12">&nbsp;</td>
                </tr>--%>
                <tr style="text-align: left">
                    <th>#</th>
                    <th>Cont No<b style="color: red;">*</b></th>
                    <th>SealNo</th>
                    <th>Cont Type</th>
                    <th>Weight</th>
                    <th>Service</th>
                    <th>Urgent</th>
                    <th>Permit</th>
                    <th>DG/J5</th>
                    <th>Depot</th>
                    <th>ScheduleDate</th>
                    <th>Time</th>
                    <th>Remark</th>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('');">AutoG</a>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="btn_ContNo" ClientInstanceName="btn_ContNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_SealNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxComboBox ID="txt_ContType" ClientInstanceName="txt_ContType" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress" ClientInstanceName="btn_YardAddress" runat="server" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress);" class="bx_a_button">&nbsp;..&nbsp;</a></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxDateEdit ID="date_YardExpiry" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time" runat="server" Width="50">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('1');">AutoG</a></td>
                    <td>
                        <dxe:ASPxTextBox ID="btn_ContNo1" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo1" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType1" ClientInstanceName="txt_ContType1" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight1" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType1" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent1" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit1" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg1" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>

                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress1" ClientInstanceName="btn_YardAddress1" runat="server" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>

                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress1);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry1" ClientInstanceName="date_YardExpiry1" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time1" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark1" ClientInstanceName="txt_Remark1" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('2');">AutoG</a></td>
                    <td>
                        <dxe:ASPxTextBox ID="btn_ContNo2" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo2" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType2" ClientInstanceName="txt_ContType2" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight2" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType2" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent2" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit2" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg2" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress2" ClientInstanceName="btn_YardAddress2" runat="server" AutoPostBack="False" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress2);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry2" ClientInstanceName="date_YardExpiry2" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time2" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark2" ClientInstanceName="txt_Remark2" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('3');">AutoG</a></td>
                    <td>
                        <dxe:ASPxTextBox ID="btn_ContNo3" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo3" ClientInstanceName="txt_SealNo3" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType3" ClientInstanceName="txt_ContType3" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight3" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType3" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent3" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit3" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg3" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress3" ClientInstanceName="btn_YardAddress3" runat="server" AutoPostBack="False" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress3);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry3" ClientInstanceName="date_YardExpiry3" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time3" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark3" ClientInstanceName="txt_Remark3" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('4');">AutoG</a></td>
                    <td>
                        <dxe:ASPxTextBox ID="btn_ContNo4" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo4" ClientInstanceName="txt_SealNo4" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType4" ClientInstanceName="txt_ContType4" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight4" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType4" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent4" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit4" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg4" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress4" ClientInstanceName="btn_YardAddress4" runat="server" AutoPostBack="False" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress4);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry4" ClientInstanceName="date_YardExpiry4" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time4" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark4" ClientInstanceName="txt_Remark4" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('5');">AutoG</a></td>
                    <td>
                        <dxe:ASPxTextBox ID="btn_ContNo5" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SealNo5" ClientInstanceName="txt_SealNo5" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_ContType5" ClientInstanceName="txt_ContType5" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight5" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType5" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent5" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit5" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg5" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress5" ClientInstanceName="btn_YardAddress5" runat="server" AutoPostBack="False" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress5);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_YardExpiry5" ClientInstanceName="date_YardExpiry5" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time5" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Remark5" ClientInstanceName="txt_Remark5" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('6');">AutoG</a>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="btn_ContNo6" ClientInstanceName="btn_ContNo6" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_SealNo6" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxComboBox ID="txt_ContType6" ClientInstanceName="txt_ContType6" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight6" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType6" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent6" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit6" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg6" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress6" ClientInstanceName="btn_YardAddress6" runat="server" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress6);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxDateEdit ID="date_YardExpiry6" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time6" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark6" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('7');">AutoG</a>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="btn_ContNo7" ClientInstanceName="btn_ContNo7" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_SealNo7" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxComboBox ID="txt_ContType7" ClientInstanceName="txt_ContType7" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight7" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType7" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent7" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit7" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg7" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress7" ClientInstanceName="btn_YardAddress7" runat="server" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress7);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxDateEdit ID="date_YardExpiry7" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time7" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark7" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('8');">AutoG</a>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="btn_ContNo8" ClientInstanceName="btn_ContNo8" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_SealNo8" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxComboBox ID="txt_ContType8" ClientInstanceName="txt_ContType8" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight8" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType8" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent8" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit8" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg8" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress8" ClientInstanceName="btn_YardAddress8" runat="server" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress8);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxDateEdit ID="date_YardExpiry8" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time8" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark8" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="#" onclick="autoCopy('9');">AutoG</a>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="btn_ContNo9" ClientInstanceName="btn_ContNo9" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxTextBox ID="txt_SealNo9" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxComboBox ID="txt_ContType9" ClientInstanceName="txt_ContType9" runat="server" Width="80" DataSourceID="dsContType" ValueField="containerType" TextField="containerType"></dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="txt_Weight9" runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_ServiceType9" runat="server" Width="80">
                            <Items>
                                <dxe:ListEditItem Value="Two-Way" Text="Two-Way" Selected="true" />
                                <dxe:ListEditItem Value="COL" Text="COL" />
                                <dxe:ListEditItem Value="EXP" Text="EXP" />
                                <dxe:ListEditItem Value="IMP" Text="IMP" />
                                <dxe:ListEditItem Value="RET" Text="RET" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Urgent9" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_Permit9" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="txt_dg9" runat="server" Width="60">
                            <Items>
                                <dxe:ListEditItem Value="Y" Text="Y" />
                                <dxe:ListEditItem Value="N" Text="N" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 165px">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxMemo ID="btn_YardAddress9" ClientInstanceName="btn_YardAddress9" runat="server" Width="120px" Height="16px">
                                    </dxe:ASPxMemo>
                                </td>
                                <td>
                                    <a href="#" onclick="PopupCustAdr(null,btn_YardAddress9);" class="bx_a_button">&nbsp;..&nbsp;</a>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxDateEdit ID="date_YardExpiry9" Width="100" runat="server"
                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 100px">
                        <dxe:ASPxTextBox ID="txt_time9" runat="server" Width="100%">
                            <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                            <ValidationSettings ErrorDisplayMode="None" />
                        </dxe:ASPxTextBox>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark9" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="450"
            Width="600" EnableViewState="False">
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
