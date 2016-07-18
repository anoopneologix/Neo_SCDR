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
using System.Web.Security;
using System.Security.Principal;

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

            if (Membership.ValidateUser(UserName.Text, pwd.Text))
            {

                SecurityToken tk = SPSecurityContext.SecurityTokenForFormsAuthentication(new Uri(SPContext.Current.Web.Url), "LdapMember", "LdapRole", UserName.Text, pwd.Text);

                if (tk != null)
                {
                    SPFederationAuthenticationModule fam = SPFederationAuthenticationModule.Current;
                    fam.SetPrincipalAndWriteSessionToken(tk);
                    hfloginstatus.Value = "True";

                    GetCurrentUserDetails();


                }
            }
            else
            {
               
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


            }
            



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
                  }
                    else
                    {
                        HttpContext.Current.Response.Redirect("~/sites/SCDR/en/SitePages/Home.aspx");
                    }
                    oWeb.AllowUnsafeUpdates = false;
                }


            }
        }

   /*     private bool Authenticate(string userName,string password, string domain)
        {
            bool authentic = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + domain,userName, password);
                object nativeObject = entry.NativeObject;
                authentic = true;
            }
            catch (DirectoryServicesCOMException) { }
            return authentic;
        }
        */

       
     
       
    }
}
