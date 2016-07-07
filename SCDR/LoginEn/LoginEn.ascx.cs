using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Collections;
using System.DirectoryServices;

namespace SCDR.LoginEn
{
    [ToolboxItemAttribute(false)]
    public partial class LoginEn : WebPart
    {
        private string _path;
        private string _filterAttribute;
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public LoginEn()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.CurrentUser != null)
            {
                
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.AuthenticationType.ToUpper() != "FORMS")
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.RedirectToLoginPage();
            }
            else
            {
                HttpContext.Current.Response.Redirect("/_layouts/Signout.aspx");
            }
        }

        protected void btnSignin_Click(object sender, EventArgs e)
        {

        }

        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(_path,
            domainAndUsername, pwd);
            try
            {
                // Bind to the native AdsObject to force authentication.
                Object obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null == result)
                {
                    return false;
                }
                // Update the new path to the user in the directory
                _path = result.Path;
                _filterAttribute = (String)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }
            return true;
        }
       
    }
}
