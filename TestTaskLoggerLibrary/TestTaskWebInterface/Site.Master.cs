using Microsoft.Ajax.Utilities;
using System;
using System.Web.UI;
using TestTaskLibrary;

namespace TestTaskWebInterface
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Clicking a button that displays the text of the log file
        /// </summary>
        protected void FileSearchButton_Click(object sender, EventArgs e)
        {
            //Get the name of the module and file
            string moduleName = ModuleNameInsert.Text;
            string fileName = FileNameInsert.Text;

            //We check whether the input is correct and display entries from the logs on the screen
            if (!moduleName.IsNullOrWhiteSpace() && !fileName.IsNullOrWhiteSpace())
            { 
                string[] LogsData = LoggerClass.LogRead(moduleName, fileName);

                foreach(string log in LogsData)
                {
                    LogLinesOutput.Items.Add(log);
                }
            }
            //If the data is not entered, an error message is displayed on the screen.
            else
            {
                string errorLine = "An error has occurred, check that the entered module name and file name are correct";
                LogLinesOutput.Items.Add(errorLine);

            }
        }
    }
}