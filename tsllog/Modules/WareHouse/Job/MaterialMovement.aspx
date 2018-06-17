<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialMovement.aspx.cs" Inherits="WareHouse_Job_MaterialMovement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="noprint">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="End :">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                        <div style="display: none">
                            <dxe:ASPxDateEdit ID="txt_date" ClientInstanceName="txt_date" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </div>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="lbl_Company" runat="server" Text="Company Name">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cmb_CompanyName" runat="server" Width="60px">
                            <Items>
                                <dxe:ListEditItem Text="SPJ" Value="SPJ" />      
                                <dxe:ListEditItem Text="MST" Value="MST" />                     
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="120" ClientInstanceName="btn_search" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_export" Width="120" runat="server" Text="Save To Excel" OnClick="btn_export_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
    <div>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="100%"
            KeyFieldName="Id" AutoGenerateColumns="False">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewDataTextColumn      Caption="Item Code" FieldName="Code" VisibleIndex="1" Width="80">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn      Caption="Description" FieldName="Description" VisibleIndex="1" Width="180">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn
                    Caption="Party Details" FieldName="PartyRef" VisibleIndex="1" Width="80">
                </dxwgv:GridViewDataTextColumn>
              
                 <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="DoDate" VisibleIndex="3" Width="100px">
                     <DataItemTemplate>
                         <%# R.Date3(Eval("DoDate")) %>
                     </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Do No" FieldName="DoNo" VisibleIndex="3" Width="40px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Bill No" FieldName="BillNo" VisibleIndex="3" Width="40px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Inwards" FieldName="InQty" VisibleIndex="3" Width="100px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Outwards" FieldName="OutQty" VisibleIndex="3" Width="100px">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Closing" FieldName="BalQty" VisibleIndex="3" Width="100px">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFooter="true" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Code" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
