<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Party.aspx.cs" Inherits="WareHouse_MastData_Party" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
       <title>Party</title>
        <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script>
        function OnCloseCallBack(v) {
            if (v == "Success") {
                alert("Action Success!");
                grid.Refresh();
            }
            else if (v == "Fail")
                alert("Action Fail,please try again!");
        }
    </script>
</head>
<body>
   <form id="form1" runat="server">
        <div>
            <wilson:DataSource ID="dsXXParty" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXParty" KeyMember="SequenceId" />
            <wilson:DataSource ID="dsPartyGroup" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXPartyGroup" KeyMember="Code" />

            <table width="450">
                <tr>
                    <td>Name
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Name" Width="300" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" Width="100" runat="server" Text="Retrieve" OnClick="btn_Sch_Click">
                            <ClientSideEvents Click="function(s,e){
                        window.location='Party.aspx?name='+txt_Name.GetText();
                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" runat="server" ClientInstanceName="grid" DataSourceID="dsXXParty"
                KeyFieldName="SequenceId" AutoGenerateColumns="False"
                Width="1000px" OnInit="grid_Init" OnInitNewRow="grid_InitNewRow" OnRowDeleting="grid_RowDeleting" OnCustomDataCallback="grid_CustomDataCallback" OnCustomCallback="grid_CustomCallback" OnHtmlEditFormCreated="grid_HtmlEditFormCreated">
                <SettingsEditing Mode="EditForm" />
                <SettingsPager PageSize="100" Mode="ShowPager">
                </SettingsPager>
                <SettingsCustomizationWindow Enabled="True" />
                <Settings ShowFilterRow="false" />
                <SettingsBehavior ConfirmDelete="True" />
                <Columns>
                    <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="50">
                        <EditButton Visible="true"></EditButton>
                        <DeleteButton Visible="true" Text="Delete"></DeleteButton>
                    </dxwgv:GridViewCommandColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="PartyId" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" VisibleIndex="2" />
                    <dxwgv:GridViewDataTextColumn Caption="Name" FieldName="Name" VisibleIndex="3" />
                    <dxwgv:GridViewDataTextColumn Caption="Tel" FieldName="Tel1" VisibleIndex="4" />
                    <dxwgv:GridViewDataTextColumn Caption="Fax" FieldName="Fax1" VisibleIndex="5" />
                    <dxwgv:GridViewDataTextColumn Caption="Email" FieldName="Email1" VisibleIndex="6" />
                    <dxwgv:GridViewDataTextColumn Caption="IsCust" FieldName="IsCustomer" VisibleIndex="7" />
                </Columns>
                <Styles Header-HorizontalAlign="Center">
                    <Header HorizontalAlign="Center"></Header>
                    <Cell HorizontalAlign="Center"></Cell>
                </Styles>
                <Templates>
                    <EditForm>
                        <table style="text-align: right; padding: 2px 2px 2px 2px; width: 100%">
                            <tr>
                                <td width="90%"></td>
                                 <td>
                                    <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="Back" AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    grid.PerformCallback('Cancle');
                                                 }" />
                                    </dxe:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <dxtc:ASPxPageControl ID="pageControl" runat="server" Width="100%" Height="400px" EnableCallBacks="true" BackColor="#F0F0F0" ActiveTabIndex="0">
                            <TabPages>
                                <dxtc:TabPage Text="Info" Visible="true" >
                                    <ContentCollection >
                                        <dxw:ContentControl>
                                            <div style="display: none">
                                                <dxe:ASPxTextBox ID="txt_SequenceId" ClientInstanceName="txt_Id" runat="server" ReadOnly="true"
                                                    BackColor="Control" Text='<%# Eval("SequenceId") %>' Width="170">
                                                </dxe:ASPxTextBox>
                                            </div>
                                            <div style="padding: 4px 4px 3px 4px;">
                                                <table width="70%">
                                                    <tr>
                                                        <td>Party Id：
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtPartyId" runat="server" Width="100%" Value='<%# Eval("PartyId") %>'>
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>UEN No:</td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtCrNo" runat="server" BackColor="Control" ReadOnly="true" EnableIncrementalFiltering="True" Value='<%# Eval("CrNo") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Short Name：
                                                        </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtCode" runat="server" BackColor="Control" ReadOnly="true"  Width="100%" Value='<%# Eval("Code") %>'>
                                                            </dxe:ASPxTextBox>
                                                            <dxe:ASPxLabel ID="lblMessage" runat="server" Text=""></dxe:ASPxLabel>
                                                        </td>
                                                        <td>Group:</td>
                                                        <td>
                                                            <dxe:ASPxComboBox ID="cbGroup" runat="server" BackColor="Control" ReadOnly="true"  DataSourceID="dsPartyGroup" TextFormatString="{0}" EnableIncrementalFiltering="True" IncrementalFilteringMode="StartsWith" Value='<%# Eval("GroupId") %>' TextField="Code" ValueField="Code" Width="100%">
                                                                <Columns>
                                                                    <dxe:ListBoxColumn FieldName="Code" Width="50px" />
                                                                    <dxe:ListBoxColumn FieldName="Description" Width="100%" />
                                                                </Columns>
                                                            </dxe:ASPxComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Full Name： </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxTextBox ID="txtName" runat="server" BackColor="Control" ReadOnly="true"  Value='<%# Eval("Name") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Address： </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="txtAddress" BackColor="Control" ReadOnly="true"  runat="server" Rows="3" Value='<%# Eval("Address") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Telephone： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtTel1" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Tel1") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Fax： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtFax1" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Fax1") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtTel2" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Tel2") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtFax2" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Fax2") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Contact： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtContact1" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Contact1") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td>Email： </td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtEmail1" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Email1") %>' Width="100%">
                                                            </dxe:ASPxTextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtContact2" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Contact2") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                        <td></td>
                                                        <td>
                                                            <dxe:ASPxTextBox ID="txtEmail2" BackColor="Control" ReadOnly="true"  runat="server" Value='<%# Eval("Email2") %>' Width="100%">
                                                            </dxe:ASPxTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Remarks： </td>
                                                        <td colspan="3">
                                                            <dxe:ASPxMemo ID="memoRemark"  runat="server" Rows="3" Value='<%# Eval("Remark") %>' Width="100%">
                                                            </dxe:ASPxMemo>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="6">
                                                            <hr>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 80px;">Creation</td>
                                                                    <td style="width: 160px"><%# Eval("CreateBy")%> @ <%# SafeValue.SafeDateStr( Eval("CreateDateTime"))%></td>
                                                                    <td style="width: 100px;">Last Updated</td>
                                                                    <td style="width: 160px; text-align: center"><%# Eval("UpdateBy")%> @ <%# SafeValue.SafeDateStr(Eval("UpdateDateTime"))%></td>
                                                                </tr>
                                                            </table>
                                                            <hr>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </div>

                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                                <dxtc:TabPage Text="Price" ActiveTabStyle-Height="600">
                                    <ContentCollection>
                                        <dxw:ContentControl Height="600">
                                            <table width="450">
                                                <tr>
                                                    <td>
                                                        <dxe:ASPxButton ID="btn_Add" Width="100" runat="server" Text="Add New" AutoPostBack="false" Enabled='<%# SafeValue.SafeString(Eval("SequenceId"),"0")!="0"&&(SafeValue.SafeString(Eval("Status"),"USE")!="InActive")%>'>
                                                            <ClientSideEvents Click="function(s,e){
                                grid_Price.AddNewRow();
                                }" />
                                                        </dxe:ASPxButton>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                            <div>
                                                <dxwgv:ASPxGridView ID="grid_Price" ClientInstanceName="grid_Price" runat="server"></dxwgv:ASPxGridView>
                                            </div>
                                        </dxw:ContentControl>
                                    </ContentCollection>
                                </dxtc:TabPage>
                            </TabPages>
                            <TabStyle BackColor="#F0F0F0">
                            </TabStyle>
                            <ContentStyle BackColor="#F0F0F0">
                            </ContentStyle>
                        </dxtc:ASPxPageControl>

                    </EditForm>
                </Templates>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
        </div>
    </form>
</body>
</html>
