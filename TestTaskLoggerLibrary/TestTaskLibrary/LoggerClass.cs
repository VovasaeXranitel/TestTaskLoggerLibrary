using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NLog;

namespace TestTaskLibrary
{
    public class LoggerClass
    {
        public Guid UserID { get; set; }
        public Guid Module {  get; set; }
        public string ActionType { get; set; }
        public string PathToSaveFile { get; set; }

        public LoggerClass(int MaxFileSize, Guid UserID, Guid Module, string ActionType, string PathToSaveFile) 
        {
            this.UserID = UserID;
            this.Module = Module;
            this.ActionType = ActionType;
            this.PathToSaveFile = PathToSaveFile;
        }

        public bool SizeCheck(string PathToFile)
        {
          FileInfo LogFileInfo = new FileInfo(PathToFile);
          long FileSize = LogFileInfo.Length;

          bool Result;
          
          if (FileSize > 500000000)
          {
            Result = true;
          }
          else
          {
            Result= false;
          }
          
          return Result;
        }

        public void CreateNewFile()
        {

        }

        public void CreateNewDirectory()
        {

        }
        
        public void FillingLoggingFile()
        {

        }
    }
}
