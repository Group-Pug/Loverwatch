public class Character
{
    private string name;
    private Profession profession;
    private float approval;

    public string GetName ()
    {
        return name;
    }

    public void SetName (string val)
    {
        name = val;
    }

    public Profession GetProfession()
    {
        return profession;
    }

    public void SetProfession(Profession val)
    {
        profession = val;
    }
}

