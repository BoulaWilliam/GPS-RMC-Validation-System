
using GPS_RMC_Validation_System.Models;

namespace GPS_RMC_Validation_System.Display
{
    public class RmcDisplay
    {
        public void Display(RmcData rmcData)
        {
            Console.WriteLine("\n=============================== GPS DATA ===============================");

            Console.WriteLine("{0,-22} | {1}", "Field", "Value");

            Console.WriteLine("-------------------------------------------------------------------------");

            Console.WriteLine("{0,-22} | {1}", "Message Type", rmcData.MessageType);

            Console.WriteLine("{0,-22} | {1}", "Time", rmcData.Time);

            Console.WriteLine("{0,-22} | {1}",
                "Status",
                rmcData.Status == "A" ? "Active (Valid Fix)" : "Void (Invalid Fix)");

            Console.WriteLine("{0,-22} | {1}",
                "Latitude",
                rmcData.Latitude + " " + rmcData.LatitudeDirection);

            Console.WriteLine("{0,-22} | {1}",
                "Longitude",
                rmcData.Longitude + " " + rmcData.LongitudeDirection);

            Console.WriteLine("{0,-22} | {1}", "Speed", rmcData.Speed + " Knots");

            Console.WriteLine("{0,-22} | {1}", "Course", rmcData.Course + " Degrees");

            Console.WriteLine("{0,-22} | {1}", "Date", rmcData.Date);

            Console.WriteLine("{0,-22} | {1}",
                "Magnetic Variation",
                string.IsNullOrEmpty(rmcData.MagneticVariation) &&
                string.IsNullOrEmpty(rmcData.MagneticDirection)
                ? "Not Available"
                : rmcData.MagneticVariation + " " + rmcData.MagneticDirection);

            Console.WriteLine("==========================================================================");

            if (rmcData.Status == "A")
            {
                Console.WriteLine("GPS RMC Message Is Valid!");
            }
            else
            {
                Console.WriteLine("RMC Sentence Is Valid, But GPS Position Fix Is Invalid!");
            }
        }
    }
}
