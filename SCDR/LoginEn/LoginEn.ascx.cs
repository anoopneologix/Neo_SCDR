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

     

        protected void btnSignin_Click1(object sender, EventArgs e)
        {
            #region
               if (Membership.ValidateUser(UserName.Text, pwd.Text))
               {
                 
                   SecurityToken tk = SPSecurityContext.SecurityTokenForFormsAuthentication(new Uri(SPContext.Current.Web.Url), "LdapMember","LdapRole", UserName.Text, pwd.Text);
                   if (tk != null)
                   {
                       SPFederationAuthenticationModule fam = SPFederationAuthenticationModule.Current;
                       fam.SetPrincipalAndWriteSessionToken(tk);
                       hfloginstatus.Value = "True";

                       GetCurrentUserDetails();
                 
                   
                   }
               }
            #endregion
            #region
            /*
            bool status = SPClaimsUtility.AuthenticateFormsUser(new Uri(SPContext.Current.Web.Url), UserName.Text, pwd.Text);

            if (!status)
            {

                //Label1.Text = “Wrong Userid or Password”;

            }

            else
            {

                //if (Context.Request.QueryString.Keys.Count > 1)
                //{

                //    HttpContext.Current.Response.Redirect("Home.aspx");

                //}

                //else

                HttpContext.Current.Response.Redirect("Home.aspx");


            }*/
            #endregion

   

        }
        //Function for retreiving User Details from SharePoint
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
                        FormsAuthentication.RedirectToLoginPage();


                        //  FederatedAuthentication.SessionAuthenticationModule.SignOut();
                        //  WSFederationAuthenticationModule authModule = FederatedAuthentication.WSFederationAuthenticationModule;
                        //  SPUtility.Redirect(WSFederationAuthenticationModule.GetFederationPassiveSignOutUrl(authModule.Issuer, authModule.Realm, null), SPRedirectFlags.Default, HttpContext.Current);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/sites/SCDR/en/SitePages/Home.aspx");
                    }
                    oWeb.AllowUnsafeUpdates = false;
                }


            }
        }
       
    }
}
