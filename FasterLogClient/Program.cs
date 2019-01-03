using System;
using System.Threading.Tasks;
using ObeidatLog;
namespace FasterLogClient
{
    class Program
    {
        static void Main(string[] args)
        {
           
          //  Helper.TryCatch(() => { CallDiv(10, 0); });
            Console.WriteLine(DateTime.Now.ToString("mm:ss:ffff"));
            Task logTask = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {

                    Logger.Instance.Info("", "");
                }
            });
           
            Task.WaitAll(logTask);
            Console.WriteLine(DateTime.Now.ToString("mm:ss:ffff"));
            Console.ReadKey(true);
        }
        private static void CallDiv(int v1, int v2)
        {
            int result = v1 / v2;
        }
    }
}
