<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewJobListBill2.aspx.cs" Inherits="PagesContTrucking_Job_NewJobListBill2" %>

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

        //======================
        //==================

        function cont_update_inline(rowIndex) {
            console.log(rowIndex);

            loading.show();
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('UpdateInline_' + rowIndex, cont_update_inline_callback);
            }, config.timeout);
        }
        var btnId=null;
        //btn_create,
        function inv_create_inline(rowIndex) {
            console.log(rowIndex);
            loading.show();
            setTimeout(function () {
                detailGrid.GetValuesOnCustomCallback('CreateInvline_' + rowIndex, inv_create_inline_callback);
            }, config.timeout);
            //btnId=btn_create;
        }
        function cont_update_inline_callback(res) {
            console.log(res);
            loading.hide();
            if (res.indexOf('Save Error') >= 0) {
                console.log('===========', res);
                alert('Save Error');
            } else {
                parent.notice('Save Successful', '', 'success');

                var ar = res.split(',');
                var driver = ",";
                for (var i = 2; i < ar.length; i++) {
                    driver = driver + ar[i] + ',';
                }
                var detail = {
                    controller: ar[0],
                    no: ar[1],
                    driver: driver,
                }
                console.log('=========', detail);
                SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
            }
        }
        function inv_create_inline_callback(res) {
            console.log(res);
            loading.hide();
            if (res.indexOf('Save Error') >= 0) {
                console.log('===========', res);
                alert('Save Error');
            } 
            else {
                parent.notice('Save Successful', '', 'success');
                var ar = res.split(',');
                var driver = ",";
                for (var i = 2; i < ar.length; i++) {
                    driver = driver + ar[i] + ',';
                }
                var detail = {
                    controller: ar[0],
                    no: ar[1],
                    driver: driver,
                }
                //btnId.SetText(res);
                btn_search.OnClick(null,null);
                SV_Firebase.publish_system_msg_send('refreshList', 'SV_EGL_JobTrip_Schedule', JSON.stringify(detail));
            }
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
                                <dxe:ListEditItem Text="IMP" Value="IMP" />
                                <dxe:ListEditItem Text="EXP" Value="EXP" />
                                <dxe:ListEditItem Text="LOC" Value="LOC" />
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
                        <dxe:ASPxLabel ID="ASPxLabel7" runat="server" Text="Client"></dxe:ASPxLabel>
                    </td>
                    <td>
                        <dxe:ASPxButtonEdit ID="btn_ClientId" ClientInstanceName="btn_ClientId" runat="server" Width="100" AutoPostBack="False" ReadOnly="true">
                            <Buttons>
                                <dxe:EditButton Enabled="true" Text=".."></dxe:EditButton>
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
            <dxwgv:ASPxGridView ID="grid_Transport" ClientInstanceName="detailGrid" runat="server"
                KeyFieldName="Id" Width="100%" AutoGenerateColumns="False" OnCustomDataCallback="grid_Transport_CustomDataCallback">
                <SettingsCustomizationWindow Enabled="True" />
                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                <%--<Settings HorizontalScrollBarMode="Visible" />
                <Settings VerticalScrollableHeight="450" />
                <Settings VerticalScrollBarMode="Visible" />--%>
                <Columns>
                    <dx:GridViewDataColumn FieldName="Id" Caption="#" Settings-AllowSort="False" Width="120" FixedStyle="Left">
                        <DataItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <div class='<%# SafeValue.SafeString(Eval("DocNo"))==""?"show":"hide" %>' >
                                            <input type="button" class="button" value="Save" onclick="cont_update_inline(<%# Container.VisibleIndex %>);" />
                                        </div>
                                     </td>
                                </tr>
                                <tr style="height: 50px">
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                       <div class='<%# SafeValue.SafeString(Eval("DocNo"))==""?"show":"hide" %>' style="min-width: 70px;">
                                            <input type="button" class="button" value="Create Inv" onclick="inv_create_inline(<%# Container.VisibleIndex %>);" />
                                        </div>

                                        <%--<dxe:ASPxButton ID="btn_CreateInv" Width="100" AutoPostBack="false" runat="server" Text="Create Inv" OnInit="btn_CreateInv_Init"  ClientInstanceName="btn_CreateInv">
                                                
                                            </dxe:ASPxButton>--%>
                                        <div class='<%# SafeValue.SafeString(Eval("DocNo"))==""?"hide":"show" %>' style="min-width: 70px;">
                                            
                                            <a href='javascript: parent.navTab.openTab("<%# SafeValue.SafeString(Eval("DocNo"))%>","/PagesAccount/EditPage/ArInvoiceEdit.aspx?no=<%# SafeValue.SafeString(Eval("DocNo"))%>",{title:"<%# SafeValue.SafeString(Eval("DocNo"))%>", fresh:false, external:true});'><%# SafeValue.SafeString(Eval("DocNo"))%></a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div style="display:none">
                                <dxe:ASPxTextBox ID="txt_cntId" runat="server" Text='<%# Eval("Id") %>'></dxe:ASPxTextBox>
                                <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Text='<%# Eval("JobNo") %>'></dxe:ASPxLabel>
                                <dxe:ASPxLabel ID="lbl_ClientId" runat="server" Text='<%# Eval("ClientId") %>'></dxe:ASPxLabel>
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
                                            <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?jobNo=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Job Date:</td>
                                    <td><%# SafeValue.SafeDateStr(Eval("JobDate")) %></td>
                                </tr>
                                <tr>
                                    <td>Client:</td>
                                    <td><%# Eval("client") %></td>
                                </tr>
                                <tr>
                                    <td>Vessel/Voyage:</td>
                                    <td><%# Eval("Vessel") %> / <%# Eval("Voyage") %></td>
                                </tr>
                                 
                                <tr>
                                    <td>ETA:</td>
                                    <td><%# SafeValue.SafeDateStr(Eval("EtaDate")) %>&nbsp;TT:&nbsp;<%# SafeValue.SafeString(Eval("EtaTime"),"0000") %></td>
                                </tr>
                              <tr>
                                    <td>Pol/Pod:</td>
                                    <td><%# Eval("Pol") %> / <%# Eval("Pod") %></td>
                                </tr>
                                <tr>
                                    <td>From: / To</td>
                                    <td>
                                        <%# Eval("PickupFrom") %>&nbsp;/&nbsp;<%# Eval("DeliveryTo") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="180">Billing Date
                                    </td>
                                    <td width="160">
                                        <dxe:ASPxDateEdit ID="txt_DocDt" runat="server" Width="120" Value='<%# Eval("EtaDate")%>'
                                            EditFormat="Custom" EditFormatString="dd/MM/yyyy" DisplayFormatString="dd/MM/yyyy">
                                        </dxe:ASPxDateEdit>
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
                                </tr>
                                <tr>
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
                                </tr>
                                <tr>
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
                                </tr>
                                <tr>
                                    <td>Weight:</td>
                                    <td>
                                        <%# Eval("Weight") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Depot:</td>
                                    <td>
                                        <%# Eval("YardAddress") %>
                                    </td>
                                </tr>
                            </table>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Amount" Width="180px" CellStyle-VerticalAlign="Top">
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
                Width="980" EnableViewState="False">
                <ClientSideEvents CloseUp="function(s, e) {
      document.getElementById('btn_search').click();
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
