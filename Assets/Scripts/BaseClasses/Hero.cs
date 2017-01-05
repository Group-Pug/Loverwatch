public class Hero : Character {
    private string fullName;
    private float relationship; //relationship meter towards player (0-9)

    public string GetFullName ()
    {
        return fullName;
    }

    public void SetFullName (string val)
    {
        fullName = val;
    }

    public float GetRelationShipStatus ()
    {
        return relationship;
    }

    public void ModRelationship (float val)
    {
        relationship += val;
        
    }


    
}
