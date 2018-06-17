<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankList.aspx.cs" Inherits="PagesMaster_BankList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
            <script type="text/javascript" src="/Script/pages.js"></script>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <%--<td>
                        <dxe:ASPxLabel ID="lab_PoNo" runat="server" Text="Name">
                        </dxe:ASPxLabel>

                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LcNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>--%>
<%--                     <td>
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
                    </td>--%>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Refresh" >
                            <ClientSideEvents  Click="function(s,e){
                                detailGrid.Refresh();
                                grid_Active.Refresh();
                                }"/>
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server" OnCellEditorInitialize="grid_CellEditorInitialize" OnRowUpdating="grid_RowUpdating"
                KeyFieldName="SequenceId" Width="100%" AutoGenerateColumns="False" Styles-Cell-HorizontalAlign="Left">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="Inline"/>
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>                      
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="40" Visible="false">
                        <EditButton Visible="true" Text="Edit"></EditButton>
                    </dxwgv:GridViewCommandColumn>       
                    <dxwgv:GridViewDataTextColumn Caption="Bank Name" FieldName="Code" VisibleIndex="1" Width="200">
                        <EditItemTemplate>
                                <dxe:ASPxLabel ID="txt_Code" Width="100%" runat="server" Text='<%# Eval("Code")%>' ></dxe:ASPxLabel>
                            </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Credit Limit" FieldName="WarningAmt" VisibleIndex="1" Width="200">
                    </dxwgv:GridViewDataTextColumn>
                     <dxwgv:GridViewDataTextColumn Caption="RemainingLimit" FieldName="RemainingLimit" VisibleIndex="1" Width="200">
                         <EditItemTemplate>
                                <dxe:ASPxLabel ID="txt_RemainingLimit" Width="100%" runat="server" Text='<%# Eval("RemainingLimit")%>' ></dxe:ASPxLabel>
                            </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>

                <SettingsDetail  ShowDetailRow="true"/>
                <Templates>
                    <DetailRow>
                        <dxwgv:ASPxGridView ID="grid_Active" ClientInstanceName="grid_Active" runat="server"
                            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="grid_Active_BeforePerformDataSelect">
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Columns>
                                <dxwgv:GridViewDataTextColumn Caption="Order No" FieldName="DoNo" VisibleIndex="1" Width="100">
                                    <DataItemTemplate>
                                        <a href='javascript: parent.navTab.openTab("<%# Eval("LcNo") %>","/PagesMaster/BuyingLCEdit.aspx?no=<%# Eval("LcNo") %>",{title:"<%# Eval("LcNo") %>", fresh:false, external:true});'><%# Eval("LcNo") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="App Date" FieldName="LcAppDate" VisibleIndex="2" Width="60">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ExpirtDate" FieldName="LcExpirtDate" VisibleIndex="2" Width="60">
                                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="PoNo" FieldName="LcRef" VisibleIndex="3" Width="100">
                                    <DataItemTemplate>
                                        <a href='javascript: parent.navTab.openTab("<%# Eval("LcRef") %>","/Warehouse/Job/PoEdit.aspx?no=<%# Eval("LcRef") %>",{title:"<%# Eval("LcRef") %>", fresh:false, external:true});'><%# Eval("LcRef") %></a>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="EntityName" FieldName="LcEntityName" VisibleIndex="4" Width="200">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="BeneName" FieldName="LcBeneName" VisibleIndex="4" Width="200">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Total LC Amount" FieldName="LcAmount" VisibleIndex="4" Width="40">
                                </dxwgv:GridViewDataTextColumn>
                                 <dxwgv:GridViewDataTextColumn Caption="RemainingLimit" FieldName="RemainingLimit" VisibleIndex="4" Width="200" Visible="false">
                                 </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="LcAmount" SummaryType="Sum" DisplayFormat="{0:00.00}"/>
                            </TotalSummary>
                        </dxwgv:ASPxGridView>
                    </DetailRow>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
