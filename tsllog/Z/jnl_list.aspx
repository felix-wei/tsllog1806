<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jnl_list.aspx.cs" Inherits="Z_jnl_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Receipt</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var clientId = null;
        var clientName = null;
        var clientType = null;
        var clientAcCode = null;
        function PutValue(s, name, custType, acCode) {
            if (clientId != null) {
                clientId.SetText(s);
                if (clientName != null) {
                    clientName.SetText(name);
                }
                clientId = null;
                clientName = null;
                popubCtr.Hide();
                popubCtr.SetContentUrl('about:blank');
            }
        }
        function PopupCust(txtId, txtName, txtAcCode) {
            clientId = txtId;
            clientName = txtName;
            clientAcCode = txtAcCode;
            popubCtr.SetContentUrl('SelectPage/CustomerList.aspx');
            popubCtr.SetHeaderText('Customer');
            popubCtr.Show();
        }
        function ShowArReceipt(invN) {
            parent.navTab.openTab(invN, "/Z/jnl_edit.aspx?no=" + invN, { title: invN, fresh: false, external: true });
        }
        function SetPCVisible(doShow) {
            if (doShow) {
                ASPxPopupClientControl.Show();
            }
            else {
                ASPxPopupClientControl.Hide();
            }
        }
        function OnSaveCallBack(v) {
            if (v != null && v.length > 0) {
                cmb_Customer.SetText();
                cbo_DocType1.SetText("Job");
                cbo_DocType.SetText("RE");
                txt_AcDorc.SetText("DB");
                txt_OtherPartyName.SetText();
                ASPxPopupClientControl.Hide();
                parent.navTab.openTab(v, '/Z/jnl_edit.aspx?no=' + v, { title: v, fresh: false, external: true });
            }
        }
    </script>

</head>
<body width="800">
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsArReceipt" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.jnl_doc" KeyMember="Id" FilterExpression="1=0" />
          <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="IsCustomer='true' or IsAgent='true'" />
            <dxpc:ASPxPopupControl ID="ASPxPopupControl1" ClientInstanceName="ASPxPopupClientControl" SkinID="None" Width="240px"
                ShowOnPageLoad="false" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                AllowDragging="True" CloseAction="None" PopupElementID="popupArea"
                EnableViewState="False" runat="server" PopupHorizontalOffset="0"
                PopupVerticalOffset="0" EnableHierarchyRecreation="True">
                <HeaderTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">New Bill
                            </td>
                            <td>
                                <a id="a_X" onclick="SetPCVisible(false)" onmousedown="event.cancelBubble = true;" style="width: 15px; height: 14px; cursor: pointer;">X</a>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl runat="server">
                        <table align="center">
                            <tr>
                                <td>Doc Date
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="txt_DocDt" ClientInstanceName="txt_DocDt" runat="server" Width="100"
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>Category
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="cbo_DocType1" ClientInstanceName="cbo_DocType1" Width="100" Text='<%# Eval("DocType1")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="Job" Text="Job" />
                                            <dxe:ListEditItem Value="General" Text="General" />
                                            <dxe:ListEditItem Value="Refund" Text="Refund" />
                                        </Items>
                                        <ClientSideEvents ValueChanged='function(s,e){
                                        if(s.GetValue()=="Refund")
                                        {
                                        txt_AcDorc.SetValue("CR");
                                        cbo_DocType.SetValue("PC");
                                        }
                                        else{
                                        txt_AcDorc.SetValue("DB");
                                        cbo_DocType.SetValue("RE");
                                        }
                                        }' />
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Customer
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxComboBox runat="server" ID="cmb_Customer" ClientInstanceName="cmb_Customer" DataSourceID="dsCustomerMast" ValueField="PartyId" TextField="Name"
                                        Width="380" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:150px">OtherName
                                </td>
                                <td colspan="3">
                                    <dxe:ASPxTextBox runat="server" ID="txt_OtherPartyName" ClientInstanceName="txt_OtherPartyName" Width="380">
                                    </dxe:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="4">
                                    <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="100" UseSubmitBehavior="false" Text="Save" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                               ASPxGridView1.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="txt_AcDorc" ClientInstanceName="txt_AcDorc" Width="22" BackColor="Control" ReadOnly="true"
                                        Text='<%# Eval("AcSource")%>' Border-BorderWidth="0">
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="cbo_DocType" ClientInstanceName="cbo_DocType" Width="100" Text='<%# Eval("DocType")%>'>
                                        <Items>
                                            <dxe:ListEditItem Value="RE" Text="RE" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
       <table border="0">
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Receipt No"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txt_refNo" Width="120" runat="server"></asp:TextBox>
                </td>
                <td>
                    Party To
                </td>
                <td colspan="3">
                    <dxe:ASPxComboBox ID="cmb_PartyTo" ClientInstanceName="cmb_PartyTo" runat="server"
                        Width="100%" DropDownWidth="380" DropDownStyle="DropDownList" DataSourceID="dsCustomerMast"
                        ValueField="PartyId" ValueType="System.String" TextFormatString="{1}" EnableCallbackMode="true" 
                        EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" CallbackPageSize="100">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="PartyId" Caption="ID" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" Width="100%" />
                        </Columns>
                    </dxe:ASPxComboBox>
                </td>
                <td>Cheque No</td>
                <td>
                    <asp:TextBox ID="txt_ChqNo" Width="100" runat="server"></asp:TextBox></td>
                <td>
                    PostInd
                </td>
                <td>
                                <dxe:ASPxComboBox runat="server" ID="cbo_PostInd" width="50" >
                                    <Items>
                                        <dxe:ListEditItem Value="All" Text="All"/>
                                        <dxe:ListEditItem Value="Y" Text="Y" />
                                        <dxe:ListEditItem Value="N" Text="N" />
                                    </Items>
                                </dxe:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td>Invoice No</td>
                <td>
                    <asp:TextBox ID="txt_InvNo" Width="120" runat="server"></asp:TextBox></td>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Date From"></asp:Label>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="To"></asp:Label>
                </td>
                <td>
                    <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                    </dxe:ASPxDateEdit>
                </td>
                <td colspan="4">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <table style="width: 100px; height: 20px;" id="popupArea">
                                    <tr>
                                        <td id="addnew" style="text-align: center; vertical-align: middle;">
                                            <dxe:ASPxButton ID="btn_AddNew" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                                            </dxe:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="display:none">
                                <dxe:ASPxButton ID="btn_Add" runat="server" AutoPostBack="false" UseSubmitBehavior="false"
                                    Text="Add Receipt" Width="110">
                                    <ClientSideEvents Click="function(s, e) {
    ShowArReceipt('0');}" />
                                </dxe:ASPxButton>
                            </td>
                            <td>
                                <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" OnCustomDataCallback="ASPxGridView1_CustomDataCallback"
            DataSourceID="dsArReceipt" Width="800" KeyFieldName="Id" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                    <DataItemTemplate>
                        <a onclick="ShowArReceipt('<%# Eval("DocNo") %>');">Edit</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Catetory" FieldName="DocType1" VisibleIndex="3"
                    Width="5%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                    Width="8%">
                    <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="DocCurrency" VisibleIndex="6"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="DocExRate" VisibleIndex="7"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="8"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="9"
                    Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                        </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="PostInd" FieldName="ExportInd" VisibleIndex="90"
                    Width="8%">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
        HeaderText="Ar Receipt Edit" AllowDragging="True" EnableAnimation="False" Height="400"
        Width="600" EnableViewState="False">
        <contentcollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </contentcollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
