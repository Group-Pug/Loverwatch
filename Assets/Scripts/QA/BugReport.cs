using UnityEngine;
using System.IO;
using System;
using Fungus;

public class BugReport : MonoBehaviour {
    [SerializeField]
    private Flowchart flow;
    [SerializeField]
    private GameObject qaPanel;
    [SerializeField]
    private DialogInput sayDia;

    private string documentsFolder;
    private int problemId = 0;

    private int hackedCurrentBlock;

    void Start ()
    {
        documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/loverwatchBugs.txt";
    }
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.F9))
        {
            problemId = hackedCurrentBlock;
            qaPanel.SetActive(!qaPanel.activeSelf);
            toggleDialogo();
        }
	}

    public void SubmitReport (string val)
    {
        StreamWriter sw = File.AppendText(documentsFolder);

        sw.WriteLine("Line: " + problemId + " " + val);

        sw.Close();

        //File.AppendAllText(documentsFolder, "Line: " + problemId + " " + val + Environment.NewLine);
        toggleDialogo();
    }

    void toggleDialogo ()
    {
        sayDia.enabled = !sayDia.enabled;
    }

    public void SetCurrentBlock (int val)
    {
        hackedCurrentBlock = val;
    }

}
