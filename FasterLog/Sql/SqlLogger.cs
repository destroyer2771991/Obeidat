using System;
using System.Configuration;

namespace ObeidatLog
{
    internal class SqlLogger : Logger
    {

        internal protected readonly string logConnection = ConfigurationManager.AppSettings["logConnection"];
        internal SqlLogger() : base()
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

        internal override void Initiate()
        {
            throw new NotImplementedException();
        }

    }
}
