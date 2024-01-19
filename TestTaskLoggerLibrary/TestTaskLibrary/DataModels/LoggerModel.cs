using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskLibrary
{
    internal class LoggerModel
    {
        /// <summary>
        /// The ID of the user who creates log entries
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// ID of the module that is currently being used
        /// </summary>
        public Guid Module { get; set; }
        /// <summary>
        /// A string with the path to the module
        /// </summary>
        public string PathToModulesDirectory { get; set; }
        /// <summary>
        /// The name of the module in the form of a string
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// The name of the file in the form of a string
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// The type of actions performed by the user
        /// </summary>
        public string ActionType { get; set; }
        /// <summary>
        /// Field that is used for the path to a new file when moving from one file to another
        /// </summary>
        public string CreatedFilePath { get; set; }
    }
}
