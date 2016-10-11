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
                //SPSecurity.RunWithElevatedPrivileges(delegate()
                //{

                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPUser currentUser = oWeb.CurrentUser;
                            //SPServiceContext serverContext = SPServiceContext.GetContext(oSite);
                            //UserProfileManager profileManager = new UserProfileManager(serverContext);
                            //UserProfile profile = profileManager.GetUserProfile(currentUser.LoginName);
                            //string firstName = profile["FirstName"].Value.ToString();
                            //string LastName = profile["LastName"].Value.ToString();
                            //string usName = this.Context.User.Identity.Name;
                            string usName = currentUser.Name;
                            string resolvedName = usName;
                            if (resolvedName.LastIndexOf('|') > 0)
                            {
                                resolvedName = resolvedName.Substring(usName.LastIndexOf('|') + 1);
                            }
                            if (resolvedName.LastIndexOf('\\') > 0)
                            { 
                            resolvedName = resolvedName.Substring(resolvedName.LastIndexOf('\\')+1);
                            }
                            if (resolvedName.LastIndexOf('.') > 0)
                            {
                                resolvedName = resolvedName.Substring(1, resolvedName.LastIndexOf('.'));
                            }
                            if (resolvedName.LastIndexOf('@') > 0)
                            {
                                resolvedName = resolvedName.Substring(1, resolvedName.LastIndexOf('@'));
                            }
                            lblUsername.Text = usName;

                        }
                    }
                //});
            }
            catch (Exception ex)
            { }

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            FormsAuthentication.SignOut();
            //if (HttpContext.Current == null)
            //    HttpContext.Current.Response.Redirect("/_login/default.aspx?ReturnUrl=%2fsites%2fSCDR%2far%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Far%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Far%2FSitePages%2FHome%2Easp", false);
            HttpContext.Current.Response.Redirect("/sites/scdr/ar/SitePages/Sign_out.aspx", false);

            //using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
            //{
            //    using (SPWeb oWeb = oSite.OpenWeb())
            //    {


            //        oWeb.AllowUnsafeUpdates = true;
            //        //if (HttpContext.Current.User.Identity.AuthenticationType.ToUpper() != "FORMS")
            //        //{
            //            WSFederationAuthenticationModule authModule = FederatedAuthentication.WSFederationAuthenticationModule;
            //            //string signoutUrl = (WSFederationAuthenticationModule.GetFederationPassiveSignOutUrl(authModule.Issuer, authModule.Realm, null));
            //           // WSFederationAuthenticationModule.FederatedSignOut(new Uri(SPContext.Current.Site.Url + "/_login/default.aspx?ReturnUrl=%2fsites%2fSCDR%2far%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Far%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Far%2FSitePages%2FHome%2Easp"), new Uri(SPContext.Current.Site.Url + "/_login/default.aspx?ReturnUrl=%2fsites%2fSCDR%2far%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Far%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Far%2FSitePages%2FHome%2Easp"));
            //            FormsAuthentication.SignOut();
            //            //Page.Response.Redirect("/_login/default.aspx?ReturnUrl=%2fsites%2fSCDR%2far%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Far%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Far%2FSitePages%2FHome%2Easp");
            //            SPFederationAuthenticationModule.FederatedSignOut(new Uri(SPContext.Current.Web.Url + "/SitePages/Home.aspx"), null);
            //            //HttpContext.Current.Response.Redirect("/_layouts/closeConnection.aspx?loginasanotheruser=true");
            //        //}
            //        //else
            //        //{

            //        //    HttpContext.Current.Response.Redirect("/_layouts/closeConnection.aspx?loginasanotheruser=true");
            //        //}
            //        oWeb.AllowUnsafeUpdates = false;
               // }


           // }
        }
        protected void btnSignin_Click1(object sender, EventArgs e)
        {
            Page.Response.Redirect("/_login/default.aspx?ReturnUrl=%2fsites%2fSCDR%2far%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Far%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Far%2FSitePages%2FHome%2Easp");
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
            Page.Response.Redirect("/sites/SCDR/ar/SitePages/SignupADUser.aspx");
        }
    }
}
