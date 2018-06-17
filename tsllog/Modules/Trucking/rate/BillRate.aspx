<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillRate.aspx.cs" Inherits="Page_BillRate" %>

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
        <wilson:DataSource ID="ds1" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.SeaQuoteDet1" KeyMember="SequenceId" FilterExpression="QuoteId = '-1' and Status1='BILL'" />

        <wilson:DataSource ID="dsRate" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsParty" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="" />
        <wilson:DataSource ID="dsAgent" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.XXParty" KeyMember="SequenceId" FilterExpression="GroupId='AGENT'" />
		
                    <h2>Trucking Billing Rate</h2><hr>
<table>
            <tr>
                <td>
                    Imp/Exp
                </td>
                <td>
										<dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="40" ID="cbx_ImpExp">
                                        <Items>
                                           <dxe:ListEditItem Text="-" Value="-" />
                                           <dxe:ListEditItem Text="I" Value="I" />
                                           <dxe:ListEditItem Text="E" Value="E" />
                                        </Items>
                                        </dxe:ASPxComboBox>					
                </td>
     
                <td>
                    Rate
                </td>
                <td>
                     
										<dxe:ASPxComboBox DataSourceId="dsRate" ValueType="System.String" TextField="ChgCodeId" ValueField="ChgCodeId" EnableIncrementalFiltering="True" runat="server" Width="90" ID="cbx_Rate">
                                        </dxe:ASPxComboBox>	
                </td>
                <td>
                   Client
                </td>
                <td>
										<dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="90" ID="cbx_Client">
                                        <Items>
                                           <dxe:ListEditItem Text="-" Value="-" />
                                           <dxe:ListEditItem Text="CLIENT1" Value="CLIENT1" />
                   
                                        </Items>
                                        </dxe:ASPxComboBox>	

										
                </td>
                <td>
                    Port
                </td>
                <td>
                    <dxe:ASPxTextBox ID="txt_Port" Width="100" runat="server">
                    </dxe:ASPxTextBox>
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

                                                <dxwgv:GridViewDataComboBoxColumn FieldName="Status2" Caption="I/E" Width="50" VisibleIndex="1">
                                                    <PropertiesComboBox>
                                                        <Items>
                                                            <dxe:ListEditItem Text="I" Value="I" />
                                                            <dxe:ListEditItem Text="E" Value="E" />
                                                        </Items>
                                                    </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>							
                                               						
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="Status4" Caption="Optional" Width="50" VisibleIndex="98">
                                                    <PropertiesComboBox>
                                                        <Items>
                                                            <dxe:ListEditItem Text="Y" Value="Y" />
                                                            <dxe:ListEditItem Text="N" Value="N" />
                                                        </Items>
                                                    </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>							
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="Status5" Caption="Client" Width="80" VisibleIndex="1">
                                                    <PropertiesComboBox>
                                                        <Items>
                                           <dxe:ListEditItem Text=" " Value=" " />
                                           <dxe:ListEditItem Text="APLLL" Value="APLLL" />
                                           <dxe:ListEditItem Text="ATFRM" Value="ATFRM" />
                                           <dxe:ListEditItem Text="BENKE" Value="BENKE" />
                                           <dxe:ListEditItem Text="SCHEN" Value="SCHEN" />
                                           <dxe:ListEditItem Text="VICTO" Value="VICTO" />
                                                        </Items>
                                                    </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>							
                                               					
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="Status6" Caption="Agent" Width="90" VisibleIndex="1">
												<PropertiesComboBox DataSourceId="dsAgent" ValueType="System.String" TextField="PartyId" ValueField="PartyId">
                                                   </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>							
							
                            <dxwgv:GridViewDataTextColumn Caption="Port" FieldName="Status7" VisibleIndex="3"
                                Width="90">
                            </dxwgv:GridViewDataTextColumn>
                                        					
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="ChgCode" Caption="ChargeCode" Width="90" VisibleIndex="1">
												<PropertiesComboBox DataSourceId="dsRate" ValueType="System.String" TextField="ChgCodeId" ValueField="ChgCodeId">
                                                   </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>							
                                                <dxwgv:GridViewDataComboBoxColumn FieldName="Unit" Caption="Unit" Width="80" VisibleIndex="5">
                                                    <PropertiesComboBox>
                                                        <Items>
                                <dxe:ListEditItem Text="20FT" Value="20FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40FT" Value="40FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="40HQ" Value="40HQ"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="45FT" Value="45FT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="4M3" Value="4M3"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SET" Value="SET"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="SHPT" Value="SHPT"></dxe:ListEditItem>
                                <dxe:ListEditItem Text="WT/M3" Value="WT/M3"></dxe:ListEditItem>
                                                        </Items>
                                                    </PropertiesComboBox>
                                                </dxwgv:GridViewDataComboBoxColumn>	
                           <dxwgv:GridViewDataTextColumn Caption="20GP" FieldName="Rate1" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="20HC" FieldName="Rate2" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="20FR" FieldName="Rate3" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="20RF" FieldName="Rate4" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="20OT" FieldName="Rate5" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="20HD" FieldName="Rate6" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="TANKER" FieldName="Rate7" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="40GP" FieldName="Rate8" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="40HC" FieldName="Rate9" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="40FR" FieldName="Rate10" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="40RF" FieldName="Rate11" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="40OT" FieldName="Rate12" VisibleIndex="5" Width="50" />
                           <dxwgv:GridViewDataTextColumn Caption="40HD" FieldName="Rate13" VisibleIndex="5" Width="50" />
  
 <dxwgv:GridViewDataTextColumn Caption="Min-Qty" FieldName="Qty" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Min-Amt" FieldName="MinAmt" VisibleIndex="5"
                                Width="50">
                            </dxwgv:GridViewDataTextColumn>
                                                <dxwgv:GridViewDataDateColumn FieldName="Date1" Caption="Effective" VisibleIndex="6"
                                                    Width="100">
                                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" />
                                                </dxwgv:GridViewDataDateColumn>
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
