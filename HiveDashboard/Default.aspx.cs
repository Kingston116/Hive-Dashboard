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
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Hive-isopInternProdSC-FT-04-03FirmwareTest_SC-FT-04-03.HTML"));
                int Counter = (str.Replace("Counter :","£").Split('£').Length-1)/2;
                int PassCounter = 0;
                int FailCounter = 0;
                for(int i =1;i<Counter;i++)
                {
                    if(str.Replace("Counter :", "£").Split('£')[i * 2].Contains("FAIL"))
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
    }
}