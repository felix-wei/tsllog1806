<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectAssignedCargo.aspx.cs" Inherits="Modules_Freight_SelectPages_SelectAssignedCargo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="../../Script/ContTrucking/JobEdit.js"></script>
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/Basepages.js"></script>
    <link href="../script/f_dev.css" rel="stylesheet" />
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.AfterPopub(); }
        }
        document.onkeydown = keydown;
        function AfterClose(v) {
            if (v != null)
            {
                alert(v);
                parent.AfterPopub();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsWh" runat="server" ObjectSpace="C2.Manager.ORManager" TypeName="C2.JobHouse"
            KeyMember="Id" FilterExpression="1=0" />
        <div style="display: none">
            <dxe:ASPxLabel ID="lbl_Id" runat="server"></dxe:ASPxLabel>
        </div>
        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Assign" AutoPostBack="false" UseSubmitBehavior="false">
            <ClientSideEvents  Click="function(s,e){
                   grid.GetValuesOnCustomCallback('Assign', AfterClose);
                }"/>
        </dxe:ASPxButton>
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="dsWh"
                KeyFieldName="Id" Width="1000px" AutoGenerateColumns="False" OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsBehavior ConfirmDelete="true" />
                <SettingsEditing Mode="Inline" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="JobNo" FieldName="JobNo" Width="160px" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Qty" FieldName="QtyOrig" VisibleIndex="2" Width="60">
                        <DataItemTemplate>
                            <dxe:ASPxSpinEdit ID="spin_Qty" Value='<%# Eval("QtyOrig") %>' runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Weight" FieldName="WeightOrig" VisibleIndex="2" Width="60">
                         <DataItemTemplate>
                             <dxe:ASPxSpinEdit ID="spin_Weight" Value='<%# Eval("WeightOrig") %>' runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Volume" FieldName="Volume" VisibleIndex="2" Width="60">
                         <DataItemTemplate>
                             <dxe:ASPxSpinEdit ID="spin_Volume" Value='<%# Eval("VolumeOrig") %>' runat="server" Width="60" Increment="0" SpinButtons-ShowIncrementButtons="false"></dxe:ASPxSpinEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ship Date" FieldName="ShipDate" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <%--<%# SafeValue.SafeDateStr(Eval("TptDate")) %>--%>
                            <dxe:ASPxDateEdit ID="date_ShipDate" runat="server" Width="120px" Value='<%# Eval("ShipDate") %>'
                                EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                            </dxe:ASPxDateEdit>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Ship Index" FieldName="ShipIndex" VisibleIndex="3" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_ShipIndex" ClientInstanceName="cbb_ShipIndex" Value='<%# Eval("ShipIndex") %>' runat="server" Width="80">
                                <Items>
                                    <dxe:ListEditItem Text="1" Value="1" Selected="true" />
                                    <dxe:ListEditItem Text="2" Value="2" />
                                    <dxe:ListEditItem Text="3" Value="3" />
                                    <dxe:ListEditItem Text="4" Value="4" />
                                    <dxe:ListEditItem Text="5" Value="5" />
                                    <dxe:ListEditItem Text="6" Value="6" />
                                    <dxe:ListEditItem Text="7" Value="7" />
                                    <dxe:ListEditItem Text="8" Value="8" />
                                    <dxe:ListEditItem Text="9" Value="9" />
                                    <dxe:ListEditItem Text="10" Value="10" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Port Code" FieldName="ShipPortCode" VisibleIndex="3" Width="120">
                        <DataItemTemplate>
                            <dxe:ASPxTextBox ID="txt_ShipPortCode" Width="120" runat="server" Text='<%# Eval("ShipPortCode") %>'>
                            </dxe:ASPxTextBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Cont Index" FieldName="ContIndex" VisibleIndex="3" Width="80">
                        <DataItemTemplate>
                            <dxe:ASPxComboBox ID="cbb_ContIndex" ClientInstanceName="cbb_ContIndex" Value='<%# Eval("ContIndex") %>' runat="server" Width="80">
                                <Items>
                                    <dxe:ListEditItem Text="1" Value="1" Selected="true" />
                                    <dxe:ListEditItem Text="2" Value="2" />
                                    <dxe:ListEditItem Text="3" Value="3" />
                                    <dxe:ListEditItem Text="4" Value="4" />
                                    <dxe:ListEditItem Text="5" Value="5" />
                                    <dxe:ListEditItem Text="6" Value="6" />
                                    <dxe:ListEditItem Text="7" Value="7" />
                                    <dxe:ListEditItem Text="8" Value="8" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>

                </Columns>
                <Settings ShowFooter="True" />
                <TotalSummary>
                    <dxwgv:ASPxSummaryItem FieldName="ContNo" SummaryType="Count" DisplayFormat="{0}" />
                </TotalSummary>
            </dxwgv:ASPxGridView>
    </form>
</body>
</html>
