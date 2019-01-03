using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ObeidatLog
{
    internal class SqlExceptionLog : SqlLogger
    {
        private static DataTable Table = new DataTable();

        internal protected SqlExceptionLog() : base()
        {

        }
        internal override void Initiate()
        {
            dataExceptionItems = new BlockingCollection<LogModelException>(logCapacity);
            string createCommandText = string.Format("IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) CREATE TABLE [dbo].[{0}]( [Id] [bigint] IDENTITY(1,1) NOT NULL, [Time] [datetime] NOT NULL, [Message] [nvarchar](MAX) NOT NULL, [LineNumber] [int] NOT NULL, [FileName] [nvarchar](127) NOT NULL, [Method] [nvarchar](127) NOT NULL,[AdditinalMessage] [nvarchar](511) NULL, CONSTRAINT [PK_{0}] PRIMARY KEY CLUSTERED ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ", logName + "_Exception");
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
            Table.Columns.Add("Message", typeof(string));
            Table.Columns.Add("LineNumber", typeof(int));
            Table.Columns.Add("FileName", typeof(string));
            Table.Columns.Add("Method", typeof(string));
            Table.Columns.Add("AdditinalMessage", typeof(string));
        }
        
        public override void Debug( string client, string username, string data)
        {

        }
        public override void Info(string mothod, string data)
        {

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
            try
            {
                Table.Rows.Clear();
                while (!dataExceptionItems.IsCompleted && dataExceptionItems.Count > 0 && Table.Rows.Count < dataExceptionItems.BoundedCapacity)
                {
                    LogModelException item = dataExceptionItems.Take();
                    Table.Rows.Add(0, item.Time, item.Data, item.LineNumber, item.FileName, item.Method, item.Caption);
                }
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(logConnection))
                {
                    bulkCopy.DestinationTableName = "dbo." + logName + "_Exception";
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
