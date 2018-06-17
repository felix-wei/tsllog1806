<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportVolumeByPort.aspx.cs" Inherits="ReportFreightSea_ExportVolumeByPort" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v12.2" Namespace="DevExpress.Web.ASPxPopupControl"
    TagPrefix="dxpc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bill List</title>

    <script type="text/javascript">
        function Print() {
            var refType = cmb_RefType.GetValue();
            var jobType = cmb_JobType.GetValue();
            var frm = frmDate.GetText();
            var party = cmb_Port.GetValue();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=35&docType=0&d1=' + frm + '&d2=' + to + '&refType=' + refType + '&type=' + jobType + '&p=' + party)
        }
        function ExportToExcel() {
            var refType = cmb_RefType.GetValue();
            var jobType = cmb_JobType.GetValue();
            var party = cmb_Port.GetValue();
            var frm = frmDate.GetText();
            var to = toDate.GetText();
            parent.PrintReport('/ReportFreightSea/Rptprintview.aspx?doc=35&docType=1&d1=' + frm + '&d2=' + to + '&refType=' + refType + '&type=' + jobType + '&p=' + party)
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
         <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXPort" KeyMember="Code" FilterExpression="isnull(Code,'')<>''" />
       <table>
            <tr>
                <td>
                    Type
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_RefType" ClientInstanceName="cmb_RefType" runat="server"
                        Width="150">
                        <Items>
                            <dxe:ListEditItem Text="ALL" Value="" />
                            <dxe:ListEditItem Text="Export" Value="SE" />
                            <dxe:ListEditItem Text="CrossTrade" Value="SC" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    Job Type
                </td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_JobType" ClientInstanceName="cmb_JobType" runat="server"
                        Width="150">
                        <Items>
                            <dxe:ListEditItem Text="ALL" Value="" />
                            <dxe:ListEditItem Text="FCL" Value="FCL" />
                            <dxe:ListEditItem Text="LCL" Value="LCL" />
                            <dxe:ListEditItem Text="CONSOL" Value="CONSOL" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    From Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_From" ClientInstanceName="frmDate" EditFormat="Custom"
                        EditFormatString="dd/MM/yyyy" Width="150" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
            <tr>
                <td>
                    To Date
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="date_End" ClientInstanceName="toDate" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                        Width="150" DisplayFormatString="dd/MM/yyyy" runat="server">
                    </dxe:ASPxDateEdit>
                </td>
            </tr>
        </table>
            <table>
                <tr>
                    <td>
                        Port
                    </td>
                    <td>                  
                        <dxe:ASPxComboBox ID="cmb_Port" ClientInstanceName="cmb_Port" runat="server"
                        Width="190" DropDownWidth="190" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="Code" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true"
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" Width="55px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                        <Buttons>
                            <dxe:EditButton Text="Reset"></dxe:EditButton>
                        </Buttons>
                        <ClientSideEvents buttonclick="function(s, e) {
                            if(e.buttonIndex == 0){
                                s.SetText('NA');
                         }
                        }" 
                        />
                    </dxe:ASPxComboBox>

                    </td>
                </tr>
            <tr>
                <td colspan="2">
                    <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Print" Width="120" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        Print();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Export To Excel" Width="120"
                        AutoPostBack="False" UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                        ExportToExcel();
                                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
            </table>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="600" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
