public class Hero : Character {
    private string fullName;
    private int relationship; //relationship meter towards player (0-100)

    public string GetFullName ()
    {
        return fullName;
    }

    public void SetFullName (string val)
    {
        fullName = val;
    }

    public int GetRelationShipStatus ()
    {
        return relationship;
    }

    public void ModRelationship (int val)
    {
        relationship += val;
    }


    
}
