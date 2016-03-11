<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bug.aspx.cs" Inherits="HiveDashboard.Bug" %>
<!DOCTYPE html>
<html lang="en">
<head>
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
    
	<!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries. Placeholdr.js enables the placeholder attribute -->
	<!--[if lt IE 9]>
        <link rel="stylesheet" href="assets/css/ie8.css">
		<script type="text/javascript" src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
		<script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/respond.js/1.1.0/respond.min.js"></script>
        <script type="text/javascript" src="assets/plugins/charts-flot/excanvas.min.js"></script>
	<![endif]-->

	<!-- The following CSS are included as plugins and can be removed if unused-->

<link rel='stylesheet' type='text/css' href="CSS/daterangepicker-bs3.css" /> 
<link rel='stylesheet' type='text/css' href='CSS/fullcalendar.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/bootstrap-markdown.min.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/prettify.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/toggles.css' /> 
</head>

<body class="">
<script>
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
                     <form>
                        
                    </form>
                </li>
                <li class="divider"></li>
                <li><a href="index.php"><i class="fa fa-home"></i> <span>Dashboard</span></a></li>
                <li class="haschild active open"><a href="javascript:;"><i class="fa fa-th"></i> <span>Bugs</span> </a>
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
                <li><a href="index.htm">Dashboard</a></li>
                <li class='active'>Bugs</li>
            </ol>

            <h1>Bugs</h1>
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
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <a class="info-tiles tiles-toyo" href="#">
                                <div class="tiles-heading">Today's Bugs</div>
                                <div class="tiles-body-alt">
                                    <!--i class="fa fa-bar-chart-o"></i-->
                                    <div class="text-center"><span class="text-top">#</span><asp:Label ID="lblBugs" runat="server" Text="0"></asp:Label></div>
                                    <small>
                                        <asp:Label ID="lblBugDiff" runat="server" Text="+/-0"></asp:Label> from last period</small>
                                </div>
                                <div class="tiles-footer">more info</div>
                            </a>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <a class="info-tiles tiles-success" href="#">
                                <div class="tiles-heading">Open Bugs</div>
                                <div class="tiles-body-alt">
                                    <!--i class="fa fa-money"></i-->
                                    <div class="text-center"><span class="text-top">#</span><asp:Label ID="lblOpenBug" runat="server" Text="0"></asp:Label><span class="text-smallcaps"></span></div>
                                    <small>
                                        <asp:Label ID="lblOpenBugs" runat="server" Text="+/-0"></asp:Label> from last period</small>
                                </div>
                                <div class="tiles-footer">more info</div>
                            </a>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <a class="info-tiles tiles-orange" href="#">
                                <div class="tiles-heading">Ready for test</div>
                                <div class="tiles-body-alt">
                                    <i class="fa fa-group"></i>
                                    <div class="text-center">0</div>
                                    <small>In QA / Resolved status</small>
                                </div>
                                <div class="tiles-footer">more info</div>
                            </a>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <a class="info-tiles tiles-alizarin" href="#">
                                <div class="tiles-heading">P1 Bugs this week</div>
                                <div class="tiles-body-alt">
                                    <i class="fa fa-shopping-cart"></i>
                                    <div class="text-center">0</div>
                                    <small>Total P1 raised this week</small>
                                </div>
                                <div class="tiles-footer">more info</div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>




            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 clearfix">
                                    <h4 class="pull-left" style="margin: 0 0 20px;">Bugs Report <small>(weekly)</small></h4>
                                    <div class="btn-group pull-right">
                                        <a href="javascript:;" class="btn btn-default btn-sm active">this week</a>
                                        <a href="javascript:;" class="btn btn-default btn-sm ">previous week</a>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div id="site-statistics" style="height:250px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-grape">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 clearfix">
                                    <h4 class="pull-left" style="margin: 0 0 20px;">Month Report <small>(by week)</small></h4>
                                    <div class="btn-group pull-right">
                                        <a href="javascript:;" class="btn btn-default btn-sm active">This month</a>
                                        <a href="javascript:;" class="btn btn-default btn-sm ">Previos month</a>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div id="budget-variance" style="height:250px;"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 clearfix">
                                    <h4 class="pull-left" style="margin:0 0 10px">Bug Reports <small>(overview)</small></h4>
                                    <div class="pull-right">
                                        <a href="javascript:;" class="btn btn-default-alt btn-sm"><i class="fa fa-refresh"></i></a>
                                        <a href="javascript:;" class="btn btn-default-alt btn-sm"><i class="fa fa-wrench"></i></a>
                                        <a href="javascript:;" class="btn btn-default-alt btn-sm"><i class="fa fa-cog"></i></a>
                                    </div>
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-2">
                                    <div id="indexvisits" style="width: 90px; margin: 0 auto; padding: 10px 0 6px;"><canvas width="90" height="45" style="display: inline-block; width: 90px; height: 45px; vertical-align: top;"></canvas></div>
                                    <h3 style="text-align: center; margin: 0; color: #808080;">
                                        <asp:Label ID="lblBlocker" runat="server" Text="0"></asp:Label>
                                       </h3>
                                    <p style="text-align: center; margin: 0;">Blocker Bugs</p>
                                    <hr class="hidden-md hidden-lg">
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-2">
                                    <div id="indexpageviews" style="width: 90px; margin: 0 auto; padding: 10px 0 6px;"><canvas width="90" height="45" style="display: inline-block; width: 90px; height: 45px; vertical-align: top;"></canvas></div>
                                    <h3 style="text-align: center; margin: 0; color: #808080;">
                                        <asp:Label ID="lblCritical" runat="server" Text="0"></asp:Label></h3>
                                    <p style="text-align: center; margin: 0;">Critical Bugs
                                        </p>
                                    <hr class="hidden-md hidden-lg">
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-2">
                                    <div id="indexpagesvisit" style="width: 90px; margin: 0 auto; padding: 10px 0 6px;"><canvas width="90" height="45" style="display: inline-block; width: 90px; height: 45px; vertical-align: top;"></canvas></div>
                                    <h3 style="text-align: center; margin: 0; color: #808080;">
                                        <asp:Label ID="lblMajor" runat="server" Text="0"></asp:Label>
                                        </h3>
                                    <p style="text-align: center; margin: 0;">Major Bugs</p>
                                    <hr class="hidden-md hidden-lg">
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-2">
                                    <div id="indexavgvisit" style="width: 90px; margin: 0 auto; padding: 10px 0 6px;"><canvas width="90" height="45" style="display: inline-block; width: 90px; height: 45px; vertical-align: top;"></canvas></div>
                                    <h3 style="text-align: center; margin: 0; color: #808080;">
                                        <asp:Label ID="lblMinor" runat="server" Text="0"></asp:Label>
                                       </h3>
                                    <p style="text-align: center; margin: 0;">Minor Bugs</p>
                                    <hr class="hidden-md hidden-lg">
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-2">
                                    <div id="indexnewvisits" style="width: 90px; margin: 0 auto; padding: 10px 0 6px;"><canvas width="90" height="45" style="display: inline-block; width: 90px; height: 45px; vertical-align: top;"></canvas></div>
                                    <h3 style="text-align: center; margin: 0; color: #808080;">
                                        <asp:Label ID="lblTrivial" runat="server" Text="0"></asp:Label></h3>
                                    <p style="text-align: center; margin: 0;">Trivial Bugs</p>
                                </div>
                                <div class="col-xs-6 col-sm-6 col-md-2">
                                    <div id="indexbouncerate" style="width: 90px; margin: 0 auto; padding: 10px 0 6px;"><canvas width="90" height="45" style="display: inline-block; width: 90px; height: 45px; vertical-align: top;"></canvas></div>
                                    <h3 style="text-align: center; margin: 0; color: #808080;">
                                        <asp:Label ID="lblTotalOpen" runat="server" Text="0"></asp:Label></h3>
                                    <p style="text-align: center; margin: 0;">Total Open Bugs</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xs-12 col-md-3 col-sm-6 col-lg-3">
                    <a class="info-tiles tiles-alizarin" href="#">
                        <div class="tiles-heading">
                            <div class="pull-left">NGDE Open Bugs</div>
                            <div class="pull-right"> </div>
                        </div>
                        <div class="tiles-body">
                            <div class="pull-left"><i class="fa fa-comments-o"></i></div>
                            <div class="pull-right"><asp:Label ID="lblNGDOpenBug" runat="server" Text="0"></asp:Label><div id="indexinfocomments" style="margin-top: 10px; margin-bottom: -10px;"></div></div>
                        </div>
                    </a>
                </div>
                <div class="col-xs-12 col-md-3 col-sm-6 col-lg-3">
                    <a class="info-tiles tiles-orange" href="#">
                        <div class="tiles-heading">
                            <div class="pull-left">Web Open Bugs</div>
                            <div class="pull-right"><asp:Label ID="lblWebOpenBugs" runat="server" Text="0"></asp:Label></div>
                        </div>
                        <div class="tiles-body">
                            <div class="pull-left"><i class="fa fa-thumbs-o-up"></i></div>
                            <div class="pull-right"><asp:Label ID="Label1" runat="server" Text="0"></asp:Label><div id="indexinfolikes" style="margin-top: 10px; margin-bottom: -10px;"></div></div>
                        </div>
                    </a>
                </div>
                <div class="col-xs-12 col-md-3 col-sm-6 col-lg-3">
                    <a class="info-tiles tiles-success" href="#">
                        <div class="tiles-heading">
                            <div class="pull-left">Android Open Bugs</div>
                            <div class="pull-right"></div>
                        </div>
                        <div class="tiles-body">
                            <div class="pull-left"><i class="fa fa-check"></i></div>
                            <div class="pull-right"><asp:Label ID="lblAndOpenBugs" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </a>
                </div>
                <div class="col-xs-12 col-md-3 col-sm-6 col-lg-3">
                    <a class="info-tiles tiles-toyo" href="#">
                        <div class="tiles-heading">
                            <div class="pull-left">iOS Open Bugs</div>
                            <div class="pull-right"> </div>
                        </div>
                        <div class="tiles-body">
                            <div class="pull-left"><i class="fa fa-download"></i></div>
                            <div class="pull-right"><asp:Label ID="lblIOSOpenBugs" runat="server" Text="0"></asp:Label></div>
                        </div>
                    </a>
                </div>

                
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-grape">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 clearfix">
                                    <h4 class="pull-left" style="margin:0 0 10px">NGDE <small>(overview)</small></h4>
                                    <div class="btn-group pull-right">
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-orange">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 clearfix">
                                    <h4 class="pull-left" style="margin: 0 0 20px;">Android</h4>
                                    <div class="btn-group pull-right">
                                        
                                    </div>
                                    <h4 class="pull-left" style="margin:0 0 10px">&nbsp;<small>(overview)</small></h4>
                                    <div class="btn-group pull-right">
                                        
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-grape">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 clearfix">
                                    <h4 class="pull-left" style="margin:0 0 10px">Web <small>(overview)</small></h4>
                                    <div class="btn-group pull-right">
                                        
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-orange">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 clearfix">
                                    <h4 class="pull-left" style="margin: 0 0 20px;">iOS</h4>
                                    <div class="btn-group pull-right">
                                        
                                    </div>
                                &nbsp;<h4 class="pull-left" style="margin:0 0 10px">&nbsp;<small>(overview)</small></h4>
                                    <div class="btn-group pull-right">
                                        
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            


            <div class="row">        
                <div class="col-md-6">
                    <div class="panel panel-indigo">
                        <div class="panel-heading">
                            <h4>Resource Level Details</h4>
                            <div class="options">
                                <a href="javascript:;"><i class="fa fa-cog"></i></a>
                                <a href="javascript:;"><i class="fa fa-wrench"></i></a> 
                                <a href="javascript:;" class="panel-collapse"><i class="fa fa-chevron-down"></i></a>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                               
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
                    <div class="panel panel-grape">
                        <div class="panel-heading">
                              <h4><i class="icon-highlight fa fa-check"></i>List</h4>
                              <!-- <div class="options">
                                <a href="javascript:;"><i class="fa fa-cog"></i></a>
                                <a href="javascript:;"><i class="fa fa-wrench"></i></a> 
                                <a href="javascript:;" class="panel-collapse"><i class="fa fa-chevron-down"></i></a>
                              </div> -->
                        </div>
                        <div class="panel-body">
                            
                        </div>
                    </div>
                </div>
            </div>

           <%-- <div class="row">
                <div class="col-md-6">
                    <div class="panel panel-inverse">
                        <div class="panel-heading">
                              <h4><i class="icon-highlight fa fa-calendar"></i> Calendar</h4>
                              <div class="options">
                                <a href="javascript:;"><i class="fa fa-cog"></i></a>
                                <a href="javascript:;"><i class="fa fa-wrench"></i></a> 
                                <a href="javascript:;" class="panel-collapse"><i class="fa fa-chevron-down"></i></a>
                              </div>
                        </div>
                        <div class="panel-body" id="calendardemo">
                              <div id='calendar-drag'></div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="panel panel-midnightblue">
                        <div class="panel-heading">
                              <h4>
                                <ul class="nav nav-tabs">
                                  <li class="active"><a href="#threads" data-toggle="tab"><i class="fa fa-list visible-xs icon-scale"></i><span class="hidden-xs">Threads</span></a></li>
                                  <li><a href="#comments" data-toggle="tab"><i class="fa fa-comments visible-xs icon-scale"></i><span class="hidden-xs">Comments</span></a></li>
                                  <li><a href="#users" data-toggle="tab"><i class="fa fa-group visible-xs icon-scale"></i><span class="hidden-xs">Users</span></a></li>
                                </ul>
                              </h4>
                              <!-- <div class="options">
                                <a href="javascript:;"><i class="fa fa-cog"></i></a>
                                <a href="javascript:;"><i class="fa fa-wrench"></i></a> 
                              </div> -->
                        </div>
                        <div class="panel-body">
                            <div class="tab-content">
                                <div class="tab-pane active" id="threads">
                                    <ul class="panel-threads">
                                        <li>
                                            <img src="assets/demo/avatar/aniss.png" alt="Aniss">
                                            <div class="content">
                                                <span class="time">20 mins</span>
                                                <a href="#" class="title">Envato’s Most Wanted – $5,000 Reward for Music & Band Themes and Templates</a>
                                                <span class="thread">asked by <a href="#">Jim Gordon</a> in <a href="#">Section #3</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/corbett.png" alt="Corbett">
                                            <div class="content">
                                                <span class="time">2 hrs</span>
                                                <a href="#" class="title">How to create a corporate wordpress theme?</a>
                                                <span class="thread">asked by <a href="#">Simon Corbett</a> in <a href="#">Section #15</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/dangerfield.png" alt="Dangerfield">
                                            <div class="content">
                                                <span class="time">4 hrs</span>
                                                <a href="#" class="title">Which cart is growing in popularity - WOOCOMMERCE or OPENCART? And which one would you choose?</a>
                                                <span class="thread">asked by <a href="#">Jeff Dangerfield</a> in <a href="#">Section #9</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/doyle.png" alt="Doyle">
                                            <div class="content">
                                                <span class="time">13 hrs</span>
                                                <a href="#" class="title">Pros and Cons of Using Grids in Responsive Web Design</a>
                                                <span class="thread">asked by <a href="#">Alan Doyle</a> in <a href="#">Section #11</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/jackson.png" alt="Jackson">
                                            <div class="content">
                                                <span class="time">19 hrs</span>
                                                <a href="#" class="title">Best Web & Graphic Design Proposal Software</a>
                                                <span class="thread">asked by <a href="#">Eric Jackson</a> in <a href="#">Section #18</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/aniss.png" alt="Aniss">
                                            <div class="content">
                                                <span class="time">20 mins</span>
                                                <a href="#" class="title">Envato’s Most Wanted – $5,000 Reward for Music & Band Themes and Templates</a>
                                                <span class="thread">asked by <a href="#">Jim Gordon</a> in <a href="#">Section #3</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/corbett.png" alt="Corbett">
                                            <div class="content">
                                                <span class="time">2 hrs</span>
                                                <a href="#" class="title">How to create a corporate wordpress theme?</a>
                                                <span class="thread">asked by <a href="#">Simon Corbett</a> in <a href="#">Section #15</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/dangerfield.png" alt="Dangerfield">
                                            <div class="content">
                                                <span class="time">4 hrs</span>
                                                <a href="#" class="title">Which cart is growing in popularity - WOOCOMMERCE or OPENCART? And which one would you choose?</a>
                                                <span class="thread">asked by <a href="#">Jeff Dangerfield</a> in <a href="#">Section #9</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/doyle.png" alt="Doyle">
                                            <div class="content">
                                                <span class="time">13 hrs</span>
                                                <a href="#" class="title">Pros and Cons of Using Grids in Responsive Web Design</a>
                                                <span class="thread">asked by <a href="#">Alan Doyle</a> in <a href="#">Section #11</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/jackson.png" alt="Jackson">
                                            <div class="content">
                                                <span class="time">19 hrs</span>
                                                <a href="#" class="title">Best Web & Graphic Design Proposal Software</a>
                                                <span class="thread">asked by <a href="#">Eric Jackson</a> in <a href="#">Section #18</a></span>
                                            </div>
                                        </li>
                                    </ul>
                                    <a href="#" class="btn btn-default-alt btn-sm pull-right">Load More</a>
                                </div>
                                <div class="tab-pane" id="comments">
                                    <ul class="panel-comments">
                                        <li>
                                            <img src="assets/demo/avatar/aniss.png" alt="Aniss">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Jim Gordon</a> commented on <a href="#">Article #121</a></span>
                                                Just wondering - can random users see our comments? If so, allow them to comment!
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/corbett.png" alt="Corbett">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Simon Corbett</a> commented on <a href="#">Article #55</a></span>
                                                Not sure what changed but for the last few weeks a few of my regulars are having their comments held for moderation.
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/paton.png" alt="Corbett">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Polly Paton</a> commented on <a href="#">Article #12</a></span>
                                                I’m sure there is a tool for that. 
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/watson.png" alt="Watson">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Annie Watson</a> commented on <a href="#">Article #223</a></span>
                                                We have enough problems with Spammers already without letting non members leave comments.
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/aniss.png" alt="Aniss">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Jim Gordon</a> commented on <a href="#">Article #121</a></span>
                                                Just wondering - can random users see our comments? If so, allow them to comment!
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/corbett.png" alt="Corbett">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Simon Corbett</a> commented on <a href="#">Article #55</a></span>
                                                Not sure what changed but for the last few weeks a few of my regulars are having their comments held for moderation.
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/paton.png" alt="Corbett">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Polly Paton</a> commented on <a href="#">Article #12</a></span>
                                                I’m sure there is a tool for that. 
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/watson.png" alt="Watson">
                                            <div class="content">
                                                <span class="actions"><div class="options"><div class="btn-group"><button class="btn btn-default btn-xs"><i class="fa fa-pencil"></i></button><button class="btn btn-default btn-xs"><i class="fa fa-times"></i></button></div></div></span>
                                                <span class="commented"><a href="#">Annie Watson</a> commented on <a href="#">Article #223</a></span>
                                                We have enough problems with Spammers already without letting non members leave comments.
                                            </div>
                                        </li>
                                    </ul>
                                    <a href="#" class="btn btn-default-alt btn-sm pull-right">Load More</a>
                                </div>
                                <div class="tab-pane" id="users">
                                    <ul class="panel-users">
                                        <li>
                                            <img src="assets/demo/avatar/paton.png" alt="Paton">
                                            <div class="content">
                                                <span class="time">11 mins</span>
                                                <span class="desc"><a href="#">Polly Paton</a> followed <a href="#">John White</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/tennant.png" alt="Tennant">
                                            <div class="content">
                                                <span class="time">48 mins</span>
                                                <span class="desc"><a href="#">David Tennant</a> unfollowed <a href="#">Tony Doubleday</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/jobs.png" alt="Jobs">
                                            <div class="content">
                                                <span class="time">5 hrs</span>
                                                <span class="desc"><a href="#">Howard Jobs</a> commented on <a href="#">Selling PSD Template Rights!</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/dangerfield.png" alt="Dangerfield">
                                            <div class="content">
                                                <span class="time">6 hrs</span>
                                                <span class="desc"><a href="#">Jeff Dangerfield</a> posted on <a href="#">Please help with Theme Design</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/aniss.png" alt="Aniss">
                                            <div class="content">
                                                <span class="time">22 hrs</span>
                                                <span class="desc"><a href="#">Jim Gordon</a> followed <a href="#">Polly Paton</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/corbett.png" alt="Corbett">
                                            <div class="content">
                                                <span class="time">3 days</span>
                                                <span class="desc"><a href="#">Simon Corbett</a> followed <a href="#">Anna Johansson</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/paton.png" alt="Paton">
                                            <div class="content">
                                                <span class="time">11 mins</span>
                                                <span class="desc"><a href="#">Polly Paton</a> followed <a href="#">John White</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/tennant.png" alt="Tennant">
                                            <div class="content">
                                                <span class="time">48 mins</span>
                                                <span class="desc"><a href="#">David Tennant</a> unfollowed <a href="#">Tony Doubleday</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/jobs.png" alt="Jobs">
                                            <div class="content">
                                                <span class="time">5 hrs</span>
                                                <span class="desc"><a href="#">Howard Jobs</a> commented on <a href="#">Selling PSD Template Rights!</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/dangerfield.png" alt="Dangerfield">
                                            <div class="content">
                                                <span class="time">6 hrs</span>
                                                <span class="desc"><a href="#">Jeff Dangerfield</a> posted on <a href="#">Please help with Theme Design</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/aniss.png" alt="Aniss">
                                            <div class="content">
                                                <span class="time">22 hrs</span>
                                                <span class="desc"><a href="#">Jim Gordon</a> followed <a href="#">Polly Paton</a></span>
                                            </div>
                                        </li>
                                        <li>
                                            <img src="assets/demo/avatar/corbett.png" alt="Corbett">
                                            <div class="content">
                                                <span class="time">3 days</span>
                                                <span class="desc"><a href="#">Simon Corbett</a> followed <a href="#">Anna Johansson</a></span>
                                            </div>
                                        </li>
                                    </ul>
                                    <a href="#" class="btn btn-default-alt btn-sm pull-right">Load More</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>

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

