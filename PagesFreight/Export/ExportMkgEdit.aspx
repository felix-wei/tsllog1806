<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="ExportMkgEdit.aspx.cs" Inherits="PagesFreight_Export_ExportMkgEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsMarking" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.SeaExportMkg" KeyMember="SequenceId" FilterExpression="" />
        <table>
            <tr>
                <td>Ref No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_RefN" Width="120" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>Container No
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_ContN" Width="120" runat="server">
                    </dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                        <ClientSideEvents Click="function(s,e){
                        window.location='ExportMkgEdit.aspx?RefN='+txt_RefN.GetText();
                    }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Marking" ClientInstanceName="detailGrid" runat="server"
            KeyFieldName="SequenceId" Width="850px" AutoGenerateColumns="False" DataSourceID="dsMarking"
            OnInitNewRow="grid_Marking_InitNewRow" OnInit="grid_Marking_Init" OnCustomCallback="grid_Marking_CustomCallback" OnRowDeleting="grid_Marking_RowDeleting" OnRowInserting="grid_Marking_RowInserting" OnRowUpdating="grid_Marking_RowUpdating"
            OnCustomDataCallback="grid_Marking_CustomDataCallback" OnHtmlEditFormCreated="grid_Marking_HtmlEditFormCreated">
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
                <dxwgv:GridViewDataColumn FieldName="RefNo" VisibleIndex="1" Visible="true">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ContainerNo" Caption="ContNo" VisibleIndex="2" Visible="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ContainerType" Caption="ContType" VisibleIndex="3" Visible="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel" VisibleIndex="4" Visible="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage" VisibleIndex="5" Visible="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="SealNo" Caption="SealNo" VisibleIndex="6" Visible="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Pol" Caption="Pol" VisibleIndex="7" Visible="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Pod" Caption="Pod" VisibleIndex="8" Visible="true">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Eta" Caption="Eta" VisibleIndex="9" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="EtaDest" Caption="EtaDest" VisibleIndex="10" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PolEta" Caption="PolEta" VisibleIndex="11" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PolClearDate" Caption="PolClearDate" VisibleIndex="12" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PolReturnDate" Caption="PolReturnDate" VisibleIndex="13" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PodEta" Caption="PodEta" VisibleIndex="14" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PodClearDate" Caption="PodClearDate" VisibleIndex="15" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="PodReturnDate" Caption="PodReturnDate" VisibleIndex="16" Visible="true">
                    <PropertiesTextEdit DisplayFormatString="dd/MM/yyyy"></PropertiesTextEdit>
                </dxwgv:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div style="padding: 2px 2px 2px 2px">
                        <div style="display: none">
                            <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Text='<%# Eval("SequenceId") %>'>
                            </dxe:ASPxTextBox>
                            <dxe:ASPxTextBox ID="txt_AgentId" ClientInstanceName="txt_AgentId" runat="server" Text='<%# Eval("AgentId") %>'>
                            </dxe:ASPxTextBox>
                        </div>
                        <div>
                            <table>
                                <tr>
                                    <td>Ref No
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_RefNo" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("RefNo") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Eta
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_Eta" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Eta")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>Eta Dest
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_EtaDest" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("EtaDest")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Vessel
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Vessel" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Vessel") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Voyage
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Voyage" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Voyage") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Pod
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_Pod" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("Pod") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Agent
                                    </td>
                                    <td colspan="5">
                                        <dxe:ASPxTextBox ID="txt_AgentName" Width="635" ReadOnly="true" BackColor="Control" runat="server">
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Container No
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_ContainerNo" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("ContainerNo") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Container Type
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_ContainerType" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("ContainerType") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                    <td>Seal No
                                    </td>
                                    <td>
                                        <dxe:ASPxTextBox ID="txt_SealNo" Width="150" ReadOnly="true" BackColor="Control" runat="server" Value='<%# Eval("SealNo") %>'>
                                        </dxe:ASPxTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PolEta
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PolEta" Width="150" runat="server" Value='<%# Bind("PolEta")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>PolClearDate
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PolClearDate" Width="150" runat="server" Value='<%# Bind("PolClearDate")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>PolReturnDate
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PolReturnDate" Width="150" runat="server" Value='<%# Bind("PolReturnDate")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PolRemark
                                    </td>
                                    <td colspan="5">
                                        <dxe:ASPxMemo ID="memo_PolRemark" runat="server" Rows="2" Width="635" Value='<%# Bind("PolRemark")%>'>
                                        </dxe:ASPxMemo>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PodEta
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PodEta" Width="150" runat="server" Value='<%# Bind("PodEta")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>PodClearDate
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PodClearDate" Width="150" runat="server" Value='<%# Bind("PodClearDate")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                    <td>PodReturnDate
                                    </td>
                                    <td>
                                        <dxe:ASPxDateEdit ID="date_PodReturnDate" Width="150" runat="server" Value='<%# Bind("PodReturnDate")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td>PodRemark
                                    </td>
                                    <td colspan="5">
                                        <dxe:ASPxMemo ID="memo_PodRemark" runat="server" Rows="2" Width="635" Value='<%# Bind("PodRemark")%>'>
                                        </dxe:ASPxMemo>
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
                        </div>
                    </div>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>
    </form>
</body>
</html>
