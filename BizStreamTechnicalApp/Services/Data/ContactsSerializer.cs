using BizStreamTechnicalApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BizStreamTechnicalApp.Services.Data
{
    public class ContactsSerializer
    {
        // serializing object writer
        XmlSerializer serializer = new XmlSerializer(typeof(List<ContactModel>));

        public List<ContactModel> Deserialize(string path)
        {
            List<ContactModel> contactSubmissions = new List<ContactModel>();

            // object that reads text from path
            using (TextReader txtReader = new StreamReader(path))
            { 
                // get previous submissions into a list of submissions
                contactSubmissions = (List<ContactModel>)serializer.Deserialize(txtReader);
            }

            return contactSubmissions;
        }
        public void Serialize(string path, List<ContactModel> contactModels)
        {
            using (System.IO.FileStream file = System.IO.File.Create(path))
            {       
                serializer.Serialize(file, contactModels);
            }
        }
    }
}