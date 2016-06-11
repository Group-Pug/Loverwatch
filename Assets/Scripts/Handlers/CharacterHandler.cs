using UnityEngine;
using System.Collections.Generic;

public class CharacterHandler : MonoBehaviour {
    [SerializeField]
    private Transform characterHolder;

    private List<Hero> heroes = new List<Hero>();
    private Player player = new Player();

    void Start ()
    {
        foreach(Transform child in characterHolder)
        {
            string name = child.GetComponent<Fungus.Character>().GetStandardText();
            string fullName = child.GetComponent<Fungus.Character>().fullName;
            Profession prof = child.GetComponent<Fungus.Character>().profession;


            if (name == "Player")
            {
                player.SetName(name);
            }
            else
            {
                Hero newHero = new Hero();
                newHero.SetName(name);
                newHero.SetFullName(fullName);
                newHero.SetProfession(prof);
                heroes.Add(newHero);
            }
        }
    }

    public Player GetPlayer ()
    {
        return player;
    }

    public Hero FindHeroByProfession (Profession val)
    {
        Hero found = null;

        foreach(Hero h in heroes)
        {
            if(h.GetProfession() == val)
            {
                found = h;
            }
        }

        return found;
    }

}
