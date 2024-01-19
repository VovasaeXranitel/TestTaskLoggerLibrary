using System;
using System.IO;
using NLog;
using NLog.Targets;

namespace TestTaskLibrary
{
    public class LoggerClass
    {
        //Maximum size of file
        public const int MaxFileSize = 500000000;

        /// <summary>
        /// Method for checking the file size
        /// </summary>
        /// <param name="PathToModulesDirectory">A string with the path to the modules folder</param>
        /// <param name="ModuleName">The name of the module</param>
        /// <param name="FileName">The name of the file</param>
        /// <returns>The result of the check is in the form of a Boolean value</returns>
        public bool SizeCheck(string PathToModulesDirectory, string ModuleName, string FileName)
        {
            //Creating a path to the log file
            string PathToFile = PathToModulesDirectory + "/" + ModuleName + "/" + FileName;

            //Recieving Size of file with logs
            FileInfo LogFileInfo = new FileInfo(PathToFile);
            long FileSize = LogFileInfo.Length;

            //Variable, which will be returned as a result of our check
            bool Result;

            //Checking file size
            if (FileSize > MaxFileSize)
            {
                //If Result is true - file is overflowed, and we need to create a new file to continue logging
                Result = true;
            }
            else
            {
                //If Result is false - we don't need to create a new file
                Result = false;
            }
            //Returning result of our check
            return Result;
        }

        /// <summary>
        /// A method that, in the absence of a directory for logging files of a certain module, creates it
        /// </summary>
        /// <param name="PathToModulesDirectory">A string with the path to the modules folder</param>
        /// <param name="ModuleName">The name of the module</param>
        public void CreateNewDirectory(string PathToModulesDirectory, string ModuleName)
        {
            //Creating the expected or existing path to the module folder
            string PathToModuleForlder = PathToModulesDirectory + "/" + ModuleName;

            //Checking if the folder does not exist
            if (!Directory.Exists(PathToModuleForlder))
            {
                //If the folder does not exist, create a folder
                Directory.CreateDirectory(PathToModuleForlder);
            }
        }

        /// <summary>
        /// A method that creates a new file to write logs there in a specific folder related to a specific module.
        /// </summary>
        /// <param name="PathToModulesDirectory">A string with the path to the modules folder</param>
        /// <param name="ModuleName">The name of the module</param>
        /// <param name="FileName">The name of the file</param>
        /// <returns>The path to the file created inside the method</returns>
        public string CreateNewFile(string PathToModulesDirectory, string ModuleName, string FileName)
        {
            //Creating a path to the resulting file and adding date and time of creation as navigation throught all log files
            string PathToResultFile = PathToModulesDirectory + "/" + ModuleName + "/" + DateTime.Now;

            //Calling the method for creating a new directory
            CreateNewDirectory(PathToModulesDirectory, ModuleName);

            //Checking the size of the current file
            bool CheckResult = SizeCheck(PathToModulesDirectory, ModuleName, FileName);

            //We check what the SizeCheck method outputs
            if (CheckResult == true)
            {
                //Creating new log file
                File.Create(PathToResultFile);
            }

            //Returning Path to a new file, which we created in this method
            return PathToResultFile;
        }

        public void ChangeLoggingFile(string PathToModulesDirectory, string ModuleName, string FileName)
        {

            if (SizeCheck(PathToModulesDirectory, ModuleName, FileName) == true)
            {
                string CreatedFilePath = CreateNewFile(PathToModulesDirectory, ModuleName, FileName);

                var fileTarget = new FileTarget("logfile")
                {
                    FileName = CreatedFilePath
                };
            }
        }
    }
}
