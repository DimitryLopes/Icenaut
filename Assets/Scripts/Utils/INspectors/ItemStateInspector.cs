using UnityEditor;
using UnityEngine;

//[CustomEditor(typeof(MultiStateItemData))]
public class ItemStateInspector : Editor
{
    private bool[] itemFoldouts;
    public override void OnInspectorGUI()
    {
        MultiStateItemData data = (MultiStateItemData)target;

        if (itemFoldouts == null || itemFoldouts.Length != data.States.Count)
        {
            itemFoldouts = new bool[data.States.Count];
        }

        for (int i = 0; i < data.States.Count; i++)
        {
            ItemState state = data.States[i];

            if (state != null)
            {
                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.margin = new RectOffset(10, 10, 5, 5);
                boxStyle.padding = new RectOffset(10, 10, 10, 10);

                EditorGUILayout.BeginVertical(boxStyle);

                itemFoldouts[i] = EditorGUILayout.Foldout(itemFoldouts[i], state.name, true);

                if (itemFoldouts[i])
                {
                    EditorGUI.indentLevel++;

                    if (state is UnusedItemState unusedItemState)
                    {
                        EditorGUILayout.LabelField("On Interaction Enabled Tooltip");
                        unusedItemState.OnInteractionEnabledTooltip = EditorGUILayout.TextField(string.Empty, unusedItemState.OnInteractionEnabledTooltip);
                    }
                    else if (state is UsedItemState usedItemState)
                    {

                    }
                }
                serializedObject.ApplyModifiedProperties();
                EditorGUILayout.EndVertical();
            }
        }

        base.OnInspectorGUI();
    }
}