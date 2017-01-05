using UnityEngine;

public class InboxCloser : MonoBehaviour {
    [SerializeField]
    private Fungus.Flowchart flow;

    private string messageOnClick;

    public void SetMessageToSendTo (string val)
    {
        messageOnClick = val;
    }

    public void MessageFungus ()
    {
        flow.SendFungusMessage(messageOnClick);
    }

}
