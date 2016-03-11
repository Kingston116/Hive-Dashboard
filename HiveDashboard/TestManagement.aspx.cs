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

namespace HiveDashboard
{
    public partial class TestManagement : System.Web.UI.Page
    {
        DataTable dtDevices = new DataTable();
        DataTable dtScenario = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable dtKit = new DataTable();
                dtKit.Columns.Add("Kit");
                dtKit.Columns.Add("CurrentStatus");
                //JObject o2;
                //using (StreamReader file = File.OpenText(@"\\vmware-host\Shared Folders\Desktop\results.json"))
                //{
                //    using (JsonTextReader reader = new JsonTextReader(file))
                //    {
                //        o2 = (JObject)JToken.ReadFrom(reader);
                //    }
                //}
                DataTable dt = (DataTable)Session["dtScenario"];
                //dt.Columns.Add("Text");
                //using (JsonTextReader reader = new JsonTextReader(new StringReader(o2.ToString().Replace("\r\n", ""))))
                //{
                //    string temp = "";
                //    string tempKey = "";
                //    string tempValue = "";
                //    bool Type = false;
                //    bool repFlag = false;
                //    while (reader.Read())
                //    {
                //        if (reader.Value != null)
                //        {
                //            dt.Rows.Add(reader.Value.ToString());
                //        }
                //    }
                //}


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
                string strKit = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == 1)
                    {
                        dtKit.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i + 2][0].ToString());
                        strKit = dt.Rows[i][0].ToString();
                    }
                    if (dt.Rows[i][0].ToString().Contains("Scenario"))
                    {
                        flag = false;
                        dtScenario.Rows.Add(strKit, dt.Rows[i + 2][0].ToString(), dt.Rows[i + 4][0].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i + 6][0].ToString(), dt.Rows[i + 8][0].ToString(), dt.Rows[i + 10][0].ToString(), dt.Rows[i + 12][0].ToString(), "");
                    }
                    if (flag == true)
                    {
                        if (dt.Rows[i][0].ToString() == "Device" || dt.Rows[i][0].ToString() == "Device\t")
                        {
                            if (dt.Rows[i + 2][0].ToString() == "SLR1")
                            {
                                strImagePath = "~/Images/slr1.png";
                            }
                            if (dt.Rows[i + 2][0].ToString() == "SLR2")
                            {
                                strImagePath = "~/Images/slr1.png";
                            }
                            if (dt.Rows[i + 2][0].ToString() == "SLT3")
                            {
                                strImagePath = "~/Images/slt3.png";
                            }
                            if (dt.Rows[i + 2][0].ToString() == "SLT2")
                            {
                                strImagePath = "~/Images/slt2.png";
                            }
                            if (dt.Rows[i + 2][0].ToString() == "SLB1")
                            {
                                strImagePath = "~/Images/slb1.png";
                            }
                            if (dt.Rows[i + 2][0].ToString() == "SLP1")
                            {
                                strImagePath = "~/Images/spg2.png";
                            }
                            dtDevices.Rows.Add("", dt.Rows[i + 2][0].ToString(), dt.Rows[i + 4][0].ToString(), strImagePath);

                        }
                    }
                }
                Repeater1.DataSource = dtKit;
                Repeater1.DataBind();
                Session["dtScenario"] = dtScenario;

            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater r2 = (Repeater)e.Item.FindControl("Repeater2");
            r2.DataSource = dtDevices; // you'll have to query for appropriate data
            r2.DataBind();
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            dtScenario = (DataTable)Session["dtScenario"];
            var dtFilter = from p in dtScenario.AsEnumerable()
                           where (p.Field<string>("DeviceName") == "RPI0")
                           select new
                           {
                               ScenarioId = p.Field<string>("ScenarioId"),
                               ScenarioName = p.Field<string>("ScenarioName"),
                               ScenarioNo = p.Field<string>("ScenarioNo"),
                               Status = p.Field<string>("Status"),
                               TimeStamp = p.Field<string>("TimeStamp"),
                               Duration = p.Field<string>("Duration"),
                               FeatureFileName = p.Field<string>("FeatureFileName"),
                               ResultsLink = p.Field<string>("ResultsLink")
                           };
            Repeater3.DataSource = dtFilter.ToList();
            Repeater3.DataBind();

            foreach (DataRow row in GetData().Rows)
            {
                PieChart1.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                {
                    Category = row[0].ToString(),                    
                    Data = Convert.ToDecimal(row[1].ToString())
                     
                });
            }
        }
        
        public DataTable GetData()
        {
            DataTable dtOverallChart = (DataTable)Session["dtScenario"];
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Status");
            dtTemp.Columns.Add("Count");
            

            var Passed = from p in dtOverallChart.AsEnumerable()
                         where (p.Field<string>("DeviceName") == "RPI0" && p.Field<string>("Status") == "Passed")
                         select new
                         {
                             count = p.Field<string>("status").Count()
                         };
            dtTemp.Rows.Add("Passed", Passed.ToList().Count);
            var Failed = from p in dtOverallChart.AsEnumerable()
                         where (p.Field<string>("DeviceName") == "RPI0" && p.Field<string>("Status") == "Failed")
                         select new
                         {
                             count = p.Field<string>("status").Count()
                         };
            dtTemp.Rows.Add("Failed", Failed.ToList().Count);
            var InProgress = from p in dtOverallChart.AsEnumerable()
                             where (p.Field<string>("DeviceName") == "RPI0" && p.Field<string>("Status") == "InProgress")
                             select new
                             {
                                 count = p.Field<string>("status").Count()
                             };
            dtTemp.Rows.Add("InProgress", InProgress.ToList().Count);

            return dtTemp;
        }
        
    }
    

        
}

