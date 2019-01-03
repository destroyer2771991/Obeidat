using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ObeidatLog
{
    internal class TextDebug : TextInfo
    {
        internal protected TextDebug() : base()
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
                List<string> items = new List<string>();
                while (!dataDebugItems.IsCompleted && dataDebugItems.Count > 0 && items.Count < dataDebugItems.BoundedCapacity)
                {
                    LogModelDebug item = dataDebugItems.Take();
                    items.Add($"StringTime: {item.StringTime} |=> IP: {item.IP} |=> Username: {item.Username} |=> Client: {item.Client} |=> DeviceName: {item.DeviceName} |=> Method: {item.Method} |=> URL: {item.URL} |=> Data: {item.Data}");
                }
                WritetoDisk(items, "Debug");
            }
            finally
            {
                await Task.Delay(30);
            }
        }
    }
}
