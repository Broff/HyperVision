using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(SkinsController))]
public class SkinEditor : Editor
{
    private ReorderableList skins;
    private bool showSkins = true;

    private void OnEnable()
    {
        skins = new ReorderableList(serializedObject,
                serializedObject.FindProperty("skins"),
                true, true, true, true);
    }

    public override void OnInspectorGUI()
    {
        BuyingCleanButton();

        serializedObject.Update();
        skins.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Skins");
        };
        skins.drawElementCallback = (rect, index, active, focused) =>
        {
            SerializedProperty element = skins.serializedProperty.GetArrayElementAtIndex(index);

            element.FindPropertyRelative("open").boolValue = EditorGUI.Foldout(
                new Rect(rect.x + 10 , rect.y, 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("open").boolValue, "", new GUIStyle(EditorStyles.foldout));

            EditorGUI.LabelField(new Rect(rect.x + 15, rect.y, 70, EditorGUIUtility.singleLineHeight),
                new GUIContent("Index:" + index));
            element.FindPropertyRelative("number").intValue = index;

            EditorGUI.LabelField(new Rect(rect.x + 70, rect.y, 70, EditorGUIUtility.singleLineHeight),
                new GUIContent("Coins cost"));
            EditorGUI.PropertyField(
                new Rect(rect.x + 140, rect.y, 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("cost"), GUIContent.none);

            element.FindPropertyRelative("skin").objectReferenceValue = EditorGUI.ObjectField(
                    new Rect(rect.x + 185, rect.y, EditorGUIUtility.singleLineHeight * 16, EditorGUIUtility.singleLineHeight * 1),
                    GUIContent.none,
                    element.FindPropertyRelative("skin").objectReferenceValue,
                    typeof(GameObject));

           

            if (element.FindPropertyRelative("open").boolValue == true)
            {
                EditorGUI.LabelField(new Rect(rect.x + 10, rect.y + EditorGUIUtility.singleLineHeight * 1.3f, 40, EditorGUIUtility.singleLineHeight),
                new GUIContent("Name"));
                EditorGUI.PropertyField(
                    new Rect(rect.x + 60, rect.y + EditorGUIUtility.singleLineHeight * 1.3f, 100, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("name"), GUIContent.none);
            }
            //EditorGUI.ObjectField(rect, element, GUIContent.none);
        };
        skins.elementHeightCallback = (index) =>
        {
            Repaint();
            float height = 0;
            var element = skins.serializedProperty.GetArrayElementAtIndex(index);
            if (element.FindPropertyRelative("open").boolValue == true)
            {
                height = EditorGUIUtility.singleLineHeight * 3;
            }
            else
            {
                height = EditorGUIUtility.singleLineHeight * 1;
            }

            return height;
        };
        showSkins = EditorGUILayout.Foldout(showSkins, "Skins", new GUIStyle(EditorStyles.foldout));
        ShowMenu(showSkins, skins);
        serializedObject.ApplyModifiedProperties();
    }

    private void BuyingCleanButton(){
        if (GUILayout.Button("Clear buying skins"))
        {
            PlayerPrefs.SetString("skins", "0!0");
            Debug.Log("Buying skins clear!");
        }
    }

    private void ShowMenu(bool b, ReorderableList r)
    {
        if (b == true)
        {
            r.elementHeight = EditorGUIUtility.singleLineHeight;
            r.displayRemove = true;
            r.displayAdd = true;
            r.DoLayoutList();
        }
        else
        {
            r.elementHeight = 0;
            r.displayRemove = false;
            r.displayAdd = false;
        }
    }
}
