<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu2.aspx.cs" Inherits="PagesMaster_Control_Menu2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/Script/BasePages.js" ></script>
    <script type="text/javascript">
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
    <script type="text/javascript">
        function ShowSubMenu(v) {
            popubCtr.SetContentUrl('Menu3.aspx?masterId=' + v);
            popubCtr.SetHeaderText('Sub Menu');
            popubCtr.Show();
        }
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
                    <dxwgv:GridViewDataColumn FieldName="SubId" Caption="ID" VisibleIndex="2" />
                    <dxwgv:GridViewDataColumn FieldName="Name" VisibleIndex="2" />
                    <dxwgv:GridViewDataColumn FieldName="Link" VisibleIndex="3" />
                    <dxwgv:GridViewDataColumn FieldName="IsActive" VisibleIndex="4" />
                    <dxwgv:GridViewDataSpinEditColumn FieldName="SortIndex" VisibleIndex="5">
                        <PropertiesSpinEdit Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="100">
                        <EditItemTemplate></EditItemTemplate>
                        <DataItemTemplate><a href="#" onclick='ShowSubMenu("<%# Eval("SubId") %>")'>Sub Menu</a></DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource ID="dsMenuSub" runat="server" ConnectionString="<%$ ConnectionStrings:SqlConnectString %>"
                SelectCommand="SELECT SequenceId, MasterId, Name, IsActive,SubId, Link, SortIndex  FROM [Menu2] where MasterId=@MasterId Order by SortIndex"
                UpdateCommand="Update Menu2 set Name=@Name, Link=@Link, subId=@SubId, IsActive=@IsActive, SortIndex=@SortIndex,MasterId=@MasterId
                where SequenceId=@SequenceId"
                InsertCommand="Insert Into Menu2 (MasterId, Name, IsActive,SubId, Link, SortIndex) 
                Values(@MasterId, @Name, @IsActive, @SubId, @Link,@SortIndex)"
                DeleteCommand="Delete from Menu2 where SequenceId=@SequenceId">
                <SelectParameters>
                    <asp:Parameter Name="MasterId" Type="String" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="SequenceId" Type="Int32" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="MasterId" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="Link" Type="String" />
                    <asp:Parameter Name="SortIndex" Type="Int32" />
                    <asp:Parameter Name="SubId" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="MasterId" Type="String" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="SubId" Type="String" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                    <asp:Parameter Name="Link" Type="String" />
                    <asp:Parameter Name="SortIndex" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>

            
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="720" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
