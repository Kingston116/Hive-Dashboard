using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Mail;
using System.Web.Mvc;

namespace HiveDashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //c333187.bgchnet.co.uk/Dashboard
                try
                {
                    #region RPI1
                    DataTable dtDevices1 = new DataTable();
                    DataTable dtScenario1 = new DataTable();
                    DataTable dtKit1 = new DataTable();
                    dtKit1.Columns.Add("Kit");
                    dtKit1.Columns.Add("CurrentStatus");
                    JObject o1;
                    //using (StreamReader file1 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified1 = DateTime.Now;
                    DateTime devLastModified1 = DateTime.Now;
                    var myFile1 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev1 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb1 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists)
                    {
                        var directory1 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\");
                        myFileDev1 = (from f in directory1.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified1 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFileDev1.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists)
                    {
                        var directory1 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\");
                        myFileWeb1 = (from f in directory1.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified1 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFileWeb1.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists))
                    {
                        if (devLastModified1 > webLastModified1)
                        {
                            myFile1 = myFileDev1;
                        }
                        else
                        {
                            myFile1 = myFileWeb1;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists)
                        {
                            myFile1 = myFileWeb1;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists)
                            {
                                myFile1 = myFileDev1;
                            }

                    int PassCounter1 = 0;
                    int FailCounter1 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists)) && (DateTime.Now.Subtract(devLastModified1).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified1).TotalMinutes < 7200))
                    {
                        JObject oJ1 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile1.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file1 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile1.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file1))
                                {
                                    o1 = (JObject)JToken.ReadFrom(reader);
                                    oJ1 = o1;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile1.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi1/" + myFile1.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter1++;
                                    }
                                    else
                                    {
                                        PassCounter1++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFile1.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file1 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFile1.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file1))
                                {
                                    o1 = (JObject)JToken.ReadFrom(reader);
                                    oJ1 = o1;
                                }

                                string ofName = new Delimon.Win32.IO.FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFile1.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = Delimon.Win32.IO.File.ReadAllText("\\\\?\\" + Server.MapPath(@"~/Hardware/Web-Mobile_Test_Automation/Test_Results/rpi1") + "\\" + myFile1.Name + @"\HTML\" + ofName);
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter1++;
                                    }
                                    else
                                    {
                                        PassCounter1++;
                                    }
                                }
                            }
                        }
                        DataTable dt1 = new DataTable();
                        dt1.Columns.Add("Text");
                        using (JsonTextReader reader1 = new JsonTextReader(new StringReader(oJ1.ToString().Replace("\r\n", ""))))
                        {
                            string temp1 = "";
                            string tempKey1 = "";
                            string tempValue1 = "";
                            bool Type1 = false;
                            bool repFlag1 = false;
                            while (reader1.Read())
                            {
                                if (reader1.Value != null)
                                {
                                    dt1.Rows.Add(reader1.Value.ToString());
                                }
                            }
                        }


                        dtDevices1.Columns.Add("Kit");
                        dtDevices1.Columns.Add("DeviceType");
                        dtDevices1.Columns.Add("DeviceVersion");
                        dtDevices1.Columns.Add("Image");
                        string strImagePath1 = "";
                        bool flag1 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario1.Columns.Add("DeviceName");
                        dtScenario1.Columns.Add("ScenarioId");
                        dtScenario1.Columns.Add("ScenarioName");
                        dtScenario1.Columns.Add("ScenarioNo");
                        dtScenario1.Columns.Add("Status");
                        dtScenario1.Columns.Add("TimeStamp");
                        dtScenario1.Columns.Add("Duration");
                        dtScenario1.Columns.Add("FeatureFileName");
                        dtScenario1.Columns.Add("ResultsLink");
                        dtScenario1.Columns.Add("IterationComp");
                        dtScenario1.Columns.Add("IterationPass");
                        dtScenario1.Columns.Add("IterationFail");
                        string strKit1 = "";
                        string strDevicesList1 = "";
                        string strResult1 = "";
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (dt1.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile1.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile1.Name).GetFiles("*.html").First().Name;
                                    strResult1 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi1/" + myFile1.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFile1.Name).GetFiles("*.html").First().Name;
                                    strResult1 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi1/" + myFile1.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit1.Rows.Add(dt1.Rows[i][0].ToString(), dt1.Rows[i + 2][0].ToString());
                                strKit1 = dt1.Rows[i][0].ToString();
                            }
                            if (dt1.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag1 = false;
                                dtScenario1.Rows.Add(strKit1, dt1.Rows[i + 6][0].ToString(), dt1.Rows[i + 10][0].ToString(), dt1.Rows[i][0].ToString(), dt1.Rows[i + 14][0].ToString(), dt1.Rows[i + 8][0].ToString(), dt1.Rows[i + 10][0].ToString(), dt1.Rows[i + 12][0].ToString(), strResult1, dt1.Rows[i + 8][0].ToString(), dt1.Rows[i + 12][0].ToString(), dt1.Rows[i + 4][0].ToString());
                            }
                            if (flag1 == true)
                            {
                                if (dt1.Rows[i][0].ToString().Contains("SLP") || dt1.Rows[i][0].ToString().Contains("SLB") || dt1.Rows[i][0].ToString().Contains("SLR") || dt1.Rows[i][0].ToString().Contains("SLT") || dt1.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt1.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath1 = "~/Images/slr1.png";
                                    }
                                    if (dt1.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath1 = "~/Images/slr1.png";
                                    }
                                    if (dt1.Rows[i][0].ToString().Contains("SLT1"))
                                    {
                                        strImagePath1 = "~/Images/slt1.png";
                                    }
                                    if (dt1.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath1 = "~/Images/slt2.png";
                                    }
                                    if (dt1.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath1 = "~/Images/slb1.png";
                                    }
                                    if (dt1.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath1 = "~/Images/spg2.png";
                                    }
                                    dtDevices1.Rows.Add("", dt1.Rows[i][0].ToString(), dt1.Rows[i + 6][0].ToString(), strImagePath1);
                                    strDevicesList1 += dt1.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt1.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl1.Text = lbl1.Text + " - " + dt1.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario1.Rows[0][1] != null && dtScenario1.Rows[0][10] != null && dtScenario1.Rows[0][1] != null)
                        {
                            lblRPI1IterationComp.Text = dtScenario1.Rows[0][9].ToString();
                            lblRPI1IterationPass.Text = PassCounter1.ToString();
                            lblRPI1IterationFailed.Text = FailCounter1.ToString();
                        }
                        imgRPI1Status.Visible = true;
                        imgRPI1Result.Visible = true;
                        lblRPI1TestName.Text = dtScenario1.Rows[0][2].ToString();
                        if (dtScenario1.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario1.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI1Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI1Type.Visible = true;
                        }
                        else
                        {
                            imgRPI1Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI1Type.Visible = true;
                        }
                        lblRPI1DeviceList.Text = strDevicesList1;
                        lblRPI1Status.Text = dtScenario1.Rows[0][4].ToString();
                        if (dtScenario1.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi1Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI1Status.Width = Unit.Pixel(74);
                            imgRPI1Status.Height = Unit.Pixel(64);
                            imgRPI1Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario1.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi1Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI1Status.Width = Unit.Pixel(74);
                            imgRPI1Status.Height = Unit.Pixel(64);
                            imgRPI1Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario1.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi1Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI1Status.Width = Unit.Pixel(74);
                            imgRPI1Status.Height = Unit.Pixel(64);
                            imgRPI1Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI1Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario1.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi1Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI1Status.Width = Unit.Pixel(64);
                            imgRPI1Status.Height = Unit.Pixel(74);
                            imgRPI1Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario1.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi1Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI1Status.Width = Unit.Pixel(64);
                            imgRPI1Status.Height = Unit.Pixel(74);
                            imgRPI1Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario1.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified1).TotalMinutes > 10)
                            {
                                FileInfo fileRPI1 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr1 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt");
                                    {
                                        String line1 = sr1.ReadToEnd();
                                        if (line1.Contains(myFile1.Name))
                                        {
                                            if (line1.Contains(myFile1.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI1 for the past 10 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr1.Close();
                                                        client.Send(mail);
                                                        FileStream file1 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt", FileMode.Append);
                                                        file1.Flush();
                                                        StreamWriter str1 = new StreamWriter(file1);
                                                        str1.Write(myFile1.Name + ";sent");
                                                        str1.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr1.Close();
                                                using (StreamWriter outputFile1 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt"))
                                                {
                                                    outputFile1.Flush();
                                                    outputFile1.Write(myFile1.Name);
                                                    outputFile1.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi1Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI1Status.Width = Unit.Pixel(74);
                                imgRPI1Status.Height = Unit.Pixel(64);
                                imgRPI1Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI1Status.Text = "No Response";
                            }
                        }
                        if (dtScenario1.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified1).TotalMinutes > 10)
                            {
                                FileInfo fileRPI1 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr1 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt");
                                    {
                                        String line1 = sr1.ReadToEnd();
                                        if (line1.Contains(myFile1.Name))
                                        {
                                            if (line1.Contains(myFile1.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI1 for the past 10 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr1.Close();
                                                        client.Send(mail);
                                                        FileStream file1 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt", FileMode.Append);
                                                        file1.Flush();
                                                        StreamWriter str1 = new StreamWriter(file1);
                                                        str1.Write(myFile1.Name + ";sent");
                                                        str1.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr1.Close();
                                                using (StreamWriter outputFile1 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI1.txt"))
                                                {
                                                    outputFile1.Flush();
                                                    outputFile1.Write(myFile1.Name);
                                                    outputFile1.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi1Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI1Status.Width = Unit.Pixel(74);
                                imgRPI1Status.Height = Unit.Pixel(64);
                                imgRPI1Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI1Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi1Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI1Status.Width = Unit.Pixel(153);
                        imgRPI1Status.Height = Unit.Pixel(68);
                        imgRPI1Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI1Status.Visible = true;
                    }

                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario1"] = dtScenario1;
                    #endregion
                }
                catch
                {
                    rpi1Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI1Status.Width = Unit.Pixel(153);
                    imgRPI1Status.Height = Unit.Pixel(68);
                    imgRPI1Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI1Status.Visible = true;
                }
                try
                {
                    #region RPI2
                    DataTable dtDevices = new DataTable();
                    DataTable dtScenario = new DataTable();
                    DataTable dtKit = new DataTable();
                    dtKit.Columns.Add("Kit");
                    dtKit.Columns.Add("CurrentStatus");
                    JObject o2;
                    //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified = DateTime.Now;
                    DateTime devLastModified = DateTime.Now;
                    var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists)
                    {
                        var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\");
                        myFileDev = (from f in directory.GetDirectories()
                                     orderby f.LastWriteTime descending
                                     select f).First();
                        devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFileDev.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists)
                    {
                        var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\");
                        myFileWeb = (from f in directory.GetDirectories()
                                     orderby f.LastWriteTime descending
                                     select f).First();
                        webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFileWeb.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists))
                    {
                        if (devLastModified > webLastModified)
                        {
                            myFile = myFileDev;
                        }
                        else
                        {
                            myFile = myFileWeb;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists)
                        {
                            myFile = myFileWeb;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists)
                            {
                                myFile = myFileDev;
                            }
                    int PassCounter = 0;
                    int FailCounter = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
                    {
                        JObject oJ2 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file))
                                {
                                    o2 = (JObject)JToken.ReadFrom(reader);
                                    oJ2 = o2;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi2/" + myFile.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter++;
                                    }
                                    else
                                    {
                                        PassCounter++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file))
                                {
                                    o2 = (JObject)JToken.ReadFrom(reader);
                                    oJ2 = o2;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi2/" + myFile.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter++;
                                    }
                                    else
                                    {
                                        PassCounter++;
                                    }
                                }
                            }
                        }
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Text");
                        using (JsonTextReader reader = new JsonTextReader(new StringReader(oJ2.ToString().Replace("\r\n", ""))))
                        {
                            string temp = "";
                            string tempKey = "";
                            string tempValue = "";
                            bool Type = false;
                            bool repFlag = false;
                            while (reader.Read())
                            {
                                if (reader.Value != null)
                                {
                                    dt.Rows.Add(reader.Value.ToString());
                                }
                            }
                        }


                        dtDevices.Columns.Add("Kit");
                        dtDevices.Columns.Add("DeviceType");
                        dtDevices.Columns.Add("DeviceVersion");
                        dtDevices.Columns.Add("Image");
                        string strImagePath = "";
                        bool flag = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario.Columns.Add("DeviceName");
                        dtScenario.Columns.Add("ScenarioId");
                        dtScenario.Columns.Add("ScenarioName");
                        dtScenario.Columns.Add("ScenarioNo");
                        dtScenario.Columns.Add("Status");
                        dtScenario.Columns.Add("TimeStamp");
                        dtScenario.Columns.Add("Duration");
                        dtScenario.Columns.Add("FeatureFileName");
                        dtScenario.Columns.Add("ResultsLink");
                        dtScenario.Columns.Add("IterationComp");
                        dtScenario.Columns.Add("IterationPass");
                        dtScenario.Columns.Add("IterationFail");
                        string strKit = "";
                        string strDevicesList = "";
                        string strResult = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name).GetFiles("*.html").First().Name;
                                    strResult = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi2/" + myFile.Name + "/" + strHTML;
                                    // strResult = Server.MapPath("~/hardware/Device_Test_Automation/Test_Results/rpi2/" + myFile.Name + "/" + strHTML);
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFile.Name).GetFiles("*.html").First().Name;
                                    strResult = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi2/" + myFile.Name + "/" + strHTML;
                                    //strResult = Server.MapPath("~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi2/" + myFile.Name + "/" + strHTML);
                                }
                            }
                        }
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i + 2][0].ToString());
                                strKit = dt.Rows[i][0].ToString();
                            }
                            if (dt.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag = false;
                                dtScenario.Rows.Add(strKit, dt.Rows[i + 6][0].ToString(), dt.Rows[i + 10][0].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i + 14][0].ToString(), dt.Rows[i + 8][0].ToString(), dt.Rows[i + 10][0].ToString(), dt.Rows[i + 12][0].ToString(), strResult, dt.Rows[i + 8][0].ToString(), dt.Rows[i + 12][0].ToString(), dt.Rows[i + 4][0].ToString());
                            }
                            if (flag == true)
                            {
                                if (dt.Rows[i][0].ToString().Contains("SLP") || dt.Rows[i][0].ToString().Contains("SLB") || dt.Rows[i][0].ToString().Contains("SLR") || dt.Rows[i][0].ToString().Contains("SLT") || dt.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath = "~/Images/slr1.png";
                                    }
                                    if (dt.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath = "~/Images/slr1.png";
                                    }
                                    if (dt.Rows[i][0].ToString().Contains("SLT"))
                                    {
                                        strImagePath = "~/Images/slt.png";
                                    }
                                    if (dt.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath = "~/Images/slt2.png";
                                    }
                                    if (dt.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath = "~/Images/slb1.png";
                                    }
                                    if (dt.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath = "~/Images/spg2.png";
                                    }
                                    dtDevices.Rows.Add("", dt.Rows[i][0].ToString(), dt.Rows[i + 6][0].ToString(), strImagePath);
                                    strDevicesList += dt.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl2.Text = lbl2.Text + " - " + dt.Rows[i + 1][0].ToString();
                            }
                            if (dt.Rows[i][0].ToString().Contains("test_start_time_stamp"))
                            {
                                lblStart2.Text = dt.Rows[i + 1][0].ToString();
                            }
                            if (dt.Rows[i][0].ToString().Contains("update_time_stamp"))
                            {
                                lblLastUpdate2.Text = dt.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario.Rows[0][9] != null && dtScenario.Rows[0][10] != null && dtScenario.Rows[0][11] != null)
                        {
                            lblRPI2IterationComp.Text = dtScenario.Rows[0][9].ToString();
                            lblRPI2IterationPass.Text = PassCounter.ToString();
                            lblRPI2IterationFailed.Text = FailCounter.ToString();
                            DataTable dtPi2 = new DataTable();
                            dtPi2.Columns.Add("Status");
                            dtPi2.Columns.Add("Count");
                            dtPi2.Rows.Add("Passed", PassCounter);
                            dtPi2.Rows.Add("Failed", FailCounter);
                            foreach (DataRow row in dtPi2.Rows)
                            {
                                piChart2.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                                {
                                    Category = row["Status"].ToString(),
                                    Data = Convert.ToInt32(row["Count"])
                                });
                            }
                        }
                        lblRPI2TestName.Text = dtScenario.Rows[0][2].ToString();
                        Session["resRPI2"] = dtScenario.Rows[0][8].ToString();
                        imgRPI2Status.Visible = true;
                        imgRPI2Result.Visible = true;
                        //imgRPI2Result.OnClientClick = String.Format("window.open({0});return false;", dtScenario.Rows[0][8].ToString());
                        if (dtScenario.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI2Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI2Type.Visible = true;
                        }
                        else
                        {
                            imgRPI2Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI2Type.Visible = true;
                        }
                        lblRPI2DeviceList.Text = strDevicesList;
                        lblRPI2Status.Text = dtScenario.Rows[0][4].ToString();
                        if (dtScenario.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi2Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI2Status.Width = Unit.Pixel(74);
                            imgRPI2Status.Height = Unit.Pixel(64);
                            imgRPI2Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi2Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI2Status.Width = Unit.Pixel(74);
                            imgRPI2Status.Height = Unit.Pixel(64);
                            imgRPI2Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi2Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI2Status.Width = Unit.Pixel(74);
                            imgRPI2Status.Height = Unit.Pixel(64);
                            imgRPI2Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI2Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi2Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI2Status.Width = Unit.Pixel(53);
                            imgRPI2Status.Height = Unit.Pixel(73);
                            imgRPI2Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi2Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI2Status.Width = Unit.Pixel(53);
                            imgRPI2Status.Height = Unit.Pixel(73);
                            imgRPI2Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified).TotalMinutes > 90)
                            {
                                FileInfo fileRPI2 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt");
                                    {
                                        String line = sr.ReadToEnd();
                                        if (line.Contains(myFile.Name))
                                        {
                                            if (line.Contains(myFile.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI2 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        client.Send(mail);
                                                        sr.Close();
                                                        FileStream file2 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt", FileMode.Append);
                                                        file2.Flush();
                                                        StreamWriter str2 = new StreamWriter(file2);
                                                        str2.Write(myFile.Name + ";sent");
                                                        str2.Close();
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr.Close();
                                                using (StreamWriter outputFile = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt", true))
                                                {

                                                    outputFile.Flush();
                                                    outputFile.Write(myFile.Name);
                                                    outputFile.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }

                                rpi2Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI2Status.Width = Unit.Pixel(74);
                                imgRPI2Status.Height = Unit.Pixel(64);
                                imgRPI2Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI2Status.Text = "No Response";
                            }
                        }
                        if (dtScenario.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified).TotalMinutes > 90)
                            {
                                FileInfo fileRPI2 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt");
                                    {
                                        String line = sr.ReadToEnd();
                                        if (line.Contains(myFile.Name))
                                        {
                                            if (line.Contains(myFile.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI2 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        client.Send(mail);
                                                        sr.Close();
                                                        FileStream file2 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt", FileMode.Append);
                                                        file2.Flush();
                                                        StreamWriter str2 = new StreamWriter(file2);
                                                        str2.Write(myFile.Name + ";sent");
                                                        str2.Close();
                                                    }
                                                    catch
                                                    {
                                                    }
                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr.Close();
                                                using (StreamWriter outputFile = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI2.txt"))
                                                {
                                                    outputFile.Flush();
                                                    outputFile.Write(myFile.Name);
                                                    outputFile.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {

                                }

                                rpi2Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI2Status.Width = Unit.Pixel(74);
                                imgRPI2Status.Height = Unit.Pixel(64);
                                imgRPI2Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI2Status.Text = "No Response";
                            }
                        }

                    }
                    else
                    {

                        rpi2Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI2Status.Width = Unit.Pixel(153);
                        imgRPI2Status.Height = Unit.Pixel(68);
                        imgRPI2Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI2Status.Visible = true;
                        Panel11.Visible = false;
                    }
                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario2"] = dtScenario;
                    #endregion
                }
                catch
                {
                    rpi2Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI2Status.Width = Unit.Pixel(153);
                    imgRPI2Status.Height = Unit.Pixel(68);
                    imgRPI2Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI2Status.Visible = true;
                    Panel11.Visible = false;
                }
                try
                {
                    #region RPI3
                    DataTable dtDevices3 = new DataTable();
                    DataTable dtScenario3 = new DataTable();
                    DataTable dtKit3 = new DataTable();
                    dtKit3.Columns.Add("Kit");
                    dtKit3.Columns.Add("CurrentStatus");
                    JObject o3;
                    //using (StreamReader file3 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified3 = DateTime.Now;
                    DateTime devLastModified3 = DateTime.Now;
                    var myFile3 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev3 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb3 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists)
                    {
                        var directory3 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\");
                        myFileDev3 = (from f in directory3.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified3 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFileDev3.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists)
                    {
                        var directory3 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\");
                        myFileWeb3 = (from f in directory3.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified3 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFileWeb3.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists))
                    {
                        if (devLastModified3 > webLastModified3)
                        {
                            myFile3 = myFileDev3;
                        }
                        else
                        {
                            myFile3 = myFileWeb3;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists)
                        {
                            myFile3 = myFileWeb3;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists)
                            {
                                myFile3 = myFileDev3;
                            }
                    int PassCounter3 = 0;
                    int FailCounter3 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists)) && (DateTime.Now.Subtract(devLastModified3).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified3).TotalMinutes < 7200))
                    {
                        JObject oJ3 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile3.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file3 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile3.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file3))
                                {
                                    o3 = (JObject)JToken.ReadFrom(reader);
                                    oJ3 = o3;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile3.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi3/" + myFile3.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter3++;
                                    }
                                    else
                                    {
                                        PassCounter3++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFile3.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file3 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFile3.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file3))
                                {
                                    o3 = (JObject)JToken.ReadFrom(reader);
                                    oJ3 = o3;
                                }
                            }
                            string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFile3.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                            string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi3/" + myFile3.Name + @"/HTML/" + ofName));
                            int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                            for (int i = 1; i < Counter; i++)
                            {
                                if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                {
                                    FailCounter3++;
                                }
                                else
                                {
                                    PassCounter3++;
                                }
                            }
                        }
                        DataTable dt3 = new DataTable();
                        dt3.Columns.Add("Text");
                        using (JsonTextReader reader3 = new JsonTextReader(new StringReader(oJ3.ToString().Replace("\r\n", ""))))
                        {
                            string temp3 = "";
                            string tempKey3 = "";
                            string tempValue3 = "";
                            bool Type3 = false;
                            bool repFlag3 = false;
                            while (reader3.Read())
                            {
                                if (reader3.Value != null)
                                {
                                    dt3.Rows.Add(reader3.Value.ToString());
                                }
                            }
                        }


                        dtDevices3.Columns.Add("Kit");
                        dtDevices3.Columns.Add("DeviceType");
                        dtDevices3.Columns.Add("DeviceVersion");
                        dtDevices3.Columns.Add("Image");
                        string strImagePath3 = "";
                        bool flag3 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario3.Columns.Add("DeviceName");
                        dtScenario3.Columns.Add("ScenarioId");
                        dtScenario3.Columns.Add("ScenarioName");
                        dtScenario3.Columns.Add("ScenarioNo");
                        dtScenario3.Columns.Add("Status");
                        dtScenario3.Columns.Add("TimeStamp");
                        dtScenario3.Columns.Add("Duration");
                        dtScenario3.Columns.Add("FeatureFileName");
                        dtScenario3.Columns.Add("ResultsLink");
                        dtScenario3.Columns.Add("IterationComp");
                        dtScenario3.Columns.Add("IterationPass");
                        dtScenario3.Columns.Add("IterationFail");
                        string strKit3 = "";
                        string strDevicesList3 = "";
                        string strResult3 = "";
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (dt3.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile3.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile3.Name).GetFiles("*.html").First().Name;
                                    strResult3 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi3/" + myFile3.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFile3.Name).GetFiles("*.html").First().Name;
                                    strResult3 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi3/" + myFile3.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt3.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit3.Rows.Add(dt3.Rows[i][0].ToString(), dt3.Rows[i + 2][0].ToString());
                                strKit3 = dt3.Rows[i][0].ToString();
                            }
                            if (dt3.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag3 = false;
                                dtScenario3.Rows.Add(strKit3, dt3.Rows[i + 6][0].ToString(), dt3.Rows[i + 10][0].ToString(), dt3.Rows[i][0].ToString(), dt3.Rows[i + 14][0].ToString(), dt3.Rows[i + 8][0].ToString(), dt3.Rows[i + 10][0].ToString(), dt3.Rows[i + 12][0].ToString(), strResult3, dt3.Rows[i + 8][0].ToString(), dt3.Rows[i + 12][0].ToString(), dt3.Rows[i + 4][0].ToString());
                            }
                            if (flag3 == true)
                            {
                                if (dt3.Rows[i][0].ToString().Contains("SLP") || dt3.Rows[i][0].ToString().Contains("SLB") || dt3.Rows[i][0].ToString().Contains("SLR") || dt3.Rows[i][0].ToString().Contains("SLT") || dt3.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt3.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath3 = "~/Images/slr1.png";
                                    }
                                    if (dt3.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath3 = "~/Images/slr1.png";
                                    }
                                    if (dt3.Rows[i][0].ToString().Contains("SLT3"))
                                    {
                                        strImagePath3 = "~/Images/slt3.png";
                                    }
                                    if (dt3.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath3 = "~/Images/slt2.png";
                                    }
                                    if (dt3.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath3 = "~/Images/slb1.png";
                                    }
                                    if (dt3.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath3 = "~/Images/spg2.png";
                                    }
                                    dtDevices3.Rows.Add("", dt3.Rows[i][0].ToString(), dt3.Rows[i + 6][0].ToString(), strImagePath3);
                                    strDevicesList3 += dt3.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt3.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl3.Text = lbl3.Text + " - " + dt3.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario3.Rows[0][9] != null && dtScenario3.Rows[0][10] != null && dtScenario3.Rows[0][11] != null)
                        {
                            lblRPI3IterationComp.Text = dtScenario3.Rows[0][9].ToString();
                            lblRPI3IterationPass.Text = PassCounter3.ToString();
                            lblRPI3IterationFailed.Text = FailCounter3.ToString();
                        }
                        imgRPI3Status.Visible = true;
                        imgRPI3Result.Visible = true;
                        lblRPI3TestName.Text = dtScenario3.Rows[0][2].ToString();
                        if (dtScenario3.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario3.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI3Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI3Type.Visible = true;
                        }
                        else
                        {
                            imgRPI3Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI3Type.Visible = true;
                        }
                        lblRPI3DeviceList.Text = strDevicesList3;
                        lblRPI3Status.Text = dtScenario3.Rows[0][4].ToString();
                        if (dtScenario3.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi3Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI3Status.Width = Unit.Pixel(74);
                            imgRPI3Status.Height = Unit.Pixel(64);
                            imgRPI3Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario3.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi3Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI3Status.Width = Unit.Pixel(74);
                            imgRPI3Status.Height = Unit.Pixel(64);
                            imgRPI3Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario3.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi3Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI3Status.Width = Unit.Pixel(74);
                            imgRPI3Status.Height = Unit.Pixel(64);
                            imgRPI3Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI3Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario3.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi3Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI3Status.Width = Unit.Pixel(53);
                            imgRPI3Status.Height = Unit.Pixel(73);
                            imgRPI3Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario3.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi3Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI3Status.Width = Unit.Pixel(53);
                            imgRPI3Status.Height = Unit.Pixel(73);
                            imgRPI3Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario3.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified3).TotalMinutes > 90)
                            {
                                FileInfo fileRPI3 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr3 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt");
                                    {
                                        String line3 = sr3.ReadToEnd();
                                        if (line3.Contains(myFile3.Name))
                                        {
                                            if (line3.Contains(myFile3.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI3 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr3.Close();
                                                        client.Send(mail);

                                                        FileStream file3 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt", FileMode.Append);
                                                        file3.Flush();
                                                        StreamWriter str3 = new StreamWriter(file3);
                                                        str3.Write(myFile3.Name + ";sent");
                                                        str3.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr3.Close();
                                                using (StreamWriter outputFile3 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt"))
                                                {
                                                    outputFile3.Flush();
                                                    outputFile3.Write(myFile3.Name);
                                                    outputFile3.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                rpi3Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI3Status.Width = Unit.Pixel(74);
                                imgRPI3Status.Height = Unit.Pixel(64);
                                imgRPI3Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI3Status.Text = "No Response";
                            }
                        }
                        if (dtScenario3.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified3).TotalMinutes > 90)
                            {
                                FileInfo fileRPI3 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr3 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt");
                                    {
                                        String line3 = sr3.ReadToEnd();
                                        if (line3.Contains(myFile3.Name))
                                        {
                                            if (line3.Contains(myFile3.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI3 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr3.Close();
                                                        client.Send(mail);
                                                        FileStream file3 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt", FileMode.Append);
                                                        file3.Flush();
                                                        StreamWriter str3 = new StreamWriter(file3);
                                                        str3.Write(myFile3.Name + ";sent");
                                                        str3.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr3.Close();
                                                using (StreamWriter outputFile3 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI3.txt"))
                                                {
                                                    outputFile3.Flush();
                                                    outputFile3.Write(myFile3.Name);
                                                    outputFile3.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                rpi3Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI3Status.Width = Unit.Pixel(74);
                                imgRPI3Status.Height = Unit.Pixel(64);
                                imgRPI3Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI3Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi3Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI3Status.Width = Unit.Pixel(153);
                        imgRPI3Status.Height = Unit.Pixel(68);
                        imgRPI3Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI3Status.Visible = true;
                    }
                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario3"] = dtScenario3;
                    #endregion
                }
                catch
                {
                    rpi3Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI3Status.Width = Unit.Pixel(153);
                    imgRPI3Status.Height = Unit.Pixel(68);
                    imgRPI3Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI3Status.Visible = true;
                }
                try
                {
                    #region RPI4
                    DataTable dtDevices4 = new DataTable();
                    DataTable dtScenario4 = new DataTable();
                    DataTable dtKit4 = new DataTable();
                    dtKit4.Columns.Add("Kit");
                    dtKit4.Columns.Add("CurrentStatus");
                    JObject o4;
                    //using (StreamReader file4 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified4 = DateTime.Now;
                    DateTime devLastModified4 = DateTime.Now;
                    var myFile4 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev4 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb4 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists)
                    {
                        var directory4 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\");
                        myFileDev4 = (from f in directory4.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified4 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFileDev4.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists)
                    {
                        var directory4 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\");
                        myFileWeb4 = (from f in directory4.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified4 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFileWeb4.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists))
                    {
                        if (devLastModified4 > webLastModified4)
                        {
                            myFile4 = myFileDev4;
                        }
                        else
                        {
                            myFile4 = myFileWeb4;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists)
                        {
                            myFile4 = myFileWeb4;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists)
                            {
                                myFile4 = myFileDev4;
                            }
                    int PassCounter4 = 0;
                    int FailCounter4 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists)) && (DateTime.Now.Subtract(devLastModified4).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified4).TotalMinutes < 7200))
                    {
                        JObject oJ4 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile4.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file4 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile4.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file4))
                                {
                                    o4 = (JObject)JToken.ReadFrom(reader);
                                    oJ4 = o4;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile4.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi4/" + myFile4.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter4++;
                                    }
                                    else
                                    {
                                        PassCounter4++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFile4.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file4 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFile4.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file4))
                                {
                                    o4 = (JObject)JToken.ReadFrom(reader);
                                    oJ4 = o4;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFile4.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi4/" + myFile4.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter4++;
                                    }
                                    else
                                    {
                                        PassCounter4++;
                                    }
                                }
                            }
                        }
                        DataTable dt4 = new DataTable();
                        dt4.Columns.Add("Text");
                        using (JsonTextReader reader4 = new JsonTextReader(new StringReader(oJ4.ToString().Replace("\r\n", ""))))
                        {
                            string temp4 = "";
                            string tempKey4 = "";
                            string tempValue4 = "";
                            bool Type4 = false;
                            bool repFlag4 = false;
                            while (reader4.Read())
                            {
                                if (reader4.Value != null)
                                {
                                    dt4.Rows.Add(reader4.Value.ToString());
                                }
                            }
                        }


                        dtDevices4.Columns.Add("Kit");
                        dtDevices4.Columns.Add("DeviceType");
                        dtDevices4.Columns.Add("DeviceVersion");
                        dtDevices4.Columns.Add("Image");
                        string strImagePath4 = "";
                        bool flag4 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario4.Columns.Add("DeviceName");
                        dtScenario4.Columns.Add("ScenarioId");
                        dtScenario4.Columns.Add("ScenarioName");
                        dtScenario4.Columns.Add("ScenarioNo");
                        dtScenario4.Columns.Add("Status");
                        dtScenario4.Columns.Add("TimeStamp");
                        dtScenario4.Columns.Add("Duration");
                        dtScenario4.Columns.Add("FeatureFileName");
                        dtScenario4.Columns.Add("ResultsLink");
                        dtScenario4.Columns.Add("IterationComp");
                        dtScenario4.Columns.Add("IterationPass");
                        dtScenario4.Columns.Add("IterationFail");
                        string strKit4 = "";
                        string strDevicesList4 = "";
                        string strResult4 = "";
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            if (dt4.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile4.Name + @"\results.json").Exists)
                                {
                                    string strHTML4 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile4.Name).GetFiles("*.html").First().Name;
                                    strResult4 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi4/" + myFile4.Name + "/" + strHTML4;
                                }
                                else
                                {
                                    string strHTML4 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFile4.Name).GetFiles("*.html").First().Name;
                                    strResult4 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi4/" + myFile4.Name + "/" + strHTML4;
                                }
                            }
                        }
                        for (int i = 0; i < dt4.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit4.Rows.Add(dt4.Rows[i][0].ToString(), dt4.Rows[i + 2][0].ToString());
                                strKit4 = dt4.Rows[i][0].ToString();
                            }
                            if (dt4.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag4 = false;
                                dtScenario4.Rows.Add(strKit4, dt4.Rows[i + 6][0].ToString(), dt4.Rows[i + 10][0].ToString(), dt4.Rows[i][0].ToString(), dt4.Rows[i + 14][0].ToString(), dt4.Rows[i + 8][0].ToString(), dt4.Rows[i + 10][0].ToString(), dt4.Rows[i + 12][0].ToString(), strResult4, dt4.Rows[i + 8][0].ToString(), dt4.Rows[i + 12][0].ToString(), dt4.Rows[i + 4][0].ToString());
                            }
                            if (flag4 == true)
                            {
                                if (dt4.Rows[i][0].ToString().Contains("SLP") || dt4.Rows[i][0].ToString().Contains("SLB") || dt4.Rows[i][0].ToString().Contains("SLR") || dt4.Rows[i][0].ToString().Contains("SLT") || dt4.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt4.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath4 = "~/Images/slr1.png";
                                    }
                                    if (dt4.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath4 = "~/Images/slr1.png";
                                    }
                                    if (dt4.Rows[i][0].ToString().Contains("SLT4"))
                                    {
                                        strImagePath4 = "~/Images/slt4.png";
                                    }
                                    if (dt4.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath4 = "~/Images/slt2.png";
                                    }
                                    if (dt4.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath4 = "~/Images/slb1.png";
                                    }
                                    if (dt4.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath4 = "~/Images/spg2.png";
                                    }
                                    dtDevices4.Rows.Add("", dt4.Rows[i][0].ToString(), dt4.Rows[i + 6][0].ToString(), strImagePath4);
                                    strDevicesList4 += dt4.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt4.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl4.Text = lbl4.Text + " - " + dt4.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario4.Rows[0][9] != null && dtScenario4.Rows[0][10] != null && dtScenario4.Rows[0][11] != null)
                        {
                            lblRPI4IterationComp.Text = dtScenario4.Rows[0][9].ToString();
                            lblRPI4IterationPass.Text = PassCounter4.ToString();
                            lblRPI4IterationFailed.Text = FailCounter4.ToString();
                        }
                        imgRPI4Status.Visible = true;
                        imgRPI4Result.Visible = true;
                        lblRPI4TestName.Text = dtScenario4.Rows[0][2].ToString();
                        if (dtScenario4.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario4.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI4Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI4Type.Visible = true;
                        }
                        else
                        {
                            imgRPI4Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI4Type.Visible = true;
                        }
                        lblRPI4DeviceList.Text = strDevicesList4;
                        lblRPI4Status.Text = dtScenario4.Rows[0][4].ToString();
                        if (dtScenario4.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi4Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI4Status.Width = Unit.Pixel(74);
                            imgRPI4Status.Height = Unit.Pixel(64);
                            imgRPI4Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario4.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi4Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI4Status.Width = Unit.Pixel(74);
                            imgRPI4Status.Height = Unit.Pixel(64);
                            imgRPI4Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario4.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi4Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI4Status.Width = Unit.Pixel(74);
                            imgRPI4Status.Height = Unit.Pixel(64);
                            imgRPI4Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI4Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario4.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi4Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI4Status.Width = Unit.Pixel(53);
                            imgRPI4Status.Height = Unit.Pixel(73);
                            imgRPI4Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario4.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi4Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI4Status.Width = Unit.Pixel(53);
                            imgRPI4Status.Height = Unit.Pixel(73);
                            imgRPI4Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario4.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified4).TotalMinutes > 90)
                            {
                                FileInfo fileRPI4 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr4 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt");
                                    {
                                        String line4 = sr4.ReadToEnd();
                                        if (line4.Contains(myFile4.Name))
                                        {
                                            if (line4.Contains(myFile4.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI4 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr4.Close();
                                                        client.Send(mail);
                                                        FileStream file4 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt", FileMode.Append);
                                                        file4.Flush();
                                                        StreamWriter str4 = new StreamWriter(file4);
                                                        str4.Write(myFile4.Name + ";sent");
                                                        str4.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr4.Close();
                                                using (StreamWriter outputFile4 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt"))
                                                {
                                                    outputFile4.Flush();
                                                    outputFile4.Write(myFile4.Name);
                                                    outputFile4.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi4Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI4Status.Width = Unit.Pixel(74);
                                imgRPI4Status.Height = Unit.Pixel(64);
                                imgRPI4Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI4Status.Text = "No Response";
                            }
                        }
                        if (dtScenario4.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified4).TotalMinutes > 90)
                            {
                                FileInfo fileRPI4 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr4 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt");
                                    {
                                        String line4 = sr4.ReadToEnd();
                                        if (line4.Contains(myFile4.Name))
                                        {
                                            if (line4.Contains(myFile4.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI4 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr4.Close();
                                                        client.Send(mail);
                                                        FileStream file4 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt", FileMode.Append);
                                                        file4.Flush();
                                                        StreamWriter str4 = new StreamWriter(file4);
                                                        str4.Write(myFile4.Name + ";sent");
                                                        str4.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr4.Close();
                                                using (StreamWriter outputFile4 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI4.txt"))
                                                {
                                                    outputFile4.Flush();
                                                    outputFile4.Write(myFile4.Name);
                                                    outputFile4.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi4Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI4Status.Width = Unit.Pixel(74);
                                imgRPI4Status.Height = Unit.Pixel(64);
                                imgRPI4Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI4Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi4Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI4Status.Width = Unit.Pixel(153);
                        imgRPI4Status.Height = Unit.Pixel(68);
                        imgRPI4Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI4Status.Visible = true;
                    }
                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario4"] = dtScenario4;
                    #endregion
                }
                catch
                {
                    rpi4Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI4Status.Width = Unit.Pixel(153);
                    imgRPI4Status.Height = Unit.Pixel(68);
                    imgRPI4Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI4Status.Visible = true;
                }
                try
                {
                    #region RPI5
                    DataTable dtDevices5 = new DataTable();
                    DataTable dtScenario5 = new DataTable();
                    DataTable dtKit5 = new DataTable();
                    dtKit5.Columns.Add("Kit");
                    dtKit5.Columns.Add("CurrentStatus");
                    JObject o5;
                    //using (StreamReader file5 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified5 = DateTime.Now;
                    DateTime devLastModified5 = DateTime.Now;
                    var myFile5 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev5 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb5 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists)
                    {
                        var directory5 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\");
                        myFileDev5 = (from f in directory5.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified5 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFileDev5.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists)
                    {
                        var directory5 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\");
                        myFileWeb5 = (from f in directory5.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified5 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFileWeb5.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists))
                    {
                        if (devLastModified5 > webLastModified5)
                        {
                            myFile5 = myFileDev5;
                        }
                        else
                        {
                            myFile5 = myFileWeb5;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists)
                        {
                            myFile5 = myFileWeb5;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists)
                            {
                                myFile5 = myFileDev5;
                            }
                    int PassCounter5 = 0;
                    int FailCounter5 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists)) && (DateTime.Now.Subtract(devLastModified5).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified5).TotalMinutes < 7200))
                    {
                        JObject oJ5 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile5.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file5 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile5.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file5))
                                {
                                    o5 = (JObject)JToken.ReadFrom(reader);
                                    oJ5 = o5;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile5.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi5/" + myFile5.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter5++;
                                    }
                                    else
                                    {
                                        PassCounter5++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFile5.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file5 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFile5.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file5))
                                {
                                    o5 = (JObject)JToken.ReadFrom(reader);
                                    oJ5 = o5;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFile5.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi5/" + myFile5.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter5++;
                                    }
                                    else
                                    {
                                        PassCounter5++;
                                    }
                                }
                            }
                        }
                        DataTable dt5 = new DataTable();
                        dt5.Columns.Add("Text");
                        using (JsonTextReader reader5 = new JsonTextReader(new StringReader(oJ5.ToString().Replace("\r\n", ""))))
                        {
                            string temp5 = "";
                            string tempKey5 = "";
                            string tempValue5 = "";
                            bool Type5 = false;
                            bool repFlag5 = false;
                            while (reader5.Read())
                            {
                                if (reader5.Value != null)
                                {
                                    dt5.Rows.Add(reader5.Value.ToString());
                                }
                            }
                        }


                        dtDevices5.Columns.Add("Kit");
                        dtDevices5.Columns.Add("DeviceType");
                        dtDevices5.Columns.Add("DeviceVersion");
                        dtDevices5.Columns.Add("Image");
                        string strImagePath5 = "";
                        bool flag5 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario5.Columns.Add("DeviceName");
                        dtScenario5.Columns.Add("ScenarioId");
                        dtScenario5.Columns.Add("ScenarioName");
                        dtScenario5.Columns.Add("ScenarioNo");
                        dtScenario5.Columns.Add("Status");
                        dtScenario5.Columns.Add("TimeStamp");
                        dtScenario5.Columns.Add("Duration");
                        dtScenario5.Columns.Add("FeatureFileName");
                        dtScenario5.Columns.Add("ResultsLink");
                        dtScenario5.Columns.Add("IterationComp");
                        dtScenario5.Columns.Add("IterationPass");
                        dtScenario5.Columns.Add("IterationFail");
                        string strKit5 = "";
                        string strDevicesList5 = "";
                        string strResult5 = "";
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            if (dt5.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile5.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile5.Name).GetFiles("*.html").First().Name;
                                    strResult5 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi5/" + myFile5.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFile5.Name).GetFiles("*.html").First().Name;
                                    strResult5 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi5/" + myFile5.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit5.Rows.Add(dt5.Rows[i][0].ToString(), dt5.Rows[i + 2][0].ToString());
                                strKit5 = dt5.Rows[i][0].ToString();
                            }
                            if (dt5.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag5 = false;

                                dtScenario5.Rows.Add(strKit5, dt5.Rows[i + 6][0].ToString(), dt5.Rows[i + 10][0].ToString(), dt5.Rows[i][0].ToString(), dt5.Rows[i + 14][0].ToString(), dt5.Rows[i + 8][0].ToString(), dt5.Rows[i + 10][0].ToString(), dt5.Rows[i + 12][0].ToString(), strResult5, dt5.Rows[i + 8][0].ToString(), dt5.Rows[i + 12][0].ToString(), dt5.Rows[i + 4][0].ToString());
                            }
                            if (flag5 == true)
                            {
                                if (dt5.Rows[i][0].ToString().Contains("SLP") || dt5.Rows[i][0].ToString().Contains("SLB") || dt5.Rows[i][0].ToString().Contains("SLR") || dt5.Rows[i][0].ToString().Contains("SLT") || dt5.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt5.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath5 = "~/Images/slr1.png";
                                    }
                                    if (dt5.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath5 = "~/Images/slr1.png";
                                    }
                                    if (dt5.Rows[i][0].ToString().Contains("SLT5"))
                                    {
                                        strImagePath5 = "~/Images/slt5.png";
                                    }
                                    if (dt5.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath5 = "~/Images/slt2.png";
                                    }
                                    if (dt5.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath5 = "~/Images/slb1.png";
                                    }
                                    if (dt5.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath5 = "~/Images/spg2.png";
                                    }
                                    dtDevices5.Rows.Add("", dt5.Rows[i][0].ToString(), dt5.Rows[i + 6][0].ToString(), strImagePath5);
                                    strDevicesList5 += dt5.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt5.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl5.Text = lbl5.Text + " - " + dt5.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario5.Rows[0][9] != null && dtScenario5.Rows[0][10] != null && dtScenario5.Rows[0][11] != null)
                        {
                            lblRPI5IterationComp.Text = dtScenario5.Rows[0][9].ToString();
                            lblRPI5IterationPass.Text = PassCounter5.ToString();
                            lblRPI5IterationFailed.Text = FailCounter5.ToString();
                        }
                        imgRPI5Status.Visible = true;
                        imgRPI5Result.Visible = true;
                        lblRPI5TestName.Text = dtScenario5.Rows[0][2].ToString();
                        if (dtScenario5.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario5.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI5Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI5Type.Visible = true;
                        }
                        else
                        {
                            imgRPI5Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI5Type.Visible = true;
                        }
                        lblRPI5DeviceList.Text = strDevicesList5;
                        lblRPI5Status.Text = dtScenario5.Rows[0][4].ToString();
                        if (dtScenario5.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi5Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI5Status.Width = Unit.Pixel(74);
                            imgRPI5Status.Height = Unit.Pixel(64);
                            imgRPI5Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario5.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi5Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI5Status.Width = Unit.Pixel(74);
                            imgRPI5Status.Height = Unit.Pixel(64);
                            imgRPI5Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario5.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi5Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI5Status.Width = Unit.Pixel(74);
                            imgRPI5Status.Height = Unit.Pixel(64);
                            imgRPI5Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI5Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario5.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi5Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI5Status.Width = Unit.Pixel(54);
                            imgRPI5Status.Height = Unit.Pixel(74);
                            imgRPI5Status.ImageUrl = "~/Images/completed.png";
                        }
                        if (dtScenario5.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified5).TotalMinutes > 90)
                            {
                                FileInfo fileRPI5 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr5 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt");
                                    {
                                        String line5 = sr5.ReadToEnd();
                                        if (line5.Contains(myFile5.Name))
                                        {
                                            if (line5.Contains(myFile5.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI5 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr5.Close();
                                                        client.Send(mail);
                                                        FileStream file5 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt", FileMode.Append);
                                                        file5.Flush();
                                                        StreamWriter str5 = new StreamWriter(file5);
                                                        str5.Write(myFile5.Name + ";sent");
                                                        str5.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr5.Close();
                                                using (StreamWriter outputFile5 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt"))
                                                {
                                                    outputFile5.Flush();
                                                    outputFile5.Write(myFile5.Name);
                                                    outputFile5.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi5Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI5Status.Width = Unit.Pixel(74);
                                imgRPI5Status.Height = Unit.Pixel(64);
                                imgRPI5Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI5Status.Text = "No Response";
                            }
                        }
                        if (dtScenario5.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified5).TotalMinutes > 90)
                            {
                                FileInfo fileRPI5 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr5 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt");
                                    {
                                        String line5 = sr5.ReadToEnd();
                                        if (line5.Contains(myFile5.Name))
                                        {
                                            if (line5.Contains(myFile5.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI5 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr5.Close();
                                                        client.Send(mail);
                                                        sr5.Close();
                                                        client.Send(mail);
                                                        FileStream file5 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt", FileMode.Append);
                                                        file5.Flush();
                                                        StreamWriter str5 = new StreamWriter(file5);
                                                        str5.Write(myFile5.Name + ";sent");
                                                        str5.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr5.Close();
                                                using (StreamWriter outputFile5 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI5.txt"))
                                                {
                                                    outputFile5.Flush();
                                                    outputFile5.Write(myFile5.Name);
                                                    outputFile5.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi5Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI5Status.Width = Unit.Pixel(74);
                                imgRPI5Status.Height = Unit.Pixel(64);
                                imgRPI5Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI5Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi5Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI5Status.Width = Unit.Pixel(153);
                        imgRPI5Status.Height = Unit.Pixel(68);
                        imgRPI5Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI5Status.Visible = true;
                    }
                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario5"] = dtScenario5;
                    #endregion
                }
                catch
                {
                    rpi5Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI5Status.Width = Unit.Pixel(153);
                    imgRPI5Status.Height = Unit.Pixel(68);
                    imgRPI5Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI5Status.Visible = true;
                }
                try
                {
                    #region RPI6
                    DataTable dtDevices6 = new DataTable();
                    DataTable dtScenario6 = new DataTable();
                    DataTable dtKit6 = new DataTable();
                    dtKit6.Columns.Add("Kit");
                    dtKit6.Columns.Add("CurrentStatus");
                    JObject o6;
                    //using (StreamReader file6 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified6 = DateTime.Now;
                    DateTime devLastModified6 = DateTime.Now;
                    var myFile6 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev6 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb6 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists)
                    {
                        var directory6 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\");
                        myFileDev6 = (from f in directory6.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified6 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFileDev6.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists)
                    {
                        var directory6 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\");
                        myFileWeb6 = (from f in directory6.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified6 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFileWeb6.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists))
                    {
                        if (devLastModified6 > webLastModified6)
                        {
                            myFile6 = myFileDev6;
                        }
                        else
                        {
                            myFile6 = myFileWeb6;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists)
                        {
                            myFile6 = myFileWeb6;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists)
                            {
                                myFile6 = myFileDev6;
                            }
                    int PassCounter6 = 0;
                    int FailCounter6 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists)) && (DateTime.Now.Subtract(devLastModified6).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified6).TotalMinutes < 7200))
                    {
                        JObject oJ6 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile6.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file6 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile6.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file6))
                                {
                                    o6 = (JObject)JToken.ReadFrom(reader);
                                    oJ6 = o6;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile6.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi6/" + myFile6.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter6++;
                                    }
                                    else
                                    {
                                        PassCounter6++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFile6.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file6 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFile6.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file6))
                                {
                                    o6 = (JObject)JToken.ReadFrom(reader);
                                    oJ6 = o6;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFile6.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi6/" + myFile6.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter6++;
                                    }
                                    else
                                    {
                                        PassCounter6++;
                                    }
                                }
                            }
                        }
                        DataTable dt6 = new DataTable();
                        dt6.Columns.Add("Text");
                        using (JsonTextReader reader6 = new JsonTextReader(new StringReader(oJ6.ToString().Replace("\r\n", ""))))
                        {
                            string temp6 = "";
                            string tempKey6 = "";
                            string tempValue6 = "";
                            bool Type6 = false;
                            bool repFlag6 = false;
                            while (reader6.Read())
                            {
                                if (reader6.Value != null)
                                {
                                    dt6.Rows.Add(reader6.Value.ToString());
                                }
                            }
                        }


                        dtDevices6.Columns.Add("Kit");
                        dtDevices6.Columns.Add("DeviceType");
                        dtDevices6.Columns.Add("DeviceVersion");
                        dtDevices6.Columns.Add("Image");
                        string strImagePath6 = "";
                        bool flag6 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario6.Columns.Add("DeviceName");
                        dtScenario6.Columns.Add("ScenarioId");
                        dtScenario6.Columns.Add("ScenarioName");
                        dtScenario6.Columns.Add("ScenarioNo");
                        dtScenario6.Columns.Add("Status");
                        dtScenario6.Columns.Add("TimeStamp");
                        dtScenario6.Columns.Add("Duration");
                        dtScenario6.Columns.Add("FeatureFileName");
                        dtScenario6.Columns.Add("ResultsLink");
                        dtScenario6.Columns.Add("IterationComp");
                        dtScenario6.Columns.Add("IterationPass");
                        dtScenario6.Columns.Add("IterationFail");
                        string strKit6 = "";
                        string strDevicesList6 = "";
                        string strResult6 = "";
                        for (int i = 0; i < dt6.Rows.Count; i++)
                        {
                            if (dt6.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile6.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile6.Name).GetFiles("*.html").First().Name;
                                    strResult6 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi6/" + myFile6.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFile6.Name).GetFiles("*.html").First().Name;
                                    strResult6 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi6/" + myFile6.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt6.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit6.Rows.Add(dt6.Rows[i][0].ToString(), dt6.Rows[i + 2][0].ToString());
                                strKit6 = dt6.Rows[i][0].ToString();
                            }
                            if (dt6.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag6 = false;
                                dtScenario6.Rows.Add(strKit6, dt6.Rows[i + 6][0].ToString(), dt6.Rows[i + 10][0].ToString(), dt6.Rows[i][0].ToString(), dt6.Rows[i + 14][0].ToString(), dt6.Rows[i + 8][0].ToString(), dt6.Rows[i + 10][0].ToString(), dt6.Rows[i + 12][0].ToString(), strResult6, dt6.Rows[i + 8][0].ToString(), dt6.Rows[i + 12][0].ToString(), dt6.Rows[i + 4][0].ToString());
                            }
                            if (flag6 == true)
                            {
                                if (dt6.Rows[i][0].ToString().Contains("SLP") || dt6.Rows[i][0].ToString().Contains("SLB") || dt6.Rows[i][0].ToString().Contains("SLR") || dt6.Rows[i][0].ToString().Contains("SLT") || dt6.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt6.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath6 = "~/Images/slr1.png";
                                    }
                                    if (dt6.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath6 = "~/Images/slr1.png";
                                    }
                                    if (dt6.Rows[i][0].ToString().Contains("SLT6"))
                                    {
                                        strImagePath6 = "~/Images/slt6.png";
                                    }
                                    if (dt6.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath6 = "~/Images/slt2.png";
                                    }
                                    if (dt6.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath6 = "~/Images/slb1.png";
                                    }
                                    if (dt6.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath6 = "~/Images/spg2.png";
                                    }
                                    dtDevices6.Rows.Add("", dt6.Rows[i][0].ToString(), dt6.Rows[i + 6][0].ToString(), strImagePath6);
                                    strDevicesList6 += dt6.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt6.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl6.Text = lbl6.Text + " - " + dt6.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario6.Rows[0][9] != null && dtScenario6.Rows[0][10] != null && dtScenario6.Rows[0][11] != null)
                        {
                            lblRPI6IterationComp.Text = dtScenario6.Rows[0][9].ToString();
                            lblRPI6IterationPass.Text = PassCounter6.ToString();
                            lblRPI6IterationFailed.Text = FailCounter6.ToString();
                        }
                        imgRPI6Status.Visible = true;
                        imgRPI6Result.Visible = true;
                        lblRPI6TestName.Text = dtScenario6.Rows[0][2].ToString();
                        if (dtScenario6.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario6.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI6Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI6Type.Visible = true;
                        }
                        else
                        {
                            imgRPI6Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI6Type.Visible = true;
                        }
                        lblRPI6DeviceList.Text = strDevicesList6;
                        lblRPI6Status.Text = dtScenario6.Rows[0][4].ToString();
                        if (dtScenario6.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi6Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI6Status.Width = Unit.Pixel(74);
                            imgRPI6Status.Height = Unit.Pixel(64);
                            imgRPI6Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario6.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi6Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI6Status.Width = Unit.Pixel(74);
                            imgRPI6Status.Height = Unit.Pixel(64);
                            imgRPI6Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario6.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi6Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI6Status.Width = Unit.Pixel(74);
                            imgRPI6Status.Height = Unit.Pixel(64);
                            imgRPI6Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI6Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario6.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi6Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI6Status.Width = Unit.Pixel(64);
                            imgRPI6Status.Height = Unit.Pixel(74);
                            imgRPI6Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario6.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi6Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI6Status.Width = Unit.Pixel(64);
                            imgRPI6Status.Height = Unit.Pixel(74);
                            imgRPI6Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario6.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified6).TotalMinutes > 90)
                            {
                                FileInfo fileRPI6 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr6 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt");
                                    {
                                        String line6 = sr6.ReadToEnd();
                                        if (line6.Contains(myFile6.Name))
                                        {
                                            if (line6.Contains(myFile6.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI6 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr6.Close();
                                                        client.Send(mail);

                                                        FileStream file6 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt", FileMode.Append);
                                                        file6.Flush();
                                                        StreamWriter str6 = new StreamWriter(file6);
                                                        str6.Write(myFile6.Name + ";sent");
                                                        str6.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr6.Close();
                                                using (StreamWriter outputFile6 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt"))
                                                {
                                                    outputFile6.Flush();
                                                    outputFile6.Write(myFile6.Name);
                                                    outputFile6.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi6Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI6Status.Width = Unit.Pixel(74);
                                imgRPI6Status.Height = Unit.Pixel(64);
                                imgRPI6Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI6Status.Text = "No Response";
                            }
                        }
                        if (dtScenario6.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified6).TotalMinutes > 90)
                            {
                                FileInfo fileRPI6 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr6 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt");
                                    {
                                        String line6 = sr6.ReadToEnd();
                                        if (line6.Contains(myFile6.Name))
                                        {
                                            if (line6.Contains(myFile6.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI6 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr6.Close();
                                                        client.Send(mail);
                                                        FileStream file6 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt", FileMode.Append);
                                                        file6.Flush();
                                                        StreamWriter str6 = new StreamWriter(file6);
                                                        str6.Write(myFile6.Name + ";sent");
                                                        str6.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr6.Close();
                                                using (StreamWriter outputFile6 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI6.txt"))
                                                {
                                                    outputFile6.Flush();
                                                    outputFile6.Write(myFile6.Name);
                                                    outputFile6.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi6Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI6Status.Width = Unit.Pixel(74);
                                imgRPI6Status.Height = Unit.Pixel(64);
                                imgRPI6Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI6Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi6Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI6Status.Width = Unit.Pixel(153);
                        imgRPI6Status.Height = Unit.Pixel(68);
                        imgRPI6Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI6Status.Visible = true;
                    }
                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario6"] = dtScenario6;
                    #endregion
                }
                catch
                {
                    rpi6Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI6Status.Width = Unit.Pixel(153);
                    imgRPI6Status.Height = Unit.Pixel(68);
                    imgRPI6Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI6Status.Visible = true;
                }
                try
                {
                    #region RPI7
                    DataTable dtDevices7 = new DataTable();
                    DataTable dtScenario7 = new DataTable();
                    DataTable dtKit7 = new DataTable();
                    dtKit7.Columns.Add("Kit");
                    dtKit7.Columns.Add("CurrentStatus");
                    JObject o7;
                    //using (StreamReader file7 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified7 = DateTime.Now;
                    DateTime devLastModified7 = DateTime.Now;
                    var myFile7 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev7 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb7 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists)
                    {
                        var directory7 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\");
                        myFileDev7 = (from f in directory7.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified7 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFileDev7.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists)
                    {
                        var directory7 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\");
                        myFileWeb7 = (from f in directory7.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified7 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFileWeb7.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists))
                    {
                        if (devLastModified7 > webLastModified7)
                        {
                            myFile7 = myFileDev7;
                        }
                        else
                        {
                            myFile7 = myFileWeb7;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists)
                        {
                            myFile7 = myFileWeb7;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists)
                            {
                                myFile7 = myFileDev7;
                            }
                    int PassCounter7 = 0;
                    int FailCounter7 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists)) && (DateTime.Now.Subtract(devLastModified7).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified7).TotalMinutes < 7200))
                    {
                        JObject oJ7 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile7.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file7 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile7.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file7))
                                {
                                    o7 = (JObject)JToken.ReadFrom(reader);
                                    oJ7 = o7;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile7.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi7/" + myFile7.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter7++;
                                    }
                                    else
                                    {
                                        PassCounter7++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFile7.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file7 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFile7.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file7))
                                {
                                    o7 = (JObject)JToken.ReadFrom(reader);
                                    oJ7 = o7;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFile7.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi7/" + myFile7.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter7++;
                                    }
                                    else
                                    {
                                        PassCounter7++;
                                    }
                                }
                            }
                        }
                        DataTable dt7 = new DataTable();
                        dt7.Columns.Add("Text");
                        using (JsonTextReader reader7 = new JsonTextReader(new StringReader(oJ7.ToString().Replace("\r\n", ""))))
                        {
                            string temp7 = "";
                            string tempKey7 = "";
                            string tempValue7 = "";
                            bool Type7 = false;
                            bool repFlag7 = false;
                            while (reader7.Read())
                            {
                                if (reader7.Value != null)
                                {
                                    dt7.Rows.Add(reader7.Value.ToString());
                                }
                            }
                        }


                        dtDevices7.Columns.Add("Kit");
                        dtDevices7.Columns.Add("DeviceType");
                        dtDevices7.Columns.Add("DeviceVersion");
                        dtDevices7.Columns.Add("Image");
                        string strImagePath7 = "";
                        bool flag7 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario7.Columns.Add("DeviceName");
                        dtScenario7.Columns.Add("ScenarioId");
                        dtScenario7.Columns.Add("ScenarioName");
                        dtScenario7.Columns.Add("ScenarioNo");
                        dtScenario7.Columns.Add("Status");
                        dtScenario7.Columns.Add("TimeStamp");
                        dtScenario7.Columns.Add("Duration");
                        dtScenario7.Columns.Add("FeatureFileName");
                        dtScenario7.Columns.Add("ResultsLink");
                        dtScenario7.Columns.Add("IterationComp");
                        dtScenario7.Columns.Add("IterationPass");
                        dtScenario7.Columns.Add("IterationFail");
                        string strKit7 = "";
                        string strDevicesList7 = "";
                        string strResult7 = "";
                        for (int i = 0; i < dt7.Rows.Count; i++)
                        {
                            if (dt7.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile7.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile7.Name).GetFiles("*.html").First().Name;
                                    strResult7 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi7/" + myFile7.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFile7.Name).GetFiles("*.html").First().Name;
                                    strResult7 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi7/" + myFile7.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt7.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit7.Rows.Add(dt7.Rows[i][0].ToString(), dt7.Rows[i + 2][0].ToString());
                                strKit7 = dt7.Rows[i][0].ToString();
                            }
                            if (dt7.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag7 = false;
                                dtScenario7.Rows.Add(strKit7, dt7.Rows[i + 6][0].ToString(), dt7.Rows[i + 10][0].ToString(), dt7.Rows[i][0].ToString(), dt7.Rows[i + 14][0].ToString(), dt7.Rows[i + 8][0].ToString(), dt7.Rows[i + 10][0].ToString(), dt7.Rows[i + 12][0].ToString(), strResult7, dt7.Rows[i + 8][0].ToString(), dt7.Rows[i + 12][0].ToString(), dt7.Rows[i + 4][0].ToString());
                            }
                            if (flag7 == true)
                            {

                                if (dt7.Rows[i][0].ToString().Contains("SLP") || dt7.Rows[i][0].ToString().Contains("SLB") || dt7.Rows[i][0].ToString().Contains("SLR") || dt7.Rows[i][0].ToString().Contains("SLT") || dt7.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt7.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath7 = "~/Images/slr1.png";
                                    }
                                    if (dt7.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath7 = "~/Images/slr1.png";
                                    }
                                    if (dt7.Rows[i][0].ToString().Contains("SLT7"))
                                    {
                                        strImagePath7 = "~/Images/slt7.png";
                                    }
                                    if (dt7.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath7 = "~/Images/slt2.png";
                                    }
                                    if (dt7.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath7 = "~/Images/slb1.png";
                                    }
                                    if (dt7.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath7 = "~/Images/spg2.png";
                                    }
                                    dtDevices7.Rows.Add("", dt7.Rows[i][0].ToString(), dt7.Rows[i + 6][0].ToString(), strImagePath7);
                                    strDevicesList7 += dt7.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt7.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl7.Text = lbl7.Text + " - " + dt7.Rows[i + 1][0].ToString();
                            }

                        }
                        if (dtScenario7.Rows[0][9] != null && dtScenario7.Rows[0][10] != null && dtScenario7.Rows[0][11] != null)
                        {
                            lblRPI7IterationComp.Text = dtScenario7.Rows[0][9].ToString();
                            lblRPI7IterationPass.Text = PassCounter7.ToString();
                            lblRPI7IterationFailed.Text = FailCounter7.ToString();
                        }
                        imgRPI7Status.Visible = true;
                        imgRPI7Result.Visible = true;
                        lblRPI7TestName.Text = dtScenario7.Rows[0][2].ToString();
                        if (dtScenario7.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario7.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI7Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI7Type.Visible = true;
                        }
                        else
                        {
                            imgRPI7Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI7Type.Visible = true;
                        }
                        lblRPI7DeviceList.Text = strDevicesList7;
                        lblRPI7Status.Text = dtScenario7.Rows[0][4].ToString();
                        if (dtScenario7.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi7Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI7Status.Width = Unit.Pixel(74);
                            imgRPI7Status.Height = Unit.Pixel(64);
                            imgRPI7Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario7.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi7Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI7Status.Width = Unit.Pixel(74);
                            imgRPI7Status.Height = Unit.Pixel(64);
                            imgRPI7Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario7.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi7Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI7Status.Width = Unit.Pixel(74);
                            imgRPI7Status.Height = Unit.Pixel(64);
                            imgRPI7Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI7Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario7.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi7Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI7Status.Width = Unit.Pixel(53);
                            imgRPI7Status.Height = Unit.Pixel(73);
                            imgRPI7Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario7.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi7Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI7Status.Width = Unit.Pixel(64);
                            imgRPI7Status.Height = Unit.Pixel(74);
                            imgRPI7Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario7.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified7).TotalMinutes > 90)
                            {
                                FileInfo fileRPI7 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr7 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt");
                                    {
                                        String line7 = sr7.ReadToEnd();
                                        if (line7.Contains(myFile7.Name))
                                        {
                                            if (line7.Contains(myFile7.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI7 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr7.Close();
                                                        client.Send(mail);
                                                        FileStream file7 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt", FileMode.Append);
                                                        file7.Flush();
                                                        StreamWriter str7 = new StreamWriter(file7);
                                                        str7.Write(myFile7.Name + ";sent");
                                                        str7.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr7.Close();
                                                using (StreamWriter outputFile7 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt"))
                                                {
                                                    outputFile7.Flush();
                                                    outputFile7.Write(myFile7.Name);
                                                    outputFile7.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi7Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI7Status.Width = Unit.Pixel(74);
                                imgRPI7Status.Height = Unit.Pixel(64);
                                imgRPI7Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI7Status.Text = "No Response";
                            }
                        }
                        if (dtScenario7.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified7).TotalMinutes > 90)
                            {

                                FileInfo fileRPI7 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr7 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt");
                                    {
                                        String line7 = sr7.ReadToEnd();
                                        if (line7.Contains(myFile7.Name))
                                        {
                                            if (line7.Contains(myFile7.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI7 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr7.Close();
                                                        client.Send(mail);
                                                        FileStream file7 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt", FileMode.Append);
                                                        file7.Flush();
                                                        StreamWriter str7 = new StreamWriter(file7);
                                                        str7.Write(myFile7.Name + ";sent");
                                                        str7.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr7.Close();
                                                using (StreamWriter outputFile7 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI7.txt"))
                                                {
                                                    outputFile7.Flush();
                                                    outputFile7.Write(myFile7.Name);
                                                    outputFile7.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi7Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI7Status.Width = Unit.Pixel(74);
                                imgRPI7Status.Height = Unit.Pixel(64);
                                imgRPI7Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI7Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi7Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI7Status.Width = Unit.Pixel(153);
                        imgRPI7Status.Height = Unit.Pixel(68);
                        imgRPI7Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI7Status.Visible = true;
                    }
                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario7"] = dtScenario7;
                    #endregion
                }
                catch
                {
                    rpi7Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI7Status.Width = Unit.Pixel(153);
                    imgRPI7Status.Height = Unit.Pixel(68);
                    imgRPI7Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI7Status.Visible = true;
                }
                try
                {
                    #region RPI8
                    DataTable dtDevices8 = new DataTable();
                    DataTable dtScenario8 = new DataTable();
                    DataTable dtKit8 = new DataTable();
                    dtKit8.Columns.Add("Kit");
                    dtKit8.Columns.Add("CurrentStatus");
                    JObject o8;
                    //using (StreamReader file8 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified8 = DateTime.Now;
                    DateTime devLastModified8 = DateTime.Now;
                    var myFile8 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev8 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb8 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists)
                    {
                        var directory8 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\");
                        myFileDev8 = (from f in directory8.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified8 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFileDev8.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists)
                    {
                        var directory8 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\");
                        myFileWeb8 = (from f in directory8.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified8 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFileWeb8.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists))
                    {
                        if (devLastModified8 > webLastModified8)
                        {
                            myFile8 = myFileDev8;
                        }
                        else
                        {
                            myFile8 = myFileWeb8;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists)
                        {
                            myFile8 = myFileWeb8;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists)
                            {
                                myFile8 = myFileDev8;
                            }
                    int PassCounter8 = 0;
                    int FailCounter8 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists)) && (DateTime.Now.Subtract(devLastModified8).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified8).TotalMinutes < 7200))
                    {
                        JObject oJ8 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile8.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file8 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile8.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file8))
                                {
                                    o8 = (JObject)JToken.ReadFrom(reader);
                                    oJ8 = o8;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile8.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi8/" + myFile8.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter8++;
                                    }
                                    else
                                    {
                                        PassCounter8++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFile8.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file8 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFile8.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file8))
                                {
                                    o8 = (JObject)JToken.ReadFrom(reader);
                                    oJ8 = o8;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFile8.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi8/" + myFile8.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter8++;
                                    }
                                    else
                                    {
                                        PassCounter8++;
                                    }
                                }
                            }
                        }
                        DataTable dt8 = new DataTable();
                        dt8.Columns.Add("Text");
                        using (JsonTextReader reader8 = new JsonTextReader(new StringReader(oJ8.ToString().Replace("\r\n", ""))))
                        {
                            string temp8 = "";
                            string tempKey8 = "";
                            string tempValue8 = "";
                            bool Type8 = false;
                            bool repFlag8 = false;
                            while (reader8.Read())
                            {
                                if (reader8.Value != null)
                                {
                                    dt8.Rows.Add(reader8.Value.ToString());
                                }
                            }
                        }


                        dtDevices8.Columns.Add("Kit");
                        dtDevices8.Columns.Add("DeviceType");
                        dtDevices8.Columns.Add("DeviceVersion");
                        dtDevices8.Columns.Add("Image");
                        string strImagePath8 = "";
                        bool flag8 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario8.Columns.Add("DeviceName");
                        dtScenario8.Columns.Add("ScenarioId");
                        dtScenario8.Columns.Add("ScenarioName");
                        dtScenario8.Columns.Add("ScenarioNo");
                        dtScenario8.Columns.Add("Status");
                        dtScenario8.Columns.Add("TimeStamp");
                        dtScenario8.Columns.Add("Duration");
                        dtScenario8.Columns.Add("FeatureFileName");
                        dtScenario8.Columns.Add("ResultsLink");
                        dtScenario8.Columns.Add("IterationComp");
                        dtScenario8.Columns.Add("IterationPass");
                        dtScenario8.Columns.Add("IterationFail");
                        string strKit8 = "";
                        string strDevicesList8 = "";
                        string strResult8 = "";
                        for (int i = 0; i < dt8.Rows.Count; i++)
                        {
                            if (dt8.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile8.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile8.Name).GetFiles("*.html").First().Name;
                                    strResult8 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi8/" + myFile8.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFile8.Name).GetFiles("*.html").First().Name;
                                    strResult8 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi8/" + myFile8.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt8.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit8.Rows.Add(dt8.Rows[i][0].ToString(), dt8.Rows[i + 2][0].ToString());
                                strKit8 = dt8.Rows[i][0].ToString();
                            }
                            if (dt8.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag8 = false;
                                dtScenario8.Rows.Add(strKit8, dt8.Rows[i + 6][0].ToString(), dt8.Rows[i + 10][0].ToString(), dt8.Rows[i][0].ToString(), dt8.Rows[i + 14][0].ToString(), dt8.Rows[i + 8][0].ToString(), dt8.Rows[i + 10][0].ToString(), dt8.Rows[i + 12][0].ToString(), strResult8, dt8.Rows[i + 8][0].ToString(), dt8.Rows[i + 12][0].ToString(), dt8.Rows[i + 4][0].ToString());
                            }
                            if (flag8 == true)
                            {
                                if (dt8.Rows[i][0].ToString().Contains("SLP") || dt8.Rows[i][0].ToString().Contains("SLB") || dt8.Rows[i][0].ToString().Contains("SLR") || dt8.Rows[i][0].ToString().Contains("SLT") || dt8.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt8.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath8 = "~/Images/slr1.png";
                                    }
                                    if (dt8.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath8 = "~/Images/slr1.png";
                                    }
                                    if (dt8.Rows[i][0].ToString().Contains("SLT8"))
                                    {
                                        strImagePath8 = "~/Images/slt8.png";
                                    }
                                    if (dt8.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath8 = "~/Images/slt2.png";
                                    }
                                    if (dt8.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath8 = "~/Images/slb1.png";
                                    }
                                    if (dt8.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath8 = "~/Images/spg2.png";
                                    }
                                    dtDevices8.Rows.Add("", dt8.Rows[i][0].ToString(), dt8.Rows[i + 6][0].ToString(), strImagePath8);
                                    strDevicesList8 += dt8.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt8.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl8.Text = lbl8.Text + " - " + dt8.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario8.Rows[0][9] != null && dtScenario8.Rows[0][10] != null && dtScenario8.Rows[0][11] != null)
                        {
                            lblRPI8IterationComp.Text = dtScenario8.Rows[0][9].ToString();
                            lblRPI8IterationPass.Text = PassCounter8.ToString();
                            lblRPI8IterationFailed.Text = FailCounter8.ToString();
                        }
                        imgRPI8Status.Visible = true;
                        imgRPI8Result.Visible = true;
                        lblRPI8TestName.Text = dtScenario8.Rows[0][2].ToString();
                        if (dtScenario8.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario8.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI8Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI8Type.Visible = true;
                        }
                        else
                        {
                            imgRPI8Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI8Type.Visible = true;
                        }
                        lblRPI8DeviceList.Text = strDevicesList8;
                        lblRPI8Status.Text = dtScenario8.Rows[0][4].ToString();
                        if (dtScenario8.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi8Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI8Status.Width = Unit.Pixel(74);
                            imgRPI8Status.Height = Unit.Pixel(64);
                            imgRPI8Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario8.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi8Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI8Status.Width = Unit.Pixel(74);
                            imgRPI8Status.Height = Unit.Pixel(64);
                            imgRPI8Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario8.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi8Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI8Status.Width = Unit.Pixel(74);
                            imgRPI8Status.Height = Unit.Pixel(64);
                            imgRPI8Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI8Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario8.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi8Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI8Status.Width = Unit.Pixel(64);
                            imgRPI8Status.Height = Unit.Pixel(74);
                            imgRPI8Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario8.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi8Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI8Status.Width = Unit.Pixel(64);
                            imgRPI8Status.Height = Unit.Pixel(74);
                            imgRPI8Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario8.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified8).TotalMinutes > 90)
                            {
                                FileInfo fileRPI8 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr8 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt");
                                    {
                                        String line8 = sr8.ReadToEnd();
                                        if (line8.Contains(myFile8.Name))
                                        {
                                            if (line8.Contains(myFile8.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI8 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr8.Close();
                                                        client.Send(mail);
                                                        FileStream file8 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt", FileMode.Append);
                                                        file8.Flush();
                                                        StreamWriter str8 = new StreamWriter(file8);
                                                        str8.Write(myFile8.Name + ";sent");
                                                        str8.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr8.Close();
                                                using (StreamWriter outputFile8 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt"))
                                                {
                                                    outputFile8.Flush();
                                                    outputFile8.Write(myFile8.Name);
                                                    outputFile8.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi8Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI8Status.Width = Unit.Pixel(74);
                                imgRPI8Status.Height = Unit.Pixel(64);
                                imgRPI8Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI8Status.Text = "No Response";
                            }
                        }
                        if (dtScenario8.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified8).TotalMinutes > 90)
                            {
                                FileInfo fileRPI8 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr8 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt");
                                    {
                                        String line8 = sr8.ReadToEnd();
                                        if (line8.Contains(myFile8.Name))
                                        {
                                            if (line8.Contains(myFile8.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI8 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr8.Close();
                                                        client.Send(mail);
                                                        FileStream file8 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt", FileMode.Append);
                                                        file8.Flush();
                                                        StreamWriter str8 = new StreamWriter(file8);
                                                        str8.Write(myFile8.Name + ";sent");
                                                        str8.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr8.Close();
                                                using (StreamWriter outputFile8 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI8.txt"))
                                                {
                                                    outputFile8.Flush();
                                                    outputFile8.Write(myFile8.Name);
                                                    outputFile8.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi8Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI8Status.Width = Unit.Pixel(74);
                                imgRPI8Status.Height = Unit.Pixel(64);
                                imgRPI8Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI8Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi8Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI8Status.Width = Unit.Pixel(153);
                        imgRPI8Status.Height = Unit.Pixel(68);
                        imgRPI8Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI8Status.Visible = true;
                    }
                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario8"] = dtScenario8;
                    #endregion
                }
                catch
                {
                    rpi8Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI8Status.Width = Unit.Pixel(153);
                    imgRPI8Status.Height = Unit.Pixel(68);
                    imgRPI8Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI8Status.Visible = true;
                }
                try
                {
                    #region RPI9
                    DataTable dtDevices9 = new DataTable();
                    DataTable dtScenario9 = new DataTable();
                    DataTable dtKit9 = new DataTable();
                    dtKit9.Columns.Add("Kit");
                    dtKit9.Columns.Add("CurrentStatus");
                    JObject o9;
                    //using (StreamReader file9 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified9 = DateTime.Now;
                    DateTime devLastModified9 = DateTime.Now;
                    var myFile9 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev9 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb9 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists)
                    {
                        var directory9 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\");
                        myFileDev9 = (from f in directory9.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        devLastModified9 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFileDev9.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists)
                    {
                        var directory9 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\");
                        myFileWeb9 = (from f in directory9.GetDirectories()
                                      orderby f.LastWriteTime descending
                                      select f).First();
                        webLastModified9 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFileWeb9.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists))
                    {
                        if (devLastModified9 > webLastModified9)
                        {
                            myFile9 = myFileDev9;
                        }
                        else
                        {
                            myFile9 = myFileWeb9;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists)
                        {
                            myFile9 = myFileWeb9;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists)
                            {
                                myFile9 = myFileDev9;
                            }

                    int PassCounter9 = 0;
                    int FailCounter9 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists)) && (DateTime.Now.Subtract(devLastModified9).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified9).TotalMinutes < 7200))
                    {
                        JObject oJ9 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile9.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file9 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile9.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file9))
                                {
                                    o9 = (JObject)JToken.ReadFrom(reader);
                                    oJ9 = o9;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile9.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi9/" + myFile9.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter9++;
                                    }
                                    else
                                    {
                                        PassCounter9++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFile9.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file9 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFile9.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file9))
                                {
                                    o9 = (JObject)JToken.ReadFrom(reader);
                                    oJ9 = o9;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFile9.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Web-Mobile_Test_Automation/Test_Results/rpi9/" + myFile9.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter9++;
                                    }
                                    else
                                    {
                                        PassCounter9++;
                                    }
                                }
                            }
                        }
                        DataTable dt9 = new DataTable();
                        dt9.Columns.Add("Text");
                        using (JsonTextReader reader9 = new JsonTextReader(new StringReader(oJ9.ToString().Replace("\r\n", ""))))
                        {
                            string temp9 = "";
                            string tempKey9 = "";
                            string tempValue9 = "";
                            bool Type9 = false;
                            bool repFlag9 = false;
                            while (reader9.Read())
                            {
                                if (reader9.Value != null)
                                {
                                    dt9.Rows.Add(reader9.Value.ToString());
                                }
                            }
                        }


                        dtDevices9.Columns.Add("Kit");
                        dtDevices9.Columns.Add("DeviceType");
                        dtDevices9.Columns.Add("DeviceVersion");
                        dtDevices9.Columns.Add("Image");
                        string strImagePath9 = "";
                        bool flag9 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario9.Columns.Add("DeviceName");
                        dtScenario9.Columns.Add("ScenarioId");
                        dtScenario9.Columns.Add("ScenarioName");
                        dtScenario9.Columns.Add("ScenarioNo");
                        dtScenario9.Columns.Add("Status");
                        dtScenario9.Columns.Add("TimeStamp");
                        dtScenario9.Columns.Add("Duration");
                        dtScenario9.Columns.Add("FeatureFileName");
                        dtScenario9.Columns.Add("ResultsLink");
                        dtScenario9.Columns.Add("IterationComp");
                        dtScenario9.Columns.Add("IterationPass");
                        dtScenario9.Columns.Add("IterationFail");
                        string strKit9 = "";
                        string strDevicesList9 = "";
                        string strResult9 = "";
                        for (int i = 0; i < dt9.Rows.Count; i++)
                        {
                            if (dt9.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile9.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile9.Name).GetFiles("*.html").First().Name;
                                    strResult9 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi9/" + myFile9.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFile9.Name).GetFiles("*.html").First().Name;
                                    strResult9 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi9/" + myFile9.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt9.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit9.Rows.Add(dt9.Rows[i][0].ToString(), dt9.Rows[i + 2][0].ToString());
                                strKit9 = dt9.Rows[i][0].ToString();
                            }
                            if (dt9.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag9 = false;
                                dtScenario9.Rows.Add(strKit9, dt9.Rows[i + 6][0].ToString(), dt9.Rows[i + 10][0].ToString(), dt9.Rows[i][0].ToString(), dt9.Rows[i + 14][0].ToString(), dt9.Rows[i + 8][0].ToString(), dt9.Rows[i + 10][0].ToString(), dt9.Rows[i + 12][0].ToString(), strResult9, dt9.Rows[i + 8][0].ToString(), dt9.Rows[i + 12][0].ToString(), dt9.Rows[i + 4][0].ToString());
                            }
                            if (flag9 == true)
                            {
                                if (dt9.Rows[i][0].ToString().Contains("SLP") || dt9.Rows[i][0].ToString().Contains("SLB") || dt9.Rows[i][0].ToString().Contains("SLR") || dt9.Rows[i][0].ToString().Contains("SLT") || dt9.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt9.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath9 = "~/Images/slr1.png";
                                    }
                                    if (dt9.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath9 = "~/Images/slr1.png";
                                    }
                                    if (dt9.Rows[i][0].ToString().Contains("SLT9"))
                                    {
                                        strImagePath9 = "~/Images/slt9.png";
                                    }
                                    if (dt9.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath9 = "~/Images/slt2.png";
                                    }
                                    if (dt9.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath9 = "~/Images/slb1.png";
                                    }
                                    if (dt9.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath9 = "~/Images/spg2.png";
                                    }
                                    dtDevices9.Rows.Add("", dt9.Rows[i][0].ToString(), dt9.Rows[i + 6][0].ToString(), strImagePath9);
                                    strDevicesList9 += dt9.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt9.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl9.Text = lbl9.Text + " - " + dt9.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario9.Rows[0][9] != null && dtScenario9.Rows[0][10] != null && dtScenario9.Rows[0][11] != null)
                        {
                            lblRPI9IterationComp.Text = dtScenario9.Rows[0][9].ToString();
                            lblRPI9IterationPass.Text = PassCounter9.ToString();
                            lblRPI9IterationFailed.Text = FailCounter9.ToString();
                        }
                        imgRPI9Status.Visible = true;
                        imgRPI9Result.Visible = true;
                        lblRPI9TestName.Text = dtScenario9.Rows[0][2].ToString();
                        if (dtScenario9.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario9.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI9Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI9Type.Visible = true;
                        }
                        else
                        {
                            imgRPI9Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI9Type.Visible = true;
                        }
                        lblRPI9DeviceList.Text = strDevicesList9;
                        lblRPI9Status.Text = dtScenario9.Rows[0][4].ToString();
                        if (dtScenario9.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi9Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI9Status.Width = Unit.Pixel(74);
                            imgRPI9Status.Height = Unit.Pixel(64);
                            imgRPI9Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario9.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi9Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI9Status.Width = Unit.Pixel(74);
                            imgRPI9Status.Height = Unit.Pixel(64);
                            imgRPI9Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario9.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi9Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI9Status.Width = Unit.Pixel(74);
                            imgRPI9Status.Height = Unit.Pixel(64);
                            imgRPI9Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI9Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario9.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi9Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI9Status.Width = Unit.Pixel(64);
                            imgRPI9Status.Height = Unit.Pixel(74);
                            imgRPI9Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario9.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi9Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI9Status.Width = Unit.Pixel(64);
                            imgRPI9Status.Height = Unit.Pixel(74);
                            imgRPI9Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario9.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified9).TotalMinutes > 90)
                            {
                                FileInfo fileRPI9 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr9 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt");
                                    {
                                        String line9 = sr9.ReadToEnd();
                                        if (line9.Contains(myFile9.Name))
                                        {
                                            if (line9.Contains(myFile9.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI9 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr9.Close();
                                                        client.Send(mail);
                                                        FileStream file9 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt", FileMode.Append);
                                                        file9.Flush();
                                                        StreamWriter str9 = new StreamWriter(file9);
                                                        str9.Write(myFile9.Name + ";sent");
                                                        str9.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr9.Close();
                                                using (StreamWriter outputFile9 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt"))
                                                {
                                                    outputFile9.Flush();
                                                    outputFile9.Write(myFile9.Name);
                                                    outputFile9.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi9Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI9Status.Width = Unit.Pixel(74);
                                imgRPI9Status.Height = Unit.Pixel(64);
                                imgRPI9Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI9Status.Text = "No Response";
                            }
                        }
                        if (dtScenario9.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified9).TotalMinutes > 90)
                            {
                                FileInfo fileRPI9 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr9 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt");
                                    {
                                        String line9 = sr9.ReadToEnd();
                                        if (line9.Contains(myFile9.Name))
                                        {
                                            if (line9.Contains(myFile9.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI9 for the past 90 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr9.Close();
                                                        client.Send(mail);
                                                        FileStream file9 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt", FileMode.Append);
                                                        file9.Flush();
                                                        StreamWriter str9 = new StreamWriter(file9);
                                                        str9.Write(myFile9.Name + ";sent");
                                                        str9.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr9.Close();
                                                using (StreamWriter outputFile9 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI9.txt"))
                                                {
                                                    outputFile9.Flush();
                                                    outputFile9.Write(myFile9.Name);
                                                    outputFile9.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi9Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI9Status.Width = Unit.Pixel(74);
                                imgRPI9Status.Height = Unit.Pixel(64);
                                imgRPI9Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI9Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi9Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI9Status.Width = Unit.Pixel(153);
                        imgRPI9Status.Height = Unit.Pixel(68);
                        imgRPI9Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI9Status.Visible = true;
                    }

                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario9"] = dtScenario9;
                    #endregion
                }
                catch
                {
                    rpi9Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI9Status.Width = Unit.Pixel(153);
                    imgRPI9Status.Height = Unit.Pixel(68);
                    imgRPI9Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI9Status.Visible = true;
                }
                try
                {
                    #region RPI11
                    DataTable dtDevices11 = new DataTable();
                    DataTable dtScenario11 = new DataTable();
                    DataTable dtKit11 = new DataTable();
                    dtKit11.Columns.Add("Kit");
                    dtKit11.Columns.Add("CurrentStatus");
                    JObject o11;
                    //using (StreamReader file11 = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                    DateTime webLastModified11 = DateTime.Now;
                    DateTime devLastModified11 = DateTime.Now;
                    var myFile11 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileDev11 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    var myFileWeb11 = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists)
                    {
                        var directory11 = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\");
                        myFileDev11 = (from f in directory11.GetDirectories()
                                       orderby f.LastWriteTime descending
                                       select f).First();
                        devLastModified11 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFileDev11.Name + @"\results.json");

                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists)
                    {
                        var directory11 = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\");
                        myFileWeb11 = (from f in directory11.GetDirectories()
                                       orderby f.LastWriteTime descending
                                       select f).First();
                        webLastModified11 = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFileWeb11.Name + @"\results.json");
                    }

                    if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists))
                    {
                        if (devLastModified11 > webLastModified11)
                        {
                            myFile11 = myFileDev11;
                        }
                        else
                        {
                            myFile11 = myFileWeb11;
                        }
                    }
                    else
                        if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists)
                        {
                            myFile11 = myFileWeb11;
                        }
                        else
                            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists)
                            {
                                myFile11 = myFileDev11;
                            }

                    int PassCounter11 = 0;
                    int FailCounter11 = 0;
                    if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists)) && (DateTime.Now.Subtract(devLastModified11).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified11).TotalMinutes < 7200))
                    {
                        JObject oJ11 = new JObject();
                        if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile11.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file11 = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile11.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file11))
                                {
                                    o11 = (JObject)JToken.ReadFrom(reader);
                                    oJ11 = o11;
                                }
                                string ofName = new FileInfo(Directory.GetFiles(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile11.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~/hardware/Device_Test_Automation/Test_Results/rpi11/" + myFile11.Name + @"/HTML/" + ofName));
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter11++;
                                    }
                                    else
                                    {
                                        PassCounter11++;
                                    }
                                }
                            }
                        }
                        if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFile11.Name + @"\results.json").Exists)
                        {
                            using (StreamReader file11 = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFile11.Name + @"\results.json"))
                            {
                                using (JsonTextReader reader = new JsonTextReader(file11))
                                {
                                    o11 = (JObject)JToken.ReadFrom(reader);
                                    oJ11 = o11;
                                }

                                string ofName = new Delimon.Win32.IO.FileInfo(Directory.GetFiles(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFile11.Name + @"/HTML\", "*.html", SearchOption.AllDirectories)[0]).Name;
                                string str = Delimon.Win32.IO.File.ReadAllText("\\\\?\\" + Server.MapPath(@"~/Hardware/Web-Mobile_Test_Automation/Test_Results/rpi11") + "\\" + myFile11.Name + @"\HTML\" + ofName);
                                int Counter = (str.Replace("Counter :", "£").Split('£').Length - 1) / 2;

                                for (int i = 1; i < Counter; i++)
                                {
                                    if (str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
                                    {
                                        FailCounter11++;
                                    }
                                    else
                                    {
                                        PassCounter11++;
                                    }
                                }
                            }
                        }
                        DataTable dt11 = new DataTable();
                        dt11.Columns.Add("Text");
                        using (JsonTextReader reader11 = new JsonTextReader(new StringReader(oJ11.ToString().Replace("\r\n", ""))))
                        {
                            string temp11 = "";
                            string tempKey11 = "";
                            string tempValue11 = "";
                            bool Type11 = false;
                            bool repFlag11 = false;
                            while (reader11.Read())
                            {
                                if (reader11.Value != null)
                                {
                                    dt11.Rows.Add(reader11.Value.ToString());
                                }
                            }
                        }


                        dtDevices11.Columns.Add("Kit");
                        dtDevices11.Columns.Add("DeviceType");
                        dtDevices11.Columns.Add("DeviceVersion");
                        dtDevices11.Columns.Add("Image");
                        string strImagePath11 = "";
                        bool flag11 = true;
                        //DataTable dtScenario = new DataTable();
                        dtScenario11.Columns.Add("DeviceName");
                        dtScenario11.Columns.Add("ScenarioId");
                        dtScenario11.Columns.Add("ScenarioName");
                        dtScenario11.Columns.Add("ScenarioNo");
                        dtScenario11.Columns.Add("Status");
                        dtScenario11.Columns.Add("TimeStamp");
                        dtScenario11.Columns.Add("Duration");
                        dtScenario11.Columns.Add("FeatureFileName");
                        dtScenario11.Columns.Add("ResultsLink");
                        dtScenario11.Columns.Add("IterationComp");
                        dtScenario11.Columns.Add("IterationPass");
                        dtScenario11.Columns.Add("IterationFail");
                        string strKit11 = "";
                        string strDevicesList11 = "";
                        string strResult11 = "";
                        for (int i = 0; i < dt11.Rows.Count; i++)
                        {
                            if (dt11.Rows[i][0].ToString() == "result_folder")
                            {
                                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile11.Name + @"\results.json").Exists)
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile11.Name).GetFiles("*.html").First().Name;
                                    strResult11 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Device_Test_Automation/Test_Results/rpi11/" + myFile11.Name + "/" + strHTML;
                                }
                                else
                                {
                                    string strHTML = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFile11.Name).GetFiles("*.html").First().Name;
                                    strResult11 = "http://c333187.bgchnet.co.uk/Dashboard/hardware/Web-Mobile_Test_Automation/Test_Results/rpi11/" + myFile11.Name + "/" + strHTML;
                                }
                            }
                        }
                        for (int i = 0; i < dt11.Rows.Count; i++)
                        {
                            if (i == 1)
                            {
                                dtKit11.Rows.Add(dt11.Rows[i][0].ToString(), dt11.Rows[i + 2][0].ToString());
                                strKit11 = dt11.Rows[i][0].ToString();
                            }
                            if (dt11.Rows[i][0].ToString().Contains("scenario_"))
                            {
                                flag11 = false;
                                dtScenario11.Rows.Add(strKit11, dt11.Rows[i + 6][0].ToString(), dt11.Rows[i + 10][0].ToString(), dt11.Rows[i][0].ToString(), dt11.Rows[i + 14][0].ToString(), dt11.Rows[i + 8][0].ToString(), dt11.Rows[i + 10][0].ToString(), dt11.Rows[i + 12][0].ToString(), strResult11, dt11.Rows[i + 8][0].ToString(), dt11.Rows[i + 12][0].ToString(), dt11.Rows[i + 4][0].ToString());
                            }
                            if (flag11 == true)
                            {
                                if (dt11.Rows[i][0].ToString().Contains("SLP") || dt11.Rows[i][0].ToString().Contains("SLB") || dt11.Rows[i][0].ToString().Contains("SLR") || dt11.Rows[i][0].ToString().Contains("SLT") || dt11.Rows[i][0].ToString().Contains("NANO"))
                                {
                                    if (dt11.Rows[i][0].ToString().Contains("SLR1"))
                                    {
                                        strImagePath11 = "~/Images/slr1.png";
                                    }
                                    if (dt11.Rows[i][0].ToString().Contains("SLR2"))
                                    {
                                        strImagePath11 = "~/Images/slr1.png";
                                    }
                                    if (dt11.Rows[i][0].ToString().Contains("SLT11"))
                                    {
                                        strImagePath11 = "~/Images/slt11.png";
                                    }
                                    if (dt11.Rows[i][0].ToString().Contains("SLT2"))
                                    {
                                        strImagePath11 = "~/Images/slt2.png";
                                    }
                                    if (dt11.Rows[i][0].ToString().Contains("SLB1"))
                                    {
                                        strImagePath11 = "~/Images/slb1.png";
                                    }
                                    if (dt11.Rows[i][0].ToString().Contains("SLP1"))
                                    {
                                        strImagePath11 = "~/Images/spg2.png";
                                    }
                                    dtDevices11.Rows.Add("", dt11.Rows[i][0].ToString(), dt11.Rows[i + 6][0].ToString(), strImagePath11);
                                    strDevicesList11 += dt11.Rows[i][0].ToString() + ",";
                                }
                            }
                            if (dt11.Rows[i][0].ToString().Contains("username"))
                            {
                                lbl11.Text = lbl11.Text + " - " + dt11.Rows[i + 1][0].ToString();
                            }
                        }
                        if (dtScenario11.Rows[0][11] != null && dtScenario11.Rows[0][10] != null && dtScenario11.Rows[0][11] != null)
                        {
                            lblRPI11IterationComp.Text = dtScenario11.Rows[0][11].ToString();
                            lblRPI11IterationPass.Text = PassCounter11.ToString();
                            lblRPI11IterationFailed.Text = FailCounter11.ToString();
                        }
                        imgRPI11Status.Visible = true;
                        imgRPI11Result.Visible = true;
                        lblRPI11TestName.Text = dtScenario11.Rows[0][2].ToString();
                        if (dtScenario11.Rows[0][2].ToString().ToLower().Contains("continuous") || dtScenario11.Rows[0][2].ToString().ToLower().Contains("infinite"))
                        {
                            imgRPI11Type.ImageUrl = "~/Images/infinite.png";
                            imgRPI11Type.Visible = true;
                        }
                        else
                        {
                            imgRPI11Type.ImageUrl = "~/Images/reg.gif";
                            imgRPI11Type.Visible = true;
                        }
                        lblRPI11DeviceList.Text = strDevicesList11;
                        lblRPI11Status.Text = dtScenario11.Rows[0][4].ToString();
                        if (dtScenario11.Rows[0][4].ToString().ToUpper() == "IN-PROGRESS")
                        {
                            rpi11Class.Attributes["class"] = "info-tiles tiles-success";
                            imgRPI11Status.Width = Unit.Pixel(74);
                            imgRPI11Status.Height = Unit.Pixel(64);
                            imgRPI11Status.ImageUrl = "~/Images/inprogress.gif";
                        }
                        if (dtScenario11.Rows[0][4].ToString().ToUpper() == "FAILED")
                        {
                            rpi11Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI11Status.Width = Unit.Pixel(74);
                            imgRPI11Status.Height = Unit.Pixel(64);
                            imgRPI11Status.ImageUrl = "~/Images/Failed.gif";
                        }
                        if (dtScenario11.Rows[0][4].ToString().ToUpper() == "FAIL")
                        {
                            rpi11Class.Attributes["class"] = "info-tiles tiles-danger";
                            imgRPI11Status.Width = Unit.Pixel(74);
                            imgRPI11Status.Height = Unit.Pixel(64);
                            imgRPI11Status.ImageUrl = "~/Images/inprogress.gif";
                            lblRPI11Status.Text = "IN-PROGRESS";
                        }
                        if (dtScenario11.Rows[0][4].ToString().ToUpper() == "PASSED")
                        {
                            rpi11Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI11Status.Width = Unit.Pixel(64);
                            imgRPI11Status.Height = Unit.Pixel(74);
                            imgRPI11Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario11.Rows[0][4].ToString().ToUpper() == "COMPLETED")
                        {
                            rpi11Class.Attributes["class"] = "info-tiles tiles-sky";
                            imgRPI11Status.Width = Unit.Pixel(64);
                            imgRPI11Status.Height = Unit.Pixel(74);
                            imgRPI11Status.ImageUrl = "~/Images/completed.png";

                        }
                        if (dtScenario11.Rows[0][8].ToString().Contains("Device_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(devLastModified11).TotalMinutes > 110)
                            {
                                FileInfo fileRPI11 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr11 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt");
                                    {
                                        String line11 = sr11.ReadToEnd();
                                        if (line11.Contains(myFile11.Name))
                                        {
                                            if (line11.Contains(myFile11.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI11 for the past 110 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr11.Close();
                                                        client.Send(mail);
                                                        FileStream file11 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt", FileMode.Append);
                                                        file11.Flush();
                                                        StreamWriter str11 = new StreamWriter(file11);
                                                        str11.Write(myFile11.Name + ";sent");
                                                        str11.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr11.Close();
                                                using (StreamWriter outputFile11 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt"))
                                                {
                                                    outputFile11.Flush();
                                                    outputFile11.Write(myFile11.Name);
                                                    outputFile11.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi11Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI11Status.Width = Unit.Pixel(74);
                                imgRPI11Status.Height = Unit.Pixel(64);
                                imgRPI11Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI11Status.Text = "No Response";
                            }
                        }
                        if (dtScenario11.Rows[0][8].ToString().Contains("Web-Mobile_Test_Automation"))
                        {
                            if (DateTime.Now.Subtract(webLastModified11).TotalMinutes > 110)
                            {
                                FileInfo fileRPI11 = new FileInfo(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt");
                                try
                                {   // Open the text file using a stream reader.
                                    StreamReader sr11 = new StreamReader(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt");
                                    {
                                        String line11 = sr11.ReadToEnd();
                                        if (line11.Contains(myFile11.Name))
                                        {
                                            if (line11.Contains(myFile11.Name + ";sent"))
                                            {

                                            }
                                            else
                                            {
                                                try
                                                {
                                                    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                                                    mail.To.Add("Kingston.SamSelwyn@bgch.co.uk");
                                                    mail.From = new MailAddress("Kingston.SamSelwyn@bgch.co.uk", "Email head", System.Text.Encoding.UTF8);
                                                    mail.Subject = "ATTENTION REQUIRED";
                                                    mail.SubjectEncoding = System.Text.Encoding.UTF8;
                                                    mail.Body = "No Response from RPI11 for the past 110 mins";
                                                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                                                    mail.IsBodyHtml = true;
                                                    mail.Priority = MailPriority.High;
                                                    SmtpClient client = new SmtpClient();
                                                    client.Credentials = new System.Net.NetworkCredential("Kingston.SamSelwyn@bgch.co.uk", "Bharath@123");
                                                    client.Port = 587;
                                                    client.Host = "smtp.gmail.com";
                                                    client.EnableSsl = true;
                                                    try
                                                    {
                                                        sr11.Close();
                                                        client.Send(mail);
                                                        FileStream file11 = File.Open(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt", FileMode.Append);
                                                        file11.Flush();
                                                        StreamWriter str11 = new StreamWriter(file11);
                                                        str11.Write(myFile11.Name + ";sent");
                                                        str11.Close();
                                                    }

                                                    catch
                                                    {

                                                    }

                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                sr11.Close();
                                                using (StreamWriter outputFile11 = new StreamWriter(@"C:\inetpub\wwwroot\HiveDashboard\HiveDashboard\Email Check/RPI11.txt"))
                                                {
                                                    outputFile11.Flush();
                                                    outputFile11.Write(myFile11.Name);
                                                    outputFile11.Close();
                                                }
                                            }
                                            catch
                                            {

                                            }

                                        }
                                    }
                                }
                                catch
                                {
                                }
                                rpi11Class.Attributes["class"] = "info-tiles tiles-toyo";
                                imgRPI11Status.Width = Unit.Pixel(74);
                                imgRPI11Status.Height = Unit.Pixel(64);
                                imgRPI11Status.ImageUrl = "~/Images/Failed.gif";
                                lblRPI11Status.Text = "No Response";
                            }
                        }
                    }
                    else
                    {
                        rpi11Class.Attributes["class"] = "info-tiles tiles-toyo";
                        imgRPI11Status.Width = Unit.Pixel(153);
                        imgRPI11Status.Height = Unit.Pixel(68);
                        imgRPI11Status.ImageUrl = "~/Images/comp.gif";
                        imgRPI11Status.Visible = true;
                    }

                    //Repeater1.DataSource = dtKit;
                    //Repeater1.DataBind();
                    Session["dtScenario11"] = dtScenario11;
                    #endregion
                }
                catch
                {
                    rpi11Class.Attributes["class"] = "info-tiles tiles-toyo";
                    imgRPI11Status.Width = Unit.Pixel(153);
                    imgRPI11Status.Height = Unit.Pixel(68);
                    imgRPI11Status.ImageUrl = "~/Images/comp.gif";
                    imgRPI11Status.Visible = true;
                }
            }
        }

        protected void imgRPI2Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario2"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI3Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario3"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI4Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario4"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI5Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario5"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI6Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario6"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI7Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario7"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI8Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario8"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI9Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario9"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI11Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario11"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void imgRPI1Result_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = (DataTable)Session["dtScenario1"];
            Response.Redirect(dt.Rows[0][8].ToString());
        }

        protected void rpi2_Click(object sender, EventArgs e)
        {
            Session["dtScenario"] = Session["dtScenario2"];
            Response.Redirect("~/TestManagement.aspx");
        }

        protected void imgIgnore2_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi2\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }

        }

        protected void imgIgnore7_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi7\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    
                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi7\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r","").Replace("\\n",""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore3_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi3\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi3\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore4_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi4\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi4\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore5_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi5\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi5\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore6_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi6\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi6\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore8_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi8\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi8\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore9_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi9\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi9\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore11_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi11\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi11\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }

        protected void imgIgnore1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtDevices = new DataTable();
            DataTable dtScenario = new DataTable();
            DataTable dtKit = new DataTable();
            dtKit.Columns.Add("Kit");
            dtKit.Columns.Add("CurrentStatus");
            JObject o7;
            //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
            DateTime webLastModified = DateTime.Now;
            DateTime devLastModified = DateTime.Now;
            var myFile = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileDev = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            var myFileWeb = new DirectoryInfo(@"\\nas1\hardware\").GetDirectories().First();
            if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\");
                myFileDev = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                devLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFileDev.Name + @"\results.json");

            }

            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists)
            {
                var directory = new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\");
                myFileWeb = (from f in directory.GetDirectories()
                             orderby f.LastWriteTime descending
                             select f).First();
                webLastModified = System.IO.File.GetLastWriteTime(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFileWeb.Name + @"\results.json");
            }
            if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists && (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists))
            {
                if (devLastModified > webLastModified)
                {
                    myFile = myFileDev;
                }
                else
                {
                    myFile = myFileWeb;
                }
            }
            else
                if (new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists)
                {
                    myFile = myFileWeb;
                }
                else
                    if (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists)
                    {
                        myFile = myFileDev;
                    }

            if ((new DirectoryInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\").Exists || (new DirectoryInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\").Exists)) && (DateTime.Now.Subtract(devLastModified).TotalMinutes < 7200 || DateTime.Now.Subtract(webLastModified).TotalMinutes < 7200))
            {
                JObject oJ7 = new JObject();
                if (new FileInfo(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Device_Test_Automation\Test_Results\rpi1\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));

                    strWrite.Dispose();
                }
                if (new FileInfo(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFile.Name + @"\results.json").Exists)
                {
                    using (StreamReader file = File.OpenText(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFile.Name + @"\results.json"))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            o7 = (JObject)JToken.ReadFrom(reader);
                            oJ7 = o7;
                        }
                    }
                    StreamWriter strWrite = new StreamWriter(@"\\nas1\hardware\Web-Mobile_Test_Automation\Test_Results\rpi1\" + myFile.Name + @"\results.json");
                    strWrite.Flush();
                    strWrite.Write(oJ7.ToString().Replace("\"FAIL\"", "\"IN-PROGRESS\"").Replace("\\r", "").Replace("\\n", ""));
                    strWrite.Dispose();

                }
            }
        }
    
    }
}