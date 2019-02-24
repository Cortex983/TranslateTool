using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using TranslatorTool.Models;

namespace TranslatorTool.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            string credential_path = HostingEnvironment.MapPath("~/App_Data/TranslateTool-f2d425cde5a8.json");
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credential_path);

            return View();

        }

        public JsonResult TranslateText(string text)
        {
            string xml_path = HostingEnvironment.MapPath("~/App_Data/TranslateList.xml");

            var credential = GoogleCredential.GetApplicationDefault();
            int Max = 0;
            string response = "";



            TranslationClient client = TranslationClient.Create();

             var detectLng = client.DetectLanguage(text).Language;


            switch (detectLng)
            {
                case "hr":
                     response = client.TranslateText(text, "en").TranslatedText;

                    break;
                case "sr":
                     response = client.TranslateText(text, "en").TranslatedText;

                    break;
                default:
                     response = client.TranslateText(text, "sr").TranslatedText;

                    break;
            }


              



            XmlDocument oXmlDocument = new XmlDocument();
           

            if (System.IO.File.Exists(xml_path))
            {
                
                 oXmlDocument.Load(xml_path);
                
                XmlNodeList nodelist = oXmlDocument.GetElementsByTagName("Translation");

                

                foreach (XmlElement item in nodelist)
                {
                    int EId = Convert.ToInt32(item.GetAttribute("id"));
                    if (EId > Max)
                    {
                        Max = EId;
                    }
                }

                XDocument xmlDoc = XDocument.Load(xml_path);

                Max = Max + 1;
                xmlDoc.Element("Translations").Add(new XElement("Translation", new XAttribute("timestamp", DateTime.Now), new XAttribute("id", Max), new XElement("From", text), new XElement("To", response)));
                xmlDoc.Save(xml_path);

            }
            else
            {
                Max = Max + 1;
                XDocument doc =  new XDocument(new XDeclaration("1.0", "utf-16", null),
                                               new XElement("Translations",
                                                 new XElement("Translation",
                                                   new XAttribute("timestamp", DateTime.Now), 
                                                   new XAttribute("id", Max), 
                                                     new XElement("From", text), 
                                                     new XElement("To", response))));
                doc.Save(xml_path);
            }

            return Json(new { translateTxt = response, Id = Max });
        }

        [HttpPost]
        public ActionResult FinalTranslation(ProjectModels model)
        {
            if (model.Id > 0)
            {
                string xml_path = HostingEnvironment.MapPath("~/App_Data/TranslateList.xml");

                XDocument oXmlDocument = XDocument.Load(xml_path);
                var items = (from item in oXmlDocument.Descendants("Translation")
                             where Convert.ToInt32(item.Attribute("id").Value) == model.Id
                             select new ProjectModels
                             {
                                 Id = Convert.ToInt32(item.Attribute("id").Value),
                                 From = item.Element("From").Value,
                                 To = item.Element("To").Value,
                             }).SingleOrDefault();
                if (items != null)
                {
                    model.Id = items.Id;
                    model.From = items.From;
                    model.To = items.To;
                }

                return View(model);
            }


            return Redirect("Index");
           
        }

        public ActionResult About()
        {

            ViewBag.Message = "Your application description page.";
        

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}