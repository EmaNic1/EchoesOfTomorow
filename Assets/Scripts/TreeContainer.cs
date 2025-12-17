using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tree Container")]
public class TreeContainer : ScriptableObject
{
    public List<TreeTile> trees;

    public TreeTile Get(Vector3Int position)
    {
        return trees.Find(x => x.position == position);
    }

    public void Add(TreeTile tree)
    {
        trees.Add(tree);
    }

    
}
