<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ContainerType.aspx.cs" Inherits="PagesTpt_Job_ContainerType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="Id" FilterExpression="" />
        <table>
            <tr>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="160" runat="server" Enabled='True' Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Refresh">
                            <ClientSideEvents Click="function(s,e){
                        window.location='ContainerType.aspx';
                    }" />
                        </dxe:ASPxButton>
                    </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
            KeyFieldName="Id" Width="850px" AutoGenerateColumns="False" DataSourceID="dsTransport"
            OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnCustomCallback="grid_Transport_CustomCallback" OnRowDeleting="grid_Transport_RowDeleting"
            OnCustomDataCallback="grid_Transport_CustomDataCallback" OnHtmlEditFormCreated="grid_Transport_HtmlEditFormCreated" OnRowInserting="grid_Transport_RowInserting" OnRowUpdating="grid_Transport_RowUpdating">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsEditing Mode="EditForm" />
            <SettingsBehavior ConfirmDelete="true" />
            <SettingsPager Mode="ShowPager" PageSize="100"></SettingsPager>
            <Columns>
                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                <EditButton Visible="True" />
                                <DeleteButton Visible="True">
                                </DeleteButton>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="ContainerType" VisibleIndex="4"  Visible="true" >
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn FieldName="Material" Caption="Material" VisibleIndex="4"  Visible="true" >
                            </dxwgv:GridViewDataColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div style="padding: 2px 2px 2px 2px">
                        <div style="display:none">
                            <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("Id") %>'>
                            </dxe:ASPxTextBox>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td>Container Type
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_ContainerType" runat="server" Width="165" Text='<%# Bind("ContainerType") %>' ReadOnly='<%#  SafeValue.SafeInt(Eval("Id"),0)>0  %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>External Length
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_ExternalLength" Increment="0" runat="server" Value='<%# Bind("ExternalLength") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>mm</td>
                                    <td>External Width
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_ExternalWidth" Increment="0" runat="server" Value='<%# Bind("ExternalBreadth") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>mm</td>
                                </tr>
                                <tr>
                                    <td>External Height
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_ExternalHeight" Increment="0" runat="server" Value='<%# Bind("ExternalHeight") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>mm</td>
                                    <td>Internal Length
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_InternalLength" Increment="0" runat="server" Value='<%# Bind("InternalLength") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>mm</td>
                                </tr>
                                <tr>
                                    <td>Internal Width
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_InternalBreadth" Increment="0" runat="server" Value='<%# Bind("InternalBreadth") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>mm</td>
                                    <td>Internal Height
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_InternalHeight" Increment="0" runat="server" Value='<%# Bind("InternalHeight") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>mm</td>
                                </tr>
                                <tr>
                                    <td>Material
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox ID="txt_Material" runat="server" Width="165" Text='<%# Bind("Material") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>External coat
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_ExternalCoat" runat="server" Width="165" Text='<%# Bind("ExternalCoat") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Capacity
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_Capacity" Increment="0" runat="server" Value='<%# Bind("Capacity") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>cum</td>
                                    <td>Maximum gross weight
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_MaxGrossWeight" Increment="0" runat="server" Value='<%# Bind("MaxGrossWeight") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>kg</td>
                                </tr>
                                <tr>
                                    <td>Tare Weight(±2%)
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_TareWeight" Increment="0" runat="server" Value='<%# Bind("TareWeight") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>kg</td>
                                    <td>Max PayLoad
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_MaxPayload" Increment="0" runat="server" Value='<%# Bind("MaxPayload") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.000" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>kg</td>
                                </tr>
                                <tr>
                                    <td>Stacking
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox ID="txt_Stacking" runat="server" Value='<%# Bind("Stacking") %>' Width="165">
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Working Pressure
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_TestPress" Increment="0" runat="server" Value='<%# Bind("TestPress") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.00" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>Bars</td>
                                </tr>
                                <tr>
                                    <td>Approvals
                                    </td>
                                    <td>
                                        <dxe:ASPxMemo ID="txt_Approvals" Rows="3" Width="165" ClientInstanceName="txt_Approvals"
                                            runat="server" Text='<%# Bind("Approvals") %>'>
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                <td colspan="4">
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>

                                    </div>
                                </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
