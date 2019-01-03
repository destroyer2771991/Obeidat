using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ObeidatLog
{
    internal class XmlDebug : XmlInfo
    {
        internal protected XmlDebug() : base()
        {

        }
        internal override void Initiate()
        {
            dataDebugItems = new BlockingCollection<LogModelDebug>(logCapacity);
            base.Initiate();
        }
        public override void Info(string method, string data)
        {
            base.Info(method, data);
        }

        public override void Debug(string client, string username, string data)
        {
            if (HttpContext.Current != null)
            {
                string ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ip))
                {
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                string url = HttpContext.Current.Request.Url.PathAndQuery;
                string httpMethod = HttpContext.Current.Request.HttpMethod;
                GetLogModelDebug(ip, url, httpMethod, client, username, data, System.Net.Dns.GetHostName());
            }
        }
        internal override void Consumer()
        {
            Task.Run(async () =>
            {
                while (!dataDebugItems.IsCompleted)
                {
                    if (DateTime.Now.Second == 0 || dataDebugItems.Count == dataDebugItems.BoundedCapacity)
                    {
                        await WriteDebug();
                    }
                }
            });
            base.Consumer();

        }
        internal async override Task WriteDebug()
        {
            try
            {
                List<LogModelDebug> items = new List<LogModelDebug>();
                while (!dataDebugItems.IsCompleted && dataDebugItems.Count > 0 && items.Count < dataDebugItems.BoundedCapacity)
                {
                    items.Add(dataDebugItems.Take());
                }
                if (items.Any())
                {
                    DateTime logTime = DateTime.Now.AddHours(AdditinalHour);
                    string path = Path.Combine(logPath, logName) + " " + logTime.ToString("yyyy-MM-dd") + " Debug.xml";
                    if (File.Exists(path))
                    {
                        LogModelDebug item = items[0];
                        XDocument xDocument = XDocument.Load(path, LoadOptions.PreserveWhitespace);
                        XElement root = xDocument.Element(logName);
                        XElement currentparent =
                            new XElement("Request",
                            new XElement("IP", item.IP),
                            new XElement("Username", item.Username),
                            new XElement("Client", item.Client),
                            new XElement("DeviceName", item.DeviceName),
                            new XElement("URL", $"{item.Method}:{item.URL}"),
                            new XElement("Data", item.Data)
                        );
                        currentparent.SetAttributeValue("Time", item.StringTime);
                        root.AddFirst(currentparent);
                        xDocument.Save(path);
                        WriteItemList(path, items.Skip(1));
                    }
                    else
                    {
                        WriteItemList(path, items);
                    }
                }
            }
            finally
            {
                await Task.Delay(30);
            }
        }
        private void WriteItemList(string path, IEnumerable<LogModelDebug> items)
        {

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;
            xmlWriterSettings.NewLineChars = "\n";
            xmlWriterSettings.NewLineOnAttributes = true;
            using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                foreach (var item in items)
                {
                    xmlWriter.WriteAttributeString("Time", item.StringTime);

                    xmlWriter.WriteElementString("IP", item.IP);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteElementString("Username", item.Username);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("Client", item.Client);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("DeviceName", item.DeviceName);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("URL", $"{item.Method}:{item.URL}");
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("Data", item.Data);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                xmlWriter.Close();

            }

        }
    }
}
