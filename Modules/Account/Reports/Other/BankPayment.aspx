<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankPayment.aspx.cs" Inherits="PagesAccount_Other_BankPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Payment</title>

    <script type="text/javascript">
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
        }
        function ClearAll() {
            //jQuery("INPUT[type='checkbox']").attr('checked', true);
            //jQuery("INPUT[type='checkbox']").next(".Idate input").val(dateClear.GetText());
            jQuery("INPUT[type='checkbox']").each(function(ind) {
                if (!this.checked) {
                    this.click();
                    var el = jQuery(".Idate INPUT[type='text']")[ind];
                    jQuery(el).val(dateClear.GetText());
                }
            });
            // jQuery(".Idate input").val(dateClear.GetText());
        }
        function UnClearAll() {
            jQuery("INPUT[type='checkbox']").each(function() {
                if (this.checked)
                    this.click();
            });
            jQuery(".Idate input").val("01/01/1900");
        }

        function Save() {
            var s = "";
            jQuery(".Idate input").each(function() {
                s += this.value + ",";
            });
            grid.GetValuesOnCustomCallback(s, OnCallback);
        }
    </script>

    <script type="text/javascript" src="../../Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsArReceipt" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XAArReceipt" KeyMember="SequenceId" FilterExpression="1=0" />
        <wilson:DataSource ID="dsAcCode" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChartAcc" KeyMember="SequenceId" FilterExpression="AcBank='Y'" />
        <table border="0">
            <tr>
                <td>
                    From
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" ClientInstanceName="frm" runat="server"
                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    To
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" ClientInstanceName="to" runat="server"
                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    Chart of Account
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_acCode" ClientInstanceName="cmb_acCode" runat="server"
                        Width="250px" DropDownWidth="250" DropDownStyle="DropDownList" DataSourceID="dsAcCode"
                        ValueField="Code" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="35px" />
                            <dxe:ListBoxColumn FieldName="AcDesc" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
               <td>
                    <dxe:ASPxButton ID="btn_Add" runat="server" Text="Retrieve" OnClick="btn_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td>
                    Clear Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="dateClear" Width="100" ClientInstanceName="dateClear" runat="server"
                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                </td>
                <td colspan="4">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                                   ClearAll();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="UnSelect All" Width="110" AutoPostBack="False"
                                    UseSubmitBehavior="False">
                                    <ClientSideEvents Click="function(s, e) {
                                   UnClearAll();
                                    }" />
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
                     <table style="border-bottom:solid 1px black;">
                        <tr>
                            <td width="100">
                                Payment No
                            </td>
                            <td width="100">
                                Payment Date
                            </td>
                            <td width="100">
                                Cheque No
                            </td>
                            <td width="100">
                                Cheque Date
                            </td>
                            <td width="60">
                            Cleared
                            </td>
                            <td width="100">Cleared Date
                            </td>
                        </tr>
                    </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="600"
            KeyFieldName="Oid" AutoGenerateColumns="False" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
            OnCustomCallback="ASPxGridView1_CustomCallback">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings ShowColumnHeaders="false" />
            <SettingsBehavior AllowFocusedRow="True" />
            <Templates>
                <DataRow>
                    <table style="border-bottom:solid 1px black;">
                        <tr>
                            <td width="100">
                                <%# Eval("DocNo") %>(<%# Eval("DocType")%>)
                                <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_oid" runat="server" Text='<%# Eval("SequenceId") %>'>
                                </dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="txt_docType" runat="server" Text='<%# Eval("DocType") %>'>
                                </dxe:ASPxTextBox>
                                </div>
                            </td>
                            <td width="100">
                                <%# Eval("DocDate", "{0:dd/MM/yyyy}")%>
                            </td>
                            <td width="100">
                                <%# Eval("ChqNo") %>
                            </td>
                            <td width="100">
                                <%# Eval("ChqDate","{0:dd/MM/yyyy}") %>
                            </td>
                            <td width="60">
                                <dxe:ASPxCheckBox ID="ck_bankRec" runat="server" Width="55" Checked='<%# Eval("BankRec") %>'>
                                </dxe:ASPxCheckBox>
                            </td>
                            <td width="100">
                                <dxe:ASPxDateEdit ID="date_bankDate" runat="server" Width="95" EditFormat="Date" CssClass="Idate"
                                    EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Value='<%# Eval("BankDate") %>'>
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </DataRow>
            </Templates>
        </dxwgv:ASPxGridView>
        <dxe:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
            Text="Save">
            <ClientSideEvents Click="function(s, e) {
                                          Save();}" />
        </dxe:ASPxButton>
    </div>
    </form>
</body>
</html>
