using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Classes
{
    public class FormData
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
    }
}