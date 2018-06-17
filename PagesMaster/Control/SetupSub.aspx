<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetupSub.aspx.cs" Inherits="PagesMaster_Control_SetupSub" %>

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
                    <dxwgv:GridViewDataColumn FieldName="YearCurrentNo" VisibleIndex="3" />
                    <dxwgv:GridViewDataColumn FieldName="Mth01CurrentNo" Caption="Mth01" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth02CurrentNo" Caption="Mth02" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth03CurrentNo" Caption="Mth03" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth04CurrentNo" Caption="Mth04" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth05CurrentNo" Caption="Mth05" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth06CurrentNo" Caption="Mth06" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth07CurrentNo" Caption="Mth07" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth08CurrentNo" Caption="Mth08" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth09CurrentNo" Caption="Mth09" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth10CurrentNo" Caption="Mth10" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth11CurrentNo" Caption="Mth11" VisibleIndex="4" />
                    <dxwgv:GridViewDataColumn FieldName="Mth12CurrentNo" Caption="Mth12" VisibleIndex="4" />
                </Columns>
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource ID="dsSetupSub" runat="server" ConnectionString="<%$ ConnectionStrings:local %>"
                SelectCommand="SELECT SequenceId, MastId, Year, YearCurrentNo, Mth01CurrentNo, Mth02CurrentNo, Mth03CurrentNo, Mth04CurrentNo, Mth05CurrentNo, Mth06CurrentNo, Mth07CurrentNo, 
                      Mth08CurrentNo, Mth09CurrentNo, Mth10CurrentNo, Mth11CurrentNo, Mth12CurrentNo  FROM [XXSetup2] where MastId=@MastId Order by Year"
                UpdateCommand="Update XXSetup2 set Year=@Year, YearCurrentNo=@YearCurrentNo, Mth01CurrentNo=@Mth01CurrentNo, Mth02CurrentNo=@Mth02CurrentNo, Mth03CurrentNo=@Mth03CurrentNo, Mth04CurrentNo=@Mth04CurrentNo, Mth05CurrentNo=@Mth05CurrentNo, Mth06CurrentNo=@Mth06CurrentNo, Mth07CurrentNo=@Mth07CurrentNo, 
                      Mth08CurrentNo=@Mth08CurrentNo, Mth09CurrentNo=@Mth09CurrentNo, Mth10CurrentNo=@Mth10CurrentNo, Mth11CurrentNo=@Mth11CurrentNo, Mth12CurrentNo=@Mth12CurrentNo
                where SequenceId=@SequenceId"
                InsertCommand="Insert Into XXSetup2 (MastId, Year, YearCurrentNo, Mth01CurrentNo, Mth02CurrentNo, Mth03CurrentNo, Mth04CurrentNo, Mth05CurrentNo, Mth06CurrentNo, Mth07CurrentNo, 
                      Mth08CurrentNo, Mth09CurrentNo, Mth10CurrentNo, Mth11CurrentNo, Mth12CurrentNo) 
                Values(@MastId, @Year, @YearCurrentNo, @Mth01CurrentNo, @Mth02CurrentNo, @Mth03CurrentNo, @Mth04CurrentNo, @Mth05CurrentNo, @Mth06CurrentNo, @Mth07CurrentNo, 
                      @Mth08CurrentNo, @Mth09CurrentNo, @Mth10CurrentNo, @Mth11CurrentNo, @Mth12CurrentNo)">
                <SelectParameters>
                    <asp:Parameter Name="MastId" Type="String" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Year" Type="String" />
                    <asp:Parameter Name="YearCurrentNo" Type="String" />
                    <asp:Parameter Name="Mth01CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth02CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth03CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth04CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth05CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth06CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth07CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth08CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth09CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth10CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth11CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth12CurrentNo" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="MastId" Type="String" />
                    <asp:Parameter Name="Year" Type="String" />
                    <asp:Parameter Name="YearCurrentNo" Type="String" />
                    <asp:Parameter Name="Mth01CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth02CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth03CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth04CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth05CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth06CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth07CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth08CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth09CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth10CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth11CurrentNo" Type="String" />
                    <asp:Parameter Name="Mth12CurrentNo" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>


        </div>
    </form>
</body>
</html>
