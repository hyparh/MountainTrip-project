namespace MountainTrip.Data
{
    public class DataConstants
    {
        // Trip
        public const int TripNameMinLength = 5;        
        public const int TripNameMaxLength = 50;        
        public const int TripDescriptionMinLength = 10;        
        public const string DurationRegex = "^[0-9]{2}[h]:[0-9]{2}[m]$";

        public const string EasyDifficulty = "Easy";
        public const string MediumDifficulty = "Medium";
        public const string HardDifficulty = "Hard";
        public const string ExtremeDifficulty = "Extreme";

        // Mountain
        public const int MountainNameMaxLength = 20;
    }
}
