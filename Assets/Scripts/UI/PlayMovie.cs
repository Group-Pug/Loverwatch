using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {
    public MovieTexture mt;

	// Use this for initialization
	void Start () {
       mt = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
       mt.loop = true;
       mt.Play();
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
