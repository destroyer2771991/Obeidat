using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ObeidatLog
{
    internal class TextInfo : TextException
    {
        internal protected TextInfo() : base()
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
            List<string> items = new List<string>();
            try
            {
                while (!dataItems.IsCompleted && dataItems.Count > 0 && items.Count < dataItems.BoundedCapacity)
                {
                    LogModel item = dataItems.Take();
                    items.Add($"StringTime: {item.StringTime} |=> Method: {item.Method} |=> Data: {item.Data}");
                }
                WritetoDisk(items, "Info");
            }
            finally
            {
                items.Clear();
                items = null;
                await Task.Delay(30);
            }
        }
    }
}
