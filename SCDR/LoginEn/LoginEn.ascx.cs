using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections;
using System.DirectoryServices;
using System.Web.UI;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.IdentityModel;
using System.IdentityModel.Tokens;
using Microsoft.SharePoint.Administration.Claims;
using System.IdentityModel.Services;
using Microsoft.SharePoint.Utilities;
using System.Security.Principal;
using System.Net;
using System.Net.Mail;
using Microsoft.SharePoint.Administration;

using Microsoft.Office.Server.UserProfiles;


namespace SCDR.LoginEn
{
    [ToolboxItemAttribute(false)]
    public partial class LoginEn : WebPart
    {

      
        public LoginEn()
        {
        }

        protected override void OnInit(EventArgs e)
        {
          
            base.OnInit(e);
            InitializeControl();
        }
        #region CustomWebPartProperty
        private const string DefaultListName = "ADUserDetailsList";
        private static string listName = DefaultListName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultListName),
        WebDisplayName("List Name:"),
        WebDescription("Please Enter a valid List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        #endregion

        /// <summary>
        /// function fires when the page loads
        /// check for logged in users'
        /// set visibility of login division
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
   
        /// <summary>
        /// fires on button "sign in" click
        /// perform ldap authentication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void btnSignin_Click1(object sender, EventArgs e)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if(val1)
            {
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

                            HttpContext.Current.Response.Redirect("~/sites/SCDR/en/SitePages/Home.aspx");
                        }
                        oWeb.AllowUnsafeUpdates = false;
                    }


                }
            }
            else 
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                Page.Response.Redirect("/sites/SCDR/en/_layouts/15/Authenticate.aspx?Source=" + path);
                //Page.Response.Redirect(@"/sites/SCDR/_layouts/15/Authenticate.aspx?Source==%2fsites%2fSCDR%2fen%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Fen%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Fen%2FSitePages%2FHome%2Easp");
            }
            
          
            



        }


     
       
    }
}
