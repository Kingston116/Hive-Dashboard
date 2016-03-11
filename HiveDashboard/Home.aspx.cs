using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WinHttp;
using System.IO;
using System.Data;
using NReco.PivotData;
using System.Collections;
using HtmlAgilityPack;
using System.Text.RegularExpressions;


namespace HiveDashboard
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region GetData

                WinHttpRequest oReq = new WinHttpRequest();
                oReq.Open("GET", "https://jira.bgchtest.info/sr/jira.issueviews:searchrequest-xml/temp/SearchRequest.xml?jqlQuery=issuetype%20%3D%20Bug%20AND%20labels%20%3D%20NGD_POST_LAUNCH%20ORDER%20BY%20priority%20DESC");
                ///AND%20status%20in%20(Open%2C%20Resolved%2C%20\"In%20QA\")%20
                oReq.SetCredentials("Kingston", "Bharath@123", 0);
                oReq.SetRequestHeader("Authorization", "Basic");
                oReq.SetRequestHeader("Content-Type", "application/xml");
                oReq.Send();
                string xml = oReq.ResponseText;

                StringReader oRdr = new StringReader(xml);
                DataSet oDst = new DataSet();
                oDst.ReadXml(oRdr);
                DataTable oDt = oDst.Tables[4];
                DataTable oDrReport = oDst.Tables[12];
                DataTable oDrStatus = oDst.Tables[8];
                DataTable oDrProj = oDst.Tables[5];
                DataTable oDrST = oDst.Tables[9];
                oDt.Columns.Add("Reporter");
                oDt.Columns.Add("Priority");
                oDt.Columns.Add("Project");
                oDt.Columns.Add("Status");
                oDt.Columns.Remove("Description");
                oDt.Columns.Remove("Environment");
                oDt.Columns.Remove("Due");
                oDt.Columns.Remove("Votes");
                oDt.Columns.Remove("Watches");

                for (int i = 0; i < oDt.Rows.Count; i++)
                {
                    try
                    {
                        if (oDt.Rows[i]["fixVersion"] == null)
                        {
                            oDt.Rows[i]["fixVersion"] = "-";
                        }
                    }
                    catch
                    {

                    }
                    oDt.Rows[i][oDt.Columns.Count - 4] = oDrReport.Rows[i][1];
                    oDt.Rows[i][oDt.Columns.Count - 3] = oDrStatus.Rows[i][2];
                    oDt.Rows[i][oDt.Columns.Count - 2] = oDrProj.Rows[i][2];
                    oDt.Rows[i][oDt.Columns.Count - 1] = oDrST.Rows[i][2];
                }

                foreach (DataRow dr in oDt.Rows)
                {
                    dr["created"] = Convert.ToDateTime(dr["created"]).ToShortDateString();
                    dr["updated"] = Convert.ToDateTime(dr["updated"]).ToShortDateString();
                    try
                    {
                        dr["resolved"] = Convert.ToDateTime(dr["resolved"]).ToShortDateString();
                    }
                    catch
                    {

                    }

                }

                #endregion

                #region Open Bugs Project Wise

                int AndCt = 0;
                var ct = from p in oDt.AsEnumerable()
                         where (p.Field<string>("Project") == "Hive Android" && p.Field<string>("status") == "Open")
                         select new
                         {
                             Project = p.Field<string>("Project").Count()
                         };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblAndOpenBugs.Text = AndCt.ToString();
                }
                else
                {
                    lblAndOpenBugs.Text = "0";
                }

                int AndIOS = 0;
                var ctIOS = from p in oDt.AsEnumerable()
                            where (p.Field<string>("Project") == "Hive iOS" && p.Field<string>("status") == "Open") 
                            select new
                            {
                                Project = p.Field<string>("Project").Count()
                            };
                if (ctIOS.ToArray().Length > 0)
                {
                    AndIOS = ctIOS.ToArray().Length;
                    lblIOSOpenBugs.Text = AndIOS.ToString();
                }
                else
                {
                    lblIOSOpenBugs.Text = "0";
                }
                int AndctWeb = 0;
                var ctWeb = from p in oDt.AsEnumerable()
                            where (p.Field<string>("Project") == "Hive Web" && p.Field<string>("status") == "Open")
                            select new
                            {
                                Project = p.Field<string>("Project").Count()
                            };
                if (ctWeb.ToArray().Length > 0)
                {
                    AndctWeb = ctWeb.ToArray().Length;
                    lblWebOpenBugs.Text = AndctWeb.ToString();
                }
                else
                {
                    lblWebOpenBugs.Text = "0";
                }

                int AndctNGD = 0;
                var ctNGD = from p in oDt.AsEnumerable()
                            where (p.Field<string>("Project") == "NGD (External)" && p.Field<string>("status") == "Open")
                            select new
                            {
                                Project = p.Field<string>("Project").Count()
                            };
                if (ctNGD.ToArray().Length > 0)
                {
                    AndctNGD = ctNGD.ToArray().Length;
                    lblNGDOpenBug.Text = AndctNGD.ToString();
                }
                else
                {
                    lblNGDOpenBug.Text = "0";
                }

