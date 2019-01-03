using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ObeidatLog
{
    internal class XmlInfo : XmlException
    {
        internal protected XmlInfo() : base()
        {

        }
        internal override void Initiate()
        {
            dataItems = new BlockingCollection<LogModel>(logCapacity);
            base.Initiate();
        }
        public override void Debug(string client, string username, string data)
        {

        }
        public override void Info(string method, string data)
        {
            Task.Run(() =>
            {
                LogModel model = new LogModel()
                {
                    Time = DateTime.Now.AddHours(AdditinalHour),
                    Data = data,
                    Method = method
                };
                dataItems.Add(model);
            });
        }

        internal override void Consumer()
        {
            Task.Run(async () =>
            {
                while (!dataItems.IsCompleted)
                {
                    if (DateTime.Now.Second == 0 || dataItems.Count == dataItems.BoundedCapacity)
                    {
                        await WriteInfo();
                    }
                }
            });
            base.Consumer();

        }
        internal async override Task WriteInfo()
        {
            List<LogModel> items = new List<LogModel>();
            try
            {
                while (!dataItems.IsCompleted && dataItems.Count > 0 && items.Count < dataItems.BoundedCapacity)
                {
                    items.Add(dataItems.Take());
                }
                if (items.Any())
                {
                    DateTime logTime = DateTime.Now.AddHours(AdditinalHour);
                    string path = Path.Combine(logPath, logName) + " " + logTime.ToString("yyyy-MM-dd") + " Info.xml";
                    if (File.Exists(path))
                    {
                        LogModel item = items[0];
                        XDocument xDocument = XDocument.Load(path, LoadOptions.PreserveWhitespace);
                        XElement root = xDocument.Element(logName);
                        XElement currentparent =
                            new XElement("Request",
                            new XElement("Method", item.Method),
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
                items.Clear();
                items = null;
                await Task.Delay(30);
            }
        }

        private void WriteItemList(string path, IEnumerable<LogModel> items)
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
                    xmlWriter.WriteStartElement("", logName, "");

                    xmlWriter.WriteAttributeString("Time", item.StringTime);

                    xmlWriter.WriteElementString("Method", item.Method);
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
