namespace GameLog.Common.Validation;

public static class ValidationConstants
{
    public static class MaxLength
    {
        public const int Email = 255;
        public const int Nickname = 24;
        public const int FirstName = 100;
        public const int LastName = 100;

        public static class GameProfiles
        {
            public const int Name = 150;
            public const int Description = 1000;
            public const int Genre = 100;
            public const int Developer = 200;
            public const int Publisher = 200;
        }
    }
}