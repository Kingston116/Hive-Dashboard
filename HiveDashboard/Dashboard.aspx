<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HiveDashboard.Dashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="initial-scale=0.25">
    <meta http-equiv="refresh" content="30">
    <title></title>
    <link rel='stylesheet' type='text/css' href="CSS/daterangepicker-bs3.css" /> 
<link rel='stylesheet' type='text/css' href='CSS/fullcalendar.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/bootstrap-markdown.min.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/prettify.css' /> 
<link rel='stylesheet' type='text/css' href='CSS/toggles.css' /> 
    <link rel="stylesheet" href="CSS/styles.min.css" />
    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="CSS/styles.min.css" />
    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600' rel='stylesheet' type='text/css' />
    <style type="text/css">
        html { 
    zoom: .70; 
}
    </style>
</head>
<body style="background-color:black;background-image:url('http://w71001324114.bgchnet.co.uk/HiveDashboard/Images/bg.JPG');background-size:100% 100%">
    
   
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <cc1:BalloonPopupExtender ID="BalloonPopupExtender1" runat="server" TargetControlID="imgRPI2Result" BalloonPopupControlID="Panel11" DisplayOnMouseOver="true" BalloonSize="Large" ></cc1:BalloonPopupExtender>
    <asp:Panel ID="Panel11" runat="server" ForeColor="Black">
    This is a Cloud Balloon Popup
        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
                <td>More Info</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <cc1:PieChart ID="piChart2" runat="server">
                    </cc1:PieChart>
                </td>
                <td>&nbsp;Start Time &nbsp;</td>
                <td>
                    <asp:Label ID="lblStart2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Last Uppdated time</td>
                <td>
                    <asp:Label ID="lblLastUpdate2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
