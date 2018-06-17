<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowContPermit.aspx.cs" Inherits="PagesContTrucking_SelectPage_ShowContPermit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dxwgv:ASPxGridView runat="server" ID="grid_permit" ClientInstanceName="grid_permit"
            Width="1000px" KeyFieldName="Id" AutoGenerateColumns="false">
            <SettingsPager Mode="ShowPager" PageSize="10" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Permit No" FieldName="PermitNo" VisibleIndex="1"
                    Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="HblNo" Caption="Hbl No" Width="120" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Permit By" FieldName="PermitBy" VisibleIndex="1"
                    Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn Caption="Permit Date" FieldName="PermitDate" VisibleIndex="1" Width="120px">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataTextColumn Caption="IncoTerms" FieldName="IncoTerms" VisibleIndex="1"
                    Width="100">
                </dxwgv:GridViewDataTextColumn>

                <dxwgv:GridViewDataSpinEditColumn Caption="GstAmt" FieldName="GstAmt" VisibleIndex="1" Width="120px">
                    <PropertiesSpinEdit NumberType="Float" DecimalPlaces="2" DisplayFormatString="0.00" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="Supp. InvNo" FieldName="PartyInvNo" VisibleIndex="1"
                    Width="150">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Payment Status" FieldName="PaymentStatus" VisibleIndex="1"
                    Width="150">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
