
namespace GPS_RMC_Validation_System.Validation
{
    public class ChecksumValidator
    {
        public bool ValidateCheckSum(string GPS)
        {
            int starIndex = GPS.IndexOf('*');

            if (starIndex == -1 ||
                starIndex != GPS.LastIndexOf('*') ||
                GPS.Length != starIndex + 3)
            {
                return false;
            }

            string checksumString = GPS.Substring(starIndex + 1, 2);

            int checkSum = 0;

            for (int i = 1; i < starIndex; i++)
            {
                checkSum ^= GPS[i];
            }

            string calculatedChecksum = checkSum.ToString("X2");

            return calculatedChecksum == checksumString;
        }
    }
}
