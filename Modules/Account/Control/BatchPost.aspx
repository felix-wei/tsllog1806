<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="BatchPost.aspx.cs"
    Inherits="BatchPost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Import</title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />

	    <script type="text/javascript">
        function PostBatch() {
            if (confirm("Confirm post this batch of documents?")) {
                var refnos = "";
			            jQuery("input.batch").each(function () {
						if(this.checked)
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
                Doc Type
            </td>
            <td>
                    <dxe:ASPxComboBox runat="server" ID="cbo_DocType" clientinstancename="cbo_DocType" Width="70">
                        <Items>
                            <dxe:ListEditItem Value="IV" Text="IV" />
                            <dxe:ListEditItem Value="DN" Text="DN" />
                            <dxe:ListEditItem Value="CN" Text="CN" />
                            <dxe:ListEditItem Value="RE" Text="RE" />
                            <dxe:ListEditItem Value="PL" Text="PL" />
                            <dxe:ListEditItem Value="SC" Text="SC" />
                            <dxe:ListEditItem Value="SD" Text="SD" />
                            <dxe:ListEditItem Value="VO" Text="VO" />
                            <dxe:ListEditItem Value="PS" Text="PS" />
                        </Items>
                    </dxe:ASPxComboBox>
            </td>
            <td>
                Doc No
            </td>
            <td>
                <dxe:ASPxTextBox ID="txt_Doc1" Width="100" runat="server"
                    Text="">
                </dxe:ASPxTextBox>
            </td>
            <td>
                -
            </td>
            <td>
                <dxe:ASPxTextBox ID="txt_Doc2" Width="100" runat="server"
                    Text="">
                </dxe:ASPxTextBox>
            </td>
            <td>
                Doc Date
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
                <dxe:ASPxButton ID="btn_search" Width="90" runat="server" Text="Retrieve" OnClick="btn_search_Click">
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
                <dxe:ASPxButton ID="btn_batch" Width="110" runat="server" Text="Batch Post" 
								AutoPostBack="False"  UseSubmitBehavior="False">
                        <ClientSideEvents Click="function(s, e) {
								PostBatch();
                                    }" />
                    </dxe:ASPxButton>

            </td>
        </tr>
    </table>
    <dxwgv:ASPxGridView ID="grd" ClientInstanceName="grd" runat="server" Width="960"
        KeyFieldName="TrxNo" OnCustomDataCallback="grd_CustomDataCallback">
        <SettingsPager Mode="ShowAllRecords" />
        <SettingsDetail ShowDetailRow="false" />
        <Columns>
            <dxwgv:GridViewDataColumn FieldName="DocNo" VisibleIndex="1">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="DocType" VisibleIndex="2">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="DocDate" VisibleIndex="3">
		<DataItemTemplate><%# Eval("DocDate","{0:dd/MM/yyyy}") %></DataItemTemplate>
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="OtherDoc" Caption="BillNo" VisibleIndex="4">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="AcCode" VisibleIndex="4">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="Currency" VisibleIndex="4">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="ExRate" VisibleIndex="4">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="DocAmt" VisibleIndex="4">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="LocAmt" VisibleIndex="5">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="RefNo" VisibleIndex="6">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="JobNo" VisibleIndex="7">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="JobType" VisibleIndex="7">
            </dxwgv:GridViewDataColumn>
            <dxwgv:GridViewDataColumn FieldName="DocNo" Caption="Select" VisibleIndex="7">
		<DataItemTemplate>
			<input type="checkbox" class="batch" id='<%# Eval("SequenceId")%>' />
		</DataItemTemplate>
            </dxwgv:GridViewDataColumn>
        </Columns>
    </dxwgv:ASPxGridView>
 
    </form>
</body>
</html>
