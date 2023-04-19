namespace Common.Managers.Achievements
{
    public static class AchievementKeys
    {
        public const ushort Level10Finished   = 1;
        public const ushort Level25Finished   = 2;
        public const ushort Level50Finished   = 3;
        public const ushort Level100Finished  = 4;
        public const ushort Level200Finished  = 5;
        public const ushort Level300Finished  = 6;
        public const ushort Level400Finished  = 7;
        public const ushort Level500Finished  = 8;
        public const ushort Level600Finished  = 9;
        public const ushort Level700Finished  = 10;
        public const ushort Level800Finished  = 11;
        public const ushort Level900Finished  = 12;
        public const ushort Level1000Finished = 13;

        public static ushort? GetLevelFinishedAchievementKey(long _LevelIndex)
        {
            return _LevelIndex switch
            {
                10   => Level10Finished,
                25   => Level25Finished,
                50   => Level50Finished,
                100  => Level100Finished,
                200  => Level200Finished,
                300  => Level300Finished,
                400  => Level400Finished,
                500  => Level500Finished,
                600  => Level600Finished,
                700  => Level700Finished,
                800  => Level800Finished,
                900  => Level900Finished,
                1000 => Level1000Finished,
                _    => null
            };
        }
    }
}