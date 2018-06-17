<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobBilling.aspx.cs" Inherits="Modules_Client_JobBilling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../../Style/ConTrucking_planner.css" rel="stylesheet" />
    <link href="../script/f_dev.css" rel="stylesheet" />
    <script type="text/javascript" src="/Script/pages.js"></script>
    <script type="text/javascript" src="/Script/ContTrucking/JobEdit.js"></script>
   <script src="../script/firebase.js"></script>
    <script src="../script/js_company.js"></script>
    <script src="../script/js_firebase.js"></script>
    <script src="../script/jquery.js"></script>
    <style type="text/css">
        #grid_Transport_DXMainTable > tbody > tr:hover {
            background-color: #e9e9e9;
        }
        .show {
          display:block;
        }
        .hide {
           display:none
        }
    </style>
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
        function AfterPopub(){
            popubCtr1.Hide();
            popubCtr1.SetContentUrl('about:blank');
            btn_search.OnClick(null,null);
        }
        function create_inv_inline(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('CreateInvline_'+rowIndex, create_inv_inline_callback);
            }, config.timeout);
        }
        function create_inv_inline_callback(res){
            console.log(res);
            var str=new Array();
            str=res.split('_');
            var jobNo=str[0];
            var type=str[1];
            var client=str[2];
            popubCtr1.SetHeaderText('Job Cost ');
            popubCtr1.SetContentUrl('../SelectPage/SelectJobCost.aspx?no=' + jobNo + '&type=' + type + '&client=' + client);
            popubCtr1.Show();

        }
        function open_inv_page(invNo){
            parent.navTab.openTab(invNo,"/PagesAccount/EditPage/ArInvoiceEdit.aspx?no="+invNo,{title:invNo, fresh:false, external:true});
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Text="Cont No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_ContNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cont Status"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_StatusCode" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Value="All" Text="All" />
                                <dxe:ListEditItem Value="New" Text="New" />
                                <%--<dxe:ListEditItem Value="Scheduled" Text="Scheduled" />--%>
                                <dxe:ListEditItem Value="InTransit" Text="InTransit" />
                                <dxe:ListEditItem Value="Completed" Text="Completed" />
                                <%--<dxe:ListEditItem Value="Canceled" Text="Canceled" />--%>
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Text="From"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateFrom" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel3" runat="server" Text="To"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxDateEdit ID="txt_search_dateTo" runat="server" Width="100" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy"></dxe:ASPxDateEdit>
                    </td>
                   

                </tr>
                <tr>
                    <td>
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel5" runat="server" Text="Job Type"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="search_JobType" runat="server" Width="100" DropDownStyle="DropDownList">
                            <Items>
                                <dxe:ListEditItem Text="" Value="" />
                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                <dxe:ListEditItem Text="LOC" Value="LOC" />
                                <dxe:ListEditItem Text="WGR" Value="WGR" />
                                <dxe:ListEditItem Text="WDO" Value="WDO" />
                                <dxe:ListEditItem Text="TPT" Value="TPT" />
                                <dxe:ListEditItem Text="CRA" Value="CRA" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel8" runat="server" Text="Vessel"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_Vessel" Width="100" runat="server"></dxe:ASPxTextBox>
                    </td>
                     <td>
                       Billing Status
                    </td>
                    <td>
                        <dxe:ASPxComboBox ID="cbb_BillingStatusCode" runat="server" Width="100">
                            <Items>
                                <dxe:ListEditItem Value="All" Text="All" />
                                <dxe:ListEditItem Value="Billed" Text="Billed" />
                                <dxe:ListEditItem Value="Unbilled" Text="Unbilled" />
                            </Items>
                        </dxe:ASPxComboBox>
                    </td>
                    <td style="display:none">
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td style="display:none">
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False" ReadOnly="true">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4" style="display:none">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" OnHtmlDataCellPrepared="grid_Transport_HtmlDataCellPrepared"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <%--<Settings HorizontalScrollBarMode="Visible" />
                <Settings VerticalScrollableHeight="450" />
                <Settings VerticalScrollBarMode="Visible" />--%>
                <Columns>
                    <dx:GridViewDataColumn FieldName="Id" Caption="Invoice" Settings-AllowSort="False" Width="120" FixedStyle="Left">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <div style="min-width: 70px; margin-bottom:20px;display:none">
                                            <input type="button" class="button" value="Breakdown" onclick="create_inv_inline(<%# Container.VisibleIndex %>);" />
                                        </div>
                                        <div class='<%# SafeValue.SafeString(Eval("DocNo"))==""?"hide":"show" %>' style="min-width: 70px;">
                                            
                                            <a href='javascript: parent.navTab.openTab("<%# SafeValue.SafeString(Eval("DocNo"))%>","/Modules/Client/InvoiceEdit.aspx?no=<%# SafeValue.SafeString(Eval("DocNo"))%>",{title:"<%# SafeValue.SafeString(Eval("DocNo"))%>", fresh:false, external:true});'><%# SafeValue.SafeString(Eval("DocNo"))%></a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_cntId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Text='<%# Eval("JobNo") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_ClientId" runat="server" Text='<%# Eval("ClientId") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_JobType" runat="server" Text='<%# Eval("JobType") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_ContNo" runat="server" Text='<%# Eval("ContainerNo") %>'></dxe:ASPxLabel>
                                 <dxe:ASPxLabel ID="lbl_ContType" runat="server" Text='<%# Eval("ContainerType") %>'></dxe:ASPxLabel>
                            </div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Info" FieldName="JobNo" Settings-AllowSort="False" Width="200" FixedStyle="Left" CellStyle-VerticalAlign="Top">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>No:</td>
                                    <td>
                                        <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                            <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/Modules/Client/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                                        </div>
                                    </td>
                                    <td>Job Date:</td>
                                    <td><%# SafeValue.SafeDateStr(Eval("JobDate")) %></td>
                                </tr>
                                <tr>
                                    <td>Client:</td>
                                    <td><%# Eval("client") %></td>
                                    <td>Vessel/Voyage:</td>
                                    <td><%# Eval("Vessel") %> / <%# Eval("Voyage") %></td>
                                </tr>
                                 
                                <tr>
                                    <td>ETA:</td>
                                    <td><%# SafeValue.SafeDateStr(Eval("EtaDate")) %>&nbsp;TT:&nbsp;<%# SafeValue.SafeString(Eval("EtaTime"),"0000") %></td>
                                    <td>Pol/Pod:</td>
                                    <td><%# Eval("Pol") %> / <%# Eval("Pod") %></td>
                                </tr>
                                <tr>
                                    <td>From: / To</td>
                                    <td>
                                        <%# Eval("PickupFrom") %>&nbsp;/&nbsp;<%# Eval("DeliveryTo") %>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Container Info" Settings-AllowSort="False" Width="150" FixedStyle="Left" CellStyle-VerticalAlign="Top">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>No:</td>
                                    <td><%# Eval("ContainerNo") %></td>
                                    <td>Type:</td>
                                    <td><%# Eval("ContainerType") %></td>
                                </tr>
                                <tr>
                                    <td>Status:</td>
                                    <td>
                                        <div style="background-color: <%# ShowColor(SafeValue.SafeString(Eval("StatusCode"))) %>" class="div_contStatus">
                                            <%# Eval("StatusCode") %>
                                        </div>
                                    </td>
                                    <td>Schedule Date:</td>
                                    <td><%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %></td>
                                </tr>
                                <tr>
                                    <td>Hours:</td>
                                    <td>
                                        <div class="div_hr" style='<%# SafeValue.SafeInt(Eval("hr"),0)>72?"background-color:red;color:white": (SafeValue.SafeInt(Eval("hr"),0)>48?"background-color:orange;color:white":"")%>'>
                                            <%# Eval("hr") %>
                                        </div>
                                    </td>
                                    <td>Weight:</td>
                                    <td>
                                        <%# Eval("Weight") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Depot:</td>
                                    <td colspan="3">
                                        <%# Eval("YardAddress") %>
                                    </td>

                                </tr>
                                <tr>
                                    <td>DG/J5:</td>
                                    <td>
                                        <%# Eval("F5Ind") %>
                                    </td>
                                    <td>DG Cls:</td>
                                    <td>
                                        <%# Eval("DgClass") %>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Cargo Info"  Settings-AllowSort="False" Width="150" FixedStyle="Left" CellStyle-VerticalAlign="Top">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                 <tr>
                                    <td width="100">Lot No:</td>
                                    <td><%# Eval("BookingNo") %></td>
                                </tr>
                                <tr>
                                    <td>BL/Bkg No:</td>
                                    <td><%# Eval("HblNo") %></td>
                                </tr>
                                <tr>
                                    <td>Cont No:</td>
                                    <td><%# Eval("ContNo") %></td>
                                </tr>
                                                                <tr>
                                    <td>SKU:</td>
                                    <td>
                                        <%# Eval("SkuCode") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Qty:</td>
                                    <td>
                                        <%# Eval("QtyOrig") %>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Amount" Width="180px" CellStyle-VerticalAlign="Top" Visible="false">
                        <DataItemTemplate>
                            <table style="width:100%">
                                 <tr>
                                    <td>Total:</td>
                                    <td><%# Eval("TotalAmt")%></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Billing:</td>
                                    <td><%# Eval("BillingAmt") %></td>
                                </tr>
                                <tr>
                                    <td>PSA RB:</td>
                                    <td><%# Eval("PsaAmt") %></td>
                                </tr>
                                <tr>
                                    <td>Incentive:</td>
                                    <td><%# Eval("Incentive") %></td>
                                </tr>
                                <tr>
                                    <td>Claims:</td>
                                    <td><%# Eval("Claims") %></td>
                                </tr>

                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            <dxpc:ASPxPopupControl ID="popubCtr" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="400"
                Width="600" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
            <dxpc:ASPxPopupControl ID="popubCtr1" runat="server" CloseAction="CloseButton" Modal="True"
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popubCtr1"
                HeaderText="Party" AllowDragging="True" EnableAnimation="False" Height="530"
                Width="1080" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      
}" />
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </div>
    </form>
</body>
</html>
