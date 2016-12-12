using Microsoft.SharePoint;
using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;

namespace SCDR.InternalExternal
{
    [ToolboxItemAttribute(false)]
    public partial class InternalExternal : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public InternalExternal()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SPContext.Current.Web.AllRolesForCurrentUser.Contains(SPContext.Current.Web.RoleDefinitions["Full Control"]))
            {
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsg", "$(document).ready(function(){onSuccessMethod();});", true);
            }
            else
            {
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsgFalse", "$(document).ready(function(){onFailureMethod();});", true);
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "UpdateMsgFalse", "alert('check1')", true);
            }
        }
    }
}