#endregion

                #region Open Bugs Priority Wise

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("Priority") == "Blocker" && p.Field<string>("status") == "Open")
                         select new
                         {
                             Project = p.Field<string>("Project").Count()
                         };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblBlocker.Text = AndCt.ToString();
                }
                else
                {
                    lblBlocker.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("Priority") == "Critical" && p.Field<string>("status") == "Open")
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblCritical.Text = AndCt.ToString();
                }
                else
                {
                    lblCritical.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("Priority") == "Major" && p.Field<string>("status") == "Open")
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblMajor.Text = AndCt.ToString();
                }
                else
                {
                    lblMajor.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("Priority") == "Minor" && p.Field<string>("status") == "Open")
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblMinor.Text = AndCt.ToString();
                }
                else
                {
                    lblMinor.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("Priority") == "Trivial" && p.Field<string>("status") == "Open")
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblTrivial.Text = AndCt.ToString();
                }
                else
                {
                    lblTrivial.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where ( p.Field<string>("status") == "Open")
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblTotalOpen.Text = AndCt.ToString();
                }
                else
                {
                    lblTotalOpen.Text = "0";
                }

                #endregion

                #region Header Tiles
                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("created") == DateTime.Now.ToShortDateString())
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblBugs.Text = AndCt.ToString();
                }
                else
                {
                    lblBugs.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("status") == "Open")
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblOpenBug.Text = AndCt.ToString();
                }
                else
                {
                    lblOpenBug.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("Priority") == "Blocker" && (Convert.ToDateTime(p.Field<string>("created")) >= Convert.ToDateTime(DateTime.Now.AddDays(-8))))
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblP1.Text = AndCt.ToString();
                }
                else
                {
                    lblP1.Text = "0";
                }

                AndCt = 0;
                ct = from p in oDt.AsEnumerable()
                     where (p.Field<string>("Status") == "Resolved" || p.Field<string>("status") == "In QA")
                     select new
                     {
                         Project = p.Field<string>("Project").Count()
                     };
                if (ct.ToArray().Length > 0)
                {
                    AndCt = ct.ToArray().Length;
                    lblQA.Text = AndCt.ToString();
                }
                else
                {
                    lblQA.Text = "0";
                }
#endregion

                #region Resource Level
                var pivotData = new PivotData(new string[] { "Reporter", "Created" }, new CountAggregatorFactory(), new DataTableReader(oDt));
                var pvtTbl = new PivotTable(new[] { "Reporter" }, new[] { "Created" }, pivotData);

                var htmlResultStatus = new StringWriter();
                var pvt = new NReco.PivotData.Output.PivotTableHtmlWriter(htmlResultStatus);
                pvt.Write(pvtTbl);
                //htmlPivot.InnerHtml = htmlResult.ToString().Replace("<table>", "<table class=\"table table-tools table-hover dataTable no-footertable table-tools table-hover dataTable no-footer\">");
                String[] strHtml = htmlResultStatus.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None);
                string strHead = htmlResultStatus.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None)[0].Replace("tr>", "th>");
                string result = "";
                result = strHead + "</th>";
                for (int i = 1; i < strHtml.Length; i++)
                {
                    result += strHtml[i].ToString() + "</tr>";
                }
                result.Replace("</table></tr>", "</table>");
                DataSet odtWeek = new DataSet();
                odtWeek = ConvertHTMLTablesToDataSet(htmlResultStatus.ToString().Replace('{', ' ').Replace('}', ' ').Replace("<td />", "<td></td>").Replace("<th rowspan=\"1\" colspan=\"1\" />", "<th rowspan=\"1\" colspan=\"1\" >Name</th>"));
                //tblResource.InnerHtml = htmlResultStatus.ToString().Replace("<table","<table class=\"table\" ");
                grdResource.DataSource = odtWeek;
                for (int i = odtWeek.Tables[0].Columns.Count-2; i > 0 ;i-- )
                {
                    odtWeek.Tables[0].Columns.RemoveAt(i);
                }
                    grdResource.DataBind();


