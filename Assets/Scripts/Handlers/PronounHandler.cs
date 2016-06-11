using UnityEngine;
using System.Collections;

public class PronounHandler : MonoBehaviour {
    private int selectedPronoun;
    private string pSubject;
    private string pObject;
    private string pPossessive;
    private string pReflexive;

    public void SetPronoun (int val)
    {
        selectedPronoun = val;
        SetTerms(val);
    }

    public int GetPronoun ()
    {
        return selectedPronoun;
    }

    public void SetTerms (int val)
    {
        switch(val)
        {
            case 0:         //she
                pSubject = "she";
                pObject = "her";
                pPossessive = "her";
                pReflexive = "herself";
                break;
            case 1:         //they
                pSubject = "they";
                pObject = "them";
                pPossessive = "their";
                pReflexive = "themself";
                break;
            case 2:         //he
                pSubject = "he";
                pObject = "him";
                pPossessive = "his";
                pReflexive = "himself";
                break;
        }
    }

    public string GetSubject ()
    {
        return pSubject;
    }

    public string GetObject ()
    {
        return pObject;
    }

    public string GetPossessive ()
    {
        return pPossessive;
    }

    public string GetReflexive ()
    {
        return pReflexive;
    }
}
