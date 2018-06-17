<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileControl.aspx.cs" Inherits="Mobile_MobileControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="DataSource1" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Role" KeyMember="SequenceId" />
            <wilson:DataSource ID="DataSource2" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.MobileControl" KeyMember="Id" />
            <table>
                <tr>
                    <td>Role</td>
                    <td>
                        <dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" DataSourceID="DataSource1" TextField="Code"
                            ValueField="Code" ValueType="System.String" Width="160" OnSelectedIndexChanged="ASPxComboBox1_SelectedIndexChanged" AutoPostBack="true">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton2" Width="100" runat="server" Text="Quickly Update" OnClick="ASPxButton2_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>

            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="DataSource2" KeyFieldName="Id" Width="100%" EnableRowsCache="False" AutoGenerateColumns="false" OnBeforePerformDataSelect="grid_BeforePerformDataSelect">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsEditing Mode="Inline" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="20%">
                        <EditButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="Code" VisibleIndex="1" ReadOnly="true" Width="20%">
                    </dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn FieldName="Type" VisibleIndex="2" ReadOnly="true" Width="20%" />
                    <dxwgv:GridViewDataColumn FieldName="RoleName" VisibleIndex="3" ReadOnly="true" Width="20%">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataCheckColumn FieldName="IsActive" VisibleIndex="4" Width="20%"></dxwgv:GridViewDataCheckColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
