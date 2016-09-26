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
   
        /// <summary>
        /// fires on button "sign in" click
        /// perform ldap authentication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void btnSignin_Click1(object sender, EventArgs e)
        {
            Page.Response.Redirect("/_login/default.aspx?ReturnUrl=%2fsites%2fSCDR%2fen%2f_layouts%2f15%2fAuthenticate.aspx%3fSource%3d%252Fsites%252FSCDR%252Fen%252FSitePages%252FHome%252Easpx&Source=%2Fsites%2FSCDR%2Fen%2FSitePages%2FHome%2Easp");
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
        /// <summary>
        /// Function for retreiving User Details from SharePoint
        /// </summary>
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

        /// <summary>
        /// function fires when the btnSignOut get clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// function for sending password to user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnForgotPassword_Click(object sender, EventArgs e)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {

                    using (SPSite oSite = new SPSite(SPContext.Current.Site.Url))
                    {
                        using (SPWeb oWeb = oSite.OpenWeb())
                        {
                            SPList oList = oWeb.Lists[ListName];
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='EmailId' /><Value Type='Text'>" + txtForgotEmailId.Text.Trim() + "</Value></Eq></Where>";
                            SPListItemCollection oItems = oList.GetItems(query);
                            if (oItems.Count > 0)
                            {
                                 string userPassword =string.Empty;
                                foreach (SPListItem item in oItems)
                                {
                                    userPassword = item["Password"].ToString();

                                }
                                 //MailMessage Msg = new MailMessage();
                                 //// Sender e-mail address.
                                 //Msg.From = new MailAddress("FromAddress");
                                 //// Recipient e-mail address.
                                 //Msg.To.Add("ToAddress");
                                 //Msg.Subject = "Enquiry";
                                 //Msg.Body = "Your Password is : " + userPassword;
                                 //// your remote SMTP server IP.
                                 //SmtpClient server = new SmtpClient("my.server");
                                 //// Credentials are necessary if the server requires the client 
                                 //// to authenticate before it will send e-mail on the client's behalf.
                                 //server.UseDefaultCredentials = true;
                                 //server.Send(Msg);
                                 //Msg = null;
                                string subject = "PASSWORD RESET";
                                string body = "Your Password is : " + userPassword;
                                bool isBodyHtml = true;
                                string to = txtForgotEmailId.Text.Trim();
                                SendMail(subject, body, isBodyHtml, to);
                               ScriptManager.RegisterStartupScript(this, typeof(Page), "UserMsg", "<script>alert('Mail sent thank you...');if(alert){ window.location='contactus.aspx';}</script>", false);
 
                                 }
                        }
                    }
                });
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "UserMsg", "<script>alert('Mail not sent ');if(alert){ window.location='page.aspx';}</script>", false);
            }

        }

        public static bool SendMail(string Subject, string Body, bool IsBodyHtml,string To)
        {
            bool IsMailSent = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite currentSite = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb currentWeb = currentSite.OpenWeb(SPContext.Current.Web.ID))
                        {
                            string From = currentWeb.Site.WebApplication.OutboundMailReplyToAddress.ToString();

                            using (MailMessage mailMessage = new MailMessage(From, To, Subject, Body))
                            {
                                mailMessage.IsBodyHtml = IsBodyHtml;
                                SmtpClient smtpClient = new SmtpClient();
                                smtpClient.Host = currentWeb.Site.WebApplication.OutboundMailServiceInstance.Server.Address;
                                smtpClient.UseDefaultCredentials = true;
                                smtpClient.Port = 25;
                                using (smtpClient as IDisposable)
                                {
                                    smtpClient.Send(mailMessage);
                                }
                            }

                            //sharepoint Default
                           // IsMailSent = SPUtility.SendEmail(currentWeb, true, true, To, Subject, Body);
                            /* 
                             * If you want to send HTML Content then use this method
                             * IsMailSent = SPUtility.SendEmail(currentWeb, true, false, To, Subject, Body);
                             */
                           

                            IsMailSent = true;
                        }
                    }
                });
                return IsMailSent;
            }
            catch (Exception ex)
            {
                IsMailSent = false;
                return IsMailSent;
            }
        }



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
