using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Classes
{
        public class Comment
        {
            public string name { get; set; }
            public DateTime date { get; set; }
            public string emailAddress { get; set; }
            public string message { get; set; }
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

        public class Root
        {
            public List<BlogPost> blogPosts { get; set; }
        }
}