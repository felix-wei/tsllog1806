<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobCount.aspx.cs" Inherits="WareHouse_Job_JobCount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                    <td style="padding-left:40px;">
                        <dxe:ASPxDateEdit ID="txt_end" ClientInstanceName="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Add" Width="120px" runat="server" Text="Search"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                        		window.location = 'jobcount.aspx?d=' + txt_end.GetText();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
            </tr>
        </table>
        <wilson:DataSource ID="dsJobCount" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobCount" KeyMember="Id" />
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" DataSourceID="dsJobCount" OnRowUpdating="ASPxGridView1_RowUpdating"
            KeyFieldName="Id" AutoGenerateColumns="False" Width="400" OnInitNewRow="ASPxGridView1_InitNewRow" OnRowInserting="ASPxGridView1_RowInserting"
            oninit="ASPxGridView1_Init" OnRowDeleting="ASPxGridView1_RowDeleting" Styles-Cell-HorizontalAlign="left" >
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="Inline"/>
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="60">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="false" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Count" FieldName="Count" VisibleIndex="1">
                    <PropertiesSpinEdit SpinButtons-ShowIncrementButtons="false" Increment="0" NumberType="Integer"></PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Jobs" FieldName="Count" VisibleIndex="9">
                     <EditItemTemplate>
                        <%# D.One("select count(*) from JobSchedule Where WorkStatus <> 'Cancel' and JobDate>='"+Eval("DateFrom","{0:yyyy-MM-dd}")+" 00:00' and JobDate<='"+Eval("DateFrom","{0:yyyy-MM-dd}")+" 23:59'") %>
                     </EditItemTemplate>
                    <DataItemTemplate>
                        <%# D.One("select count(*) from JobSchedule Where WorkStatus <> 'Cancel' and JobDate>='"+Eval("DateFrom","{0:yyyy-MM-dd}")+" 00:00' and JobDate<='"+Eval("DateFrom","{0:yyyy-MM-dd}")+" 23:59'") %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataSpinEditColumn>
                <dxwgv:GridViewDataDateColumn Caption="Date From" ReadONly="true" FieldName="DateFrom" VisibleIndex="1">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"  EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                    <DataItemTemplate>
                        <%# Eval("DateFrom","{0:dd-MMM-yyyy}") + " " + Eval("DateFrom","{0:dddd}").Substring(0,3) %>
                    </DataItemTemplate>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn Caption="Date To" FieldName="DateTo" VisibleIndex="1" Visible="false">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm"  EditFormat="DateTime" EditFormatString="dd/MM/yyyy HH:mm"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1">
        </dxwgv:ASPxGridViewExporter>
        
    </div>
    </form>
</body>
</html>
