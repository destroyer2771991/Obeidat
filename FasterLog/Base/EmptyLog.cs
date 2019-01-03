using System;

namespace ObeidatLog
{
    internal class EmptyLog : Logger
    {
        internal EmptyLog()
        {

        }
        public override void LogException(Exception e, string caption = "", Action<string> ExceptionFinalMessage = null)
        {
            
        }
        public override void Info(string method,string data)
        {
        }

        internal override void Consumer()
        {
        }
         
         

        internal override void Initiate()
        {
            
        }
         
        public override void Debug(string client, string username, string data)
        {

        }

    }
}
