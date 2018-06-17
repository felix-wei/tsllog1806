<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusUpdateEdit.aspx.cs" Inherits="PagesContainer_Job_StatusUpdateEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsXXUom" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXUom" KeyMember="Id" FilterExpression="CodeType='TankState'" />
            <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.ContAssetEvent" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lblContainerNo" Text="Container No" runat="server"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_ContainerNo" Width="120" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" Text="EventCode" runat="server" />
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cb_Status" runat="server" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" TextField="Code" ValueField="Code" Width="100%">
                            <Items>
                                <dxe:ListEditItem />
                                            <dxe:ListEditItem Text="gatein" Value="gatein" />
                                            <dxe:ListEditItem Text="gateout" Value="gateout" />
                                            <dxe:ListEditItem Text="boxload" Value="boxload" />
                                            <dxe:ListEditItem Text="boxdischarge" Value="boxdischarge" />
                                        </Items>
                        </dxe:ASPxComboBox>

                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" Text="Date" runat="server" />
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_EventFrom" runat="server" EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" Text="-" runat="server" />
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_EventTo" runat="server" EditFormat="DateTime" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td colspan="2">
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="900px" AutoGenerateColumns="False"  OnHtmlEditFormCreated="grid_Transport_HtmlEditFormCreated" DataSourceID="dsTransport" OnRowInserting="grid_Transport_RowInserting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" >
                <SettingsPager Mode="ShowAllRecords" >
                </SettingsPager>
                <SettingsEditing Mode="EditForm" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Caption="#" VisibleIndex="0" Width="100">

                        <EditButton Visible="true" Text="Edit"></EditButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerNo" FieldName="ContainerNo" VisibleIndex="1"
                        SortIndex="1" SortOrder="Ascending" Width="300" ReadOnly="true">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ContainerType" FieldName="ContainerType" VisibleIndex="2" Width="100" ReadOnly="true">
                        <EditFormSettings Visible="False" />
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="EventCode" FieldName="EventCode" VisibleIndex="3" Width="180">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataDateColumn Caption="DateTime" FieldName="EventDateTime" VisibleIndex="4" Width="150" ReadOnly="true">
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataComboBoxColumn Caption="State" FieldName="State" VisibleIndex="5" Width="180">
                    </dxwgv:GridViewDataComboBoxColumn>
                    <dxwgv:GridViewDataTextColumn Caption="EventPort" FieldName="EventPort" VisibleIndex="5" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Depot/Yard" FieldName="EventDepot" VisibleIndex="6"
                        Width="100">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <Templates>
                    <EditForm>
                        <table>
                            <tr>
                                <td>Cont No</td>
                                <td>
                                    <div style="display:none">
                                    <dxe:ASPxTextBox ID="txt_sequenceId" runat="server" Text='<%# Eval("id") %>' ></dxe:ASPxTextBox>
                                        <dxe:ASPxTextBox ID="txt_EventCode" runat="server" Text='<%# Eval("EventCode") %>'></dxe:ASPxTextBox>
                                    </div>
                                    <dxe:ASPxTextBox ID="txt_ContainerNo" Width="100%" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("ContainerNo") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>Cont Type</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_ContainerType" Width="100%" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("ContainerType") %>'>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td>EventCode</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cb_EventCode" Width="100%" runat="server" Value='<%# Bind("EventCode") %>'>
                                    </dxe:ASPxComboBox>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <div style="text-align: right; padding: 2px 2px 2px 2px">
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton"
                                            runat="server"></dxwgv:ASPxGridViewTemplateReplacement>

                                    </div>
                                </td>
                            </tr>
                        </table>
                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
