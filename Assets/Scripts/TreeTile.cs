using UnityEngine;

public class TreeTile : MonoBehaviour
{
    public Tree tree;
    public int growStage;
    public int growTimer;
    public SpriteRenderer renderer;
    public Vector3Int position;

    public bool Complete
    {
        get
        {
            if (tree == null) return false;
            return growTimer >= tree.timeToGrow;
        }
    }

    public void Harvested()
    {
        growTimer = 0;
        growStage = 0;
        tree = null;
        renderer.gameObject.SetActive(false);
    }
}
