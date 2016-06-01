using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace Greenways.Service.WebApi.Controllers
{
    /// <summary>
    /// Test controller
    /// </summary>
    public class TestController : ApiController
    {
        /// <summary>
        /// Now
        /// </summary>
        /// <returns>Returns DateTime.Now</returns>
        [HttpGet]
        public string Now()
        {
            return DateTime.Now.ToString("o");
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Index()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(typeof(TestController).Name)
            };
        }

        [HttpGet]
        public HttpResponseMessage Help()
        {
            string path = "Greenways.Service.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(XmlDocumentation));

            StreamReader reader = new StreamReader(path);
            var xmlDocumentation = (XmlDocumentation)serializer.Deserialize(reader);
            reader.Close();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<!doctype html>");
            stringBuilder.AppendLine("<html>");
            stringBuilder.AppendLine("<head>");
            stringBuilder.AppendLine("</head>");
            stringBuilder.AppendLine("<body>");
            stringBuilder.AppendLine($"<h1>Documentation of {xmlDocumentation.Assembly.Name}</h1>");
            XmlDocumentationMember typeMember = null;
            foreach (var member in xmlDocumentation.Members)
            {
                if (member.IsTypeClassInterfaceStructEnumOrDelegate)
                {
                    typeMember = member;
                    stringBuilder.AppendLine($"<h2>Type: {member.OnlyName}</h2>");
                    stringBuilder.AppendLine($"Summary: {member.Summary}");
                }
                if (member.IsMethod)
                {
                    var methodName = member.OnlyName.Replace(typeMember.OnlyName + ".", "");
                    stringBuilder.AppendLine($"<h3>Method: <a href='{methodName}'>{methodName}</a></h3>");
                    stringBuilder.AppendLine($"Summary: {member.Summary}</br>");
                    stringBuilder.AppendLine($"Returns: {member.Returns}");
                }

            }
            stringBuilder.AppendLine("</body>");
            stringBuilder.AppendLine("</html>");
            var response = new HttpResponseMessage();
            response.Content = new StringContent(stringBuilder.ToString());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }

    [Serializable()]
    [XmlRoot("doc")]
    public class XmlDocumentation
    {
        [XmlElement("assembly")]
        public XmlDocumentationAssembly Assembly { get; set; }

        [XmlArray("members")]
        [XmlArrayItem("member", typeof(XmlDocumentationMember))]
        public XmlDocumentationMember[] Members { get; set; }
    }

    [Serializable()]
    public class XmlDocumentationAssembly
    {
        [XmlElement("name")]
        public string Name { get; set; }
    }

    [Serializable()]
    public class XmlDocumentationMember
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("summary")]
        public string Summary { get; set; }

        [XmlElement("returns")]
        public string Returns { get; set; }

        public string OnlyType
        {
            get { return Name.Split(':')[0]; }
        }

        public string OnlyName
        {
            get { return Name.Split(':')[1]; }
        }

        public bool IsNamespace
        {
            get
            {
                return OnlyType == "N";
            }
        }

        public bool IsTypeClassInterfaceStructEnumOrDelegate
        {
            get
            {
                return OnlyType == "T";
            }
        }

        public bool IsField
        {
            get
            {
                return OnlyType == "F";
            }
        }

        public bool IsProperty
        {
            get
            {
                return OnlyType == "P";
            }
        }

        public bool IsMethod
        {
            get
            {
                return OnlyType == "M";
            }
        }

        public bool IsEvent
        {
            get
            {
                return OnlyType == "E";
            }
        }
    }
}

