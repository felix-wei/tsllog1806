<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPL.aspx.cs" Inherits="WareHouse_Report_ProductPL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

</head>
<body>
    
    <wilson:DataSource ID="dsProduct" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProduct" KeyMember="Id" />
    <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.RefProductClass" KeyMember="Id" />
    <form id="form1" runat="server">
        <div>
            <table width="450">
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="Productlass">
                        </dxe:ASPxLabel> 
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cb_ProductClss" runat="server" DataSourceID="dsProductClass" TextField="Code" ValueField="Code" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Width="100%">
                            <Columns>
                                <dxe:ListBoxColumn  Caption="Code"  FieldName="Code"/>
                            </Columns>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                    </td>
                    <td><dxe:ASPxButton ID="btnPrint" runat="server" Text="Export To Excel" Width="120" AutoPostBack="False" OnClick="btnPrint_Click">
                                    </dxe:ASPxButton></td>
                </tr>
            </table>
            <div>
                <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsProduct"
                    KeyFieldName="Id" AutoGenerateColumns="False"
                    Width="1000px" Theme="DevEx">
                    <SettingsPager Mode="ShowAllRecords">
                    </SettingsPager>
                    <SettingsEditing Mode="EditForm" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior ConfirmDelete="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="Id" VisibleIndex="1" Visible="false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ProductClass" FieldName="ProductClass" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="ProductType" FieldName="ProductType" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="StatusCode" FieldName="StatusCode" VisibleIndex="2" Width="150">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" VisibleIndex="3">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CreateBy" FieldName="CreateBy" VisibleIndex="4" Width="100">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="CreateDateTime" FieldName="CreateDateTime" VisibleIndex="5" Width="100">
                            <EditFormSettings Visible="False" />
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    
                    <SettingsPager Mode="ShowPager"></SettingsPager>
                    <Styles Header-HorizontalAlign="Center">
                        <Header HorizontalAlign="Center"></Header>
                        <Cell HorizontalAlign="Center"></Cell>
                    </Styles>
                </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                    HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                    AllowResize="True" Width="600" EnableViewState="False">
                </dxpc:ASPxPopupControl>
            </div>
        </div>

    </form>
</body>
</html>
