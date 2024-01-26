using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestTaskLibrary
{
    internal class LoggerModel
    {
        /// <summary>
        /// Path to the folder with all logs
        /// </summary>
        public string pathToLogsFolder { get; set; } = @"C:\Logs";

        /// <summary>
        /// Name of the module in which the actions are performed
        /// </summary>
        public string moduleName { get; set; }

        /// <summary>
        /// The name of the file that needs to be checked and possibly archived
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// The ID of the user who creates log entries
        /// </summary>
        public Guid userID { get; set; }

        /// <summary>
        /// The type of actions performed by the user
        /// </summary>
        public string actionType { get; set; }

        /// <summary>
        ///  Maximum File Size, default value is 500 MB
        /// </summary>
        public int maxFileSize { get; set;} = 500000000;
    }
}
