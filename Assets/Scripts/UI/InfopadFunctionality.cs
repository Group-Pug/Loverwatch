using UnityEngine;
using UnityEngine.UI;

public class InfopadFunctionality : MonoBehaviour {
    [SerializeField]
    private Profession profession;

    private CharacterHandler ch;
    private Player player;

    private Fungus.Flowchart flow;

    void Start ()
    {
        ch = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CharacterHandler>();
        player = ch.GetPlayer();

        flow = GameObject.Find("Fungus").GetComponent<Fungus.Flowchart>();
        
    }

    public void HandleProfessionSelection ()
    {
        player.SetProfession(profession);
        flow.SetStringVariable("Profession", GetRouteString());

        UpdateProfessionUI();
        UpdateMentorUI();
    }

    private string GetRouteString ()
    {
        switch(profession)
        {
            case Profession.BirdWatcher:
                return "BastionRoute";
            case Profession.Oceanographer:
                return "MeiRoute";
            case Profession.Reconnaissance:
                return "GenjiRoute";
            default:
                return null;
        }
    }

    void UpdateProfessionUI ()
    {
        transform.parent.FindChild("Profession").GetComponent<Text>().text = EnumReflection.GetDescription(player.GetProfession());
    }

    void UpdateMentorUI ()
    {
        Profession prof = player.GetProfession();
        //string mentor = "";

        if(ch.FindHeroByProfession(prof) != null)
        {
             //mentor = ch.FindHeroByProfession(prof).GetFullName();
        }

        transform.parent.FindChild("Mentor").GetComponent<Text>().text = "???";
    }
}
