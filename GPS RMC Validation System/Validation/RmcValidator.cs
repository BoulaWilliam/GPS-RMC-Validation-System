
using System;
using System.Globalization;

namespace GPS_RMC_Validation_System.Validation
{
    public class RmcValidator
    {
        // Step 1: Validate Message Type
        public bool ValidateMessageType(string messageType)
        {
            if (messageType == "$GPRMC" || messageType == "$GNRMC")
            {
                return true;
            }

            return false;
        }

        // Step 2: Validate Time
        public bool ValidateTime(string time)
        {
            if (string.IsNullOrEmpty(time) || time.Length < 6)
            {
                return false;
            }

            int hours;
            int minutes;
            double seconds;

            if (!int.TryParse(time.Substring(0, 2),
                NumberStyles.None, CultureInfo.InvariantCulture, out hours) ||
                !int.TryParse(time.Substring(2, 2),
                NumberStyles.None, CultureInfo.InvariantCulture, out minutes) ||
                !double.TryParse(time.Substring(4),
                NumberStyles.Float, CultureInfo.InvariantCulture, out seconds))
            {
                return false;
            }

            if (double.IsNaN(seconds) || double.IsInfinity(seconds))
            {
                return false;
            }

            if (hours > 23 || minutes > 59 || seconds >= 60 || seconds < 0)
            {
                return false;
            }

            return true;
        }

        // Step 3: Validate Status
        public bool ValidateStatus(string status)
        {
            if (status == "V" || status == "A")
            {
                return true;
            }

            return false;
        }

        // Step 4: Validate Latitude
        public bool ValidateLatitude(string latitude, string direction)
        {
            if (string.IsNullOrEmpty(latitude) ||
                latitude.Length < 5 ||
                string.IsNullOrEmpty(direction))
            {
                return false;
            }

            string degreesText = latitude.Substring(0, 2);
            string minutesText = latitude.Substring(2);

            int degrees;
            double minutes;

            if (!int.TryParse(degreesText, NumberStyles.None,
                CultureInfo.InvariantCulture, out degrees) ||
                !double.TryParse(minutesText, NumberStyles.Float,
                CultureInfo.InvariantCulture, out minutes))
            {
                return false;
            }

            if (double.IsNaN(minutes) || double.IsInfinity(minutes))
            {
                return false;
            }

            if (degrees > 90)
            {
                return false;
            }

            if (minutes < 0 || minutes >= 60)
            {
                return false;
            }

            if (degrees == 90 && minutes != 0)
            {
                return false;
            }

            if (direction != "N" && direction != "S")
            {
                return false;
            }

            return true;
        }

        // Step 5: Validate Longitude
        public bool ValidateLongitude(string longitude, string direction)
        {
            if (string.IsNullOrEmpty(longitude) ||
                longitude.Length < 6 ||
                string.IsNullOrEmpty(direction))
            {
                return false;
            }

            string degreesText = longitude.Substring(0, 3);
            string minutesText = longitude.Substring(3);

            int degrees;
            double minutes;

            if (!int.TryParse(degreesText, NumberStyles.None,
                CultureInfo.InvariantCulture, out degrees) ||
                !double.TryParse(minutesText, NumberStyles.Float,
                CultureInfo.InvariantCulture, out minutes))
            {
                return false;
            }

            if (double.IsNaN(minutes) || double.IsInfinity(minutes))
            {
                return false;
            }

            if (degrees > 180)
            {
                return false;
            }

            if (minutes < 0 || minutes >= 60)
            {
                return false;
            }

            if (degrees == 180 && minutes != 0)
            {
                return false;
            }

            if (direction != "E" && direction != "W")
            {
                return false;
            }

            return true;
        }

        // Step 6: Validate Speed
        public bool ValidateSpeed(string speed)
        {
            double speedValue;

            if (!double.TryParse(speed, NumberStyles.Float,
                CultureInfo.InvariantCulture, out speedValue) ||
                double.IsNaN(speedValue) ||
                double.IsInfinity(speedValue) ||
                speedValue < 0)
            {
                return false;
            }

            return true;
        }

        // Step 7: Validate Course
        public bool ValidateCourse(string course)
        {
            double courseValue;

            if (!double.TryParse(course, NumberStyles.Float,
                CultureInfo.InvariantCulture, out courseValue) ||
                double.IsNaN(courseValue) ||
                double.IsInfinity(courseValue) ||
                courseValue < 0 || courseValue > 360)
            {
                return false;
            }

            return true;
        }


        // Step 8: Validate Date
        public bool ValidateDate(string date)
        {
            if (string.IsNullOrEmpty(date) || date.Length != 6)
            {
                return false;
            }

            string dayText = date.Substring(0, 2);
            string monthText = date.Substring(2, 2);
            string yearText = date.Substring(4, 2);

            int day;
            int month;
            int year;

            if (!int.TryParse(dayText, NumberStyles.None,
                CultureInfo.InvariantCulture, out day) ||
                !int.TryParse(monthText, NumberStyles.None,
                CultureInfo.InvariantCulture, out month) ||
                !int.TryParse(yearText, NumberStyles.None,
                CultureInfo.InvariantCulture, out year))
            {
                return false;
            }

            if (month < 1 || month > 12 || day < 1)
            {
                return false;
            }

            // Assuming years from 2000 to 2099
            int fullYear = 2000 + year;

            int daysInMonth = DateTime.DaysInMonth(fullYear, month);

            if (day > daysInMonth)
            {
                return false;
            }

            return true;
        }

        public bool ValidateMagneticVariation(string magneticVariation, string direction)
        {
            // Both fields are optional
            if (string.IsNullOrEmpty(magneticVariation) &&
                string.IsNullOrEmpty(direction))
            {
                return true;
            }

            // Both fields must be provided together
            if (string.IsNullOrEmpty(magneticVariation) ||
                string.IsNullOrEmpty(direction))
            {
                return false;
            }

            double variationValue;

            if (!double.TryParse(magneticVariation, NumberStyles.Float,
                CultureInfo.InvariantCulture, out variationValue) ||
                double.IsNaN(variationValue) ||
                double.IsInfinity(variationValue) ||
                variationValue < 0 || variationValue > 180)
            {
                return false;
            }

            if (direction != "E" && direction != "W")
            {
                return false;
            }

            return true;
        }
        // Step 10: Validate Complete RMC Data
        public bool ValidateRmcData(
            string messageType,
            string time,
            string status,
            string latitude,
            string latitudeDirection,
            string longitude,
            string longitudeDirection,
            string speed,
            string course,
            string date,
            string magneticVariation,
            string magneticDirection)
        {
            if (!ValidateMessageType(messageType))
                return false;

            if (!ValidateTime(time))
                return false;

            if (!ValidateStatus(status))
                return false;

            if (!ValidateLatitude(latitude, latitudeDirection))
                return false;

            if (!ValidateLongitude(longitude, longitudeDirection))
                return false;

            if (!ValidateSpeed(speed))
                return false;

            if (!ValidateCourse(course))
                return false;

            if (!ValidateDate(date))
                return false;

            if (!ValidateMagneticVariation(magneticVariation, magneticDirection))
                return false;

            return true;
        }
    }
}
