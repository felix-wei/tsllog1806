<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportRefSelect.aspx.cs" Inherits="Pages_Export_ExportRefSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export Ref</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Ref No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_RefNo" Width="100" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    Ocean BL
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_Obl" Width="100" runat="server">
                    </dxe:ASPxTextBox>
                </td>
               <td colspan="4">
                    <table>
                        <tr>
                            <td>
                                <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                </dxe:ASPxButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="dsExportRef" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaExportRef" KeyMember="SequenceId" FilterExpression="1=0" />
        <dxwgv:ASPxGridView ID="grid_ref" ClientInstanceName="grid_ref" runat="server" KeyFieldName="SequenceId"
            Width="900px" AutoGenerateColumns="False" DataSourceID="dsExportRef" >
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsPager PageSize="8">
            </SettingsPager>
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="900" PopupEditFormHorizontalAlign="WindowCenter"
                PopupEditFormVerticalAlign="WindowCenter" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0"
                    SortIndex="1">
                    <DataItemTemplate>
                        <a onclick='parent.PutRefNo("<%# Eval("RefNo") %>");'>
                            Select</a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Ref No" FieldName="RefNo" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Vessel" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Voyage" FieldName="Voyage" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Ocean BL" FieldName="OblNo" VisibleIndex="4">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="POD" FieldName="Pod" VisibleIndex="5">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn Caption="Eta" FieldName="Eta" VisibleIndex="6">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                    </PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        
    </div>
    </form>
</body>
</html>
