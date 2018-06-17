<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintEmployeeSimplify.aspx.cs" Inherits="Modules_Hr_Report_PrintEmployeeSimplify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <link href="/Modules/Hr/Css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">

        .td_top_left_bottom {
            border-top:1px solid #666;
            border-left:1px solid #666;
            border-bottom:1px solid #666;
            padding:0px;
            margin:0px;
        }
        .td_left_bottom {
            border-left:1px solid #666;
            border-bottom:1px solid #666;
            padding:0px;
            margin:0px;
        }
        .td_top_left_bottom_right {
            border-top:1px solid #666;
            border-left:1px solid #666;
            border-bottom:1px solid #666;
            border-right:1px solid #666;
            padding:0px;
            margin:0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>Date</td>
                <td>
                    <dxe:ASPxDateEdit ID="date_Employee" Width="120" runat="server" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                     <dxe:ASPxButton ID="btn_export" Width="120" runat="server" Text="Save Excel" OnClick="btn_export_Click">
                        </dxe:ASPxButton>
                </td>
            </tr>
            
        </table>
        <div class="table">
            <table  cellpadding="0" cellspacing="0" style="margin-top:50px;width:1400px;" >
                <tr style="text-align: left; padding: 0;">
                    <td colspan="13">&nbsp;<%=PrintDate() %> IR8A</td>
                </tr>
                <tr>
                    <td height="40px" colspan="13"></td>
                </tr>
                <tr style="text-align: center;">
                    <td width="30px">&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="td_top_left_bottom_right" rowspan="3" width="120px">Total</td>
                    <td colspan="3" ></td>
                </tr>
                <tr style="text-align: center;">
                    <td rowspan="2" class="td_top_left_bottom">S/N</td>
                    <td rowspan="2" class="td_top_left_bottom" width="260px">Employee</td>
                    <td class="td_top_left_bottom" width="160px" rowspan="2">NRIC No. /FIN No.<br />
                        /WP No.</td>
                    <td rowspan="2" class="td_top_left_bottom" width="100px">Nationality</td>
                    <td rowspan="2" class="td_top_left_bottom" width="100px">Sex</td>
                    <td rowspan="2" class="td_top_left_bottom" width="300px">Designation</td>
                    <td rowspan="2" class="td_top_left_bottom" width="120px">Date Joined</td>
                    <td rowspan="2" class="td_top_left_bottom" width="120px">Date Left</td>
                    <td rowspan="2" class="td_top_left_bottom" width="120px">Date of Birth</td>
                    <td colspan="3" class="td_left_bottom">&nbsp;</td>
                </tr>
                <tr style="text-align: center;">
                    <td class="td_left_bottom"  width="120px" style="background-color:#d1cdcd;">CPF</td>
                    <td class="td_left_bottom"  width="120px">MBMF</td>
                    <td class="td_top_left_bottom_right" width="120px">SINDA</td>
                </tr>
                <% 
                    string dateTo = date_Employee.Date.ToString("yyyy-MM-dd");
                    DataTable dt = GetData(dateTo);
                   
                    decimal totalSalary = 0;
                    
                    
                    for (int i=0;i<dt.Rows.Count;i++){
                         
                 %>
                <tr style="text-align: center;">
                    <td class="td_top_left_bottom" style="text-align:center"><%=i+1 %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Name"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["IcNo"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Country"] %> </td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["Gender"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["HrRole"] %></td>
                    <td class="td_top_left_bottom"><%=dt.Rows[i]["BeginDate"] %></td>
                    <td class="td_top_left_bottom"> <%=dt.Rows[i]["ResignDate"] %></td>
                    <td class="td_top_left_bottom"> <%=dt.Rows[i]["BirthDay"] %></td>
                    <td class="td_top_left_bottom" style="text-align:right"><%=dt.Rows[i]["TotalAmt"] %></td>
                    <td class="td_top_left_bottom" style="background-color:#d1cdcd;">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="20px">$</td>
                                <td width="100%"><%=SafeAccountSz(dt.Rows[i]["CPF2"]) %></td>
                            </tr>
                        </table>
                    </td>
                    <td class="td_top_left_bottom"><%=SafeAccountSz(dt.Rows[i]["Account5"]) %></td>
                    <td class="td_top_left_bottom"><%=SafeAccountSz(dt.Rows[i]["Account6"]) %></td>
                </tr>
                <%} %>
                
            </table>
        </div>
    </form>
</body>
</html>