</div> <!-- page-container -->

<!--
<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js"></script>

<script>!window.jQuery && document.write(unescape('%3Cscript src="assets/js/jquery-1.10.2.min.js"%3E%3C/script%3E'))</script>
<script type="text/javascript">!window.jQuery.ui && document.write(unescape('%3Cscript src="assets/js/jqueryui-1.10.3.min.js'))</script>
-->

<script type='text/javascript' src='JS/jquery-1.10.2.min.js'></script> 
<script type='text/javascript' src='JS/jqueryui-1.10.3.min.js'></script> 
<script type='text/javascript' src='JS/bootstrap.min.js'></script> 
<script type='text/javascript' src='JS/enquire.js'></script> 
<script type='text/javascript' src='JS/jquery.cookie.js'></script> 
<script type='text/javascript' src='JS/jquery.nicescroll.min.js'></script> 
<%--<script type='text/javascript' src='assets/plugins/codeprettifier/prettify.js'></script> 
<script type='text/javascript' src='assets/plugins/easypiechart/jquery.easypiechart.min.js'></script> 
<script type='text/javascript' src='assets/plugins/sparklines/jquery.sparklines.min.js'></script> 
<script type='text/javascript' src='assets/plugins/form-toggle/toggle.min.js'></script> 
<script type='text/javascript' src='assets/plugins/fullcalendar/fullcalendar.min.js'></script> 
<script type='text/javascript' src='assets/plugins/form-daterangepicker/daterangepicker.min.js'></script> 
<script type='text/javascript' src='assets/plugins/form-daterangepicker/moment.min.js'></script> 
<script type='text/javascript' src='assets/plugins/charts-flot/jquery.flot.min.js'></script> 
<script type='text/javascript' src='assets/plugins/charts-flot/jquery.flot.resize.min.js'></script> 
<script type='text/javascript' src='assets/plugins/charts-flot/jquery.flot.orderBars.min.js'></script> 
<script type='text/javascript' src='assets/plugins/pulsate/jQuery.pulsate.min.js'></script> --%>
<script type='text/javascript' src='assets/demo/demo-index.js'></script> 
<script type='text/javascript' src='JS/placeholdr.js'></script> 
<script type='text/javascript' src='JS/application.js'></script> 
<script type='text/javascript' src='assets/demo/demo.js'></script> 

</body>
</html>