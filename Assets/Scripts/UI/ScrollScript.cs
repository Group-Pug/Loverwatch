using UnityEngine;
using System.Collections;

public class ScrollScript : MonoBehaviour
{
    public RectTransform[] content;
    public Vector2 scrollDirection;
    public float offset;

    void Update()
    {
        foreach(RectTransform rt in content)
        {
            scrollContent(rt);
        }
    }

    void scrollContent(RectTransform rt)
    {
        Vector3 contentPos = rt.localPosition ;

        //move
        contentPos.x += scrollDirection.x * Time.deltaTime;

        if ((scrollDirection.x > 0) && (contentPos.x) >  rt.rect.width + offset)
        {
            contentPos.x = -rt.rect.width - offset;
        }

        if ((scrollDirection.x < 0) && (contentPos.x) < -rt.rect.width - offset)
        {
            contentPos.x = rt.rect.width + offset;
        }

        //set position
        rt.localPosition = contentPos;
    }
}