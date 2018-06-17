<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransportMonitor.aspx.cs" Inherits="Modules_WareHouse_Job_TransportMonitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowHouse(masterId, type) {
            if (type == "IN") {
                parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoInEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
            }
            if (type == "OUT") {
                parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/DoOutEdit.aspx?no=" + masterId, { title: masterId, fresh: false, external: true });
            }
        }
    </script>
    <style type="text/css">
        a:hover {
            color: black;
        }

        .link a:link {
            color: red;
        }

        .link a:hover {
            color: red;
        }

        .none a:link {
        }


        .a_ltrip span {
            display: inline-block;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding: 2px;
            width: 33px;
            height: 21px;
            overflow: hidden;
            white-space: nowrap;
            text-align: center;
            margin: 2px;
            /*margin-top:2px;
            margin-left:2px;
            margin-bottom:2px;
            margin-right:4px;*/
        }

        .a_ltrip .S {
            background-color: green;
        }

        .a_ltrip .X {
            background-color: gray;
        }

        .a_ltrip .C {
            background-color: blue;
        }

        .a_ltrip .P {
            background-color: orange;
        }

        .a_ltrip .div_FixWith {
            min-width: 150px;
        }

        .div_contStatus {
            width: 80px;
            height: 21px;
            text-align: center;
            border: 1px solid #e8e8e8;
            box-sizing: border-box;
            color: white;
            padding-top: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lab_PoNo" runat="server" Text="Cnt No">
                        </dxe:ASPxLabel>

                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PoNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text="From">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <%--                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Permit No">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_PermitNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="Lot No">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_LotNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="SKU Code">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_SKUCode" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>--%>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel6" runat="server" Text="Customer Ref">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_CustomerRef" ClientInstanceName="txt_CustomerRef" Width="120" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Ref Date">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_RefDate" Width="120" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Customer">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server" Width="120px" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_CustId,txt_CustName);
                                }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="100%" runat="server" Style="margin-bottom: 0px">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <wilson:DataSource ID="dsIssue" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.WhDo"
                KeyMember="Id" />
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="detailGrid" runat="server" DataSourceID="dsIssue"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnRowUpdating="grid_RowUpdating">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0" Width="5%">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("DoNo") %>','<%# Eval("DoType") %>');"><%# Eval("DoNo") %></a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <div style="display: none">
                                 <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Client" FieldName="PartyName" VisibleIndex="3" Width="120" ReadOnly="true">
                       <EditItemTemplate>
                           <%# Eval("PartyName") %>
                       </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Transporter" FieldName="TransportName" VisibleIndex="3" Width="120" ReadOnly="true">
                         <EditItemTemplate>
                           <%# Eval("TransportName") %>
                       </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Status" FieldName="TransportStatus" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("TransportStatus"))) %>" class="div_contStatus">
                                <%# Eval("TransportStatus") %>
                            </div>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxComboBox ID="txt_TransportStatus" runat="server" Width="150px" DropDownStyle="DropDownList" Text='<%# Bind("TransportStatus") %>'>
                                <Items>
                                    <dxe:ListEditItem Value="New" Text="New" />
                                    <dxe:ListEditItem Value="Scheduled" Text="Scheduled" />
                                    <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                    <dxe:ListEditItem Value="Completed" Text="Completed" />
                                    <dxe:ListEditItem Value="Canceled" Text="Canceled" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Driver Name" FieldName="DriverName" VisibleIndex="3" Width="120">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_DriveName" Text='<%# Eval("DriverName") %>' ClientInstanceName="txt_DriveName">
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Driver IC" FieldName="DriverIC" VisibleIndex="4" Width="120">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_DriverIC" Text='<%# Eval("DriverIC") %>' ClientInstanceName="txt_DriverIC">
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Driver Tel" FieldName="DriverTel" VisibleIndex="5" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_DriverTel" Text='<%# Eval("DriverTel") %>' ClientInstanceName="txt_DriveName">
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="VehicleNo" FieldName="VehicleNo" VisibleIndex="5" Width="100">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_VechicleNo" Text='<%# Eval("VehicleNo") %>' ClientInstanceName="txt_DriveName">
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn
                        Caption="Job No" FieldName="TptJobNo" VisibleIndex="5" Width="60">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" Width="150" ID="txt_TptJobNo" Text='<%# Eval("TptJobNo") %>' ClientInstanceName="txt_DriveName">
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Start" FieldName="TransportStart" VisibleIndex="7" Width="80">
                        <PropertiesDateEdit DisplayFormatString="yyyy/MM/dd HH:mm"></PropertiesDateEdit>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_TransportStart" runat="server" Value='<%# Bind("TransportStart") %>' Width="150px" EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm">
                            </dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remarks" VisibleIndex="7" Width="150">
                         <EditItemTemplate>
                            <dxe:ASPxMemo ID="meno_Remark" Rows="2" Width="100%" runat="server" Text='<%# Eval("Remarks") %>'></dxe:ASPxMemo>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="8">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { detailGrid.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { detailGrid.UpdateEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                            ClientSideEvents-Click='<%# "function(s) { detailGrid.CancelEdit() }"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="DoNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
