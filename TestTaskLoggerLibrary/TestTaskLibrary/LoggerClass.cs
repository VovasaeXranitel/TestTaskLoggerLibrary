using System;
using System.IO;
using NLog;
using NLog.Targets;

namespace TestTaskLibrary
{
    public class LoggerClass
    {
        //Maximum size of file
        public const int maxFileSize = 500000000;

        /// <summary>
        /// Method for checking the file size
        /// </summary>
        /// <param name="pathToModulesDirectory">A string with the path to the modules folder</param>
        /// <param name="moduleName">The name of the module</param>
        /// <param name="fileName">The name of the file</param>
        /// <returns>The result of the check is in the form of a Boolean value</returns>
        public bool SizeCheck(string pathToModulesDirectory, string moduleName, string fileName)
        {
            //Creating a path to the log file
            string pathToFile = Path.Combine(pathToModulesDirectory, moduleName, fileName);
            Path.GetFullPath(pathToFile);

            //Recieving Size of file with logs
            FileInfo logFileInfo = new FileInfo(pathToFile);
            long fileSize = logFileInfo.Length;

            //Variable, which will be returned as a result of our check
            bool result;

            //Checking file size
            if (fileSize > maxFileSize)
            {
                //If result is true - file is overflowed, and we need to create a new file to continue logging
                result = true;
            }
            else
            {
                //If result is false - we don't need to create a new file
                result = false;
            }
            //Returning result of our check
            return result;
        }

        /// <summary>
        /// A method that, in the absence of a directory for logging files of a certain module, creates it
        /// </summary>
        /// <param name="pathToModulesDirectory">A string with the path to the modules folder</param>
        /// <param name="moduleName">The name of the module</param>
        public void CreateNewDirectory(string pathToModulesDirectory, string moduleName)
        {
            //Creating the expected or existing path to the module folder
            string pathToModuleForlder = pathToModulesDirectory + "/" + moduleName;

            //Checking if the folder does not exist
            if (!Directory.Exists(pathToModuleForlder))
            {
                //If the folder does not exist, create a folder
                Directory.CreateDirectory(pathToModuleForlder);
            }
        }

        /// <summary>
        /// A method that creates a new file to write logs there in a specific folder related to a specific module.
        /// </summary>
        /// <param name="pathToModulesDirectory">A string with the path to the modules folder</param>
        /// <param name="moduleName">The name of the module</param>
        /// <param name="fileName">The name of the file</param>
        /// <returns>The path to the file created inside the method</returns>
        public string CreateNewFile(string pathToModulesDirectory, string moduleName, string fileName)
        {
            //Creating a path to the resulting file and adding date and time of creation as navigation throught all log files
            string pathToResultFile = pathToModulesDirectory + "/" + moduleName + "/" + DateTime.Now;

            //Calling the method for creating a new directory
            CreateNewDirectory(pathToModulesDirectory, moduleName);

            //Checking the size of the current file
            bool checkResult = SizeCheck(pathToModulesDirectory, moduleName, fileName);

            //We check what the SizeCheck method outputs
            if (checkResult == true)
            {
                //Creating new log file
                File.Create(pathToResultFile);
            }

            //Returning Path to a new file, which we created in this method
            return pathToResultFile;
        }

        public void ChangeLoggingFile(string pathToModulesDirectory, string moduleName, string fileName)
        {

            if (SizeCheck(pathToModulesDirectory, moduleName, fileName) == true)
            {
                string createdFilePath = CreateNewFile(pathToModulesDirectory, moduleName, fileName);

                var fileTarget = new FileTarget("logfile")
                {
                    FileName = createdFilePath
                };
            }
        }
    }
}
