<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollProcessList.aspx.cs" Inherits="PagesHr_Job_PayrollProcessList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>PayrollProcessList</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsPayrollProcess" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.RefProcess" KeyMember="Id" FilterExpression="ProcessModule='HRS' and ProcessType='Payroll'" />
            <table>
                <tr>
                    <td>From Date
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To</td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Export" runat="server" Width="100" Text="Save Excel" OnClick="btn_Export_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" Width="110" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) {
                                                        grid_PayrollProcess.AddNewRow();;
                                                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <hr />
            <dxwgv:ASPxGridView ID="grid_PayrollProcess" DataSourceID="dsPayrollProcess" runat="server" Width="100%" KeyFieldName="Id"
                AutoGenerateColumns="False" ClientInstanceName="grid_PayrollProcess" OnInit="grid_PayrollProcess_Init" OnInitNewRow="grid_PayrollProcess_InitNewRow"
                OnRowInserting="grid_PayrollProcess_RowInserting" OnRowUpdating="grid_PayrollProcess_RowUpdating">
                <SettingsEditing Mode="Inline" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="false" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataDateColumn Caption="ProcessDate" FieldName="ProcessDateTime" VisibleIndex="1" Width="90">
                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                        <DataItemTemplate>
                            <dxe:ASPxDateEdit ID="date_ProcessDateTime" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" DropDownButton-Visible="false" 
                                Value='<%#Eval("ProcessDateTime") %>' runat="server" Width="90" ReadOnly="true" Border-BorderWidth="0">
                            </dxe:ASPxDateEdit>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxDateEdit ID="date_ProcessDateTime" EditFormat="Custom" EditFormatString="dd/MM/yyyy" Value='<%#Bind("ProcessDateTime") %>' runat="server" Width="100"
                                ClientEnabled='<%#SafeValue.SafeString(Eval("ProcessStatus"))!="Closed" %>'>
                            </dxe:ASPxDateEdit>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ProcessBy" FieldName="ProcessBy" VisibleIndex="2" Width="80">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox ID="txt_ProcessBy" runat="server" ReadOnly="true" Border-BorderWidth="0" Width="80" Text='<%#Bind("ProcessBy") %>'>
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ProcessStatus" FieldName="ProcessStatus" VisibleIndex="3" Width="80">
                        <EditItemTemplate>
                            <dxe:ASPxTextBox runat="server" ID="txt_ProcessStatus" ClientInstanceName="txt_ProcessStatus" Width="80" 
                                ReadOnly="true" Border-BorderWidth="0" Text='<%#Bind("ProcessStatus") %>'>
                            </dxe:ASPxTextBox>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ProcessRemark" FieldName="ProcessRemark" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption=" " FieldName="(None)" VisibleIndex="5" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxButton ID="btn_Process1" runat="server" AutoPostBack="false" Width="80" Text="Process" ClientEnabled='<%#SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("ProcessStatus"),"Closed")=="Open" %>'
                                OnCommand="btn_Process_Command" OnClick="btn_Process_Click" CommandName="Process" CommandArgument='<%# Eval("Id") %>'>
                            </dxe:ASPxButton>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <dxe:ASPxButton ID="btn_Process2" runat="server" AutoPostBack="false" Width="80" Text="Process" ClientEnabled='<%#SafeValue.SafeInt(Eval("Id"),0)>0&&SafeValue.SafeString(Eval("ProcessStatus"),"Closed")=="Open" %>'
                                OnCommand="btn_Process_Command" OnClick="btn_Process_Click" CommandName="Process" CommandArgument='<%# Eval("Id") %>'>
                            </dxe:ASPxButton>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_PayrollProcess">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="600" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
