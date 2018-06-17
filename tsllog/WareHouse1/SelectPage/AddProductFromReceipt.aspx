<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProductFromReceipt.aspx.cs" Inherits="WareHouse_SelectPage_AddProductFromReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>product</title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">
          function $(s) {
              return document.getElementById(s) ? document.getElementById(s) : s;
          }
          function keydown(e) {
              if (e.keyCode == 27) {
                  parent.ClosePopupCtr1();
              }
          }
          document.onkeydown = keydown;
    </script>
        <script type="text/javascript">
            function OnCallback(v) {
                if (v != null && v.length > 0)
                    alert(v)
                else
                    parent.AfterPopubMultiInv();

            }
            function SelectAll() {
                jQuery("input[id*='ack_IsPay']").each(function (ind) {
                    var el = jQuery(".Idate INPUT[type='text']")[ind];
                    if ($(this).val() == "C") {
                        this.click();
                        //jQuery(".Idate input").val("01/01/1900");
                    } else {
                        this.click();
                        //jQuery(el).val(dateClear.GetText());
                    }
                });
            }
            function RowClickHandler(s, e) {
                SetLookupKeyValue(e.visibleIndex);
                DropDownEdit.HideDropDown();
            }
            function SetLookupKeyValue(rowIndex) {
                DropDownEdit.SetText(GridView.cpCode[rowIndex]);
                spin_GstP.SetText(GridView.cpGstValue[rowIndex]);
            }
    </script>
        <script type="text/javascript" src="../../Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <wilson:DataSource ID="dsGstType" runat="server" ObjectSpace="C2.Manager.ORManager"
        TypeName="C2.XXGstType" KeyMember="SequenceId"  />
        <div>
            <table>
                <tr>
                    <td><dxe:ASPxLabel ID="lbl_No" runat="server" Text=" Code"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_No" runat="server" Text='' Width="150">
                        </dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Retrieve" Width="110" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                          <dxe:ASPxButton ID="btnSelect" runat="server" Text="Invert Select" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ok" AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                        grid.GetValuesOnCustomCallback('OK',OnCallback);
                        }" />
                        </dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <table style="border:thin solid #C0C0C0; color: #000000; background-color: #CCCCCC; width:980px;text-align:left">
                <tr style="text-align:left;">
                    <td style="width:120px">Product
                    </td>
                    <td style="width:60px">CostPrice
                    </td>
                    <td style="width:60px">BalQty
                    </td>
                    <td style="width:60px">Qty
                    </td>
                    <td style="width:60px">Price
                    </td>
                    <td style="width:60px">Currency
                    </td>
                    <td style="width:70px">ExRate</td>
                    <td style="width:60px">Gst Type
                    </td>
                    <td style="width:60px">Gst</td>
                    <td style="width:50px"></td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server"
                KeyFieldName="Id" Width="980px"
                OnCustomDataCallback="grid_CustomDataCallback">
                <SettingsPager Mode="ShowAllRecords" />
                <Settings ShowColumnHeaders="false" />
                <Templates>
                   <DataRow>
                            <table style="border-bottom: solid 1px black; width:980px;text-align:left">
                                <tr style="font-weight: bold; font-size: 11px">
                                    <td style="width: 120px;">
                                        <dxe:ASPxLabel ID="lab_Code" Width="120px" runat="server" Text=' <%# Eval("Product")%>'></dxe:ASPxLabel>
                                       
                                    </td>
                                     <td style="width: 60px;">
                                         <dxe:ASPxLabel ID="ASPxLabel1" Width="60px" runat="server" Text=' <%# Eval("Price")%>  '></dxe:ASPxLabel>
                                         
                                    </td>
                                    <td style="width: 60px;">
                                         <dxe:ASPxLabel ID="ASPxLabel2" Width="60px" runat="server" Text=' <%# Eval("Qty")%>  '></dxe:ASPxLabel>
                                         
                                    </td>
                                    <td style="width: 60px;">
                                        <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_Qty"
                                            ClientInstanceName="spin_Qty" runat="server">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>

                                    </td>
                                    <td style="width: 60px;">
                                         <dxe:ASPxSpinEdit Increment="0.00" Width="60" ID="spin_Price"
                                            ClientInstanceName="spin_Price" runat="server">
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td style="width: 60px;">
                                        <dxe:ASPxTextBox ID="txt_Currency" runat="server" Width="60" Text='<%# System.Configuration.ConfigurationManager.AppSettings["Currency"] %>' ClientInstanceName="txt_det_Currency" MaxLength="3"></dxe:ASPxTextBox>  
                                    </td>
                                    <td style="width: 70px;"> 
                                        <dxe:ASPxSpinEdit Increment="0" Width="70" ID="spin_ExRate"
                                                                ClientInstanceName="spin_ExRate"
                                                                runat="server" Value='<%# SafeValue.SafeDecimal(1)%>' DisplayFormatString="0.000000" DecimalPlaces="6">
                                                                <SpinButtons ShowIncrementButtons="false" />
                                                            </dxe:ASPxSpinEdit>
                                    </td>
                                    <td style="width: 60px; ">
                                        <dxe:ASPxComboBox ID="cmb_GstType" Width="60" runat="server" DataSourceID="dsGstType" EnableIncrementalFiltering="true" TextField="Code"  SelectedIndex="0" ValueField="Code" ValueType="System.String">
                                        </dxe:ASPxComboBox>                                 
                                    </td>
                                    <td style="width: 60px;">
                                        <dxe:ASPxSpinEdit Increment="0" Width="60" ID="spin_GstP"
                                            ClientInstanceName="spin_GstP" runat="server" 
                                            DisplayFormatString="0.00"   Value='<%#SafeValue.SafeDecimal(0) %>'>
                                            <SpinButtons ShowIncrementButtons="false" />
                                        </dxe:ASPxSpinEdit>
                                    </td>
                                    <td width="50" valign="top">
                                        <dxe:ASPxCheckBox ID="ack_IsPay" runat="server" Width="10">
                                        </dxe:ASPxCheckBox>
                                        <div style="display: none">

                                            <dxe:ASPxTextBox ID="txt_Id" runat="server"
                                                Text='<%# Eval("Id") %>'>
                                            </dxe:ASPxTextBox>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                    </DataRow>
                </Templates>
            </dxwgv:ASPxGridView>
             <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="500"
                AllowResize="True" Width="800" EnableViewState="False">
                   
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
