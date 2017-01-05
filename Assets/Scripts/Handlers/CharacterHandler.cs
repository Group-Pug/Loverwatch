using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterHandler : MonoBehaviour {
    [SerializeField]
    private Transform characterHolder;
    [SerializeField]
    private Transform romanceContainer;

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

    //updates the actual hero values
    public void ModRelationship (string val, float mod)
    {
        for (int i = 0; i < heroes.Count; i++)
        {
            if (heroes[i].GetName() == val)
            {
                heroes[i].ModRelationship(mod);
            }
        }

        UpdateRomanceCounter(val);
    }

    //updates the object that displays the relationship
    public void UpdateRomanceCounter (string val)
    {
        float status = 0f;

        for(int i = 0; i < heroes.Count; i++)
        {
            if(heroes[i].GetName() == val)
            {
                status = heroes[i].GetRelationShipStatus() / 9; //hardcoded as our bar is only 9 spaces long
            }
        }

        Vector2 ogSize = romanceContainer.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        status *= ogSize.x; //relative to the base
        Vector2 newSize = new Vector2(status, ogSize.y);

        //TODO: HOLY SHIT YOU'RE A DIRTY MOTHERFUCK YOU BETTER FIX THIS SHIT BEFORE SHIP
        romanceContainer.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = newSize;
    }

}
