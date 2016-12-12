using Microsoft.IdentityModel.Web;
using Microsoft.Office.Server.UserProfiles;
using Microsoft.SharePoint;
using Microsoft.SharePoint.IdentityModel;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace SCDR.LoginAr
{
    [ToolboxItemAttribute(false)]
    public partial class LoginAr : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public LoginAr()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            //{
            //    if (SPContext.Current.Web.CurrentUser != null)
            //    {
            //        hfloginstatus.Value = "True";
            //        GetCurrentUserDetails();
            //    }
            //    else
            //    {
            //        hfloginstatus.Value = "False";
            //    }
            //}
        }

      
        protected void btnSignin_Click1(object sender, EventArgs e)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1)
            {
                //Page.Response.Redirect("/sites/SCDR/ar/_layouts/15/SignOut.aspx");
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb oWeb = oSite.OpenWeb())
                    {


                        oWeb.AllowUnsafeUpdates = true;
                        if (HttpContext.Current.User.Identity.AuthenticationType.ToUpper() != "FORMS")
                        {
                            FormsAuthentication.SignOut();
                            SPFederationAuthenticationModule.FederatedSignOut(new Uri(SPContext.Current.Web.Url + "/SitePages/Home.aspx"), new Uri(SPContext.Current.Web.Url + "/SitePages/Home.aspx"));
                        }
                        else
                        {

                            HttpContext.Current.Response.Redirect("~/sites/SCDR/ar/SitePages/Home.aspx");
                        }
                        oWeb.AllowUnsafeUpdates = false;
                    }


                }
            }
            else
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                Page.Response.Redirect("/sites/SCDR/ar/_layouts/15/Authenticate.aspx?Source=" + path);
                //Page.Response.Redirect(@"/sites/SCDR/_layouts/15/Authenticate.aspx?Source==%2fsites%2fSCDR%2fen%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Fen%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Fen%2FSitePages%2FHome%2Easp");
            }


        }


    }
}
