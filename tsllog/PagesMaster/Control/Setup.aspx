<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Setup.aspx.cs" Inherits="PagesMaster_Control_Setup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function ShowSubMenu(v,type) {
			if(type=='D'){
			  popubCtr1.SetContentUrl('Setup3Sub.aspx?id=' + v);
            popubCtr1.SetHeaderText('Detail');
            popubCtr1.Show();
			}
			else{
			popubCtr1.SetContentUrl('SetupSub.aspx?id=' + v);
            popubCtr1.SetHeaderText('Detail');
            popubCtr1.Show();
			}
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
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsMenuMast" KeyFieldName="SequenceId" Width="100%" EnableRowsCache="False" AutoGenerateColumns="false" OnRowDeleted="grid_RowDeleted">
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0">
                        <EditButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataColumn FieldName="Category" VisibleIndex="1" >
                        </dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn FieldName="Prefix" VisibleIndex="3" />
                    <dxwgv:GridViewDataColumn FieldName="Suffix" VisibleIndex="4" />
                    <dxwgv:GridViewDataSpinEditColumn FieldName="CurrentNo" VisibleIndex="5">
                        <PropertiesSpinEdit Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataComboBoxColumn FieldName="Cycle" Width="60" VisibleIndex="6">
                        <PropertiesComboBox>
                            <Items>
                                <dxe:ListEditItem Text="C" Value="C" />
                                <dxe:ListEditItem Text="D" Value="D" />
								<dxe:ListEditItem Text="M" Value="M" />
                                <dxe:ListEditItem Text="Y" Value="Y" />
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataColumn FieldName="Formula" VisibleIndex="7" />
                    <dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="8" />
                    <dxwgv:GridViewDataComboBoxColumn FieldName="MasterId" Caption="Master Id" Width="60" VisibleIndex="6">
                        <PropertiesComboBox DataSourceID="dsMast" TextField="Category" ValueField="Category" ValueType="System.String"></PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataColumn VisibleIndex="100">
                        <EditItemTemplate></EditItemTemplate>
                        <DataItemTemplate><a href="#" onclick='ShowSubMenu("<%# Eval("SequenceId") %>","<%# Eval("Cycle") %>")'>Detail</a></DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <asp:SqlDataSource ID="dsMenuMast" runat="server" ConnectionString="<%$ ConnectionStrings:local %>"
                SelectCommand="SELECT SequenceId, Category, Description, Prefix, Suffix, Cycle, CurrentNo, Formula,MasterId FROM XXSetup1"
                UpdateCommand="Update XXSetup1 set  Category=@Category, Description=@Description, Prefix=@Prefix, Suffix=@Suffix, Cycle=@Cycle, CurrentNo=@CurrentNo,Formula=@Formula,MasterId=@MasterId
                where SequenceId=@SequenceId"
                InsertCommand="Insert Into XXSetup1 (Category, Description, Prefix, Suffix, Cycle, CurrentNo, Formula,MasterId) 
                Values(@Category, @Description, @Prefix, @Suffix, @Cycle, @CurrentNo, @Formula,@MasterId)"
                >
                <UpdateParameters>
                    <asp:Parameter Name="Category" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Prefix" Type="String" />
                    <asp:Parameter Name="Suffix" Type="String" />
                    <asp:Parameter Name="Cycle" Type="String" />
                    <asp:Parameter Name="CurrentNo" Type="String" />
                    <asp:Parameter Name="Formula" Type="String" />
                    <asp:Parameter Name="MasterId" Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Category" Type="String" />
                    <asp:Parameter Name="Description" Type="String" />
                    <asp:Parameter Name="Prefix" Type="String" />
                    <asp:Parameter Name="Suffix" Type="String" />
                    <asp:Parameter Name="Cycle" Type="String" />
                    <asp:Parameter Name="CurrentNo" Type="String" />
                    <asp:Parameter Name="Formula" Type="String" />
                    <asp:Parameter Name="MasterId" Type="String" />
                </InsertParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="dsMast" runat="server" ConnectionString="<%$ ConnectionStrings:local %>"
                SelectCommand="SELECT SequenceId, Category, Description, Prefix, Suffix, Cycle, CurrentNo, Formula FROM XXSetup1 where isnull(MasterId,'')=''" >

            </asp:SqlDataSource>
        <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
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
