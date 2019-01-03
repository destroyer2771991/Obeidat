using ObeidatLog.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ObeidatLog
{
    internal class TextException : TextLogger
    {
        internal protected TextException() : base()
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
            List<string> items = new List<string>();
            try
            {
                while (!dataExceptionItems.IsCompleted && dataExceptionItems.Count > 0 && items.Count < dataExceptionItems.BoundedCapacity)
                {
                    LogModelException item = dataExceptionItems.Take();
                    items.Add($"StringTime: {item.StringTime} |=> Caption: {item.Caption} |=> Message: {item.Data} |=> FileName: {item.FileName} |=> Method: {item.Method} |=> LineNumber: {item.LineNumber}");
                }
                WritetoDisk(items, "Exception");
                 
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
