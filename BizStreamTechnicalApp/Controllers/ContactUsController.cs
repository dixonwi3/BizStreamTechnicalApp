using BizStreamTechnicalApp.Models;
using BizStreamTechnicalApp.Services.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace BizStreamTechnicalApp.Controllers
{
    public class ContactUsController : Controller
    {
        ContactsSerializer serializer = new ContactsSerializer();

        // GET: ContactUs
        public ActionResult Index()
        {
            return View("ContactUs");
        }

        public ActionResult Submit(ContactModel contactModel)
        {
            // get the root directory for object deserialization, then serialization
            var path = AppDomain.CurrentDomain.BaseDirectory + "/ContactUsSubmissions.xml";

            List<ContactModel> contactSubmissions = new List<ContactModel>();

            // if the serialization file already exists, deserialize it
            if (System.IO.File.Exists(path))
            {
                contactSubmissions = serializer.Deserialize(path);
            }

            contactSubmissions.Add(contactModel);

            try
            {
                serializer.Serialize(path, contactSubmissions);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View("ContactUsFailure");
            }

            // entry successs
            return View("ContactUsSuccess");

        }
    }
}