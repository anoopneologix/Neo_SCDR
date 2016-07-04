﻿//--------------------------------------------------
// Project Name : SCDR
// Program Name : Rating And Feedback Control (Visual WebPart)
// Developed by : Sreejith C S
// Created Date : 08/04/2016
//---------------------------------------------------
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using System.Text.RegularExpressions;

namespace SCDR.RatingAndFeedBack
{
    [ToolboxItemAttribute(false)]
    public partial class RatingAndFeedBack : WebPart
    {
        
         public RatingAndFeedBack()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "RatingAndFeedBack_List";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("List Name:"),
        WebDescription("Please Enter a valid List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        private const string DefaultTitle = "Rating";
        private static string titleName = DefaultTitle;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultTitle),
        WebDisplayName("Title:"),
        WebDescription("Example : Rating")]
        public string TitleName
        {
            get { return titleName; }
            set { titleName = value; }
        }
        private const string DefaultQuestion = "How you rate our website?";
        private static string questionName = DefaultQuestion;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultQuestion),
        WebDisplayName("Question to display:"),
        WebDescription("Example : How You Rate Our Website")]

        public string QuestionName
        {
            get { return questionName; }
            set { questionName = value; }
        }
        private const string DefaultFeedback = "Write your feedback";
        private static string feedbackText = DefaultFeedback;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultFeedback),
        WebDisplayName("Question to display:"),
        WebDescription("Example : Write your feedback")]

        public string FeedbackText
        {
            get { return feedbackText; }
            set { feedbackText = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
              
                divShowFeedback.Visible = false;
                divAddComments.Visible = false;
                divButton.Visible = false;
                spanRating.InnerHtml = RatingAndFeedBack.titleName;
                headingRating.InnerHtml = RatingAndFeedBack.questionName;
                headingfeedback.InnerHtml = RatingAndFeedBack.feedbackText;
                BindAverageRating();
           ;
           
            }
        }

        // Function to call images from SharePoint Library
        // Bind those images to the Repeater/Slider
        public void BindAverageRating()
        {
            try
            {
              
                divAddComments.Visible = false;

                if (SPContext.Current.Web.CurrentUser != null)
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb("en/"))
                        {
                            string liveUser = string.Empty;
                            SPList oList = oWeb.Lists[ListName];
                            SPListItemCollection oItems = oList.GetItems();
                            //  imgStar.ImageUrl = oWeb.Url + "/_catalogs/masterpage/images/star.png";
                            decimal avgRating = Convert.ToDecimal(0);
                            decimal yourRate = Convert.ToDecimal(0);
                            if (oItems.Count > 0)
                            {
                                foreach (SPListItem li in oItems)
                                {
                                    avgRating += Convert.ToDecimal(li["Rating"]);
                                }
                                avgRating /= Convert.ToDecimal(oItems.Count);
                                if (avgRating > Convert.ToDecimal(2.2))
                                {
                                    decimal averageRate = Math.Round(avgRating, 1);
                                    averageRating.InnerHtml = "Average Rating : " + averageRate;
                                    //  input21c.Text = avgRating.ToString();
                                }
                                else
                                {
                                    averageRating.InnerHtml = "Average Rating : 2.2 ";
                                }
                            }
                            else
                            {
                                avgRating = Convert.ToDecimal(2.2);
                                averageRating.InnerHtml = "Average Rating : " + avgRating;
                                // input21c.Text = avgRating.ToString();
                            }
                            SPUser currentUser = oWeb.CurrentUser;
                            liveUser = currentUser.LoginName;
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + currentUser.LoginName + "</Value></Eq></Where>";
                            SPListItemCollection Items = oList.GetItems(query);
                            if (Items.Count > 0)
                            {
                                DataTable dt = new DataTable();
                                DataColumn dcUsername = new DataColumn("Username", typeof(string));
                                dt.Columns.Add(dcUsername);
                                DataColumn dcFeedback = new DataColumn("Feedback", typeof(string));
                                dt.Columns.Add(dcFeedback);
                                foreach (SPListItem li in Items)
                                {
                                    yourRate = Convert.ToDecimal(li["Rating"]);
                                    if (li["Feedback"].ToString() != null)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Username"] = li["Title"].ToString();
                                        dr["Feedback"] = li["Feedback"].ToString();
                                        dt.Rows.Add(dr);
                                    }
                                }
                               // repFeedback.DataSource = dt;
                               // repFeedback.DataBind();
                               // yourRating.InnerHtml = "Your Rating : " + yourRate;
                                input21b.Text = yourRate.ToString();
                                string liFeedback = dt.Rows[0]["Feedback"].ToString();
                                txtShowFeedBack.Text = Regex.Replace(liFeedback, "<.*?>", string.Empty);
                                txtShowFeedBack.ReadOnly = true;
                              //  divViewComments.Visible = true;
                                divShowFeedback.Visible = true;
                                divAddComments.Visible = false;
                                divButton.Visible = false;
                            }
                            else
                            {
                              //  yourRating.InnerHtml = "Your Rating : " + yourRate;
                            //    divViewComments.Visible = false;
                                txtShowFeedBack.Text = "";
                                divAddComments.Visible = true;
                                divButton.Visible = false;
                                divShowFeedback.Visible = false;
                            }

                        }
                    }
                }
                else
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
              {

                  using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
                  {
                      using (SPWeb oWeb = oSite.OpenWeb("en/"))
                      {
                          SPList oList = oWeb.Lists[ListName];
                          SPListItemCollection oItems = oList.GetItems();
                          //  imgStar.ImageUrl = oWeb.Url + "/_catalogs/masterpage/images/star.png";
                          decimal avgRating = Convert.ToDecimal(0);
                          decimal yourRate = Convert.ToDecimal(0);
                          if (oItems.Count > 0)
                          {
                              foreach (SPListItem li in oItems)
                              {
                                  avgRating += Convert.ToDecimal(li["Rating"]);
                              }
                              avgRating /= Convert.ToDecimal(oItems.Count);
                              if (avgRating > Convert.ToDecimal(2.2))
                              {
                                  decimal averageRate = Math.Round(avgRating, 1);
                                  averageRating.InnerHtml = "Average Rating : " + averageRate;
                                  //  input21c.Text = avgRating.ToString();
                              }
                              else
                              {
                                  averageRating.InnerHtml = "Average Rating : 2.2 ";
                              }
                          }
                          else
                          {
                              avgRating = Convert.ToDecimal(2.2);
                              averageRating.InnerHtml = "Average Rating : " + avgRating;
                              // input21c.Text = avgRating.ToString();
                          }
                          string ipaddress;
                          ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                          if (ipaddress == "" || ipaddress == null)
                          {
                              ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                          }
                          SPQuery query = new SPQuery();
                          query.Query = @"<Where><And><Eq><FieldRef Name='Host' /><Value Type='Text'>" + ipaddress + "</Value></Eq><Eq><FieldRef Name='Title'/><Value Type='Text'>Guest User</Value></Eq></And></Where>";
                          SPListItemCollection Items = oList.GetItems(query);
                          if (Items.Count > 0)
                          {
                              DataTable dt = new DataTable();
                              DataColumn dcUsername = new DataColumn("Username", typeof(string));
                              dt.Columns.Add(dcUsername);
                              DataColumn dcFeedback = new DataColumn("Feedback", typeof(string));
                              dt.Columns.Add(dcFeedback);
                              foreach (SPListItem li in Items)
                              {
                                  yourRate = Convert.ToDecimal(li["Rating"]);
                                  if (li["Feedback"]!=null)
                                  {
                                      DataRow dr = dt.NewRow();
                                      dr["Username"] = li["Title"].ToString();
                                      dr["Feedback"] = li["Feedback"].ToString();
                                      dt.Rows.Add(dr);
                                  }
                                  else
                                  {
                                    
                                  }
                              }
                           
                          
                              input21b.Text = yourRate.ToString();
                              divButton.Visible = false;
                              divShowFeedback.Visible = false;

                          }
                          else
                          {
                            
                              txtShowFeedBack.Text = "";
                              divAddComments.Visible = false;
                              divButton.Visible = true;
                              divShowFeedback.Visible = false;
                          }

                      }
                  }

              });
                }
             
            }
            catch (Exception ex)
            {

            }
        }

        

        protected void btnSUbmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ipaddress;
                ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                {
                    ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }


                using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb("en/"))
                    {
                            string feedback = string.Empty;
                            if (txtFeedback.Text.Trim() == string.Empty)
                            {
                                feedback = "no feedback received";
                            }
                            else
                            {
                                feedback = txtFeedback.Text.Trim();
                            }
                            SPUser currentUser = oWeb.CurrentUser;
                            SPList list = oWeb.Lists[ListName];
                            SPListItem item = list.Items.Add();
                            item["Title"] =currentUser.LoginName;
                            item["Rating"] = input21b.Text;
                            item["Host"] = ipaddress;
                            item["Feedback"] = feedback;
                            oWeb.AllowUnsafeUpdates = true;
                            item.Update();
                            oWeb.AllowUnsafeUpdates = false;
                            BindAverageRating();
                         //   BindAllComments();
                          //  string url = oWeb.Url + "en/SitePages/Home.aspx";
                     

                        }
                        string sMessage = "Thank you for rating our site";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>var ourLocation = document.URL;alert('" + sMessage + "');window.location.href=ourLocation;</script>", false);
                    }
               
           

            }
            catch(Exception ex)
            {
               // System.Web.HttpContext.Current.Response.Write("<script>alert('Already rated!');</script>");
            }
        }

        protected void btnAnonymosSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ipaddress;
                ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                {
                    ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                SPSecurity.RunWithElevatedPrivileges(delegate()
             {

                 using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
                 {
                     using (SPWeb oWeb = oSite.OpenWeb("en/"))
                     {
                        
                         SPUser currentUser = oWeb.CurrentUser;
                         SPList list = oWeb.Lists[ListName];
                         SPListItem item = list.Items.Add();
                         item["Title"] = "Guest User";
                         item["Rating"] = input21b.Text;
                         item["Host"] = ipaddress;
                         oWeb.AllowUnsafeUpdates = true;
                         item.Update();
                         oWeb.AllowUnsafeUpdates = false;
                         BindAverageRating();
                     

                     }
                 }
                 string sMessage = "Thank you for rating our site";
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>var ourLocation = document.URL;alert('" + sMessage + "');window.location.href=ourLocation;</script>", false);


             });

            }
            catch (Exception ex)
            {
            }

        }





    }
}
