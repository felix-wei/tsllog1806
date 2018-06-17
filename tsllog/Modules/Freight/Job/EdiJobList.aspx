<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiJobList.aspx.cs" Inherits="Modules_Freight_Job_EdiJobList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

	    <script type="text/javascript">

	        function goJob(jobno) {
	            //window.location = "JobEdit.aspx?jobNo=" + jobno;
	            parent.navTab.openTab(jobno, "/PagesContTrucking/Job/JobEdit.aspx?jobNo=" + jobno, { title: jobno, fresh: false, external: true });
	        }

	        function EdiBatch() {
	            if (confirm("Confirm EDI these batch of documents?")) {
	                var refnos = "";
	                jQuery("input.batch").each(function () {
	                    if (this.checked)
	                        refnos += this.id + ',';
	                });
	                var pos = "BP" + cbo_DocType.GetText() + refnos;

	                grd.GetValuesOnCustomCallback(pos, OnCallbackBatch);
	            }
	        }
	        function SelectAll() {
	            if (btnSelect.GetText() == "Select All")
	                btnSelect.SetText("UnSelect All");
	            else
	                btnSelect.SetText("Select All");
	            jQuery("input.batch").each(function () {
	                this.click();
	            });
	        }

	        function OnCallbackBatch(v) {
	            alert(v);
	            btn_search.OnClick(null, null);
	        }
    </script>
    <script type="text/javascript" src="/Script/jquery.js" />

    <script type="text/javascript">
        $.noConflict();
    </script>
