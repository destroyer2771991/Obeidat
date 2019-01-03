using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace ObeidatLog
{
    /// <summary>
    /// Write log fast in text file or Sql database.
    /// </summary>
    public abstract class Logger
    {
        private static Regex _base64Escape = new Regex(@"data:[a-z]+/[a-z]+;base64,([\d+]|[\w+]|[/])+=+", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        private static volatile Logger _instance = null;
        private static object lockObject = new object();
        private static byte logSource = Convert.ToByte(ConfigurationManager.AppSettings["LogSource"]);
        private static readonly int logLevel = Convert.ToInt32(ConfigurationManager.AppSettings["LogLevel"]);
        internal static readonly int logCapacity = Convert.ToInt32(ConfigurationManager.AppSettings["LogCapacity"]);
        private static readonly string[] escapeParameters = string.IsNullOrEmpty(ConfigurationManager.AppSettings["EscapeParameters"]) ? new string[0] : ConfigurationManager.AppSettings["EscapeParameters"].Split(',');

        private static Regex[] _exraEscape
        {
            get
            {
                
                if (escapeParameters != null && escapeParameters.Any())
                {
                    Regex[] resultArray = new Regex[escapeParameters.Length];
                    for (int i = 0; i < escapeParameters.Length; i++)
                    {
                        resultArray[i] = new Regex(escapeParameters[0] + ":[^,]+,", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                    }
                    return resultArray;
                }

                return new Regex[0];
            }
        }

        /// <summary>
        /// If time defference between hosting and client.
        /// </summary>
        internal protected static readonly int AdditinalHour = Convert.ToInt32(ConfigurationManager.AppSettings["AdditinalHour"]);

        /// <summary>
        /// Project name use this log.
        /// </summary>
        internal protected readonly string logName = ConfigurationManager.AppSettings["LogName"];

        /// <summary>
        /// Info blocking collection.
        /// </summary>
        internal BlockingCollection<LogModel> dataItems = null;

        /// <summary>
        /// Exception blocking collection.
        /// </summary>
        internal BlockingCollection<LogModelException> dataExceptionItems = null;

        /// <summary>
        /// Debug blocking collection.
        /// </summary>
        internal BlockingCollection<LogModelDebug> dataDebugItems = null;

        /// <summary>
        /// Get Instance based on configuration.
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObject)
                    {
                        if (_instance == null)
                        {
                            switch (logLevel)
                            {
                                case 1:
                                    switch (logSource)
                                    {
                                        case 0:
                                            _instance = new TextException();
                                            break;
                                        case 1:
                                            _instance = new SqlExceptionLog();
                                            break;
                                        case 2:
                                            _instance = new XmlException();
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                case 2:
                                    switch (logSource)
                                    {
                                        case 0:
                                            _instance = new TextInfo();
                                            break;
                                        case 1:
                                            _instance = new SqlInfo();
                                            break;
                                        case 2:
                                            _instance = new XmlInfo();
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                case 3:
                                    switch (logSource)
                                    {
                                        case 0:
                                            _instance = new TextDebug();
                                            break;
                                        case 1:
                                            _instance = new SqlDebug();
                                            break;
                                        case 2:
                                            _instance = new XmlDebug();
                                            break;
                                        default:
                                            break;
                                    }
                                    break;

                                default:
                                    _instance = new EmptyLog();
                                    break;
                            }

                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Simple constructor.
        /// </summary>
        internal protected Logger()
        {
            Initiate();
            Task.Run(() => Consumer());
        }
        internal abstract void Initiate();

        /// <summary>
        /// Log exception with filename, method and line number.
        /// </summary>
        /// <param name="e">The exception to be logged</param>
        /// <param name="caption">If you want to add title message for this exception</param>
        /// <param name="ExceptionFinalMessage">Delegate on final exception message</param>
        public virtual void LogException(Exception e, string caption = "", Action<string> ExceptionFinalMessage = null)
        {

            Task.Run(() =>
            {
                try
                {
                    LogModelException model = new LogModelException()
                    {
                        Time = DateTime.Now.AddHours(AdditinalHour),
                    };
                    StringBuilder message = new StringBuilder();
                    Exception temp = e;
                    do
                    {
                        message.Append(temp.Message);
                        if (temp.InnerException == null)
                        {
                            // Create a StackDebug that captures
                            // filename, line number, and column
                            // information for the current thread.
                            StackTrace st = new StackTrace(temp, true);
                            if (st.FrameCount == 0)
                            {
                                model.FileName = "";
                                model.Method = "";
                                model.LineNumber = -1;
                            }
                            else
                            {
                                StackFrame sf = st.GetFrame(0);
                                model.FileName = string.Join(" => ", sf.GetFileName().Split('\\').Skip(2));
                                model.Method = sf.GetMethod().Name;
                                model.LineNumber = sf.GetFileLineNumber();
                            }
                            break;
                        }
                        temp = temp.InnerException;
                    } while (temp != null);
                    model.Data = message.ToString();
                    model.Caption = caption;
                    ExceptionFinalMessage?.Invoke(model.Data);
                    dataExceptionItems.Add(model);

                }
                catch { }
            });
        }

        /// <summary>
        /// Write Info or information log.
        /// </summary>
        /// <param name="method">Method name or caption</param>
        /// <param name="data">The information to be logged</param>
        public abstract void Info(string method, string data);

        /// <summary>
        /// Write http request web Debug log.
        /// </summary>
        /// <param name="client">Client name</param>
        /// <param name="username">Login username</param>
        /// <param name="data">request data</param>
        public abstract void Debug(string client, string username, string data);

        internal abstract void Consumer();

        internal virtual Task WriteInfo()
        {
            throw new NotImplementedException();
        }

        internal virtual Task WriteException()
        {
            throw new NotImplementedException();
        }

        internal virtual Task WriteDebug()
        {
            throw new NotImplementedException();
        }

        protected void GetLogModelDebug(string ip, string url, string httpMethod, string client, string username, string data, string deviceName)
        {

            Task.Run(() =>
            {
                try
                {
                    var tempData = _base64Escape.Replace(data, "[Base64 string]") + ",";
                    foreach (var item in _exraEscape)
                    {
                        tempData = item.Replace(tempData, "");
                    }
                    data = tempData.TrimEnd(',');
                    LogModelDebug model = new LogModelDebug()
                    {
                        Time = DateTime.Now.AddHours(AdditinalHour),
                        Data = data,
                        IP = ip,
                        Client = client,
                        URL = url,
                        Username = username,
                        Method = httpMethod,
                        DeviceName = deviceName
                    };
                    dataDebugItems.Add(model);
                }
                catch { }
            });
        }
    }

    /// <summary>
    /// Use try and cath exception to be centrilized.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Covered function by try and catch.
        /// </summary>
        /// <param name="process">The delegate function to be covered</param>
        /// <param name="catchException">The delegate function done on exception [default log it]</param>
        /// <param name="finalyAction">The delegate in final section of try and catch</param>
        public static void TryCatch(Action process, Action<Exception> catchException = null, Action finalyAction = null)
        {
            try
            {
                process();
            }
            catch (NullReferenceException n)
            {
                HandleException(catchException, n);
            }
            catch (KeyNotFoundException n)
            {
                HandleException(catchException, n);
            }
            catch (IndexOutOfRangeException n)
            {
                HandleException(catchException, n);
            }
            catch (ArgumentOutOfRangeException n)
            {
                HandleException(catchException, n);
            }
            catch (OutOfMemoryException n)
            {
                HandleException(catchException, n);
            }
            catch (TimeoutException n)
            {
                HandleException(catchException, n);
            }
            catch (NoNullAllowedException n)
            {
                HandleException(catchException, n);
            }
            catch (SqlException n)
            {
                HandleException(catchException, n);
            }
            catch (FieldAccessException n)
            {
                HandleException(catchException, n);
            }

            catch (FileNotFoundException n)
            {
                HandleException(catchException, n);
            }
            catch (EndOfStreamException n)
            {
                HandleException(catchException, n);
            }
            catch (UnauthorizedAccessException n)
            {
                HandleException(catchException, n);
            }
            catch (IOException n)
            {
                HandleException(catchException, n);
            }
            catch (ArithmeticException n)
            {
                HandleException(catchException, n);
            }
            catch (DllNotFoundException n)
            {
                HandleException(catchException, n);
            }

            catch (EntryPointNotFoundException n)
            {
                HandleException(catchException, n);
            }
            catch (ObjectDisposedException n)
            {
                HandleException(catchException, n);
            }
            catch (Exception e)
            {
                catchException?.Invoke(e);
            }
            finally
            {
                finalyAction?.Invoke();
            }
        }

        /// <summary>
        /// Covered function by try and catch.
        /// </summary>
        /// <param name="process">The delegate function to be covered</param>
        /// <param name="catchException">The delegate function done on exception [default log it]</param>
        /// <param name="finalyAction">The delegate in final section of try and catch</param>
        public async static Task TryCatchAsync(Func<Task> process, Action<Exception> catchException = null, Action finalyAction = null)
        {
            try
            {
                await process();
            }
            catch (NullReferenceException n)
            {
                HandleException(catchException, n);
            }
            catch (KeyNotFoundException n)
            {
                HandleException(catchException, n);
            }
            catch (IndexOutOfRangeException n)
            {
                HandleException(catchException, n);
            }
            catch (ArgumentOutOfRangeException n)
            {
                HandleException(catchException, n);
            }
            catch (OutOfMemoryException n)
            {
                HandleException(catchException, n);
            }
            catch (TimeoutException n)
            {
                HandleException(catchException, n);
            }
            catch (NoNullAllowedException n)
            {
                HandleException(catchException, n);
            }
            catch (SqlException n)
            {
                HandleException(catchException, n);
            }
            catch (FieldAccessException n)
            {
                HandleException(catchException, n);
            }

            catch (FileNotFoundException n)
            {
                HandleException(catchException, n);
            }
            catch (EndOfStreamException n)
            {
                HandleException(catchException, n);
            }
            catch (UnauthorizedAccessException n)
            {
                HandleException(catchException, n);
            }
            catch (IOException n)
            {
                HandleException(catchException, n);
            }
            catch (ArithmeticException n)
            {
                HandleException(catchException, n);
            }
            catch (DllNotFoundException n)
            {
                HandleException(catchException, n);
            }

            catch (EntryPointNotFoundException n)
            {
                HandleException(catchException, n);
            }
            catch (ObjectDisposedException n)
            {
                HandleException(catchException, n);
            }
            catch (Exception e)
            {
                catchException?.Invoke(e);
            }
            finally
            {
                finalyAction?.Invoke();
            }
        }

        private static void HandleException(Action<Exception> catchException = null, Exception n = null)
        {
            if (catchException == null)
            {
                Logger.Instance.LogException(n);
            }
            else
            {
                catchException(n);
            }
        }
    }
}
