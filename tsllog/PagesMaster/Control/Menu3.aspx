<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu3.aspx.cs" Inherits="PagesMaster_Control_Menu3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/Script/BasePages.js" ></script>
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
            <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
            </dxe:ASPxButton>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsMenuSub" KeyFieldName="SequenceId" Width="100%" EnableRowsCache="False" AutoGenerateColumns="false"
                 OnInitNewRow="ASPxGridView1_InitNewRow" OnRowInserting="grid_RowInserting" OnRowDeleting="grid_RowDeleting" >
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0">
                        <EditButton Visible="true" />
                        <DeleteButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="MasterId" VisibleIndex="1" />
                    <dxwgv:GridViewDataColumn FieldName="Name" VisibleIndex="2" />
                    <dxwgv:GridViewDataColumn FieldName="Link" VisibleIndex="3" />
                    <dxwgv:GridViewDataColumn FieldName="IsActive" VisibleIndex="4" />
                    <dxwgv:GridViewDataSpinEditColumn FieldName="SortIndex" VisibleIndex="5">
                        <PropertiesSpinEdit Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource ID="dsMenuSub" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectString %>"
                SelectCommand="SELECT SequenceId, MasterId, Name, IsActive,Link, SortIndex  FROM [Menu3] where MasterId=@MasterId Order by SortIndex"
                UpdateCommand="Update Menu3 set Name=@Name, Link=@Link,  IsActive=@IsActive, SortIndex=@SortIndex
                where SequenceId=@SequenceId"
                InsertCommand="Insert Into Menu3 (MasterId, Name, IsActive, Link, SortIndex) 
                Values(@MasterId, @Name, @IsActive, @Link,@SortIndex)"
                DeleteCommand="Delete from Menu3 where SequenceId=@SequenceId">
                <SelectParameters>
                    <asp:Parameter Name="MasterId" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="SequenceId" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="Link" Type="String" />
                    <asp:Parameter Name="SortIndex" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="MasterId" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="Link" Type="String" />
                    <asp:Parameter Name="SortIndex" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>


        </div>
    </form>
</body>
</html>
