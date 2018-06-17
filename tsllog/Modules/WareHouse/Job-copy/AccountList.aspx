<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountList.aspx.cs" Inherits="WareHouse_Job_AccountList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <script type="text/javascript" src="/Script/Wh/WareHouse.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script type="text/javascript" src="/Script/Sea/Acc.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lab_PoNo" runat="server" Text="DocNo">
                        </dxe:ASPxLabel>

                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PoNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="From">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
<%--                    <td><dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Type">
                        </dxe:ASPxLabel></td>
                    <td> <dxe:ASPxComboBox ID="cmb_Type" runat="server" Width="100px" DropDownStyle="DropDown" IncrementalFilteringMode="StartsWith" >
                            <Items>
                                <dxe:ListEditItem Text="Draft" Value="Draft" />
                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                <dxe:ListEditItem Text="Closed" Value="Closed" />
                                <dxe:ListEditItem Text="Canceled" Value="Canceled" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Customer">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120px" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                             PopupParty(txt_CustId,txt_CustName,null,null,null,null,null,null,'C','');
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="100%" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server"
            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Order No" VisibleIndex="0" SortIndex="1" SortOrder="Descending" Width="100">
                    <DataItemTemplate>
                        <a href='javascript: parent.navTab.openTab("<%# Eval("DocNo") %>","/OpsAccount/ArInvoiceEdit.aspx?no=<%# Eval("DocNo") %>",{title:"<%# Eval("DocNo") %>", fresh:false, external:true});'><%# Eval("DocNo") %></a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Doc Type" FieldName="DocType" VisibleIndex="1" Width="30">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Doc Date" FieldName="DocDate" VisibleIndex="2" Width="70">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataTextColumn Caption="Sales Order No" VisibleIndex="0" SortIndex="1" SortOrder="Descending" Width="100">
                    <DataItemTemplate>
                        <a href='javascript: parent.navTab.openTab("<%# Eval("MastRefNo") %>","/Modules/WareHouse/Job/SoEdit.aspx?no=<%# Eval("MastRefNo") %>",{title:"<%# Eval("MastRefNo") %>", fresh:false, external:true});'><%# Eval("MastRefNo") %></a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Party To" FieldName="PartyName" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Currency" FieldName="CurrencyId" VisibleIndex="4" Width="50">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="DocAmt" FieldName="DocAmt" VisibleIndex="5" Width="50">
                    <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="LocAmt" FieldName="LocAmt" VisibleIndex="6" Width="50">
                    <PropertiesTextEdit DisplayFormatString="{0:#,##0.00}">
                    </PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFooter="True" />
            <TotalSummary>
                <dxwgv:ASPxSummaryItem FieldName="DocNo" SummaryType="Count" DisplayFormat="{0}" />
                <dxwgv:ASPxSummaryItem FieldName="DocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}" />
                <dxwgv:ASPxSummaryItem FieldName="LocAmt" SummaryType="Sum" DisplayFormat="{0:00.00}" />
            </TotalSummary>
        </dxwgv:ASPxGridView>
          <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="1000" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                      if(isUpload)
	                    grd_Photo.Refresh();
                }" />
            </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
