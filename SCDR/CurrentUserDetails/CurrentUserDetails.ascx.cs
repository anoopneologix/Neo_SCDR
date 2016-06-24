//--------------------------------------------------
// Project Name : SCDR
// Program Name : CurrentUserDetails (Visual WebPart)
// Developed by : Sreejith C S
// Created Date : 28/03/2016
//---------------------------------------------------
using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Data;
using System.Web;
using Microsoft.SharePoint.Administration.Claims;
using System.Web.UI;

namespace SCDR.CurrentUserDetails
{
    [ToolboxItemAttribute(false)]
    public partial class CurrentUserDetails : WebPart
    {
          public CurrentUserDetails()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }
        // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultlbUsername = "Username";
        private static string lbUserName = DefaultlbUsername;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultlbUsername),
        WebDisplayName("Label UserName Text:"),
        WebDescription("Example : Username")]
        public string LbUserName
        {
            get { return lbUserName; }
            set { lbUserName = value; }
        }
        private const string DefaultlbEmail = "Email Address";
        private static string lbEmail = DefaultlbEmail;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultlbEmail),
        WebDisplayName("Label Email Address Text:"),
        WebDescription("Example : Email Address")]
        public string LbEmail
        {
            get { return lbEmail; }
            set { lbEmail = value; }
        }
        private const string DefaultbtnSettings = "Settings";
        private static string buttonSettings = DefaultbtnSettings;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultbtnSettings),
        WebDisplayName("Settings Button Text:"),
        WebDescription("Example : Settings")]
        public string BtnSettings
        {
            get { return buttonSettings; }
            set { buttonSettings = value; }
        }
        private const string DefaultbtnSignOut = "Sign Out";
        private static string buttonSignOut = DefaultbtnSignOut;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultbtnSignOut),
        WebDisplayName("Sign Out Button Text:"),
        WebDescription("Example : Sign Out")]
        public string bBttonSignOut
        {
            get { return buttonSignOut; }
            set { buttonSignOut = value; }
        }
        #endregion

        // Fuction for loading the User Details on Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!((Page)System.Web.HttpContext.Current.CurrentHandler).IsPostBack)
            {
                GetCurrentUserDetails();
                lblUserName.InnerHtml = CurrentUserDetails.lbUserName;
                lblEmail.InnerHtml = CurrentUserDetails.lbEmail;
                btnSettings.Text = CurrentUserDetails.buttonSettings;
                btnSignOut.Text = CurrentUserDetails.buttonSignOut;
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
                            txtUsername.Text = currentUser.Name;
                            txtEmail.Text = currentUser.Email;
                        }
                    }
                });
            }
            catch(Exception ex)
            { }
           
        }

        // Function for redirecting to signout page 
        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect("signout.aspx", SPRedirectFlags.RelativeToLayoutsPage, HttpContext.Current);
        }
    }
}
