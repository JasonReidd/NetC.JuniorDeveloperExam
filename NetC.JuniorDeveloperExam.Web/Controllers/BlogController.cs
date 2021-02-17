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
        // mysite/Blog/BlogPost/{1?}
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

            //fix comment IDs and allow coments to have replies
            var newid = 0;
            foreach (BlogPost blogPosts in rootObj.blogPosts)
            {
                if (blogPosts.comments == null)
                {
                    blogPosts.comments = new List<Comment>();
                }
                foreach (Comment comment in blogPosts.comments)
                {
                    if (comment.replies == null)
                    {
                        comment.replies = new List<Reply>();
                    }
                    comment.id = newid;
                    newid++;
                }

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
        public ActionResult BlogPostComment(int id, FormData formData)
        {
            //gets content from Blog-Posts.json
            var path = Server.MapPath(@"~/App_Data/Blog-Posts.json");
            Root rootObj = JsonConvert.DeserializeObject<Root>(System.IO.File.ReadAllText(path));
            //adds new comment to content
            rootObj.blogPosts.Where(bp => bp.id == id).FirstOrDefault().comments.Add(new Comment()
            {
                id = rootObj.blogPosts.Last().comments.Last().id, // temperary fix, max & any aggregate
                name = formData.Name,
                emailAddress = formData.Email,
                message = formData.Message,

                date = formData.Date,
            }) ;
            //saves content to Blog-Posts.json
            var convertedJson = JsonConvert.SerializeObject(rootObj, Formatting.Indented);
                System.IO.File.WriteAllText(path, convertedJson);
            //redirect to Blog and retain id
            return RedirectToAction("BlogPost", "Blog", new { id = id });          
        }

        [HttpPost]
        public ActionResult CommentReply(int id, int commentId, FormData formData)
        {
            //gets content from Blog-Posts.json
            var path = Server.MapPath(@"~/App_Data/Blog-Posts.json");
            Root rootObj = JsonConvert.DeserializeObject<Root>(System.IO.File.ReadAllText(path));
            //adds new comment to content

            if(rootObj.blogPosts.Where(bp => bp.id == id).FirstOrDefault().comments.Where(c => c.id == commentId).FirstOrDefault().replies == null)
            {
                rootObj.blogPosts
                    .Where(bp => bp.id == id).FirstOrDefault().comments
                    .Where(c => c.id == commentId).FirstOrDefault().replies
                    = new List<Reply>();
            }

            rootObj.blogPosts.Where(bp => bp.id == id).FirstOrDefault().comments.Where(c => c.id == commentId).FirstOrDefault().replies.Add(new Reply()
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