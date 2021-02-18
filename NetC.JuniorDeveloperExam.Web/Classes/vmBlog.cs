using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Classes
{
    /// <summary>
    /// Used in the Blog/Index ActionResult
    /// </summary>
    public class vmBlog
    {
        public List<BlogPost> ListOfBlogPost { get; internal set; }
        public int SelectedBlogPostID { get; internal set; }
    }
}