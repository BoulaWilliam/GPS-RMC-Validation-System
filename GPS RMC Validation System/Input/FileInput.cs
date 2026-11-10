
using GPS_RMC_Validation_System.Interface;
using System;
using System.IO;

namespace GPS_RMC_Validation_System.Input
{
    public class FileInput : IGpsInput
    {
        public string GetInput()
        {
            while (true)
            {
                Console.Write("Enter File Path: ");

                string filePath = Console.ReadLine() ?? string.Empty;

                try
                {
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        Console.WriteLine("File Path Cannot Be Empty!");
                        continue;
                    }

                    if (Directory.Exists(filePath))
                    {
                        Console.WriteLine("The Path Is A Folder, Not A File!");
                        continue;
                    }

                    if (!File.Exists(filePath))
                    {
                        Console.WriteLine("File Was Not Found!");
                        continue;
                    }

                    string GPS = File.ReadAllText(filePath);

                    if (string.IsNullOrWhiteSpace(GPS))
                    {
                        Console.WriteLine("File Is Empty!");
                        continue;
                    }

                    return GPS.Trim();
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine(
                        "You Do Not Have Permission To Read This File!");
                }
                catch (IOException)
                {
                    Console.WriteLine(
                        "An Error Occurred While Reading The File!");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid File Path!");
                }
                catch (NotSupportedException)
                {
                    Console.WriteLine("File Path Format Is Not Supported!");
                }

                Console.WriteLine("Please Try Again.\n");
            }
        }
    }
}
