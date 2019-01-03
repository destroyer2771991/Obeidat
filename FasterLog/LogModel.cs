using System;
using System.Globalization;

namespace ObeidatLog
{
    /// <summary>
    /// Info log model.
    /// </summary>
    internal  class LogModel
    {
        /// <summary>
        /// Method (function) name.
        /// </summary>
        public string Method { get; set; }
        private static CultureInfo culture = new CultureInfo("en-US");

        /// <summary>
        /// Information data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Time for this log.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// time in "yyyy-MM-dd HH:mm:ss:ff" to log in text file.
        /// </summary>
        public string StringTime
        {
            get
            {
                if (Time != null)
                {
                    return Time.ToString("HH:mm:ss:ff", culture);
                }
                return string.Empty;
            }
        }
    }

    /// <summary>
    /// Exception log model.
    /// </summary>
    internal class LogModelException : LogModel
    {
        /// <summary>
        /// Caption message for exception.
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Line number where exception happend in file.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// File name where exception happend.
        /// </summary>
        public string FileName { get; set; }
      
    }

    /// <summary>
    /// Debug log model
    /// </summary>
    internal class LogModelDebug : LogModel
    {
        /// <summary>
        /// Request Ip address.
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Request device name
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Login username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Client name.
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// Request url.
        /// </summary>
        public string URL { get; set; }
    }
}
