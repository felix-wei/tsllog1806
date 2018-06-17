<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="Modules_Freight_Job_OrderList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script src="/script/jquery171.js"></script>
    <script type="text/javascript" src="/Script/Acc/Doc.js"></script>
    <script src="../script/jquery.js"></script>
    <script>
        jQuery.noConflict();

        jQuery(document).ready(function ($) {
            $(":input").on("keydown", function (event) {
                if (event.which === 13 && !$(this).is("textarea, :button, :submit")) {
                    event.stopPropagation();
                    event.preventDefault();

                    $(this)
                        .nextAll(":input:not(:disabled, [readonly='readonly'])")
                        .first()
                        .focus();
                }
            });
        });


    </script>

    <script type="text/javascript">
        function PopupExcel() {
            popubCtr.SetHeaderText('Import Order/从Excel导入订单');
            popubCtr.SetContentUrl('/Modules/Freight/Job/ImportOrder.aspx');
            popubCtr.Show();
        }
        function open_page(no) {
            parent.navTab.openTab(no, "/Modules/Freight/Job/OrderEdit.aspx?no=" + no, { title: no, fresh: false, external: true });
        }
        function AfterUploadExcel() {
            popubCtr.Hide();
            popubCtr.SetContentUrl('about:blank');
           
        }
        var loading = {
            show: function () {
                $("#div_tc").css("display", "block");
            },
            hide: function () {
                $("#div_tc").css("display", "none");
            }
        }

        $(function () {
            loading.hide();
        })
        var config = {
            timeout: 0,
            gridview: 'grd_Det',
        }
        function update_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                grd_Det.GetValuesOnCustomCallback('UpdateInline_' + rowIndex, update_inline_callBack);
            }, config.timeout);
        }
        function update_inline_callBack(v) {
            btn_Sch.OnClick();
        }
    </script>
      <script type="text/javascript">
          var isUpload = false;
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <wilson:DataSource ID="dsJobDet" runat="server" ObjectSpace="C2.Manager.ORManager"
            TypeName="C2.JobHouse" KeyMember="Id"  />
        <div>
            <table style="width:1300px">
                <tr>
                    
                    <td>
                        <dxe:ASPxLabel ID="lbl_Type" runat="server" Text="状态"></dxe:ASPxLabel>
                        </td>
                    <td>
                        <dxe:ASPxComboBox EnableIncrementalFiltering="True" runat="server" Width="80" ID="cmb_Type"
                              AutoPostBack="false">
                            <Items>
                                <dxe:ListEditItem Text="所有单" Value="All" Selected="true" />
                                <dxe:ListEditItem Text="待确认" Value="USE" />
                                <dxe:ListEditItem Text="已下单" Value="ORDER" />
                                <dxe:ListEditItem Text="已入库" Value="IN" />
                                <dxe:ListEditItem Text="已排库" Value="PICKED" />
                                <dxe:ListEditItem Text="已出库" Value="OUT" />
                                <dxe:ListEditItem Text="已取消" Value="CANCEL" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        唛头号/Bkg RefNo
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_BkgRefNo" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        联系方式/Tel
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Tel" runat="server" Width="120"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_Sch" ClientInstanceName="btn_Sch" Width="100" runat="server" Text="搜索" OnClick="btn_Sch_Click">
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton3" Width="100" runat="server" Text="添加" 
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                   open_page('0');
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="ASPxButton5" Width="100" runat="server" Text="从Excel导入订单"
                            AutoPostBack="false" UseSubmitBehavior="false">
                            <ClientSideEvents Click="function(s,e) {
                                            isUpload=true;
                                    PopupExcel();
                                    }" />
                        </dxe:ASPxButton>
                    </td>
                    <td width="110">
                        <dxe:ASPxButton ID="btn_download" Width="100" runat="server" Text="Download(下载模板)" OnClick="btn_download_Click"
                            AutoPostBack="false" UseSubmitBehavior="false">
                        </dxe:ASPxButton>
                    </td>

                    <td>
                        <dxe:ASPxLabel ID="lbl_mes" runat="server" ForeColor="Red" Text="←Download Templete/下载货物列表模板"></dxe:ASPxLabel>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grd_Det" ClientInstanceName="grd_Det" runat="server" DataSourceID="dsJobDet"
                KeyFieldName="Id" Width="100%"  OnCustomDataCallback="grd_Det_CustomDataCallback"  OnRowDeleting="grd_Det_RowDeleting">
                <SettingsBehavior ConfirmDelete="True"  />
                <SettingsPager Mode="ShowPager" PageSize="20">
                </SettingsPager>
                <SettingsEditing Mode="EditForm" />
                <Settings />
                <Columns>
                    <dxwgv:GridViewDataColumn Caption="#" Width="110" VisibleIndex="0">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <a onclick='open_page("<%# Eval("Id") %>");'>编辑</a>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="修改状态" Width="60" VisibleIndex="1">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>

                                        <dxe:ASPxButton ID="btn_Ops" runat="server" ClientInstanceName="btn_Ops"
                                            Text="操作" Width="60" AutoPostBack="false" OnInit="btn_Ops_Init"
                                            ClientSideEvents-Click='<%# "function(s) { if(confirm(\"确认对订单做\"+btn_Ops"+Container.VisibleIndex+".GetText()+\"操作吗？\")){update_inline("+Container.VisibleIndex+")} }"  %>'>
                                        </dxe:ASPxButton>

                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="CargoStatus" Caption="状态" Width="80" VisibleIndex="1">
                        <DataItemTemplate>
                            <%# ShowStatus(Eval("CargoStatus")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Id" Caption="编号" VisibleIndex="1" Width="20"
                        SortIndex="0" SortOrder="Descending">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="BookingNo" Caption="唛头号" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientId" Caption="收货人" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark1" Caption="地址" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientEmail" Caption="电话" VisibleIndex="1" Width="100">
                    </dxwgv:GridViewDataColumn>
                     <dxwgv:GridViewDataColumn FieldName="ConsigneeInfo" Caption="发货人" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="货主信息(UEN/IC)" VisibleIndex="2" Width="200">
                        <DataItemTemplate>
                            <%# Eval("ConsigneeRemark") %> / <%# Eval("ConsigneeEmail") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Email1" Caption="邮箱地址" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Tel1" Caption="座机" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Mobile1" Caption="手机" VisibleIndex="2" Width="100">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Qty" Caption="数量" VisibleIndex="5" Width="50">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Weight" Caption="重量" VisibleIndex="5"
                        Width="80">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Volume" Caption="体积" VisibleIndex="5" Width="80">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="删除" Width="60" VisibleIndex="90">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dxe:ASPxButton ID="btn_mkg_del" runat="server"  Visible='<%# (SafeValue.SafeString(Eval("Role"),"Dongji")=="Dongji"||SafeValue.SafeString(Eval("Role"),"Admin")=="Admin") %>'
                                            Text="删除" Width="40" AutoPostBack="false" ClientSideEvents-Click='<%# "function(s) { if(confirm(\"确认删除吗？\")){grd_Det.DeleteRow("+Container.VisibleIndex+") }}"  %>'>
                                        </dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
                <Settings ShowFooter="True" />
            </dxwgv:ASPxGridView>
             <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Customer" AllowDragging="True" EnableAnimation="False" Height="400"
                AllowResize="True" Width="900" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      if(isUpload){
	    btn_Sch.OnClick();
      }
}" />
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
