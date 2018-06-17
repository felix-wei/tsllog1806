<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DispatchPlanner_Stage_level1.aspx.cs" Inherits="PagesContTrucking_MasterData_DispatchPlanner_Stage_level1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="ds_stage" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.CtmDispatchPlannerStage" KeyMember="Id" />
            <div style="display: none">
                <dxe:ASPxLabel ID="lb_ParId" runat="server"></dxe:ASPxLabel>
            </div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){gv.AddNewRow();}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="gv" ClientInstanceName="gv" runat="server" Width="500px" AutoGenerateColumns="false" DataSourceID="ds_stage" KeyFieldName="Id" OnRowInserting="gv_RowInserting" OnRowUpdating="gv_RowUpdating" OnRowDeleting="gv_RowDeleting" OnInit="gv_Init">
                <SettingsPager PageSize="1000" />
                <SettingsEditing Mode="Inline" />
                <SettingsBehavior ConfirmDelete="true" />
                <Columns>
                    <dxwgv:GridViewCommandColumn Width="20%">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Stage" FieldName="Stage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="SortIndex" FieldName="SortIndex" CellStyle-HorizontalAlign="Left" Width="10%">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
