using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace SCDR.AdminForms.AddNews
{
    [ToolboxItemAttribute(false)]
    public partial class AddNews : WebPart
    {
        
         int SpListItemId = 0;
         int SpListItemIdAr = 0;
        public AddNews()
        {
           
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

          // Following code is for enabling custom webpart property
        #region CustomWebPartProperty
        private const string DefaultLibraryName = "CustomNewsList";
        private static string listName = DefaultLibraryName;
        [Category("Extended Settings"),
        Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        DefaultValue(DefaultLibraryName),
        WebDisplayName("List Name:"),
        WebDescription("Please Enter a valid List Name")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
          
            
        }

        public int GetNewsId()
        {
            int newsid=0;
            try
            {
                  using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                  {
                      int lastItemIdEn = -1;
                      int lastItemIdAr = -1;
                     
                      using (SPWeb oWeb = oSite.OpenWeb("en/"))
                      {
                          SPList list = oWeb.Lists[ListName];
                          SPQuery query = new SPQuery();
                          query.RowLimit = 1;
                          query.Query = "<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                          SPListItem maxItem = list.GetItems(query).Cast<SPListItem>().FirstOrDefault();
                         
                          if (maxItem != null)
                          {
                              lastItemIdEn = maxItem.ID;
                          } 
                      }
                      using (SPWeb oWeb = oSite.OpenWeb("ar/"))
                      {
                          SPList list = oWeb.Lists[ListName];
                          SPQuery query = new SPQuery();
                          query.RowLimit = 1;
                          query.Query = "<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>";
                          SPListItem maxItem = list.GetItems(query).Cast<SPListItem>().FirstOrDefault();

                          if (maxItem != null)
                          {
                              lastItemIdAr = maxItem.ID;
                          }
                      }
                      if(lastItemIdAr>=lastItemIdEn)
                      {
                          newsid = lastItemIdAr;
                      }
                      else
                      {
                          newsid = lastItemIdEn;
                      }
                   

                  }
                  return newsid;
            }
            catch
            {
                return newsid;
            }

        }

        public void SaveToEnglishNewsList(List<string> fileName, byte[] fileContents, int newsId)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
              {
                  using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                  {

                      using (SPWeb oWeb = oSite.OpenWeb("en/"))
                      {
                          SPList list = oWeb.Lists[ListName];
                          SPListItem item = list.Items.Add();
                          item["NewsID"] = newsId;
                          item["Title"] = txtNewsHeading.Text;
                          item["Date"] = txtNewsdate.Text;
                          item["Location"] = txtNewsLocation.Text;
                          item["Description"] = hfNewsDescription.Value.ToString();
                          if (fileName.Count > 0)
                          {
                              SPAttachmentCollection attachAr = item.Attachments;
                              foreach (string filename in fileName)
                              {
                                  attachAr.Add(filename, fileContents);
                              }
                          }
                          oWeb.AllowUnsafeUpdates = true;
                          item.Update();
                          oWeb.AllowUnsafeUpdates = false;
                          SpListItemId = item.ID;
                      }
                  }
              });
            }
            catch
            {

            }
        }
        public void SaveToArabicNewsList(List<string> fileName, byte[] fileContents, int newsId)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
              {
                  using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                  {

                      using (SPWeb oWebAr = oSite.OpenWeb("ar/"))
                      {
                          SPList listAr = oWebAr.Lists[ListName];
                          SPListItem itemAr = listAr.Items.Add();
                          itemAr["NewsID"] = newsId;
                          itemAr["Title"] = txtNewsHeadingAr.Text;
                          itemAr["Date"] = txtNewsDateAr.Text;
                          itemAr["Location"] = txtNewsLocationAr.Text;
                          itemAr["Description"] = hfNewsDescriptionAr.Value.ToString();
                          if (fileName.Count>0)
                          {
                              SPAttachmentCollection attachAr = itemAr.Attachments;
                              foreach (string filename in fileName)
                              {
                                  attachAr.Add(filename, fileContents);
                              }
                          }
                          oWebAr.AllowUnsafeUpdates = true;
                          itemAr.Update();
                          oWebAr.AllowUnsafeUpdates = false;
                          SpListItemIdAr = itemAr.ID;
                      }
                  }
              });
            }
            catch
            { }
        }

        public void UpdateDefaultThumbnailToEnglishNewsList()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
              {
                  using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                  {
                      using (SPWeb oWeb = oSite.OpenWeb("en/"))
                      {
                          SPList list = oWeb.Lists[ListName];
                          SPListItem item = list.GetItemById(SpListItemId);
                          string thumbnailUrl = oWeb.Url + "/_layouts/15/SCDR/images/default.png";
                          item["ThumbnailUrl"] = thumbnailUrl;
                          oWeb.AllowUnsafeUpdates = true;
                          item.Update();
                          oWeb.AllowUnsafeUpdates = false;
                      }
                  }
              });
            }
            catch
            {

            }

        }

        public void UpdateDefaultThumbnailToArabicNewsList()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWebAr = oSite.OpenWeb("ar/"))
                        {
                            SPList listAr = oWebAr.Lists[ListName];
                            SPListItem itemAr = listAr.GetItemById(SpListItemIdAr);
                            string thumbnailUrl = oWebAr.Url + "/_layouts/15/SCDR/images/default.png";
                            itemAr["ThumbnailUrl"] = thumbnailUrl;
                            oWebAr.AllowUnsafeUpdates = true;
                            itemAr.Update();
                            oWebAr.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch
            {

            }

        }

        public void UpdateThumbnailToEnglishNewsList()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                 {
                     using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                     {
                         using (SPWeb oWeb = oSite.OpenWeb("en/"))
                         {
                             SPList list = oWeb.Lists[ListName];
                             SPListItem item = list.GetItemById(Convert.ToInt32(hfListItemId.Value));
                             item["ThumbnailUrl"] = lblUrl.Value.ToString();
                             oWeb.AllowUnsafeUpdates = true;
                             item.Update();
                             oWeb.AllowUnsafeUpdates = false;
                         }
                     }
                 });
            }
            catch { }

        }
        public void UpdateThumbnailToArabicNewsList()
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb oWebAr = oSite.OpenWeb("ar/"))
                        {
                            SPList listAr = oWebAr.Lists[ListName];
                            SPListItem itemAr = listAr.GetItemById(Convert.ToInt32(hfListItemIdAr.Value));
                            string engThumbnailUrl = lblUrl.Value.ToString();
                            if (engThumbnailUrl.Contains("/en/"))
                            {

                                string arUrl = engThumbnailUrl.Replace("/en/", "/ar/");
                                string arThumbnailUrl = arUrl.Replace("/" + hfListItemId.Value + "/", "/" + hfListItemIdAr.Value + "/");
                                itemAr["ThumbnailUrl"] = arThumbnailUrl;
                            }
                            else
                            {
                                itemAr["ThumbnailUrl"] = engThumbnailUrl;
                            }
                           
                            oWebAr.AllowUnsafeUpdates = true;
                            itemAr.Update();
                            oWebAr.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch { }

        }

        //function for add items to list
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
             try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
              {
                  using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                  {


                      byte[] fileContents = new byte[16 * 1024];
                      List<string> fileName=new List<string>();
                      List<byte> fileContent = new List<byte>();
                      int newsId = GetNewsId();
                      if (fuThumbnailImage.HasFile)
                      {

                          foreach (HttpPostedFile postedFile in fuThumbnailImage.PostedFiles)
                          {

                              Stream fs = postedFile.InputStream;
                              fileContents = new byte[fs.Length];
                              fs.Read(fileContents, 0, (int)fs.Length);
                              fs.Close();
                            //  fileContent.Add(fileContents);
                              fileName.Add(Path.GetFileName(postedFile.FileName));
                              
                          }
                      }
                      if (rbBoth.Checked)
                      {
                          SaveToEnglishNewsList(fileName, fileContents, newsId);
                          SaveToArabicNewsList(fileName, fileContents, newsId);

                          if (rbYes.Checked)
                          {
                              BindEnglishThumbnailImages();
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                          }
                          else if (rbNo.Checked)
                          {
                              UpdateDefaultThumbnailToEnglishNewsList();
                              UpdateDefaultThumbnailToArabicNewsList();
                              formClear();
                              string sMessage = "successfully completed";
                              ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);
                          }
                      }
                      else if(rbEnglish.Checked)
                      {
                          SaveToEnglishNewsList(fileName, fileContents, newsId);
                          if (rbYes.Checked)
                          {
                              BindEnglishThumbnailImages();
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                          }
                          else if (rbNo.Checked)
                          {
                              UpdateDefaultThumbnailToEnglishNewsList();
                              formClear();
                              string sMessage = "successfully completed";
                              ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);
                          }
                      }
                      else if (rbArabic.Checked)
                      {
                          SaveToArabicNewsList(fileName, fileContents, newsId);
                          if (rbYes.Checked)
                          {
                              BindArabicThumbnailImages();
                              ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);

                          }
                          else if (rbNo.Checked)
                          {
                              UpdateDefaultThumbnailToArabicNewsList();
                              formClear();
                              string sMessage = "successfully completed";
                              ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);
                          }
                      }


                  }
              });



            }
            catch (Exception ex)
            {
                
            }

        }

        //function for update list item (Thumbnail Url)
        protected void btnSaveThumbnail_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbBoth.Checked)
                {
                    UpdateThumbnailToEnglishNewsList();
                    UpdateThumbnailToArabicNewsList();
                }
                else if (rbEnglish.Checked)
                {
                    UpdateThumbnailToEnglishNewsList();
                }
                else if (rbArabic.Checked)
                {
                    UpdateThumbnailToArabicNewsList();
                }

                formClear();
                string sMessage = "successfully completed";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Alert", "<script>alert('" + sMessage + "');</script>", false);


            }
            catch { }

        }

        //function for binding uploaded English News  images to pop up modal
        public void BindEnglishThumbnailImages()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
             {
                 using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                 {
                     
                     using (SPWeb oWeb = oSite.OpenWeb("en/"))
                     {
                         SPList list = oWeb.Lists[ListName];
                         SPListItem item = list.GetItemById(SpListItemId);
                         hfListItemId.Value = SpListItemId.ToString();
                         hfListItemIdAr.Value = SpListItemIdAr.ToString();
                         SPAttachmentCollection docs = item.Attachments;
                         DataTable dt = new DataTable();
                         DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                         dt.Columns.Add(dcImageUrl);
                         if (docs.Count > 0)
                         {
                             foreach (string fileName in item.Attachments)
                             {
                                 DataRow dr = dt.NewRow();
                                 dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                 dt.Rows.Add(dr);
                             }
                             repThumbnail.DataSource = dt;
                             repThumbnail.DataBind();

                         }
                     }
                    
                 }
             });
        }

        //function for binding uploaded Arabic News images to pop up modal
        public void BindArabicThumbnailImages()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite oSite = new SPSite(SPContext.Current.Web.Url))
                {

                    using (SPWeb oWeb = oSite.OpenWeb("ar/"))
                    {
                        SPList list = oWeb.Lists[ListName];
                        SPListItem item = list.GetItemById(SpListItemIdAr);
                        hfListItemIdAr.Value = SpListItemIdAr.ToString();
                        SPAttachmentCollection docs = item.Attachments;
                        DataTable dt = new DataTable();
                        DataColumn dcImageUrl = new DataColumn("ImageUrl", typeof(string));
                        dt.Columns.Add(dcImageUrl);
                        if (docs.Count > 0)
                        {
                            foreach (string fileName in item.Attachments)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ImageUrl"] = SPUrlUtility.CombineUrl(item.Attachments.UrlPrefix, fileName);
                                dt.Rows.Add(dr);
                            }
                            repThumbnail.DataSource = dt;
                            repThumbnail.DataBind();

                        }
                    }

                }
            });
        }

         //function for clearing the controls in form
        public void formClear()
        {
            txtNewsHeading.Text = "";
            txtNewsdate.Text = "";
            txtNewsLocation.Text = "";
            txtNewsDescription.Text = "";
            rbYes.Checked = true;
            txtNewsLocationAr.Text = "";
            txtNewsHeadingAr.Text = "";
            txtNewsDescriptionAr.Text = "";
            txtNewsDateAr.Text = "";
        }
    }
}
