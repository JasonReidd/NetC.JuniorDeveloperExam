using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Classes
{
    public class Reply
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public string emailAddress { get; set; }
        public string message { get; set; }
    }

    public class Comment
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public string emailAddress { get; set; }
        public string message { get; set; }
        public List<Reply> replies { get; set; }
    }

    public class BlogPost
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string htmlContent { get; set; }
        public List<Comment> comments { get; set; }
    }

    /// <summary>
    /// Root is used to map Models to the JSON file
    /// </summary>
    public class Root
    {
        public List<BlogPost> blogPosts { get; set; }
    }
}