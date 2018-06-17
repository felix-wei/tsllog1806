<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Letter.aspx.cs" Inherits="Modules_Hr_Master_Letter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="/Style/StyleSheet.css"  rel="stylesheet" type="text/css"/>
        <script src="/Script/jquery.js"></script>
       <script type="text/javascript">
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }
        var config = {
            timeout: 0,
            gridview: 'grid_Transport',
        }

        $(function () {
            loading.hide();
        })
        function upload_inline(rowIndex){
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                grid.GetValuesOnCustomCallback('UploadLine_'+rowIndex, upload_inline_callback);
            }, config.timeout);
        }
        function upload_inline_callback(res){
            popubCtrPic.SetHeaderText('Upload Letter ');
            popubCtrPic.SetContentUrl('UploadLetter.aspx?no='+res);
            popubCtrPic.Show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsPerson" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.HrPerson" KeyMember="Id" FilterExpression="Status='Employee' and Id>0" />
        <div>
        <table>
            <tr>
                <td>
                    <dxe:ASPxLabel ID="lbl_Employee" runat="server" Text="Employee"></dxe:ASPxLabel>
                </td>
                <td>
                    <dxe:ASPxComboBox runat="server" EnableIncrementalFiltering="true" ID="txtSchName" ClientInstanceName="txtSchName" TextFormatString="{1}"
                            EnableCallbackMode="true" IncrementalFilteringMode="StartsWith" DropDownStyle="DropDownList"
                            DataSourceID="dsPerson" ValueField="Id" TextField="Name" Width="100%" ValueType="System.Int32">
                            <Buttons>
                                <dxe:EditButton Text="Clear"></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                if(e.buttonIndex == 0){
                                s.SetText('');
                             }
                            }" />
                        </dxe:ASPxComboBox>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="100" runat="server" Text="Add New" AutoPostBack="false">
                        <ClientSideEvents Click="function(s,e){
                                grid.AddNewRow();
                                }" />
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
        <wilson:DataSource ID="dsCtmAttachment" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.CtmAttachment" KeyMember="Id"  FilterExpression="AttachType='LETTER'"/>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsCtmAttachment"
            Width="100%" KeyFieldName="Id" AutoGenerateColumns="False" OnInit="grid_Init" OnCellEditorInitialize="grid_CellEditorInitialize"
            OnInitNewRow="grid_InitNewRow" OnRowInserting="grid_RowInserting" OnRowDeleting="grid_RowDeleting" OnRowUpdating="grid_RowUpdating"
            OnCustomCallback="grid_CustomCallback"  OnCustomDataCallback="grid_CustomDataCallback"
            onrowinserted="grid_RowInserted">
            <SettingsPager Mode="ShowPager" PageSize="20">
            </SettingsPager>
            <SettingsEditing Mode="InLine" PopupEditFormWidth="900px" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="true" />
            <Columns>
                <dxwgv:GridViewCommandColumn Width="10%" VisibleIndex="0">
                    <EditButton Visible="True" />
                    <DeleteButton Visible="True" />
                </dxwgv:GridViewCommandColumn>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="0">
                    <DataItemTemplate>
                         <input type="button" class="button" value="Upload" onclick="upload_inline(<%# Container.VisibleIndex %>);" />
                        <div style="display:none">
                              <dxe:ASPxTextBox ID="txt_Id" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                        </div>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataTextColumn Caption="Path" FieldName="FilePath" VisibleIndex="0">
                    <DataItemTemplate>
                         <a href='<%# Eval("Path")%>' target="_blank">
                            <img src='<%# Eval("ImgPath") %>' width="150" height="150" id='foto_<%# Eval("Id")%>'
                                class='foto' />
                        </a>
                    </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataTextColumn Caption="File Type" FieldName="FileType" VisibleIndex="0">
                     <DataItemTemplate>
                         <%# SafeValue.SafeString(Eval("FileType")).ToUpper() %>
                         </DataItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Status" FieldName="AttachStatus" VisibleIndex="0" Width="300">
                    <PropertiesComboBox ValueType="System.String"  Width="150" TextFormatString="{1}" DropDownWidth="105"
                        TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                        <Items>
                            <dxe:ListEditItem  Text="Draft" Value="Draft"/>
                            <dxe:ListEditItem  Text="Approved" Value="Approved"/>
                            <dxe:ListEditItem  Text="Confirmed" Value="Confirmed"/>
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Employee" FieldName="Employee" VisibleIndex="0" Width="100">
                    <PropertiesComboBox ValueType="System.String" DataSourceID="dsPerson" Width="150" TextFormatString="{1}" DropDownWidth="105"
                        TextField="Name" EnableIncrementalFiltering="true" EnableCallbackMode="true" ValueField="Id" DataMember="{1}">
                        <Columns>
                            <dxe:ListBoxColumn FieldName="Id" Caption="Id" Width="35px" />
                            <dxe:ListBoxColumn FieldName="Name" />
                        </Columns>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="FileNote" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Letter Type" FieldName="TypeCode" VisibleIndex="1" Width="300">
                    <PropertiesComboBox ValueType="System.String"  Width="150" TextFormatString="{1}" DropDownWidth="105"
                        TextField="Name" EnableIncrementalFiltering="true" ValueField="Id" DataMember="{1}">
                        <Items>
                            <dxe:ListEditItem  Text="Offer Letter" Value="Offer Letter"/>
                            <dxe:ListEditItem  Text="Confirmation Letter" Value="Confirmation Letter"/>
                            <dxe:ListEditItem  Text="Termination" Value="Termination"/>
                            <dxe:ListEditItem  Text="Reference Letter" Value="Reference Letter"/>
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
            </Columns>
          </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtrPic" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtrPic"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="500"
                Width="600" AllowResize="true" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
                    

      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
