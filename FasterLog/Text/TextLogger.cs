using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ObeidatLog.Text
{
    internal class TextLogger : Logger
    {
        internal protected readonly string logPath = ConfigurationManager.AppSettings["LogPath"];
        public TextLogger():base()
        {

        }
        public override void Info(string method,string data)
        {
            throw new NotImplementedException();
        }


        public override void Debug(string client, string username, string data)
        {
            throw new NotImplementedException();
        }

        internal override void Consumer()
        {
            throw new NotImplementedException();
        }
        protected void WritetoDisk(List<string> items, string logLevel)
        {
            if (items.Any())
            {
                File.AppendAllLines($"{Path.Combine(logPath, logName)} {DateTime.Now.AddHours(AdditinalHour).ToString("yyyy-MM-dd")} - {logLevel}.txt", items);
            }
        }
        internal override void Initiate()
        {
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);
          
        }
    }
}
