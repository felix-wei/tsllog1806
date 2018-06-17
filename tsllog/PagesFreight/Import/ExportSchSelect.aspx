<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportSchSelect.aspx.cs" Inherits="Pages_Import_ExportSchSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export Ref</title>
    <script type="text/javascript" src="/Script/pages.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsExportRef" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.SeaExportRef" KeyMember="SequenceId" FilterExpression="1=0" />
            <table>
                <tr>
                    <td>Port
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="txt_Pod" ClientInstanceName="txt_Pod" runat="server" MaxLength="5" HorizontalAlign="Left" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                          PopupPort(txt_Pod,null);
                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Retrieve" UseSubmitBehavior="false"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s, e) {
    grid_Sch.PerformCallback(txt_Pod.GetText());
}" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Sch" ClientInstanceName="grid_Sch" runat="server" DataSourceID="dsExportRef"
                KeyFieldName="SequenceId" Width="750" OnCustomCallback="grid_CustomCallback">
                <SettingsPager Mode="ShowAllRecords" />
                <Columns>
                    <dxwgv:GridViewDataColumn FieldName="RefNo" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Vessel/Voy" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="POD" Visible="true" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Eta" VisibleIndex="7" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Etd" VisibleIndex="7" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataDateColumn Caption="EtaDest" VisibleIndex="7">
                    </dxwgv:GridViewDataDateColumn>
                </Columns>
                <Templates>
                    <DataRow>
                        <div style="padding: 5px">
                            <table width="750" style="border-bottom: solid 1px black;">
                                <tr style="font-weight: bold; font-size: 11px">
                                    <td style="width: 100px">
                                        <a onclick='parent.PutBooking("<%# Eval("RefNo") %>")'><%# Eval("RefNo") %></a>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("Vessel")%><%# Eval("Voyage")%>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("Pod")%>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("Eta", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("Etd", "{0:dd/MM/yyyy}")%>
                                    </td>
                                    <td style="width: 100px">
                                        <%# Eval("EtaDest", "{0:dd/MM/yyyy}")%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </DataRow>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="300"
                AllowResize="True" Width="450" EnableViewState="False">
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
