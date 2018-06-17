<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintSiteSurvey.aspx.cs" Inherits="ReportJob_PrintSiteSurvey" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function doPrint() { 
            bdhtml=window.document.body.innerHTML; 
            sprnstr="<!--startprint-->"; 
            eprnstr="<!--endprint-->"; 
            prnhtml=bdhtml.substr(bdhtml.indexOf(sprnstr)+17); 
            prnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr)); 
            window.document.body.innerHTML = prnhtml;
            pagesetup_null();
            window.print();

        }
</script>
<script type="text/javascript">
    var hkey_root,hkey_path,hkey_key
    hkey_root="HKEY_CURRENT_USER"
    hkey_path="//Software//Microsoft//Internet Explorer//PageSetup//"
    //设置网页打印的页眉页脚为空
    function pagesetup_null(){
        try{
            var RegWsh = new ActiveXObject("WScript.Shell");
            hkey_key = "header";
            RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
            hkey_key = "footer";

            RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "");
        }catch(e){}
    }
    //设置网页打印的页眉页脚为默认值
    function pagesetup_default(){
        try{
            var RegWsh = new ActiveXObject("WScript.Shell")
            hkey_key = "footer";
            RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d");
            hkey_key = "footer";
            RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d")
        }catch(e){}
    }
</script>
</head>
<body onload="doPrint();">
    <form id="form1" runat="server">       
        <!--startprint-->
    <div style="margin:0 auto;width: 210mm;">
        <table  class="head">
        <tr>
            <td style="width:80%"><img src=""/></td>
            <td>

                <p>
                    Collin's Movers Pie Ltd<br />
                    22 Jurong Port Road Singapore 61914<br />
                    Tel:6873 9595 /Fax:6774 4156<br />
                    Gen.Enquiry:collinsmovers@pacigic.net.sg<br />
                    Co Regn No:200208650G<br />
                    GST Regn No:20-028650-G<br />
                </p>

            </td>
        </tr>
    </table>
        <table class="A4">
            <tr>
                <td><dxe:ASPxLabel runat="server" ID="lbl_JobType"  CssClass="Text"></dxe:ASPxLabel></td>
            </tr>
        </table>
        <table class="A4">
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" Styles-EditForm-BackColor="White"  Border-BorderWidth="0">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <Settings ShowColumnHeaders="false" />
            <Templates>
                <EditForm>
                    <!-- Local Move Begin-->
                    <div style="display: <%# SafeValue.SafeString(Eval("JobType"),"")=="Local Move"?"block":"none" %>">
                    <table class="table_A4">
                        <tr class="tr">
                            <td class="td_bottom_right">Surveyer:<%# Eval("Value2") %></td>
                            <td class="td_bottom_right">Survey Date:<%# SafeValue.SafeDateStr(Eval("DateTime2") )%></td>
                            <td class="td_bottom_right">Survey Time:<%# SafeValue.SafeDate(Eval("DateTime2"),DateTime.Now).ToShortTimeString()%></td>
                            <td class="td_bottom">RefNo:<%# Eval("JobNo") %></td>
                        </tr>
                        <tr style="height: 60px;">
                            <td colspan="2" class="td_bottom_right" width="50%">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Name:</td>
                                        <td><%# Eval("CustomerName") %></td>
                                    </tr>
                                    <tr style="height: 50px; vertical-align:top;">
                                        <td>Origin Address:</td>
                                        <td><%# Eval("OriginAdd") %></td>
                                    </tr>
                                    <tr>
                                        <td>Contact Person:</td>
                                        <td><%# Eval("Contact") %></td>
                                    </tr>
                                    <tr>
                                        <td>Tel:</td>
                                        <td><%# Eval("Tel") %></td>
                                    </tr>
                                    <tr>
                                        <td>E-mail:</td>
                                        <td><%# Eval("Email") %></td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="2" class="td_bottom" width="50%">
                                <table>
                                    <tr>
                                        <td>Destination:</td>
                                        <td><%# Eval("DestinationPort") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr  class="tr">
                            <td colspan="2" class="td_bottom_right"><b>MOVE DETAILS</b></td>
                            <td colspan="2" class="td_bottom"><b>MOVE SCHEDULE</b></td>
                        </tr>
                        <tr style="height: 60px;" >
                            <td colspan="2" class="td_bottom_right">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Volume(cuft):</td>
                                        <td><%# Eval("Volumne") %></td>
                                    </tr>
                                    <tr style="height: 50px;">
                                        <td>No of Trips:</td>
                                        <td><%# Eval("TripNo") %></td>
                                    </tr>
                                    <tr>
                                        <td>Charges:</td>
                                        <td><%# Eval("Charges") %></td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="2" style="height: 60px;" class="td_bottom">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Pack Date:</td>
                                        <td><%# Eval("PackDate") %></td>
                                    </tr>
                                    <tr style="height: 50px;">
                                        <td>Move Date:</td>
                                        <td><%# Eval("MoveDate") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tr">
                            <td colspan="4" class="td_bottom" style="text-align:center"><b>ADDITIONALS</b></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="vertical-align:top;">
                                <table  class="table_Detail">
                                    <tr class="tr">
                                        <td class="td_bottom_right">
                                           Full Packing: <%# Eval("Item1")%>
                                        </td>

                                        <td class="td_bottom" colspan="3">
                                           Full Packing Details: <%# Eval("ItemDetail1") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right"> 
                                            Full Un Packing:<%# Eval("Item2") %>
                                        </td>
                                        <td class="td_bottom"  colspan="3">
                                           Unpack Details:<%# Eval("ItemDetail2") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">
                                           Insurance:<%# Eval("Item3") %>
                                        </td>
                                        <td class="td_bottom_right">Insurance Percentage:<%# Eval("ItemValue3") %></td>
                                        <td class="td_bottom_right">Insurance Value:<%# Eval("ItemData3") %></td>
                                        <td class="td_bottom">Insurance Premium:<%# Eval("ItemPrice3") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Piano Apply: <%# Eval("Item4") %></td>
                                        <td colspan="2" class="td_bottom_right">Paino Details: <%# Eval("ItemDetail4") %></td>
                                        <td class="td_bottom">Charges S$: <%# Eval("ItemPrice4") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td  class="td_bottom_right">Safe: <%# Eval("Item5") %></td>
                                        <td colspan="2" class="td_bottom_right">Brand:<%# Eval("ItemValue5") %></td>
                                        <td class="td_bottom">Weight in KG:<%# Eval("ItemPrice5") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Crating:<%# Eval("Item6") %></td>
                                        <td colspan="2" class="td_bottom_right">Details:<%# Eval("ItemDetail6") %></td>
                                        <td class="td_bottom">Charges S$ <%# Eval("ItemPrice6") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Handyman Services:<%# Eval("Item7") %></td>
                                        <td class="td_bottom_right">Complimentory:<%# Eval("ItemValue7") %></td>
                                        <td class="td_bottom_right">Details:<%# Eval("ItemDetail7") %></td>
                                        <td class="td_bottom">Charges S$:<%# Eval("ItemPrice7") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Maid Services:<%# Eval("Item8") %></td>
                                        <td class="td_bottom_right">Complimentory:<%# Eval("ItemValue8") %></td>
                                        <td class="td_bottom_right">Details: <%# Eval("ItemDetail8") %></td>
                                        <td class="td_bottom">Charges S$: <%# Eval("ItemPrice8") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Shifting Services:<%# Eval("Item9") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue9") %></td>
                                        <td class="td_bottom_right">Details： <%# Eval("ItemDetail9") %></td>
                                        <td class="td_bottom">Charges S$：<%# Eval("ItemPrice9") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Disposal Services：<%# Eval("Item10") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue10") %></td>
                                        <td class="td_bottom_right">Details：<%# Eval("ItemDetail10") %></td>
                                        <td class="td_bottom">Charges S$： <%# Eval("ItemPrice10") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Additional Pick-Up：<%# Eval("Item11") %></td>
                                        <td colspan="3">Details： <%# Eval("ItemDetail11") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Additional Delivery： <%# Eval("Item12") %></td>
                                        <td colspan="3" class="td_bottom">Details： <%# Eval("ItemDetail12") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Bad Access：<%# Eval("Item13") %></td>
                                        <td colspan="2" class="td_bottom_right">Origin: <%# Eval("ItemValue13") %> </td>
                                        <td class="td_bottom">Destination：<%# Eval("ItemData13") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Storage：<%# Eval("Item14") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue14") %></td>
                                        <td class="td_bottom_right">Details：<%# Eval("ItemDetail14") %></td>
                                        <td class="td_bottom">Charges S$： <%# Eval("ItemPrice14") %></td>
                                    </tr>
                                    <tr style="text-align: left; vertical-align: top; height:125px">
                                        <td class="td_bottom" colspan="4">
                                            Notes：<%# Eval("Notes") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4" class="td_bottom">How did you come to know about our Company:<%# Eval("Answer1") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4" class="td_bottom">Other:<%# Eval("Answer2") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4">Move Competitors:<%# Eval("Answer3") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                        </div>
                    <!-- Local Move End-->

                    <!-- Office Move Begin-->
                    <div style="display: <%# SafeValue.SafeString(Eval("JobType"),"")=="Office Move"?"block":"none" %>">
                        <table class="table_A4">
                        <tr class="tr">
                            <td class="td_bottom_right">Surveyer:<%# Eval("Value2") %></td>
                            <td class="td_bottom_right">Survey Date:<%# SafeValue.SafeDateStr(Eval("DateTime2") )%></td>
                            <td class="td_bottom_right">Survey Time:<%# SafeValue.SafeDate(Eval("DateTime2"),DateTime.Now).ToShortTimeString()%></td>
                            <td class="td_bottom">RefNo:<%# Eval("JobNo") %></td>
                        </tr>
                        <tr style="height: 60px;">
                            <td colspan="2" class="td_bottom_right" width="50%">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Name:</td>
                                        <td><%# Eval("CustomerName") %></td>
                                    </tr>
                                    <tr style="height: 50px; vertical-align:top;">
                                        <td>Origin Address:</td>
                                        <td><%# Eval("OriginAdd") %></td>
                                    </tr>
                                    <tr>
                                        <td>Contact Person:</td>
                                        <td><%# Eval("Contact") %></td>
                                    </tr>
                                    <tr>
                                        <td>Tel:</td>
                                        <td><%# Eval("Tel") %></td>
                                    </tr>
                                    <tr>
                                        <td>E-mail:</td>
                                        <td><%# Eval("Email") %></td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="2" class="td_bottom" width="50%">
                                <table>
                                    <tr>
                                        <td>Destination:</td>
                                        <td><%# Eval("DestinationPort") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr  class="tr">
                            <td colspan="2" class="td_bottom_right"><b>MOVE DETAILS</b></td>
                            <td colspan="2" class="td_bottom"><b>MOVE SCHEDULE</b></td>
                        </tr>
                        <tr style="height: 60px;" >
                            <td colspan="2" class="td_bottom_right">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Volume(cuft):</td>
                                        <td><%# Eval("Volumne") %></td>
                                    </tr>
                                    <tr style="height: 50px;">
                                        <td>No of Trips:</td>
                                        <td><%# Eval("TripNo") %></td>
                                    </tr>
                                    <tr>
                                        <td>Charges:</td>
                                        <td><%# Eval("Charges") %></td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="2" style="height: 60px;" class="td_bottom">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Pack Date:</td>
                                        <td><%# Eval("PackDate") %></td>
                                    </tr>
                                    <tr style="height: 50px;">
                                        <td>Move Date:</td>
                                        <td><%# Eval("MoveDate") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tr">
                            <td colspan="4" class="td_bottom" style="text-align:center"><b>ADDITIONALS</b></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="vertical-align:top;">
                                <table  class="table_Detail">
                                    <tr class="tr">
                                        <td class="td_bottom_right">
                                           Full Packing: <%# Eval("Item1")%>
                                        </td>

                                        <td class="td_bottom" colspan="3">
                                           Full Packing Details: <%# Eval("ItemDetail1") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right"> 
                                            Full Un Packing:<%# Eval("Item2") %>
                                        </td>
                                        <td class="td_bottom"  colspan="3">
                                           Unpack Details:<%# Eval("ItemDetail2") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">
                                           Insurance:<%# Eval("Item3") %>
                                        </td>
                                        <td class="td_bottom_right">Insurance Percentage:<%# Eval("ItemValue3") %></td>
                                        <td class="td_bottom_right">Insurance Value:<%# Eval("ItemData3") %></td>
                                        <td class="td_bottom">Insurance Premium:<%# Eval("ItemPrice3") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Piano Apply: <%# Eval("Item4") %></td>
                                        <td colspan="2" class="td_bottom_right">Paino Details: <%# Eval("ItemDetail4") %></td>
                                        <td class="td_bottom">Charges S$: <%# Eval("ItemPrice4") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td  class="td_bottom_right">Safe: <%# Eval("Item5") %></td>
                                        <td colspan="2" class="td_bottom_right">Brand:<%# Eval("ItemValue5") %></td>
                                        <td class="td_bottom">Weight in KG:<%# Eval("ItemPrice5") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Crating:<%# Eval("Item6") %></td>
                                        <td colspan="2" class="td_bottom_right">Details:<%# Eval("ItemDetail6") %></td>
                                        <td class="td_bottom">Charges S$ <%# Eval("ItemPrice6") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Handyman Services:<%# Eval("Item7") %></td>
                                        <td class="td_bottom_right">Complimentory:<%# Eval("ItemValue7") %></td>
                                        <td class="td_bottom_right">Details:<%# Eval("ItemDetail7") %></td>
                                        <td class="td_bottom">Charges S$:<%# Eval("ItemPrice7") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Shifting Services:<%# Eval("Item9") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue9") %></td>
                                        <td class="td_bottom_right">Details： <%# Eval("ItemDetail9") %></td>
                                        <td class="td_bottom">Charges S$：<%# Eval("ItemPrice9") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Disposal Services：<%# Eval("Item10") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue10") %></td>
                                        <td class="td_bottom_right">Details：<%# Eval("ItemDetail10") %></td>
                                        <td class="td_bottom">Charges S$： <%# Eval("ItemPrice10") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Additional Pick-Up：<%# Eval("Item11") %></td>
                                        <td colspan="3">Details： <%# Eval("ItemDetail11") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Additional Delivery： <%# Eval("Item12") %></td>
                                        <td colspan="3" class="td_bottom">Details： <%# Eval("ItemDetail12") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Bad Access：<%# Eval("Item13") %></td>
                                        <td colspan="2" class="td_bottom_right">Origin: <%# Eval("ItemValue13") %> </td>
                                        <td class="td_bottom">Destination：<%# Eval("ItemData13") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Storage：<%# Eval("Item14") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue14") %></td>
                                        <td class="td_bottom_right">Details：<%# Eval("ItemDetail14") %></td>
                                        <td class="td_bottom">Charges S$： <%# Eval("ItemPrice14") %></td>
                                    </tr>
                                    <tr style="text-align: left; vertical-align: top; height:125px">
                                        <td class="td_bottom" colspan="4">
                                            Notes：<%# Eval("Notes") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4" class="td_bottom">How did you come to know about our Company:<%# Eval("Answer1") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4" class="td_bottom">Other:<%# Eval("Answer2") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4">Move Competitors:<%# Eval("Answer3") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <!-- Office Move End-->

                    <!-- International Move Begin-->
                    <div style="display: <%# SafeValue.SafeString(Eval("JobType"),"")=="International Move"?"block":"none" %>">
                        <table class="table_A4">
                        <tr class="tr">
                            <td class="td_bottom_right">Surveyer:<%# Eval("Value2") %></td>
                            <td class="td_bottom_right">Survey Date:<%# SafeValue.SafeDateStr(Eval("DateTime2") )%></td>
                            <td class="td_bottom_right">Survey Time:<%# SafeValue.SafeDate(Eval("DateTime2"),DateTime.Now).ToShortTimeString()%></td>
                            <td class="td_bottom">RefNo:<%# Eval("JobNo") %></td>
                        </tr>
                        <tr style="height: 60px;">
                            <td colspan="2" class="td_bottom_right" width="50%">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Name:</td>
                                        <td><%# Eval("CustomerName") %></td>
                                    </tr>
                                    <tr style="height: 50px; vertical-align:top;">
                                        <td>Origin Address:</td>
                                        <td><%# Eval("OriginAdd") %></td>
                                    </tr>
                                    <tr>
                                        <td>Contact Person:</td>
                                        <td><%# Eval("Contact") %></td>
                                    </tr>
                                    <tr>
                                        <td>Tel:</td>
                                        <td><%# Eval("Tel") %></td>
                                    </tr>
                                    <tr>
                                        <td>E-mail:</td>
                                        <td><%# Eval("Email") %></td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="2" class="td_bottom" width="50%">
                                <table>
                                    <tr>
                                        <td>Destination:</td>
                                        <td><%# Eval("DestinationPort") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr  class="tr">
                            <td colspan="2" class="td_bottom_right"><b>MOVE DETAILS</b></td>
                            <td colspan="2" class="td_bottom"><b>MOVE SCHEDULE</b></td>
                        </tr>
                        <tr style="height: 60px;" >
                            <td colspan="2" class="td_bottom_right">
                                <table>
                                    <tr style="height: 30px; text-align: left;">
                                        <td width="84" height="22">Volume(cuft):</td>
                                        <td width="367"><%# Eval("Volumne") %></td>
                                        <td width="88"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Mode(LCL/20"/40"/40"HC/20 Con./40 Con./Air/Road):</td>
                                        <td><%# Eval("Mode") %></td>
                                    </tr>
                                </table>
                            </td>
                            <td colspan="2" style="height: 60px;" class="td_bottom">
                                <table>
                                    <tr style="height: 30px;">
                                        <td>Pack Date:</td>
                                        <td><%# Eval("PackDate") %></td>
                                    </tr>
                                    <tr style="height: 30px;">
                                        <td>Move Date:</td>
                                        <td><%# Eval("MoveDate") %></td>
                                    </tr>
                                    <tr>
                                        <td>Eta:</td>
                                        <td><%# Eval("Eta") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tr">
                            <td colspan="4" class="td_bottom" style="text-align:center"><b>ADDITIONALS</b></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="vertical-align:top;">
                                <table  class="table_Detail">
                                    <tr class="tr">
                                        <td class="td_bottom_right">
                                           Full Packing: <%# Eval("Item1")%>
                                        </td>

                                        <td class="td_bottom" colspan="3">
                                           Full Packing Details: <%# Eval("ItemDetail1") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right"> 
                                            Full Un Packing:<%# Eval("Item2") %>
                                        </td>
                                        <td class="td_bottom"  colspan="3">
                                           Unpack Details:<%# Eval("ItemDetail2") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">
                                           Insurance:<%# Eval("Item3") %>
                                        </td>
                                        <td class="td_bottom_right">Insurance Percentage:<%# Eval("ItemValue3") %></td>
                                        <td class="td_bottom_right">Insurance Value:<%# Eval("ItemData3") %></td>
                                        <td class="td_bottom">Insurance Premium:<%# Eval("ItemPrice3") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Piano Apply: <%# Eval("Item4") %></td>
                                        <td colspan="2" class="td_bottom_right">Paino Details: <%# Eval("ItemDetail4") %></td>
                                        <td class="td_bottom">Charges S$: <%# Eval("ItemPrice4") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td  class="td_bottom_right">Safe: <%# Eval("Item5") %></td>
                                        <td colspan="2" class="td_bottom_right">Brand:<%# Eval("ItemValue5") %></td>
                                        <td class="td_bottom">Weight in KG:<%# Eval("ItemPrice5") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Crating:<%# Eval("Item6") %></td>
                                        <td colspan="2" class="td_bottom_right">Details:<%# Eval("ItemDetail6") %></td>
                                        <td class="td_bottom">Charges S$ <%# Eval("ItemPrice6") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Handyman Services:<%# Eval("Item7") %></td>
                                        <td class="td_bottom_right">Complimentory:<%# Eval("ItemValue7") %></td>
                                        <td class="td_bottom_right">Details:<%# Eval("ItemDetail7") %></td>
                                        <td class="td_bottom">Charges S$:<%# Eval("ItemPrice7") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Maid Services:<%# Eval("Item8") %></td>
                                        <td class="td_bottom_right">Complimentory:<%# Eval("ItemValue8") %></td>
                                        <td class="td_bottom_right">Details: <%# Eval("ItemDetail8") %></td>
                                        <td class="td_bottom">Charges S$: <%# Eval("ItemPrice8") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Shifting Services:<%# Eval("Item9") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue9") %></td>
                                        <td class="td_bottom_right">Details： <%# Eval("ItemDetail9") %></td>
                                        <td class="td_bottom">Charges S$：<%# Eval("ItemPrice9") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Disposal Services：<%# Eval("Item10") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue10") %></td>
                                        <td class="td_bottom_right">Details：<%# Eval("ItemDetail10") %></td>
                                        <td class="td_bottom">Charges S$： <%# Eval("ItemPrice10") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Additional Pick-Up：<%# Eval("Item11") %></td>
                                        <td colspan="3">Details： <%# Eval("ItemDetail11") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Additional Delivery： <%# Eval("Item12") %></td>
                                        <td colspan="3" class="td_bottom">Details： <%# Eval("ItemDetail12") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Bad Access：<%# Eval("Item13") %></td>
                                        <td colspan="2" class="td_bottom_right">Origin: <%# Eval("ItemValue13") %> </td>
                                        <td class="td_bottom">Destination：<%# Eval("ItemData13") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td class="td_bottom_right">Storage：<%# Eval("Item14") %></td>
                                        <td class="td_bottom_right">Complimentory：<%# Eval("ItemValue14") %></td>
                                        <td class="td_bottom_right">Details：<%# Eval("ItemDetail14") %></td>
                                        <td class="td_bottom">Charges S$： <%# Eval("ItemPrice14") %></td>
                                    </tr>
                                    <tr style="text-align: left; vertical-align: top; height:125px">
                                        <td class="td_bottom" colspan="4">
                                            Notes：<%# Eval("Notes") %>
                                        </td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4" class="td_bottom">How did you come to know about our Company:<%# Eval("Answer1") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4" class="td_bottom">Other:<%# Eval("Answer2") %></td>
                                    </tr>
                                    <tr class="tr">
                                        <td colspan="4">Move Competitors:<%# Eval("Answer3") %></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <!-- International Move End-->
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>

            </table>
    </div>
                <!--endprint-->
    </form>
</body>
</html>
