public static class CommonUtilsFheads
{
    public static bool Int2Bool(int _V)
    {
        return _V != 0;
    }
    
    public static int Stadium(int _Game)
    {
        return _Game switch
        {
            0 => 0,
            1 => 1,
            2 => 2,
            3 => 3,
            4 => 6,
            5 => 7,
            6 => 8,
            7 => 9,
            8 => 13,
            9 => 16,
            _ => 0
        };
    }
}