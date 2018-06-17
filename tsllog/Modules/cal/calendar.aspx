<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calendar.aspx.cs" Inherits="_Calendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style media="print">
	.noprint {display:none;}
	.noprintb {border:0px;}
	.doprint {font-size:10pt;}
	.onlyprint {font-size:10pt;}
    </style>	
	
     <link href="css/cupertino/jquery-ui-1.7.3.custom.css" rel="stylesheet" type="text/css" />
    <link href="fullcalendar/fullcalendar.css" rel="stylesheet" type="text/css" />

    <script src="jquery/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="jquery/jquery-ui-1.7.3.custom.min.js" type="text/javascript"></script>

    <script src="jquery/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>

    <script src="fullcalendar/fullcalendar.min.js" type="text/javascript"></script>

    <script src="scripts/calendarscript2.js" type="text/javascript"></script>

    
    <style type='text/css'>
        body
        {
            margin-top: 40px;
            text-align: center;
            font-size: 14px;
            font-family: "Lucida Grande" ,Helvetica,Arial,Verdana,sans-serif;
        }
        #calendar
        {
            width: 960px;
            margin: 0 auto;
        }
		
		.OT a {background-color:#cccccc;}
		.LM a {background-color:lightblue;color:black;}
		.OM a {background-color:lightgreen;color:black;}
		.OB a {background-color:orange;color:black;}
		.IB a {background-color:yellow;color:black;}
		.AR a {background-color:#CCCCCC;}
		.SR a {background-color:#123456;}
		
        /* css for timepicker */
        .ui-timepicker-div dl
        {
            text-align: left;
        }
        .ui-timepicker-div dl dt
        {
            height: 25px;
        }
        .ui-timepicker-div dl dd
        {
            margin: -25px 0 10px 65px;
        }
        .style1
        {
            width: 100%;
        }
        
        /* table fields alignment*/
        .alignRight
        {
        	text-align:right;
        	padding-right:10px;
        	padding-bottom:10px;
        }
        .alignLeft
        {
        	text-align:left;
        	padding-bottom:10px;
        }
    </style>
	<script>
	function ShowJob(masterId) {
            parent.navTab.openTab(masterId, "/Warehouse/Job/JobEdit.aspx?refN=" + masterId, { title: masterId, fresh: false, external: true });
        }

	</script>
</head>
<body>
    <div class=noprint style="background:#eeefff;text-align:center;height:16px;position:fixed;top:0px;width:100%;">
<a href="javascript:window.print();">Print Calendar</a>
</div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="calendar">
    </div>
 
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" />
    </form>
</body>
</html>
