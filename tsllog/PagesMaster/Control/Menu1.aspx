<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu1.aspx.cs" Inherits="PagesMaster_Control_Menu1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript">
        function ShowSubMenu(v) {
            popubCtr.SetContentUrl('Menu2.aspx?masterId=' + v);
            popubCtr.SetHeaderText('Sub Menu');
            popubCtr.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="DataSource1" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Role" KeyMember="SequenceId" />
            <table>
                <tr>
                    <td>Role</td>
                    <td>
                        <dxe:ASPxComboBox ID="ASPxComboBox1" runat="server" DataSourceID="dsRole" TextField="Code"
                            ValueField="Code" ValueType="System.String" Width="160">
                           </dxe:ASPxComboBox>
                    </td>
                    <td>
            <dxe:ASPxButton ID="ASPxButton2" Width="100" runat="server" Text="Retrieve" OnClick="ASPxButton2_Click" >
            </dxe:ASPxButton>
                    </td>
                    <td>
            <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
            </dxe:ASPxButton>

                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsMenuMast" KeyFieldName="SequenceId" Width="100%" EnableRowsCache="False" AutoGenerateColumns="false" OnRowDeleted="grid_RowDeleted" OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0">
                        <EditButton Visible="true" />
                        <DeleteButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="MasterId" VisibleIndex="1">
                    </dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn FieldName="Name" VisibleIndex="2" />
                    <dxwgv:GridViewDataColumn FieldName="IsActive" VisibleIndex="4" />
                    <dxwgv:GridViewDataSpinEditColumn FieldName="SortIndex" VisibleIndex="5">
                        <PropertiesSpinEdit Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataColumn FieldName="RoleName" VisibleIndex="6" ReadOnly="true">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="100">
                        <EditItemTemplate></EditItemTemplate>
                        <DataItemTemplate><a href="#" onclick='ShowSubMenu("<%# Eval("MasterId") %>")'>Sub Menu</a></DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource ID="dsMenuMast" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectString %>"
                SelectCommand="SELECT SequenceId,MasterId, Name, IsActive, SortIndex, RoleName  FROM [Menu1] where RoleName=@RoleName order by SortIndex"
                UpdateCommand="Update Menu1 set  MasterId=@MasterId, Name=@Name, IsActive=@IsActive, SortIndex=@SortIndex, RoleName=@RoleName
                where SequenceId=@SequenceId"
                InsertCommand="Insert Into Menu1 (MasterId, Name, IsActive, SortIndex, RoleName) 
                Values(@MasterId, @Name, @IsActive, @SortIndex, @RoleName)"
                DeleteCommand="Delete from Menu1 where SequenceId=@SequenceId">
                <DeleteParameters>
                    <asp:Parameter Name="SequenceId" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="MasterId" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="SortIndex" Type="Int32" />
                    <asp:Parameter Name="RoleName" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="MasterId" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="SortIndex" Type="Int32" />
                    <asp:Parameter Name="RoleName" Type="String" />
                </InsertParameters>
                <SelectParameters>
                    <asp:Parameter Name="RoleName" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="dsRole" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectString %>"
                SelectCommand="SELECT Code  FROM [Role]"></asp:SqlDataSource>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="600"
                AllowResize="True" Width="920" EnableViewState="False">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
