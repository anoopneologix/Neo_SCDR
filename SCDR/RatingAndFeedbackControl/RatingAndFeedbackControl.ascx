<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RatingAndFeedbackControl.ascx.cs" Inherits="SCDR.RatingAndFeedbackControl.RatingAndFeedbackControl" %>


                <!--floating menu area-->
                <div class="star">
                    <img src="/sites/sreejith/en/_catalogs/masterpage/images/star.png" alt="" />
                    <span class="star_text">Rating
                    
                    </span>
                    <div class="map_out">
                        <h1>How You Rate Our Website
                        
                        </h1>
                        <asp:TextBox id="input-21b" value="0" type="number" class="rating" min="0" max="5" step="0.5" data-size="lg" data-rtl="0" runat="server"></asp:TextBox>
                      <!--  <input id="input-21b" value="0" type="number" class="rating" min="0" max="5" step="0.5" data-size="lg" data-rtl="0" />-->
                        <h2 class="result-display">Average Rating: 2.2
                        
                        </h2>
                        <hr />
                        <h2>Write your feedback
                        
                        </h2>
                        <textarea>
                        </textarea>
                        <input type="button" value="Submit" />
                    </div>
                </div>
                <a href="#">
                    <div class="map">
                        <img src="/sites/sreejith/en/_catalogs/masterpage/images/blog-icon.png" alt="" />
                        <span class="star_text">Blog
                        
                        </span>
                    </div>
                </a>
                
                <!--floating menu area END-->