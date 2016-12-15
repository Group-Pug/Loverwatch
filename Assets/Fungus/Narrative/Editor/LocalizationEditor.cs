using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Rotorz.ReorderableList;

namespace Fungus
{
	[CustomEditor(typeof(Localization))]
	public class LocalizationEditor : Editor 
	{
		protected SerializedProperty activeLanguageProp;
		protected SerializedProperty localizationFileProp;
        //Mikehack
        protected SerializedProperty rtfDocument;
        protected SerializedProperty characterIds;

        protected virtual void OnEnable()
		{
			activeLanguageProp = serializedObject.FindProperty("activeLanguage");
			localizationFileProp = serializedObject.FindProperty("localizationFile");
            rtfDocument = serializedObject.FindProperty("rtfDocument");
            characterIds = serializedObject.FindProperty("characterIds");
        }

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			Localization localization = target as Localization;

			EditorGUILayout.PropertyField(activeLanguageProp);
			EditorGUILayout.PropertyField(localizationFileProp);

			GUILayout.Space(10);

			EditorGUILayout.HelpBox("Exports a localization csv file to disk. You should save this file in your project assets and then set the Localization File property above to use it.", MessageType.Info);

			if (GUILayout.Button(new GUIContent("Export Localization File")))
			{
				ExportLocalizationFile(localization);
			}

			GUILayout.Space(10);

			EditorGUILayout.HelpBox("Exports all standard text in the scene to a text file for easy editing in a text editor. Use the Import option to read the standard text back into the scene.", MessageType.Info);

			if (GUILayout.Button(new GUIContent("Export Standard Text")))
			{
				ExportStandardText(localization);
			}

			if (GUILayout.Button(new GUIContent("Import Standard Text")))
			{
				ImportStandardText(localization);
			}

            GUILayout.Space(10);

            EditorGUILayout.HelpBox("Mike Hacking Shit", MessageType.Info);

            EditorGUILayout.PropertyField(rtfDocument);
            EditorGUILayout.PropertyField(characterIds, true);

            
            if (GUILayout.Button(new GUIContent("Create New Block")))
            {
                CreateNewBlock();
            }

            serializedObject.ApplyModifiedProperties();
		}

		public virtual void ExportLocalizationFile(Localization localization)
		{
			string path = EditorUtility.SaveFilePanelInProject("Export Localization File",
			                                                   "localization.csv",
			                                                   "csv",
			                                                   "Please enter a filename to save the localization file to");
			if (path.Length == 0) 
			{
				return;
			}

			string csvData = localization.GetCSVData();			
			File.WriteAllText(path, csvData);
			AssetDatabase.ImportAsset(path);

			TextAsset textAsset = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
			if (textAsset != null)
			{
				localization.localizationFile = textAsset;
			}

			ShowNotification(localization);
		}

		public virtual void ExportStandardText(Localization localization)
		{
			string path = EditorUtility.SaveFilePanel("Export Standard Text", "Assets/", "standard.txt", "");
			if (path.Length == 0) 
			{
				return;
			}

			localization.ClearLocalizeableCache();

			string textData = localization.GetStandardText();			
			File.WriteAllText(path, textData);
			AssetDatabase.Refresh();

			ShowNotification(localization);
		}
		
		public virtual void ImportStandardText(Localization localization)
		{
			string path = EditorUtility.OpenFilePanel("Import Standard Text", "Assets/", "txt");
			if (path.Length == 0) 
			{
				return;
			}

            localization.ClearLocalizeableCache();

			string textData = File.ReadAllText(path);
			localization.SetStandardText(textData);

			ShowNotification(localization);
		}

		protected virtual void ShowNotification(Localization localization)
		{
			FlowchartWindow.ShowNotification(localization.notificationText);
			localization.notificationText = "";
		}

