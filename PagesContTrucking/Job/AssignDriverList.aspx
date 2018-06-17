<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignDriverList.aspx.cs" Inherits="PagesContTrucking_Job_AssignDriverList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function PopupAssignDriver() {
            popubCtrAD.SetContentUrl('AssignDriverEdit.aspx');
            popubCtrAD.Show();
        }
        function AfterPopupAD() {
            popubCtrAD.Hide();
            popubCtrAD.SetContentUrl('about:blank');
            document.getElementById("btn_Search").click();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Driver Code</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_SearchDriveCode" ClientInstanceName="btn_SearchDriveCode" runat="server" HorizontalAlign="Left" AutoPostBack="False" Width="120">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                PopupCTM_Driver(btn_SearchDriveCode,null);
                            }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>ContainerNo</td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_SearchContNo" ClientInstanceName="btn_SearchContNo" runat="server"  AutoPostBack="False" Width="120">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupContainer(btn_SearchContNo,null);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>

                    <td>From Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_SearchFromDate" runat="server" Width="120" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>To Date</td>
                    <td>
                        <dxe:ASPxDateEdit ID="date_SearchToDate" runat="server" Width="120" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Search" ClientInstanceName="btn_Search" runat="server" Text="Retrieve" OnClick="btn_Search_Click"></dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_AddNew" runat="server" Text="Assign Driver" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) {
                                    PopupAssignDriver();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="850" AutoGenerateColumns="False">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsEditing Mode="EditForm" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="Container" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DriverCode" Caption="Driver Code" Width="100">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="TowheadCode" Caption="PrimeMover"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="ChessisCode" Caption="ChessisCode"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="CarrierBkgNo" Caption="CarrierBkgNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="EtaDate" Caption="Eta" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataDateColumn FieldName="EtdDate" Caption="Etd" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dxwgv:GridViewDataDateColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
                Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(grid!=null)
	    grid.Refresh();
	    grid=null;
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtrAD" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrAD"
                HeaderText="Assign Driver" AllowDragging="True" EnableAnimation="False" Height="550"
                AllowResize="True" Width="800" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
