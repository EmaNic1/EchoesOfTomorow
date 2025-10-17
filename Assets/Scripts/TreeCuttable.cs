using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class TreeCuttable : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;



    public override void Hit()
    {
        // As long as there are still dropCount remaining, the loop continues.
        // dropCount-- – decrements the number of drops remaining.
        while (dropCount > 0)
        {
            dropCount--;

            // The starting position is where the tree stands.
            Vector3 position = transform.position;

            // position.x and position.y are changed randomly so that the items are not in one place but scattered around the tree.
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            // the position is set to the calculated position
            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;
        }

        // Destroy the tree
        Destroy(gameObject);
    }
}