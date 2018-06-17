<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnbilledStorage.aspx.cs" Inherits="Modules_Tpt_Report_UnbilledStorage" %>

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
   <script src="/script/firebase.js"></script>
    <script src="/script/js_company.js"></script>
    <script src="/script/js_firebase.js"></script>
    <script src="/script/jquery.js"></script>
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
            var str=new Array();
            str=res.split(',');
            var jobNo=str[0];
            var type=str[1];
            var client=str[2];
            var quoteNo=str[3];
            parent.navTab.openTab(jobNo+"_"+"Cost", "/Modules/Tpt/SelectPage/TallySheet.aspx?no=" + jobNo + '&type=' + type + '&client=' + client, { title: jobNo+"_"+"Cost", fresh: false, external: true });
            //popubCtr1.SetHeaderText('Unbilled Storage Cost ');
            //popubCtr1.SetContentUrl('/Modules/Tpt/SelectPage/TallySheet.aspx?no=' + jobNo);
            //popubCtr1.Show();
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
                        <dxe:ASPxLabel ID="lbl1" runat="server" Text="Job No"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxTextBox ID="txt_search_jobNo" runat="server" Width="100"></dxe:ASPxTextBox>
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
                    <td>
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".." ></dxe:EditButton>
                            </Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
                                                                        PopupParty(btn_ClientId,txt_ClientName);
                                                                        }" />
                        </dxe:ASPxButtonEdit>
                    </td>
                    <td colspan="4">
                        <dxe:ASPxTextBox ID="txt_ClientName" ClientInstanceName="txt_ClientName" runat="server" Width="100%" ReadOnly="true" BackColor="Control"></dxe:ASPxTextBox>
                    </td>
                    <td>
                        <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" runat="server" Text="Retrieve" OnClick="btn_search_Click"></dxe:ASPxButton>
                    </td>
                </tr>
            </table>
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server" OnHtmlDataCellPrepared="grid_Transport_HtmlDataCellPrepared" OnPageSizeChanged="grid_Transport_PageSizeChanged"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback" OnPageIndexChanged="grid_Transport_PageIndexChanged">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <Columns>
                    <dx:GridViewDataColumn FieldName="Id" Caption="#" Settings-AllowSort="False" Width="140" FixedStyle="Left">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                <tr style="display:none">
                                    <td>
                                        <input type="button" class="button" value="Breakdown" onclick="breakdown_inline(<%# Container.VisibleIndex %>);" />
                                     </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="min-width: 70px; margin-bottom:20px">
                                            <input type="button" class="button" value="Cost Breakdown" onclick="create_inv_inline(<%# Container.VisibleIndex %>);" />
                                        </div>
                                        <div class="hide" style="min-width: 120px;">
                                            
                                            <a href='javascript: parent.navTab.openTab("<%# SafeValue.SafeString(Eval("DocNo"))%>","/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=<%# SafeValue.SafeString(Eval("DocNo"))%>",{title:"<%# SafeValue.SafeString(Eval("DocNo"))%>", fresh:false, external:true});'><%# SafeValue.SafeString(Eval("DocNo"))%></a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_cntId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Text='<%# Eval("JobNo") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_QuoteNo" runat="server" Text='<%# Eval("QuoteNo") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_ClientId" runat="server" Text='<%# Eval("ClientId") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_JobType" runat="server" Text='<%# Eval("JobType") %>'></dxe:ASPxLabel>

                            </div>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                     <dxwgv:GridViewDataTextColumn Caption="Job No" FieldName="JobNo" Width="160" FixedStyle="Left">
                         <DataItemTemplate>
                             <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                             <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                         </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType" Width="60"  FixedStyle="Left"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsTrucking" Caption="CT" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsTrucking","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsWarehouse" Caption="WH" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsWarehouse","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsLocal" Caption="TP" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsLocal","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="IsAdhoc" Caption="CR" Width="30">
                        <DataItemTemplate>
                            <%# Eval("IsAdhoc","{0}")=="Yes"?"X":""%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="contsCnt" Caption="Conts" Width="45" ></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="tripsCnt" Caption="Trips" Width="40" ></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="jobsCnt" Caption="Sub Jobs" Width="60" ></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="subContsCnt" Caption="SubJob Conts"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="subTripsCnt" Caption="SubJob Trips"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Status" Visible="false">
                        <DataItemTemplate>
                            <%# SafeValue.SafeString(Eval("JobStatus")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 200px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="billed" Caption="Billing"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    
                    <dxwgv:GridViewDataTextColumn FieldName="Depot" Caption="Depot"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PermitNo" Caption="PermitNo"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="Terminalcode" Caption="Terminal"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="ClientRefNo" Caption="Client Ref No"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Job Date">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("JobDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn FieldName="Remark" Caption="Remark" Visible="false"></dxwgv:GridViewDataColumn>
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
                Width="1100" EnableViewState="False">
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
