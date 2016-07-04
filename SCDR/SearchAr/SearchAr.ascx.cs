using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Web.UI;
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.Administration;
using System.Linq;

namespace SCDR.SearchAr
{
    [ToolboxItemAttribute(false)]
    public partial class SearchAr : WebPart
    {
        public SearchAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            /**/
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
            {
                string siteUrl = oSite.Url;
                if (txtSearch.Text != "")
                {
                    Page.Response.Redirect(siteUrl + "/ar/SitePages/SearchResult.aspx?kw=" + txtSearch.Text.Trim());
                }
            }
        }
    }
}
