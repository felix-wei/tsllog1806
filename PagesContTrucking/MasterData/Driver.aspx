<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Driver.aspx.cs" Inherits="PagesContTrucking_MasterData_Driver" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmDriver" KeyMember="Id"  />
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lbl_1" runat="server" Text="Driver Code"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_search_Code" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Driver Name"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_search_Name" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>
                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="Team"></dxe:ASPxLabel>

                </td>
                <td>
                    <dxe:ASPxComboBox ID="cbb_TeamNo" runat="server" Width="120">
                        <Items>
                            <dxe:ListEditItem Value="" Text="" />
                            <dxe:ListEditItem Value="A" Text="A" />
                            <dxe:ListEditItem Value="B" Text="B" />
                            <dxe:ListEditItem Value="C" Text="C" />
                            <dxe:ListEditItem Value="D" Text="D" />
                            <dxe:ListEditItem Value="E" Text="E" />
                            <dxe:ListEditItem Value="F" Text="F" />
                        </Items>
                    </dxe:ASPxComboBox>
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

        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" Width="850px" DataSourceID="dsTransport" OnRowDeleting="grid_Transport_RowDeleting" KeyFieldName="Id" OnHtmlEditFormCreated="grid_Transport_HtmlEditFormCreated" OnInit="grid_Transport_Init" OnRowUpdating="grid_Transport_RowUpdating" OnRowUpdated="grid_Transport_RowUpdated" OnInitNewRow="grid_Transport_InitNewRow" OnRowInserted="grid_Transport_RowInserted" OnRowInserting="grid_Transport_RowInserting" OnRowDeleted="grid_Transport_RowDeleted" OnHtmlRowPrepared="grid_Transport_HtmlRowPrepared" OnHtmlDataCellPrepared="grid_Transport_HtmlDataCellPrepared">
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="EditForm" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="10%">
                    <EditButton Visible="true"></EditButton>
                    <DeleteButton Visible="true"></DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataColumn FieldName="Code" Caption="Driver Code"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Name" Caption="Driver Name"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Tel"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="ICNo"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="TowheaderCode" Caption="PrimeMover"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="ServiceLevel"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Attendant"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Remark"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="StatusCode"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="TeamNo" Caption="Team"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="LicenseNo" Caption="License No"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="LicenseExpiry" Caption="License Expiry"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="BankAccount" Caption="Bank Account"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="Isstaff" Width="7%" Caption="Is Staff">
                    <DataItemTemplate><%# SafeValue.SafeString(Eval("Isstaff"),"N") %></DataItemTemplate>
                </dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="SalaryBasic" Caption="SalaryBasic"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="SalaryAllowance" Caption="SalaryAllowance"></dxwgv:GridViewDataColumn>
                <dxwgv:GridViewDataColumn FieldName="SalaryRemark" Caption="SalaryRemark"></dxwgv:GridViewDataColumn>
            </Columns>
            <Templates>
                <EditForm>
                    <div style="display: none">
                        <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>Driver Code</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Code" runat="server" Text='<%# Bind("Code") %>' Width="120" ReadOnly='<%# SafeValue.SafeString(Eval("Code")).Length>0 %>'></dxe:ASPxTextBox>
                                </td>
                                <td>Driver Name</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Name" runat="server" Text='<%# Bind("Name") %>' Width="120"></dxe:ASPxTextBox>
                                </td>
                                <td>Mobile</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_Tel" runat="server" Text='<%# Bind("Tel") %>' Width="120"></dxe:ASPxTextBox>
                                </td>
                                <td>Is Staff</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Staff" runat="server" Value='<%# Bind("Isstaff") %>' Width="120">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>IC No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_ICNo" runat="server" Text='<%# Bind("ICNo") %>' Width="120"></dxe:ASPxTextBox>
                                </td>
                                <td>TowheadCode</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_TowheadCode" ClientInstanceName="btn_TowheadCode" runat="server" Text='<%# Bind("TowheaderCode") %>' AutoPostBack="False" Width="120">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        Popup_TowheadList(btn_TowheadCode,null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>
                                </td>
                                <td>ServiceLevel</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_Level" runat="server" Value='<%# Bind("ServiceLevel") %>' Width="120">
                                        <Items>
                                            <dxe:ListEditItem Value="Level1" Text="Level1" />
                                            <dxe:ListEditItem Value="Level2" Text="Level2" />
                                            <dxe:ListEditItem Value="Level3" Text="Level3" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Status</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_StatusCode" runat="server" Value='<%# Bind("StatusCode") %>' Width="120">
                                        <Items>
                                            <dxe:ListEditItem Value="Active" Text="Active" />
                                            <dxe:ListEditItem Value="InActive" Text="InActive" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>License No</td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_LicenseNo" runat="server" Text='<%# Bind("LicenseNo") %>' Width="120"></dxe:ASPxTextBox>
                                </td>
                                <td>License Expiry
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="date_LicenseExpiry" Width="120" runat="server" Value='<%# Bind("LicenseExpiry")%>'
                                        EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>Bank Account
                                </td>
                                <td>
                                    <dxe:ASPxTextBox ID="txt_BankAccount" runat="server" Text='<%# Bind("BankAccount") %>' Width="120"></dxe:ASPxTextBox>
                                </td>
                                <td>Team
                                </td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_TeamNo" runat="server" Value='<%# Bind("TeamNo") %>' Width="120">
                                        <Items>
                                            <dxe:ListEditItem Value="A" Text="A" />
                                            <dxe:ListEditItem Value="B" Text="B" />
                                            <dxe:ListEditItem Value="C" Text="C" />
                                            <dxe:ListEditItem Value="D" Text="D" />
                                            <dxe:ListEditItem Value="E" Text="E" />
                                            <dxe:ListEditItem Value="F" Text="F" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>SalaryBasic</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="120"
                                        ID="spin_SalaryBasic" Height="21px" Value='<%# Bind("SalaryBasic")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>SalaryAllowance</td>
                                <td>
                                    <dxe:ASPxSpinEdit DisplayFormatString="0.00" runat="server" Width="120"
                                        ID="spin_SalaryAllowance" Height="21px" Value='<%# Bind("SalaryAllowance")%>' DecimalPlaces="2" Increment="0">
                                        <SpinButtons ShowIncrementButtons="false" />
                                    </dxe:ASPxSpinEdit>
                                </td>
                                <td>Sub-Contract ?</td>
                                <td>
                                    <dxe:ASPxComboBox ID="cbb_SubContract_Ind" runat="server" Value='<%# Bind("SubContract_Ind") %>' Width="120">
                                        <Items>
                                            <dxe:ListEditItem Value="Y" Text="Y" />
                                            <dxe:ListEditItem Value="N" Text="N" />
                                        </Items>
                                    </dxe:ASPxComboBox>
                                </td>
                                <td>Attendant</td>
                                <td>
                                    <dxe:ASPxButtonEdit ID="btn_Attendant" ClientInstanceName="btn_Attendant" runat="server" Text='<%# Bind("Attendant") %>' AutoPostBack="False" Width="120">
                                        <Buttons>
                                            <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                                        </Buttons>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupCTM_Driver(btn_Attendant,null, null);
                                                                        }" />
                                    </dxe:ASPxButtonEdit>

                                </td>
                            </tr>
                            <tr>
                                <td>SalaryRemark</td>
                                <td colspan="7">
                                    <dxe:ASPxMemo ID="txt_SalaryRemark" Rows="3" runat="server" Text='<%# Bind("SalaryRemark") %>' Width="100%"></dxe:ASPxMemo>
                                </td>
                            </tr>
                            <tr>
                                <td>Remark</td>
                                <td colspan="7">
                                    <dxe:ASPxMemo ID="memo_Remark" Rows="3" runat="server" Text='<%# Bind("Remark") %>' Width="100%"></dxe:ASPxMemo>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="8">
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
                </EditForm>
            </Templates>
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
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_Transport">
            </dxwgv:ASPxGridViewExporter>
    </form>
</body>
</html>
