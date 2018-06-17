<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChgCode.aspx.cs" Inherits="XX_ChgCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsXXChgCode" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXChgCode" KeyMember="SequenceId" />
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Export" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="100%"
                DataSourceID="dsXXChgCode" KeyFieldName="SequenceId" OnRowInserting="ASPxGridView1_RowInserting"
                OnInit="ASPxGridView1_Init" OnInitNewRow="ASPxGridView1_InitNewRow" OnRowDeleting="ASPxGridView1_RowDeleting">
                <SettingsPager PageSize="50">
                </SettingsPager>
                <SettingsEditing Mode="InLine" PopupEditFormWidth="700px" />
                <Settings ShowFilterRow="true" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgcodeId" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgcodeDe" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="ChgUnit" VisibleIndex="3" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Gst Type" FieldName="GstTypeId" VisibleIndex="4" Width="50">
                        <PropertiesComboBox ValueType="System.String">
                            <Items>
                                <dxe:ListEditItem Text="S" Value="S"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Z" Value="Z"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="E" Value="E"></dxe:ListEditItem>
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataSpinEditColumn Caption="Gst P" FieldName="GstP" VisibleIndex="5" Width="50">
                        <PropertiesSpinEdit DisplayFormatString="g">
                            <SpinButtons ShowIncrementButtons="false" />
                        </PropertiesSpinEdit>
                    </dxwgv:GridViewDataSpinEditColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ar Code" FieldName="ArCode" VisibleIndex="9" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="AP Code" FieldName="ApCode" VisibleIndex="10" Width="50">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
