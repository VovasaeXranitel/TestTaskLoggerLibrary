using System;
using TestTaskLibrary;

namespace TestTaskConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Guid guid = new Guid("00000000-0000-0000-0000-000000000000");

           
            try
            {
                LoggerClass.LogWrite(guid, "testModule", "17-02-2024.2.txt", "someAction aoaoa");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.Write("Complete");
            

            
            try
            {
                string output = string.Join("\n", LoggerClass.LogRead("testModule", "17-02-2024.2.txt"));
                Console.WriteLine($"Complete: \n \n {output}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
