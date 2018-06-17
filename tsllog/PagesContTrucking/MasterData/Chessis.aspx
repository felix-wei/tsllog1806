<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chessis.aspx.cs" Inherits="PagesContTrucking_MasterData_Chessis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">
        function gridView_callback(v) {
            console.log(v);
            if (v == "success") {
                detailGrid.CancelEdit();
                setTimeout(function () {
                    detailGrid.Refresh();
                }, 500);
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastData" KeyMember="Id" FilterExpression="type='chessis'" />
        <wilson:DataSource ID="dslog" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmMastDataLog" KeyMember="Id" />
        <table>
            <tr>
                <td>Trailer Code</td>
                <td>
                    <dxe:ASPxTextBox ID="txt_search_Code" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>Trailer Name</td>
                <td>
                    <dxe:ASPxTextBox ID="txt_search_Name" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_AddNew" runat="server" AutoPostBack="false" Text="Add New">
                        <ClientSideEvents Click="function(s,e){
                                detailGrid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_saveExcel" runat="server" Text="Save Excel" OnClick="btn_saveExcel_Click"></dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="1100px" DataSourceID="dsTransport" KeyFieldName="Id" OnRowInserting="grid_Transport_RowInserting" OnRowDeleting="grid_Transport_RowDeleting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init" OnHtmlDataCellPrepared="grid_Transport_HtmlDataCellPrepared" OnCustomDataCallback="grid_Transport_CustomDataCallback">
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="EditForm" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Code" Caption="Trailer Code" VisibleIndex="1" Width="100">
                    <EditItemTemplate>
                        <dxe:ASPxTextBox ID="txt" ReadOnly='<%# SafeValue.SafeString(Eval("Code")).Length>0 %>' Width="80%" runat="server" Text='<%# Bind("Code")%>'>
                        </dxe:ASPxTextBox>
                        <div style="display: none">
                            <dxe:ASPxLabel ID="lb_id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                        </div>
                    </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataColumn FieldName="Note2" Caption="Own">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Note3" Caption="Finance">
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn Caption="Size" FieldName="Remark" Width="100"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataDateColumn FieldName="Date1" Caption="Next Maintenance" Width="100">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="Date2" Caption="Next Inspection" Width="100">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="Date3" Caption="Insurance Expiry Date" Width="100">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="Date4" Caption="Road Tax Expiry Date" Width="100">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataDateColumn FieldName="Date5" Caption="VpcExpiryDate" Width="100">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataColumn Caption="Remark" FieldName="Note1" Width="150"></dxwgv:GridViewDataColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div style="display: none">
                        <dxe:ASPxLabel ID="lb_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxLabel>
                    </div>
                    <table>
                        <tr>
                            <td>Code</td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_Code" runat="server" Text='<%# Eval("Code") %>' Width="165"></dxe:ASPxTextBox>
                            </td>
                            <td>Size</td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_Size" runat="server" Text='<%# Eval("Remark") %>' Width="165"></dxe:ASPxTextBox>
                            </td>
                            <td>Status</td>
                            <td>
                                <dxe:ASPxComboBox ID="cbb_Status" runat="server" Text='<%# Eval("Type1") %>' Width="165">
                                    <Items>
                                        <dxe:ListEditItem Value="Active" Text="Active" />
                                        <dxe:ListEditItem Value="InActive" Text="InActive" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Self</td>
                            <td>
                                <dxe:ASPxComboBox ID="cbb_self" runat="server" Text='<%# Bind("Note2") %>' Width="165">
                                    <Items>
                                        <dxe:ListEditItem Value="Y" Text="Y" />
                                        <dxe:ListEditItem Value="N" Text="N" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>Finance</td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_finance" runat="server" Text='<%# Eval("Note3") %>' Width="165"></dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Remark</td>
                            <td colspan="3">
                                <dxe:ASPxMemo ID="txt_Remark" ClientInstanceName="txt_Remark" Rows="3" runat="server" Text='<%# Eval("Note1") %>' Width="100%">
                                </dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td>Next Maintenance</td>
                            <td>
                                <dxe:ASPxDateEdit ID="ASPxDateEdit1" runat="server" Width="165" Value='<%# Bind("Date1")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td>Next Inspection</td>
                            <td>
                                <dxe:ASPxDateEdit ID="ASPxDateEdit2" runat="server" Width="165" Value='<%# Bind("Date2")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td>Insurance Expiry Date</td>
                            <td>
                                <dxe:ASPxDateEdit ID="ASPxDateEdit3" runat="server" Width="165" Value='<%# Bind("Date3")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>Road Tax Expiry Date</td>
                            <td>
                                <dxe:ASPxDateEdit ID="ASPxDateEdit4" runat="server" Width="165" Value='<%# Bind("Date4")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                            <td>Vpc Expiry Date</td>
                            <td>
                                <dxe:ASPxDateEdit ID="ASPxDateEdit5" runat="server" Width="165" Value='<%# Bind("Date5")%>' EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                </dxe:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>

                    <div style='display: <%# SafeValue.SafeString(Eval("Type1"))=="InActive"?"none":"" %>'>
                        <dxe:ASPxButton ID="btn_borrow" runat="server" Text="Borrow" Enabled='<%# SafeValue.SafeString(Eval("Note2"))=="N" %>' AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){detailGrid.GetValuesOnCustomCallback('borrow',gridView_callback);}" />
                        </dxe:ASPxButton>
                        <dxe:ASPxButton ID="btn_bReturn" runat="server" Text="Borrow Return" Enabled='<%# SafeValue.SafeString(Eval("Note2"))=="N" %>' AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){detailGrid.GetValuesOnCustomCallback('return',gridView_callback);}" />
                        </dxe:ASPxButton>
                        <dxe:ASPxButton ID="btn_lend" runat="server" Text="Lend" Enabled='<%# SafeValue.SafeString(Eval("Note2"))!="N" %>' AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){detailGrid.GetValuesOnCustomCallback('borrow',gridView_callback);}" />
                        </dxe:ASPxButton>
                        <dxe:ASPxButton ID="btn_lReturn" runat="server" Text="Lend Return" Enabled='<%# SafeValue.SafeString(Eval("Note2"))!="N" %>' AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){detailGrid.GetValuesOnCustomCallback('return',gridView_callback);}" />
                        </dxe:ASPxButton>
                    </div>

                    <dxwgv:ASPxGridView ID="grid_log" ClientInstanceName="grid_log" runat="server" Width="800px" DataSourceID="dslog" KeyFieldName="Id" OnBeforePerformDataSelect="grid_Transport_BeforePerformDataSelect">
                        <SettingsPager PageSize="100" />
                        <Columns>
                            <dxwgv:GridViewDataColumn Caption="Code" FieldName="EventCode" Width="100"></dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataDateColumn FieldName="EventDate" Caption="DateTime" Width="100" SortOrder="Descending">
                                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy HH:mm:ss" EditFormatString="dd/MM/yyyy HH:mm:ss"></PropertiesDateEdit>
                            </dxwgv:GridViewDataDateColumn>
                        </Columns>
                    </dxwgv:ASPxGridView>


                    <table style="width: 100%;">
                        <tr>
                            <td colspan="12">
                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                    <span style="float: right">&nbsp;
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    </span>
                                    <span style='float: right;'>
                                        <a onclick="detailGrid.GetValuesOnCustomCallback('save',gridView_callback);" href="#">Update</a>
                                    </span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>
        <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
        </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
