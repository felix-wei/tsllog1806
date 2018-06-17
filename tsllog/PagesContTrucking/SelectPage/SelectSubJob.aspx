<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectSubJob.aspx.cs" Inherits="PagesContTrucking_SelectPage_SelectSubJob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function OnCallback(v) {
            if (v != null) {
                alert(v);
                btn_search.OnClick(null,null);
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
        <table>
            <tr>
                <td>Job No</td>
                <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Job Type"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="100" DropDownStyle="DropDownList">
                        <Items>
                            <dxe:ListEditItem Text="All" Value="All" Selected="true" />
                            <dxe:ListEditItem Text="IMP" Value="IMP" />
                            <dxe:ListEditItem Text="EXP" Value="EXP" />
                            <dxe:ListEditItem Text="LOC" Value="LOC" />
                            <dxe:ListEditItem Text="WGR" Value="WGR" />
                            <dxe:ListEditItem Text="WDO" Value="WDO" />
                            <dxe:ListEditItem Text="TPT" Value="TPT" />
                            <dxe:ListEditItem Text="TPT" Value="TPT" />
                            <dxe:ListEditItem Text="LI" Value="LI" />
                            <dxe:ListEditItem Text="LE" Value="LE" />
                            <dxe:ListEditItem Text="CRA" Value="CRA" />
                            <dxe:ListEditItem Text="FRT" Value="FRT" />
                            <dxe:ListEditItem Text="ROS" Value="ROS" />
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
                <td>
                     <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                </td>
                <td>

                    <dxe:ASPxButton ID="btnSelect" ClientInstanceName="btnSelect" Width="100" runat="server" Text="Select All"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                   SelectAll();          
                                                        }" />
                    </dxe:ASPxButton>
                </td>

                <td>

                    <dxe:ASPxButton ID="ASPxButton3" Width="60" runat="server" Text="Save"
                        AutoPostBack="false" UseSubmitBehavior="false">
                        <ClientSideEvents Click="function(s,e) {
                                    grid.GetValuesOnCustomCallback('Save',OnCallback);              
                                                        }" />
                    </dxe:ASPxButton>
                </td>
                <td>Show In Invoice</td>
                <td>
                    <dxe:ASPxComboBox ID="cmb_IsShow" runat="server" Width="100">
                        <Items>

                            <dxe:ListEditItem Text="Yes" Value="Yes"  />
                            <dxe:ListEditItem Text="No" Value="No" Selected="true"/>
                        </Items>
                    </dxe:ASPxComboBox>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
            KeyFieldName="Id" Width="1500" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback" OnPageIndexChanged="grid_PageIndexChanged" OnPageSizeChanged="grid_PageSizeChanged">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <SettingsPager Mode="ShowPager" PageSize="10"></SettingsPager>
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" FieldName="Id" SortOrder="Descending" SortIndex="0" Settings-SortMode="Value" Settings-AllowSort="True"
                    Width="40">
                    <DataItemTemplate>
                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                        </dxe:ASPxCheckBox>
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_Id" BackColor="Control" ReadOnly="true" runat="server"
                                Text='<%# Eval("Id") %>' Width="150">
                            </dxe:ASPxTextBox>
                        </div>
                    </DataItemTemplate>
                    <EditItemTemplate>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="Job No" Width="150"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="IsTrucking" Caption="CT">
                    <DataItemTemplate>
                        <%# Eval("IsTrucking","{0}")=="Yes"?"X":""%>
                    </DataItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="IsWarehouse" Caption="WH">
                    <DataItemTemplate>
                        <%# Eval("IsWarehouse","{0}")=="Yes"?"X":""%>
                    </DataItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="IsLocal" Caption="TP">
                    <DataItemTemplate>
                        <%# Eval("IsLocal","{0}")=="Yes"?"X":""%>
                    </DataItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="IsAdhoc" Caption="CR">
                    <DataItemTemplate>
                        <%# Eval("IsAdhoc","{0}")=="Yes"?"X":""%>
                    </DataItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Status" Visible="false">
                    <DataItemTemplate>
                        <%# SafeValue.SafeString(Eval("JobStatus")) %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                    <DataItemTemplate>
                        <div style="min-width: 200px"><%# Eval("PickupFrom") %></div>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To">
                    <DataItemTemplate>
                        <div style="min-width: 200px"><%# Eval("DeliveryTo") %></div>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80">
                    <DataItemTemplate>
                        <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PermitNo" Caption="PermitNo"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="ClientRefNo" Caption="Client Ref No"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataTextColumn Caption="Job Date">
                    <DataItemTemplate>
                        <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Visible="false"></dxwgv:GridViewDataColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
