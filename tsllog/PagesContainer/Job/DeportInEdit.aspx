<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeportInEdit.aspx.cs" Inherits="PagesContainer_Job_DepotInList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Transport Job</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script>
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
            else
                window.location = "DeportInEdit.aspx"
            //parent.ClosePopup();
        }
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
            else {
                parent.AfterPopubDeportIn();
            }
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }

    </script>

    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAssetEvent" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsDepot" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Code" FilterExpression="CodeType='DepotCode'" />
            <wilson:DataSource ID="dsState" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Code" FilterExpression="CodeType='TankState'  " />
            <wilson:DataSource ID="dsUser" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.User" KeyMember="Id" FilterExpression="Port is not null" />
            <table style="width: 100%">
                <tr>
                    <td>Depot
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_Depot"
                            DataSourceID="dsDepot" TextField="Code" ValueField="Code" Width="150">
                        </dxe:ASPxComboBox>
                    </td>
                    <td>ReceiveDate
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_DepotDate" Width="150" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Port</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_Port" ClientInstanceName="btn_Port" runat="server" Width="150" HorizontalAlign="Left" AutoPostBack="False" Enabled="false">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                PopupPort(btn_Port);
                                                                    }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                            KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" DataSourceID="dsTransport" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                            <SettingsCustomizationWindow Enabled="True" />
                            <SettingsEditing Mode="EditFormAndDisplayRow" />
                            <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                            <Columns>
                                <dxwgv:GridViewDataColumn Caption="#" Width="10" VisibleIndex="0">
                                </dxwgv:GridViewDataColumn>
                                <dxwgv:GridViewDataTextColumn Caption="VehicleNo" FieldName="VehicleNo" VisibleIndex="1"
                                    SortIndex="1" SortOrder="Ascending" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ContNo" FieldName="ContainerNo" VisibleIndex="2" Width="120">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="ContType" FieldName="ContainerType" VisibleIndex="3" Width="150">
                                </dxwgv:GridViewDataTextColumn>
                                <dxwgv:GridViewDataTextColumn Caption="State" FieldName="State" VisibleIndex="4" Width="100">
                                </dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dxwgv:ASPxSummaryItem FieldName="VehicleNo" SummaryType="Count" DisplayFormat="{0}" />
                            </TotalSummary>
                            <Templates>
                                <DataRow>

                                    <table border="1" bordercolor="#b1cfef" style="border-collapse: collapse; font-family: Tahoma; font-size: 8pt;">
                                        <tr>
                                            <td width="23">
                                                <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                                </dxe:ASPxCheckBox>
                                            </td>
                                            <td width="133">
                                                <dxe:ASPxLabel ID="txt_VehicleNo" ReadOnly="true" runat="server"
                                                    Text='<%# Eval("VehicleNo") %>' Width="110">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td width="156">
                                                <dxe:ASPxLabel ID="txt_ContNo" runat="server" Text='<%# Eval("ContainerNo") %>' Width="120">
                                                </dxe:ASPxLabel>
                                                <div style="display: none">
                                                    <dxe:ASPxLabel ID="txt_jobId" BackColor="Control" ReadOnly="true" runat="server"
                                                        Text='<%# Eval("Id") %>' Width="80">
                                                    </dxe:ASPxLabel>
                                                </div>
                                            </td>
                                            <td width="193">
                                                <dxe:ASPxLabel ID="txt_ContType" runat="server" Text='<%# Eval("ContainerType") %>' Width="150">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td width="132">
                                                <%--<dxe:ASPxLabel runat="server" EnableIncrementalFiltering="true" ID="lbl_State" Value='<%# Eval("State")%>'>
                                                </dxe:ASPxLabel>--%>
                                                <dxe:ASPxComboBox ID="cbb_state" runat="server" DataSourceID="dsState" TextField="Code" ValueField="Code" Value='<%# Eval("State") %>' Width="130"></dxe:ASPxComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </DataRow>
                            </Templates>
                        </dxwgv:ASPxGridView>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_assign1" runat="server" Text="Assign" AutoPostBack="false"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        detailGrid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="DepotIn" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
