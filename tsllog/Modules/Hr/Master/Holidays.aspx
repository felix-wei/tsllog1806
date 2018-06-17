<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Holidays.aspx.cs" Inherits="Modules_Hr_Master_Holidays" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<form id="form1" runat="server">
        <wilson:DataSource ID="dsHolidays" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Holiday" KeyMember="Id" />
        <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="(Status='Employee' or Status='Resignation') and Id>0" />
                <wilson:DataSource ID="dsDepartment" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.HrMastData" KeyMember="Id" FilterExpression="Type='Department'" />
        <div>
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
                    <dxe:ASPxButton ID="btn_Save" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsHolidays"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow"
            OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" OnRowDeleting="grid_RowDeleting">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="true" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Employee" FieldName="Person" VisibleIndex="0" Width="150" Visible="false">
                    <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                        TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Department" FieldName="Department" VisibleIndex="0" Width="150" Visible="false">
                    <PropertiesComboBox ValueType="System.String" DataSourceID="dsDepartment" Width="150" TextFormatString="{0}" DropDownWidth="105"
                        TextField="Code" EnableIncrementalFiltering="true" ValueField="Code" DataMember="{0}">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Code" Caption="Code" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Days" FieldName="Days" VisibleIndex="3" Width="140" Visible="false">
                    <PropertiesSpinEdit NumberType="Float" Increment="0" SpinButtons-ShowIncrementButtons="false"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataTextColumn Caption="From"  Width="120px" VisibleIndex="3">
                    <DataItemTemplate>
                        <%# Eval("FromDay")+"/"+Eval("FromMonth") %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>Month</td>
                                <td><dxe:ASPxComboBox runat="server" ID="cmb_Month"   Text='<%# Bind("FromMonth") %>' ClientInstanceName="cmb_Month" Width="50" >
                                        <Items>
                                            <dxe:ListEditItem Text="1" Value="1" />
                                            <dxe:ListEditItem Text="2" Value="2" />
                                            <dxe:ListEditItem Text="3" Value="3"/>
                                            <dxe:ListEditItem Text="4" Value="4"/>
                                            <dxe:ListEditItem Text="5" Value="5"/>
                                            <dxe:ListEditItem Text="6" Value="6"/>
                                            <dxe:ListEditItem Text="7" Value="7"/>
                                            <dxe:ListEditItem Text="8" Value="8"/>
                                            <dxe:ListEditItem Text="9" Value="9"/>
                                            <dxe:ListEditItem Text="10" Value="10"/>
                                            <dxe:ListEditItem Text="11" Value="11"/>
                                            <dxe:ListEditItem Text="12" Value="12"/>
                                        </Items>
                                    </dxe:ASPxComboBox></td>
                                <td>-</td>
                                <td>Day</td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="cmb_Day" Text='<%# Bind("FromDay") %>'  ClientInstanceName="cmb_Day"  Width="50">
                                        <Items>
                                            <dxe:ListEditItem Text="1" Value="1" />
                                            <dxe:ListEditItem Text="2" Value="2" />
                                            <dxe:ListEditItem Text="3" Value="3"/>
                                            <dxe:ListEditItem Text="4" Value="4"/>
                                            <dxe:ListEditItem Text="5" Value="5"/>
                                            <dxe:ListEditItem Text="6" Value="6"/>
                                            <dxe:ListEditItem Text="7" Value="7"/>
                                            <dxe:ListEditItem Text="8" Value="8"/>
                                            <dxe:ListEditItem Text="9" Value="9"/>
                                            <dxe:ListEditItem Text="10" Value="10"/>
                                            <dxe:ListEditItem Text="11" Value="11"/>
                                            <dxe:ListEditItem Text="12" Value="12"/>
                                            <dxe:ListEditItem Text="13" Value="13"/>
                                            <dxe:ListEditItem Text="14" Value="14"/>
                                            <dxe:ListEditItem Text="15" Value="15"/>
                                            <dxe:ListEditItem Text="16" Value="16"/>
                                            <dxe:ListEditItem Text="17" Value="17"/>
                                            <dxe:ListEditItem Text="18" Value="18"/>
                                            <dxe:ListEditItem Text="19" Value="19"/>
                                            <dxe:ListEditItem Text="20" Value="20"/>
                                            <dxe:ListEditItem Text="21" Value="21"/>
                                            <dxe:ListEditItem Text="22" Value="22"/>
                                            <dxe:ListEditItem Text="23" Value="23"/>
                                            <dxe:ListEditItem Text="24" Value="24"/>
                                            <dxe:ListEditItem Text="25" Value="25"/>
                                            <dxe:ListEditItem Text="26" Value="26"/>
                                            <dxe:ListEditItem Text="27" Value="27"/>
                                            <dxe:ListEditItem Text="28" Value="28"/>
                                            <dxe:ListEditItem Text="29" Value="29"/>
                                            <dxe:ListEditItem Text="30" Value="30"/>
                                            <dxe:ListEditItem Text="31" Value="31"/>
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="To"  Width="120px" VisibleIndex="3">
                    <DataItemTemplate>
                        <%# Eval("ToDay")+"/"+ Eval("ToMonth") %>
                    </DataItemTemplate>
                    <EditItemTemplate>
                           <table>
                            <tr>
                                <td>Month</td>
                                <td><dxe:ASPxComboBox runat="server" ID="cmb_Month1" Text='<%# Bind("ToMonth") %>'  ClientInstanceName="cmb_Month1" Width="50" >
                                        <Items>
                                            <dxe:ListEditItem Text="1" Value="1" />
                                            <dxe:ListEditItem Text="2" Value="2" />
                                            <dxe:ListEditItem Text="3" Value="3"/>
                                            <dxe:ListEditItem Text="4" Value="4"/>
                                            <dxe:ListEditItem Text="5" Value="5"/>
                                            <dxe:ListEditItem Text="6" Value="6"/>
                                            <dxe:ListEditItem Text="7" Value="7"/>
                                            <dxe:ListEditItem Text="8" Value="8"/>
                                            <dxe:ListEditItem Text="9" Value="9"/>
                                            <dxe:ListEditItem Text="10" Value="10"/>
                                            <dxe:ListEditItem Text="11" Value="11"/>
                                            <dxe:ListEditItem Text="12" Value="12"/>
                                        </Items>
                                    </dxe:ASPxComboBox></td>
                                <td>-</td>
                                <td>Day</td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="cmb_Day1" Text='<%# Bind("ToDay") %>' ClientInstanceName="cmb_Day1"  Width="50">
                                        <Items>
                                            <dxe:ListEditItem Text="1" Value="1" />
                                            <dxe:ListEditItem Text="2" Value="2" />
                                            <dxe:ListEditItem Text="3" Value="3"/>
                                            <dxe:ListEditItem Text="4" Value="4"/>
                                            <dxe:ListEditItem Text="5" Value="5"/>
                                            <dxe:ListEditItem Text="6" Value="6"/>
                                            <dxe:ListEditItem Text="7" Value="7"/>
                                            <dxe:ListEditItem Text="8" Value="8"/>
                                            <dxe:ListEditItem Text="9" Value="9"/>
                                            <dxe:ListEditItem Text="10" Value="10"/>
                                            <dxe:ListEditItem Text="11" Value="11"/>
                                            <dxe:ListEditItem Text="12" Value="12"/>
                                            <dxe:ListEditItem Text="13" Value="13"/>
                                            <dxe:ListEditItem Text="14" Value="14"/>
                                            <dxe:ListEditItem Text="15" Value="15"/>
                                            <dxe:ListEditItem Text="16" Value="16"/>
                                            <dxe:ListEditItem Text="17" Value="17"/>
                                            <dxe:ListEditItem Text="18" Value="18"/>
                                            <dxe:ListEditItem Text="19" Value="19"/>
                                            <dxe:ListEditItem Text="20" Value="20"/>
                                            <dxe:ListEditItem Text="21" Value="21"/>
                                            <dxe:ListEditItem Text="22" Value="22"/>
                                            <dxe:ListEditItem Text="23" Value="23"/>
                                            <dxe:ListEditItem Text="24" Value="24"/>
                                            <dxe:ListEditItem Text="25" Value="25"/>
                                            <dxe:ListEditItem Text="26" Value="26"/>
                                            <dxe:ListEditItem Text="27" Value="27"/>
                                            <dxe:ListEditItem Text="28" Value="28"/>
                                            <dxe:ListEditItem Text="29" Value="29"/>
                                            <dxe:ListEditItem Text="30" Value="30"/>
                                            <dxe:ListEditItem Text="31" Value="31"/>
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                        </table>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Remark" VisibleIndex="3" Width="140">
                </dxwgv:GridViewDataTextColumn>
            </Columns>
          </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
        </dxwgv:ASPxGridViewExporter>
    </div>
    </form>
</body>
</html>
