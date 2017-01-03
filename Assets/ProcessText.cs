using UnityEngine;
using UnityEngine.UI;

public class ProcessText : MonoBehaviour {

    [SerializeField]
    private Text report;
    [SerializeField]
    private BugReport bugReporter;

    public void SendTextToReporter ()
    {
        bugReporter.SubmitReport(report.text);
        ClosePanel ();
    }

    void ClosePanel ()
    {
        transform.parent.gameObject.SetActive(false);
    }

}
