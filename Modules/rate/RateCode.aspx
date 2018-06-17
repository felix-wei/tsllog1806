<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RateCode.aspx.cs" Inherits="Page_RateCode" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="ChgTypeId in ('OS','OF','AF','DS','MS')"/>
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
                DataSourceID="ds1" KeyFieldName="SequenceId" OnRowInserting="Grid1_RowInserting"
                OnInit="Grid1_Init" OnInitNewRow="Grid1_InitNewRow" OnRowDeleting="Grid1_RowDeleting">
                <SettingsPager PageSize="50">
                </SettingsPager>
                <SettingsEditing Mode="InLine" PopupEditFormWidth="700px" />
                <Settings ShowFilterRow="true" />
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="false" />
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgcodeId" VisibleIndex="1" Width="100">
					<EditItemTemplate>
						<%# Eval("ChgcodeId") %>
					</EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgcodeDe" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Unit" FieldName="ChgUnit" VisibleIndex="3" Width="80">
                        <PropertiesComboBox ValueType="System.String">
                            <Items>
                                <dxe:ListEditItem Text="-" Value="-"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="KGS" Value="KGS"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="KGSG" Value="KGSG"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="CUFT" Value="CUFT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="CUFTG" Value="CUFTG"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="CBM" Value="CBM"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="CBMG" Value="CBMG"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="CBMR" Value="CBMR"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="20FT" Value="20FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40FT" Value="40FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40HC" Value="40HC"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="45FT" Value="45FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="4M3" Value="4M3"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SET" Value="SET"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SHPT" Value="SHPT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="WT/M3" Value="WT/M3"></dxe:ListEditItem>
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="Type" FieldName="ChgTypeId" VisibleIndex="3" Width="120">
                        <PropertiesComboBox ValueType="System.String">
                            <Items>
                                <dxe:ListEditItem Text="Origin Service" Value="OS"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Ocean Freight" Value="OF"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Air Freight" Value="AF"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Destination Service" Value="DS"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="Others" Value="MS"></dxe:ListEditItem>
                            </Items>
                        </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
