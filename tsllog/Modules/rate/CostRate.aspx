<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostRate.aspx.cs" Inherits="Page_CostRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quotation Edit</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Script/BasePages.js">
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.SeaQuoteDet1" KeyMember="SequenceId" FilterExpression="QuoteId = '-1' and Status1='COST'" />

        <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="ChgTypeId='CFS'" />
        <wilson:DataSource ID="dsParty" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="GroupId='VENDOR' OR GroupId='HAULIER'" />
        <wilson:DataSource ID="dsAgent" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="GroupId='CFS'" />
		
                    <h2>Costing Rate</h2><hr>
<table>
            <tr>
                <td>
                    Type
                </td>
                <td>
										<dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="120" ID="cbx_Type">
                                        <Items>
                                           <dxe:ListEditItem Text="-" Value="Local" />
                                           <dxe:ListEditItem Text="Local Move" Value="Local Move" />
                                           <dxe:ListEditItem Text="Office Move" Value="Office Move" />
                                           <dxe:ListEditItem Text="Outbound" Value="Outbound" />
                                           <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                           <dxe:ListEditItem Text="Storage" Value="Storage" />
                                        </Items>
                                        </dxe:ASPxComboBox>					
                </td>
     
                <td>
                    Mode (OB)
                </td>
                <td>
                     
										<dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="120" ID="cbx_Mode">
                                        <Items>
                                           <dxe:ListEditItem Text="-" Value="LOCAL" />
											<dxe:ListEditItem Text="LCL" Value="LCL" />
											<dxe:ListEditItem Text="20FT" Value="20FT" />
											<dxe:ListEditItem Text="40FT" Value="40FT" />
											<dxe:ListEditItem Text="40HC" Value="40HC" />
											<dxe:ListEditItem Text="20CON" Value="20CON" />
											<dxe:ListEditItem Text="40CON" Value="40CON" />
											<dxe:ListEditItem  Text="AIR" Value="AIR"/>
                                        </Items>
                                        </dxe:ASPxComboBox>					
                </td>
				<td>
                    <dxe:ASPxButton ID="btn_search" Width="100" runat="server" Text="Search" OnClick="btn_search_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="btn_Export" Width="100" runat="server" Text="Save Excel" OnClick="btn_export_Click">
                    </dxe:ASPxButton>
                </td>
                <td>
                    <dxe:ASPxButton ID="ASPxButton1" Width="110" runat="server" Text="Add New" OnClick="btn_add_Click">
                    </dxe:ASPxButton>
                </td>
            </tr>
        </table>
		<hr>
        <table>
            <tr>
                <td colspan="6">
                    <dxwgv:ASPxGridView ID="grid1" ClientInstanceName="grid1" runat="server"
                        DataSourceID="ds1" KeyFieldName="SequenceId" OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting"
                        OnRowInserting="Grid1_RowInserting" OnInitNewRow="Grid1_InitNewRow"
                        OnInit="Grid1_Init" Width="100%" AutoGenerateColumns="False">
                        <SettingsEditing Mode="Inline" />
						<SettingsBehavior ConfirmDelete="True" />
                        <SettingsPager Mode="ShowAllRecords" />
                        <Columns>
                            <dxwgv:GridViewCommandColumn Visible="true" VisibleIndex="0" Width="5%">
                        <EditButton Visible="True" />
                        <DeleteButton Visible="true" />
                    </dxwgv:GridViewCommandColumn>

                                                <dxwgv:GridViewDataComboBoxColumn FieldName="Status1" Caption="Type" Width="120" VisibleIndex="1">
                                                    <PropertiesComboBox>
                                                        <Items>
                                           <dxe:ListEditItem Text="Local Move" Value="Local Move" />
                                           <dxe:ListEditItem Text="Office Move" Value="Office Move" />
                                           <dxe:ListEditItem Text="Outbound" Value="Outbound" />
                                           <dxe:ListEditItem Text="Inbound" Value="Inbound" />
                                           <dxe:ListEditItem Text="Storage" Value="Storage" />
                                                        </Items>
                                                    </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>							
                                               						
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="Status2" Caption="Optional" Width="120" VisibleIndex="98">
                                                    <PropertiesComboBox>
                                                        <Items>
                                     <dxe:ListEditItem Text="-" Value="LOCAL" />
											<dxe:ListEditItem Text="LCL" Value="LCL" />
											<dxe:ListEditItem Text="20FT" Value="20FT" />
											<dxe:ListEditItem Text="40FT" Value="40FT" />
											<dxe:ListEditItem Text="40HC" Value="40HC" />
											<dxe:ListEditItem Text="20CON" Value="20CON" />
											<dxe:ListEditItem Text="40CON" Value="40CON" />
											<dxe:ListEditItem  Text="AIR" Value="AIR"/>
                                                        </Items>
                                                    </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>			
  
                                        					
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="ChgCode" Caption="ChargeCode" Width="90" VisibleIndex="1">
												<PropertiesComboBox DataSourceId="dsRate" ValueType="System.String" TextField="ChgCodeId" ValueField="ChgCodeId">
                                                   </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>							
                                              
                           <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgCode" VisibleIndex="5"
                                Width="80">
								<DataItemTemplate><%# R.Charge(Eval("ChgCode")) %></DataItemTemplate>
								<EditItemTemplate><%# R.Charge(Eval("ChgCode")) %></EditItemTemplate>
                            </dxwgv:GridViewDataTextColumn>
                           <dxwgv:GridViewDataTextColumn Caption="Default Rate" FieldName="Price" VisibleIndex="5"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                           <dxwgv:GridViewDataTextColumn Caption="Min Charge" FieldName="MinAmt" VisibleIndex="5"
                                Width="80">
                            </dxwgv:GridViewDataTextColumn>
                                   
                            <dxwgv:GridViewDataTextColumn Caption="Remark" FieldName="Rmk" VisibleIndex="99"
                                Width="150">
                            </dxwgv:GridViewDataTextColumn>
							</Columns>
                        <Settings ShowFooter="true" />
                        <TotalSummary>
                            <dxwgv:ASPxSummaryItem FieldName="ChgCode" SummaryType="Count" DisplayFormat="{0:0}" />
                        </TotalSummary>
                        
                    </dxwgv:ASPxGridView>
                </td>
            </tr>
        </table>
    </div>
    <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
        HeaderText="Ar Invoice Edit" AllowDragging="True" EnableAnimation="False" Height="400"
        Width="800" EnableViewState="False">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    </form>
</body>
</html>
