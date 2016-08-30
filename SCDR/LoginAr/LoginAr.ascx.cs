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
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                if (SPContext.Current.Web.CurrentUser != null)
                {
                    hfloginstatus.Value = "True";
                    GetCurrentUserDetails();
                }
                else
                {
                    hfloginstatus.Value = "False";
                }
            }
        }

        public void GetCurrentUserDetails()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPUser currentUser = oWeb.CurrentUser;
                            lblUsername.Text = currentUser.Name;

                        }
                    }
                });
            }
            catch (Exception ex)
            { }

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
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

                        HttpContext.Current.Response.Redirect("~/sites/SCDR/ar/SitePages/Home.aspx");
                    }
                    oWeb.AllowUnsafeUpdates = false;
                }


            }
        }
        protected void btnSignin_Click1(object sender, EventArgs e)
        {
            Page.Response.Redirect("/_layouts/15/Sp.Login.Custom/Login.aspx?ReturnUrl=%2fsites%2fSCDR%2far%2fSitePages%2fhome.aspx");
            //if (Membership.ValidateUser(UserName.Text, pwd.Text))
            //{

            //    SecurityToken tk = SPSecurityContext.SecurityTokenForFormsAuthentication(new Uri(SPContext.Current.Web.Url), "LdapMember", "LdapRole", UserName.Text, pwd.Text);

            //    if (tk != null)
            //    {
            //        SPFederationAuthenticationModule fam = SPFederationAuthenticationModule.Current;
            //        fam.SetPrincipalAndWriteSessionToken(tk);
            //        hfloginstatus.Value = "True";

            //        GetCurrentUserDetails();


            //    }
            //}
            //else
            //{

            /*  string domain = "scdr.gov.ae";
              if(Authenticate(UserName.Text,pwd.Text,domain)==true)
              {
                  SecurityToken tk = SPSecurityContext.SecurityTokenForFormsAuthentication(new Uri(SPContext.Current.Web.Url), "LdapMemberSCDR", "LdapRoleSCDR", UserName.Text, pwd.Text);

                  if (tk != null)
                  {
                      SPFederationAuthenticationModule fam = SPFederationAuthenticationModule.Current;
                      fam.SetPrincipalAndWriteSessionToken(tk);
                      hfloginstatus.Value = "True";

                      GetCurrentUserDetails();


                  }
              }*/


            //}




        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("/sites/SCDR/en/SitePages/SignupADUser.aspx");
        }
    }
}
