[System.Serializable]
public enum Locations
{
    Dorado = 0,
    Anubis = 1,
    Hanamura = 2,
    Ilios = 3,
    KingsRow = 4,
    Lijiang = 5,
    Hollywood = 6,
    Nepal = 7,
    Numbani = 8,
    Route66 = 9,
    HQ = 10,
    Volskaya = 11,
    Antarctica = 12,
    Gibraltar = 13,
    GrandMesa = 14,
}

[System.Serializable]
public class Location {
    public string name;
    public Locations locationEnum;

    public string GetLocationName ()
    {
        return name;
    }
}
