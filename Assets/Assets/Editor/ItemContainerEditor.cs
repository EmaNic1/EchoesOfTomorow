using UnityEditor;
using UnityEngine;

/// <summary>
/// papildomas mygtukas inspector lange
/// </summary>

[CustomEditor(typeof(ItemContainer))]//atributas naudojamas inspektoriuje
public class ItemContainerEditor : Editor
{
    /// <summary>
    /// isvalo visa inventoriu vienu mygtuko paspaudimu
    /// </summary>
    public override void OnInspectorGUI()
    {
        ItemContainer container = target as ItemContainer;
        if (GUILayout.Button("Clear container."))
        {
            for(int i = 0; i < container.slot.Count; i++)
            {
                container.slot[i].Clear();
            }
        }
        DrawDefaultInspector();
    }
}
