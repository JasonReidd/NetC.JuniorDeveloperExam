using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetC.JuniorDeveloperExam.Web.Controllers
{
    /// <summary>
    /// Redirect to BlogController/Index with error message in TempData["Error"]
    /// </summary>
    public class ErrorController : Controller
    {
        // mysite/Error
        // mysite/Error/Index
        /// <summary>
        /// Redirect with default error message
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            TempData["Error"] = "Error.";
            return RedirectToAction("Index", "Blog", new { id = 1 });
        }

        // mysite/Error/BadRequest
        /// <summary>
        /// Redirect with bad request message
        /// </summary>
        /// <returns></returns>
        public ActionResult BadRequest()
        {
            TempData["Error"] = "Bad Request.";
            return RedirectToAction("Index", "Blog", new { id = 1 });
        }

        // mysite/Error/NotFound
        /// <summary>
        /// Redirect with page not found message
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            TempData["Error"] = "Page Not Found.";
            return RedirectToAction("Index", "Blog", new { id = 1 });
        }

        // mysite/Error/InternalError
        /// <summary>
        /// Redirect with internal error message
        /// </summary>
        /// <returns></returns>
        public ActionResult InternalError()
        {
            TempData["Error"] = "Internal Error.";
            return RedirectToAction("Index", "Blog", new { id = 1 });
        }
    }
}