<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoteList_new.aspx.cs" Inherits="PagesFreight_Account_QuoteList_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quotation</title>
    <script type="text/javascript">
        function OnCallBack(v) {
            if (v == "Fail") {
                alert("Create Invoice Fail,please try again!");
            }
            else {
                window.location='ArInvoiceEdit.aspx?no=' + v;
            }
        }
        function CreateInvoice(quoteId) {
            grid_Sch.GetValuesOnCustomCallback(quoteId, OnCallBack);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div> <dxe:ASPxButton ID="btn_search" Width="140" runat="server" Text="Add Normal Invoice" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
        <dxwgv:ASPxGridView ID="grid_Sch" ClientInstanceName="grid_Sch" runat="server"
            KeyFieldName="SequenceId" Width="750" OnCustomDataCallback="grid_CustomDataCallback" >
            <SettingsPager PageSize="4" />
            <Columns>
                <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="1" Width="50">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn Caption="quote No" VisibleIndex="1" Width="50">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataDateColumn Caption="Party To" VisibleIndex="1">
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataColumn Caption="Expire Date" Visible="true" VisibleIndex="2" Width="100">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn Caption="Currency" VisibleIndex="3" Width="100">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn Caption="Pol" VisibleIndex="4" Width="100">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn Caption="Pod" VisibleIndex="4" Width="100">
                </dxwgv:GridViewDataColumn>
            </Columns>
            <Templates>
                <DataRow>
                    <div style="padding: 5px">
                        <table width="750" style="border-bottom: solid 1px black;">
                            <tr style="font-weight: bold; font-size: 11px">
                                <td style="width: 50px">
                                    <a onclick='CreateInvoice(<%# Eval("SequenceId") %>)'>Select</a>
                                </td>
                                <td style="width: 80px">
                                    <%# Eval("QuoteNo")%>
                                </td>
                                <td style="width: 200px">
                                    <%# Eval("PartyName")%>
                                </td>
                                <td style="width: 100px">
                                    <%# Eval("ExpireDate", "{0:dd/MM/yyyy}")%>
                                </td>
                                <td style="width: 100px">
                                    <%# Eval("CurrencyId")%>
                                </td>
                                <td style="width: 100px">
                                    <%# Eval("Pol")%>
                                </td>
                                <td style="width: 100px">
                                    <%# Eval("Pod")%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </DataRow>
            </Templates>
        </dxwgv:ASPxGridView>        
    </div>
    </form>
</body>
</html>
