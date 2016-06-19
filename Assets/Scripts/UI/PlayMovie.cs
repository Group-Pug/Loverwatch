using UnityEngine;

//fuckit sticking with the name
public class PlayMovie : MonoBehaviour {
    private MovieTexture mt;

	void Start () {
        mt = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
        mt.loop = true;
        mt.Play();
    }

    public void PlayMovieTexture (MovieTexture val)
    {
        mt = val;
        GetComponent<Renderer>().material.mainTexture = mt;
        mt.Play();
    }

}
