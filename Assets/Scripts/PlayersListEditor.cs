#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;

[CustomEditor(typeof(PlayersList))]
public class PlayersListEditor : Editor 
{
	private ReorderableList list;
    private ReorderableList list_2;

	private void OnEnable() 
	{
		list = new ReorderableList(serializedObject, 
			serializedObject.FindProperty("Players"), // Elements
			true, // Dragable
			true, // Display Header
			true, // Display Add Button
			true);// Display Remove Button

		list.elementHeight = EditorGUIUtility.singleLineHeight + 4;

		list.drawElementCallback = 
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			int ind = index + 1;

			string indStr;
			indStr = ind % 10 == 0 ? ind.ToString() + "   #####" : ind.ToString();

			//Player № "#":
			EditorGUI.LabelField(new Rect(
				rect.x,
				rect.y,
				100, 
				EditorGUIUtility.singleLineHeight),
				indStr);

			EditorGUI.PropertyField(new Rect(
				rect.x + 100,
				rect.y,
				rect.width - 100 - 2, 
				EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("player"),
				GUIContent.none);
		};

		list.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(rect, "Players List. Count = " + list.count.ToString());
		};

		list.onCanRemoveCallback = (ReorderableList l) => {
			return l.count > 1;
		};


        list_2 = new ReorderableList(serializedObject,
        serializedObject.FindProperty("Players_2"), // Elements
        true, // Dragable
        true, // Display Header
        true, // Display Add Button
        true);// Display Remove Button

        list_2.elementHeight = EditorGUIUtility.singleLineHeight + 4;

        list_2.drawElementCallback =
            (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = list_2.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                int ind = index + 1;

                string indStr;
                indStr = ind % 10 == 0 ? ind.ToString() + "   #####" : ind.ToString();

                //Player № "#":
                EditorGUI.LabelField(new Rect(
                    rect.x,
                    rect.y,
                    100,
                    EditorGUIUtility.singleLineHeight),
                    indStr);

                EditorGUI.PropertyField(new Rect(
                    rect.x + 100,
                    rect.y,
                    rect.width - 100 - 2,
                    EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("player"),
                    GUIContent.none);
            };

        list_2.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Players List. Count = " + list_2.count.ToString());
        };

        list_2.onCanRemoveCallback = (ReorderableList l) =>
        {
            return l.count > 1;
        };
	}
	
	public override void OnInspectorGUI() 
	{
		serializedObject.Update();
		list.DoLayoutList();
		//serializedObject.ApplyModifiedProperties();



        //serializedObject.Update();
        list_2.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
	}

	private void clickHandler(object target) 
	{
		var index = list.serializedProperty.arraySize;
		list.serializedProperty.arraySize++;
		list.index = index;



        var index_2 = list_2.serializedProperty.arraySize;
        list_2.serializedProperty.arraySize++;
        list_2.index = index_2;
	}
}
#endif