<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mo_clm.aspx.cs" Inherits="WareHouse_Job_Mo_CLM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript">
        function ShowJob(masterId) {
            parent.navTab.openTab(masterId, "/Modules/WareHouse/Job/JobEdit.aspx?refN=" + masterId, { title: masterId, fresh: false, external: true });
        }
        function PrintDo() {
            window.open("/ReportJob/PrintDoClm.aspx?DateFrom=" + txt_from.GetText() + "&DateTo=" + txt_end.GetText());
        }
        function PrintIv() {
            window.open("/ReportJob/PrintInvoiceSpj.aspx?DateTo=" + txt_end.GetText());
        }
    
     
        function OnCopyClick(id) {
            grid.GetValuesOnCustomCallback('Copy' + id, OnScheduleCallback);
        }
		
		function OnAddMat(cd,qt) {
		    grid.GetValuesOnCustomCallback('Add#' + _id + "#" + cd + "#" + qt, OnAddCallback);
        }
		function OnScheduleCallback()
		{
			grid.Refresh();
		}
		
		var _id = 0;
		function OnAddCallback(v)
		{
			alert(v);
			AddMat(false,0);
			btn_search.DoClick();
		}
		function AddMat(doShow,id) {
            if (doShow) {
				_id = id;
                pcAdd.Show();

            }
            else {
				_id = 0;
                pcAdd.Hide();
            }
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
                    <wilson:DataSource ID="dsStock" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.Material"
                KeyMember="Id" FilterExpression="1=0" />
                    <wilson:DataSource ID="dsMaterial" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.RefMaterial"
                KeyMember="Id" FilterExpression="Note1='SPJ'" />
                    <wilson:DataSource ID="dsParty" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty2"
                KeyMember="SequenceId" FilterExpression="IsCustomer='true'"    />
				      
            <wilson:DataSource ID="dsTerm" runat="server" ObjectSpace="C2.Manager.ORManager"
                TypeName="C2.XXTerm" KeyMember="SequenceId" />

        <div>
            <table class="noprint">
                <tr>
                    <td>
						
                        <dxe:ASPxLabel ID="Label1x" runat="server" Text="Do/Invoice No :">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_no" Width="100" runat="server">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label1" runat="server" Text=" Date :">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_from" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                        <div style="display: none">
                            <dxe:ASPxDateEdit ID="txt_date" ClientInstanceName="txt_date" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </div>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="Label2" runat="server" Text="To">
                        </dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_end" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                        </dxe:ASPxDateEdit>
                        <div style="display: none">
                            <dxe:ASPxDateEdit ID="txt_date2" ClientInstanceName="txt_date2" Width="100" runat="server" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </div>
                    </td>
                      <td>
                        <dxe:ASPxButton ID="btn_search" Width="120" ClientInstanceName="btn_search" runat="server" Text="Search" OnClick="btn_search_Click">
                        </dxe:ASPxButton>
                    </td>
                     <td>
                        <dxe:ASPxButton ID="btn_Add" Width="120px" runat="server" Text="Add New"
                            AutoPostBack="False" UseSubmitBehavior="False">
                            <ClientSideEvents Click="function(s, e) {
                                 grid.AddNewRow();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
             <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsStock" 
				KeyFieldName="Id" Width="900" AutoGenerateColumns="False" 
				OnRowUpdating="grid_RowUpdating" 
				OnRowInserting="grid_RowInserting" 
                OnRowDeleting="grid_RowDeleting" 
				OnInitNewRow="grid_InitNewRow"
				OnCustomDataCallback="grid_CustomDataCallback" 
				OnInit="grid_Init"
				>
				<Settings ShowFilterRow="True" />
                <SettingsEditing Mode="Inline" />
				<SettingsBehavior AllowSort="false" AllowGroup="false" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                                        <dxwgv:GridViewDataColumn Caption="#" Width="80" VisibleIndex="0">
                                            <DataItemTemplate>
                                                <div  >
                                                    <a href="#" onclick='<%# "grid.StartEditRow("+Container.VisibleIndex+"); " %>'>Edit</a>
                                                    <a href="#" onclick='if(confirm("Confirm Delete it?"))  {<%# "grid.DeleteRow("+Container.VisibleIndex+");"  %>}'>
                                                        Del</a>
                                                </div>
                                            </DataItemTemplate>
											<EditItemTemplate>
                                                <div  >
                                                    <a href="#" onclick='<%# "grid.UpdateEdit("+Container.VisibleIndex+"); " %>'>Save</a>
                                                    <a href="#" onclick='grid.CancelEdit();'>
                                                        Cancel</a>
                                                </div>
											
											</EditItemTemplate>
											</dxwgv:GridViewDataColumn>

 
                  
                    <dxwgv:GridViewDataDateColumn Caption="Date" FieldName="DocDate" VisibleIndex="2" Width="80" SortOrder="Descending" SortIndex="1">
                       <PropertiesDateEdit Width="100" DisplayFormatString="dd/MM/yyyy" EditFormat="DateTime" EditFormatString="dd/MM/yyyy"></PropertiesDateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Do No" FieldName="DocNo" VisibleIndex="2" Width="80" SortOrder="Descending" SortIndex="2">
						<DataItemTemplate>
							<b><%# Eval("DocNo")%></b>
							<div style='display:<%# Eval("DoType","{0}") == "OUT3" ? "" : "none" %>'>
						<a href='/ReportJob/PrintDoClm.aspx?no=<%# Eval("DocNo")%>' Target=_blank>Print</a>
							</div>
						</DataItemTemplate>
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="BillNo" VisibleIndex="2" Width="80">
						<DataItemTemplate>
							<b><%# Eval("BillNo")%></b>
							<div style='display:<%# Eval("DoType","{0}") == "OUT3" ? "" : "none" %>'>
							<br>
							<a href='javascript:AddMat(true,<%# Eval("Id")%>)' >Add</a>
							</div>
						</DataItemTemplate>
					</dxwgv:GridViewDataTextColumn>
                 <dxwgv:GridViewDataComboBoxColumn Visible="false" Caption="Term" Width="80" FieldName="BillTerm" VisibleIndex="2" >
                                    <PropertiesComboBox DataSourceID="dsTerm" ValueField="Code" TextField="Code" 
									DropDownStyle="DropDownList" ValueType="System.String" IncrementalFilteringMode="StartsWith">
                                    </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>					
                 <dxwgv:GridViewDataComboBoxColumn Caption="Item Code" Width="80" FieldName="Code" VisibleIndex="2" >
                                    <PropertiesComboBox DataSourceID="dsMaterial" ValueField="Code" TextField="Name" 
									DropDownStyle="DropDownList" ValueType="System.String" IncrementalFilteringMode="StartsWith">
                                    </PropertiesComboBox>
                    </dxwgv:GridViewDataComboBoxColumn>					
                    <dxwgv:GridViewDataTextColumn Caption="Req New" FieldName="RequisitionNew" VisibleIndex="2" Width="60">
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Req Used" FieldName="RequisitionUsed" VisibleIndex="2" Width="60">
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Add New" FieldName="AdditionalNew" VisibleIndex="2" Width="60">
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Add Used" FieldName="AdditionalUsed" VisibleIndex="2" Width="60">
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Return New" FieldName="ReturnedNew" VisibleIndex="2" Width="60">
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Return Used" FieldName="ReturnedUsed" VisibleIndex="2" Width="60">
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Total New" FieldName="TotalNew" VisibleIndex="2" Width="60">
					<EditItemTemplate />
					<DataItemTemplate>
					<%# S.Int(Eval("TotalNew")) * -1 %>
					</DataItemTemplate>
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Total Used" FieldName="TotalUsed" VisibleIndex="2" Width="60">
					<EditItemTemplate />
					<DataItemTemplate>
					<%# S.Int(Eval("TotalUsed")) * -1 %>
					</DataItemTemplate>
					</dxwgv:GridViewDataTextColumn>
					
                    <dxwgv:GridViewDataTextColumn Caption="Unit"  FieldName="Code" VisibleIndex="2" Width="80">
					<DataItemTemplate>
						<%# R.MatUnit(Eval("Code")) %>
					</DataItemTemplate>
					<EditItemTemplate />
					</dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Whole/Loose"  FieldName="Code" VisibleIndex="2" Width="80">
					<DataItemTemplate>
						<%# R.MatLoose(Eval("Code"),Eval("Qty")) %>
					</DataItemTemplate>
					<EditItemTemplate />
					</dxwgv:GridViewDataTextColumn>
                  			
                </Columns>
                <Settings ShowFooter="True" />
                 <TotalSummary>
                     <dxwgv:ASPxSummaryItem  FieldName="DocNo" DisplayFormat="{0}" SummaryType="Count"/>
                     <dxwgv:ASPxSummaryItem  FieldName="Qty" DisplayFormat="{0}" SummaryType="Sum"/>
                     <dxwgv:ASPxSummaryItem  FieldName="GstAmt" DisplayFormat="{0:0.00}" SummaryType="Sum"/>
                     <dxwgv:ASPxSummaryItem  FieldName="DocAmt" DisplayFormat="{0:0.00}" SummaryType="Sum"/>
                 </TotalSummary>
            </dxwgv:ASPxGridView>
            <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid">
            </dxwgv:ASPxGridViewExporter>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500px"
                AllowResize="True" Width="900px" EnableViewState="False">
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500px"
                AllowResize="True" Width="800px" EnableViewState="False">
            </dxpc:ASPxPopupControl>
			
<dxpc:ASPxPopupControl ID="pcAdd" ClientInstanceName="pcAdd" SkinID="None" Width="240px"
                ShowOnPageLoad="false" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="TopSides"
                AllowDragging="True" CloseAction="None" PopupElementID="popupArea"
                EnableViewState="False" runat="server" PopupHorizontalOffset="0"
                PopupVerticalOffset="0" EnableHierarchyRecreation="True">
                <HeaderTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 100%;">Add New Material
                            </td>
                            <td>
                                <a id="a_X" onclick="AddMat(false,0)" onmousedown="event.cancelBubble = true;" style="width: 15px; height: 14px; cursor: pointer;">X</a>
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <ContentStyle>
                    <Paddings Padding="0px" />
                </ContentStyle>
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                         <div style="padding: 2px 2px 2px 2px;width:660px">
                            <div style="display: none">
                              
                            </div>
                           <table style="text-align: left; padding: 2px 2px 2px 2px; width: 650px">
                                
                             
                                   <td>Material</td>
                                   <td>
                                       <dxe:ASPxComboBox ID="cmb_Code" ClientInstanceName="cmb_Code" runat="server" Width="160" 
         DataSourceID="dsMaterial" ValueField="Code" TextField="Name" 
									DropDownStyle="DropDownList" ValueType="System.String" IncrementalFilteringMode="StartsWith">
                                       </dxe:ASPxComboBox>
                                   </td>
                               
                                     <td>Qty </td>
                                    <td>
								 
                                   <td>
									<div>
                                       <dxe:ASPxTextBox   ID="txt_Qty" Width="160px" runat="server" ClientInstanceName="txt_Qty">
                                       </dxe:ASPxTextBox>
									  </div>
                                   </td>
                               </tr>
                               <tr style="text-align: left;">
                                  
                               </tr>
                             
                            </table>
                            <table style="text-align: right; padding: 2px 2px 2px 2px; width: 660px">
                                 <tr>
                                   <td colspan="4" style="width:90%"></td>
                                    <td >

                                        <dxe:ASPxButton ID="btn_Ref_Save" runat="server" Width="80" AutoPostBack="false"
                                            Text="Save">
                                            <ClientSideEvents Click="function(s,e) {
                                                    OnAddMat(cmb_Code.GetValue(), txt_Qty.GetText());
                                                 }" />
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
			
        </div>
    </form>
</body>
</html>
