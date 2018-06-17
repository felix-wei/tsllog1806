<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ContainerMaster.aspx.cs" Inherits="PagesTpt_Job_ContainerMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Container List</title>
    <script type="text/javascript">
        function showCallBack(Result) {
            alert(Result);
            //detailGrid.CancelEdit();
            detailGrid.Refresh();
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container" KeyMember="Id"/>
        <wilson:DataSource ID="dsContainerType" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.Container_Type" KeyMember="Id"  />
        <wilson:DataSource ID="dsContainerCategory" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXUom" KeyMember="Code" FilterExpression="CodeType='TankCate'" />
        <table>
            <tr>
                <td>Container No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_ContN" Width="120" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        <ClientSideEvents Click="function(s,e){
                        window.location='ContainerMaster.aspx?ContN='+txt_ContN.GetText();
                    }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="120" runat="server" Enabled='True' Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
            KeyFieldName="Id" Width="850px" AutoGenerateColumns="False" DataSourceID="dsTransport"
            OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnCustomCallback="grid_Transport_CustomCallback" OnRowDeleting="grid_Transport_RowDeleting" OnRowInserting="grid_Transport_RowInserting" OnRowUpdating="grid_Transport_RowUpdating" 
            OnCustomDataCallback="grid_Transport_CustomDataCallback" OnHtmlEditFormCreated="grid_Transport_HtmlEditFormCreated">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="EditForm" />
            <SettingsBehavior ConfirmDelete="true" />
            <Settings ShowColumnHeaders="true" />
            <Columns>
                            <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                                <EditButton Visible="True" />
                                <DeleteButton Visible="True">
                                </DeleteButton>
                            </dxwgv:GridViewCommandColumn>
                            <dxwgv:GridViewDataColumn FieldName="ContainerNo" VisibleIndex="4"  Visible="true" >
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn FieldName="ContainerType" Caption="ContainerType" VisibleIndex="5"  Visible="true" >
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn FieldName="Lessor" Caption="Lessor" VisibleIndex="6"  Visible="true" >
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="OnHireDateTime" Caption="OnHireDateTime" VisibleIndex="7" Visible="true">
                                <propertiesTextedit displayformatstring="dd/MM/yyyy HH:mm"></propertiesTextedit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn FieldName="CommDate" Caption="CommissionDate" VisibleIndex="8"  Visible="true" >
                                <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataColumn FieldName="TankCat" Caption="Category" VisibleIndex="9"  Visible="true" >
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
                                    <td>Container No
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox ID="txt_ContainerNo" runat="server" Width="165" Text='<%# Bind("ContainerNo") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Commission Date
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_CommissionDate" runat="server" Value='<%# Bind("CommDate") %>' Width="165" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Container Category
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_ContainerCategory"
                                            DataSourceID="dsContainerCategory" TextField="Code" ValueField="Code" Width="165" Value='<%# Eval("TankCat")%>'>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td>Lessor
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Lessor" runat="server" Width="165" Text='<%# Bind("Lessor") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>On Hire Date
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxDateEdit ID="date_OnHireDate" Width="165" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm" Value='<%# Bind("OnHireDateTime") %>'>
                                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Off Hire Date
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_OffHireDate" Width="165" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy HH:mm" DisplayFormatString="dd/MM/yyyy HH:mm" Value='<%# Bind("OffHireDateTime") %>'>
                                            <TimeSectionProperties Visible="true" TimeEditProperties-EditFormatString="HH:mm" TimeEditProperties-SpinButtons-ShowIncrementButtons="false"></TimeSectionProperties>
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of Manufacture
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxDateEdit ID="date_Manufacture" runat="server" Value='<%# Bind("ManuDate") %>' Width="165">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Manufacturer
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Manufacturer" runat="server" Width="165" Text='<%# Bind("Manufacturer") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>CSC Plate No.
                                    </td>
                                    <td colspan="2">
                                        <dxe:ASPxTextBox ID="txt_PlateNo" runat="server" Width="165" Text='<%# Bind("PlateNo") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Container Type
                                    </td>
                                    <td>
                                        <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="cmb_ContainerType"
                                            DataSourceID="dsContainerType" TextField="ContainerType" ValueField="ContainerType" Width="165" Value='<%# Eval("ContainerType")%>'>
                                        </dxe:ASPxComboBox>
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
                                    <td>Testing Pressure
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_TestPress" Increment="0" runat="server" Value='<%# Bind("TestPress") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.00" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>Bars</td>
                                    <td>Shell Thickness
                                    </td>
                                    <td>
                                        <dxe:ASPxSpinEdit ID="spin_Thickness" Increment="0" runat="server" Value='<%# Bind("Thickness") %>'
                                            SpinButtons-ShowIncrementButtons="false" DisplayFormatString="0.00" Width="165">
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td>mm</td>
                                </tr>
                                <tr><td>InActive</td>
                                    <td><dxe:ASPxComboBox ID="cbb_InActive" runat="server" Width="165" TextField="Code" ValueField="Code" Value='<%# Bind("StatusCode") %>'  >
                                        <Items>
                                            <dxe:ListEditItem Value="Use" Text="Use" />
                                            <dxe:ListEditItem Value="InActive" Text="InActive" />
                                        </Items>
                                        </dxe:ASPxComboBox></td>
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
                                        <div style="display:none">
                                        <a href="#" onclick="javascript:detailGrid.GetValuesOnCustomCallback('Save',showCallBack);">Update</a></div>
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
