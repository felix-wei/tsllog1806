<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepotRate.aspx.cs" Inherits="PagesMaster_DepotRate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js">
    </script>
    <script type="text/javascript" src="/Script/pages.js">
    </script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobRate" KeyMember="Id" FilterExpression="LineType='DEPOT'" />
            <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="" />
            <wilson:DataSource ID="dsRateType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RateType" KeyMember="Id" />
            <wilson:DataSource ID="dsContType" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.Container_Type" KeyMember="id" />
            <wilson:DataSource ID="dsProductClass" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefProductClass" KeyMember="Id" />
            <wilson:DataSource ID="dsXXUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Id" FilterExpression="CodeType='2'" />
            <hr>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Depot Address"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                 PopupAddress(btn_ClientId,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="6">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_export_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" OnClick="btn_add_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <hr>
            <table>
                <tr>
                    <td colspan="6">
                        <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
                            DataSourceID="ds1" KeyFieldName="Id" OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting"
                            OnRowInserting="Grid1_RowInserting" OnInitNewRow="Grid1_InitNewRow"
                            OnInit="Grid1_Init" Width="100%" AutoGenerateColumns="False">
                            <SettingsEditing Mode="Inline" />
                            <SettingsBehavior ConfirmDelete="True" />
                            <SettingsPager Mode="ShowPager" PageSize="20" />
                            <Columns>
                                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                                    <EditButton Visible="True" />
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="999" Width="5%">
                                    <DeleteButton Visible="true" />
                                </dxwgv:GridViewCommandColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Update" FieldName="RowUpdateTime" VisibleIndex="998"
                                    Width="140">
                                    <DataItemTemplate>
                                        <%# Eval("RowUpdateUser","{0}") %> <%# Eval("RowUpdateTime","{0:dd/MM#HH:mm}") %>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="LineStatus" Caption="Optional" Width="50" VisibleIndex="98" Visible="false">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="Y" Value="Y" />
                                            <dxe:ListEditItem Text="N" Value="N" />
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ClientId" Caption="Client" Width="150" VisibleIndex="1">
                                    <DataItemTemplate>
                                        <%# Eval("ClientId") %>
                                    </DataItemTemplate>
                                    <EditItemTemplate>
                                        <dxe:ASPxButtonEdit ID="btn_Line_ClientId" ClientInstanceName="btn_Line_ClientId" Text='<%# Bind("ClientId") %>' runat="server" Width="100" AutoPostBack="False">
                                            <Buttons>
                                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                         PopupAddress(btn_Line_ClientId,null);
                                                                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </EditItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn FieldName="ChgCode" Caption="ChargeCode" Width="150" VisibleIndex="1">
                                    <EditItemTemplate>
                                        <dxe:ASPxButtonEdit ID="txt_Line_ChgCode" ClientInstanceName="txt_Line_ChgCode" Width="100" runat="server" Text='<%# Bind("ChgCode") %>'>
                                            <Buttons>
                                                <dxe:EditButton Text=".."></dxe:EditButton>
                                            </Buttons>
                                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                                        PopupChgCode(txt_Line_ChgCode,null);
                        }" />
                                        </dxe:ASPxButtonEdit>
                                    </EditItemTemplate>
                                    <DataItemTemplate>
                                        <%# SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(string.Format("select ChgcodeDes from XXChgCode where ChgcodeId='{0}'", Eval("ChgCode")))) %>
                                    </DataItemTemplate>
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ChgCode Des" FieldName="ChgCodeDe" VisibleIndex="1"
                                    Width="150">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="1"
                                    Width="150">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="JobType" Caption="Job Type" Width="80" VisibleIndex="1" Visible="false">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="IMP" Value="IMP"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="EXP" Value="EXP"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="LOC" Value="LOC"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="WGR" Value="WGR"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="WDO" Value="WDO"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="TPT" Value="TPT"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="CRA" Value="CRA"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataComboBoxColumn FieldName="BillScope" Caption="Scope" Width="80" VisibleIndex="1">
                                    <PropertiesComboBox>
                                        <Items>
                                            <dxe:ListEditItem Text="Job" Value="JOB"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Cont" Value="CONT"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="Trip" Value="TRIP"></dxe:ListEditItem>
                                            <dxe:ListEditItem Text="" Value=""></dxe:ListEditItem>
                                        </Items>
                                    </PropertiesComboBox>
                                </dxwgv:GridViewDataComboBoxColumn>
                                <dxwgv:GridViewDataTextColumn Caption="Rate" FieldName="Price" VisibleIndex="5"
                                    Width="50">
                                </dxwgv:GridViewDataTextColumn>

                            </Columns>
                            <Settings ShowFooter="true" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                            </TotalSummary>

                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid1">
        </dxwgv:ASPxGridViewExporter>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
            Width="800" EnableViewState="False">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
