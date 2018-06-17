<%@ Page Language="C#" EnableViewState="true" AutoEventWireup="true" CodeFile="InternationalLcl.aspx.cs" Inherits="WareHouse_Job_InternationalLcl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
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
        function ChangeValue() {
            var C23_ = 0;
            if (C23.GetNumber() == null) {
                C23.SetNumber(parseFloat(0.00));
            } else {
                C23_ = parseFloat(C23.GetNumber());
            }
            var C24_ = 0;
            if (C24.GetNumber() == null) {
                C24.SetNumber(parseFloat(0.00));
            } else {
                C24_ = parseFloat(C24.GetNumber());
            }
            var B28_ = 0;
            if (B28.GetNumber() == null) {
                B28.SetNumber(parseFloat(0.00));
            } else {
                B28_ = parseFloat(B28.GetNumber());
            }
            var E27_ = 0;
            if (E27.GetNumber() == null) {
                E27.SetNumber(parseFloat(0.00));
            } else {
                E27_ = parseFloat(E27.GetNumber());
            }
            var B33_ = 0;
            if (B33.GetNumber() == null) {
                B33.SetNumber(parseFloat(0.00));
            } else {
                B33_ = parseFloat(B33.GetNumber());
            }
            var C34_ = 0;
            if (C34.GetNumber() == null) {
                C34.SetNumber(parseFloat(0.00));
            } else {
                C34_ = parseFloat(C34.GetNumber());
            }
            var C35_ = 0;
            if (C35.GetNumber() == null) {
                C35.SetNumber(parseFloat(0.00));
            } else {
                C35_ = parseFloat(C35.GetNumber());
            }
            var C36_ = 0;
            if (C36.GetNumber() == null) {
                C36.SetNumber(parseFloat(0.00));
            } else {
                C36_ = parseFloat(C36.GetNumber());
            }
            var C37_ = 0;
            if (C37.GetNumber() == null) {
                C37.SetNumber(parseFloat(0.00));
            } else {
                C37_ = parseFloat(C37.GetNumber());
            }
            var B41_ = 0;
            if (B41.GetNumber() == null) {
                B41.SetNumber(parseFloat(0.00));
            } else {
                B41_ = parseFloat(B41.GetNumber());
            }
            var B42_ = 0;
            if (B42.GetNumber() == null) {
                B42.SetNumber(parseFloat(0.00));
            } else {
                B42_ = parseFloat(B42.GetNumber());
            }
            var B43_ = 0;
            if (B43.GetNumber() == null) {
                B43.SetNumber(parseFloat(0.00));
            } else {
                B43_ = parseFloat(B43.GetNumber());
            }
            var B44_ = 0;
            if (B44.GetNumber() == null) {
                B44.SetNumber(parseFloat(0.00));
            } else {
                B44_ = parseFloat(B44.GetNumber());
            }
            var B45_ = 0;
            if (B45.GetNumber() == null) {
                B45.SetNumber(parseFloat(0.00));
            } else {
                B45_ = parseFloat(B45.GetNumber());
            }
            var B46_ = 0;
            if (B46.GetNumber() == null) {
                B46.SetNumber(parseFloat(0.00));
            } else {
                B46_ = parseFloat(B46.GetNumber());
            }
            var E40_ = 0;
            if (E40.GetNumber() == null) {
                E40.SetNumber(parseFloat(0.00));
            } else {
                E40_ = parseFloat(E40.GetNumber());
            }
            var C52_ = 0;
            if (C52.GetNumber() == null) {
                C52.SetNumber(parseFloat(0.00));
            } else {
                C52_ = parseFloat(C52.GetNumber());
            }
            var C53_ = 0;
            if (C53.GetNumber() == null) {
                C53.SetNumber(parseFloat(0.00));
            } else {
                C53_ = parseFloat(C53.GetNumber());
            }
            var C54_ = 0;
            if (C54.GetNumber() == null) {
                C54.SetNumber(parseFloat(0.00));
            } else {
                C54_ = parseFloat(C54.GetNumber());
            }
            var C55_ = 0;
            if (C55.GetNumber() == null) {
                C55.SetNumber(parseFloat(0.00));
            } else {
                C55_ = parseFloat(C55.GetNumber());
            }
            var C57_ = 0;
            if (C57.GetNumber() == null) {
                C57.SetNumber(parseFloat(0.00));
            } else {
                C57_ = parseFloat(C57.GetNumber());
            }
            var C59_ = 0;
            if (C59.GetNumber() == null) {
                C59.SetNumber(parseFloat(0.00));
            } else {
                C59_ = parseFloat(C59.GetNumber());
            }
            var C60_ = 0;
            if (C60.GetNumber() == null) {
                C60.SetNumber(parseFloat(0.00));
            } else {
                C60_ = parseFloat(C60.GetNumber());
            }
            var m = parseFloat(B15.GetText());
            var C15_ = m - m * 0.2;
            var D15_ = m * 6.5;
            var B16_ = m / 35.312;
            var C16_ = C15_ * 6.5;
            var D16_ = FormatNumber(D15_ / 2.2, 0);
            var D18_ = C15_ * 1;
            var C20_ = m * 1.2;
            var C25_ = 270 + C20_ + C23_;
            var C28_ = B16_ * B28_ * E27_;
            var B30_ = B28_ * 0.03;
            var C29_ = B30_ * E27_;
            var C33_ = B33_ * E27_;
            var C38_ = C28_ + C29_ + C33_ + C34_ + C35_ + C36_ + C37_;
            var C41_ = B41_ * E40_;
            var C42_ = (B42_ * B16_) * E40_;
            var C43_ = B43_ * E40_;
            var C44_ = B44_ * E40_;
            var C45_ = B45_ * E40_;
            var C46_ = B46_ * E40_;
            var C47_ = C41_ + C42_ + C43_ + C44_ + C45_ + C46_;
            var E42_ = C25_ / (E27_ - 0.05);
            var E43_ = C38_ / (E27_ - 0.05);
            var E44_ = (C52_ + C53_ + C54_ + C55_) / (E27_ - 0.05);
            var E45_ = C47_ / (E27_ - 0.05);
            var F42_ = E42_ / D15_ * 100;
            var F43_ = E43_ / D15_ * 100;
            var F44_ = E44_ / D15_ * 100;
            var F45_ = E45_ / D15_ * 100;
            var F46_ = E46_ / D15_ * 100;
            var F47_ = E47_ / D15_ * 100;
            var C49_ = C25_ + C38_ + C47_;
            var C58_ = C49_ + C52_ + C53_ + C54_ + C55_;
            var C56_ = 0.14;
            var D56_ = C58_ * C56_;
            var D57_ = C57_ * C58_;
            var D58_ = C58_ + D56_ + D57_;
            var E46_ = (D56_ + D57_) / (E27_ - 0.05);
            var E47_ = D58_ / (E27_ - 0.05);
            var F46_ = E46_ / D15_ * 100;
            var F47_ = E47_ / D15_ * 100;
            var E56_ = D58_ / m;
            var E57_ = E56_ * 35.312;
            var D59_ = D58_ * C59_;
            var E59_ = D58_ + D59_;
            var D60_ = E59_ * C60_;
            var E60_ = E59_ - D60_;

            C15.SetNumber(FormatNumber(C15_,0));
            D15.SetNumber(FormatNumber(D15_,0));
            B16.SetNumber(FormatNumber(B16_,2));
            C16.SetNumber(FormatNumber(C16_,0));
            D16.SetNumber(FormatNumber(D16_, 0));
            D18.SetNumber(FormatNumber(D18_,4) * 100 / 100);
            C20.SetNumber(C20_);
            C25.SetNumber(C25_);//170+m * 1.2+parseFloat(C23.GetText())+parseFloat(C24.GetText())
            C28.SetNumber(C28_);
            C29.SetNumber(C29_);
            B30.SetNumber(FormatNumber(B30_,2));
            
            C33.SetNumber(C33_);
            C38.SetNumber(C38_);//SUM(C28:C37);
            C41.SetNumber(C41_);
            C42.SetNumber(C42_);
            C43.SetNumber(C43_);
            C44.SetNumber(C44_);
            C45.SetNumber(C45_);
            C46.SetNumber(C46_);
            C47.SetNumber(C47_);//SUM(C41:C46));
            E42.SetNumber(E42_);
            E43.SetNumber(E43_);
            E44.SetNumber(E44_);
            E45.SetNumber(E45_);
            //以下还没好
            F42.SetNumber(F42_);
            F43.SetNumber(F43_);
            F44.SetNumber(F44_);
            F45.SetNumber(F45_);
            F46.SetNumber(F46_);
            F47.SetNumber(F47_);
            C49.SetNumber(C49_);
            
            
            
            C58.SetNumber(C58_);
            D56.SetNumber(D56_);
            D57.SetNumber(D57_);
            D58.SetNumber(D58_);
            E46.SetNumber(E46_);
            E47.SetNumber(E47_);
            F46.SetNumber(F46_);
            F47.SetNumber(F47_);
            E56.SetNumber(E56_);
            E57.SetNumber(FormatNumber(E57_,2));
            D59.SetNumber(D59_);
            E59.SetNumber(E59_);
            D60.SetNumber(D60_);
            E60.SetNumber(FormatNumber(E60_,2));





            //C16.SetNumber(C15*6.5);
            //D16.SetNumber(D15 / 2.2);
            //D18.SetNumber(C15 * 1);
            //C20.SetNumber(B15 * 1.2);
            //C25.SetNumber(SUM(C18:C24));
            //C28.SetNumber(B16*B28*E27);
            //C29.SetNumber(B30*E27);
            //B30.SetNumber(B28*3%);
            //C28.SetNumber(B16 * B28 * E27);
            //C29.SetNumber(B30 * E27);
            //C33.SetNumber(B33*E27);
            //C38.SetNumber(SUM(C28:C37);
            //C41.SetNumber(B41*E40);
            //C42.SetNumber((B42*B16)*E40);
            //C43.SetNumber(B43*E40);
            //C44.SetNumber(B44*E40);
            //C45.SetNumber(B45*E40);
            //C46.SetNumber(B46*E40);
            //C47.SetNumber(SUM(C41:C46));
            //E42.SetNumber(C25/(E27-0.05));
            //E43.SetNumber(C38/(E27-0.05));
            //E44.SetNumber(SUM(C50:C55)/(E27-0.05));
            //E45.SetNumber(C47/(E27-0.05));
            //E46.SetNumber(SUM(D56:D57)/(E27-0.05));
            //E47.SetNumber(D58/(E27-0.05));
            //F42.SetNumber(E42/D15*100);
            //F43.SetNumber(E43/D15*100);
            //F44.SetNumber(E44/D15*100);
            //F45.SetNumber(E45/D15*100);
            //F46.SetNumber(E46/D15*100);
            //F47.SetNumber(E47/D15*100);
            //C49.SetNumber(C25+C38+C47);
            //D56.SetNumber(C58*C56);
            //E56.SetNumber(D58/B15);
            //D57.SetNumber(C57*C58);
            //E57.SetNumber(E56*35.312);
            //C58.SetNumber(SUM(C49:C55));
            //D58.SetNumber(C58+D56+D57);
            //D59.SetNumber(D58*C59);
            //E59.SetNumber(D58+D59);
            //D60.SetNumber(E59*C60);
            //E60.SetNumber(E59-D60);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div><dxe:ASPxTextBox runat="server" ID="txt_Id" ClientVisible="false"></dxe:ASPxTextBox></div>
        <div>
            <table>
                <tr>
                    <td>
                        <h2>LCL ACCRUAL SHEET</h2>
                    </td>
                    <td width="200"></td>
                    <td>
                        <dxe:ASPxButton runat="server" ID="btn_Save" OnClick="btn_Save_Click" Text="Save All Changes">
                            <ClientSideEvents Click="function(){
                            lab.SetText('Saving...');
                            }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lab" ClientInstanceName="lab" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="Red">
                        </dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>Cuft/Gross</td>
                    <td><b>Net vol.</b></td>
                    <td></td><%--Transit Time--%>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B15" ClientInstanceName="B15" runat="server" ForeColor="Red" DecimalPlaces="3"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.000">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C15" ClientInstanceName="C15" runat="server" HorizontalAlign="Center" ReadOnly="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" Border-BorderWidth="0" DecimalPlaces="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D15" ClientInstanceName="D15" runat="server" HorizontalAlign="Center" ReadOnly="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" Border-BorderWidth="0" DecimalPlaces="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>lbs</td>
                </tr>
                <tr>
                    <td>Cbm</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B16" ClientInstanceName="B16" runat="server" HorizontalAlign="Center" Border-BorderWidth="0" ReadOnly="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C16" ClientInstanceName="C16" runat="server" Font-Bold="true" ReadOnly="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" HorizontalAlign="Center" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D16" ClientInstanceName="D16" runat="server" ReadOnly="true" DisplayFormatString="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" HorizontalAlign="Center" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>kgs</td>
                </tr>
            </table>
            <hr />
            <table>
                <tr>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                </tr>
                <tr>
                    <td colspan="3" align="center"><h4>Origin Services</h4></td>
                </tr>
                <tr>
                    <td>Labor</td>
                    <td></td>
                    <td>
                        <dxe:ASPxTextBox ID="C18" ClientInstanceName="C18" runat="server" Text="150.00"
                            Width="100" HorizontalAlign="Right" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D18" ClientInstanceName="D18" runat="server" Font-Bold="true" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="E18" ClientInstanceName="E18" runat="server" ReadOnly="true" Border-BorderWidth="0"
                            Width="100" Text="or min 150" Font-Bold="true">
                        </dxe:ASPxTextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Materials</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C19" ClientInstanceName="C19" runat="server" Font-Bold="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Liftvan </td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C20" ClientInstanceName="C20" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Transportation</td>
                    <td></td>
                    <td>
                        <dxe:ASPxTextBox ID="C21" ClientInstanceName="C21" runat="server" DisplayFormatString="0.00"
                            Width="100" Text="0.00" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Trans to Port</td>
                    <td></td>
                    <td>
                        <dxe:ASPxTextBox ID="C22" ClientInstanceName="C22" runat="server"
                            Width="100" ReadOnly="true" Border-BorderWidth="0" Text="120.00" HorizontalAlign="Right">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Fumigation</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C23" ClientInstanceName="C23" runat="server" HorizontalAlign="Right" ForeColor="Red"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Other</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C24" ClientInstanceName="C24" runat="server" ForeColor="Red" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" HorizontalAlign="Right">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr> 
                <tr>
                    <td><b>Sub Total</b></td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C25" ClientInstanceName="C25" runat="server" Font-Bold="true" ReadOnly="true" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" Border-BorderWidth="0" HorizontalAlign="Right">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr> 
            </table>
            <table>
                <tr>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                </tr>
                <tr>
                    <td colspan="3" align="center"><h4>Freight Charges</h4></td>
                </tr>
                <tr>
                    <td></td>
                    <td>US$ values</td>
                    <td>S$ value</td>
                    <td>US$ Ex Rate </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="E27" ClientInstanceName="E27" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td style="font-size:5px">Ocean Freight/ Cbm</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B28" ClientInstanceName="B28" runat="server" ForeColor="Red" DisplayFormatString="0.00"
                            Width="100" HorizontalAlign="Right" SpinButtons-ShowIncrementButtons="false" Increment="0">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C28" ClientInstanceName="C28" runat="server" Font-Bold="true" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>BAF</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C29" ClientInstanceName="C29" runat="server" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>CAF</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B30" ClientInstanceName="B30" runat="server" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ForeColor="Red" HorizontalAlign="Right">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>GRI</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C31" ClientInstanceName="C31" runat="server" Font-Bold="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ForeColor="Red" HorizontalAlign="Right">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Other</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B33" ClientInstanceName="B33" runat="server" Font-Bold="true" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ForeColor="Red" HorizontalAlign="Right">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C33" ClientInstanceName="C33" runat="server" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="font:5px">THC/ Forklift</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C34" ClientInstanceName="C34" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>BL</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C35" ClientInstanceName="C35" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>BL Surrender</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C36" ClientInstanceName="C36" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr> 
                <tr>
                    <td>Other +AM</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C37" ClientInstanceName="C37" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr> 
                <tr>
                    <td>Sub Total</td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C38" ClientInstanceName="C38" runat="server" Font-Bold="true" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" Border-BorderWidth="0" ReadOnly="true">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr> 
            </table>
            <table>
                <tr>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                    <td width="110"></td>
                </tr>
                <tr>
                    <td colspan="4" align="center"><h4>Destination Services</h4></td>
                    <td style="text-underline-position:below">Euro  Ex Rate</td>
                </tr>
                <tr>
                    <td></td>
                    <td style="font-size:5px">In any currency</td>
                    <td>S$ value</td>
                    <td>Ex Rate</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="E40" ClientInstanceName="E40" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>Dest. Rate</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B41" ClientInstanceName="B41" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C41" ClientInstanceName="C41" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td></td>
                    <td style="font-size:5px">Per Cwt</td>
                </tr>
                <tr>
                    <td><table><tr><td>THC/</td><td style="color:purple">cbm</td></tr></table></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B42" ClientInstanceName="B42" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C42" ClientInstanceName="C42" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font:5px;" align="right">OS</td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="E42" ClientInstanceName="E42" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="F42" ClientInstanceName="F42" runat="server" HorizontalAlign="Right" DisplayFormatString="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>NVOCC</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B43" ClientInstanceName="B43" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C43" ClientInstanceName="C43" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font: 5px;" align="right">OF</td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="E43" ClientInstanceName="E43" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="F43" ClientInstanceName="F43" runat="server" HorizontalAlign="Right" DisplayFormatString="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>VAT etc.</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B44" ClientInstanceName="B44" runat="server" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ForeColor="Red" HorizontalAlign="Right">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C44" ClientInstanceName="C44" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font:5px;" align="right">Misc</td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="E44" ClientInstanceName="E44" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="F44" ClientInstanceName="F44" runat="server" HorizontalAlign="Right" DisplayFormatString="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>Special svcs</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B45" ClientInstanceName="B45" runat="server" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ForeColor="Red" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C45" ClientInstanceName="C45" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font:5px;" align="right">Misc</td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="E45" ClientInstanceName="E45" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="F45" ClientInstanceName="F45" runat="server" HorizontalAlign="Right" DisplayFormatString="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td style="font:5px">Other</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="B46" ClientInstanceName="B46" runat="server" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ForeColor="Red" HorizontalAlign="Right">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C46" ClientInstanceName="C46" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font:5px;" align="right">Misc</td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="E46" ClientInstanceName="E46" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="F46" ClientInstanceName="F46" runat="server" HorizontalAlign="Right" DisplayFormatString="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>Sub Total</td>
                    <td>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C47" ClientInstanceName="C47" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="E47" ClientInstanceName="E47" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size: 5px">
                        <dxe:ASPxSpinEdit ID="F47" ClientInstanceName="F47" runat="server" HorizontalAlign="Right" DisplayFormatString="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>Cost</td>
                    <td>Charge Cust.</td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C49" ClientInstanceName="C49" runat="server" HorizontalAlign="Right" DisplayFormatString="0.00"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" ReadOnly="true" Border-BorderWidth="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Handyman</td>
                    <td>
                        <dxe:ASPxTextBox ID="C50" ClientInstanceName="C50" runat="server" Text="0.00"
                            Width="100" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">House cleaning</td>
                    <td>
                        <dxe:ASPxTextBox ID="C51" ClientInstanceName="C51" runat="server" Text="0.00"
                            Width="100" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Absormatic @ S$40.00</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C52" ClientInstanceName="C52" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Crating</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C53" ClientInstanceName="C53" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Shuttling/Stairs/Piano</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C54" ClientInstanceName="C54" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>Other</td>
                    <td>fumation</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C55" ClientInstanceName="C55" runat="server" ForeColor="Red" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">Fixed overhead charges</td>
                    <td>
                        <dxe:ASPxTextBox ID="C56" ClientInstanceName="C56" runat="server" HorizontalAlign="Right" Text="14.0%"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" Border-BorderWidth="0" ReadOnly="true">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D56" ClientInstanceName="D56" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="E56" ClientInstanceName="E56" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" Font-Bold="true" DisplayFormatString="$0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size:5px"><b>Per cuft</b></td>
                </tr> 
                <tr>
                    <td colspan="2">Margin</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C57" ClientInstanceName="C57" runat="server" ForeColor="Red" Font-Bold="true" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0%">
                            <ClientSideEvents ValueChanged="function(s,e){
                                ChangeValue();
                                }" />
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D57" ClientInstanceName="D57" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="E57" ClientInstanceName="E57" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" Font-Bold="true" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td style="font-size:5px"><b>Per cbm</b></td>
                </tr> 
                <tr>
                    <td colspan="2"><b>Door to door cost</b></td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C58" ClientInstanceName="C58" runat="server" HorizontalAlign="Right" ReadOnly="true" Border-BorderWidth="0"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D58" ClientInstanceName="D58" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="$0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr> 
                <tr style="font-size:5px">
                    <td colspan="2">Corporate Margin</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C59" ClientInstanceName="C59" runat="server" HorizontalAlign="Right" Font-Bold="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0%">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D59" ClientInstanceName="D59" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="E59" ClientInstanceName="E59" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr> 
                <tr>
                    <td colspan="2">Corporate Discount</td>
                    <td>
                        <dxe:ASPxSpinEdit ID="C60" ClientInstanceName="C60" runat="server" HorizontalAlign="Right" Font-Bold="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="0%">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="D60" ClientInstanceName="D60" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right" Font-Bold="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0">
                        </dxe:ASPxSpinEdit>
                    </td>
                    <td>
                        <dxe:ASPxSpinEdit ID="E60" ClientInstanceName="E60" runat="server" ReadOnly="true" Border-BorderWidth="0" HorizontalAlign="Right" Font-Bold="true"
                            Width="100" SpinButtons-ShowIncrementButtons="false" Increment="0" DisplayFormatString="$0.00">
                        </dxe:ASPxSpinEdit>
                    </td>
                </tr> 
            </table>
        </div>
    </form>
</body>
</html>
