
using GPS_RMC_Validation_System.Services;

namespace GPS_RMC_Validation_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RmcApplication application = new RmcApplication();
            application.Run();
        }
    }
}
