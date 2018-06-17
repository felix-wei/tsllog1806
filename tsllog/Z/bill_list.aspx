<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bill_list.aspx.cs" Inherits="Z_bill_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <title></title>
    <script type="text/javascript">
        function ShowArInvoice(invN) {
            parent.navTab.openTab(invN, "/Z/bill_edit.aspx?no=" + invN, { title: invN, fresh: false, external: true });
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
                txt_TermId.SetText();
                ASPxPopupClientControl.Hide();
                parent.navTab.openTab(v, '/Z/bill_edit.aspx?no=' + v, { title: v, fresh: false, external: true });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsArInvoice" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.bill_doc" KeyMember="SequenceId" FilterExpression="1=0" />
            <wilson:DataSource ID="dsCustomerMast" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXParty" KeyMember="SequenceId" />
            <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXTerm" KeyMember="SequeceId" />
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
                            </tr>
                            <tr>
                                <td>Customer
                                </td>
                                <td colspan="3">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <dxe:ASPxComboBox runat="server" ID="cmb_Customer" ClientInstanceName="cmb_Customer" DataSourceID="dsCustomerMast" ValueField="PartyId" TextField="Name"
                                                    Width="405" EnableCallbackMode="true" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" OnCustomJSProperties="cmb_Customer_CustomJSProperties">
                                                    
                                                    <ClientSideEvents SelectedIndexChanged="function ClickHandler(s, e) {
                                                    txt_TermId.SetValue(cmb_Customer.cpTerm[s.GetSelectedIndex()]);}" />
                                                </dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>Term
                                </td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txt_TermId" ClientInstanceName="txt_TermId"
                                        DataSourceID="dsTerm" TextField="Code" ValueField="Code" Width="100" ValueType="System.String">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td align="right" colspan="2">
                                    <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="100" UseSubmitBehavior="false" Text="Save" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                               grid.GetValuesOnCustomCallback('Save',OnSaveCallBack);
                                            }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <table border="0">
                <tr>
                    <td>Doc No
                    </td>
                    <td>
                        <asp:TextBox ID="txt_refNo" Width="120" runat="server"></asp:TextBox>
                    </td>
                    <td>Party To
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
                            <Buttons>
                                <dxe:EditButton Text="Clear"></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            if(e.buttonIndex == 0){
                                s.SetText('');
                         }
                        }" />
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Date From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
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
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="dsArInvoice"
                ClientInstanceName="grid" Width="900" KeyFieldName="SequenceId" OnCustomDataCallback="ASPxGridView1_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick='ShowArInvoice("<%# Eval("DocNo") %>")'>Edit</a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DocNo" FieldName="DocNo" VisibleIndex="1"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DocType" FieldName="DocType" VisibleIndex="3"
                        Width="5%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="PartyTo" FieldName="PartyName" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="DocDate" FieldName="DocDate" VisibleIndex="5"
                        Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:dd/MM/yyyy}">
                        </PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="6"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ExRate" FieldName="ExRate" VisibleIndex="7"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Doc Amt" FieldName="DocAmt" VisibleIndex="7" 
                        Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Loc Amt" FieldName="LocAmt" VisibleIndex="8"
                        Width="8%">
                        <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CancelInd" FieldName="CancelInd" VisibleIndex="10"
                        Width="8%">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>

        </div>
    </form>
</body>
</html>
