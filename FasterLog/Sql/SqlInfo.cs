using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ObeidatLog
{
    internal class SqlInfo : SqlExceptionLog
    {
        internal protected SqlInfo() : base()
        {

        }
        private static DataTable Table = new DataTable();
        internal override void Initiate()
        {
            dataItems = new System.Collections.Concurrent.BlockingCollection<LogModel>(logCapacity);
            string createCommandText = string.Format("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) CREATE TABLE [dbo].[{0}]( [Id] [bigint] IDENTITY(1,1) NOT NULL, [Time] [datetime] NOT NULL, [Data] [nvarchar](max) NOT NULL, [Method] [nvarchar](127) NOT NULL, CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ", logName + "_Info");
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
            Table.Columns.Add("Data", typeof(string));
            Table.Columns.Add("Method", typeof(string));
            base.Initiate();
        }
        public override void Debug( string client, string username, string data)
        {

        }
        internal async override Task WriteInfo()
        {
            try
            {
                Table.Rows.Clear();
                while (!dataItems.IsCompleted && dataItems.Count > 0 && Table.Rows.Count < dataItems.BoundedCapacity)
                {
                    LogModel item = dataItems.Take();
                    Table.Rows.Add(0, item.Time, item.Data, item.Method);
                }
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(logConnection))
                {
                    bulkCopy.DestinationTableName = "dbo." + logName + "_Info";
                    await bulkCopy.WriteToServerAsync(Table);
                }
            }
            finally
            {
                await Task.Delay(30);
            }

        }
        public override void Info(string method, string data)
        {
            Task.Run(() =>
            {
                LogModel model = new LogModel()
                {
                    Time = DateTime.Now.AddHours(AdditinalHour),
                    Data = data,
                    Method=method
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
    }
}
