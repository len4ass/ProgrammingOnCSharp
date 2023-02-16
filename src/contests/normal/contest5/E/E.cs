internal static class GiftCreator
{
    private static int _currentPhone;
    private static int _currentPlayStation;
    
    public static Gift CreateGift(string giftName)
    {
        if (giftName == "Phone")
        {
            return new Phone(_currentPhone++);
        }
        
        if (giftName == "PlayStation")
        {
            return new PlayStation(_currentPlayStation++);
        }

        return null;
    }
}

internal sealed class Phone : Gift
{

    public Phone(int id) : base(id)
    {
        
    }
}

internal sealed class PlayStation : Gift
{
    public PlayStation(int id) : base(id)
    {
        
    }
}