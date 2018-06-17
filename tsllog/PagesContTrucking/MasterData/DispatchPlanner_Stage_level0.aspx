<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner_Stage_level0.aspx.cs" Inherits="PagesContTrucking_MasterData_DispatchPlanner_Stage_level0" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script src="../../Script/ContTrucking/DispatchPlanner.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="ds_stage" runat="server"  ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmDispatchPlannerStage" KeyMember="Id" />
        <div>
            <table>
                <tr>
                    <td>Stage:</td>
                    <td>
                        <dxe:ASPxTextBox ID="search_stage" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrive" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){gv.AddNewRow();}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gv" ClientInstanceName="gv" runat="server" Width="500px" AutoGenerateColumns="false" DataSourceID="ds_stage" KeyFieldName="Id"  OnRowUpdating="gv_RowUpdating" OnInit="gv_Init"
                 OnRowDeleting="gv_RowDeleting" OnRowInserting="gv_RowInserting"
                 OnInitNewRow="gv_InitNewRow" >
                <SettingsPager PageSize="1000" />
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Width="20%">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Stage" FieldName="Stage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SortIndex" FieldName="SortIndex" CellStyle-HorizontalAlign="Left" Width="10%" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn>
                        <DataItemTemplate>
                            <a href="#" onclick='PopupStage_Level1(<%# Eval("Id") %>);'>Sub Stage</a>
                        </DataItemTemplate>
                        <EditItemTemplate>
                            <a href="#" onclick='PopupStage_Level1(<%# Eval("Id") %>);'>Sub Stage</a>
                            <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                            </div>
                        </EditItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
        
        <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Planner2" AllowDragging="True" EnableAnimation="False" Height="500"
            Width="800" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
