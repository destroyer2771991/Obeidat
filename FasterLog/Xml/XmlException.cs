using ObeidatLog.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ObeidatLog
{
    internal class XmlException : XmlLogger
    {
        internal protected XmlException() : base()
        {

        }

        public override void Info(string method,string data)
        {

        }
        public override void Debug(string client, string username, string data)
        {
            
        }
        internal override void Initiate()
        {
            dataExceptionItems = new System.Collections.Concurrent.BlockingCollection<LogModelException>(logCapacity);
            base.Initiate();
        }
        internal override void Consumer()
        {
            Task.Run(async () =>
            {
                // A simple blocking consumer with no cancellation.
                while (!dataExceptionItems.IsCompleted)
                {
                    if (DateTime.Now.Second == 0 || DateTime.Now.Second == 30 || dataExceptionItems.Count == dataExceptionItems.BoundedCapacity)
                    {
                        await WriteException();
                    }
                }
            });
        }

         

        internal override async Task WriteException()
        {
            List<LogModelException> items = new List<LogModelException>();
            try
            {
                while (!dataExceptionItems.IsCompleted && dataExceptionItems.Count > 0 && items.Count < dataExceptionItems.BoundedCapacity)
                { 
                    items.Add(dataExceptionItems.Take());
                }
                if(items.Any())
                {
                    DateTime logTime = DateTime.Now.AddHours(AdditinalHour);
                    string path = Path.Combine(logPath, logName) + " " + logTime.ToString("yyyy-MM-dd") + " Exception.xml";
                    if (File.Exists(path))
                    {
                        LogModelException item = items[0];
                        XDocument xDocument = XDocument.Load(path, LoadOptions.PreserveWhitespace);
                        XElement root = xDocument.Element(logName);
                        XElement currentparent =
                            new XElement("Exception",
                            new XElement("Caption", item.Caption),
                            new XElement("Data", item.Data),
                            new XElement("FileName", item.FileName),
                            new XElement("Method", item.Method),
                            new XElement("LineNumber", item.LineNumber)
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
            catch(Exception ex)
            {

            }
            finally
            {
                items.Clear();
                items = null;
                await Task.Delay(30);
            }
        }
        private void WriteItemList(string path, IEnumerable<LogModelException> items)
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

                  
                    xmlWriter.WriteElementString("Caption", item.Caption);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteElementString("Data", item.Data);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("FileName", item.FileName);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("Method", item.Method);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteElementString("LineNumber", item.LineNumber.ToString());
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
