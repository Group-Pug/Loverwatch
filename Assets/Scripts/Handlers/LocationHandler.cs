using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LocationHandler : MonoBehaviour {
    [SerializeField]
    private List<Location> location;
    [SerializeField]
    private Text locationUI;
    [SerializeField]
    private Text deploymentUI;

    private short deploymentNumber = 0;
    private PlayMovie background;

    void Awake ()
    {
        background = GameObject.FindGameObjectWithTag("Background").GetComponent<PlayMovie>();
    }

    public void NextDeployment ()
    {
        deploymentNumber++;
    }

    public string GetDeploymentNumber ()
    {
        string val = NumberToString(deploymentNumber);
        return val;
    }

    string NumberToString (short val)
    {
        string text = "";

        switch (val)
        {
            case 1:
                text = "One";
                break;
            case 2:
                text = "Two";
                break;
            case 3:
                text = "Three";
                break;
            default:
                text = val.ToString();
                break;
        }

        return text;
    }

    public Location GetLocation (Locations val)
    {
        Location found = new Location();
        for(int i = 0; i < location.Count; i++)
        {
            if(location[i].locationEnum == val)
            {
                found = location[i];
            }
        }
        return found;
    }

    public void SetBackground (Locations val)
    {
        MovieTexture mt = new MovieTexture();
        for(int i = 0; i < location.Count; i++)
        {
            if(location[i].locationEnum == val)
            {
                mt = location[i].background;
            }
        }

        background.PlayMovieTexture(mt);

        UpdateUIText(val);
    }

    void UpdateUIText (Locations val)
    {
        string depText = "";
        if(deploymentNumber == 0)
        {
            depText = "Overwatch Headquarters";
        }
        else
        {
            depText = "Deployment " + GetDeploymentNumber();
        }

        deploymentUI.text = depText;
        locationUI.text = GetLocation(val).name;
    }
}
