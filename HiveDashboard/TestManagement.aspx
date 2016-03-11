<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestManagement.aspx.cs" Inherits="HiveDashboard.TestManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<meta charset="utf-8">
	<title>Hive Dashboard</title>
        <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	<meta name="description" content="Avant">
	<meta name="author" content="The Red Team">

    <link rel="stylesheet" href="CSS/styles.min.css">
    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600' rel='stylesheet' type='text/css'>

	 
        <link href='assets/demo/variations/default.css' rel='stylesheet' type='text/css' media='all' id='styleswitcher'> 
    
            <link href='assets/demo/variations/default.css' rel='stylesheet' type='text/css' media='all' id='headerswitcher'> 
  
	<!-- The following CSS are included as plugins and can be removed if unused-->

<link rel='stylesheet' type='text/css' href="CSS/daterangepicker-bs3.css" /> 
<link rel='stylesheet' type='text/css' href='CSS/fullcalendar.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/bootstrap-markdown.min.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/prettify.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/toggles.css' /> 
   
</head>

<body class="">
    <form runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-44426473-2', 'auto');
        ga('send', 'pageview');

    </script>
    
    <div id="headerbar">
        <div class="container">
            <div class="row">
                <div class="col-xs-6 col-sm-2">
                    <a href="#" class="shortcut-tiles tiles-brown">
                       
                    </a>
                </div>
                
                            
            </div>
        </div>
    </div>

    <header class="navbar navbar-inverse navbar-fixed-top" role="banner">
        <a id="leftmenu-trigger" class="tooltips" data-toggle="tooltip" data-placement="right" title="Toggle Sidebar"></a>

        <div class="navbar-header pull-left">
            
        </div>

        
    </header>

    <div id="page-container">
        <!-- BEGIN SIDEBAR -->
        <nav id="page-leftbar" role="navigation">
                <!-- BEGIN SIDEBAR MENU -->
            <ul class="acc-menu" id="sidebar">
                <li id="search">
                    <a href="javascript:;"><i class="fa fa-search opacity-control"></i></a>
                     
                </li>
                <li class="divider"></li>
                <li class="active"><a href="index.php"><i class="fa fa-home"></i> <span>Dashboard</span></a></li>
                <li ><a href="javascript:;"><i class="fa fa-th"></i> <span>Bugs</span> </a>
                    <ul class="acc-menu">
                        <li><a href="layout-grid.php"><span>Today's Status</span></a></li>
                        <li><a href="layout-horizontal.php"><span>7 Day status</span></a></li>
                        <li><a href="layout-horizontal2.php"><span>Month Status</span></a></li>
                        <li><a href="layout-fixed.php"><span>Resource Wise Status</span></a></li>
                    </ul>
                </li>
                
                <li class="divider"></li>
                <li><a href="javascript:;"><i class="fa fa-briefcase"></i> <span>Reports</span> </a>
                    
                </li>
                
            </ul>
            <!-- END SIDEBAR MENU -->
        </nav>

        <!-- BEGIN RIGHTBAR -->
        <div id="page-rightbar">

            <div id="chatarea">
                
            </div>

            <div id="widgetarea">
                <div class="widget">
                   
                </div>


                <div id="chatbar" class="widget">
                   
                </div>

                <div class="widget">
                
                </div>

 

                <div class="widget">
                    
                </div>

                <div class="widget">
                   
                </div>

            </div>
        </div>
        <!-- END RIGHTBAR -->
