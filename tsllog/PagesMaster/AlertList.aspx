<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AlertList.aspx.cs" Inherits="PagesMaster_AlertList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsSysAlert" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SysAlert" KeyMember="Id" FilterExpression="1=0" />
            <table>
                <tr>
                    <td><dxe:ASPxLabel ID="lbl_Code" runat="server" Text="No"></dxe:ASPxLabel></td>
                    <td><dxe:ASPxTextBox ID="txt_Code" runat="server" Width="100"></dxe:ASPxTextBox></td>
                    <td><dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Subject"></dxe:ASPxLabel></td>
                    <td><dxe:ASPxTextBox ID="txt_Subject" runat="server" Width="100"></dxe:ASPxTextBox></td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="From"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Type"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_AlertType" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Text="All" Value="All"/>
                                <dxe:ListEditItem Text="Job" Value="Job"/>
                                <dxe:ListEditItem Text="Container" Value="Container" />
                                <dxe:ListEditItem Text="Trip" Value="Trip" />
                                <dxe:ListEditItem Text="Vehicle" Value="Vehicle" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsSysAlert" Width="1500" 
                KeyFieldName="Id"  OnInit="grid_Init" OnRowInserting="grid_RowInserting" OnRowUpdating="grid_RowUpdating" 
                OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting"  >
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="True">
                        </DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                     <dxwgv:GridViewDataMemoColumn FieldName="AlertType" Caption="Type" Width="100"></dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="RefNo" Caption="Ref No" Width="100"></dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobNo" Caption="Job No" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="Job Type" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DocNo" Caption="Doc No" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="DocType" Caption="Doc Type" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SendType" Caption="Send Type" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataMemoColumn FieldName="SendMethod" Caption="Send Method" Width="80">
                    </dxwgv:GridViewDataMemoColumn>
                    <dxwgv:GridViewDataColumn FieldName="SendFrom" Caption="Send From" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="SendTo" Caption="Send To" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Subject" Caption="Subject" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Message" Caption="Message" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="StatusCode" Caption="StatusCode" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="CreateUser" Caption="CreateUser" Width="120"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="CreateTime" Caption="CreateTime" Width="120"></dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="true"/>
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="Subject" ShowInColumn="Subject"
                        SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>

            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
