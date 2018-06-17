<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="edi_bill.aspx.cs"
    Inherits="edi_bill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Import</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <script type="text/javascript">
        function PrintIm(no) {
            window.open("/rpt/RptPrintView.aspx?doc=95&no=" +no);
        }
        function PrintDo(lineN) {
            window.open("/rpt/RptPrintView.aspx?doc=94&no=" + lineN);
        }
        function DownLoad(refNo) {
            if (confirm("Confirm download this bill?")) {
            refNo_tmp=refNo;
                grd.GetValuesOnCustomCallback(refNo,OnCallback)
            }
        }
        var refNo_tmp="";
        function OnCallback(v) {
        alert(v);
          if(v.indexOf("Already Download")>-1)
          {
           if (confirm("Confirm redownload this bill?")) {
                grd.GetValuesOnCustomCallback("re"+refNo_tmp,OnCallback1)
            }
          }//else
           // alert(v);
        }
        function OnCallback1(v) {
            alert(v);
        }
    </script>

    <table>
        <tr>
            <td>
                Invoice No
            </td>
            <td>
                <dxe:ASPxTextBox ID="txt_ExpRefNo" ClientInstanceName="txt_Sn" Width="150" runat="server"
                    Text="">
                </dxe:ASPxTextBox>
            </td>
            <td>
                Job No
            </td>
            <td>
                <dxe:ASPxTextBox ID="txt_ContNo" Width="150" runat="server" Text="">
                </dxe:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Date From
            </td>
            <td>
                <dxe:ASPxDateEdit ID="txt_from" Width="150" ClientInstanceName="txt_form" runat="server"
                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
            <td>
                To
            </td>
            <td>
                <dxe:ASPxDateEdit ID="txt_end" Width="150" ClientInstanceName="txt_end" runat="server"
                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
            <td>
                <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <dxwgv:ASPxGridView ID="grd" ClientInstanceName="grd" runat="server" Width="900"
        KeyFieldName="TrxNo" OnCustomDataCallback="grd_CustomDataCallback">
        <SettingsPager Mode="ShowAllRecords" />
        <SettingsDetail ShowDetailRow="false" />
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="DownLoad" VisibleIndex="0" Width="5%">
                <DataItemTemplate>
                    <a onclick='DownLoad("<%# Eval("SequenceId") %>");'>DownLoad</a>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataColumn FieldName="DocNo" VisibleIndex="1">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="DocType" VisibleIndex="2">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="DocDate" VisibleIndex="3">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="DocAmt" VisibleIndex="4">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="LocAmt" VisibleIndex="5">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="BalanceAmt" VisibleIndex="5">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="MastRefNo" VisibleIndex="6">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="JobRefNo" VisibleIndex="7">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="MastType" VisibleIndex="7">
            </dxwgv:GridViewDataColumn>
        </Columns>
    </dxwgv:ASPxGridView>
 
    </form>
</body>
</html>
