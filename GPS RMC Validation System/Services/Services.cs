
using GPS_RMC_Validation_System.Display;
using GPS_RMC_Validation_System.Input;
using GPS_RMC_Validation_System.Interface;
using GPS_RMC_Validation_System.Models;
using GPS_RMC_Validation_System.Parsing;
using GPS_RMC_Validation_System.Validation;
using System;

namespace GPS_RMC_Validation_System.Services
{
    internal class RmcApplication
    {
        #region Run Application
        public void Run()
        {
            try
            {
                string GPS = GetGPSInput();

                if (!IdentifyRMC(GPS)) return;
                if (!ValidateChecksum(GPS)) return;

                if (!TryParseGPS(GPS, out RmcData rmcData)) return;
                if (!ValidateRMC(rmcData)) return;

                DisplayGPS(rmcData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }

        #endregion

        #region Get GPS Input

        private string GetGPSInput()
        {
            while (true)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("   GPS RMC Validation System");
                Console.WriteLine("==================================");

                Console.WriteLine("Choose Input Source:");
                Console.WriteLine("1. Console Input");
                Console.WriteLine("2. File Input");

                Console.Write("Your Choice: ");

                string choice = Console.ReadLine() ?? string.Empty;

                IGpsInput gpsInput;

                switch (choice)
                {
                    case "1":
                        gpsInput = new ConsoleInput();
                        break;

                    case "2":
                        gpsInput = new FileInput();
                        break;

                    default:
                        Console.WriteLine("Invalid Choice! Please Try Again.\n");
                        continue;
                }

                Console.Clear();

                return gpsInput.GetInput();
            }
        }

        #endregion

        #region Validate Checksum

        private bool ValidateChecksum(string GPS)
        {
            ChecksumValidator checksumValidator = new ChecksumValidator();

            if (!checksumValidator.ValidateCheckSum(GPS))
            {
                Console.WriteLine("Invalid Checksum!");
                return false;
            }

            return true;
        }

        #endregion

        #region Identify RMC

        private bool IdentifyRMC(string GPS)
        {
            int commaIndex = GPS.IndexOf(',');

            if (commaIndex == -1)
            {
                Console.WriteLine("Invalid NMEA Message Format!");
                DisplayNMEAProtocol();
                return false;
            }

            string messageType = GPS.Substring(0, commaIndex);

            RmcValidator rmcValidator = new RmcValidator();

            if (!rmcValidator.ValidateMessageType(messageType))
            {
                Console.WriteLine("This Is Not An RMC Message!");
                DisplayNMEAProtocol();
                return false;
            }

            return true;
        }

        #endregion

        #region Parse GPS

        private bool TryParseGPS(string GPS, out RmcData rmcData)
        {
            RmcParser rmcParser = new RmcParser();

            try
            {
                rmcData = rmcParser.Parse(GPS);
                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Parsing Error: " + ex.Message);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Invalid GPS Message Format!");
            }

            rmcData = new RmcData();
            return false;
        }

        #endregion

        #region Validate RMC

        private bool ValidateRMC(RmcData rmcData)
        {
            RmcValidator rmcValidator = new RmcValidator();

            bool isValid = rmcValidator.ValidateRmcData(
                rmcData.MessageType,
                rmcData.Time,
                rmcData.Status,
                rmcData.Latitude,
                rmcData.LatitudeDirection,
                rmcData.Longitude,
                rmcData.LongitudeDirection,
                rmcData.Speed,
                rmcData.Course,
                rmcData.Date,
                rmcData.MagneticVariation,
                rmcData.MagneticDirection
            );

            if (!isValid)
            {
                Console.WriteLine("Invalid RMC Data!");
                return false;
            }

            return true;
        }

        #endregion

        #region Display GPS

        private void DisplayGPS(RmcData rmcData)
        {
            RmcDisplay rmcDisplay = new RmcDisplay();
            rmcDisplay.Display(rmcData);
        }

        #endregion

        #region Display NMEA Protocol

        private void DisplayNMEAProtocol()
        {
            Console.WriteLine("\n========== NMEA 0183 RMC PROTOCOL ==========");

            Console.WriteLine("Expected Message Format:");
            Console.WriteLine("$GPRMC,Time,Status,Latitude,N/S,Longitude,E/W,Speed,Course,Date,MagneticVariation,E/W*Checksum");

            Console.WriteLine("\nExample:");
            Console.WriteLine("$GPRMC,123519,A,4807.038,N,01131.000,E,022.4,084.4,230394,003.1,W*6A");

            Console.WriteLine("\nSupported Message Types:");
            Console.WriteLine("$GPRMC");
            Console.WriteLine("$GNRMC");

            Console.WriteLine("=============================================\n");
        }

        #endregion
    }
}
