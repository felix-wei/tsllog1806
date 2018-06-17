<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Process_BatchAdd.aspx.cs" Inherits="PagesContTrucking_SelectPage_Process_BatchAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
            if (e.keyCode == 27) { parent.AfterPopub(); }
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
            <div style="display: none">
                <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server"></dxe:ASPxTextBox>
            </div>
            <table style="width: 500px;" class="bx_table_grid">

                <tr>
                    <td colspan="4" class="td_border0">
                        <dxe:ASPxButton ID="btn_Add" runat="server" Text="Save All" OnClick="btn_Add_Click"></dxe:ASPxButton>
                    </td>
                </tr>
                <%--<tr>
                    <td style="border-top: 1px solid #808080" colspan="12">&nbsp;</td>
                </tr>--%>
                <tr style="text-align: left">
                    <th>#</th>
                    <th>Process Type<b style="color: red;">*</b></th>
                    <th>Process Date</th>
                    <th>Qty</th>
                    <th>Remark</th>
                </tr>
                <tr>
                    <td>
                        <%--<a href="#" onclick="autoCopy('');">AutoG</a>--%>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType" ClientInstanceName="cbb_ProcessType" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess" ClientInstanceName="date_DateProcess" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty" ClientInstanceName="spin_Qty" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark" ClientInstanceName="txt_Remark" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                       <%-- <a href="#" onclick="autoCopy('1');">AutoG</a>--%>

                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType1" ClientInstanceName="cbb_ProcessType1" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess1" ClientInstanceName="date_DateProcess1" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty1" ClientInstanceName="spin_Qty" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark1" ClientInstanceName="txt_Remark1" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<a href="#" onclick="autoCopy('2');">AutoG</a>--%>

                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType2" ClientInstanceName="cbb_ProcessType2" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess2" ClientInstanceName="date_DateProcess2" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty2" ClientInstanceName="spin_Qty2" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark2" ClientInstanceName="txt_Remark2" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<a href="#" onclick="autoCopy('3');">AutoG</a>--%>

                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType3" ClientInstanceName="cbb_ProcessType3" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess3" ClientInstanceName="date_DateProcess3" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty3" ClientInstanceName="spin_Qty" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark3" ClientInstanceName="txt_Remark3" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<a href="#" onclick="autoCopy('4');">AutoG</a>--%>

                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType4" ClientInstanceName="cbb_ProcessType4" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess4" ClientInstanceName="date_DateProcess4" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty4" ClientInstanceName="spin_Qty4" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark4" ClientInstanceName="txt_Remark4" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                       <%-- <a href="#" onclick="autoCopy('5');">AutoG</a>--%>

                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType5" ClientInstanceName="cbb_ProcessType5" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess5" ClientInstanceName="date_DateProcess5" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty5" ClientInstanceName="spin_Qty5" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark5" ClientInstanceName="txt_Remark5" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<a href="#" onclick="autoCopy('6');">AutoG</a>--%>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType6" ClientInstanceName="cbb_ProcessType6" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess6" ClientInstanceName="date_DateProcess6" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty6" ClientInstanceName="spin_Qty6" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark6" ClientInstanceName="txt_Remark6" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                       <%-- <a href="#" onclick="autoCopy('7');">AutoG</a>--%>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType7" ClientInstanceName="cbb_ProcessType7" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess7" ClientInstanceName="date_DateProcess7" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty7" ClientInstanceName="spin_Qty7" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark7" ClientInstanceName="txt_Remark7" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<a href="#" onclick="autoCopy('8');">AutoG</a>--%>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType8" ClientInstanceName="cbb_ProcessType8" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess8" ClientInstanceName="date_DateProcess8" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty8" ClientInstanceName="spin_Qty8" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark8" ClientInstanceName="txt_Remark8" runat="server" Width="165"></dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<a href="#" onclick="autoCopy('9');">AutoG</a>--%>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxComboBox ID="cbb_ProcessType9" ClientInstanceName="cbb_ProcessType9" runat="server" Width="100%">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Inspection" Value="Inspection" />
                                <dxe:ListEditItem Text="Refurbish" Value="Refurbish" />
                                <dxe:ListEditItem Text="Painting" Value="Painting" />
                                <dxe:ListEditItem Text="Washing" Value="Washing" />
                                <dxe:ListEditItem Text="Others" Value="Others" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="width: 120px">
                        <dxe:ASPxDateEdit ID="date_DateProcess9" ClientInstanceName="date_DateProcess9" runat="server"  Width="120" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td style="width: 80px">
                        <dxe:ASPxSpinEdit DisplayFormatString="0.000" runat="server" Width="60"
                            ID="spin_Qty9" ClientInstanceName="spin_Qty9" Height="21px"  DecimalPlaces="3" Increment="0">
                            <SpinButtons ShowIncrementButtons="false" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="width: 165px">
                        <dxe:ASPxTextBox ID="txt_Remark9" ClientInstanceName="txt_Remark9" runat="server" Width="165"></dxe:ASPxTextBox>
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
