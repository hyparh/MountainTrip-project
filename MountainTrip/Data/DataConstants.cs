namespace MountainTrip.Data
{
    public class DataConstants
    {
        public class User 
        {
            public const int FullNameMinLength = 4;
            public const int FullNameMaxLength = 50;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }

        public class Trip 
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 50;
            public const int DescriptionMinLength = 10;
            public const string DurationRegex = "^[0-9]{2}[h]:[0-9]{2}[m]$";

            public const string EasyDifficulty = "Easy";
            public const string MediumDifficulty = "Medium";
            public const string HardDifficulty = "Hard";
            public const string ExtremeDifficulty = "Extreme";
        }

        public class Mountain 
        {
            public const int NameMaxLength = 20;
        }

        public class Guide 
        {
            public const int FullNameMinLength = 3;
            public const int FullNameMaxLength = 50;
            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 30;
        }                     
    }
}
