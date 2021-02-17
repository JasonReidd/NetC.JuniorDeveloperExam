using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using NetC.JuniorDeveloperExam.Web.Classes;
using System.Linq;

namespace NetC.JuniorDeveloperExam.Web.Controllers
{
    public class BlogController : Controller
    {
        // mysite
        // mysite/Blog
        // mysite/Blog/Blogpost/{1?}
        /// <summary>
        /// Displays blog. Defaults to first if no valid id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BlogPost(int id = 1)
        {
            //gets content from Blog-Posts.json
            var path = Server.MapPath(@"~/App_Data/Blog-Posts.json");
            Root rootObj = JsonConvert.DeserializeObject<Root>(System.IO.File.ReadAllText(path));
            //if invalid id (SelectedBlogPostID) default to 1
            if (rootObj.blogPosts.Where(bp => bp.id == id) == null)
            {
                id = 1;//SelectedBlogPostID
            }
            //creates a view model
            vmBlog model = new vmBlog()
            {
                ListOfBlogPost = rootObj.blogPosts,
                SelectedBlogPostID = id
            };
            return View(model);
        }

        /// <summary>
        /// Saves comment and returns to current blog.
        /// </summary>
        /// <param name="id">SelectedBlogPostID</param>
        /// <param name="formData">Contains Comment</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BlogPost(int id, FormData formData)
        {
            //gets content from Blog-Posts.json
            var path = Server.MapPath(@"~/App_Data/Blog-Posts.json");
            Root rootObj = JsonConvert.DeserializeObject<Root>(System.IO.File.ReadAllText(path));
            //adds new comment to content
            rootObj.blogPosts.Where(bp => bp.id == id).FirstOrDefault().comments.Add(new Comment()
            {
                name = formData.Name,
                emailAddress = formData.Email,
                message = formData.Message,

                date = formData.Date,
            });
            //saves content to Blog-Posts.json
            var convertedJson = JsonConvert.SerializeObject(rootObj, Formatting.Indented);
                System.IO.File.WriteAllText(path, convertedJson);
            //redirect to Blog and retain id
            return RedirectToAction("BlogPost", "Blog", new { id = id });          
        }
    }
}