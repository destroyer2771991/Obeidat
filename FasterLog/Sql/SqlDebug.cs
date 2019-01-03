using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace ObeidatLog
{
    internal class SqlDebug : SqlInfo
    {
        internal protected SqlDebug() : base()
        {

        }
        private static DataTable Table = new DataTable();
        internal override void Initiate()
        {
            try
            {
                dataDebugItems = new System.Collections.Concurrent.BlockingCollection<LogModelDebug>(logCapacity);
                string createCommandText = string.Format("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) CREATE TABLE  [dbo].[{0}](  [Id] [bigint] IDENTITY(1,1) NOT NULL,  [Time] [datetime] NOT NULL,  [IP] [varchar](50) NOT NULL, [Username] [nvarchar](50) NOT NULL, [Client] [varchar](15) NOT NULL, [URL] [varchar](127) NOT NULL,[Method] [varchar](6) NOT NULL,[DeviceName] [nvarchar](50) NOT NULL, [Data] [nvarchar](max) NOT NULL,   CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ", logName + "_Debug");
                Task createTable = Task.Run(async () =>
                {
                    using (SqlConnection con = new SqlConnection(logConnection))
                    {
                        using (SqlCommand cmd = new SqlCommand(createCommandText, con))
                        {
                            await cmd.Connection.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                });
                createTable.Wait();
                Table.Columns.Add("Id", typeof(long));
                Table.Columns.Add("Time", typeof(DateTime));
                Table.Columns.Add("IP", typeof(string));
                Table.Columns.Add("Username", typeof(string));
                Table.Columns.Add("Client", typeof(string));
                Table.Columns.Add("URL", typeof(string));
                Table.Columns.Add("Method", typeof(string));
                Table.Columns.Add("DeviceName", typeof(string));
                Table.Columns.Add("Data", typeof(string));
                base.Initiate();
            }
            catch { }
        }

        public override void Info(string method, string data)
        {
            base.Info(method, data);
        }

        public override void Debug(string client, string username, string data)
        {
            if (HttpContext.Current != null)
            {
                try
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
                catch { }
            }
        }

        internal override void Consumer()
        {
            Task.Run(async () =>
            {
                // A simple blocking consumer with no cancellation.
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
        internal override async Task WriteDebug()
        {
            try
            {
                Table.Rows.Clear();
                while (!dataDebugItems.IsCompleted && dataDebugItems.Count > 0 && Table.Rows.Count < dataDebugItems.BoundedCapacity)
                {
                    LogModelDebug item = dataDebugItems.Take();
                    Table.Rows.Add(0, item.Time, item.IP, item.Username, item.Client, item.URL, item.Method,item.DeviceName, item.Data);
                }
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(logConnection))
                {
                    bulkCopy.DestinationTableName = "dbo." + logName + "_Debug";
                    await bulkCopy.WriteToServerAsync(Table);
                }
            }
            finally
            {
                await Task.Delay(30);
            }
        }
    }
}
