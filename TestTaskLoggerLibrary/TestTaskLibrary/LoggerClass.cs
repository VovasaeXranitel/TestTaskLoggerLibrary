using System;
using System.IO;

namespace TestTaskLibrary
{
    public class LoggerClass
    {
        /// <summary>
        /// Checking the existence of a folder for all logs. If the folder does not exist, then the method creates such a folder
        /// </summary>
        /// <param name="pathToLogsFolder">Path to the required folder</param>
        /// <returns>A boolean value that we use in subsequent methods to check whether a given method worked</returns>
        public bool LogsFolderCheck(string pathToLogsFolder)
        {
            if (Directory.Exists(pathToLogsFolder))
            {
                //If the folder exists, simply return true
                return true;
            }
            else 
            {
                //If the folder does not exist, then create the folder and still return true
                Directory.CreateDirectory(pathToLogsFolder);
                return true;
            }
        }

        /// <summary>
        /// Checks the existence of a folder with logs for a specific module. If there is no such folder for the module, it creates it.
        /// </summary>
        /// <param name="pathToLogsFolder">The path to the folder with all logs in general. It is needed to create a path to the folder with the module.</param>
        /// <param name="moduleName">The name of the module. It is necessary to search or create a folder</param>
        /// <returns>A boolean value that we use in subsequent methods to check whether a given method worked</returns>
        public bool ModuleFolderCheck(string pathToLogsFolder, string moduleName)
        {
            //Creating a string with the path to the module folder and normalizing the path
            string pathToModuleFolder = Path.Combine(pathToLogsFolder, moduleName);
            Path.GetFullPath(pathToModuleFolder);

            if(Directory.Exists(pathToModuleFolder))
            {
                //The folder exists, nothing needs to be created, return true
                return true;
            }
            else
            {
                //The folder does not exist.Creating it and returning trueThe folder does not exist.Creating it and returning true
                Directory.CreateDirectory(pathToModuleFolder);
                return true;
            }
        }
        
        //TODO: Допилить этот метод, еще раз подумать над присвоением нового значения numOfLog, понять почему не все пути к коду возвращают значение и как это обойти.
        public string FileCheck(string pathToLogsFolder, string moduleName, string fileName, int maxFileSize)
        {
            string pathToTargetFile = Path.Combine(pathToLogsFolder, moduleName, fileName);
            Path.GetFullPath(pathToTargetFile);

            int numOfLog = 0;
            string newFileName = DateTime.Today.ToString() + numOfLog;

            string pathToNewFile = Path.Combine(pathToLogsFolder, moduleName, newFileName);
            Path.GetFullPath(pathToNewFile);

            FileInfo targetFileInfo = new FileInfo(pathToTargetFile);
            long targetFileSize = targetFileInfo.Length;

            if(targetFileSize > maxFileSize && File.Exists(pathToTargetFile))
            {
                File.CreateText(pathToNewFile);
                numOfLog++;
                return newFileName;
            }
            else if(!File.Exists(pathToTargetFile)) 
            {
                File.CreateText(pathToTargetFile);
                return fileName;
            }
            
        }


       

    }
}
