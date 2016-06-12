using UnityEngine;

public class AccessibilityOptions : MonoBehaviour {
    private bool dyslexicFont = false;

    public void SetDyslexicFont (bool val)
    {
        dyslexicFont = val;
    }

    public bool GetDyslexicFont ()
    {
        return dyslexicFont;
    }
}
