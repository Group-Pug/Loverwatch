using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InfopadMsg
{
    public string from;
    public string subject;
    [Multiline]
    public string message;
}

public class InboxHandler : MonoBehaviour {
    [SerializeField]
    private InfopadMsg[] bastionEmails;
    [SerializeField]
    private Transform infopadGO;

    private Text fromField;
    private Text subjectField;
    private Text bodyField;
    private InboxCloser ic; 

    void Start ()
    {
        fromField = infopadGO.FindChild("From").GetComponent<Text>();
        subjectField = infopadGO.FindChild("Subject").GetComponent<Text>();
        bodyField = infopadGO.FindChild("Body").GetComponent<Text>();
        ic = infopadGO.FindChild("Confirm").GetComponent<InboxCloser>();
    }

    public void SetEmailBody(string route, int val)
    {
        InfopadMsg temp = new InfopadMsg();

        switch (route)
        {
            case "bastion":
                temp = bastionEmails[val];
                break;
            case "mei":
                break;
            case "genji":
                break;
        }

        fromField.text = "From: " +temp.from;
        subjectField.text = "Subject: " + temp.subject;
        bodyField.text = temp.message;

        ic.SetMessageToSendTo("B_Email" + val);
        
    }

}
