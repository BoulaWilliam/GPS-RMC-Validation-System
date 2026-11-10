using System;
using System.Collections.Generic;
using System.Text;

namespace GPS_RMC_Validation_System.Models
{
    public class RmcData
    {
        public string MessageType { get; set; } = string.Empty;

        public string Time { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public string Latitude { get; set; } = string.Empty;

        public string LatitudeDirection { get; set; } = string.Empty;

        public string Longitude { get; set; } = string.Empty;

        public string LongitudeDirection { get; set; } = string.Empty;

        public string Speed { get; set; } = string.Empty;

        public string Course { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string MagneticVariation { get; set; } = string.Empty;

        public string MagneticDirection { get; set; } = string.Empty;
    }
}
