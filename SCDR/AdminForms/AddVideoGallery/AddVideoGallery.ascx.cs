using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.IO;
using System.Text.RegularExpressions;

namespace SCDR.AdminForms.AddVideoGallery
{
    [ToolboxItemAttribute(false)]
    public partial class AddVideoGallery : WebPart
    {
        string subsiteName = string.Empty;
        public AddVideoGallery()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomVideoGallery";
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string url = txtVideoUrl.Text.Trim();
                if (IsYouTubeUrl(url) && txtTitle.Text != "")
                {

                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                        {
                            if (rbArabic.Checked)
                            {
                                subsiteName = "ar/";
                            }
                            else if (rbEnglish.Checked)
                            {
                                subsiteName = "en/";
                            }
                            using (SPWeb oWeb = oSite.OpenWeb(subsiteName))
                            {
                                SPList list = oWeb.Lists[ListName];
                                SPListItem item = list.Items.Add();
                                item["Title"] = txtTitle.Text;
                              
                              //  var result = url.Substring(url.LastIndexOf('=') + 1);
                                var result = GetYouTubeVideo(url);
                                item["VideoUrl"] = @"https://www.youtube.com/embed/" + result;
                                oWeb.AllowUnsafeUpdates = true;
                                item.Update();
                                oWeb.AllowUnsafeUpdates = false;

                            }
                        }
                    });

                    formClear();
                    string sMessage = "successfully completed";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddVideoGallery.aspx';</script>", false);


                }
                else
                {
                    string sMessage = "Invalid data found. Please try again";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');window.location='AddVideoGallery.aspx';</script>", false);

                }
            }
            catch (Exception ex)
            {

            }

        }
        //function for clearing the controls in form
        public void formClear()
        {
            txtVideoUrl.Text = "";
           

        }

        private static bool IsYouTubeUrl(string testUrl)
        {
            return TestUrl(@"^(http://youtu\.be/([a-zA-Z0-9]|_)+($|\?.*)|https?://www\.youtube\.com/watch\?v=([a-zA-Z0-9]|_)+($|&).*)", testUrl);
        }
        private static bool TestUrl(string pattern, string testUrl)
        {
            Regex l_expression = new Regex(pattern, RegexOptions.IgnoreCase);

            return l_expression.Matches(testUrl).Count > 0;
        }
        private static string GetYouTubeVideo(string testUrl)
        {
            return GetVideo(@"(/[^watch]|=)([a-zA-z0-9]|_)+($|(\?|&))", @"([a-zA-z0-9]|_)+", testUrl);
        }
        private static string GetVideo(string overallPattern, string videoPattern, string testUrl)
        {
            Regex l_overallExpression = new Regex(overallPattern, RegexOptions.IgnoreCase);
            MatchCollection l_overallMatches = l_overallExpression.Matches(testUrl);

            if (l_overallMatches.Count > 0)
            {
                Regex l_videoExpression = new Regex(videoPattern, RegexOptions.IgnoreCase);

                return l_videoExpression.Matches(l_overallMatches[0].Value)[0].Value;
            }
            else
            {
                return "";
            }
        }


      


    }
}