<div id="page-content">
    
    <div id='wrap'>
        <div id="page-heading">
            <ol class="breadcrumb">
                <li><a href="index.htm">Manage Test</a></li>
               
            </ol>

            <h1>Manage Test</h1>
            <div class="options">
                <div class="btn-toolbar">
                    <button class="btn btn-default" id="daterangepicker2">
                        <i class="fa fa-calendar-o"></i> 
                        <span class="hidden-xs hidden-sm">e-Mail</span> <b class="caret"></b>
                    </button>
                    <div class="btn-group hidden-xs">
                        <a href='#' class="btn btn-default dropdown-toggle" data-toggle='dropdown'><i class="fa fa-cloud-download"></i><span class="hidden-xs hidden-sm hidden-md"> Export Xlsx</span> <span class="caret"></span></a>
                        
                    </div>
                    <a href="#" class="btn btn-default hidden-xs"><i class="fa fa-cog"></i></a>
                </div>
            </div>
        </div>


        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand" >
                            <ItemTemplate>
                                <div class="col-md-3 col-xs-12 col-sm-6" onclick="">
                                   
                            <a class="info-tiles tiles-toyo" href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="Label1" runat="server" Text=''></asp:Label></div>
                                <div class="tiles-body-alt">
                                    <!--i class="fa fa-bar-chart-o"></i-->
                                    <div class="text-center"><span class="text-top"></span><asp:Label ID="lblRPIStatus" runat="server" Text='<%# Eval("Kit") %>'></asp:Label></div>
                                    <small>
                                        <asp:Label ID="lblBugDiff" runat="server" Text=""></asp:Label>Current status</small>
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <table style="width: 100%; font-size: x-small;" >
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="imgDevice" runat="server" ImageUrl='<%# Eval("Image") %>' Height="40px" Width="40px" /></td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblDeviceName" runat="server" Text='<%# Eval("DeviceType") %>'></asp:Label>
                                                    </td>
                                                
                                                    <td>
                                                        Firmware</td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblFirmwareVersion" runat="server" Text='<%# Eval("DeviceVersion") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                               
                                            </table>
                                            
                                        </ItemTemplate>
                                    </asp:Repeater>
                                     </div>
                                
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%" Height="100%" BackColor="Transparent" BorderColor="Transparent" /></div>
                                
                            </a>
                        </div>
                                  
                                
                            </ItemTemplate>
                        </asp:Repeater>
                       
                    </div>
                </div>
            </div>




            <div class="row">
                
                <asp:Panel ID="Panel1" runat="server">
                    <asp:PieChart ID="PieChart1" runat="server"></asp:PieChart>
                    <table style="width:100%;">
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                
                                <asp:Repeater ID="Repeater3" runat="server">
                                    <ItemTemplate>
                                        <div id="visualization" style="width: 600px; height: 350px;">
                                        <table style="width: 100%;" class="table">
                                            <tr>
                                                <th>Attribute</th>
                                                <th>
                                                    </th><th>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>Scenario No</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblScenarioNo" runat="server" Text='<%# Eval("ScenarioId") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Scenario ID</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblScenarioId" runat="server" Text='<%# Eval("ScenarioName") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Description</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("ScenarioNo") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Status</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblScenarioStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Time Stamp</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblTimeStamp" runat="server" Text='<%# Eval("TimeStamp") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Time Duration</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>Feature Filename</td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FeatureFileName") %>'></asp:Label></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
                
            </div>

            <div class="row">
               
            </div>

                
            </div>

           

        </div> <!-- container -->
    </div> <!--wrap -->
</div> <!-- page-content -->

    <footer role="contentinfo">
        <div class="clearfix">
            <ul class="list-unstyled list-inline pull-left">
                <li>Hive</li>
            </ul>
            <button class="pull-right btn btn-inverse-alt btn-xs hidden-print" id="back-to-top"><i class="fa fa-arrow-up"></i></button>
        </div>
    </footer>
    </form>

<script type='text/javascript' src='JS/jquery-1.10.2.min.js'></script> 
<script type='text/javascript' src='JS/jqueryui-1.10.3.min.js'></script> 
<script type='text/javascript' src='JS/bootstrap.min.js'></script> 
<script type='text/javascript' src='JS/enquire.js'></script> 
<script type='text/javascript' src='JS/jquery.cookie.js'></script> 
<script type='text/javascript' src='JS/jquery.nicescroll.min.js'></script> 

<script type='text/javascript' src='assets/demo/demo-index.js'></script> 
<script type='text/javascript' src='JS/placeholdr.js'></script> 
<script type='text/javascript' src='JS/application.js'></script> 
<script type='text/javascript' src='assets/demo/demo.js'></script> 

</body>
</html>
