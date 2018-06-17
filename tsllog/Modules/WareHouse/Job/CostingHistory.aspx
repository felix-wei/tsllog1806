<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="CostingHistory.aspx.cs" Inherits="CostingHistory" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/_lib/base/jquery.js"></script>
    <script type="text/javascript" src="/_lib/dx/grid.js"></script>
    <script type="text/javascript">
        function ShowAuditDet(id) {
            popubCtr1.SetHeaderText("");
            popubCtr1.SetContentUrl('/PagesAudit/ShowAuditDet.aspx?id=' + id);
            popubCtr1.Show();
        }

    </script>
</head>
<body width="800">
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsCost" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Cost" KeyMember="SequenceId"></wilson:DataSource>
            <wilson:DataSource ID="dsCostDet" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CostDet" KeyMember="SequenceId"></wilson:DataSource>
            <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" />
            <%--<asp:SqlDataSource ID="dsAudit" runat="server" ConnectionString="<%$ ConnectionStrings:local %>" SelectCommand="SELECT  ROW_NUMBER() OVER(ORDER BY Ops) AS Id,Ops,[User],MAX(Time) AS Time FROM dbo.Audit GROUP BY Ops,[User] ORDER BY Time DESC" />--%>
            <%--<asp:SqlDataSource ID="dsAuditDet" runat="server" ConnectionString="<%$ ConnectionStrings:local %>"  SelectCommand="SELECT Id, EntityNo, EntityName from [X0] Where   RowType='VEHICLE' Order By EntityNo" />--%>

            <div>
                <dxwgv:ASPxGridView ID="grd_Cost" ClientInstanceName="grd_Cost" runat="server" DataSourceID="dsCost"
                    KeyFieldName="SequenceId" AutoGenerateColumns="False" Width="100%" OnBeforePerformDataSelect="grd_Cost_BeforePerformDataSelect">
                    <Settings GridLines="Horizontal" />
                    <SettingsDetail ShowDetailRow="true" />
                    <SettingsPager Mode="ShowAllRecords" />
                    <SettingsEditing Mode="Inline" />
                    <SettingsCustomizationWindow Enabled="True" />
                    <SettingsBehavior ConfirmDelete="True" />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Version" FieldName="Version" Width="50" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataColumn Caption="Amount" FieldName="Amount" VisibleIndex="3" Width="50">
                            <DataItemTemplate><%#Eval("Amount") %></DataItemTemplate>
                            <EditItemTemplate><%#Eval("Amount") %></EditItemTemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" Width="150" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Status" Caption="Status" Width="50" VisibleIndex="5">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataColumn Caption="Remark" FieldName="Marking" VisibleIndex="3" Width="80">
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Create User/Time" FieldName="CreateBy" Width="90" VisibleIndex="98">
                            <DataItemTemplate>
                                <%#SafeValue.SafeString(Eval("CreateBy"))+" - "+SafeValue.SafeString(Eval("CreateDateTime","{0:dd/MM HH:mm}")) %>
                            </DataItemTemplate>
                            <EditItemTemplate></EditItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Update User/Time" FieldName="UpdateBy" Width="90" VisibleIndex="99">
                            <DataItemTemplate>
                                <%#SafeValue.SafeString(Eval("UpdateBy"))+" - "+SafeValue.SafeString(Eval("UpdateDateTime","{0:dd/MM HH:mm}")) %>
                            </DataItemTemplate>
                            <EditItemTemplate></EditItemTemplate>
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="grd_CostDet" ClientInstanceName="grd_CostDet" runat="server" DataSourceID="dsCostDet"
                                OnBeforePerformDataSelect="grd_CostDet_BeforePerformDataSelect" OnInit="grd_CostDet_Init"
                                AutoGenerateColumns="False">
                                <Settings ShowGroupPanel="false" />
                                <SettingsPager Mode="ShowAllRecords" />
                                <SettingsEditing Mode="Inline" />
                                <Columns>
                                    <dxwgv:GridViewDataTextColumn ReadOnly="True" Caption="Type" FieldName="Status1" Width="50" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>

                                    <dxwgv:GridViewDataComboBoxColumn FieldName="ChgCode" Caption="Charge" Width="90" VisibleIndex="1">
                                        <PropertiesComboBox DataSourceID="dsRate" ValueType="System.String" TextField="ChgCodeId" ValueField="ChgCodeId">
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgCodeDes" Width="150" VisibleIndex="1">
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="Unit" Caption="Unit" Width="80" VisibleIndex="5">
                                        <PropertiesComboBox>
                                            <Items>
                                                <dxe:ListEditItem Text="20FT" Value="20FT"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="40FT" Value="40FT"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="40HQ" Value="40HQ"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="45FT" Value="45FT"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="4M3" Value="4M3"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="SET" Value="SET"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="SHPT" Value="SHPT"></dxe:ListEditItem>
                                                <dxe:ListEditItem Text="WT/M3" Value="WT/M3"></dxe:ListEditItem>
                                            </Items>
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataColumn Caption="Qty" FieldName="SaleQty" VisibleIndex="3" Width="50">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn Caption="Price" FieldName="SalePrice" VisibleIndex="3" Width="50">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn Caption="Total" FieldName="SaleDocAmt" VisibleIndex="3" Width="80">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="CostDate" Caption="Billing Date" VisibleIndex="9"
                                        Width="100">
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" Width="90" EditFormatString="dd/MM/yyyy" />
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataColumn Caption="Bill No" FieldName="DocNo" VisibleIndex="3" Width="80">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn Caption="Cont No" FieldName="Status2" VisibleIndex="3" Width="80">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn Caption="HBL No" FieldName="Status3" VisibleIndex="3" Width="80">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn Caption="Party" FieldName="Status4" VisibleIndex="3" Width="80">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn Caption="Remark" FieldName="Remark" VisibleIndex="3" Width="80">
                                    </dxwgv:GridViewDataColumn>

                                    <dxwgv:GridViewDataColumn FieldName="RowUpdateUser" Caption="Update By" VisibleIndex="9"
                                        Width="100">
                                        <DataItemTemplate><%# Eval("RowUpdateUser") %></DataItemTemplate>
                                        <EditItemTemplate><%# Eval("RowUpdateUser") %></EditItemTemplate>
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="RowUpdateTime" Caption="Time" VisibleIndex="9" Width="100">
                                        <DataItemTemplate><%# Eval("RowUpdateTime","{0:dd/MM HH:mm}") %></DataItemTemplate>
                                        <EditItemTemplate><%# Eval("RowUpdateTime","{0:dd/MM HH:mm}") %></EditItemTemplate>
                                    </dxwgv:GridViewDataColumn>

                                </Columns>
                            </dxwgv:ASPxGridView>
                        </DetailRow>
                    </Templates>
                </dxwgv:ASPxGridView>
                <dxpc:ASPxPopupControl ID="popubCtr" runat="server" ClientInstanceName="popubCtr" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                    HeaderText=" " AllowDragging="True" EnableAnimation="False" Height="480"
                    Width="100%" EnableViewState="False">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" ClientInstanceName="popubCtr1" CloseAction="CloseButton" Modal="True"
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                    HeaderText=" " AllowDragging="True" EnableAnimation="False" Height="500"
                    Width="700" EnableViewState="False">
                    <ContentCollection>
                        <dxpc:PopupControlContentControl runat="server">
                        </dxpc:PopupControlContentControl>
                    </ContentCollection>
                </dxpc:ASPxPopupControl>
                <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
                </dxwgv:ASPxGridViewExporter>
            </div>
        </div>
    </form>
</body>
</html>
