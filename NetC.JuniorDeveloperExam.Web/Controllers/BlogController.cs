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
        // mysite/Blog/Index/{1?}
        /// <summary>
        /// Displays blog. Defaults to first if no valid id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 1)
        {
            //gets content from Blog-Posts.json
            Helper helper = new Helper();
            var path = Server.MapPath("~/App_Data/Blog-Posts.json");
            Root rootObj = JsonConvert.DeserializeObject<Root>(helper.JSONRead(path));

            //if invalid id (SelectedBlogPostID) default to 1
            if (rootObj.blogPosts.Where(bp => bp.id == id) == null)
            {
                id = 1;//SelectedBlogPostID
            }

            //Add comment IDs and list<Reply> to Comment to allow them to be replied to
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

            var convertedJson = JsonConvert.SerializeObject(rootObj, Formatting.Indented);
            helper.JSONWrite(path, convertedJson);

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
            //serverside validation 
            if (!Helper.containsNoNulls(formData))
            {
                throw new System.Exception("Form Validation Error.");
            }
            if (!Helper.IsValidEmail(formData.Email))
            {
                throw new System.Exception("Email Validation Error.");
            }

            //gets content from Blog-Posts.json
            Helper helper = new Helper();
            var path = Server.MapPath(@"~/App_Data/Blog-Posts.json");
            Root rootObj = JsonConvert.DeserializeObject<Root>(helper.JSONRead(path));

            //aggregate comments to make it possible to get Max id
            List<Comment> aggregate = new List<Comment>();
            foreach (BlogPost blogPost in rootObj.blogPosts)
            {
                if (blogPost.comments != null)
                {
                    aggregate.AddRange(blogPost.comments);
                }
            }

            //adds new comment to content
            rootObj.blogPosts.Where(bp => bp.id == id).FirstOrDefault().comments.Add(new Comment()
            {
                id = aggregate.Max(a => a.id) + 1,
                name = formData.Name,
                emailAddress = formData.Email,
                message = formData.Message,
                date = formData.Date,
                replies = new List<Reply>()
            });

            //saves content to Blog-Posts.json
            var convertedJson = JsonConvert.SerializeObject(rootObj, Formatting.Indented);
            helper.JSONWrite(path, convertedJson);
            //redirect to Blog and retain id
            return RedirectToAction("Index", "Blog", new { id = id });
        }

        /// <summary>
        ///  Saves reply and returns to current Blog.
        /// </summary>
        /// <param name="id">SelectedBlogPostID</param>
        /// <param name="commentId"></param>
        /// <param name="formData">Contains Reply</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CommentReply(int id, int commentId, FormData formData)
        {
            //serverside validation 
            if (!Helper.containsNoNulls(formData))
            {
                TempData["Error"] = "Form Validation Error.";
                return RedirectToAction("Index", "Blog", new { id = 1 });
            }
            if (!Helper.IsValidEmail(formData.Email))
            {
                TempData["Error"] = "Email Validation Error.";
                return RedirectToAction("Index", "Blog", new { id = 1 });
            }

            //gets content from Blog-Posts.json
            Helper helper = new Helper();
            var path = Server.MapPath(@"~/App_Data/Blog-Posts.json");
            Root rootObj = JsonConvert.DeserializeObject<Root>(helper.JSONRead(path));

            //adds new comment to content
            rootObj.blogPosts.Where(bp => bp.id == id).FirstOrDefault().comments.Where(c => c.id == commentId).FirstOrDefault().replies.Add(new Reply()
            {
                name = formData.Name,
                emailAddress = formData.Email,
                message = formData.Message,
                date = formData.Date,
            });

            //saves content to Blog-Posts.json
            var convertedJson = JsonConvert.SerializeObject(rootObj, Formatting.Indented);
            helper.JSONWrite(path, convertedJson);

            //redirect to Blog and retain id
            return RedirectToAction("Index", "Blog", new { id = id });
        }
    }
}