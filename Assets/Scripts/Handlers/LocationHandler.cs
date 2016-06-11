using UnityEngine;
using System.Collections.Generic;

public class LocationHandler : MonoBehaviour {
    [SerializeField]
    private List<Location> location;

    private short deploymentNumber = 1;

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
}
