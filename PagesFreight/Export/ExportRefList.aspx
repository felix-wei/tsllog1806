<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportRefList.aspx.cs" Inherits="Pages_ExportRefList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export Ref</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            window.location = "ExportRefEdit.aspx?no=" + masterId+"&refType="+lab_RefType.GetText();
        }
        function ShowHouse(masterNo, jobNo) {
            window.location = "ExportEdit.aspx?masterNo=" + masterNo + "&no=" + jobNo;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td style="display:none">
                        <dxe:ASPxLabel ID="lab_RefType" ClientInstanceName="lab_RefType" runat="server" Text="">
                        </dxe:ASPxLabel>

                    </td>
                    <td>Ref No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_RefNo" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>Container No
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Cont" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        Ocean BL
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Obl" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>HBL No
                       </td>
                    <td>
                       <dxe:ASPxTextBox ID="txt_HblN" Width="100px" runat="server">
                    </dxe:ASPxTextBox></td>
                </tr>
                <tr>
                    <td>POD
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_SchPod" ClientInstanceName="txt_SchPod" runat="server"
                            Width="100" HorizontalAlign="Left" AutoPostBack="False" MaxLength="5">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                           PopupPort(txt_SchPod,null);
                            }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        Vessel
                    </td>
                    <td colspan="3">
                        <dxe:ASPxTextBox ID="txt_Ves" Width="290" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        Voyage
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Voy" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td>Agent
                    </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_AgtId" ClientInstanceName="txt_AgtId" runat="server"
                                        Width="100" HorizontalAlign="Left" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupAgent(txt_AgtId,txt_AgtName);
                                }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                    <td colspan="2">
                                    <dxe:ASPxTextBox ID="ASPxTextBox1" ClientInstanceName="txt_AgtName" ReadOnly="true" BackColor="Control" Width="210" runat="server">
                                    </dxe:ASPxTextBox>
                    </td>
                    <td>ETD From
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                    <td>To
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Customer
                    </td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="txt_CustId" ClientInstanceName="txt_CustId" runat="server"
                                        Width="100" HorizontalAlign="Left" AutoPostBack="False">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                            PopupCust(txt_CustId,txt_CustName);
                                }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                    <td colspan="2">
                                    <dxe:ASPxTextBox ID="txt_CustName" ClientInstanceName="txt_CustName" ReadOnly="true" BackColor="Control" Width="210" runat="server">
                                </dxe:ASPxTextBox>
                    </td>
                    <td colspan="4">
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_Export_Click">
                                    </dxe:ASPxButton>
                                </td>
                                <td>
                                    <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add"
                                        AutoPostBack="False" UseSubmitBehavior="False">
                                        <ClientSideEvents Click="function(s, e) {
                                    ShowJob('0');	
                                    }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        <dxwgv:ASPxGridView ID="grid_ref" ClientInstanceName="grid_ref" runat="server" KeyFieldName="SequenceId"
           AutoGenerateColumns="False" OnHtmlRowPrepared="grid_ref_HtmlRowPrepared">
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsBehavior ConfirmDelete="True" />
            <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="900" PopupEditFormHorizontalAlign="WindowCenter"
                PopupEditFormVerticalAlign="WindowCenter" />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="Ref No" Width="100" VisibleIndex="1"
                        SortIndex="1">
                        <DataItemTemplate>
                            <a onclick="ShowJob('<%# Eval("RefNo") %>');"><%# Eval("RefNo")%></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ref Status " FieldName="StatusCode" Width="100" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="OBL" FieldName="OBL" Width="100" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Vessel" FieldName="Vessel" Width="100" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Eta" FieldName="Eta" Width="100" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Etd" FieldName="Etd" Width="100" VisibleIndex="6">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Pod" FieldName="Pod" Width="100" VisibleIndex="7">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" Width="100" VisibleIndex="20"
                        SortIndex="1">
                        <DataItemTemplate>
                            <a onclick="ShowHouse('<%# Eval("RefNo") %>','<%# Eval("JobNo")%>');"><%# Eval("JobNo")%></a>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="HBL" FieldName="HBL" Width="100" VisibleIndex="21">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="CustomerName" FieldName="CustomerName" Width="100" VisibleIndex="22">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="Weight" Width="60" VisibleIndex="24">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="volume" Width="60" VisibleIndex="25">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="Qty" Width="80" VisibleIndex="26">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Status " FieldName="JobStatusCode" Width="100" VisibleIndex="100">
                    </dxwgv:GridViewDataTextColumn>
            </Columns>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_ref">
        </dxwgv:ASPxGridViewExporter>
        
         <dxpc:ASPxPopupControl id="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
            HeaderText="Customer" AllowDragging="True" EnableAnimation="False" height="300"
            AllowResize="True" width="500" EnableViewState="False">
        </dxpc:ASPxPopupControl>
        
        </div>
    </form>
</body>
</html>
