using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;

namespace SCDR.AdminForms.AdminPanel
{
    [ToolboxItemAttribute(false)]
    public partial class AdminPanel : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public AdminPanel()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Rating
            decimal avgRating = Convert.ToDecimal(0);
            int rateCount = 0;
            using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
            {
                
                using (SPWeb oWeb = oSite.OpenWeb("en/"))
                {
                    SPList oList = oWeb.Lists["RatingAndFeedBack_List"];
                    SPListItemCollection oItems = oList.GetItems();
                    foreach (SPListItem li in oItems)
                    {
                        avgRating += Convert.ToDecimal(li["Rating"]);
                        rateCount++;
                    }
                }

                using (SPWeb oWeb = oSite.OpenWeb("ar/"))
                {
                    SPList oList = oWeb.Lists["RatingAndFeedBack_List"];
                    SPListItemCollection oItems = oList.GetItems();
                    foreach (SPListItem li in oItems)
                    {
                        avgRating += Convert.ToDecimal(li["Rating"]);
                        rateCount++;
                    }
                }

            }
            if(avgRating>0)
            {
                avgRating = avgRating / rateCount;
                avgRating = Math.Round(avgRating, 1);
                LblRate.Text = avgRating.ToString();
            }
            else
            {
                LblRate.Text = "No";
            }

            //Views
            using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
            {
                using (SPWeb oWeb = oSite.OpenWeb())
                {
                    SPList oList = oWeb.Lists["Statistics"];
                    SPListItemCollection oItems = oList.GetItems();
                    SPListItem viewCount = oItems[0];
                    lblViews.Text = viewCount["Stat"].ToString();
                }
            }

            //New Request
            NewRequestCount();
        }

        public void NewRequestCount()
        {
            int totalCount = 0;
            int englishCount=0;
            using (SPSite oSite = new SPSite(SPContext.Current.Web.Site.Url))
            {

                using (SPWeb oWeb = oSite.OpenWeb("en/"))
                {
                    SPList oList = oWeb.Lists["RatingAndFeedBack_List"];
                    SPListItemCollection oItems = oList.GetItems();
                    SPQuery query = new SPQuery();
                    query.Query = @"";            
                }
            }
        }
    }
}
