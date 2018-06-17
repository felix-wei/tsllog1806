<%@ Page Language="C#" EnableViewState="false" AutoEventWireup="true" CodeFile="ChgCodeList.aspx.cs" Inherits="SelectPage_ChgCodeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function $(s) {
            return document.getElementById(s) ? document.getElementById(s) : s;
        }
        function keydown(e) {
            if (e.keyCode == 27) { parent.ClosePopupCtr(); }
        }
        document.onkeydown = keydown;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wilson:DataSource ID="dsChgcodeMast" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.XXChgCode" KeyMember="SequenceId" FilterExpression="ImpExpInd='Export'" />
            
        <dxwgv:ASPxGridView ID="ASPxGridView1" ClientInstanceName="grid" runat="server" Width="100%" DataSourceID="dsChgcodeMast"
         KeyFieldName="SequenceId" >
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <Settings ShowFilterRow="true" />
            <SettingsCustomizationWindow Enabled="True" />
            <SettingsBehavior ConfirmDelete="True" AllowFocusedRow="True"  />
            <Columns>
                <dxwgv:GridViewDataTextColumn Caption="#" VisibleIndex="1" Width="5%">
                <DataItemTemplate>
               <a onclick='parent.PutValue("<%# Eval("ChgcodeId") %>","<%# Eval("ChgcodeDe") %>")'>
                                Select</a>
                </DataItemTemplate>
                <EditItemTemplate>
                </EditItemTemplate>
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Charge Code" FieldName="ChgcodeId" VisibleIndex="1">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="ChgcodeDe" VisibleIndex="2">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn Caption="Unit" FieldName="ChgUnit" VisibleIndex="3">
                </dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataComboBoxColumn Caption="Gst Type" FieldName="GstTypeId" VisibleIndex="4">
                    <PropertiesComboBox ValueType="System.String">
                        <Items>
                            <dxe:ListEditItem Text="S" Value="S"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="Z" Value="Z"></dxe:ListEditItem>
                            <dxe:ListEditItem Text="E" Value="E"></dxe:ListEditItem>
                        </Items>
                    </PropertiesComboBox>
                </dxwgv:GridViewDataComboBoxColumn>
                <dxwgv:GridViewDataSpinEditColumn Caption="Gst P" FieldName="GstP" VisibleIndex="5">
                    <PropertiesSpinEdit DisplayFormatString="g" >
                    <SpinButtons ShowIncrementButtons="false" />
                    </PropertiesSpinEdit>
                </dxwgv:GridViewDataSpinEditColumn>
            </Columns>
        </dxwgv:ASPxGridView>
    </div>
    </form>
</body>
</html>
