using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Classes
{
    public class Helper
    {
        /// <summary>
        /// If form contains no null values return true
        /// </summary>
        /// <param name="formData">Comment/reply data</param>
        /// <returns></returns>
        public static bool ContainsNoNulls(FormData formData)
        {
            if (formData != null)
            {
                if (formData.Date == null || formData.Email == null || formData.Message == null || formData.Name == null)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// If email is valid return tue
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }

            #pragma warning disable CS0168 // Variable is declared but never used
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            
            catch (ArgumentException e)            
            {
                return false;
            }
            #pragma warning restore CS0168 // Variable is declared but never used

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets JSON from path
        /// </summary>
        /// <param name="path">Full path including file name</param>
        /// <returns></returns>
        public string JSONRead(string path)
        {
            string jsonResult;
            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        /// <summary>
        /// Writes JSONString to path
        /// </summary>
        /// <param name="path">Full path including file name</param>
        /// <param name="jSONString">JSON to write to file</param>
        public void JSONWrite(string path, string jSONString)
        {
            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
            }
        }
    }
}