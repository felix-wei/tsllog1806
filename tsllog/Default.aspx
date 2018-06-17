<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Home" %>

<html>
<head>
    <title>Cargo:ERP</title>
    <%--<link href="UI/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="UI/themes/default/style.css" rel="stylesheet" type="text/css" />
    <link href="UI/themes/css/core.css" rel="stylesheet" type="text/css" />
    <link href="UI/themes/css/icon.css" rel="stylesheet" type="text/css" />
    <link href="UI/themes/css/ieHack.css" rel="stylesheet" type="text/css" />
    <script src="UI/js/speedup.js" type="text/javascript"></script>
    <script src="UI/js/jquery-1.4.4.js" type="text/javascript"></script>
    <script src="UI/js/jquery.cookie.js" type="text/javascript"></script>
    <script src="UI/js/jquery.validate.js" type="text/javascript"></script>
    <script src="UI/js/jquery.bgiframe.js" type="text/javascript"></script>
    <script src="UI/js/dwz.core.js" type="text/javascript"></script>
    <script src="UI/js/dwz.util.date.js" type="text/javascript"></script>
    <script src="UI/js/dwz.validate.method.js" type="text/javascript"></script>
    <script src="UI/js/dwz.barDrag.js" type="text/javascript"></script>
    <script src="UI/js/dwz.drag.js" type="text/javascript"></script>
    <script src="UI/js/dwz.tree.js" type="text/javascript"></script>
    <script src="UI/js/dwz.accordion.js" type="text/javascript"></script>
    <script src="UI/js/dwz.ui.js" type="text/javascript"></script>
    <script src="UI/js/dwz.theme.js" type="text/javascript"></script>
    <script src="UI/js/dwz.switchEnv.js" type="text/javascript"></script>
    <script src="UI/js/dwz.contextmenu.js" type="text/javascript"></script>
    <script src="UI/js/dwz.navTab.js" type="text/javascript"></script>
    <script src="UI/js/dwz.tab.js" type="text/javascript"></script>
    <script src="UI/js/dwz.resize.js" type="text/javascript"></script>
    <script src="UI/js/dwz.dialog.js" type="text/javascript"></script>
    <script src="UI/js/dwz.dialogDrag.js" type="text/javascript"></script>
    <script src="UI/js/dwz.cssTable.js" type="text/javascript"></script>
    <script src="UI/js/dwz.stable.js" type="text/javascript"></script>
    <script src="UI/js/dwz.taskBar.js" type="text/javascript"></script>
    <script src="UI/js/dwz.ajax.js" type="text/javascript"></script>
    <script src="UI/js/dwz.pagination.js" type="text/javascript"></script>
    <script src="UI/js/dwz.database.js" type="text/javascript"></script>
    <script src="UI/js/dwz.datepicker.js" type="text/javascript"></script>
    <script src="UI/js/dwz.effects.js" type="text/javascript"></script>
    <script src="UI/js/dwz.panel.js" type="text/javascript"></script>
    <script src="UI/js/dwz.checkbox.js" type="text/javascript"></script>
    <script src="UI/js/dwz.history.js" type="text/javascript"></script>
    <script src="PagesContTrucking/script/firebase.js"></script>
    <script src="PagesContTrucking/script/js_company.js"></script>
    <script src="PagesContTrucking/script/js_firebase.js"></script>
    <script src="Script/common_notification.js"></script>
    <link href="Style/common_notification.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            DWZ.init("UI/dwz.frag.xml", {
                loginUrl: "login_dialog.html", loginTitle: "Sign On", // 弹出登录对话框
                //		loginUrl:"login.html",	// 跳到登录页面
                statusCode: { ok: 200, error: 300, timeout: 301 }, //【可选】
                pageInfo: { pageNum: "pageNum", numPerPage: "numPerPage", orderField: "orderField", orderDirection: "orderDirection" }, //【可选】
                debug: false, // 调试模式 【true|false】
                callback: function () {
                    initEnv();
                    $("#themeList").theme({ themeBase: "themes" }); // themeBase 相对于index页面的主题base路径
                }
            });
            $("div.accordionContent").height = 250;

            var rootCallback = {
                callback: function (msg) {
                    //console.log('root received msg:', msg);
                    if (msg.type == 'command' && msg.content && msg.content.target == "SV_EGL_JobTrip_Schedule" && msg.content.command == 'completedTrip') {
                        //console.log('root received msg1:', msg);
                        if (notice) {
                            //console.log('root received msg2:', msg);
                            notice('Deliver trip by ' + msg.content.detail.from, '', 'success', null);
                        }
                    }
                }
            };
            SV_Firebase.add_aSystem_callback(rootCallback);
        });

        $(function () {
            startKpi();
        });




        function startKpi() {

            $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/Permit_GetListSum", function (data_Permit) {
                $(".kpi_Permit").html(data_Permit);
                $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/MustReturn_GetListSum", function (data_MustReturn) {
                    $(".kpi_MustReturn").html(data_MustReturn);
                    $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/MustSend_GetListSum", function (data_MustSend) {
                        $(".kpi_MustSend").html(data_MustSend);
                        $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/CDEM_GetListSum", function (data_CDEM) {
                            $(".kpi_CDEM").html(data_CDEM);

                            $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/Vehcile_GetListSum", function (data_Vehcile) {
                                $(".kpi_VEHICLE").html(data_Vehcile);
                                $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/Pass_GetListSum", function (data_Pass) {
                                    $(".kpi_PASSES").html(data_Pass);


                                    
                                    $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/Charges_GetListSum", function (data_Charges) {
                                        $(".kpi_Charges").html(data_Charges);
                                        $.get("/PagesContTrucking/Daily/WebService_AlertAPI.asmx/Export_GetListSum", function (data_Export) {
                                            $(".kpi_Export").html(data_Export);
                                            setTimeout(function () { startKpi() }, 300000);

                                        });
                                    });

                                });
                            });

                        });
                    });
                });
            });
            //$.get( "/api/kpi/kpi-trip.ashx?trip_type=IMP", function( data ) {
            //  $( ".kpi_imp" ).html( data );

            //	$.get( "/api/kpi/kpi-trip.ashx?trip_type=RET", function( data1 ) {
            //  		$( ".kpi_ret" ).html( data1 );

            //		$.get( "/api/kpi/kpi-trip.ashx?trip_type=COL", function( data2 ) {
            //  			$( ".kpi_col" ).html( data2 );

            //			$.get( "/api/kpi/kpi-trip.ashx?trip_type=EXP", function( data3 ) {
            //				$( ".kpi_exp" ).html( data3 );

            //				$.get( "/api/kpi/kpi-trip.ashx?trip_type=SHF", function( data4 ) {
            //					$( ".kpi_shf" ).html( data4 );


            //					$.get( "/api/kpi/kpi-trip.ashx?trip_type=LOC", function( data5 ) {
            //					    $(".kpi_loc").html(data5);

            //					    setTimeout(function () { startKpi() }, 300000);



            //					});

            //				});

            //			});
            //		});
            //	});
            //});

        }
    </script>
    <style type="text/css">
        html, body, div, span, applet, object, iframe, h1, h2, h3, h4, h5, h6, p, blockquote, pre, a, abbr, acronym, address, big, cite, code, del, dfn, em, font, img, ins, kbd, q, s, samp, small, strike, strong, sub, sup, tt, var, dl, dt, dd, ol, ul, li, fieldset, form, label, legend, table, caption, tbody, tfoot, thead, tr, th, td {
            font-family: Verdana, 微软雅黑, 宋体,Consolas;
        }

        .tabsPage .tabsPageHeader li span {
            overflow: hidden;
            white-space: nowrap;
            /*text-overflow:ellipsis;*/
            width: 110px;
            padding-right: 16px;
        }

        .list {
            text-decoration: none;
            color: white;
            border-bottom: 1px solid #F0F0F0;
        }

        table.alert {
            width: 600px;
            margin-top: 3px;
        }

        
            table.alert td{
                border-bottom:1px solid black;
            }
            table.alert td.alertinfo {
                text-align: center;
                padding: 2px;
                color: black;
                background-color: lightblue;
                font-size: 12px;
                font-weight: 400;
                width: 80px;
                white-space:nowrap;
            }

            table.alert td.alertcount {
                text-align: center;
                padding: 2px;
                background: #eeeeff;
                font-size: 13px;
                font-weight: 500;
                color: red;
                width: 40px;
            }

        td.alertcount a {
            color: red;
            font-size: 16px;
            text-decoration: none;
        }

        ;
    </style>
