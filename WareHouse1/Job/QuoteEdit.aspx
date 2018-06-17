<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoteEdit.aspx.cs" Inherits="WareHouse_Job_Quote_Edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            parent.navTab.openTab(masterId, "/Warehouse/Job/JobEdit.aspx?refN=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function PrintDo() {
            window.open("/ReportJob/PrintDoSpj.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
        }
        function PrintIv() {
            window.open("/ReportJob/PrintIvSpj.aspx?DateTo=" + txt_end.GetText());
        }
    
     
        function OnCopyClick(id) {
            grid.GetValuesOnCustomCallback('Copy' + id, OnScheduleCallback);
        }
    </script>
		 <style media="print">
	.noprint {display:none;}
	.doprint {font-size:10pt;}
	.onlyprint {font-size:10pt;}

	</style>	
</head>
<body>
    <form id="form1" runat="server">
    <wilson:DataSource ID="dsChgCode" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode"
                KeyMember="SequenceId"  FilterExpression="ChgTypeId='Billing'" />
	    <wilson:DataSource ID="dsJobCost" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobCost"
                KeyMember="SequenceId" FilterExpression="1=0"  />
        <div>
		
		
<dxe:ASPxButton ID="ASPxButton13x" Width="120" runat="server" Text="Add New Item"                                                 
                                                AutoPostBack="false" UseSubmitBehavior="false" >
                                                <ClientSideEvents Click="function(s,e) {
                                                       grid_ref_Cont.AddNewRow();
                                                        }" />
                                            </dxe:ASPxButton>		<table width=900 style="display:none;">
											<tr>
											<td>Quotation Date</td>
											<td>
                                              <dxe:ASPxDateEdit ID="date_DateTime1" Width="100" runat="server" Value='<%# Eval("DateTime1") %>'
                                            EditFormat="DateTime" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
 
											</td>
											</tr></table>
		

               <dxwgv:ASPxGridView ID="Grid_RefCont" ClientInstanceName="grid_ref_Cont" runat="server"
                                                KeyFieldName="SequenceId" DataSourceID="dsJobCost" Width="900" OnBeforePerformDataSelect="Grid_RefCont_DataSelect"
                                                OnRowUpdating="Grid_RefCont_RowUpdating" OnRowInserting="Grid_RefCont_RowInserting"
                                                OnRowDeleting="Grid_RefCont_RowDeleting" OnInitNewRow="Grid_RefCont_InitNewRow"
                                                OnInit="Grid_RefCont_Init">
                                                <SettingsBehavior ConfirmDelete="True" />
                                                <Columns>
                                                    <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                                        <DataItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_edit" runat="server" Text="Edit" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.StartEditRow("+Container.VisibleIndex+") }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_del" runat="server" 
                                                                            Text="Delete" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"Confirm Delete\")){grid_ref_Cont.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </DataItemTemplate>
                                                        <EditItemTemplate>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_update" runat="server" Text="Update" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.UpdateEdit()() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                    <td>
                                                                        <dxe:ASPxButton ID="btn_cont_cancel" runat="server" Text="Cancel" Width="40" AutoPostBack="false"
                                                                            ClientSideEvents-Click='<%# "function(s) { grid_ref_Cont.CancelEdit() }"  %>'>
                                                                        </dxe:ASPxButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EditItemTemplate>
                                                    </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataMemoColumn Caption="Description" Width="300" FieldName="ChgCodeDe" VisibleIndex="2" PropertiesMemoEdit-Rows="8">
                                                    </dxwgv:GridViewDataMemoColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="Amount" Width="100" FieldName="SalePrice" VisibleIndex="2" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataMemoColumn Caption="Charge Remark" Width="300" PropertiesMemoEdit-Rows="8" FieldName="Remark" VisibleIndex="2" >
                                                    </dxwgv:GridViewDataMemoColumn>
                                                    <dxwgv:GridViewDataComboBoxColumn
                                                        Caption="Charge Code" FieldName="ChgCode" VisibleIndex="1">
                                                        <PropertiesComboBox
                                                            ValueType="System.String" TextFormatString="{0}" DataSourceID="dsChgCode"
                                                            TextField="ChgCodeDe" EnableIncrementalFiltering="true"
                                                            ValueField="ChgCodeId" DropDownWidth="100">
                                                            <Columns>
                                                                <dxe:ListBoxColumn FieldName="ChgCodeId" Width="70px" />
                                                                <dxe:ListBoxColumn FieldName="ChgCodeDe" Width="100%" />
                                                            </Columns>
                                                        </PropertiesComboBox>
                                                    </dxwgv:GridViewDataComboBoxColumn>
                                                </Columns>
                                                <SettingsEditing Mode="InLine" />
                                            </dxwgv:ASPxGridView>
        </div>
    </form>
</body>
</html>