        //MIKEHACK
        public virtual void CreateNewBlock ()
        {
            //TODO: fix block position;
            Flowchart flow = FindObjectOfType<Flowchart>();

            //Create block
            Vector2 blockPos = new Vector2(-500, -500);
            Block b = flow.CreateBlock(blockPos);
            
            //load in data
            string textData = FindObjectOfType<Localization>().rtfDocument.text;
            b.blockName = FindObjectOfType<Localization>().rtfDocument.name;
            string[] lines = textData.Split('\n');

            Character lastSpeaker = flow.gameObject.AddComponent(typeof(Character)) as Character; //used to keep track of who's speaking, outside of loop to remember.
            Sprite speakerSprite = null;

            //process line by line
            foreach (string line in lines)
            {
                string buffer = line.Trim();

                if (buffer != "")
                {
                    //title of the block and create a new one
                    if (buffer.StartsWith("TITLE:"))
                    {
                        blockPos = new Vector2(blockPos.x + 250, -500);
                        b = flow.CreateBlock(blockPos);

                        buffer = buffer.Substring(7, buffer.Length - 7); //cuts "TTITLE: " from the front
                        b.blockName = buffer;
                    }
                    else
                    {
                    //fade the scene with default params
                    if(buffer.StartsWith("<<fade>>"))
                    {
                        FadeScreen newCommand = flow.gameObject.AddComponent(typeof(FadeScreen)) as FadeScreen;
                        newCommand.parentBlock = b;
                        newCommand.itemId = flow.NextItemId();
                        b.commandList.Add(newCommand);
                    }
                    else
                    {
                    //if it's a bg change then write the lineID and the buffer
                    if(buffer.StartsWith("<<bg>>"))
                    {
                        //Add Say command
                        Say newCommand = flow.gameObject.AddComponent(typeof(Say)) as Say;
                        newCommand.parentBlock = b;
                        newCommand.itemId = flow.NextItemId();
                        b.commandList.Add(newCommand);
                        //assign text
                        newCommand.storyText = "ADD background to object #" + flow.NextItemId() + " :" + buffer;
                    }
                    else
                    {
                    //description of the block for reference of bgs
                    if (buffer.StartsWith("BG:"))
                    {
                        buffer = buffer.Substring(4, buffer.Length - 4); //cuts "BG: " from the front
                        b.description = buffer;
                    }
                        else
                        {
                            //Check second character is a colon, which would indicate that someone must have been speaking.
                            //we're assuming the id's won't ever be longer than 1
                            //if that's the case we look for the speaker based on the first letter and save that for later
                            if (buffer.Substring(1, 1) == ":")
                            {
                                lastSpeaker = WhoIsSpeaking(buffer.Substring(0, 1));
                                //cuts the identifier + the space from the front
                                buffer = buffer.Substring(3, buffer.Length - 3);
                            }

                            if (buffer.StartsWith("<mei") || buffer.StartsWith("<bastion") || buffer.StartsWith("<soldier") || buffer.StartsWith("<mercy") || buffer.StartsWith("<genji"))
                            {
                                int lasti = buffer.LastIndexOf(">");
                                string spriteName = buffer.Substring(1, lasti - 1);
                                speakerSprite = FindSprite(lastSpeaker, spriteName);
                                if(speakerSprite != null)
                                {
                                            buffer = buffer.Substring(lasti + 1, buffer.Length - lasti - 1);
                                }
                            }

                            //Add Say command
                            Say newCommand = flow.gameObject.AddComponent(typeof(Say)) as Say;
                            newCommand.parentBlock = b;
                            newCommand.itemId = flow.NextItemId();
                            b.commandList.Add(newCommand);
                            //assign text
                            newCommand.storyText = buffer;
                            //assign speaker
                            newCommand.character = lastSpeaker;

                            if(speakerSprite != null)
                            {
                                newCommand.portrait = speakerSprite;
                            }
                            else
                            {
                                if(lastSpeaker != null && lastSpeaker.portraits.Count > 0)
                                {
                                    newCommand.portrait = lastSpeaker.portraits[0];//default
                                }
                            }
                            
                            //remove lastSpeaker object again
                            //TODO: fix this garbage, it's a workaround for the lack of "new" keyword
                            DestroyImmediate(flow.gameObject.GetComponent<Character>());
                        }
                    }
                    }
                    }
                }
            }
        }    

        //Return who is speaking
        private Character WhoIsSpeaking(string val)
        {
            SerializedProperty it = characterIds.Copy(); //buffer the characterIds 'cause well be iterating through them
            while (it.Next(true)) //next is used to iterate literally fkn everything
            {
                //gotta check type to see if it's possible to run next code
                if(it.type == "string")
                {
                    //if it's a string we can check if our start contains 
                    if (it.stringValue == val)
                    {
                        it.Next(false); //if the identifier matches, we take the next value not a child and match it with the character
                        List<Character> actives = Character.activeCharacters;
                        for (int i = 0; i < actives.Count; i++)
                        {
                            if(actives[i].nameText == it.stringValue)
                            {
                                return actives[i];
                            }
                        }
                    }
                }
            }
            return null;
        }

        private Sprite FindSprite (Character speaker, string val)
        {
            
            for(int i = 0; i<speaker.portraits.Count; i++)
            {
                if(speaker.portraits[i].name == val)
                {
                    return speaker.portraits[i];
                }
            }
            return null;
        }
    }
    
}

