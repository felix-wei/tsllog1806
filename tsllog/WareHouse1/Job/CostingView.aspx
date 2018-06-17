<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeFile="CostingView.aspx.cs"
    Inherits="CostingView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function gcancel(g, i) {
            window.location = jQuery('#ctl_page').val() + ".aspx";
        }
        function SelectAll() {
            if (btnSelect.GetText() == "Select All")
                btnSelect.SetText("UnSelect All");
            else
                btnSelect.SetText("Select All");
            jQuery("input[id*='ack_IsPay']").each(function () {
                this.click();
            });
        }

    </script>
    <script type="text/javascript">
        function PickRate(url) {
            window.frames['schFrame'].location = url;
        }
        function CostEdit(url) {
            window.frames['viewFrame'].location = url;
        }
    </script>


</head>
<frameset runat="server" id="rptFrameset" cols="*" rows="*,0" border="0" framespacing="2" bordercolor="#9B98B6">
    <noframes>
<body>

<p>此网页使用了框架，但您的浏览器不支持框架。</p>

</body>
</noframes>
	<frame name="viewFrame" src='CostingEdit.aspx?id=<%=id %>' />
	<frame name="schFrame" runat="server" id="schFrame" src="/SelectPage/PickRate.aspx" />
</frameset>
</html>