#endregion

                #region Bug Data

                    var pivotDataST = new PivotData(new string[] { "Status", "Project" }, new CountAggregatorFactory(), new DataTableReader(oDt));
                    var pvtTblST = new PivotTable(new[] { "Status" }, new[] { "Project" }, pivotDataST);

                    var htmlResultST = new StringWriter();
                    var pvtST = new NReco.PivotData.Output.PivotTableHtmlWriter(htmlResultST);
                    pvtST.Write(pvtTblST);
                    
                    String[] strHtmlST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None);
                    string strHeadST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None)[0].Replace("tr>", "th>");
                    result = "";
                    result = strHeadST + "</th>";
                    for (int i = 1; i < strHtmlST.Length; i++)
                    {
                        result += strHtmlST[i].ToString() + "</tr>";
                    }
                    result.Replace("</table></tr>", "</table>");
                    DataSet oStST = new DataSet();
                    oStST = ConvertHTMLTablesToDataSet(htmlResultST.ToString().Replace('{', ' ').Replace('}', ' ').Replace("<td />", "<td></td>").Replace("<th rowspan=\"1\" colspan=\"1\" />", "<th rowspan=\"1\" colspan=\"1\" >Name</th>"));
                    grdBug.DataSource = oStST;
                    grdBug.DataBind();

                    #endregion

                #region Bug Monthly Data

                    DataTable odtMonthTemp = new DataTable();
                    odtMonthTemp.Columns.Add("Project");
                    odtMonthTemp.Columns.Add("Priority");
                    odtMonthTemp.Columns.Add("Status");
                    odtMonthTemp.Columns.Add("created");
                    
                    var oDVct = from p in oDt.AsEnumerable()
                                where ((Convert.ToDateTime(p.Field<string>("created")) <= DateTime.Now && Convert.ToDateTime(p.Field<string>("created")) >= DateTime.Now.AddMonths(-1)))
                         select new
                         {
                Priority = p.Field<string>("Priority"),
                Project = p.Field<string>("Project"),
                Status = p.Field<string>("Status"),
                created = p.Field<string>("created")
                         };
                    DataRow row = null;
                    foreach (var rowObj in oDVct)
                    {
                        row = odtMonthTemp.NewRow();
                        odtMonthTemp.Rows.Add(rowObj.Project, rowObj.Priority,rowObj.Status,rowObj.created);
                    }

                    PivotData pivotDataBug = new PivotData(new string[] { "Status", "Project" }, new CountAggregatorFactory(), new DataTableReader(odtMonthTemp));
                    PivotTable pvtTblBug = new PivotTable(new[] { "Status" }, new[] { "Project" }, pivotDataBug);

                    StringWriter htmlResultBug = new StringWriter();
                    var pvtBug = new NReco.PivotData.Output.PivotTableHtmlWriter(htmlResultBug);
                    pvtBug.Write(pvtTblBug);

                    String[] strHtmlBug = htmlResultBug.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None);
                    string strHeadBug = htmlResultBug.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None)[0].Replace("tr>", "th>");
                    result = "";
                    result = strHeadBug + "</th>";
                    for (int i = 1; i < strHtmlBug.Length; i++)
                    {
                        result += strHtmlBug[i].ToString() + "</tr>";
                    }
                    result.Replace("</table></tr>", "</table>");
                    DataSet oStBug = new DataSet();
                    oStBug = ConvertHTMLTablesToDataSet(htmlResultBug.ToString().Replace('{', ' ').Replace('}', ' ').Replace("<td />", "<td></td>").Replace("<th rowspan=\"1\" colspan=\"1\" />", "<th rowspan=\"1\" colspan=\"1\" >Name</th>"));
                    grdBugMonth.DataSource = oStBug;
                    grdBugMonth.DataBind();

                    #endregion

                #region Bug NGDE Data

                    DataTable odtNGDE = new DataTable();
                    odtNGDE.Columns.Add("Project");
                    odtNGDE.Columns.Add("Priority");
                    odtNGDE.Columns.Add("Status");
                    odtNGDE.Columns.Add("resolved");

                    var oNGD = from p in oDt.AsEnumerable()
                                where (p.Field<string>("Project") == "NGD (External)")
                               select new
                                {
                                    Priority = p.Field<string>("Priority"),
                                    Project = p.Field<string>("Project"),
                                    Status = p.Field<string>("Status"),
                                    resolved = p.Field<DateTime>("resolved")
                                };
                    row = null;
                    foreach (var rowObj in oNGD)
                    {
                        row = odtNGDE.NewRow();
                        odtNGDE.Rows.Add(rowObj.Project, rowObj.Priority, rowObj.Status, rowObj.resolved);
                    }

                    pivotDataST = new PivotData(new string[] { "Status", "Priority" }, new CountAggregatorFactory(), new DataTableReader(odtNGDE));
                    pvtTblST = new PivotTable(new[] { "Status" }, new[] { "Priority" }, pivotDataST);

                    htmlResultST = new StringWriter();
                    pvtST = new NReco.PivotData.Output.PivotTableHtmlWriter(htmlResultST);
                    pvtST.Write(pvtTblST);

                    strHtmlST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None);
                    strHeadST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None)[0].Replace("tr>", "th>");
                    result = "";
                    result = strHeadST + "</th>";
                    for (int i = 1; i < strHtmlST.Length; i++)
                    {
                        result += strHtmlST[i].ToString() + "</tr>";
                    }
                    result.Replace("</table></tr>", "</table>");
                    oStST = new DataSet();
                    oStST = ConvertHTMLTablesToDataSet(htmlResultST.ToString().Replace('{', ' ').Replace('}', ' ').Replace("<td />", "<td></td>").Replace("<th rowspan=\"1\" colspan=\"1\" />", "<th rowspan=\"1\" colspan=\"1\" >Name</th>"));
                    grdNGD.DataSource = oStST;
                    grdNGD.DataBind();

                    #endregion

                #region Bug AND Data

                    DataTable odtAnd = new DataTable();
                    odtAnd.Columns.Add("Project");
                    odtAnd.Columns.Add("Priority");
                    odtAnd.Columns.Add("Status");
                    odtAnd.Columns.Add("resolved");

                     var oAND = from p in oDt.AsEnumerable()
                               where (p.Field<string>("Project") == "Hive Android")
                               select new
                               {
                                   Priority = p.Field<string>("Priority"),
                                   Project = p.Field<string>("Project"),
                                   Status = p.Field<string>("Status"),
                                   resolved = p.Field<string>("resolved")
                               };
                    row = null;
                    foreach (var rowObj in oAND)
                    {
                        row = odtAnd.NewRow();
                        odtAnd.Rows.Add(rowObj.Project, rowObj.Priority, rowObj.Status, rowObj.resolved);
                    }

                    pivotDataST = new PivotData(new string[] { "Status", "Priority" }, new CountAggregatorFactory(), new DataTableReader(odtAnd));
                    pvtTblST = new PivotTable(new[] { "Status" }, new[] { "Priority" }, pivotDataST);

                    htmlResultST = new StringWriter();
                    pvtST = new NReco.PivotData.Output.PivotTableHtmlWriter(htmlResultST);
                    pvtST.Write(pvtTblST);

                    strHtmlST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None);
                    strHeadST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None)[0].Replace("tr>", "th>");
                    result = "";
                    result = strHeadST + "</th>";
                    for (int i = 1; i < strHtmlST.Length; i++)
                    {
                        result += strHtmlST[i].ToString() + "</tr>";
                    }
                    result.Replace("</table></tr>", "</table>");
                    oStST = new DataSet();
                    oStST = ConvertHTMLTablesToDataSet(htmlResultST.ToString().Replace('{', ' ').Replace('}', ' ').Replace("<td />", "<td></td>").Replace("<th rowspan=\"1\" colspan=\"1\" />", "<th rowspan=\"1\" colspan=\"1\" >Name</th>"));
                    grdAnd.DataSource = oStST;
                    grdAnd.DataBind();

                    #endregion

                #region Bug IOS Data

                    DataTable odtIOS = new DataTable();
                    odtIOS.Columns.Add("Project");
                    odtIOS.Columns.Add("Priority");
                    odtIOS.Columns.Add("Status");
                    odtIOS.Columns.Add("resolved");

                    var oIOS = from p in oDt.AsEnumerable()
                               where (p.Field<string>("Project") == "Hive iOS")
                               select new
                               {
                                   Priority = p.Field<string>("Priority"),
                                   Project = p.Field<string>("Project"),
                                   Status = p.Field<string>("Status"),
                                   resolved = p.Field<string>("resolved")
                               };
                    row = null;
                    foreach (var rowObj in oIOS)
                    {
                        row = odtIOS.NewRow();
                        odtIOS.Rows.Add(rowObj.Project, rowObj.Priority, rowObj.Status, rowObj.resolved);
                    }

                    pivotDataST = new PivotData(new string[] { "Status", "Priority" }, new CountAggregatorFactory(), new DataTableReader(odtIOS));
                    pvtTblST = new PivotTable(new[] { "Status" }, new[] { "Priority" }, pivotDataST);

                    htmlResultST = new StringWriter();
                    pvtST = new NReco.PivotData.Output.PivotTableHtmlWriter(htmlResultST);
                    pvtST.Write(pvtTblST);

                    strHtmlST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None);
                    strHeadST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None)[0].Replace("tr>", "th>");
                    result = "";
                    result = strHeadST + "</th>";
                    for (int i = 1; i < strHtmlST.Length; i++)
                    {
                        result += strHtmlST[i].ToString() + "</tr>";
                    }
                    result.Replace("</table></tr>", "</table>");
                    oStST = new DataSet();
                    oStST = ConvertHTMLTablesToDataSet(htmlResultST.ToString().Replace('{', ' ').Replace('}', ' ').Replace("<td />", "<td></td>").Replace("<th rowspan=\"1\" colspan=\"1\" />", "<th rowspan=\"1\" colspan=\"1\" >Name</th>"));
                    grdIOS.DataSource = oStST;
                    grdIOS.DataBind();

                    #endregion

                #region Bug Web Data

                    DataTable odtWeb = new DataTable();
                    odtWeb.Columns.Add("Project");
                    odtWeb.Columns.Add("Priority");
                    odtWeb.Columns.Add("Status");
                    odtWeb.Columns.Add("resolved");

                    var oWeb = from p in oDt.AsEnumerable()
                               where (p.Field<string>("Project") == "Hive Web")
                               select new
                               {
                                   Priority = p.Field<string>("Priority"),
                                   Project = p.Field<string>("Project"),
                                   Status = p.Field<string>("Status"),
                                   resolved = p.Field<string>("resolved")
                               };
                    row = null;
                    foreach (var rowObj in oWeb)
                    {
                        row = odtWeb.NewRow();
                        odtWeb.Rows.Add(rowObj.Project, rowObj.Priority, rowObj.Status, rowObj.resolved);
                    }

                    pivotDataST = new PivotData(new string[] { "Status", "Priority" }, new CountAggregatorFactory(), new DataTableReader(odtWeb));
                    pvtTblST = new PivotTable(new[] { "Status" }, new[] { "Priority" }, pivotDataST);

                    htmlResultST = new StringWriter();
                    pvtST = new NReco.PivotData.Output.PivotTableHtmlWriter(htmlResultST);
                    pvtST.Write(pvtTblST);

                    strHtmlST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None);
                    strHeadST = htmlResultST.ToString().Split(new string[] { "</tr>" }, StringSplitOptions.None)[0].Replace("tr>", "th>");
                    result = "";
                    result = strHeadST + "</th>";
                    for (int i = 1; i < strHtmlST.Length; i++)
                    {
                        result += strHtmlST[i].ToString() + "</tr>";
                    }
                    result.Replace("</table></tr>", "</table>");
                    oStST = new DataSet();
                    oStST = ConvertHTMLTablesToDataSet(htmlResultST.ToString().Replace('{', ' ').Replace('}', ' ').Replace("<td />", "<td></td>").Replace("<th rowspan=\"1\" colspan=\"1\" />", "<th rowspan=\"1\" colspan=\"1\" >Name</th>"));
                    grdWeb.DataSource = oStST;
                    grdWeb.DataBind();

                    #endregion
            }


        }


        public DataSet ConvertHTMLTablesToDataSet(string HTML)
        {
            try
            {
                // Declarations 
                DataSet ds = new DataSet();
                DataTable dt = null;
                DataRow dr = null;
                DataColumn dc = null;
                string TableExpression = "<table[^>]*>(.*?)</table>";
                string HeaderExpression = "<th[^>]*>(.*?)</th>";
                string RowExpression = "<tr[^>]*>(.*?)</tr>";
                string ColumnExpression = "<td[^>]*>(.*?)</td>";
                bool HeadersExist = false;
                int iCurrentColumn = 0;
                int iCurrentRow = 0;

                // Get a match for all the tables in the HTML 
                MatchCollection Tables = Regex.Matches(HTML, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Loop through each table element 
                foreach (Match Table in Tables)
                {
                    // Reset the current row counter and the header flag 
                    iCurrentRow = 0;
                    HeadersExist = false;

                    // Add a new table to the DataSet 
                    dt = new DataTable();

                    //Create the relevant amount of columns for this table (use the headers if they exist, otherwise use default names) 
                    if (Table.Value.Contains("<th"))
                    {
                        // Set the HeadersExist flag 
                        HeadersExist = true;

                        // Get a match for all the rows in the table 
                        MatchCollection Headers = Regex.Matches(Table.Value, HeaderExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                        // Loop through each header element 
                        foreach (Match Header in Headers)
                        {
                            if (!Header.Groups[1].ToString().Contains('/'))
                            {
                                if (Header.ToString().Contains("totals pvtColumn") || Header.ToString().Contains("Name") || Header.ToString().Contains(">Blocker<") || Header.ToString().Contains(">Critical<") || Header.ToString().Contains(">Major<") || Header.ToString().Contains(">Minor<") || Header.ToString().Contains(">Trivial<") || Header.ToString().Contains("Hive Android") || Header.ToString().Contains("Hive iOS") || Header.ToString().Contains("Hive Web"))
                                {
                                    dt.Columns.Add(Header.Groups[1].ToString());
                                }
                                else
                                {
                                    Header.ToString().Replace("<th", "<td").Replace("th>", "td>");
                                }

                            }
                            else
                            {
                                dt.Columns.Add(Header.Groups[1].ToString());
                            }
                        }
                    }
                    else
                    {

                        for (int iColumns = 1; iColumns <= Regex.Matches(Regex.Matches(Regex.Matches(Table.Value, TableExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase)[0].ToString(), ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase).Count; iColumns++)
                        {
                            dt.Columns.Add("Column " + iColumns);
                        }
                    }


                    //Get a match for all the rows in the table 

                    MatchCollection Rows = Regex.Matches(Table.Value, RowExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                    // Loop through each row element 
                    foreach (Match Row in Rows)
                    {

                        // Only loop through the row if it isn't a header row 
                        if (!(iCurrentRow == 0 && HeadersExist))
                        {
                            // Create a new row and reset the current column counter 
                            dr = dt.NewRow();
                            iCurrentColumn = 0;

                            // Get a match for all the columns in the row 
                            MatchCollection Columns = Regex.Matches(Row.Value.Replace("<th", "<td").Replace("th>", "td>"), ColumnExpression, RegexOptions.Multiline | RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            // Loop through each column element 
                            foreach (Match Column in Columns)
                            {
                                // Add the value to the DataRow 
                                dr[iCurrentColumn] = Column.Groups[1].ToString();

                                // Increase the current column  
                                iCurrentColumn++;
                            }

                            // Add the DataRow to the DataTable 
                            dt.Rows.Add(dr);

                        }

                        // Increase the current row counter 
                        iCurrentRow++;
                    }


                    // Add the DataTable to the DataSet 
                    ds.Tables.Add(dt);

                }

                return ds;
            }
            catch
            {
                DataSet ds = new DataSet();
                return ds;
            }

        }


        DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue)
        {
            // find primary key columns 
            //(i.e. everything but pivot column and pivot value)
            DataTable temp = dt.Copy();
            temp.Columns.Remove(pivotColumn.ColumnName);
            temp.Columns.Remove(pivotValue.ColumnName);
            string[] pkColumnNames = temp.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();

            // prep results table
            DataTable result = temp.DefaultView.ToTable(true, pkColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();
            dt.AsEnumerable()
                .Select(r => r[pivotColumn.ColumnName].ToString())
                .Distinct().ToList()
                .ForEach(c => result.Columns.Add(c, pivotColumn.DataType));

            // load it
            foreach (DataRow row in dt.Rows)
            {
                // find row to update
                DataRow aggRow = result.Rows.Find(
                    pkColumnNames
                        .Select(c => row[c])
                        .ToArray());
                // the aggregate used here is LATEST 
                // adjust the next line if you want (SUM, MAX, etc...)
                aggRow[row[pivotColumn.ColumnName].ToString()] = row[pivotValue.ColumnName];
            }

            return result;
        }

    }
}