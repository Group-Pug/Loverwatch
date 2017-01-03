using UnityEngine;
using System.IO;
using System;
using Fungus;
using System.Collections;

public class BugReport : MonoBehaviour {
    [SerializeField]
    private Flowchart flow;
    [SerializeField]
    private GameObject qaPanel;
    [SerializeField]
    private DialogInput sayDia;

    private string documentsFolder;
    private int problemId;

    void Start ()
    {
        documentsFolder = Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\loverwatchBugs.txt";
    }
	
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.F9))
        {
            problemId = flow.selectedBlock.activeCommand.itemId;
            qaPanel.SetActive(!qaPanel.activeSelf);
            toggleDialogo();
        }
	}

    public void SubmitReport (string val)
    {
        File.AppendAllText(documentsFolder, "Line: " + problemId + " " + val + Environment.NewLine);
        toggleDialogo();
    }

    void toggleDialogo ()
    {
        sayDia.enabled = !sayDia.enabled;
    }



}