</asp:Panel>
    <div>
    
        <div>
            <table style="width:100%; font-size: xx-small;">
                <tr style="vertical-align:central; text-align: center;">
                    <td style="color: #FFFFFF; font-size: 20px; font-weight: bold; text-align: center;">
                        &nbsp;</td>
                    <td style="color: #FFFFFF; font-size: 20px; font-weight: bold; text-align: center;">
                        &nbsp;</td>
                    <td colspan="5" style="color: #FFFFFF; font-size: 20px; font-weight: bold; text-align: center;">
                      </td>
                </tr>
                <tr style="vertical-align:central">
                    <td style="vertical-align: bottom; color: #FFFFFF; font-size: 20px; font-weight: bold;">
                         &nbsp;</td>
                    <td style="vertical-align: bottom; color: #FFFFFF; font-size: 20px; font-weight: bold;">
                         &nbsp;</td>
                    <td colspan="2" style="vertical-align: bottom; color: #FFFFFF; font-size: 20px; font-weight: bold;">
                         &nbsp;</td>
                    <td colspan="3" style="vertical-align: middle; color: #FFFFFF; font-size: 20px; font-weight: bold;" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                         &nbsp;</td>
                    <td>
                         &nbsp;</td>
                    <td>
                         <div class="col-md-10 col-xs-12 col-sm-12" onclick="" style="width: 300px">
                                   
                            <a class="info-tiles tiles-success" runat="server" id="rpi2Class" href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl2" runat="server" Text='RPI2'></asp:Label></div>
                                <div class="tiles-body-alt">
                                    <!--i class="fa fa-bar-chart-o"></i-->
                                   
                                      <asp:Panel ID="Panel1" runat="server" Font-Size="Small">
                                <table style="margin: 0px; padding: 0px; width:100%; border-collapse: collapse; border-spacing: 0px;">
                                    <tr>
                                        <td colspan="4" rowspan="2" style="width: 153px">
                                            <asp:Image ID="imgRPI2Status" runat="server" Height="63px" Width="74px" Visible="False" />
                                        </td>
                                         <td rowspan="2" colspan="5">
                                            <asp:Image ID="imgRPI2Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px" Visible="False" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgRPI2Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px" OnClick="imgRPI2Result_Click" Visible="False" /> 
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td>
                                            &nbsp;
                                            <asp:ImageButton ID="imgIgnore2" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore2_Click"  />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <Marquee><asp:Label ID="lblRPI2TestName" runat="server" Font-Size="Small" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="Label3" runat="server" Text="Status" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI2Status" runat="server" Text="" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label6" runat="server" Text="Iteration" Font-Size="Small"></asp:Label>
                                            &nbsp;
                                        </td>
                                        <td style="width: 10px">&nbsp;</td>
                                        <td style="width: 10px">&nbsp;</td>
                                        <td style="width: 10px">
                                            
                                            &nbsp;</td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                            </td>
                                        <td style="width: 10px"><asp:Label ID="lblRPI2IterationComp" runat="server" Font-Size="Small" Text="0"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label67" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI2IterationPass" runat="server" Text="0"></asp:Label>
                                            </td>
                                        <td style="width: 10px"><asp:Label ID="lbl" runat="server" Text="F"></asp:Label></td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI2IterationFailed" runat="server" Text="0"></asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="10"><marquee>
                                            <asp:Label ID="lblRPI2DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label>

                                                        </marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart1" runat="server" Visible="False" Height="100px" Width="255px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                                       </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" ID="rpi2" OnClick="rpi2_Click" /></div>
                                </a>
                            
                        </div>
                    </td>
                    <td colspan="2">
                        <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-toyo"  runat="server" id="rpi3Class" href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl3" runat="server" Text='RPI3'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel2" runat="server" Font-Size="Small">
                                <table style="width:100%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                            <asp:Image ID="imgRPI3Status" runat="server" Height="68px" Width="153px" Visible="False" />
                                        </td>
                                        <td rowspan="2" colspan="3">
                                            <asp:Image ID="imgRPI3Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px"  Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI3Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px"  Visible="False" OnClick="imgRPI3Result_Click" />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore3" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore3_Click" />
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            
                                            <asp:Label ID="Label82" runat="server" Text="Test Name"></asp:Label>
                                            </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI3TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label11" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI3Status" runat="server" Text=""></asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label13" runat="server" Text="Iteration"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                             <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI3IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label9" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI3IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label22" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI3IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI3DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart2" runat="server" Visible="False" Height="100px" Width="259px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        
                                
                            
                                </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" /></div>
                                </a></div>
                    </td>
                    <td>
                       <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-sky"  runat="server" id="rpi4Class" href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl4" runat="server" Text='RPI4'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel3" runat="server" Font-Size="Small">
                                <table style="width:100%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                            <asp:Image ID="imgRPI4Status" runat="server" Height="73px" Width="74px"  Visible="False" />
                                        </td>
                                        <td rowspan="2" colspan="3">
                                            <asp:Image ID="imgRPI4Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px"  Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI4Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px"  Visible="False" OnClick="imgRPI4Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore4" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore4_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI4TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI4Status" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="Iteration"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label83" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI4IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label43" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI4IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label65" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI4IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI4DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart3" runat="server" Visible="False" Height="100px" Width="256px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" /></div>
                                </a></div>
                    </td>
                    <td>
                        <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-danger"  runat="server" id="rpi5Class"  href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl5" runat="server" Text='RPI5'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel4" runat="server" Font-Size="Small">
                                <table style="width:100%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                            <asp:Image ID="imgRPI5Status" runat="server" Height="73px" Width="76px"  Visible="False" />
                                        </td>
                                        <td rowspan="2" colspan="3">
                                            <asp:Image ID="imgRPI5Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px"  Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI5Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px"  Visible="False" OnClick="imgRPI5Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore5" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore5_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label23" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI5TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label25" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI5Status" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label27" runat="server" Text="Iteration"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label84" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI5IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label73" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI5IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label75" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI5IterationFailed" runat="server" Text="0"></asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI5DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart4" runat="server" Visible="False" Height="100px" Width="260px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                       </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" /></div>
                                </a></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;<&nbsp;;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                      &nbsp;</td>
                    <td>
                       <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-success"  runat="server" id="rpi6Class" href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl6" runat="server" Text='RPI6'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel5" runat="server" Font-Size="Small">
                                <table style="width:95%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                           
                                            <asp:Image ID="imgRPI6Status" runat="server" Height="63px" Width="74px" Visible="False" />
                                           
                                        </td>
                                        <td rowspan="2" colspan="3"><asp:Image ID="imgRPI6Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px" Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI6Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px" Visible="False" OnClick="imgRPI6Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore6" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore6_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label30" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI6TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label32" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI6Status" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label34" runat="server" Text="Iteration Completed"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label88" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI6IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label78" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI6IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label80" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI6IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI6DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart5" runat="server" Visible="False" Height="100px" Width="260px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" /></div>
                                </a></div>
                    </td>
                    <td colspan="2">
                        <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-sky" runat="server" id="rpi7Class"  href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl7" runat="server" Text='RPI7'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel6" runat="server" Font-Size="Small">
                                <table style="width:100%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                           
                                            <asp:Image ID="imgRPI7Status" runat="server" Height="73px" Width="53px"  Visible="False" />
                                           
                                        </td>
                                        <td rowspan="2" colspan="3"> <asp:Image ID="imgRPI7Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px"  Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI7Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px"  Visible="False" OnClick="imgRPI7Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore7" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px"  Visible="true" OnClick="imgIgnore7_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label37" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI7TestName" runat="server" Text="Upgarde/Downgrade"></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label39" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI7Status" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label41" runat="server" Text="Iteration"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label87" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI7IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label4" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI7IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label7" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI7IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                     <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI7DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart6" runat="server" Visible="False" Height="100px" Width="257px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                       </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" /></div>
                                </a></div>
                    </td>
                    <td>
                       <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-danger" runat="server" id="rpi8Class"  href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl8" runat="server" Text='RPI8'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel7" runat="server" Font-Size="Small">
                                <table style="width:100%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                            
                                            <asp:Image ID="imgRPI8Status" runat="server" Height="73px" Width="76px"  Visible="False" />
                                            
                                        </td>
                                        <td rowspan="2" colspan="3"> <asp:Image ID="imgRPI8Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px"  Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI8Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px"  Visible="False" OnClick="imgRPI8Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore8" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore8_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label44" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI8TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label46" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI8Status" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label48" runat="server" Text="Iteration"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label86" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI8IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label12" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI8IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label19" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI8IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                     <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI8DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart7" runat="server" Visible="False" Height="100px" Width="257px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                         </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" /></div>
                                </a></div>
                    </td>
                    <td>
                       <div class="col-md-10 col-xs-12 col-sm-12"  style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-toyo" runat="server" id="rpi9Class"  href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl9" runat="server" Text='RPI9'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel8" runat="server" Font-Size="Small">
                                <table style="width:100%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                            
                                            <asp:Image ID="imgRPI9Status" runat="server" Height="76px" Width="100px"  Visible="False" />
                                            
                                        </td>
                                        <td rowspan="2" colspan="3">
                                            <asp:Image ID="imgRPI9Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px"  Visible="False" />
                                           </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI9Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px"  Visible="False" OnClick="imgRPI9Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore9" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore9_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label51" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI9TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label53" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI9Status" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label55" runat="server" Text="Iteration"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label85" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI9IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label31" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI9IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label36" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI9IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                     <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI9DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart8" runat="server" Visible="False" Height="100px" Width="260px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" /></div>
                                </a></div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                       <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-success"  runat="server" id="rpi11Class" href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl11" runat="server" Text='RPI11'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel9" runat="server" Font-Size="Small">
                                <table style="width:95%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                           
                                            <asp:Image ID="imgRPI11Status" runat="server" Height="63px" Width="74px" Visible="False" />
                                           
                                        </td>
                                        <td rowspan="2" colspan="3"><asp:Image ID="imgRPI11Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px" Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI11Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px" Visible="False" OnClick="imgRPI11Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore11" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore11_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label90" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI11TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label91" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI11Status" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label92" runat="server" Text="Iteration Completed"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label93" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI11IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label94" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI11IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label95" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI11IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI11DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart9" runat="server" Visible="False" Height="100px" Width="260px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" ID="Button1" /></div>
                                </a></div>
                    </td>
                    <td colspan="2">
                       <div class="col-md-10 col-xs-12 col-sm-12" style="width: 300px">
                            
                                   
                            <a class="info-tiles tiles-success"  runat="server" id="rpi1Class" href="#">
                                <div class="tiles-heading">
                                    <asp:Label ID="lbl1" runat="server" Text='RPI1'></asp:Label></div>
                                <div class="tiles-body-alt">
                            <asp:Panel ID="Panel10" runat="server" Font-Size="Small">
                                <table style="width:95%;">
                                    <tr>
                                        <td rowspan="2" style="width: 153px">
                                           
                                            <asp:Image ID="imgRPI1Status" runat="server" Height="63px" Width="74px" Visible="False" />
                                           
                                        </td>
                                        <td rowspan="2" colspan="3"><asp:Image ID="imgRPI1Type" runat="server" Height="63px" ImageUrl="~/Images/14522012526989.gif" Width="74px" Visible="False" />
                                            </td>
                                        <td colspan="3">
                                            <asp:ImageButton ID="imgRPI1Result" runat="server" ImageUrl="~/Images/ClassicMushroom.png" Width="40px" Visible="False" OnClick="imgRPI1Result_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;
                                            <asp:ImageButton ID="imgIgnore1" runat="server" Height="30px" ImageUrl="~/Images/a-green-cartoon-pipe-th.png" Width="30px" OnClick="imgIgnore1_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label97" runat="server" Text="Test Name"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                             <Marquee><asp:Label ID="lblRPI1TestName" runat="server" Text=""></asp:Label> </Marquee>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label98" runat="server" Text="Status"></asp:Label>
                                        </td>
                                        <td colspan="6">
                                            <asp:Label ID="lblRPI1Status" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label99" runat="server" Text="Iteration Completed"></asp:Label>
                                        </td>
                                        <td style="width: 10px">
                                            
                                            <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="C"></asp:Label>
                                            
                                        </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI1IterationComp" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label101" runat="server" Text="P"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI1IterationPass" runat="server">0</asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="Label102" runat="server" Text="F"></asp:Label>
                                            </td>
                                        <td style="width: 10px">
                                            <asp:Label ID="lblRPI1IterationFailed" runat="server">0</asp:Label>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7"><marquee><asp:Label ID="lblRPI1DeviceList" runat="server" Text="" Font-Size="XX-Small"></asp:Label></marquee></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Chart ID="Chart10" runat="server" Visible="False" Height="100px" Width="260px">
                                <series>
                                    <asp:Series Name="Series1">
                                    </asp:Series>
                                </series>
                                <chartareas>
                                    <asp:ChartArea Name="ChartArea1">
                                    </asp:ChartArea>
                                </chartareas>
                            </asp:Chart>
                        </div>
                                <div class="tiles-footer"><asp:Button runat="server" Text="More info" ForeColor="White" Width="100%"  BackColor="Transparent" BorderColor="Transparent" ID="Button2" /></div>
                                </a></div>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td colspan="5">
                        <asp:Label ID="Label66" runat="server" Font-Bold="False" Font-Size="10pt" ForeColor="White" Text="Automation Execution Dashboard"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    
    </div>
    </form>
</body>
</html>
