using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SCDR.CommonLogin
{
    [ToolboxItemAttribute(false)]
    public partial class CommonLogin : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]

        

       

        protected void lbInternalUsers_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (null != SPContext.Current && null != SPContext.Current.Site)
                {
                    SPIisSettings iisSettings = SPContext.Current.Site.WebApplication.IisSettings[SPUrlZone.Default];
                    if (null != iisSettings && iisSettings.UseWindowsClaimsAuthenticationProvider)
                    {
                        SPAuthenticationProvider provider = iisSettings.WindowsClaimsAuthenticationProvider;
                        Redirect(provider);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void Redirect(SPAuthenticationProvider provider)
        {
            string comp = HttpContext.Current.Request.Url.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped);
            string url = provider.AuthenticationRedirectionUrl.ToString();
            if (provider is SPWindowsAuthenticationProvider)
            {
                comp = EnsureUrl(comp, true);
            }

            SPUtility.Redirect(url, SPRedirectFlags.Default, this.Context, comp);
        }

        private string EnsureUrl(string url, bool urlIsQueryStringOnly)
        {
            if (!url.Contains("ReturnUrl="))
            {
                if (urlIsQueryStringOnly)
                {
                    url = url + (string.IsNullOrEmpty(url) ? "" : "&");
                }
                else
                {
                    url = url + ((url.IndexOf('?') == -1) ? "?" : "&");
                }
                url = url + "ReturnUrl=";
            }
            return url;
        }
        public CommonLogin()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
