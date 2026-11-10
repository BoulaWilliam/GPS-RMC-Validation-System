
using GPS_RMC_Validation_System.Interface;
using System;

namespace GPS_RMC_Validation_System.Input
{
    public class ConsoleInput : IGpsInput
    {
        public string GetInput()
        {
            while (true)
            {
                Console.WriteLine("Enter GPS RMC Message:");

                string GPS = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(GPS))
                {
                    Console.WriteLine("GPS RMC Message Cannot Be Empty!");
                    Console.WriteLine("Please Try Again.\n");
                    continue;
                }

                return GPS.Trim();
            }
        }
    }
}
