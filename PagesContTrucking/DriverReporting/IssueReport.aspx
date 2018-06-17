<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="IssueReport.aspx.cs" Inherits="PagesContTrucking_DriverReporting_IssueReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title></title>
    <script type="text/javascript">

        var isUpload = false;
        function PopupUploadPhoto() {
            popubCtrPic.SetHeaderText('Upload Attachment');
            popubCtrPic.SetContentUrl('../Upload1.aspx?Type=IssueRp&Sn=' + txt_Id.GetText());
            popubCtrPic.Show();
        }
    </script>
    <style type="text/css">
        .f_button {
            background-color: #f6f6f6;
            border: 1px solid #808080;
            padding-top: 5px;
            padding-right: 12px;
            padding-bottom: 5px;
            padding-left: 10px;
            background-image: url(http://localhost:3319/DXR.axd?r=1_19-qE9x8);
        }

            .f_button:hover {
                cursor: pointer;
            }

            .f_button a {
                color: black;
                text-decoration: none;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsTransport" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.VehicleIssueReport" KeyMember="Id" FilterExpression="1=0" />
        <wilson:DataSource ID="dsJobPhoto" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmAttachment" KeyMember="Id" FilterExpression="1=0" />
        <table>
            <tr>
                <td>Driver</td>
                <td>
                    <dxe:ASPxTextBox ID="search_driver" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>Vehicle</td>
                <td>
                    <dxe:ASPxTextBox ID="search_vehicle" runat="server" Width="120"></dxe:ASPxTextBox>
                </td>
                <td>Date From:</td>
                <td>
                    <dxe:ASPxDateEdit ID="search_from" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit>
                </td>
                <td>To:</td>
                <td>
                    <dxe:ASPxDateEdit ID="search_to" runat="server" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy" Width="120"></dxe:ASPxDateEdit>
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
            </tr>
        </table>
        <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" DataSourceID="dsTransport" KeyFieldName="Id" OnRowInserting="grid_Transport_RowInserting" OnRowDeleting="grid_Transport_RowDeleting" OnRowUpdating="grid_Transport_RowUpdating" OnInitNewRow="grid_Transport_InitNewRow" OnInit="grid_Transport_Init">
            <SettingsPager PageSize="100" />
            <SettingsEditing Mode="EditForm" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn VisibleIndex="0" Width="100">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True">
                    </DeleteButton>
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn FieldName="CreateBy" Caption="Driver" Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="VehicleNo" Caption="VehicleNo" Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataDateColumn FieldName="ReportDate" Caption="ReportDate" Width="100" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" PropertiesDateEdit-EditFormatString="dd/MM/yyyy">
                </dxwgv:GridViewDataDateColumn>
                <dxwgv:GridViewDataComboBoxColumn FieldName="ActionType" Caption="ActionType" Width="100">
                    <PropertiesComboBox>
                        <Items>
                            <dxe:ListEditItem Value="" Text="" />
                            <dxe:ListEditItem Value="Prime Mover" Text="Prime Mover" />
                            <dxe:ListEditItem Value="Trailer" Text="Trailer" />
                            <dxe:ListEditItem Value="Accident" Text="Accident" />
                            <dxe:ListEditItem Value="Others" Text="Others" />
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn FieldName="ActionTaken" Caption="ActionTaken" Width="100">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Description" Caption="Des" Width="200"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Note" Caption="Note" Width="200"></dxwgv:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <EditForm>

                    <div style="display: none">
                        <dxe:ASPxTextBox ID="txt_Id" ClientInstanceName="txt_Id" runat="server" Value='<%# Eval("Id")%>'></dxe:ASPxTextBox>
                    </div>
                    <table>
                        <tr>
                            <td colspan="6"></td>
                            <td>
                                <span style="float: right">&nbsp;
                                    
                                    <button class="f_button">
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="CancelButton" ReplacementType="EditFormCancelButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    </button>
                                </span>
                                <span style='float: right;'>
                                    <button class="f_button">
                                        <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateButton" ReplacementType="EditFormUpdateButton" runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                    </button>
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td>Driver:</td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_Driver" runat="server" Value='<%# Bind("CreateBy") %>' Width="150"></dxe:ASPxTextBox>
                            </td>
                            <td>VehicleNo:</td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_VehicleNo" runat="server" Value='<%# Bind("VehicleNo") %>' Width="150"></dxe:ASPxTextBox>
                            </td>
                            <td>ReportDate:</td>
                            <td>
                                <dxe:ASPxDateEdit ID="txt_JobDate" runat="server" Value='<%# Bind("ReportDate") %>' Width="150" DisplayFormatString="dd/MM/yyyy" EditFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>ActionType:</td>
                            <td>
                                <dxe:ASPxComboBox ID="cbb_ActionType" runat="server" Value='<%# Bind("ActionType") %>' Width="150">
                                    <Items>
                                        <dxe:ListEditItem Value="" Text="" />
                                        <dxe:ListEditItem Value="Prime Mover" Text="Prime Mover" />
                                        <dxe:ListEditItem Value="Trailer" Text="Trailer" />
                                        <dxe:ListEditItem Value="Accident" Text="Accident" />
                                        <dxe:ListEditItem Value="Others" Text="Others" />
                                    </Items>
                                </dxe:ASPxComboBox>
                            </td>
                            <td>ActionTaken:</td>
                            <td>
                                <dxe:ASPxTextBox ID="txt_ActionTaken" runat="server" Value='<%# Bind("ActionTaken") %>' Width="150"></dxe:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Description:</td>
                            <td colspan="3">
                                <dxe:ASPxMemo ID="txt_Description" runat="server" Value='<%# Bind("Description") %>' Width="300"></dxe:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td>Note:</td>
                            <td colspan="3">
                                <dxe:ASPxMemo ID="txt_Note" runat="server" Value='<%# Bind("Note") %>' Width="300"></dxe:ASPxMemo>
                            </td>
                        </tr>
                    </table>
                    <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="980px">
                        <TabPages>
                            <dxtc:TabPage Text="Attachments" Name="Attachments" Visible="true">
                                <ContentCollection>
                                    <dxw:ContentControl ID="ContentControl8" runat="server">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dxe:ASPxButton ID="ASPxButton6" Width="150" runat="server" Text="Upload Attachments"
                                                        Enabled='<%# SafeValue.SafeInt(Eval("Id"),0)>0 %>' AutoPostBack="false"
                                                        UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s,e) {
                                                                isUpload=true;
                                                        PopupUploadPhoto();
                                                        }" />
                                                    </dxe:ASPxButton>
                                                </td>
                                                <td>
                                                    <dxe:ASPxButton ID="btn_Refresh" runat="server" Text="Refresh" AutoPostBack="false"
                                                        UseSubmitBehavior="false">
                                                        <ClientSideEvents Click="function(s,e) {
                                                        grd_Photo.Refresh();
                                                        }" />
                                                    </dxe:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <dxwgv:ASPxGridView ID="grd_Photo" ClientInstanceName="grd_Photo" runat="server" DataSourceID="dsJobPhoto"
                                            KeyFieldName="Id" Width="100%" EnableRowsCache="False" OnBeforePerformDataSelect="grd_Photo_BeforePerformDataSelect"
                                            AutoGenerateColumns="false" OnRowDeleting="grd_Photo_RowDeleting" OnInit="grd_Photo_Init" OnInitNewRow="grd_Photo_InitNewRow" OnRowUpdating="grd_Photo_RowUpdating">
                                            <Settings />
                                            <Columns>
                                                <dxwgv:GridViewDataColumn Caption="#" VisibleIndex="0" Width="40px">
                                                    <DataItemTemplate>
                                                        <dxe:ASPxButton ID="btn_mkg_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                            ClientSideEvents-Click='<%# "function(s) { grd_Photo.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                        </dxe:ASPxButton>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn Caption="#" Width="40px">
                                                    <DataItemTemplate>
                                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server"
                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grd_Photo.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                        </dxe:ASPxButton>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataColumn Caption="Photo" Width="100px">
                                                    <DataItemTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <a href='<%# Eval("ImgPath")%>' target="_blank">
                                                                        <dxe:ASPxImage ID="ASPxImage1" Width="80" Height="80" runat="server" ImageUrl='<%# Eval("ImgPath") %>'>
                                                                        </dxe:ASPxImage>
                                                                    </a>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </DataItemTemplate>
                                                </dxwgv:GridViewDataColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="FileName" FieldName="FileName" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Cont No" FieldName="ContainerNo" Width="200px"></dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote"></dxwgv:GridViewDataTextColumn>
                                            </Columns>
                                            <Templates>
                                                <EditForm>
                                                    <div style="display: none">
                                                        <dxe:ASPxTextBox ID="txt_PhotoId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                                    </div>
                                                    <table width="100%">
                                                        <tr>
                                                            <td>Remark
                                                            </td>
                                                            <td>
                                                                <dxe:ASPxMemo ID="txt_Rmk" runat="server" Rows="4" Width="600" Text='<%# Bind("FileNote") %>'>
                                                                </dxe:ASPxMemo>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="text-align: right; padding: 2px 2px 2px 2px">
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="UpdateMkgs" ReplacementType="EditFormUpdateButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                    <dxwgv:ASPxGridViewTemplateReplacement ID="CancelMkgs" ReplacementType="EditFormCancelButton"
                                                                        runat="server"></dxwgv:ASPxGridViewTemplateReplacement>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </EditForm>
                                            </Templates>
                                        </dxwgv:ASPxGridView>
                                    </dxw:ContentControl>
                                </ContentCollection>
                            </dxtc:TabPage>
                        </TabPages>
                    </dxtc:ASPxPageControl>
                </EditForm>
            </Templates>
        </dxwgv:ASPxGridView>

        <dxpc:ASPxPopupControl ID="popubCtrPic" runat="server" CloseAction="CloseButton" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrPic"
            HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="570"
            Width="900" EnableViewState="False">
            <ClientSideEvents CloseUp="function(s, e) {
                    if(grd_Photo!=null)
                        grd_Photo.Refresh();
      
}" />
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