</head>
<body>
    <form id="form1" runat="server">

    <table>
        <tr>
            <td>
                Job Type
            </td>
            <td>
                    <dxe:ASPxComboBox runat="server" ID="cbo_DocType" clientinstancename="cbo_DocType" Width="70">
                        <Items>
                             <dxe:ListEditItem Value="" Text="" />
                            <dxe:ListEditItem Value="IMP" Text="IMP" />
                            <dxe:ListEditItem Value="EXP" Text="EXP" />
                            <dxe:ListEditItem Value="LOC" Text="LOC" />
                        </Items>
                    </dxe:ASPxComboBox>
            </td>
            <td>
                Client Ref No
            </td>
            <td>
                <dxe:ASPxTextBox ID="txt_RefNo" Width="100" runat="server"
                    Text="">
                </dxe:ASPxTextBox>
            </td>
            <td>
                Container No
            </td>
            <td>
                <dxe:ASPxTextBox ID="txt_ContNo" Width="100" runat="server"
                    Text="">
                </dxe:ASPxTextBox>
            </td>
            <td>
                ETA/ETD
            </td>
            <td>
                <dxe:ASPxDateEdit ID="txt_from" Width="100" ClientInstanceName="txt_form" runat="server"
                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
            <td>
                -
            </td>
            <td>
                <dxe:ASPxDateEdit ID="txt_end" Width="100" ClientInstanceName="txt_end" runat="server"
                    EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                </dxe:ASPxDateEdit>
            </td>
            <td>
                <dxe:ASPxButton ID="btn_search" ClientInstanceName="btn_search" Width="90" runat="server" Text="Retrieve" OnClick="btn_search_Click">
                </dxe:ASPxButton>
            </td>
			
			                <td>
                    <dxe:ASPxButton ID="ASPxButton3" ClientInstanceName="btnSelect" runat="server" Text="Select All" Width="110" AutoPostBack="False"
                        UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
                                   SelectAll();
                                    }" />
                    </dxe:ASPxButton>
                </td>

			
            <td>
                <dxe:ASPxButton ID="btn_batch" Width="110" runat="server" Text="Batch EDI" 
								AutoPostBack="False"  UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
								EdiBatch();
                                    }" />
                    </dxe:ASPxButton>

            </td>
        </tr>
    </table>
    <dxwgv:ASPxGridView ID="grd" ClientInstanceName="grd" runat="server" Width="100%"
        KeyFieldName="Id" OnCustomDataCallback="grd_CustomDataCallback" OnHtmlRowPrepared="grd_HtmlRowPrepared">
        <SettingsPager Mode="ShowAllRecords" />
        <SettingsDetail ShowDetailRow="false" />
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="Client Ref No" FieldName="JobNo" Width="140px">
                <DataItemTemplate>
                    <dxe:ASPxLabel ID="lbl_JobNo" runat="server" Text='<%# Eval("JobNo") %>' Width="140px"></dxe:ASPxLabel>
                    <%--<a onclick="goJob('<%# Eval("JobNo") %>')"><%# Eval("JobNo") %></a>--%>
                    <%-- <div class='<%# SafeValue.SafeString(string.Format("{0}",Eval("StatusCode")))=="New"?"link":"none" %>' style="min-width: 70px;">
                                <a href='javascript: parent.navTab.openTab("<%# Eval("JobNo") %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# Eval("JobNo") %>",{title:"<%# Eval("JobNo") %>", fresh:false, external:true});'><%# Eval("JobNo") %></a>
                            </div>--%>
                </DataItemTemplate>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataColumn Caption="Job No" Width="140px">
                <DataItemTemplate>
                    <div style="width: 140px">
                        <a style="width: 140px" href='javascript: parent.navTab.openTab("<%# ShowRefNo(Eval("JobNo"))  %>","/PagesContTrucking/Job/JobEdit.aspx?no=<%# ShowRefNo(Eval("JobNo"))  %>",{title:"<%# ShowRefNo(Eval("JobNo"))  %>", fresh:false, external:true});'><%# ShowRefNo(Eval("JobNo"))  %></a>
                    </div>
                </DataItemTemplate>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="JOB_FILE_NO" Caption="Select" Width="40" VisibleIndex="1">
                <DataItemTemplate>
                    
                    <div style='display:<%# ShowRefNo(Eval("JobNo")).Length==0?"block":"none" %> ;mid-width: 40px; mid-height: 20px; <%# (Eval("JobNo","{0}").Trim().Length>3? "": "background:orange")%>'>
                        <input type="checkbox" class="batch" id='<%# Eval("Id")%>_<%# Eval("JobNo")%>_<%# Eval("ContId") %>' />
                    </div>
                </DataItemTemplate>
            </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="JobType" Caption="JobType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn FieldName="client" Caption="Client"></dxwgv:GridViewDataColumn>

                    <dxwgv:GridViewDataColumn FieldName="JobStatus" Caption="Job Status" Visible="false">
                        <DataItemTemplate>
                            <%# VilaStatus(SafeValue.SafeString(Eval("JobStatus"),""))%>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="ScheduleDate" Caption="Schedule Date" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("ScheduleDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Container No" FieldName="ContainerNo">
                        <DataItemTemplate>
                            <%# Eval("ContainerNo") %><br />
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="DischargeCell" FieldName="DischargeCell" Width="40"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="OP" FieldName="OperatorCode" Width="40"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Email" FieldName="EmailInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("EmailInd"))=="Y"?"background-color:green;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("EmailInd"))=="Y"?"@":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Urgent" FieldName="UrgentInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"background-color:red;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("UrgentInd"))=="Y"?"Y":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="SealNo" FieldName="SealNo" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="J5" FieldName="F5Ind" Width="80"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="OOG" FieldName="oogInd">
                        <DataItemTemplate>
                            <div class="div_hr" style='<%# SafeValue.SafeString(Eval("oogInd"))=="Y"?"background-color:red;color:white": "" %>'>
                                <%# SafeValue.SafeString(Eval("oogInd"))=="Y"?"Y":"" %>
                            </div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataColumn>
                    <%--<dxwgv:GridViewDataColumn Caption="MT" FieldName="WarehouseStatus" Width="80"></dxwgv:GridViewDataColumn>--%>
                    <dxwgv:GridViewDataColumn Caption="Cont Type" FieldName="ContainerType"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="StatusCode" Caption="Cont Status">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="hr" Caption="Hours">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="CfsStatus" Caption="WH Status"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PortnetStatus" Caption="Portnet"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Vessel" Caption="Vessel"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Voyage" Caption="Voyage"></dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="REFNo" FieldName="CarrierBkgNo"></dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataTextColumn Caption="ETA" Width="80">
                        <DataItemTemplate>
                            <%# SafeValue.SafeDateStr(Eval("EtaDate")) %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="TT" Width="60">
                        <DataItemTemplate>
                            <%# SafeValue.SafeString(Eval("EtaTime"),"0000") %>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PickupFrom" Caption="From">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("PickupFrom") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="DeliveryTo" Caption="To">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("DeliveryTo") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="Depot" Caption="Depot">
                        <DataItemTemplate>
                            <div style="min-width: 100px"><%# Eval("Depot") %></div>
                        </DataItemTemplate>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn FieldName="PermitNo" Caption="PermitNo"></dxwgv:GridViewDataTextColumn>
                    <%--<dxwgv:GridViewDataColumn FieldName="Haulier" Caption="Contractor"></dxwgv:GridViewDataColumn>--%>
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

 
    </form>
</body>
</html>
