
using GPS_RMC_Validation_System.Models;

namespace GPS_RMC_Validation_System.Parsing
{
    public class RmcParser
    {
        public RmcData Parse(string GPS)
        {
            // Step 1: Check If GPS Message Is Empty

            if (string.IsNullOrEmpty(GPS))
            {
                throw new FormatException("GPS Message Is Empty!");
            }

            // Step 2: Find Checksum Position

            int starIndex = GPS.IndexOf('*');

            if (starIndex == -1 || starIndex != GPS.LastIndexOf('*'))
            {
                throw new FormatException("Invalid GPS Message Format!");
            }

            // Step 3: Remove Checksum Before Splitting

            string GPSData = GPS.Substring(0, starIndex);

            // Step 4: Split GPS Message

            string[] Fields = GPSData.Split(',');

            // Step 5: Validate Number Of Fields

            if (Fields.Length != 12)
            {
                throw new FormatException("Invalid Number Of RMC Fields!");
            }

            // Step 6: Create RmcData Object

            RmcData rmcData = new RmcData();

            // Step 7: Assign Fields To Properties

            rmcData.MessageType = Fields[0];
            rmcData.Time = Fields[1];
            rmcData.Status = Fields[2];
            rmcData.Latitude = Fields[3];
            rmcData.LatitudeDirection = Fields[4];
            rmcData.Longitude = Fields[5];
            rmcData.LongitudeDirection = Fields[6];
            rmcData.Speed = Fields[7];
            rmcData.Course = Fields[8];
            rmcData.Date = Fields[9];
            rmcData.MagneticVariation = Fields[10];
            rmcData.MagneticDirection = Fields[11];

            // Step 8: Return RmcData Object

            return rmcData;
        }
    }
}
