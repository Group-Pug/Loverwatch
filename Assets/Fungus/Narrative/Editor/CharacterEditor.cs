using UnityEditor;
using UnityEngine;
using System.Collections;
using Rotorz.ReorderableList;
using System.Collections.Generic;

namespace Fungus
{

	[CustomEditor (typeof(Character))]
	public class CharacterEditor : Editor
	{
		protected SerializedProperty nameTextProp;
        protected SerializedProperty fullNameProp;
        protected SerializedProperty professionProp;
        protected SerializedProperty nameColorProp;
		protected SerializedProperty soundEffectProp;
		protected SerializedProperty portraitsProp;
		protected SerializedProperty portraitsFaceProp;
		protected SerializedProperty descriptionProp;
		protected SerializedProperty setSayDialogProp;

		protected virtual void OnEnable()
		{
			nameTextProp = serializedObject.FindProperty ("nameText");
            fullNameProp = serializedObject.FindProperty("fullName");
            professionProp = serializedObject.FindProperty("profession");
            nameColorProp = serializedObject.FindProperty ("nameColor");
			soundEffectProp = serializedObject.FindProperty ("soundEffect");
			portraitsProp = serializedObject.FindProperty ("portraits");
			portraitsFaceProp = serializedObject.FindProperty ("portraitsFace");
			descriptionProp = serializedObject.FindProperty ("description");
			setSayDialogProp = serializedObject.FindProperty("setSayDialog");
		}

		public override void OnInspectorGUI() 
		{
			serializedObject.Update();

			Character t = target as Character;

			EditorGUILayout.PropertyField(nameTextProp, new GUIContent("Name Text", "Name of the character display in the dialog"));
            EditorGUILayout.PropertyField(fullNameProp, new GUIContent("Full Name", "Full name of the character display in the dialog"));
            EditorGUILayout.PropertyField(professionProp, new GUIContent("Profession", "Profession of the character"));
            EditorGUILayout.PropertyField(nameColorProp, new GUIContent("Name Color", "Color of name text display in the dialog"));
			EditorGUILayout.PropertyField(soundEffectProp, new GUIContent("Sound Effect", "Sound to play when the character is talking. Overrides the setting in the Dialog."));
			EditorGUILayout.PropertyField(setSayDialogProp);
			EditorGUILayout.PropertyField(descriptionProp, new GUIContent("Description", "Notes about this story character (personality, attibutes, etc.)"));

			if (t.portraits != null &&
				t.portraits.Count > 0)
			{
				t.profileSprite = t.portraits[0];
			}
			else
			{
				t.profileSprite = null;
			}
			
			if (t.profileSprite != null)
			{
				Texture2D characterTexture = t.profileSprite.texture;
				float aspect = (float)characterTexture.width / (float)characterTexture.height;
				Rect previewRect = GUILayoutUtility.GetAspectRect(aspect, GUILayout.Width(100), GUILayout.ExpandWidth(true));
				if (characterTexture != null)
					GUI.DrawTexture(previewRect,characterTexture,ScaleMode.ScaleToFit,true,aspect);
			}

			ReorderableListGUI.Title(new GUIContent("Portraits", "Character image sprites to display in the dialog"));
			ReorderableListGUI.ListField(portraitsProp);

			EditorGUILayout.HelpBox("All portrait images should use the exact same resolution to avoid positioning and tiling issues.", MessageType.Info);

			EditorGUILayout.Separator();

			string[] facingArrows = new string[]
			{
				"FRONT",
				"<--",
				"-->",
			};
			portraitsFaceProp.enumValueIndex = EditorGUILayout.Popup("Portraits Face", (int)portraitsFaceProp.enumValueIndex, facingArrows);

			EditorGUILayout.Separator();

			EditorUtility.SetDirty(t);

			serializedObject.ApplyModifiedProperties();
		}

	}

}