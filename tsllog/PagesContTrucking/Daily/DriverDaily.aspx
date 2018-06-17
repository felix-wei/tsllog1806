<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DriverDaily.aspx.cs" Inherits="PagesContTrucking_Daily_DriverDaily" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">

        function RowClickHandler(s, e) {
            SetLookupKeyValue(e.visibleIndex);
            DropDownEdit.HideDropDown();
        }
        function SetLookupKeyValue(rowIndex) {

            DropDownEdit.SetText(GridView.cpContN[rowIndex]);
            cbb_Towhead.SetText(GridView.cpContType[rowIndex]);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmDriverLog" KeyMember="Id" FilterExpression="1=0" />
            <wilson:DataSource ID="dsTowhead" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.RefVehicle" KeyMember="Id" FilterExpression="VehicleType='Towhead'"></wilson:DataSource>
            <table>
                <tr>
                    <td>Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_search" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                            <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_CreateAll" runat="server" Text="Create All" OnClick="btn_CreateAll_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="850px" DataSourceID="dsTransport" KeyFieldName="Id" OnRowInserting="grid_Transport_RowInserting" OnRowDeleting="grid_Transport_RowDeleting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnHtmlEditFormCreated="grid_Transport_HtmlEditFormCreated">
                <SettingsPager PageSize="100" />
                <SettingsEditing Mode="EditForm" />
                <SettingsBehavior ConfirmDelete="true" />

                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="True">
                        </DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Driver" Caption="Driver" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Date" Caption="Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("Date")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Towhead" Caption="PrimeMover"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="TeamNo" Caption="Team"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsActive" Caption="IsActive">
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                        </div>
                        <table>
                            <tr>
                                <td>Driver</td>
                                <td>
                                    <dxe:ASPxDropDownEdit ID="DropDownEdit" runat="server" ClientInstanceName="DropDownEdit"
                                        Text='<%# Bind("Driver") %>' Width="150"  AllowUserInput="false">
                                        <DropDownWindowTemplate>
                                            <dxwgv:ASPxGridView ID="gridPopCont" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                                                Width="300px" OnCustomJSProperties="gridPopCont_CustomJSProperties">
                                                <Columns>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Code" VisibleIndex="0">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Name" Caption="Name" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="TowheaderCode" Caption="PrimeMover" VisibleIndex="2">
                                                    </dxwgv:GridViewDataTextColumn>
                                                </Columns>
                                                <ClientSideEvents RowClick="RowClickHandler" />
                                            </dxwgv:ASPxGridView>
                                        </DropDownWindowTemplate>
                                    </dxe:ASPxDropDownEdit>
                                </td>
                                <td>Date</td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_Date" runat="server" Value='<%# Bind("Date") %>' EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                                </td>
                                <td>PrimeMover</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Towhead" ClientInstanceName="cbb_Towhead" runat="server" Value='<%# Bind("Towhead") %>' DataSourceID="dsTowhead" TextField="VehicleCode" ValueField="VehicleCode">
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>IsActive</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_IsActive" runat="server" Value='<%# Bind("IsActive") %>'>
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>
                                    Team
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_TeamNo" runat="server" Value='<%# Bind("TeamNo") %>' Width="120">
                                        <Items>
                                            <dxe:ListEditItem Value="A" Text="A" />
                                            <dxe:ListEditItem Value="B" Text="B" />
                                            <dxe:ListEditItem Value="C" Text="C"/>
                                            <dxe:ListEditItem Value="D" Text="D"/>
                                            <dxe:ListEditItem Value="E" Text="E"/>
                                            <dxe:ListEditItem Value="F" Text="F"/>
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right">
                            <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateEvent" ReplacementType="EditFormUpdateButton"
                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                            <dxwgv:ASPxGridViewTemplateReplacement ID="CancelEvent" ReplacementType="EditFormCancelButton"
                                runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                        </div>
                    </EditForm>
                </Templates>

            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
