<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignDriverEdit.aspx.cs" Inherits="PagesContTrucking_Job_AssignDriverEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/BasePages.js"></script>
    <script type="text/javascript" src="/Script/jquery.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript">
        $.noConflict();
    </script>
    <script type="text/javascript">
        function OnCallback(v) {
            if (v != null && v.length > 0)
                alert(v)
            else {
                alert("Successfully Assign Driver.")
                //

                btn_Search.OnClick();

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
        function ShowEdit(TptNo) {
            console.log(TptNo);
            //window.location = "TptJobEdit.aspx?no=" + TptNo + "&typ=" + txt_type.GetText();
            parent.navTab.openTab(TptNo, "/PagesContTrucking/Job/JobEdit.aspx?no=" + TptNo, { title: TptNo, fresh: false, external: true });

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsDriver" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmDriver" KeyMember="Id" FilterExpression="statuscode='Active' order by Code" />

            <table>
                <tr>
                    <td>Driver:</td>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="search_DriverCode" DataSourceID="dsDriver" ValueField="Code" TextField="Code" Width="100"></dxe:ASPxComboBox>
                    </td>
                    <td>Status:
                    </td>
                    <td>
                        <dxe:ASPxComboBox runat="server" ID="search_JobProgress" Width="100">
                            <Items>
                                <%--<dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="Confirmed" Value="Confirmed" />
                                <dxe:ListEditItem Text="Picked" Value="Picked" />
                                <dxe:ListEditItem Text="Delivered" Value="Delivered" />--%>
                                <dxe:ListEditItem Value="P" Text="Pending" />
                                <dxe:ListEditItem Value="S" Text="Started" />
                                <dxe:ListEditItem Value="C" Text="Completed" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>Date From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Search" ClientInstanceName="btn_Search" runat="server" Text="Retrieve" Width="100" OnClick="btn_Search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="100" AutoPostBack="False"
                            UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_assign1" runat="server" Text="OK" AutoPostBack="false" Width="100"
                            UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        detailGrid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="1500"
                KeyFieldName="Id" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback" OnHtmlRowPrepared="grid_Transport_HtmlRowPrepared" SettingsBehavior-AllowSort="false">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditFormAndDisplayRow" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="10" VisibleIndex="0">
                        <DataItemTemplate>
                            <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                            </dxe:ASPxCheckBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Descending" Width="100">--%>
                    <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="JobNo" VisibleIndex="1" Width="100">
                        <DataItemTemplate>
                            <a href="#" onclick='ShowEdit("<%# Eval("JobNo") %>")'><%# Eval("JobNo") %></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Customer" FieldName="Cust" VisibleIndex="2" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="SO.NO" FieldName="SaleOrder" VisibleIndex="2" Width="80">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="ClientRefNo" FieldName="BkgRef" VisibleIndex="2" Width="80">
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="Bkg Date" FieldName="BkgDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("BkgDate"))+" " +SafeValue.SafeString(Eval("BkgTime")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Date" FieldName="TptDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%--<%# SafeValue.SafeDateStr(Eval("TptDate")) %>--%>
                            <dxe:ASPxDateEdit ID="date_TptDate" runat="server" Width="100%" Value='<%# Eval("TptDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Time" FieldName="TptTime" VisibleIndex="3" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_TptTime" Width="60" runat="server" Text='<%# Eval("TptTime") %>'>
                                <MaskSettings Mask="<00..23>:<00..59>" ErrorText="" />
                                <ValidationSettings ErrorDisplayMode="None" />
                            </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="Request Time" FieldName="Delivery_Timeslot" VisibleIndex="3" Width="80">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Status" FieldName="JobProgress" VisibleIndex="9" Width="100">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox runat="server" ID="cbb_JobProgress" Width="100" Value='<%# Eval("JobProgress") %>'>
                                <Items>
                                    <dxe:ListEditItem Value="P" Text="Pending" />
                                    <dxe:ListEditItem Value="S" Text="Started" />
                                    <dxe:ListEditItem Value="C" Text="Completed" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Driver" FieldName="Driver" VisibleIndex="9" Width="100">
                        <DataItemTemplate>
                            <%--<dxe:ASPxButtonEdit ID="btn_DriverCode" ClientInstanceName="btn_DriverCode" runat="server" Text='<%# Bind("Driver") %>' AutoPostBack="False" Width="80">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                            </dxe:ASPxButtonEdit>--%>
                            <dxe:ASPxComboBox runat="server" ID="btn_DriverCode" DataSourceID="dsDriver" ValueField="Code" TextField="Code" Width="100" Value='<%# Eval("Driver") %>'></dxe:ASPxComboBox>
                            <div style="display: none">
                                <dxe:ASPxTextBox ID="lb_Id" runat="server" Text='<%# Bind("Id") %>'>
                                </dxe:ASPxTextBox>
                                <dxe:ASPxTextBox ID="lb_progress" runat="server" Text='<%# Bind("JobProgress") %>'></dxe:ASPxTextBox>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="Index" FieldName="DeliveryIndex" VisibleIndex="9" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="txt_DeliveryIndex" runat="server" Width="100%" Increment="1" Number="0" MinValue="0"
                                NumberType="Integer" Value='<%# Eval("DeliveryIndex") %>'>
                                <SpinButtons ShowIncrementButtons="False">
                                </SpinButtons>
                            </dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <%--<dxwgv:GridViewDataTextColumn Caption="Vehicle" FieldName="VehicleNo" VisibleIndex="9" Width="100">
                        <DataItemTemplate>
                            <dxe:ASPxButtonEdit ID="btn_vehicle" ClientInstanceName="btn_vehicle" runat="server" Text='<%# Bind("VehicleNo") %>' AutoPostBack="False" Width="80">
                                <Buttons>
                                    <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                </Buttons>
                            </dxe:ASPxButtonEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>--%>
                    <%--<dxwgv:GridViewDataTextColumn Caption="PostCode" FieldName="Delivery_PostCode" VisibleIndex="9" Width="100">
                    </dxwgv:GridViewDataTextColumn>--%>
                    <dxwgv:GridViewDataTextColumn Caption="Consignee Name" FieldName="Delivery_Name" VisibleIndex="9" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Contact" FieldName="Delivery_Contact" VisibleIndex="9" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Address" FieldName="Delivery_Address" VisibleIndex="9" Width="300">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="CargoMkg" VisibleIndex="9" Width="400">
                    </dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" VisibleIndex="10" Width="80">
                    </dxwgv:GridViewDataTextColumn>--%>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="JobNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <table>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Assign Driver" AllowDragging="True" EnableAnimation="False" Height="500"
            AllowResize="True" Width="600" EnableViewState="False">
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
