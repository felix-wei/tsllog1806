<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setup3Sub.aspx.cs" Inherits="PagesMaster_Control_Setup3Sub" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
            </dxe:ASPxButton>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsSetupSub" KeyFieldName="SequenceId" Width="100%" EnableRowsCache="False" AutoGenerateColumns="false"
                 OnInitNewRow="ASPxGridView1_InitNewRow" >
                <SettingsEditing Mode="InLine" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0">
                        <EditButton Visible="true" />
                        <DeleteButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="MastId" Caption="Master" VisibleIndex="2" ReadOnly="true" />
                    <dxwgv:GridViewDataColumn FieldName="Year" VisibleIndex="2" />
                    <dxwgv:GridViewDataColumn FieldName="Month" VisibleIndex="3" />
                    <dxwgv:GridViewDataColumn FieldName="Day" Caption="Day" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="CurrentNo" Caption="Current No" VisibleIndex="4" />
                </Columns>
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource ID="dsSetupSub" runat="server" ConnectionString="<%$ ConnectionStrings:local %>"
                SelectCommand="SELECT Id, MastId, Year, Month, Day, CurrentNo  FROM [XXSetup3] where MastId=@MastId Order by Day"
                UpdateCommand="Update XXSetup3 set Year=@Year, Month=@Month, Day=@Day, CurrentNo=@CurrentNo
                where Id=@Id"
                InsertCommand="Insert Into XXSetup3 (MastId, Year, Month, Day, CurrentNo) 
                Values(@MastId, @Year, @Month, @Day, @CurrentNo)">
                <SelectParameters>
                    <asp:Parameter Name="MastId" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Year" Type="String" />
                    <asp:Parameter Name="Month" Type="String" />
                    <asp:Parameter Name="Day" Type="String" />
                    <asp:Parameter Name="CurrentNo" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Year" Type="String" />
                    <asp:Parameter Name="Month" Type="String" />
                    <asp:Parameter Name="Day" Type="String" />
                    <asp:Parameter Name="CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth12CurrentNo" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>


        </div>
    </form>
</body>
</html>
