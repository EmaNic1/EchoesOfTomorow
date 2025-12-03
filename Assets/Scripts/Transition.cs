using UnityEngine;

public class Transition : MonoBehaviour
{
    Transform destination;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destination = transform.GetChild(1).transform;
    }

    internal void Transition()
    {
        
    }
}
