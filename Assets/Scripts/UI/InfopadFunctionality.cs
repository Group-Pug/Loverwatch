using UnityEngine;
using UnityEngine.UI;

public class InfopadFunctionality : MonoBehaviour {
    [SerializeField]
    private Profession profession;

    private CharacterHandler ch;
    private Player player;

    void Start ()
    {
        ch = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CharacterHandler>();
        player = ch.GetPlayer();
    }

    public void HandleProfessionSelection ()
    {
        player.SetProfession(profession);

        UpdateProfessionUI();
        UpdateMentorUI();
    }

    void UpdateProfessionUI ()
    {
        transform.parent.FindChild("Profession").GetComponent<Text>().text = EnumReflection.GetDescription(player.GetProfession());
    }

    void UpdateMentorUI ()
    {
        Profession prof = player.GetProfession();
        string mentor = "";

        if(ch.FindHeroByProfession(prof) != null)
        {
             mentor = ch.FindHeroByProfession(prof).GetFullName();
        }

        transform.parent.FindChild("Mentor").GetComponent<Text>().text = "???";
    }
}
