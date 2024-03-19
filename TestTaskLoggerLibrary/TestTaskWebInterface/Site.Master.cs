using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestTaskLibrary;

namespace TestTaskWebInterface
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void FileSearchButton_Click(object sender, EventArgs e)
        {
            string moduleName = ModuleNameInsert.Text;
            string fileName = FileNameInsert.Text;

            if (!moduleName.IsNullOrWhiteSpace() && !fileName.IsNullOrWhiteSpace())
            { 
                string[] LogsData = LoggerClass.LogRead(moduleName, fileName);

                foreach(string log in LogsData)
                {
                    LogLinesOutput.Items.Add(log);
                }
            }
            else
            {
                string errorLine = "An error has occurred, check that the entered module name and file name are correct";
                LogLinesOutput.Items.Add(errorLine);

            }
        }
    }
}