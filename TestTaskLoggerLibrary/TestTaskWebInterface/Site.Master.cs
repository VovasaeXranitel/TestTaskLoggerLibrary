using System;
using System.Collections.Generic;
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
            if (ModuleNameInsert != null && FileNameInsert != null)
            {
                string moduleName = ModuleNameInsert.Text;
                string fileName = FileNameInsert.Text;

                string[] LogsData = LoggerClass.LogRead(moduleName, fileName);

                foreach(string log in LogsData)
                {
                    LogLinesOutput.Items.Add(log);
                }
            }
            else
            {
                //Здесь надо разместить код всплывающего окна в случае если не были заполнены поля модуля и файла
            }
        }
    }
}