</head>
<body>
    <form id="main" method="post" runat="server">
        <div id="layout">
            <div id="header">
                <div class="headerNav">
                    <table>
                        <tr>
                            <td align="centr">
                                <img src="/custom/tsl-png.png" width="40">
                            </td>
                            <td style="padding-left: 20px; font-size: 22px; color: white; font-weight: bold;">
                                <%= System.Configuration.ConfigurationManager.AppSettings["CompanyName"] %>
                            </td>
                        </tr>
                    </table>
                    <div style='float: right; padding-right: 0px; height: 40px; display: <%= S.Role()=="Client" ? "none" : "" %>'>
                        <table cellpadding="2" border="0" class="alert">
                            <tr>
                                <td class="alertinfo" style="color: red; font-weight: bold;"><u><a class="kpi_driver" href='javascript:open_printer("DRIVER","/PagesContTrucking/Job/DispatchPlanner_Trailer.aspx");'>TRAILERS</a></u></td>
                                <td style="width: 20px;border:0px;"></td>
                                

                                <td class="alertinfo" style="font-weight: bold;">Charges</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u>
                                    <a class="kpi_Charges" href='javascript:navTab.openTab("Alert Charges","/PagesContTrucking/Daily/AlertAPI.aspx?type=Charges" ,{title:"Alert Charges", fresh:true, external:true});'></a>
                                </u></td>
                                <td style="width: 20px;border:0px;"></td>


                                <td class="alertinfo" style="font-weight: bold;">Permit</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u><a class="kpi_Permit" href='javascript:navTab.openTab("Alert Permit","/PagesContTrucking/Daily/AlertAPI.aspx?type=Permit" ,{title:"Alert Permit", fresh:true, external:true});'></a></u></td>
                                <td style="width: 20px;border:0px;"></td>


                                <td class="alertinfo" style="font-weight: bold;">MustReturn</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u><a class="kpi_MustReturn" href='javascript:navTab.openTab("Alert MustReturn","/PagesContTrucking/Daily/AlertAPI.aspx?type=MustReturn" ,{title:"Alert MustReturn", fresh:true, external:true});'></a></u></td>
                                <td style="width: 20px;border:0px;"></td>


                                <td class="alertinfo" style="font-weight: bold;">VEHICLE</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u><a class="kpi_VEHICLE" href='javascript:open_printer("Alert VEHICLE","/PagesContTrucking/Daily/AlertAPI_Vehicle.aspx" );'></a></u></td>
                                <td style="width: 20px;border:0px;"></td>







                                <td class="alertinfo" style="font-weight: bold;"><a href='javascript:startKpi();'>Refresh</a></td>
                            </tr>
                            <tr>
                                <td class="alertinfo" style="color: red; font-weight: bold;"><u><a class="kpi_driver" href='javascript:open_printer("DRIVER","/PagesContTrucking/Job/DispatchPlanner2.aspx");'>DRIVERS</a></u></td>
                                <td style="width: 20px;border:0px;"></td>
                                

                                <td class="alertinfo" style="font-weight: bold;">Export</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u>
                                    <a class="kpi_Export" href='javascript:navTab.openTab("Alert Export","/PagesContTrucking/Daily/AlertAPI.aspx?type=Export" ,{title:"Alert Export", fresh:true, external:true});'></a>
                                </u></td>
                                <td style="width: 20px;border:0px;"></td>

                                
                                <td class="alertinfo" style="font-weight: bold;">C-DEM</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u><a class="kpi_CDEM" href='javascript:navTab.openTab("Alert C-DEM","/PagesContTrucking/Daily/AlertAPI.aspx?type=C-DEM" ,{title:"Alert C-DEM", fresh:true, external:true});'></a></u></td>
                                <td style="width: 20px;border:0px;"></td>

                                
                                <td class="alertinfo" style="font-weight: bold;">MustSend</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u><a class="kpi_MustSend" href='javascript:navTab.openTab("Alert MustSend","/PagesContTrucking/Daily/AlertAPI.aspx?type=MustSend" ,{title:"Alert MustSend", fresh:true, external:true});'></a></u></td>
                                <td style="width: 20px;border:0px;"></td>

                                
                                <td class="alertinfo" style="font-weight: bold;">PASSES</td>
                                <td class="alertcount" style="color: red; font-weight: bold;"><u><a class="kpi_PASSES" href='javascript:open_printer("Alert PASSES","/PagesContTrucking/Daily/AlertAPI_Passes.aspx");'></a></u></td>
                                <td style="width: 20px;border:0px;"></td>

                            </tr>

                            
                                <%--<td class=alertinfo style=" color:red;font-weight:bold;"><u><a class=kpi_cont href='javascript:navTab.openTab("CONT","/PagesContTrucking/Job/JobList.aspx" ,{title:"CONT", fresh:true, external:true});'>CONT</a></u></td>
				<td style="width:20px;"></td>--%>
                                <%--<td class="alertinfo" style="color: red; font-weight: bold;"><u><a class="kpi_loc" href='javascript:navTab.openTab("LOC","/modules/tpt/tptjoblist.aspx" ,{title:"LOC", fresh:true, external:true});'>LOC</a></u></td>
                                <td style="width: 20px;"></td>
                                <td class="alertinfo" style="color: red; font-weight: bold;"><u><a class="kpi_trips" href='javascript:navTab.openTab("TRIPS","/PagesContTrucking/Daily/UpdateContainer.aspx?type=ALL" ,{title:"TRIPS", fresh:true, external:true});'>TRIPS</a></u></td>
                                <td style="width: 20px;"></td>--%>
                                

                                <%--<td class=alertinfo style=" color:red;font-weight:bold;"><u><a class=kpi_vehicle href='javascript:open_printer("VEHICLE","/PagesContTrucking/Daily/AlertAPI_Vehicle.aspx");'>VEHICLE</a></u></td>
				<td style="width:20px;"></td>
				<td class=alertinfo style=" color:red;font-weight:bold;"><u><a class=kpi_passes href='javascript:open_printer("PASSES","/PagesContTrucking/Daily/AlertAPI_Passes.aspx");'>PASSES</a></u></td>
				<td style="width:20px;"></td>--%>
                                <%--<td class=alertinfo style=" font-weight:bold;">IMP</td>
				<td class=alertcount style=" color:red;font-weight:bold;"><u><a class=kpi_imp href='javascript:navTab.openTab("IMP","/PagesContTrucking/Daily/UpdateTrips.aspx?type=IMP" ,{title:"IMP", fresh:true, external:true});'></a></u></td>
				<td style="width:20px;"></td>
				<td class=alertinfo style=" font-weight:bold;">RET</td>
				<td class=alertcount style=" color:red;font-weight:bold;"><u><a class=kpi_ret href='javascript:navTab.openTab("RET","/PagesContTrucking/Daily/UpdateTrips.aspx?type=RET" ,{title:"RET", fresh:true, external:true});'></a></u></td>
				<td style="width:20px;"></td>
				<td class=alertinfo style=" font-weight:bold;">COL</td>
				<td class=alertcount style=" color:red;font-weight:bold;"><u><a class=kpi_col href='javascript:navTab.openTab("COL","/PagesContTrucking/Daily/UpdateTrips.aspx?type=COL" ,{title:"COL", fresh:true, external:true});'></a></u></td>
				<td style="width:20px;"></td>
				<td class=alertinfo style=" font-weight:bold;">EXP</td>
				<td class=alertcount style=" color:red;font-weight:bold;"><u><a class=kpi_exp href='javascript:navTab.openTab("EXP","/PagesContTrucking/Daily/UpdateTrips.aspx?type=EXP" ,{title:"EXP", fresh:true, external:true});'></a></u></td>
				<td style="width:20px;"></td>
				<td class=alertinfo style=" font-weight:bold;">SHF</td>
				<td class=alertcount style=" color:red;font-weight:bold;"><u><a class=kpi_shf href='javascript:navTab.openTab("SHF","/PagesContTrucking/Daily/UpdateTrips.aspx?type=SHF" ,{title:"SHF", fresh:true, external:true});'></a></u></td>
				<td style="width:20px;"></td>
				<td class=alertinfo style=" font-weight:bold;">LOC</td>
				<td class=alertcount style=" color:red;font-weight:bold;"><u><a class=kpi_loc href='javascript:navTab.openTab("LOC","/PagesContTrucking/Daily/UpdateTrips.aspx?type=LOC" ,{title:"LOC", fresh:true, external:true});'></a></u></td>
				<td style="width:20px;"></td>--%>
                        </table>
                    </div>

                    <div style="float: right">

                        <ul class="nav">
                            <li><a href="#"><%= EzshipHelper.GetUserName() %></a></li>
                            <li><a href="/Frames/Signout.ashx">Exit</a></li>
                        </ul>
                        <ul class="themeList" id="themeList">
                            <li theme="default">
                                <div class="selected">
                                    Blue
                                </div>
                            </li>
                            <li theme="green">
                                <div>
                                    Green
                                </div>
                            </li>
                            <li theme="blue">
                                <div>
                                    Dark
                                </div>
                            </li>
                            <li theme="purple">
                                <div>
                                    Purple
                                </div>
                            </li>
                            <li theme="silver">
                                <div>
                                    Silver
                                </div>
                            </li>
                            <li theme="azure">
                                <div>
                                    Sky
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
                <!-- navMenu -->
            </div>
            <div id="leftside">
                <div id="sidebar_s">
                    <div class="collapse">
                        <div class="toggleCollapse">
                            <div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="sidebar">
                    <div class="toggleCollapse">
                        <h2>Menu</h2>
                        <div>
                            Collapse
                        </div>
                    </div>
                    <div class="accordion" fillspace="sidebar" runat="server" id="menu">
                    </div>
                </div>
            </div>
            <div id="container">
                <div id="navTab" class="tabsPage">
                    <div class="tabsPageHeader">
                        <div class="tabsPageHeaderContent">
                            <!-- 显示左右控制时添加 class="tabsPageHeaderMargin" -->
                            <ul class="navTab-tab">
                                <li tabid="main" class="main"><a href="javascript:;"><span><span class="home_icon">Dashboard</span></span></a></li>
                            </ul>
                        </div>
                        <div class="tabsLeft">
                            left
                        </div>
                        <!-- 禁用只需要添加一个样式 class="tabsLeft tabsLeftDisabled" -->
                        <div class="tabsRight">
                            right
                        </div>
                        <!-- 禁用只需要添加一个样式 class="tabsRight tabsRightDisabled" -->
                        <div class="tabsMore">
                            more
                        </div>
                    </div>
                    <ul class="tabsMoreList">
                        <li><a href="javascript:;">Dashboard</a></li>
                    </ul>
                    <div class="navTab-panel tabsPageContent layoutBox">
                        <div class="page unitBox" style="height: 100%">
                            <% 
                                string user = HttpContext.Current.User.Identity.Name;
                                if (user == "MAKO")
                                {
                            %><iframe id="Iframe1" width="100%" height="100%" src="/Modules/WareHouse/Job/EcOrder.aspx"></iframe>
                            <%  } %>

                            <% else
                            { %>
                            <iframe id="MainFrame" width="100%" height="100%" src="/dashboard.aspx"></iframe>
                            <%  } %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="footer" style="vertical-align: bottom; display: none;">
        </div>

        <div id="printer" style="position: absolute; left: 400px; padding: 4px; background: #CCCCCC; border: solid 10px #AAAAAA; top: 0px; display: none; z-index: 20000; width: 900px; height: 600px; overflow: hidden">
            <div id="printer_bar" style="background: #DDDDDD; height: 40px; width: 100%">
                <table width="100%">
                    <tr>
                        <td>
                            <b id="printer_title"></b>
                        </td>
                        <td align="left" style="padding: 4px;">
                            <button onclick="close_printer(); return false;">Close</button>
                            <button onclick="refresh_printer(); return false;">Refresh</button>
                            <button onclick="print_printer(); return false;">Print</button>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="printer_pdf" style="overflow: hidden; background: lightblue; height: 300px; width: 900px;">
            </div>
        </div>
    </form>
</body>
<script>
    function open_printer(title, url) {
        var h = $(window).height();
        var w = $(window).width();

        var ifr = '<iframe name="printer_preview" id="printer_preview" width="100%" height="100%" style="width:100%;height:100%;border:0px" border=0 frameborder=0 src="' + url + '"></iframe>';
        $('#printer').css('left', w - 920);
        $('#printer').css('height', h - 22);
        $('#printer_pdf').css('height', h - 80);


        $('#printer').css('display', 'block');


        //ifr = '<iframe src="http://www.zaobao.com.sg"></iframe>';
        $("#printer_pdf").html(ifr);



    }
    function close_printer() {
        $('#printer').css('display', 'none');

        $("#prnter_pdf").html('');
    }

    function refresh_printer() {
		//alert(window.frames["printer_preview"].src );
		window.frames["printer_preview"].location.reload() ;
		//window.frames["printer_preview"].reload(true);
        
    }
	
	
	
    function print_printer() {
        window.frames["printer_preview"].focus();
        window.frames["printer_preview"].print();

    }
</script>
</html>
